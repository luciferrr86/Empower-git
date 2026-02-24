using A4.BAL;
using A4.DAL.Entites;
using DAL;
using DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;

namespace A4.DAL.Repositories
{
    public class TimeSheetModuleRepository : Repository<TimesheetUserDetail>, ITimeSheetModuleRepository
    {

        public TimeSheetModuleRepository(DbContext context) : base(context)
        {

        }
        private ApplicationDbContext _appContext => (ApplicationDbContext)_context;

        DateTime weekStartDate;
        DateTime weekEndDate;
        bool bIsManagerApprove = false;
        bool bIsUserSubmit = false;
        bool IsWeekly = false;

        public Tuple<DateTime, DateTime> GetDateOfWeek()
        {
            string dayOfWeek = DateTime.Today.DayOfWeek.ToString().ToLower();
            int offSet = 0;
            switch (dayOfWeek)
            {
                case "sunday":
                    offSet = 0;
                    break;
                case "monday":
                    offSet = 1;
                    break;
                case "tuesday":
                    offSet = 2;
                    break;
                case "wednesday":
                    offSet = 3;
                    break;
                case "thursday":
                    offSet = 4;
                    break;
                case "friday":
                    offSet = 5;
                    break;
                case "saturday":
                    offSet = 6;
                    break;
            }
            DateTime startWeek = DateTime.Now.AddDays(-offSet);
            DateTime endWeek = startWeek.AddDays(6);
            return Tuple.Create(startWeek, endWeek);
        }

        public Tuple<TimesheetUserConfigModel, List<AllottedDaysModel>> GetTimesheetConfig(Guid userId, DateTime startingDate, DateTime endingDate)
        {
            var config = new TimesheetUserConfigModel();
            List<AllottedDaysModel> lstAllottedDays = new List<AllottedDaysModel>();

            var shedule = _appContext.TimesheetUserSchedule.Where(m => m.EmployeeId == userId).FirstOrDefault();
            if (shedule != null)
            {
                var template = _appContext.TimesheetTemplate.Where(m => m.Id == shedule.TimesheetTemplateId).FirstOrDefault();
                if (template != null)
                    config.StartDate = shedule.StartDate;
                config.Frequency = shedule.ScheduleType;
                lstAllottedDays.Add(new AllottedDaysModel { IsAllotted = template.Sunday });
                lstAllottedDays.Add(new AllottedDaysModel { IsAllotted = template.Monday });
                lstAllottedDays.Add(new AllottedDaysModel { IsAllotted = template.Tuesday });
                lstAllottedDays.Add(new AllottedDaysModel { IsAllotted = template.Wednesday });
                lstAllottedDays.Add(new AllottedDaysModel { IsAllotted = template.Thursday });
                lstAllottedDays.Add(new AllottedDaysModel { IsAllotted = template.Friday });
                lstAllottedDays.Add(new AllottedDaysModel { IsAllotted = template.Saturday });
            }
            else
            {
                config.StartDate = DateTime.Now.Date;
                config.Frequency = "N/A";
                lstAllottedDays.Add(new AllottedDaysModel { IsAllotted = false });
                lstAllottedDays.Add(new AllottedDaysModel { IsAllotted = false });
                lstAllottedDays.Add(new AllottedDaysModel { IsAllotted = false });
                lstAllottedDays.Add(new AllottedDaysModel { IsAllotted = false });
                lstAllottedDays.Add(new AllottedDaysModel { IsAllotted = false });
                lstAllottedDays.Add(new AllottedDaysModel { IsAllotted = false });
                lstAllottedDays.Add(new AllottedDaysModel { IsAllotted = false });
            }

            return Tuple.Create(config, lstAllottedDays);
        }


        public List<DateTime> GetDateList(DateTime startingDate, DateTime endingDate)
        {
            if (startingDate > endingDate)
            {
                return null;
            }
            List<DateTime> daylist = new List<DateTime>();
            DateTime tmpDate = startingDate;
            do
            {
                daylist.Add(tmpDate);
                tmpDate = tmpDate.AddDays(1);
            } while (tmpDate <= endingDate);

            return daylist;
        }

