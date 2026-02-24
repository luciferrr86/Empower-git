using A4.BAL;
using A4.DAL.Entites;
using DAL;

using DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
//using System.Data.SqlClient;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace A4.DAL.Repositories
{

    public class LeaveManagementRepository : Repository<EmployeeLeaves>, ILeaveManagementRepository
    {
        readonly IUnitOfWork _unitOfWork;
        public LeaveManagementRepository(DbContext context) : base(context)
        {

        }

        private ApplicationDbContext _appContext => (ApplicationDbContext)_context;

        public List<LeaveHolidayList> GetHolidayList()
        {
            var holiday = new List<LeaveHolidayList>();
            
                holiday = _appContext.LeaveHolidayList.Where(e => e.IsActive).ToList();
          
            return holiday;
        }
        public LeaveHolidayList GetHolidayByDate(DateTime date)
        {
            var holiday = new LeaveHolidayList();
            if (date != null)
            {
                 holiday = _appContext.LeaveHolidayList.Where(e => e.Holidaydate == date).FirstOrDefault();
            }
            return holiday;
        }
        public List<LeaveInfoModel> GetAllLeaveEntitlement(Guid EmployeeId, Guid periodId)
        {
            var leaveInfo = new List<LeaveInfoModel>();
            var query = from leaveEntitlement in _appContext.EmployeeLeavesEntitlement
                        join empLeaves in _appContext.EmployeeLeaves.Where(c => c.EmployeeId == EmployeeId && c.IsActive == true && c.LeavePeriodId == periodId) on leaveEntitlement.EmployeeLeavesId equals empLeaves.Id
                        join rules in _appContext.LeaveRules on leaveEntitlement.LeaveRulesId equals rules.Id
                        join type in _appContext.LeaveType on rules.LeaveTypeId equals type.Id
                        where leaveEntitlement.IsActive == true
                        select new LeaveInfoModel
                        {
                            Id = leaveEntitlement.Id.ToString(),
                            LeaveType = type.Name,
                            NoOfDays = rules.LeavesPerYear,
                            Approved = leaveEntitlement.Approved,
                            Pending = leaveEntitlement.Pending,
                            Rejected = leaveEntitlement.Rejected
                        };
            leaveInfo = query.ToList();
            return leaveInfo;
        }

        public List<LeaveInfoModel> GetAllLeaveForMonth(Guid employeeId,int month,int year)
        {
            var leaveInfo = new List<LeaveInfoModel>();
            var startDate = new DateTime(year, month, 1);
            var lastDayOfMonth = DateTime.DaysInMonth(startDate.Year, startDate.Month);
            var endDate = new DateTime(year, month, lastDayOfMonth);
          //  Expression<Func<EmployeeLeaves, bool>> empFilter = e => e.EmployeeId == employeeId && e.EmployeeLeavesEntitlement.

           // var employee = _unitOfWork.EmployeeLeaves.Get(empFilter, null, "EmployeeCtc,EmployeeLeaveDetailEmployee").FirstOrDefault();

            var query = from leaveEntitlement in _appContext.EmployeeLeavesEntitlement
                        join empLeaves in _appContext.EmployeeLeaves.Where(c => c.EmployeeId == employeeId && c.IsActive == true) on leaveEntitlement.EmployeeLeavesId equals empLeaves.Id
                        join rules in _appContext.LeaveRules on leaveEntitlement.LeaveRulesId equals rules.Id
                        join type in _appContext.LeaveType on rules.LeaveTypeId equals type.Id
                        join leaveDetail in _appContext.EmployeeLeaveDetail.Where(q=> q.LeaveStartDate > startDate || q.LeaveEndDate < endDate) on leaveEntitlement.Id equals leaveDetail.LeavesEntitlementId
                        where leaveEntitlement.IsActive == true
                        select new LeaveInfoModel
                        {
                            Id = leaveEntitlement.Id.ToString(),
                            LeaveType = type.Name,
                            NoOfDays = rules.LeavesPerYear,
                            Approved = leaveEntitlement.Approved,
                            Pending = leaveEntitlement.Pending,
                            Rejected = leaveEntitlement.Rejected
                        };
            leaveInfo = query.ToList();
            return leaveInfo;
        }

        public void RetractEmployeeLeave(Guid leaveDetailsId)
        {
            var leaveDetail = _appContext.EmployeeLeaveDetail.Where(c => c.Id == leaveDetailsId && c.IsActive == true).FirstOrDefault();
            if (leaveDetail != null)
            {
                leaveDetail.IsActive = false;
                leaveDetail.IsSubmitted = false;
                _appContext.EmployeeLeaveDetail.Update(leaveDetail);
                _appContext.SaveChanges();
            }
        }

        public int ExcludeHolidays(DateTime startDate, DateTime endDate)
        {
            int j = 0;
            List<DateTime> excludeDates = new List<DateTime>();
            Guid periodId = _appContext.LeavePeriod.Where(c => c.IsActive == true).Select(m => m.Id).Single();
            if (periodId != Guid.Empty)
            {
                var holidays = _appContext.LeaveHolidayList.Where(c => c.IsActive == true && c.LeavePeriodId == periodId).ToList();
                if (holidays.Count() > 0)
                {
                    foreach (var item in holidays)
                    {
                        excludeDates.Add(item.Holidaydate);
                    }
                    for (DateTime index = startDate; index <= endDate; index = index.AddDays(1))
                    {
                        for (int i = 0; i < excludeDates.Count; i++)
                        {
                            if (index.Date.CompareTo(excludeDates[i].Date) == 0)
                            {
                                j += 1;
                                break;
                            }
                        }
                    }
                }
            }
            return j;
        }

        public int ExcludeWeekends(DateTime startDate, DateTime endDate)
        {
            int weekendDays = 0;
            Guid periodId = _appContext.LeavePeriod.Where(c => c.IsActive == true).Select(m => m.Id).Single();
            if (periodId != Guid.Empty)
            {
                var workingDays = _appContext.LeaveWorkingDay.Where(c => c.IsActive == true && c.LeavePeriodId == periodId && c.WorkingDayValue == "0").ToList();
                if (workingDays.Count > 0)
                {
                    int weekEndCount = 0;
                    foreach (var item in workingDays)
                    {
                        string day = item.WorkingDay;
                        if (startDate > endDate)
                        {
                            DateTime temp = startDate;
                            startDate = endDate;
                            endDate = temp;
                        }
                        TimeSpan diff = endDate - startDate;
                        int dayss = diff.Days;
                        for (var i = 0; i <= dayss; i++)
                        {
                            var testDate = startDate.AddDays(i);
                            if (day == "Monday")
                            {
                                if (testDate.DayOfWeek == DayOfWeek.Monday)
                                {
                                    weekEndCount += 1;
                                }
                            }
                            else if (day == "Tuesday")
                            {
                                if (testDate.DayOfWeek == DayOfWeek.Tuesday)
                                {
                                    weekEndCount += 1;
                                }
                            }
                            else if (day == "Wednesday")
                            {
                                if (testDate.DayOfWeek == DayOfWeek.Wednesday)
                                {
                                    weekEndCount += 1;
                                }
                            }
                            else if (day == "Thursday")
                            {
                                if (testDate.DayOfWeek == DayOfWeek.Thursday)
                                {
                                    weekEndCount += 1;
                                }
                            }
                            else if (day == "Friday")
                            {
                                if (testDate.DayOfWeek == DayOfWeek.Friday)
                                {
                                    weekEndCount += 1;
                                }
                            }
                            else if (day == "Saturday")
                            {
                                if (testDate.DayOfWeek == DayOfWeek.Saturday)
                                {
                                    weekEndCount += 1;
                                }
                            }
                            else
                            {
                                if (testDate.DayOfWeek == DayOfWeek.Sunday)
                                {
                                    weekEndCount += 1;
                                }
                            }
                        }
                        weekendDays = weekEndCount;
                    }
                }
            }
            return weekendDays;
        }

        public EmployeeLeavesEntitlement GetLeaveEntitlement(Guid EmployeeId, Guid periodId, Guid leaveTypeId)
        {
            var entitlement = new List<EmployeeLeavesEntitlement>();
            var query = (from empLeaves in _appContext.EmployeeLeaves.Where(c => c.EmployeeId == EmployeeId && c.IsActive == true && c.LeavePeriodId == periodId)
                         join leaveentitlement in _appContext.EmployeeLeavesEntitlement.Where(c => c.IsActive == true) on empLeaves.Id equals leaveentitlement.EmployeeLeavesId
                         join rules in _appContext.LeaveRules.Where(c => c.IsActive && c.LeaveTypeId == leaveTypeId) on leaveentitlement.LeaveRulesId equals rules.Id
                         join type in _appContext.LeaveType on rules.LeaveTypeId equals type.Id
                         where leaveentitlement.IsActive == true
                         select new EmployeeLeavesEntitlement { Id = leaveentitlement.Id, LeaveRules = new LeaveRules { LeaveType = new LeaveType { Name = type.Name }, LeavesPerYear = rules.LeavesPerYear }, Approved = leaveentitlement.Approved, Pending = leaveentitlement.Pending, Rejected = leaveentitlement.Rejected }).FirstOrDefault();
            return query;
        }

        public int GetCalculateNoOfDays(DateTime StartDate, DateTime EndDate)
        {
            int excludeholidayCount = 0;
            int weekendDays = 0;
            int noOfdays = 0;
            excludeholidayCount = ExcludeHolidays(StartDate, EndDate);
            weekendDays = ExcludeWeekends(StartDate, EndDate);
            noOfdays = Convert.ToDateTime(EndDate).Subtract(Convert.ToDateTime(StartDate)).Days + 1 - weekendDays - excludeholidayCount;
            return noOfdays;
        }

        public bool CheckDaterange(DateTime startdate, DateTime enddate, string id)
        {
            Guid loginId = new Guid(id);
                SqlParameter Startdate = new SqlParameter("@startdate", startdate);
                SqlParameter Enddate = new SqlParameter("@enddate", enddate);
                SqlParameter LoginId = new SqlParameter("@loginId", loginId);
                SqlParameter IsDate = new SqlParameter
                {
                    ParameterName = "@isDate",
                    SqlDbType = System.Data.SqlDbType.Bit,
                    Direction = System.Data.ParameterDirection.Output
                };
                var ab = _context.Database.ExecuteSqlRaw("uspCheckDateRange @startdate, @enddate,@loginId,@isDate OUTPUT", Startdate, Enddate, LoginId, IsDate);
                Boolean result = (bool)IsDate.Value;
                return result;
          
        }

        public int GetLeaveNoOfDays(DateTime startDate, DateTime endDate, int noOfDays)
        {
            int excludeHoliday = 0;
            int weekendDays = 0;
            excludeHoliday = ExcludeHolidays(startDate, endDate);
            weekendDays = ExcludeWeekends(startDate, endDate);
            noOfDays = noOfDays + Convert.ToDateTime(endDate).Subtract(Convert.ToDateTime(startDate)).Days + 1 - weekendDays - excludeHoliday;
            return noOfDays;
        }
    }
}
