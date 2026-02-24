using A4.BAL;
using A4.DAL.Entites;
using DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace A4.DAL.Repositories
{
   public interface IPerformanceEmpDevGoalRepository : IRepository<PerformanceEmpDevGoal>
    {
        CareerDevViewModel GetDevelopmentPlan(EmployeeGoalDetail empGoal);
        void SaveDevelopmentPlan(CareerDevViewModel careerDevViewModel,EmployeeGoalDetail empDetail, string action, bool midYearEnabled,Employee employee);
        void SaveMgrDevelopmentPlan(CareerDevViewModel careerDevViewModel, EmployeeGoalDetail empDetail, string action, bool midYearEnabled, Employee employee);
    }
}

