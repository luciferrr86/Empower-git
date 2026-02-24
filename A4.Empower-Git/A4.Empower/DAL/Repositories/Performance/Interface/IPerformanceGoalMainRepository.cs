using A4.BAL;
using A4.DAL.Entites;
using DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace A4.DAL.Repositories
{
   public interface IPerformanceGoalMainRepository : IRepository<PerformanceGoalMain>
    {
        GetSetGoalModel GetSetGoalDetail(Guid currentYearId, string userId, string filterValue, List<DropDownList> quadrant, List<DropDownList> goalName);
        bool SaveGoal(PostSetGoalModel setGoalModel, string userId, Guid currentYearId);
    }
}