        public List<TimesheetEmployeeProject> GetddlProject(Guid employeeId)
        {
            var query = from empProject in _appContext.TimesheetEmployeeProject.Where(m => m.EmployeeId == employeeId && m.IsActive == true)
                        select new TimesheetEmployeeProject
                        {
                            TimesheetProject = new TimesheetProject { Id = empProject.TimesheetProject.Id, Name = empProject.TimesheetProject.ProjectName }
                        };
            return query.ToList();

        }

        public Tuple<List<DayListModel>, List<ProjectListModel>> GetTimeSheetData(List<DateTime> dateLists, List<AllottedDaysModel> lstAllottedDays, Employee employee, bool frequency, bool IsManager, Guid timeSheetSpanId)
        {
            var dayList = new List<DayListModel>();
            var projectList = new List<ProjectListModel>();
            int totalTimeMinute = 0;

            var ddlproject = GetddlProject(employee.Id);
            if (dateLists.Count() > 0 && lstAllottedDays.Count() > 0 && ddlproject.Count() > 0)
            {
                var allweekdate = dateLists.Zip(lstAllottedDays, (date, isAllowed) => new { date = date, isAllowed = isAllowed });
                var userdetalis = _appContext.TimesheetUserDetail.Where(m => m.TimesheetUserSpanId == timeSheetSpanId && m.IsActive == true).ToList();

                if (allweekdate.Count() > 0)
                {
                    if (userdetalis.Count() > 0)
                    {
                        var query = from weekdate in allweekdate
                                    join userdetali in userdetalis on weekdate.date.ToShortDateString() equals userdetali.TimeSheetDate.Date.ToShortDateString() into JoinedDateUserDetail
                                    from b in JoinedDateUserDetail.DefaultIfEmpty()
                                    select new { weekdate, b };
                        foreach (var item in query)
                        {
                            if (item.b != null)
                            {
                                dayList.Add(new DayListModel
                                {
                                    Date = item.weekdate.date,
                                    Day = item.weekdate.date.DayOfWeek.ToString(),
                                    IsAllotted = item.weekdate.isAllowed.IsAllotted,
                                    IsAllow = (frequency) ? true : (item.weekdate.date < DateTime.Now) ? true : false,
                                    IsManagerApproved = item.b.bManagerApproved,
                                    IsUserSaved = item.b.bIsUserSaved,
                                    IsUserSubmit = item.b.bIsUserSubmit,
                                    UserDetailId = item.b.Id.ToString()

                                });

                            }
                            else
                            {
                                dayList.Add(new DayListModel
                                {
                                    Date = item.weekdate.date,
                                    Day = item.weekdate.date.DayOfWeek.ToString(),
                                    IsAllotted = item.weekdate.isAllowed.IsAllotted,
                                    IsAllow = (frequency) ? true : (item.weekdate.date < DateTime.Now) ? true : false

                                });
                            }
                        }

                    }
                    else
                    {

                        foreach (var item in allweekdate)
                        {
                            var dayModel = new DayListModel
                            {
                                Date = item.date,
                                Day = item.date.DayOfWeek.ToString(),
                                IsAllotted = item.isAllowed.IsAllotted,
                                IsAllow = (frequency) ? true : (item.date < DateTime.Now) ? true : false
                            };
                            var userDetailId = AddTimesheetUserDetails("", dayModel, timeSheetSpanId, employee.ManagerId);
                            if (userDetailId != null) dayModel.UserDetailId = userDetailId.ToString();
                            dayList.Add(dayModel);
                        }
                    }

                    foreach (var item in ddlproject)
                    {
                        var projectHour = new List<ProjectHour>();
                        totalTimeMinute = 0;
                        foreach (var daylist in dayList)
                        {

                            if (!string.IsNullOrEmpty(daylist.UserDetailId) && daylist.UserDetailId != "00000000-0000-0000-0000-000000000000")
                            {
                                var hours = _appContext.TimesheetUserDetailProjectHour.Where(m => m.ProjectId == item.TimesheetProject.Id && m.TimesheetUserDetailId == Guid.Parse(daylist.UserDetailId) && m.IsActive == true).ToList();
                                if (hours.Count() > 0)
                                {
                                    foreach (var projecthours in hours)
                                    {
                                        totalTimeMinute += CalculateTimeInMinutes(projecthours.Hour);
                                        projectHour.Add(new ProjectHour { Hour = projecthours.Hour, IsAllow = daylist.IsAllow, IsAllotted = daylist.IsAllotted, UserDetailProjectHourId = projecthours.Id.ToString() });
                                    }
                                }
                                else
                                {
                                    projectHour.Add(new ProjectHour { Hour = "00:00", IsAllow = daylist.IsAllow, IsAllotted = daylist.IsAllotted });
                                }
                            }
                            else
                            {

                                projectHour.Add(new ProjectHour { Hour = "00:00", IsAllow = daylist.IsAllow, IsAllotted = daylist.IsAllotted });

                            }
                        }
                        projectList.Add(new ProjectListModel { TotalProjectHour = string.Format("{0:00}:{1:00}", (int)TimeSpan.FromMinutes(totalTimeMinute).TotalHours, TimeSpan.FromMinutes(totalTimeMinute).Minutes), ProjectId = item.TimesheetProject.Id.ToString(), Name = item.TimesheetProject.Name, ProjectHourList = projectHour });
                    }
                }
            }
            return Tuple.Create(dayList, projectList);
        }

