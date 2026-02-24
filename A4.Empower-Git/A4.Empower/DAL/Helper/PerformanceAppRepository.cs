using A4.BAL;
using A4.DAL.Entites;
using DAL;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Text;

namespace A4.DAL
{
    public class PerformanceAppRepository : IPerformanceAppRepository
    {
        ApplicationDbContext _appContext;

        public PerformanceAppRepository(ApplicationDbContext appContext)
        {
            _appContext = appContext;

        }

        public List<DropDownList> GetQuadrantList()
        {
            var myList = new List<DropDownList>();
            myList.Add(new DropDownList { Value = "First Quarter", Label = "First Quarter" });
            myList.Add(new DropDownList { Value = "Second Quarter", Label = "Second Quarter" });
            myList.Add(new DropDownList { Value = "Third Quarter", Label = "Third Quarter" });
            myList.Add(new DropDownList { Value = "Fourth Quarter", Label = "Fourth Quarter" });
            return myList;
        }

        public List<DropDownList> GetPerformanceGoalNames(string userId, Guid yearId)
        {
            var performanceGoals = new List<DropDownList>();
            Guid managerId = new Guid();
            Guid empId = new Guid(userId);
            var objEmp = _appContext.Employee.Where(h => h.UserId == empId.ToString()).FirstOrDefault();
            if (objEmp != null)
            {
                managerId = objEmp.ManagerId;
            }
            var performancegoalnamesList = _appContext.PerformanceGoal.Where(f => f.EmployeeId == managerId & f.PerformanceYearId == yearId & f.IsActive == true);
            foreach (var item in performancegoalnamesList)
            {
                performanceGoals.Add(new DropDownList { Value = Convert.ToString(item.Id), Label = item.GoalName });
            }
            return performanceGoals;
        }


        public bool CheckManagerRelease(string userId, Guid yearId)
        {
            bool flag = false;
            Guid managerId = new Guid();

            var objEmp = _appContext.Employee.Where(h => h.UserId == userId.ToString()).FirstOrDefault();
            if (objEmp != null)
            {
                managerId = objEmp.ManagerId;
            }


            var objGoalMain = _appContext.PerformanceGoalMain.Where(i => i.ManagerId == managerId & i.PerformanceYearId == yearId).FirstOrDefault();
            if (objGoalMain != null)
            {
                flag = objGoalMain.IsManagerReleased;
            }

            return flag;
        }

        public bool CheckEmployeePerformanceStart(string userId)
        {
            var flag = false;
            var aobjPerformanceYear = _appContext.PerformanceYear.Where(t => t.IsCompleted == false & t.IsActive == true).FirstOrDefault();
            if (aobjPerformanceYear != null)
            {
                var emp = GetEmployeeByUserId(userId);
                var aobjempGoal = _appContext.PerformanceEmpGoal.Where(g => g.PerformanceYearId == aobjPerformanceYear.Id & g.EmployeeId == emp.Id);
                if (aobjempGoal.Count() > 0)
                {
                    flag = true;
                }
            }
            return flag;
        }

        public bool CheckPerformanceStart()
        {
            var flag = false;
            var aobjPerformanceYear = _appContext.PerformanceYear.Where(t => t.IsCompleted == false & t.IsActive == true).FirstOrDefault();
            if (aobjPerformanceYear != null)
            {
                flag = true;
            }
            return flag;
        }

