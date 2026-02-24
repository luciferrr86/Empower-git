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
   public class PerformanceEmpTrainingClassesRepository : Repository<PerformanceEmpTrainingClasses>, IPerformanceEmpTrainingClassesRepository
    {
        public PerformanceEmpTrainingClassesRepository(DbContext context) : base(context)
        {

        }
        private ApplicationDbContext _appContext => (ApplicationDbContext)_context;

        public TrainingClassViewModel GetTrainingClasses(EmployeeGoalDetail empGoal)
        {
            var trainingClassViewModel = new TrainingClassViewModel();
            var config = _appContext.PerformanceConfig.Where(x=>x.IsActive).FirstOrDefault();
            string text = "N/A";
            if (config != null)
            {
                text = config.TrainingClassesInstructionText;
            }
            trainingClassViewModel.InstructionText = text;
            var trainingClasses = new List<TrainingClasses>();
            var trainingClassObj =_appContext.PerformanceEmpTrainingClasses.Where(x=>x.PerformanceEmpGoalId==Guid.Parse(empGoal.EmpGoalId));
            if (trainingClassObj.Count() > 0)
            {
                foreach (var item in trainingClassObj)
                {
                    trainingClasses.Add(new TrainingClasses
                    {
                        TrainingClassId = item.Id.ToString(),
                        TrainingClass = item.TrainingClasses,
                    });
                }
            }
            else
            {
                trainingClasses.Add(new TrainingClasses
                {
                    TrainingClassId = "",
                    TrainingClass = "",
                });
            }
            trainingClassViewModel.lstTrainingClasses = trainingClasses;             
            return trainingClassViewModel;

        }

        public bool SaveTrainingClasses(TrainingClassViewModel trainingClasses, EmployeeGoalDetail empDetail,string action)
        {
            bool flag = true;
            if (trainingClasses.lstTrainingClasses.Count() > 0)
            {
                foreach (var item in trainingClasses.lstTrainingClasses)
                {
                    //objTrainingClass.CreatedBy = loginId;
                    if (item.TrainingClassId == "")
                    {
                        PerformanceEmpTrainingClasses objTrainingClass = new PerformanceEmpTrainingClasses();
                        objTrainingClass.TrainingClasses = item.TrainingClass;
                        objTrainingClass.PerformanceEmpGoalId = Guid.Parse(empDetail.EmpGoalId);
                        _appContext.PerformanceEmpTrainingClasses.Add(objTrainingClass);
                    }
                    else
                    {
                        PerformanceEmpTrainingClasses objTrainingClass = _appContext.PerformanceEmpTrainingClasses.Find(Guid.Parse(item.TrainingClassId));
                        if(objTrainingClass!=null)
                        {
                            objTrainingClass.TrainingClasses = item.TrainingClass;
                            objTrainingClass.PerformanceEmpGoalId = Guid.Parse(empDetail.EmpGoalId);
                            _appContext.PerformanceEmpTrainingClasses.Update(objTrainingClass);
                        }                        
                       
                    }
                   
                }
                var empGoal = _appContext.PerformanceEmpGoal.Find(Guid.Parse(empDetail.EmpGoalId));
                if (empGoal!=null)
                {
                    var empYeargoalDetail = _appContext.PerformanceEmpYearGoal.Where(x=>x.PerformanceEmpGoalId==empGoal.Id).FirstOrDefault();
                    if (empYeargoalDetail!=null)
                    {
                        if (action == "save")
                        {
                            empYeargoalDetail.IsEmployeeTrainingSaved = true;
                        }
                        else 
                        {
                            empYeargoalDetail.IsEmployeeTrainingSubmitted = true;
                        }
                        _appContext.PerformanceEmpYearGoal.Update(empYeargoalDetail);
                    }
                }
                _appContext.SaveChanges();
            }
            return flag;
        }
    }
}
