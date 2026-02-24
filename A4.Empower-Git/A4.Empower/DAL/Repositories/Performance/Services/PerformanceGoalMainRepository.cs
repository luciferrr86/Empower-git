using A4.BAL;
using A4.DAL.Entites;
using DAL;
using DAL.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Text;

namespace A4.DAL.Repositories
{
    public class PerformanceGoalMainRepository : Repository<PerformanceGoalMain>, IPerformanceGoalMainRepository
    {
        private int Counter = 0;

        public PerformanceGoalMainRepository(DbContext context) : base(context)
        {

        }
        private ApplicationDbContext _appContext => (ApplicationDbContext)_context;

        public GetSetGoalModel GetSetGoalDetail(Guid currentYearId, string userId, string filterValue, List<DropDownList> quadrant, List<DropDownList> goalName)
        {
            var managerId = new Guid();
            var employeeId = new Guid();
            var emp = _appContext.Employee.Where(f => f.UserId == userId).FirstOrDefault();
            if (emp != null)
            {
                managerId = emp.ManagerId;
                employeeId = emp.Id;
            }
            var setGoalModel = new GetSetGoalModel();
            var goalMain = _appContext.PerformanceGoalMain.Where(d => d.ManagerId == employeeId & d.PerformanceYearId == currentYearId).FirstOrDefault();
            if (goalMain != null)
            {
                setGoalModel.IsGoalReleased = goalMain.IsManagerReleased;
            }
            var empusers = GetUserManageLevelList(employeeId, managerId);
            setGoalModel.lstSetGoal = getYearSetGoalDetail(employeeId, managerId, currentYearId, true, filterValue, quadrant, goalName);
            setGoalModel.SearchFunctionalGroup = GetFunctionalGroupList(employeeId);
            setGoalModel.SearchIndividualGroup = empusers.Item1;
            setGoalModel.SearchLevel = empusers.Item2;
            return setGoalModel;
        }



        private List<GetSetGoal> getYearSetGoalDetail(Guid employeeId, Guid managerId, Guid YearId, bool bIsCurrent, string filterValue, List<DropDownList> quadrant, List<DropDownList> goalName)
        {
            if (bIsCurrent)
            {
                Counter = 0;
            }
            var empusers = GetUserManageLevelList(employeeId, managerId);
            List<GetSetGoal> lstEmployeeSetGoal = new List<GetSetGoal>();
            var objPerGoal = _appContext.PerformanceGoalMain.Where(c => c.ManagerId == employeeId & c.PerformanceYearId == YearId).FirstOrDefault();
            if (objPerGoal != null)
            {
                var PerformanceGoalMeasure = _appContext.PerformanceGoalMeasure.Where(d => d.PerformanceGoalMainId == objPerGoal.Id & d.IsActive == true);
                if (PerformanceGoalMeasure.Count() > 0)
                {
                    foreach (var item in PerformanceGoalMeasure)
                    {
                        Counter++;
                        var objSetGoal = new GetSetGoal();
                        List<string> performancegoalmeasurefunction = new List<string>();
                        var PerformanceGoalMeasureFunc = _appContext.PerformanceGoalMeasureFunc.Where(e => e.PerformanceGoalMeasureId == item.Id & e.IsActive == true);
                        if (PerformanceGoalMeasureFunc.Count() > 0)
                        {
                            foreach (var item1 in PerformanceGoalMeasureFunc)
                            {
                                objSetGoal.LevelId = item1.Level.ToString();
                                performancegoalmeasurefunction.Add(item1.FunctionalGroupId.ToString());
                            }
                            objSetGoal.SelectedFunctionalGroup = SelectedFunGroupCheckBox(employeeId);
                            objSetGoal.SetFunctionalGroupId = performancegoalmeasurefunction.ToList();
                        }
                        else
                        {
                            var PerformanceGoalMeasureFuncInDiv = _appContext.PerformanceGoalMeasureIndiv.Where(g => g.PerformanceGoalMeasureId == item.Id & g.IsActive == true);
                            List<string> performancegoalmeasurefunctionIndiv = new List<string>();
                            if (PerformanceGoalMeasureFuncInDiv.Count() > 0)
                            {
                                foreach (var item1 in PerformanceGoalMeasureFuncInDiv)
                                {

                                    performancegoalmeasurefunctionIndiv.Add(item1.EmployeeId.ToString());
                                }

                            }
                            objSetGoal.SetIndividual = performancegoalmeasurefunctionIndiv.ToList();
                            objSetGoal.SelectedFunctionalGroup = GetFunctionalGroupList(employeeId);
                        }
                        objSetGoal.PerformanceGoalId = item.PerformanceGoalId.ToString();

                        //objSetGoal.PerformanceGoalName = item.n;
                        objSetGoal.FirstQuadrantId = item.StartTime;
                        objSetGoal.SecondQuadrantId = item.EndTime;
                        objSetGoal.GoalMeasure = item.MeasureText;
                        objSetGoal.SelectedQuadrant = quadrant;

                        objSetGoal.SelectedIndividualManager = empusers.Item1;

                        objSetGoal.SelectedLevel = empusers.Item2;
                        objSetGoal.PerformanceGoalMeasureId = item.Id.ToString();
                        objSetGoal.SelectedPerformanceGoal = goalName;
                        // objSetGoal.Counter = Counter;
                        // objSetGoal.YearId = YearId;
                        if (filterValue != null && filterValue != "0")
                        {
                            var splitValue = filterValue.Split(',');
                            var dropDown = splitValue[0];
                            var id = splitValue[1];
                            if (dropDown == "0")
                            {
                                if (objSetGoal.SetFunctionalGroupId != null && objSetGoal.SetFunctionalGroupId.Contains(id))
                                {
                                    lstEmployeeSetGoal.Add(objSetGoal);
                                }
                            }
                            if (dropDown == "1" && PerformanceGoalMeasureFunc.Count() == 0)
                            {
                                var objCheckIndiv = objSetGoal.SetIndividual.Contains(id);
                                if (objCheckIndiv)
                                {
                                    lstEmployeeSetGoal.Add(objSetGoal);
                                }
                            }
                        }
                        else
                        {
                            lstEmployeeSetGoal.Add(objSetGoal);
                        }
                    }
                }
                else
                {
                    if (filterValue == null || filterValue == "0")
                    {
                        Counter++;
                        var objSetGoal = new GetSetGoal();
                        objSetGoal.SelectedFunctionalGroup = GetFunctionalGroupList(employeeId).ToList();
                        objSetGoal.SelectedIndividualManager = empusers.Item1;
                        objSetGoal.SelectedLevel = empusers.Item2;
                        objSetGoal.SelectedPerformanceGoal = goalName;
                        objSetGoal.SelectedQuadrant = quadrant;
                        // objSetGoal.YearId = YearId;
                        //objSetGoal.Counter = Counter;
                        lstEmployeeSetGoal.Add(objSetGoal);
                    }

                }
            }
            return lstEmployeeSetGoal;
        }