        public PerformanceFeatures GetFeatures()
        {
            var performanceFeature = new PerformanceFeatures();
            var objAppModule = _appContext.ApplicationModule.Where(v => v.ModuleName == "Performance").FirstOrDefault();
            if (objAppModule != null)
            {
                var objAppModuleDetail = _appContext.ApplicationModuleDetail.Where(h => h.ApplicationModuleId == objAppModule.Id & h.IsActive == true);
                if (objAppModuleDetail.Count() > 0)
                {
                    foreach (var item in objAppModuleDetail)
                    {
                        var configType = (PerformanceEnum.PerformanceConfig)Enum.Parse(typeof(PerformanceEnum.PerformanceConfig), item.ConfigType.ToString());
                        switch (configType)
                        {
                            case PerformanceEnum.PerformanceConfig.EnableIntialRating:
                                performanceFeature.IsInitailRatingEnabled = item.IsActive;
                                break;
                            case PerformanceEnum.PerformanceConfig.EnableDeltaAndPluses:
                                performanceFeature.IsDeltaPlusesEnabled = item.IsActive;
                                break;
                            case PerformanceEnum.PerformanceConfig.EnablePresidentCouncil:
                                performanceFeature.IsPresidentCouncilEnabled = item.IsActive;
                                break;

                            case PerformanceEnum.PerformanceConfig.EnableTrainingAndClasses:
                                performanceFeature.IsTrainingClassesEnabled = item.IsActive;
                                break;
                            case PerformanceEnum.PerformanceConfig.EnableMidYear:
                                performanceFeature.IsMidYearEnabled = item.IsActive;
                                break;
                            case PerformanceEnum.PerformanceConfig.EnableSuperAdmin:
                                performanceFeature.IsSuperAdminEnabled = item.IsActive;
                                break;
                            case PerformanceEnum.PerformanceConfig.EnableNextYear:
                                performanceFeature.IsNextYearEnabled = item.IsActive;
                                break;
                            default:
                                Console.WriteLine("Invalid search");
                                break;
                        }
                    }
                }
            }
            if (performanceFeature.IsInitailRatingEnabled)
            {
                performanceFeature.IsCEOSignOffEnabled = true;
            }
            return performanceFeature;

        }

        public bool CheckIsRatingSignedOff(string id, string userId, Guid currentYearId)
        {
            bool flag = false;
            string loginId = "";
            if (id != "")
            {
                loginId = id;
            }
            else
            {
                var emp = GetEmployeeByUserId(userId);
                loginId = emp.Id.ToString();
            }
            var objRatingSignOff = _appContext.PerformanceInitailRating.Where(f => f.PerformanceYearId == currentYearId & f.EmployeeId == Guid.Parse(loginId)).FirstOrDefault();
            if (objRatingSignOff != null)
            {
                flag = objRatingSignOff.IsCEOSignOff;
            }
            return flag;
        }

        public Employee GetEmployeeByUserId(string userId)
        {
            var emp = new Employee();
            var employee = _appContext.Employee.Where(x => x.UserId == userId & x.IsActive == true).FirstOrDefault();
            if (employee != null)
            {
                emp = employee;
            }
            return emp;
        }

        public EmployeeGoalDetail EmployeeGoalDetail(string userId, Guid currentYearId)
        {
            var empGoalDetail = new EmployeeGoalDetail();
            var emp = GetEmployeeByUserId(userId);
            var empGoal = _appContext.PerformanceEmpGoal.Where(s => s.PerformanceYearId == currentYearId & s.EmployeeId == emp.Id).FirstOrDefault();
            if (empGoal != null)
            {
                if (GetFeatures().IsMidYearEnabled)
                {
                    var empGoalMidYear = _appContext.PerformanceEmpMidYearGoal.Where(g => g.PerformanceEmpGoalId == empGoal.Id).FirstOrDefault();
                    if (empGoalMidYear != null)
                    {
                        empGoalDetail.EmployeeComment = empGoalMidYear.EmployeeAccComment;
                        empGoalDetail.ManagerComment = empGoalMidYear.ManagerAccComment;
                        empGoalDetail.MidYearRating = empGoalMidYear.FinalRating;
                    }
                }
                empGoalDetail.EmpGoalId = empGoal.Id.ToString();
                empGoalDetail.EmployeeId = empGoal.EmployeeId.ToString();
                empGoalDetail.ManagerId = empGoal.ManagerId.ToString();
                empGoalDetail.ManagerYearEndSignature = empGoal.ManagerSignature;
                var empGoalEndYear = _appContext.PerformanceEmpYearGoal.Where(g => g.PerformanceEmpGoalId == empGoal.Id).FirstOrDefault();
                if (empGoalEndYear != null)
                {
                    empGoalDetail.EmployeeSignature = empGoalEndYear.EmployeeSignature;
                    empGoalDetail.EmployeeYearEndComment = empGoalEndYear.EmployeeAccComment;
                    empGoalDetail.FinalRating = empGoalEndYear.FinalRating;
                    empGoalDetail.ManagerYearEndComment = empGoalEndYear.ManagerAccComment;
                    var managerName = "";

                    empGoalDetail.InitialRating = GetInitialRating(emp.Id, currentYearId);
                    var objEmployee = _appContext.Employee.Where(x => x.Id == emp.ManagerId).FirstOrDefault();
                    if (objEmployee != null)
                    {
                        var manager = _appContext.Users.Where(k => k.Id == objEmployee.UserId).FirstOrDefault();
                        if (manager != null)
                        {
                            managerName = manager.FullName;
                        }
                    }

                    empGoalDetail.ManagerSignature = managerName;
                    empGoalDetail.IsMgrRatingSubmit = empGoalEndYear.IsManagerRatingSubmitted;
                }
                var objYear = _appContext.PerformanceYear.Where(x => x.Id == empGoal.PerformanceYearId).FirstOrDefault();
                if (objYear != null)
                {
                    empGoalDetail.PerYear = objYear.Year;
                }
                var empGoalPresident = _appContext.PerformanceEmpGoalPresident.Where(c => c.PerformanceEmpGoalId == empGoal.Id).FirstOrDefault();
                if (empGoalPresident != null)
                {
                    empGoalDetail.PresidentComment = empGoalPresident.PresidentComment;
                    empGoalDetail.PresidentYearEndComment = empGoalPresident.PresidentYearComment;
                    empGoalDetail.PresidentSignature = empGoalPresident.PresidentSignature;
                }

            }

            return empGoalDetail;
        }

