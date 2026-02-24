using A4.DAL.Entites;
using DAL;
using DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace A4.DAL.Repositories
{
   public class PerformanceEmpMidYearGoalRepository : Repository<PerformanceEmpMidYearGoal>, IPerformanceEmpMidYearGoalRepository
    {
        public PerformanceEmpMidYearGoalRepository(DbContext context) : base(context)
        {

        }
        private ApplicationDbContext _appContext => (ApplicationDbContext)_context;
    
    }
}
