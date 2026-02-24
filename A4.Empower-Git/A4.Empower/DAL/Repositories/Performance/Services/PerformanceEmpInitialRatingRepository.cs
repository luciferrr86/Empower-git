using A4.DAL.Entites;
using DAL;
using DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace A4.DAL.Repositories
{
    public class PerformanceEmpInitialRatingRepository : Repository<PerformanceEmpInitialRating>, IPerformanceEmpInitialRatingRepository
    {
        public PerformanceEmpInitialRatingRepository(DbContext context) : base(context)
        {

        }
        private ApplicationDbContext _appContext => (ApplicationDbContext)_context;

    }
}