        public EmployeeDetail GetEmployeeDetail(string userId)
        {
            var empDetail = new EmployeeDetail();
            var empObj = GetEmployeeByUserId(userId);
            string departmentName = "N/A";
            string groupName = "N/A";
            string title = "N/A";
            string managerName = "N/A";
            var aspNetUser = _appContext.Users.Find(userId);
            if (aspNetUser != null)
            {
                empDetail.Name = aspNetUser.FullName;
            }

            if (empObj.TitleId != Guid.Empty)
            {
                var functionalTitle = _appContext.FunctionalTitle.Find(empObj.TitleId);
                if (functionalTitle != null)
                {
                    title = functionalTitle.Name;
                }
            }
            empDetail.Title = title;
            if (empObj.GroupId != Guid.Empty)
            {
                var functionalGroup = _appContext.FunctionalGroup.Find(empObj.GroupId);
                if (functionalGroup != null)
                {
                    groupName = functionalGroup.Name;
                }
            }
            empDetail.FunctionalGroup = groupName;
            var mgr = _appContext.Employee.Find(empObj.ManagerId);
            if (mgr != null)
            {
                var mgrUser = _appContext.Users.Find(mgr.UserId);
                if (mgrUser != null)
                {
                    managerName = mgrUser.FullName;
                }
            }
            empDetail.EvaluatorName = managerName;

            var objGroup = _appContext.FunctionalGroup.Find(empObj.GroupId);
            if (objGroup != null)
            {
                var objDepartment = _appContext.FunctionalDepartment.Find(objGroup.DepartmentId);
                if (objDepartment != null)
                {
                    departmentName = objDepartment.Name;
                }
            }
            empDetail.FunctionalDepartment = departmentName;
            empDetail.DateReview = DateTime.Now.ToString();
            return empDetail;

        }

        private string GetInitialRating(Guid empId, Guid currentYearId)
        {
            string InitialRatingText = "";
            var aobjInitialRatingDesc = _appContext.PerformanceInitailRating.Where(x => x.PerformanceYearId == currentYearId & x.EmployeeId == empId).FirstOrDefault();
            if (aobjInitialRatingDesc != null)
            {
                var configRating = _appContext.PerformanceConfigRating.Where(c => c.Id == aobjInitialRatingDesc.PerformanceConfigRatingId).FirstOrDefault();
                if (configRating != null)
                {
                    InitialRatingText = configRating.RatingName;
                }
            }
            return InitialRatingText;
        }

        public bool CheckIsMidYearReviewCompleted(Guid currentYearId)
        {
            bool flag = false;
            var empGoal = _appContext.PerformanceEmpGoal.Where(x => x.PerformanceYearId == currentYearId).FirstOrDefault();
            if (empGoal != null)
            {
                var listPerformanceEmpMidYearGoal = _appContext.PerformanceEmpMidYearGoal.Where(x => x.PerformanceEmpGoalId == empGoal.Id);
                if (listPerformanceEmpMidYearGoal.Count() > 0 && !listPerformanceEmpMidYearGoal.Any(x => x.IsMidYearReviewCompleted == false))
                {
                    flag = true;
                }
            }

            return flag;
        }

