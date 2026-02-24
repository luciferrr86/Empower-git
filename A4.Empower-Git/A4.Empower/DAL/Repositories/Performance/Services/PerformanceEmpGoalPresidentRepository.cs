using A4.DAL.Entites;
using DAL;
using DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace A4.DAL.Repositories
{
   public class PerformanceEmpGoalPresidentRepository : Repository<PerformanceEmpGoalPresident>, IPerformanceEmpGoalPresidentRepository
    {
        public PerformanceEmpGoalPresidentRepository(DbContext context) : base(context)
        {

        }
        private ApplicationDbContext _appContext => (ApplicationDbContext)_context;

    }
}
