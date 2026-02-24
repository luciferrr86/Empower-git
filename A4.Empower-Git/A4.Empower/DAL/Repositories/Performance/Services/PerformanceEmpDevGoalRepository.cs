using A4.BAL;
using A4.DAL.Entites;
using DAL;
using DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace A4.DAL.Repositories
{
    public class PerformanceEmpDevGoalRepository : Repository<PerformanceEmpDevGoal>, IPerformanceEmpDevGoalRepository
    {
        public PerformanceEmpDevGoalRepository(DbContext context) : base(context)
        {

        }
        private ApplicationDbContext _appContext => (ApplicationDbContext)_context;

        public CareerDevViewModel GetDevelopmentPlan(EmployeeGoalDetail empGoal)
        {
            var careerDevViewModel = new CareerDevViewModel();
            var config = _appContext.PerformanceConfig.Where(x=>x.IsActive).FirstOrDefault();
            string text = "N/A";
            if (config != null)
            {
                text = config.CareerDevInstructionText;
            }
            careerDevViewModel.InstructionText = text;
            careerDevViewModel.EmployeeCareerDevList = GetCareerDevelopment(empGoal.EmpGoalId, empGoal.EmployeeId);
            careerDevViewModel.ManagerCareerDevList = GetCareerDevelopment(empGoal.EmpGoalId, empGoal.ManagerId);
            return careerDevViewModel;
        }

        private List<CareerDevelopment> GetCareerDevelopment(string goalId, string createdBy)
        {
            var careerDevelopment = new List<CareerDevelopment>();
            var empDevGoal = _appContext.PerformanceEmpDevGoal.Where(x => x.PerformanceEmpGoalId == Guid.Parse(goalId) && x.DevGoalBy == createdBy);
            if (empDevGoal.Count() > 0)
            {
                foreach (var item in empDevGoal)
                {
                    careerDevelopment.Add(new CareerDevelopment
                    {
                        CareerDevId = item.Id.ToString(),
                        SkillText = item.SkillDevelopment,
                        //CreatedBy = item.iCreatedby,
                        CareerInterestText = item.CareerInterest

                    });
                }
            }
            else
            {
                careerDevelopment.Add(new CareerDevelopment { CareerDevId = "", CareerInterestText = "", SkillText = "" });
            }
            return careerDevelopment;
        }

        public void SaveDevelopmentPlan(CareerDevViewModel careerDevViewModel, EmployeeGoalDetail empDetail, string action, bool midYearEnabled, Employee employee)
        {
            if (careerDevViewModel.EmployeeCareerDevList != null && careerDevViewModel.EmployeeCareerDevList.Count() > 0)
            {
                SaveDevPlanList(careerDevViewModel.EmployeeCareerDevList, empDetail.EmployeeId, empDetail.EmpGoalId);
            }
           

            //Save For Document File:-
            //if (docFile != null && docFile.Count > 0)
            //{
            //    foreach (var item in docFile)
            //    {
            //        if (item != null)
            //        {
            //            cPerformanceEmpDevGoalDoc objfile = cPerformanceEmpDevGoalDoc.Create();
            //            string extension = Path.GetExtension(item.FileName).ToString();
            //            string filename = Path.GetFileNameWithoutExtension(item.FileName) + "_" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + extension;
            //            string path = Path.Combine(HttpContext.Current.Server.MapPath("~/UploadFile/Document"), filename);
            //            item.SaveAs(path);
            //            objfile.sFileName = filename;
            //            objfile.objPerformanceEmpGoal.iObjectID = careerDevViewModel.EmpGoalID;
            //            objfile.iCreatedby = loginId;
            //            objfile.Save();
            //        }

            //    }
            //}

            if (midYearEnabled)
            {
                var objEmpMidYear =_appContext.PerformanceEmpMidYearGoal.Where(x=>x.PerformanceEmpGoalId==Guid.Parse(empDetail.EmpGoalId)).FirstOrDefault();
                if (objEmpMidYear != null)
                {
                    objEmpMidYear.PerformanceStatusId = GetStatusIdByType(PerformanceEnum.PerformanceStatus.EmployeeSubmitted.ToString());
                    _appContext.PerformanceEmpMidYearGoal.Update(objEmpMidYear);
                }
            }


            var objEmpGoal =_appContext.PerformanceEmpYearGoal.Where(x=>x.PerformanceEmpGoalId== Guid.Parse(empDetail.EmpGoalId)).FirstOrDefault();
            if (objEmpGoal != null)
            {
               // var empYearGoal = new PerformanceEmpYearGoal();
                if (action == "save")
                {
                    objEmpGoal.IsEmployeeDevGoalSaved = true;
                }
                else 
                {
                    if (empDetail.EmployeeId == employee.Id.ToString())
                    {
                        if (!midYearEnabled)
                        {
                            objEmpGoal.PerformanceStatusId = GetStatusIdByType(PerformanceEnum.PerformanceStatus.EmployeeSubmitted.ToString());
                        }
                        objEmpGoal.IsEmployeeDevGoalSubmitted = true;
                    }                   
                   
                }
                _appContext.PerformanceEmpYearGoal.Update(objEmpGoal);
            }
            _appContext.SaveChanges();
        }

        public void SaveMgrDevelopmentPlan(CareerDevViewModel careerDevViewModel, EmployeeGoalDetail empDetail, string action, bool midYearEnabled, Employee employee)
        {
           
            if (careerDevViewModel.ManagerCareerDevList != null && careerDevViewModel.ManagerCareerDevList.Count() > 0)
            {
                SaveDevPlanList(careerDevViewModel.ManagerCareerDevList, empDetail.ManagerId, empDetail.EmpGoalId);
            }

            //Save For Document File:-
            //if (docFile != null && docFile.Count > 0)
            //{
            //    foreach (var item in docFile)
            //    {
            //        if (item != null)
            //        {
            //            cPerformanceEmpDevGoalDoc objfile = cPerformanceEmpDevGoalDoc.Create();
            //            string extension = Path.GetExtension(item.FileName).ToString();
            //            string filename = Path.GetFileNameWithoutExtension(item.FileName) + "_" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + extension;
            //            string path = Path.Combine(HttpContext.Current.Server.MapPath("~/UploadFile/Document"), filename);
            //            item.SaveAs(path);
            //            objfile.sFileName = filename;
            //            objfile.objPerformanceEmpGoal.iObjectID = careerDevViewModel.EmpGoalID;
            //            objfile.iCreatedby = loginId;
            //            objfile.Save();
            //        }

            //    }
            //}

            var objEmpGoal = _appContext.PerformanceEmpYearGoal.Where(x => x.PerformanceEmpGoalId == Guid.Parse(empDetail.EmpGoalId)).FirstOrDefault();
            if (objEmpGoal != null)
            {               
                if (action == "save")
                {
                    objEmpGoal.IsManagerDevGoalSaved = true;
                }
                else
                {
                        objEmpGoal.IsManagerDevGoalSubmitted = true;
                }
                _appContext.PerformanceEmpYearGoal.Update(objEmpGoal);
            }
            _appContext.SaveChanges();
        }

        private void SaveDevPlanList(List<CareerDevelopment> lstCareerDevelopment, string createdBy, string goalId)
        {

            foreach (var item in lstCareerDevelopment)
            {
                
                if (item.CareerDevId != "")
                {
                    var carrerDevGoalObj = _appContext.PerformanceEmpDevGoal.Find(Guid.Parse(item.CareerDevId));
                    if (carrerDevGoalObj != null)
                    {
                        carrerDevGoalObj.SkillDevelopment = item.SkillText;
                        carrerDevGoalObj.CareerInterest = item.CareerInterestText;
                        _appContext.PerformanceEmpDevGoal.Update(carrerDevGoalObj);

                    }
                }
                else
                {
                    var empDevGoal = new PerformanceEmpDevGoal();
                    empDevGoal.SkillDevelopment = item.SkillText;
                    empDevGoal.CareerInterest = item.CareerInterestText;
                    empDevGoal.DevGoalBy = createdBy;
                    empDevGoal.PerformanceEmpGoalId = Guid.Parse(goalId);
                    _appContext.PerformanceEmpDevGoal.Add(empDevGoal);

                }
            }            
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

        private List<CareerDevDocument> GetCareerDevDoc(Guid empGoalId)
        {
            var careerDevDoc = new List<CareerDevDocument>();
            var empDevGoalDoc = _appContext.PerformanceEmpDevGoalDoc.Where(m => m.PerformanceEmpGoalId == empGoalId).ToList();
            if (empDevGoalDoc.Count > 0)
            {
                foreach (var item in empDevGoalDoc)
                {
                    careerDevDoc.Add(new CareerDevDocument { CareerDevDocId = item.Id.ToString(), FileName = item.FileName });
                }
            }
            return careerDevDoc;
        }
    }
}