        public CheckSaveSubmit GetSaveSubmit(string empId, Guid yearId, bool isMidYearEnabled)
        {
            var empGoalDetail = new CheckSaveSubmit();
            var empGoal = _appContext.PerformanceEmpGoal.Where(x => x.PerformanceYearId == yearId & x.EmployeeId == Guid.Parse(empId)).FirstOrDefault();
            if (empGoal != null)
            {
                empGoalDetail.EnableMidYear = isMidYearEnabled;
                empGoalDetail.IsMidYearReviewCompleted = CheckIsMidYearReviewCompleted(yearId);
                if (isMidYearEnabled)
                {
                    var empMidYearGoal = _appContext.PerformanceEmpMidYearGoal.Where(x => x.PerformanceEmpGoalId == empGoal.Id).FirstOrDefault();
                    if (empMidYearGoal != null)
                    {
                        empGoalDetail.IsEmpGoalSave = empMidYearGoal.IsEmployeeGoalSaved;
                        empGoalDetail.IsMgrGoalSave = empMidYearGoal.IsManagerGoalSaved;
                        empGoalDetail.IsEmpGoalSubmit = empMidYearGoal.IsEmployeeGoalSubmitted;
                        empGoalDetail.IsMgrGoalSubmit = empMidYearGoal.IsManagerGoalSubmitted;

                        empGoalDetail.IsEmpRatingSave = empMidYearGoal.IsEmployeeRatingSaved;
                        empGoalDetail.IsMgrRatingSave = empMidYearGoal.IsManagerRatingSaved;
                        empGoalDetail.IsEmpRatingSubmit = empMidYearGoal.IsEmployeeRatingSubmitted;
                        empGoalDetail.IsMgrRatingSubmit = empMidYearGoal.IsManagerRatingSubmitted;
                    }
                }

                var empYearGoal = _appContext.PerformanceEmpYearGoal.Where(x => x.PerformanceEmpGoalId == empGoal.Id).FirstOrDefault();
                if (empYearGoal != null)
                {
                    empGoalDetail.IsEmpYearGoalSave = empYearGoal.IsEmployeeGoalSaved;
                    empGoalDetail.IsMgrYearGoalSave = empYearGoal.IsManagerGoalSaved;
                    empGoalDetail.IsEmpYearGoalSubmit = empYearGoal.IsEmployeeGoalSubmitted;
                    empGoalDetail.IsMgrYearGoalSubmit = empYearGoal.IsManagerGoalSubmitted;

                    empGoalDetail.IsEmpYearRatingSave = empYearGoal.IsEmployeeRatingSaved;
                    empGoalDetail.IsMgrYearRatingSave = empYearGoal.IsManagerRatingSaved;
                    empGoalDetail.IsEmpYearRatingSubmit = empYearGoal.IsEmployeeRatingSubmitted;
                    empGoalDetail.IsMgrYearRatingSubmit = empYearGoal.IsManagerRatingSubmitted;

                    empGoalDetail.IsEmpDeltaPlusSubmitted = empYearGoal.IsEmployeeDeltaPlusSubmitted;
                    empGoalDetail.IsEmpDeltaPlusSaved = empYearGoal.IsEmployeeDeltaPlusSaved;
                    empGoalDetail.IsMgrDeltaPlusSubmitted = empYearGoal.IsManagerDeltaPlusSubmitted;
                    empGoalDetail.IsMgrDeltaPlusSaved = empYearGoal.IsManagerDeltaPlusSaved;

                    empGoalDetail.IsEmpDevGoalSubmitted = empYearGoal.IsEmployeeDevGoalSubmitted;
                    empGoalDetail.IsEmpDevGoalSaved = empYearGoal.IsEmployeeDevGoalSaved;
                    empGoalDetail.IsMgrDevGoalSubmitted = empYearGoal.IsManagerDevGoalSubmitted;
                    empGoalDetail.IsMgrDevGoalSaved = empYearGoal.IsManagerDevGoalSaved;

                    empGoalDetail.IsEmpTrainingSubmitted = empYearGoal.IsEmployeeTrainingSubmitted;
                    empGoalDetail.IsEmpTrainingSaved = empYearGoal.IsEmployeeTrainingSaved;
                    empGoalDetail.IsMgrTrainingSubmitted = empYearGoal.IsManagerTrainingSubmitted;
                    empGoalDetail.IsMgrTrainingSaved = empYearGoal.IsManagerTrainingSaved;
                }

                var perGoalPresident = _appContext.PerformanceEmpGoalPresident.Where(x => x.PerformanceEmpGoalId == empGoal.Id).FirstOrDefault();
                if (perGoalPresident != null)
                {
                    empGoalDetail.IsPresidentSubmit = perGoalPresident.PresidentSignOff;
                    empGoalDetail.IsPresidentYearSubmit = perGoalPresident.PresidentYearSignOff;
                    if (perGoalPresident.PresidentComment != null)
                    {
                        empGoalDetail.IsPresidentSave = true;
                    }
                    if (perGoalPresident.PresidentYearComment != null)
                    {
                        empGoalDetail.IsPresidentYearSave = true;
                    }
                }
            }
            return empGoalDetail;
        }

