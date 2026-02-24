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
   public class PerformanceConfigFeedbackRepository : Repository<PerformanceConfigFeedback>, IPerformanceConfigFeedbackRepository
    {
        public PerformanceConfigFeedbackRepository(DbContext context) : base(context)
        {

        }
        private ApplicationDbContext _appContext => (ApplicationDbContext)_context;
        public override PerformanceConfigFeedback Get(Guid id)
        {
            var performanceConfigFeedback = _appContext.PerformanceConfigFeedback.Where(e => e.Id == id && e.IsActive == true).FirstOrDefault();
            return performanceConfigFeedback;
        }
        public override void Add(PerformanceConfigFeedback entity)
        {
            base.Add(entity);
            _context.SaveChanges();
        }

        public override void Update(PerformanceConfigFeedback entity)
        {
            base.Update(entity);
            _context.SaveChanges();
        }

        public override void Remove(PerformanceConfigFeedback entity)
        {
            base.Remove(entity);
            _context.SaveChanges();
        }
    }
}