        public bool CheckTimesheetConfig(Guid employyeId)
        {
            if (employyeId != Guid.Empty)
            {
                var empProject = _appContext.TimesheetEmployeeProject.Any(m => m.EmployeeId == employyeId && m.IsActive == true);
                var userSchedule = _appContext.TimesheetUserSchedule.Any(m => m.EmployeeId == employyeId && m.IsActive == true);
                if (empProject && userSchedule)
                {
                    return true;
                }
                return false;
            }
            else
            {
                return false;
            }

        }

        public EmployeeTimeSheetModel GetWeeklyTimeSheet(Guid employeeId, Guid spanId)
        {

            var model = new EmployeeTimeSheetModel();
            var timesheetSpan = _appContext.TimesheetUserSpan.Where(m => m.Id == spanId).FirstOrDefault();
            if (timesheetSpan != null)
            {
                weekStartDate = timesheetSpan.WeekStartDate;
                weekEndDate = timesheetSpan.WeekEndDate;
                model.UserSpanId = timesheetSpan.Id.ToString();
                bIsManagerApprove = timesheetSpan.bIsManagerApprove;
                bIsUserSubmit = timesheetSpan.bIsUserSubmit;
                if (timesheetSpan.TotalHour != null)
                {
                    model.TotalHour = timesheetSpan.TotalHour;
                }
            }
            var dateList = GetDateList(weekStartDate, weekEndDate);

            var config = GetTimesheetConfig(employeeId, Convert.ToDateTime(weekStartDate), Convert.ToDateTime(weekEndDate));
            if (config.Item1.Frequency == "Daily")
            {
                IsWeekly = false;
            }
            model.Frequency = config.Item1.Frequency;
            var query = from employee in _appContext.Employee.Where(m => m.Id == employeeId && m.IsActive == true)
                        select new Employee
                        {
                            Id = employee.Id,
                            ManagerId = employee.ManagerId,
                            ApplicationUser = new ApplicationUser { FullName = employee.ApplicationUser.FullName },
                            FunctionalDesignation = new FunctionalDesignation { Name = employee.FunctionalDesignation.Name }
                        };

            var emp = query.FirstOrDefault();
            if (emp != null)
            {

                model.FullName = emp.ApplicationUser.FullName;
                model.Designation = emp.FunctionalDesignation.Name;
                model.ApproverlId = emp.ManagerId.ToString();

            }
            var TimeSheetData = GetTimeSheetData(dateList, config.Item2, emp, IsWeekly, false, Guid.Parse(model.UserSpanId));
            if (TimeSheetData.Item1.Count() > 0 && TimeSheetData.Item2.Count() > 0)
            {

                model.DayList = TimeSheetData.Item1;
                model.ProjectList = TimeSheetData.Item2;
            }

            //
            bool IsManger = TimeSheetData.Item1.All(m => m.IsManagerApproved == true);
            if (IsManger && bIsManagerApprove)
            {
                model.IsManagerApproved = true;
            }
            else
            {
                model.IsManagerApproved = false;
            }
            bool IsSubmit = TimeSheetData.Item1.All(m => m.IsUserSubmit == true);
            if ((IsSubmit && bIsUserSubmit) || (!IsManger && !bIsManagerApprove))
            {
                model.IsUserSubmit = true;
            }
            else if ((IsSubmit && bIsUserSubmit) || (IsManger && bIsManagerApprove))
            {
                model.IsUserSubmit = true;
            }
            else if ((!IsSubmit && !bIsUserSubmit) || (IsManger && bIsManagerApprove))
            {
                model.IsUserSubmit = true;
            }
            else
            {
                model.IsUserSubmit = false;
            }

            bool IsSave = TimeSheetData.Item1.All(m => m.IsUserSaved == true);
            if (IsSave)
            {
                model.IsUserSaved = true;
            }
            else
            {
                model.IsUserSaved = false;
            }


            

            var lstUserWeeks = GetTimesheetUserWeeks(employeeId);
            if (lstUserWeeks.Count > 0)
            {
                model.lstUserWeeks = lstUserWeeks;
            }
            return model;
        }

