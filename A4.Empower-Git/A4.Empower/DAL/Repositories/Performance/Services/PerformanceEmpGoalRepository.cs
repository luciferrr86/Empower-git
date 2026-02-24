using A4.BAL;
using A4.DAL.Entites;
using DAL;
using DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Transactions;

namespace A4.DAL.Repositories
{
    public class PerformanceEmpGoalRepository : Repository<PerformanceEmpGoal>, IPerformanceEmpGoalRepository
    {
        public PerformanceEmpGoalRepository(DbContext context) : base(context)
        {

        }
        private ApplicationDbContext _appContext => (ApplicationDbContext)_context;


        public ReleaseGoalMessage ReleaseGoal(Guid empId, Guid yearId, PerformanceFeatures features)
        {
            var objRelaseGoal = new ReleaseGoalMessage();
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    var performanceconfig = features.IsInitailRatingEnabled;
                    bool isActiveSignOff = features.IsCEOSignOffEnabled;
                    bool isPresidentEnabled = features.IsPresidentCouncilEnabled;
                    bool isMidYearEnabled = features.IsMidYearEnabled;
                    if (performanceconfig)
                    {
                        #region Enable Initial Rating
                        //Checks rating set for all subordinate or not 
                        var aobjRatingCheck = _appContext.PerformanceInitailRating.Where(x => x.PerformanceYearId == yearId & x.ManagerId == empId);
                        if (aobjRatingCheck.Count() > 0)
                        {
                            if (!aobjRatingCheck.Any(x => x.PerformanceConfigRatingId == Guid.Empty))
                            {

                                var aobjReleaseGoal = _appContext.PerformanceGoalMain.Where(x => x.ManagerId == empId & x.PerformanceYearId == yearId).FirstOrDefault();
                                if (aobjReleaseGoal != null)
                                {
                                    aobjReleaseGoal.IsManagerReleased = true;
                                    if (isActiveSignOff)
                                    {
                                        aobjReleaseGoal.PerformanceStatusId = GetStatusIdByType(PerformanceEnum.PerformanceStatus.AwaitingCEOSignOff.ToString());
                                    }
                                    _appContext.PerformanceGoalMain.Update(aobjReleaseGoal);
                                }

                                var lstEmp = CreatePerformanceEmployee(empId, yearId, isPresidentEnabled, isMidYearEnabled);
                                if (lstEmp.Count > 0)
                                {
                                    transaction.Dispose();
                                    objRelaseGoal.Status = 2;
                                    objRelaseGoal.lstEmpName = lstEmp;
                                    return objRelaseGoal;
                                }
                                else
                                {
                                    var lstChkPresidentSet = ChkSubordinatePresidentAssign(empId, yearId);
                                    if (lstChkPresidentSet.Count > 0)
                                    {
                                        transaction.Dispose();
                                        objRelaseGoal.Status = 4;
                                        objRelaseGoal.lstEmpName = lstChkPresidentSet;
                                        return objRelaseGoal;
                                    }
                                }

                                //Status update for below manager of this manager who is releasing the goal
                                var mailList = new List<MailList>();
                                var objBelowManager = _appContext.Employee.Where(x => x.ManagerId == empId & x.IsActive == true);
                                if (objBelowManager.Count() > 0)
                                {
                                    foreach (var item in objBelowManager)
                                    {
                                        var objIsManger = _appContext.Employee.Where(x => x.ManagerId == empId & x.IsActive == true);
                                        if (objIsManger.Count() > 0)
                                        {
                                            if (item.Id != empId)
                                            {
                                                var objGoalMainStatus = _appContext.PerformanceGoalMain.Where(x => x.ManagerId == item.Id & x.PerformanceYearId == yearId).FirstOrDefault();
                                                if (objGoalMainStatus != null)
                                                {
                                                    objGoalMainStatus.PerformanceStatusId = GetStatusIdByType(PerformanceEnum.PerformanceStatus.Notstarted.ToString());
                                                    _appContext.PerformanceGoalMain.Update(objGoalMainStatus);
                                                }
                                                var appUser = _appContext.Users.Find(item.UserId);
                                                if (appUser != null)
                                                {
                                                    mailList.Add(new MailList { MailID = appUser.Email, Name = appUser.FullName });
                                                }
                                            }
                                        }
                                    }
                                }
                                //:--To Do:-----Send Email: TO below manager for Goal setting If any, and CEO for Signoff There Initial rating::-                       
                                // var thread = new Thread(new ThreadStart(() => _performanceNotificationService.SendMail(mailList, Convert.ToInt32(ApplicationEnum.MailType.InviteManagerToSetGoal))));
                                //thread.Start();
                                var mailListCEO = new List<MailList>();
                                mailListCEO.Add(new MailList { MailID = GetCEOEmailID() });
                                //Mail to notify CEO to signoff initialRating
                                //var thread1 = new Thread(new ThreadStart(() => _performanceNotificationService.SendMail(mailListCEO, Convert.ToInt32(ApplicationEnum.MailType.NotificationToCEOToSignOffInitialRating))));
                                //thread1.Start();
                                objRelaseGoal.Status = 3;
                                objRelaseGoal.mailList = mailList;
                                objRelaseGoal.mailListCEO = mailListCEO;
                            }
                            else
                            {
                                objRelaseGoal.Status = 1;
                            }

                        }
                        else
                        {
                            objRelaseGoal.Status = 1;
                            transaction.Commit();
                            return objRelaseGoal;
                        }

                    }
                    #endregion
                    else
                    {
                        #region Disable Initial Rating
                        var aobjReleaseGoal = _appContext.PerformanceGoalMain.Where(x => x.ManagerId == empId & x.PerformanceYearId == yearId).FirstOrDefault();
                        if (aobjReleaseGoal != null)
                        {
                            aobjReleaseGoal.IsManagerReleased = true;
                            aobjReleaseGoal.PerformanceStatusId = GetStatusIdByType(PerformanceEnum.PerformanceStatus.Allmangerreleasedprocessforemployee.ToString());
                            _appContext.PerformanceGoalMain.Update(aobjReleaseGoal);
                        }

                        var lstEmp = CreatePerformanceEmployee(empId, yearId, isPresidentEnabled, isMidYearEnabled);
                        if (lstEmp.Count > 0)
                        {
                            transaction.Dispose();
                            objRelaseGoal.Status = 2;
                            objRelaseGoal.lstEmpName = lstEmp;
                            return objRelaseGoal;

                        }
                        else
                        {
                            if (isPresidentEnabled)
                            {
                                var lstChkPresidentSet = ChkSubordinatePresidentAssign(empId, yearId);
                                if (lstChkPresidentSet.Count > 0)
                                {
                                    transaction.Dispose();
                                    objRelaseGoal.Status = 4;
                                    objRelaseGoal.lstEmpName = lstChkPresidentSet;
                                    return objRelaseGoal;
                                }
                            }
                        }

                        //Status update for below manager of this manager who is releasing the goal
                        var mailList = new List<MailList>();
                        var objBelowManager = _appContext.Employee.Where(x => x.ManagerId == empId & x.IsActive == true);
                        if (objBelowManager.Count() > 0)
                        {
                            foreach (var item in objBelowManager)
                            {
                                var objIsManger = _appContext.Employee.Where(x => x.ManagerId == empId & x.IsActive == true);
                                if (objIsManger.Count() > 0)
                                {
                                    if (item.Id != empId)
                                    {
                                        var objGoalMainStatus = _appContext.PerformanceGoalMain.Where(x => x.ManagerId == item.Id & x.PerformanceYearId == yearId).FirstOrDefault();
                                        if (objGoalMainStatus != null)
                                        {
                                            objGoalMainStatus.PerformanceStatusId = GetStatusIdByType(PerformanceEnum.PerformanceStatus.AwaitingToReleaseGoal.ToString());
                                            _appContext.PerformanceGoalMain.Update(objGoalMainStatus);
                                        }
                                        var appUser = _appContext.Users.Find(item.UserId);
                                        if (appUser != null)
                                        {
                                            mailList.Add(new MailList { MailID = appUser.Email, Name = appUser.FullName });
                                        }
                                    }
                                }
                            }
                        }
                        //:--To Do:-----Send Email: TO below manager for Goal setting If any, and CEO for Signoff There Initial rating::-                       
                        //var thread = new Thread(new ThreadStart(() => _performanceNotificationService.SendMail(mailList, Convert.ToInt32(ApplicationEnum.MailType.InviteManagerToSetGoal))));
                        //thread.Start();
                        objRelaseGoal.Status = 3;
                        objRelaseGoal.mailList = mailList;
                        #endregion
                    }
                    _appContext.SaveChanges();
                    transaction.Commit();

                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                }
            }
            return objRelaseGoal;
        }

        public string GetCEOEmailID()
        {
            string MailID = "";
            string Name = "";
            var aobjCEO = _appContext.Employee.Where(x => x.IsActive == true);
            if (aobjCEO.Count() > 0)
            {
                foreach (var item in aobjCEO)
                {
                    if (item.Id == item.ManagerId)
                    {
                        var appUser = _appContext.Users.Find(item.UserId);
                        if (appUser != null)
                        {
                            MailID = appUser.Email;
                            Name = appUser.FullName;
                        }
                    }
                }
            }
            return MailID;
        }

        private Guid GetStatusIdByType(string type)
        {
            Guid statusId = new Guid();
            var status = _appContext.PerformanceStatus.Where(x => x.Type == type).FirstOrDefault();
            if (status != null)
            {
                statusId = status.Id;
            }
            return statusId;
        }

        public List<string> ChkSubordinatePresidentAssign(Guid mgrId, Guid yearId)
        {
            List<string> lstSub = new List<string>();
            var lstSubordinate = _appContext.Employee.Where(x => x.ManagerId == mgrId & x.IsActive == true & x.Id != mgrId).ToList();
            if (lstSubordinate.Count > 0)
            {
                foreach (var item in lstSubordinate)
                {
                    var appUser = _appContext.Users.Find(item.UserId);
                    if (appUser != null)
                    {
                        var presidentCouncil = _appContext.PerformancePresidentCouncil.Where(x => x.EmployeeId == item.Id & x.PerformanceYearId == yearId).FirstOrDefault();
                        if (presidentCouncil != null && presidentCouncil.PresidentId == Guid.Empty)
                        {
                            lstSub.Add(appUser.FullName);
                        }
                        else if (presidentCouncil == null)
                        {
                            lstSub.Add(appUser.FullName);
                        }
                    }
                }
            }
            return lstSub;
        }

        public List<string> CreatePerformanceEmployee(Guid managerId, Guid yearID, bool isPresidentEnabled, bool isMidYearEnabled)
        {
            var lstEmp = new List<string>();
            // Add Employee in cPerformanceEmpGoal: -
            var managerSignature = _appContext.Employee.Find(managerId);
            var appUser = _appContext.Users.Find(managerSignature.UserId);

            List<Employee> aobEmployee = _appContext.Employee.Where(x => x.ManagerId == managerId & x.IsActive == true & x.Id != x.ManagerId).ToList();
            if (aobEmployee.Count > 0)
            {
                foreach (var item in aobEmployee)
                {
                    var isPerformanceEmpGoalCreate = _appContext.PerformanceEmpGoal.Where(x => x.EmployeeId == item.Id & x.PerformanceYearId == yearID);
                    if (isPerformanceEmpGoalCreate.Count() == 0)
                    {
                        //Create EmpGoal:-
                        var midYearGoalId = new Guid();
                        var performanceEmpGoal = new PerformanceEmpGoal();
                        performanceEmpGoal.PerformanceYearId = yearID;
                        performanceEmpGoal.EmployeeId = item.Id;
                        performanceEmpGoal.ManagerId = managerId;
                        performanceEmpGoal.PerformanceStatusId = GetStatusIdByType(PerformanceEnum.PerformanceStatus.GoalReleased.ToString());
                        if (appUser != null)
                        {
                            performanceEmpGoal.ManagerSignature = appUser.FullName;
                        }
                        _appContext.PerformanceEmpGoal.Add(performanceEmpGoal);

                        var appUserEmp = _appContext.Users.Find(item.UserId);
                        var perfromanceEmpYearGoal = new PerformanceEmpYearGoal();
                        perfromanceEmpYearGoal.PerformanceEmpGoalId = performanceEmpGoal.Id;
                        if (appUserEmp != null)
                        {
                            perfromanceEmpYearGoal.EmployeeSignature = appUserEmp.FullName;
                        }
                        perfromanceEmpYearGoal.IsEmployeeActive = true;
                        perfromanceEmpYearGoal.PerformanceStatusId = GetStatusIdByType(PerformanceEnum.PerformanceStatus.GoalReleased.ToString());
                        _appContext.PerformanceEmpYearGoal.Add(perfromanceEmpYearGoal);
                        if (isMidYearEnabled)
                        {
                            var performanceMidYearGoal = new PerformanceEmpMidYearGoal();
                            midYearGoalId = performanceMidYearGoal.Id;
                            if (appUserEmp != null)
                            {
                                performanceMidYearGoal.EmployeeSignature = appUserEmp.FullName;
                            }
                            performanceMidYearGoal.PerformanceStatusId = GetStatusIdByType(PerformanceEnum.PerformanceStatus.GoalReleased.ToString());
                            performanceMidYearGoal.PerformanceEmpGoalId = performanceEmpGoal.Id;
                            _appContext.PerformanceEmpMidYearGoal.Add(performanceMidYearGoal);
                        }

                        //Create PerformanceEmpGoalPresident:-
                        if (isPresidentEnabled)
                        {
                            var cpresident = _appContext.PerformancePresidentCouncil.Where(x => x.EmployeeId == item.Id & x.PerformanceYearId == yearID).FirstOrDefault();
                            var perEmppresidentCouncil = new PerformanceEmpGoalPresident();
                            perEmppresidentCouncil.PerformanceEmpGoalId = performanceEmpGoal.Id;
                            if (cpresident != null)
                            {
                                var presidentname = _appContext.Employee.Find(cpresident.PresidentId);
                                if (presidentname != null)
                                {
                                    var president = _appContext.Users.Find(presidentname.UserId);
                                    if (president != null)
                                    {
                                        perEmppresidentCouncil.PresidentSignature = president.FullName;
                                    }

                                }
                                else
                                {
                                    perEmppresidentCouncil.PresidentSignature = "";
                                }

                            }
                            else
                            {
                                perEmppresidentCouncil.PresidentSignature = "";
                            }
                            perEmppresidentCouncil.PerformanceEmpGoalId = performanceEmpGoal.Id;
                            _appContext.PerformanceEmpGoalPresident.Add(perEmppresidentCouncil);

                        }

                        List<SqlParameter> a = new List<SqlParameter>();
                        a.Add(new SqlParameter("@empId", SqlDbType.UniqueIdentifier));
                        a[a.Count - 1].Value = item.Id;
                        a.Add(new SqlParameter("@perfromanceYearId", SqlDbType.UniqueIdentifier));
                        a[a.Count - 1].Value = yearID;
                        var goalMeasurePerfromanceId = _appContext.EmpPerformanceGoalMeasure.FromSqlRaw("exec uspGetEmpPerformanceGoalMeasure @empId,@perfromanceYearId ", a.ToArray()).ToList();
                        //_appContext.PerformanceGoalMeasure.GetPerfromanceGoalMeasureEmploye(item.iID, yearID);
                        if (goalMeasurePerfromanceId.Count > 0)
                        {
                            foreach (var id in goalMeasurePerfromanceId)
                            {
                                var EmpGoalDetail = new PerformanceEmpYearGoalDetail();
                                EmpGoalDetail.PerformanceEmpYearGoalId = perfromanceEmpYearGoal.Id;
                                EmpGoalDetail.PerformanceGoalMeasureId = id.QuadID;
                                _appContext.PerformanceEmpYearGoalDetail.Add(EmpGoalDetail);

                                if (isMidYearEnabled)
                                {
                                    var midYearGoalDetail = new PerformanceEmpMidYearGoalDetail();
                                    midYearGoalDetail.PerformanceEmpMidYearGoalId = midYearGoalId;
                                    midYearGoalDetail.PerformanceGoalMeasureId = id.QuadID;
                                    _appContext.PerformanceEmpMidYearGoalDetail.Add(midYearGoalDetail);
                                }
                            }
                        }
                        else
                        {
                            string Name = appUserEmp.FullName;
                            lstEmp.Add(Name);
                        }


                    }
                }
            }
            _appContext.SaveChanges();
            return lstEmp;
        }

        public GoalViewModel GetCurrentYearGoal(EmployeeGoalDetail empGoal, bool isMidYearEnabled)
        {

            var goalMeasureViewModel = new GoalViewModel();
            // goalMeasureViewModel.EmployeeId = empGoal.EmployeeId;            
            if (isMidYearEnabled)
            {
                goalMeasureViewModel.MidYearGoalMeasureList = GetMidYearGoalMeasure(empGoal.EmpGoalId);
                // goalMeasureViewModel.IsMidYearRatingSubmitted = checkIsMidYearRatingSubmitted(Guid.Parse(empGoal.EmpGoalId));
                // goalMeasureViewModel.IsEndYearRatingSubmitted = checkIsEndYearRatingSubmitted(Guid.Parse(empGoal.EmpGoalId));
            }
            goalMeasureViewModel.EndYearGoalMeasureList = GetEndYearGoalMeasure(empGoal.EmpGoalId);
            string text = "N/A";
            var config = _appContext.PerformanceConfig.Where(x => x.IsActive == true).FirstOrDefault();
            if (config != null)
            {
                text = config.MyGoalInstructionText;
            }
            goalMeasureViewModel.GoalInstructionText = text;
            return goalMeasureViewModel;

        }



        private bool checkIsMidYearRatingSubmitted(Guid goalId)
        {
            bool flag = false;
            var empMidYearGoal = _appContext.PerformanceEmpMidYearGoal.Where(x => x.PerformanceEmpGoalId == goalId).FirstOrDefault();
            if (empMidYearGoal != null)
            {
                flag = empMidYearGoal.IsEmployeeRatingSubmitted;
            }
            return flag;
        }

        private bool checkIsEndYearRatingSubmitted(Guid goalId)
        {
            bool flag = false;
            var empYearGoal = _appContext.PerformanceEmpYearGoal.Where(x => x.PerformanceEmpGoalId == goalId).FirstOrDefault();
            if (empYearGoal != null)
            {
                flag = empYearGoal.IsEmployeeRatingSubmitted;
            }
            return flag;
        }

        private List<GoalMeasure> GetEndYearGoalMeasure(string empGoalId)
        {
            var goalMeasure = new List<GoalMeasure>();
            var midYeraGoal = _appContext.PerformanceEmpYearGoal.Where(x => x.PerformanceEmpGoalId == Guid.Parse(empGoalId)).FirstOrDefault();
            if (midYeraGoal != null)
            {
                var yearGoalDetail = _appContext.PerformanceEmpYearGoalDetail.Where(x => x.PerformanceEmpYearGoalId == midYeraGoal.Id);
                foreach (var item in yearGoalDetail)
                {
                    var goalMeasureDetail = _appContext.PerformanceGoalMeasure.Find(item.PerformanceGoalMeasureId);
                    if (goalMeasureDetail != null && goalMeasureDetail.IsActive)
                    {
                        string goalName = "";
                        var performanceGoal = _appContext.PerformanceGoal.Find(goalMeasureDetail.PerformanceGoalId);
                        if (performanceGoal != null)
                        {
                            goalName = performanceGoal.GoalName;
                        }
                        goalMeasure.Add(new GoalMeasure
                        {
                            GoalId = item.Id.ToString(),
                            GoalName = goalName,
                            Accomplishment = item.EmployeeComment,
                            ManagerComments = item.ManagerComment,
                            Measure = goalMeasureDetail.MeasureText.ToString(),
                            StartTime = goalMeasureDetail.StartTime,
                            EndTime = goalMeasureDetail.EndTime
                        });
                    }
                }
            }
            return goalMeasure;
        }

        private List<GoalMeasure> GetMidYearGoalMeasure(string empGoalId)
        {
            var goalMeasure = new List<GoalMeasure>();
            if (empGoalId != null)
            {
                var midYeraGoal = _appContext.PerformanceEmpMidYearGoal.Where(x => x.PerformanceEmpGoalId == Guid.Parse(empGoalId)).ToList().FirstOrDefault();
                if (midYeraGoal != null)
                {
                    var midYearGoalDetail = _appContext.PerformanceEmpMidYearGoalDetail.Where(x => x.PerformanceEmpMidYearGoalId == midYeraGoal.Id);
                    foreach (var item in midYearGoalDetail)
                    {
                        var goalMeasureDetail = _appContext.PerformanceGoalMeasure.Find(item.PerformanceGoalMeasureId);
                        if (goalMeasureDetail != null && goalMeasureDetail.IsActive)
                        {
                            string goalName = "";
                            var performanceGoal = _appContext.PerformanceGoal.Find(goalMeasureDetail.PerformanceGoalId);
                            if (performanceGoal != null)
                            {
                                goalName = performanceGoal.GoalName;
                            }
                            goalMeasure.Add(new GoalMeasure
                            {
                                GoalId = item.Id.ToString(),
                                GoalName = goalName,
                                Accomplishment = item.EmployeeComment,
                                ManagerComments = item.ManagerComment,
                                Measure = goalMeasureDetail.MeasureText.ToString(),
                                StartTime = goalMeasureDetail.StartTime,
                                EndTime = goalMeasureDetail.EndTime
                            });
                        }
                    }
                }
            }
            return goalMeasure;
        }



        private bool SaveEndYearGoal(string empGoalId, GoalViewModel goalCurrentYear, string action)
        {
            bool flag = false;
            if (goalCurrentYear.EndYearGoalMeasureList.Count() > 0)
            {
                var empYearGoal = _appContext.PerformanceEmpYearGoal.Where(x => x.PerformanceEmpGoalId == Guid.Parse(empGoalId)).FirstOrDefault();
                if (empYearGoal != null)
                {
                    if (action == "save")
                    {
                        empYearGoal.IsEmployeeGoalSaved = true;
                        empYearGoal.PerformanceStatusId = GetStatusIdByType(PerformanceEnum.PerformanceStatus.EmployeeSaved.ToString());
                    }
                    else
                    {
                        empYearGoal.IsEmployeeGoalSubmitted = true;
                        empYearGoal.PerformanceStatusId = GetStatusIdByType(PerformanceEnum.PerformanceStatus.EmployeeSubmitted.ToString());
                    }

                    _appContext.PerformanceEmpYearGoal.Update(empYearGoal);
                }

                foreach (var item in goalCurrentYear.EndYearGoalMeasureList)
                {
                    var empGoalDetail = _appContext.PerformanceEmpYearGoalDetail.Find(Guid.Parse(item.GoalId));
                    if (empGoalDetail != null)
                    {
                        if (item.Accomplishment != null)
                        {
                            empGoalDetail.EmployeeComment = item.Accomplishment;
                        }
                        _appContext.PerformanceEmpYearGoalDetail.Update(empGoalDetail);
                    }
                }
                flag = true;
                _appContext.SaveChanges();
            }
            return flag;
        }

        public bool SaveCurrentYearGoal(GoalViewModel goalCurrentYear, EmployeeGoalDetail empDetail, Employee emp, bool isMidYearEnabled, bool isMidYearCompleted, string action)
        {
            var flag = true;
            if (isMidYearEnabled && !isMidYearCompleted)
            {
                if (goalCurrentYear.MidYearGoalMeasureList.Count() > 0)
                {
                    var empMidYearGoal = _appContext.PerformanceEmpMidYearGoal.Where(x => x.PerformanceEmpGoalId == Guid.Parse(empDetail.EmpGoalId)).FirstOrDefault();
                    if (empMidYearGoal != null && !empMidYearGoal.IsEmployeeRatingSubmitted)
                    {
                        if (action == "save")
                        {
                            empMidYearGoal.IsEmployeeGoalSaved = true;
                            empMidYearGoal.PerformanceStatusId = GetStatusIdByType(PerformanceEnum.PerformanceStatus.EmployeeSaved.ToString());
                        }
                        else if (action == "submit")
                        {
                            empMidYearGoal.IsEmployeeGoalSubmitted = true;
                        }
                        _appContext.PerformanceEmpMidYearGoal.Update(empMidYearGoal);

                        foreach (var item in goalCurrentYear.MidYearGoalMeasureList)
                        {
                            var midYearGoalDetail = _appContext.PerformanceEmpMidYearGoalDetail.Find(Guid.Parse(item.GoalId));
                            if (midYearGoalDetail != null)
                            {
                                if (!string.IsNullOrEmpty(item.Accomplishment))
                                {
                                    midYearGoalDetail.EmployeeComment = item.Accomplishment;
                                    _appContext.PerformanceEmpMidYearGoalDetail.Update(midYearGoalDetail);
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                var isSaved = SaveEndYearGoal(empDetail.EmpGoalId, goalCurrentYear, action);
                if (isSaved)
                {
                    var mailList = new List<MailList>();
                    var mailId = "";
                    var aobjEmployee = _appContext.Employee.Find(Guid.Parse(empDetail.EmployeeId));
                    if (aobjEmployee != null)
                    {
                        var aspNetUser = _appContext.Users.Find(aobjEmployee.UserId);
                        if (aspNetUser != null)
                        {
                            mailId = aspNetUser.Email;
                        }
                        mailList.Add(new MailList { MailID = mailId });
                    }
                    //Notifcation mail to manager from employee on review submission
                    //var thread = new Thread(new ThreadStart(() => _performanceNotificationService.SendMail(mailList, Convert.ToInt32(ApplicationEnum.MailType.NotificationFromEmployeeToMgrOnGoalSubmit))));
                    //thread.Start();
                }
            }
            _appContext.SaveChanges();
            return flag;
        }

        public bool SaveMgrCurrentYearGoal(GoalViewModel goalCurrentYear, EmployeeGoalDetail empDetail, bool midYearEnabled, bool IsMidYearReviewCompleted, string button)
        {
            var flag = false;
            if (midYearEnabled && !IsMidYearReviewCompleted)
            {
                if (goalCurrentYear.MidYearGoalMeasureList.Count() > 0)
                {
                    var empMidYearGoal = _appContext.PerformanceEmpMidYearGoal.Where(i => i.PerformanceEmpGoalId == Guid.Parse(empDetail.EmpGoalId)).FirstOrDefault();
                    if (empMidYearGoal != null)
                    {
                        if (button == "save")
                        {
                            empMidYearGoal.IsManagerGoalSaved = true;
                        }
                        else
                        {
                            empMidYearGoal.IsManagerGoalSubmitted = true;
                            empMidYearGoal.PerformanceStatusId = GetStatusIdByType(PerformanceEnum.PerformanceStatus.Managersavedgoals.ToString());
                        }
                        _appContext.PerformanceEmpMidYearGoal.Update(empMidYearGoal);

                        foreach (var item in goalCurrentYear.MidYearGoalMeasureList)
                        {
                            var empGoalDetail = _appContext.PerformanceEmpMidYearGoalDetail.Find(Guid.Parse(item.GoalId));
                            if (empGoalDetail != null)
                            {
                                if (!string.IsNullOrEmpty(item.ManagerComments))
                                {
                                    empGoalDetail.ManagerComment = item.ManagerComments;
                                }
                                _appContext.PerformanceEmpMidYearGoalDetail.Update(empGoalDetail);

                            }
                        }
                        flag = true;
                    }
                }                
            }
            else
            {
                SaveMgrEndYearGoal(goalCurrentYear.EndYearGoalMeasureList, button, empDetail.EmpGoalId, empDetail.ManagerId, empDetail.EmployeeId);
            }
            _appContext.SaveChanges();
            return flag;
        }

        private bool SaveMgrEndYearGoal(List<GoalMeasure> listGoalMeasure, string button, string goalId, string mgrId, string empId)
        {
            var flag = false;
            if (listGoalMeasure.Count() > 0)
            {
                var empYearGoal = _appContext.PerformanceEmpYearGoal.Where(x => x.PerformanceEmpGoalId == Guid.Parse(goalId)).FirstOrDefault();
                if (empYearGoal != null)
                {
                    if (button == "save")
                    {
                        empYearGoal.IsManagerGoalSaved = true;
                        empYearGoal.PerformanceStatusId = GetStatusIdByType(PerformanceEnum.PerformanceStatus.Managersavedgoals.ToString());
                    }
                    else
                    {
                        empYearGoal.IsManagerGoalSubmitted = true;
                    }
                    _appContext.PerformanceEmpYearGoal.Update(empYearGoal);

                    foreach (var item in listGoalMeasure)
                    {
                        var empGoalDetail = _appContext.PerformanceEmpYearGoalDetail.Where(x => x.PerformanceEmpYearGoalId == empYearGoal.Id).FirstOrDefault();
                        if (empGoalDetail != null)
                        {
                            if (item.ManagerComments != null)
                            {
                                empGoalDetail.ManagerComment = item.ManagerComments;
                            }
                            _appContext.PerformanceEmpYearGoalDetail.Update(empGoalDetail);
                        }
                    }
                    flag = true;
                }
            }
            return flag;
        }


        public RatingModel GetRating(EmployeeGoalDetail empGoal, bool isMidYearEnabled)
        {
            var rating = new RatingModel();
            var config = _appContext.PerformanceConfig.Where(x => x.IsActive).FirstOrDefault();
            string text = "N/A";
            if (config != null)
            {
                text = config.RatingInstructionText;
            }
            rating.InstructionText = text;
            if (isMidYearEnabled)
            {
                if (empGoal.EmployeeComment != null)
                    rating.MidYearRating.EmployeeComment = empGoal.EmployeeComment;
                if (empGoal.ManagerComment != null)
                    rating.MidYearRating.ManagerComment = empGoal.ManagerComment;
                rating.MidYearRating.ManagerSignature = empGoal.ManagerSignature;
                rating.MidYearRating.RatingId = empGoal.MidYearRating;
            }
            if (empGoal.EmployeeYearEndComment != null)
                rating.EndYearRating.EmployeeComment = empGoal.EmployeeYearEndComment;
            if (empGoal.ManagerYearEndComment != null)
                rating.EndYearRating.ManagerComment = empGoal.ManagerYearEndComment;
            rating.EndYearRating.ManagerSignature = empGoal.ManagerYearEndSignature;
            if (empGoal.FinalRating != null)
                rating.EndYearRating.RatingId = empGoal.FinalRating;
            return rating;
        }

        public bool SaveRating(RatingModel ratingModel, string name, EmployeeGoalDetail empDetail, bool midYearEnabled, bool isMidYearReviewCompleted)
        {
            bool flag = true;
            List<MailList> mailList = null;
            if (midYearEnabled && !isMidYearReviewCompleted)
            {
                var empMidYearReview = _appContext.PerformanceEmpMidYearGoal.Where(x => x.PerformanceEmpGoalId == Guid.Parse(empDetail.EmpGoalId)).FirstOrDefault();
                if (empMidYearReview != null)
                {
                    empMidYearReview.EmployeeAccComment = ratingModel.MidYearRating.EmployeeComment;
                    if (name == "save")
                    {
                        empMidYearReview.IsEmployeeRatingSaved = true;
                    }
                    else if (name == "submit")
                    {
                        empMidYearReview.IsEmployeeRatingSubmitted = true;
                        empMidYearReview.PerformanceStatusId = GetStatusIdByType(PerformanceEnum.PerformanceStatus.EmployeeSignsoff.ToString());
                        mailList = new List<MailList>();
                    //    var aobjEmployee = cEmployee.Get_ID(ratingViewModel.EmployeeId);
                    //    if (aobjEmployee != null)
                    //    {
                    //        mailList.Add(new MailList { MailID = cEmployee.Get_ID(Convert.ToInt32(aobjEmployee.sReportingHead)).sEmailId });
                    //    }
                    //    // Notification mail from employee to manager on rating acceptance
                    //    string message = PerformanceTemplates.Invitation(item.MailID, item.Name);
                    //    (bool success, string errorMsg) response = await _emailer.SendEmailAsync(item.Name, item.MailID, "Email from A4.Empower", message);
                    }
                    _appContext.PerformanceEmpMidYearGoal.Update(empMidYearReview);

                }
            }
            else
            {

                var empYearReview = _appContext.PerformanceEmpYearGoal.Where(x => x.PerformanceEmpGoalId == Guid.Parse(empDetail.EmpGoalId)).FirstOrDefault();
                if (empYearReview != null)
                {
                    if (ratingModel.EndYearRating.EmployeeComment != null && ratingModel.EndYearRating.EmployeeComment != "")
                    {
                        empYearReview.EmployeeAccComment = ratingModel.EndYearRating.EmployeeComment;
                    }
                    if (name == "save")
                    {
                        empYearReview.IsEmployeeRatingSaved = true;
                    }
                    else if (name == "submit")
                    {
                        empYearReview.IsEmployeeRatingSubmitted = true;
                        empYearReview.PerformanceStatusId = GetStatusIdByType(PerformanceEnum.PerformanceStatus.EmployeeSignsoff.ToString());
                        //mailList = new List<MailList>();
                        //var aobjEmployee = cEmployee.Get_ID(ratingModel.EmployeeId);
                        //if (aobjEmployee != null)
                        //{
                        //    mailList.Add(new MailList { MailID = cEmployee.Get_ID(Convert.ToInt32(aobjEmployee.sReportingHead)).sEmailId });
                        //}
                        //Notification mail from employee to manager on rating acceptance
                        //var thread = new Thread(new ThreadStart(() => _performanceNotificationService.SendMail(mailList, Convert.ToInt32(ApplicationEnum.MailType.NotificationFromEmployeeToMgrOnRatingAcceptance))));
                        //thread.Start();
                    }
                }
                _appContext.PerformanceEmpYearGoal.Update(empYearReview);

            }
            _appContext.SaveChanges();
            return flag;
        }

        public bool SaveMgrRating(RatingModel ratingModel, string name, EmployeeGoalDetail empDetail, Employee employee, bool midYearEnabled, bool isMidYearReviewCompleted)
        {
            bool flag = true;
            List<MailList> mailList = new List<MailList>();
            //bool isActivePresident = _perfromanceFeature.IsPresidentCouncilEnabled;
            if (midYearEnabled && !isMidYearReviewCompleted)
            {
                var empMidYearReview = _appContext.PerformanceEmpMidYearGoal.Where(x => x.PerformanceEmpGoalId == Guid.Parse(empDetail.EmpGoalId)).FirstOrDefault();
                if (empMidYearReview != null)
                {
                    var aobjEmployee = _appContext.Users.Find(employee.UserId);
                    empMidYearReview.PerformanceStatusId = GetStatusIdByType(PerformanceEnum.PerformanceStatus.RatingsassignedandsavedbyManager.ToString());
                    if (ratingModel.MidYearRating.ManagerComment != null)
                    {
                        empMidYearReview.ManagerAccComment = ratingModel.MidYearRating.ManagerComment;
                    }
                    if (ratingModel.MidYearRating.RatingId != null)
                    {
                        empMidYearReview.FinalRating = ratingModel.MidYearRating.RatingId;
                    }
                    if (name == "save")
                    {
                        empMidYearReview.IsManagerRatingSaved = true;
                        if (aobjEmployee != null)
                        {
                            mailList.Add(new MailList { MailID = aobjEmployee.Email });
                        }
                        
                        
                    }
                    if (name == "submit")
                    {
                        empMidYearReview.IsManagerRatingSubmitted = true;
                        empMidYearReview.IsMidYearReviewCompleted = true;
                        empMidYearReview.PerformanceStatusId = GetStatusIdByType(PerformanceEnum.PerformanceStatus.Presidentscouncilsignsoff.ToString());
                    }
                    _appContext.PerformanceEmpMidYearGoal.Update(empMidYearReview);
                }
            }
            else
            {
                var aobjEmpGoal = _appContext.PerformanceEmpYearGoal.Where(x => x.PerformanceEmpGoalId == Guid.Parse(empDetail.EmpGoalId)).FirstOrDefault();
                if (aobjEmpGoal != null)
                {

                    var aobjEmployee = _appContext.Users.Find(employee.UserId);
                    aobjEmpGoal.PerformanceStatusId = GetStatusIdByType(PerformanceEnum.PerformanceStatus.RatingsassignedandsavedbyManager.ToString());
                    if (ratingModel.EndYearRating.ManagerComment != null)
                    {
                        aobjEmpGoal.ManagerAccComment = ratingModel.EndYearRating.ManagerComment;
                    }
                    if (ratingModel.EndYearRating.RatingId != null)
                    {
                        aobjEmpGoal.FinalRating = ratingModel.EndYearRating.RatingId;
                    }
                    if (name == "save")
                    {
                        aobjEmpGoal.IsManagerRatingSaved = true;

                        if (aobjEmployee != null)
                        {
                            mailList.Add(new MailList { MailID = aobjEmployee.Email });
                        }
                        //Notification mail from manager to employee on  final rating set
                        //var thread = new Thread(new ThreadStart(() => _performanceNotificationService.SendMail(mailList, Convert.ToInt32(ApplicationEnum.MailType.NotificationFromMgrToEmpOnRatingSet))));
                        //thread.Start();
                    }
                    if (name == "submit")
                    {
                        aobjEmpGoal.IsManagerRatingSubmitted = true;
                        aobjEmpGoal.IsReviewCompleted = true;
                        aobjEmpGoal.PerformanceStatusId = GetStatusIdByType(PerformanceEnum.PerformanceStatus.Presidentscouncilsignsoff.ToString());

                    }
                    _appContext.PerformanceEmpYearGoal.Update(aobjEmpGoal);
                }
            }
            _appContext.SaveChanges();
            return flag;
        }

    }
}
