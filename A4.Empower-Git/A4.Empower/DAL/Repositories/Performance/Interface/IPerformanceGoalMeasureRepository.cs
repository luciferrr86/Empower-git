using A4.DAL.Entites;
using DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace A4.DAL.Repositories
{
   public interface IPerformanceGoalMeasureRepository:IRepository<PerformanceGoalMeasure>
    {
        bool DeleteGoalMeasure(string measureGoalId, bool isNextYearGoal);
    }
}
