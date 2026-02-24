using A4.BAL;
using A4.DAL.Entites;
using DAL;
using DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace A4.DAL.Repositories
{
   public class PerformanceGoalRepository : Repository<PerformanceGoal>, IPerformanceGoalRepository
    {
        public PerformanceGoalRepository(DbContext context) : base(context)
        {

        }
        private ApplicationDbContext _appContext => (ApplicationDbContext)_context;


        public Goal AddGoalName(string goalName, Guid yearId, Guid mgrId)
        {
            var goal = new Goal();
            if (goalName != string.Empty && goalName != null)
            {
                var performanceGoal = new PerformanceGoal();
                performanceGoal.PerformanceYearId = yearId;
                performanceGoal.GoalName = goalName;
                performanceGoal.EmployeeId = mgrId;
                performanceGoal.CreatedBy = mgrId.ToString();
                performanceGoal.IsActive = true;
                _appContext.PerformanceGoal.Add(performanceGoal);
                goal.GoalId = performanceGoal.Id.ToString();
                goal.GoalName = performanceGoal.GoalName;
                _appContext.SaveChanges();
            }
            return goal;
        }
    }
}
