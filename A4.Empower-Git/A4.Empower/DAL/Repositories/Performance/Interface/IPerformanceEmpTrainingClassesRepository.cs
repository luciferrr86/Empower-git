using A4.BAL;
using A4.DAL.Entites;
using DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace A4.DAL.Repositories
{
   public interface IPerformanceEmpTrainingClassesRepository : IRepository<PerformanceEmpTrainingClasses>
    {
        TrainingClassViewModel GetTrainingClasses(EmployeeGoalDetail empGoal);
        bool SaveTrainingClasses(TrainingClassViewModel trainingClasses, EmployeeGoalDetail empDetail, string action);
    }
}