        public Guid AddTimesheetUserDetails(string type, DayListModel item, Guid userSpanId, Guid ApproverlId)
        {
            var timesheetUserDetail = new TimesheetUserDetail();
            timesheetUserDetail.TimeSheetDate = item.Date;
            timesheetUserDetail.Day = item.Day;
            timesheetUserDetail.TimesheetUserSpanId = userSpanId;
            timesheetUserDetail.ApproverlId = ApproverlId;
            if (item.IsAllotted == true && item.IsAllow == true)
            {
                timesheetUserDetail.bIsUserSaved = true;
            }

            if (type.Equals("Submit"))
            {
                timesheetUserDetail.bIsUserSaved = true;
                timesheetUserDetail.bIsUserSubmit = true;
            }
            _appContext.TimesheetUserDetail.Add(timesheetUserDetail);
            _appContext.SaveChanges();
            return timesheetUserDetail.Id;
        }

        public EmployeeTimeSheetModel GetEmployeeTimeSheet(Guid employeeId, Guid spanId)
        {
            var model = new EmployeeTimeSheetModel();
            if (spanId != Guid.Empty)
            {
                var checkSpan = _appContext.TimesheetUserSpan.Where(m => m.Id == spanId).FirstOrDefault();
                if (checkSpan != null)
                {
                    weekStartDate = checkSpan.WeekStartDate;
                    weekEndDate = checkSpan.WeekEndDate;
                    model.UserSpanId = checkSpan.Id.ToString();
                    bIsManagerApprove = checkSpan.bIsManagerApprove;
                    bIsUserSubmit = checkSpan.bIsUserSubmit;
                    if (checkSpan.TotalHour != null)
                    {
                        model.TotalHour = checkSpan.TotalHour;
                    }

                }
            }
            else
            {
                var weekDate = GetDateOfWeek();
                weekStartDate = weekDate.Item1;
                weekEndDate = weekDate.Item2;
                var timesheetSpan = _appContext.TimesheetUserSpan.Where(m => m.EmployeeId == employeeId && m.WeekStartDate.Date == weekStartDate.Date && m.WeekEndDate.Date == weekEndDate.Date && m.IsActive == true).FirstOrDefault();
                if (timesheetSpan == null)
                {
                    var userSpan = new TimesheetUserSpan();
                    userSpan.WeekStartDate = weekStartDate;
                    userSpan.WeekEndDate = weekEndDate;
                    userSpan.EmployeeId = employeeId;
                    userSpan.bIsManagerApprove = false;
                    userSpan.bIsUserSubmit = false;
                    userSpan.Year = DateTime.Now.Year.ToString();
                    userSpan.Month = DateTime.Now.Month.ToString();
                    _appContext.TimesheetUserSpan.Add(userSpan);
                    _appContext.SaveChanges();
                    model.UserSpanId = userSpan.Id.ToString();
                    bIsManagerApprove = false;
                    bIsUserSubmit = false;
                    model.TotalHour = "00:00";

                }
                else
                {
                    model.UserSpanId = timesheetSpan.Id.ToString();
                    bIsManagerApprove = timesheetSpan.bIsManagerApprove;
                    bIsUserSubmit = timesheetSpan.bIsUserSubmit;
                    if (timesheetSpan.TotalHour != null)
                    {
                        model.TotalHour = timesheetSpan.TotalHour;
                    }
                }

            }
            var dateList = GetDateList(weekStartDate, weekEndDate);
            var config = GetTimesheetConfig(employeeId, Convert.ToDateTime(weekStartDate), Convert.ToDateTime(weekEndDate));
            if (config.Item1.Frequency == "Daily")
            {
                IsWeekly = false;
            }
            model.Frequency = config.Item1.Frequency;

            var query = from e1 in _appContext.Employee
                        join e2 in _appContext.Employee on e1.ManagerId equals e2.Id
                        join appuser1 in _appContext.Users on e1.UserId equals appuser1.Id
                        join appuser2 in _appContext.Users on e2.UserId equals appuser2.Id
                        where e1.Id == employeeId && e1.IsActive == true
                        select new Employee { Id = e1.Id, ApplicationUser = new ApplicationUser { FullName = appuser1.FullName, Email = appuser1.Email, Id = appuser1.Id }, ManagerId = e2.Id, ManagerName = appuser2.FullName, ManagerEmail = appuser2.Email, FunctionalDesignation = new FunctionalDesignation { Name = e1.FunctionalDesignation.Name } };

            var emp = query.FirstOrDefault();
            if (emp != null)
            {

                model.FullName = emp.ApplicationUser.FullName;
                model.Designation = emp.FunctionalDesignation.Name;
                model.ApproverlId = emp.ManagerId.ToString();
                model.EmployeeId = emp.ApplicationUser.Id;
                model.MangerEmail = emp.ManagerEmail;
                model.MangerName = emp.ManagerName;
                model.EmployeeEmail = emp.ApplicationUser.Email;
            }



            var TimeSheetData = GetTimeSheetData(dateList, config.Item2, emp, IsWeekly, false, Guid.Parse(model.UserSpanId));
            if (TimeSheetData.Item1.Count() > 0 && TimeSheetData.Item2.Count() > 0)
            {
                model.DayList = TimeSheetData.Item1;
                model.ProjectList = TimeSheetData.Item2;
            }
            //TimeSheetData.Item2
            //assgin 
            bool IsSubmit = false;
            bool IsManger = false;


            if (config.Item1.Frequency == "Daily")
            {
                var submitdata = TimeSheetData.Item1.Where(m => m.IsAllow == true).ToList();
                IsSubmit = submitdata.All(m => m.IsUserSubmit == true);
                if ((IsSubmit) && (!model.IsManagerApproved))
                {
                    model.IsUserSubmit = true;
                }
                else if ((IsSubmit) && (model.IsManagerApproved))
                {
                    model.IsUserSubmit = true;
                }
                else if ((!IsSubmit) && (model.IsManagerApproved))
                {
                    model.IsUserSubmit = true;
                }
                else
                {
                    model.IsUserSubmit = false;
                }
                model.IsUserSaved = model.IsUserSubmit;

                var IsMangerdata = TimeSheetData.Item1.Where(m => m.IsManagerApproved == true).ToList();
                IsManger = submitdata.All(m => m.IsManagerApproved == true);
                if (IsManger && bIsManagerApprove)
                {
                    model.IsManagerApproved = true;
                }
                else
                {
                    model.IsManagerApproved = false;
                }
            }
            else
            {
                IsSubmit = TimeSheetData.Item1.Any(m => m.IsUserSubmit == true && m.IsAllow == true);
                if ((IsSubmit && bIsUserSubmit) && (!model.IsManagerApproved))
                {
                    model.IsUserSubmit = true;
                }
                else if ((IsSubmit && bIsUserSubmit) && (model.IsManagerApproved))
                {
                    model.IsUserSubmit = true;
                }
                else if ((!IsSubmit && !bIsUserSubmit) && (model.IsManagerApproved))
                {
                    model.IsUserSubmit = true;
                }
                else
                {
                    model.IsUserSubmit = false;
                }
                //IsSave
                bool IsSave = TimeSheetData.Item1.All(m => m.IsUserSaved == true);
                if (IsSave)
                {
                    model.IsUserSaved = true;
                }
                else
                {
                    model.IsUserSaved = false;
                }
                //IsManger
                IsManger = TimeSheetData.Item1.All(m => m.IsManagerApproved == true);
                if (IsManger && bIsManagerApprove)
                {
                    model.IsManagerApproved = true;
                }
                else
                {
                    model.IsManagerApproved = false;
                }
            }

            
            var lstUserWeeks = GetTimesheetUserWeeks(employeeId);
            if (lstUserWeeks.Count > 0)
            {
                model.lstUserWeeks = lstUserWeeks;
            }
            return model;
        }