        private Tuple<List<DropDownList>, List<DropDownList>> GetUserManageLevelList(Guid employeeId, Guid managerId)
        {

            //var emplevelDetail = cPerformanceGoalMain.GetAboveLevelEmployee(loginUserInfo.EmployeeId, loginUserInfo.ReportingHead).AsEnumerable().ToList();
            List<SqlParameter> a = new List<SqlParameter>();

            a.Add(new SqlParameter("@LoginUserID", SqlDbType.UniqueIdentifier));
            a[a.Count - 1].Value = employeeId;
            a.Add(new SqlParameter("@ReportingHead", SqlDbType.UniqueIdentifier));
            a[a.Count - 1].Value = managerId;


            var emplevelDetail = _appContext.EmployeeByLevel.FromSqlRaw("exec uspGetBelowLevelEmployee @LoginUserID,@ReportingHead", a.ToArray()).ToList();
            var userListEmp = from empLevel in emplevelDetail
                              select new DropDownList { Value = empLevel.EmpId.ToString(), Label = empLevel.Name };

            var userLevelEmp = (from empLevel in emplevelDetail.GroupBy(x => x.EmpLevel).Select(y => y.First())
                                select new DropDownList { Value = empLevel.EmpLevel.ToString(), Label = "Level" + empLevel.EmpLevel });
            return Tuple.Create(userListEmp.ToList(), userLevelEmp.ToList());
        }

        private List<DropDownList> SelectedFunGroupCheckBox(Guid employeeId)
        {
            var functionalGroup = GetFunctionalGroupList(employeeId).ToList();
            var listFunGroup = new List<DropDownList>();
            if (functionalGroup.Count > 0)
            {
                foreach (var item in functionalGroup)
                {
                    listFunGroup.Add(new DropDownList { Value = Convert.ToString(item.Value), Label = item.Label });
                }
            }
            return listFunGroup;
        }

        public List<DropDownList> GetFunctionalGroupList(Guid managerId)
        {
            var functionalGroupList = _appContext.FunctionalGroup.Where(p => p.IsActive == true);
            var userManageFunctionalGroupList = _appContext.Employee.Where(n => n.ManagerId == managerId & n.IsActive == true).ToList().Where(m => m.Id != managerId)
                .Select(m => m.GroupId).Distinct().ToList();

            var specficFunctionalGroupList = functionalGroupList.Where(fngroup => userManageFunctionalGroupList.Contains(fngroup.Id)).
                Select(fnGroup => new DropDownList
                {
                    Value = Convert.ToString(fnGroup.Id),
                    Label = fnGroup.Name
                });

            return specficFunctionalGroupList.ToList();

        }

