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
   public class PerformanceConfigRepository : Repository<PerformanceConfig>, IPerformanceConfigRepository
    {
        public PerformanceConfigRepository(DbContext context) : base(context)
        {
        }
        private ApplicationDbContext _appContext => (ApplicationDbContext)_context;
        public override PerformanceConfig Get(Guid id)
        {
            var performanceConfig = _appContext.PerformanceConfig.Where(e => e.Id == id && e.IsActive == true).FirstOrDefault();
            return performanceConfig;
        }
        public override void Add(PerformanceConfig entity)
        {
            base.Add(entity);
            _context.SaveChanges();
        }

        public override void Update(PerformanceConfig entity)
        {
            base.Update(entity);
            _context.SaveChanges();
        }

        public override void Remove(PerformanceConfig entity)
        {
            base.Remove(entity);
            _context.SaveChanges();
        }

        public Guid GetPerformanceYear()
        {
            var yearId = Guid.Empty;
            var year= _appContext.PerformanceYear.Where(a => a.IsCompleted == false && a.IsYearActive == true).FirstOrDefault();
            if (year!=null)
            {
                yearId = year.Id;
            }
            return yearId;
        }
    }
}