        public List<TimesheetUserDetail> GetEmployeeWorkingDays(Guid employeeId, int month, int year)
        {
            var userDetail = new List<TimesheetUserDetail>();
            var startDate = new DateTime(year, month, 1);
            var lastDayOfMonth = DateTime.DaysInMonth(startDate.Year, startDate.Month);
            var endDate = new DateTime(year, month, lastDayOfMonth);

            var dateList = GetDateList(startDate, endDate);
            var userWorkingDays = _appContext.TimesheetUserDetail.Where(m => m.TimesheetUserSpan.EmployeeId == employeeId && m.TimesheetUserSpan.WeekStartDate.Date > startDate.Date.AddDays(-6) && m.TimesheetUserSpan.WeekEndDate.Date < endDate.Date.AddDays(6) && m.TimesheetUserSpan.IsActive).Include(q => q.TimesheetUserSpan).ToList();

            //m.TimesheetUserSpan.bIsUserSubmit == true && 

            return userWorkingDays;
        }

        public List<DropDownList> GetTimesheetUserWeeks(Guid employeeId)
        {
            var lstWeekDate = new List<DropDownList>();
            var spandata = _appContext.TimesheetUserSpan.Where(m => m.IsActive == true && m.EmployeeId == employeeId).OrderByDescending(m => m.CreatedDate).Take(5);
            if (spandata.Count() > 0)
            {
                foreach (var item in spandata)
                {
                    lstWeekDate.Add(new DropDownList { Label = item.WeekStartDate.Date.ToShortDateString() + "-" + item.WeekEndDate.Date.ToShortDateString(), Value = item.Id.ToString() });

                }
            }
            return lstWeekDate;
        }

        private int CalculateTimeInMinutes(string input)
        {
            DateTime time = DateTime.Parse(input, new CultureInfo("en-US"));
            return (int)(time - time.Date).TotalMinutes;
        }

        
    }
}