        public bool SaveGoal(PostSetGoalModel setGoalModel, string userId, Guid currentYearId)
        {
            bool flag = false;
            foreach (var item in setGoalModel.lstSetGoal)
            {
                if (item.PerformanceGoalMeasureId == "" || item.PerformanceGoalMeasureId == null)
                {
                    flag = AddGoal(item, currentYearId, userId);
                }
                else
                {
                    flag = EditGoal(item, userId);
                }
            }
            _appContext.SaveChanges();
            return flag;
        }

        private bool AddGoal(SetGoal objEmployeeSetGoal, Guid YearId, string userId)
        {
            bool flag = false;
            var employeeId = new Guid();
            var emp = _appContext.Employee.Where(f => f.UserId == userId).FirstOrDefault();
            if (emp != null)
            {
                employeeId = emp.Id;
            }
            var goalMainId = _appContext.PerformanceGoalMain.Where(x => x.PerformanceYearId == YearId & x.ManagerId == employeeId).FirstOrDefault();
            if (goalMainId != null)
            {
                var PerformanceGoalMeasure = new PerformanceGoalMeasure();
                PerformanceGoalMeasure.CreatedBy = employeeId.ToString();
                PerformanceGoalMeasure.PerformanceGoalId = Guid.Parse(objEmployeeSetGoal.PerformanceGoalId);
                PerformanceGoalMeasure.StartTime = objEmployeeSetGoal.FirstQuadrantId;
                PerformanceGoalMeasure.EndTime = objEmployeeSetGoal.SecondQuadrantId;
                PerformanceGoalMeasure.MeasureText = objEmployeeSetGoal.GoalMeasure;
                PerformanceGoalMeasure.PerformanceGoalMainId = goalMainId.Id;
                PerformanceGoalMeasure.IsActive = true;
                _appContext.PerformanceGoalMeasure.Add(PerformanceGoalMeasure);

                if (objEmployeeSetGoal.SetIndividual == null && objEmployeeSetGoal.SetFunctionalGroupId.Count > 0)
                {
                    foreach (var item1 in objEmployeeSetGoal.SetFunctionalGroupId)
                    {
                        var performanceGoalMeasureFunc = new PerformanceGoalMeasureFunc();
                        performanceGoalMeasureFunc.CreatedBy = employeeId.ToString();
                        performanceGoalMeasureFunc.PerformanceGoalMeasureId = PerformanceGoalMeasure.Id;
                        performanceGoalMeasureFunc.FunctionalGroupId = Guid.Parse(item1);
                        performanceGoalMeasureFunc.Level = Convert.ToInt32(objEmployeeSetGoal.LevelId);
                        performanceGoalMeasureFunc.IsActive = true;
                        _appContext.PerformanceGoalMeasureFunc.Add(performanceGoalMeasureFunc);
                    }
                    flag = true;
                }
                else
                {
                    foreach (var item1 in objEmployeeSetGoal.SetIndividual.ToList())
                    {
                        var performanceGoalMeasureIndiv = new PerformanceGoalMeasureIndiv();
                        performanceGoalMeasureIndiv.CreatedBy = employeeId.ToString();
                        performanceGoalMeasureIndiv.PerformanceGoalMeasureId = PerformanceGoalMeasure.Id;
                        performanceGoalMeasureIndiv.EmployeeId = Guid.Parse(item1);
                        performanceGoalMeasureIndiv.IsActive = true;
                        _appContext.PerformanceGoalMeasureIndiv.Add(performanceGoalMeasureIndiv);
                    }
                    flag = true;
                }
            }

            return flag;

        }