        public bool IsPresidentMemeber(Guid employeeId, Guid currentYearId)
        {
            var flag = false;
            var emp = _appContext.PerformancePresidentCouncil.Where(x => x.PerformanceYearId == currentYearId & x.EmployeeId == employeeId).FirstOrDefault();
            if (emp != null)
            {
                if (emp.PresidentId == employeeId)
                {
                    flag = true;
                }
            }
            return flag;
        }

        public List<DropDownList> GetHistoryList(Guid employeeId)
        {
            var lstHistory = new List<DropDownList>();
            var objPerformanceHistory = _appContext.PerformanceEmpGoal.Where(x => x.EmployeeId == employeeId);
            if (objPerformanceHistory.Count() > 0)
            {
                foreach (var item in objPerformanceHistory)
                {
                    var objPerformanceYear = _appContext.PerformanceYear.Find(item.PerformanceYearId);
                    {
                        if (objPerformanceYear != null)
                        {
                            if (objPerformanceYear.IsCompleted == true)
                            {
                                lstHistory.Add(new DropDownList { Value = objPerformanceYear.Id.ToString(), Label = objPerformanceYear.Year });
                            }
                        }
                    }

                }
            }
            return lstHistory;
        }

        public PerformanceConfig GetPerformanceConfig()
        {
            var performanceConfig = new PerformanceConfig();
            var objPerformanceConfig = _appContext.PerformanceConfig.First();
            if (objPerformanceConfig != null)
            {
                performanceConfig.MyGoalInstructionText = objPerformanceConfig.MyGoalInstructionText;
                performanceConfig.TrainingClassesInstructionText = objPerformanceConfig.TrainingClassesInstructionText;
                performanceConfig.PlusesInstructionText = objPerformanceConfig.PlusesInstructionText;
                performanceConfig.DeltaInstructionText = objPerformanceConfig.DeltaInstructionText;
                performanceConfig.RatingInstructionText = objPerformanceConfig.RatingInstructionText;
                performanceConfig.CareerDevInstructionText = objPerformanceConfig.CareerDevInstructionText;
            }
            return performanceConfig;
        }

        public bool CheckIsCEO(string userId)
        {
            bool flag = false;
            Guid managerId = new Guid();
            Guid empId = new Guid();
            var objEmp = _appContext.Employee.Where(h => h.UserId == userId.ToString()).FirstOrDefault();
            if (objEmp != null)
            {
                managerId = objEmp.ManagerId;
                empId = objEmp.Id;
            }

            if (empId == managerId)
            {

                flag = true;

            }
            return flag;

        }

        public List<TaskListModel> GetPerformanceTaskList(string id,Guid YearId)
        {
            var emplevelDetail = new List<TaskListModel>();            
            List<SqlParameter> a = new List<SqlParameter>();
            a.Add(new SqlParameter("@loginId", SqlDbType.UniqueIdentifier));
            a[a.Count - 1].Value = Guid.Parse(id);
            a.Add(new SqlParameter("@quadId", SqlDbType.UniqueIdentifier));
            a[a.Count - 1].Value = YearId;
            emplevelDetail = _appContext.TaskList.FromSqlRaw("exec uspGetTaskList @loginId,@quadId", a.ToArray()).ToList();
            return emplevelDetail;
        }
    }
}