        private bool EditGoal(SetGoal objEmployeeSetGoal, string userId)
        {
            bool flag = false;
            var employeeId = new Guid();
            var emp = _appContext.Employee.Where(f => f.UserId == userId).FirstOrDefault();
            if (emp != null)
            {
                employeeId = emp.Id;
            }
            var performanceGoalMeasure = _appContext.PerformanceGoalMeasure.Where(y => y.Id == Guid.Parse(objEmployeeSetGoal.PerformanceGoalMeasureId)).FirstOrDefault();
            if (performanceGoalMeasure != null)
            {
                performanceGoalMeasure.PerformanceGoalId = Guid.Parse(objEmployeeSetGoal.PerformanceGoalId);
                performanceGoalMeasure.StartTime = objEmployeeSetGoal.FirstQuadrantId;
                performanceGoalMeasure.EndTime = objEmployeeSetGoal.SecondQuadrantId;
                performanceGoalMeasure.MeasureText = objEmployeeSetGoal.GoalMeasure;
                _appContext.PerformanceGoalMeasure.Update(performanceGoalMeasure);
            }

            if (objEmployeeSetGoal.SetFunctionalGroupId != null && objEmployeeSetGoal.SetFunctionalGroupId.Count() > 0)
            {
                DeactiveFunctionalMeasure(performanceGoalMeasure.Id);
                DeactiveIndividualMeasure(performanceGoalMeasure.Id);

                foreach (var item in objEmployeeSetGoal.SetFunctionalGroupId)
                {
                    var PerformanceGoalMeasureFunc = _appContext.PerformanceGoalMeasureFunc.Where(x => x.PerformanceGoalMeasureId == performanceGoalMeasure.Id & x.FunctionalGroupId == Guid.Parse(item)).FirstOrDefault();
                    if (PerformanceGoalMeasureFunc == null)
                    {
                        var PerformanceGoalMeasurefunc = new PerformanceGoalMeasureFunc();
                        PerformanceGoalMeasurefunc.CreatedBy = employeeId.ToString();
                        PerformanceGoalMeasurefunc.PerformanceGoalMeasureId = performanceGoalMeasure.Id;
                        PerformanceGoalMeasurefunc.FunctionalGroupId = Guid.Parse(item);
                        PerformanceGoalMeasurefunc.Level = Convert.ToInt32(objEmployeeSetGoal.LevelId);
                        PerformanceGoalMeasurefunc.IsActive = true;
                        _appContext.PerformanceGoalMeasureFunc.Add(PerformanceGoalMeasurefunc);
                    }
                    else
                    {
                        PerformanceGoalMeasureFunc.Level = Convert.ToInt32(objEmployeeSetGoal.LevelId);
                        PerformanceGoalMeasureFunc.IsActive = true;
                        _appContext.PerformanceGoalMeasureFunc.Update(PerformanceGoalMeasureFunc);
                    }

                }
                flag = true;
            }
            else
            {

                if (objEmployeeSetGoal.SetIndividual != null && objEmployeeSetGoal.SetIndividual.Count() > 0)
                {

                    DeactiveFunctionalMeasure(performanceGoalMeasure.Id);
                    DeactiveIndividualMeasure(performanceGoalMeasure.Id);

                    foreach (var item in objEmployeeSetGoal.SetIndividual.ToList())
                    {
                        var PerformanceGoalMeasureIndiv = _appContext.PerformanceGoalMeasureIndiv.Where(h => h.PerformanceGoalMeasureId == performanceGoalMeasure.Id & h.EmployeeId == Guid.Parse(item)).FirstOrDefault();
                        if (PerformanceGoalMeasureIndiv == null)
                        {
                            var PerformanceGoalMeasureIndivi = new PerformanceGoalMeasureIndiv();
                            PerformanceGoalMeasureIndivi.CreatedBy = employeeId.ToString();
                            PerformanceGoalMeasureIndivi.PerformanceGoalMeasureId = performanceGoalMeasure.Id;
                            PerformanceGoalMeasureIndivi.EmployeeId = Guid.Parse(item);
                            PerformanceGoalMeasureIndivi.IsActive = true;
                            _appContext.PerformanceGoalMeasureIndiv.Add(PerformanceGoalMeasureIndivi);
                        }

                        else
                        {
                            PerformanceGoalMeasureIndiv.IsActive = true;
                            _appContext.PerformanceGoalMeasureIndiv.Update(PerformanceGoalMeasureIndiv);
                        }
                    }
                    flag = true;
                }


            }
            _appContext.SaveChanges();
            return flag;
        }

        public void DeactiveIndividualMeasure(Guid perMeaureId)
        {
            var PerformanceGoalMeasureIndiv = _appContext.PerformanceGoalMeasureIndiv.Where(r => r.PerformanceGoalMeasureId == perMeaureId);
            if (PerformanceGoalMeasureIndiv.Count() > 0)
            {
                foreach (var item in PerformanceGoalMeasureIndiv)
                {
                    item.IsActive = false;
                    _appContext.PerformanceGoalMeasureIndiv.Update(item);
                }
            }
            _appContext.SaveChanges();
        }

        public void DeactiveFunctionalMeasure(Guid perMeaureId)
        {
            var PerGoalMeasureFunc = _appContext.PerformanceGoalMeasureFunc.Where(j => j.PerformanceGoalMeasureId == perMeaureId);
            if (PerGoalMeasureFunc.Count() > 0)
            {
                foreach (var item in PerGoalMeasureFunc)
                {
                    item.IsActive = false;
                    _appContext.PerformanceGoalMeasureFunc.Update(item);
                }
            }
            _appContext.SaveChanges();
        }
    }
}
