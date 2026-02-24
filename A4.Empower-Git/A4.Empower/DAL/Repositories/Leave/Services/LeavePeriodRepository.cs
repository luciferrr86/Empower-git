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
   public  class LeavePeriodRepository : Repository<LeavePeriod>, ILeavePeriodRepository
    {
        public LeavePeriodRepository(DbContext context) : base(context)
        {

        }

        private ApplicationDbContext _appContext => (ApplicationDbContext)_context;

        public override LeavePeriod Get(Guid id)
        {
            var leavePeriod = _appContext.LeavePeriod.Where(e => e.Id == id && e.IsActive == true).FirstOrDefault();
            return leavePeriod;
        }

        public PagedList<LeavePeriod> GetAllLeavePeriod(string name = "", int pageIndex = 0, int pageSize = int.MaxValue)
        {
         
            var listLeavePeriod = _appContext.LeavePeriod.AsQueryable().Where(c => c.IsActive);
            if (!string.IsNullOrWhiteSpace(name) && name != "undefined")
                listLeavePeriod = listLeavePeriod.Where(c => c.Name.Contains(name));
            listLeavePeriod = listLeavePeriod.OrderBy(c => c.Name).ThenBy(c => c.Id);
            return new PagedList<LeavePeriod>(listLeavePeriod, pageIndex, pageSize);
       
        }

        public Guid GetLeavePeriodId()
        {
            Guid PeriodId = new Guid();
            Guid leavePeriod = _appContext.LeavePeriod.Where(e => e.IsActive == true).Select(m=>m.Id).FirstOrDefault();
            if (leavePeriod != Guid.Empty)
            {
                PeriodId = leavePeriod;
            }
            return PeriodId;
        }

        public LeavePeriod GetleavePeriodRecord()
        {
            var model = new LeavePeriod();
            var leavePeriod = _appContext.LeavePeriod.Where(e => e.IsActive == true && e.IsLeavePeriodCompleted == false).FirstOrDefault();
            if (leavePeriod != null)
            {
               model.Id = leavePeriod.Id;
               model.Name = leavePeriod.Name;
               model.PeriodStart =leavePeriod.PeriodStart ;
               model.PeriodEnd = leavePeriod.PeriodEnd;
               model.IsLeavePeriodCompleted = leavePeriod.IsLeavePeriodCompleted;
               model.IsActive = leavePeriod.IsActive;
            }
            return model;
        }

        public bool CheckLeavePeriod()
        {
            bool flag = false;
            var leavePeriod = _appContext.LeavePeriod.Where(e => e.IsActive == true && e.IsLeavePeriodCompleted ==false ).FirstOrDefault();
            if (leavePeriod != null)
            {
                if (leavePeriod.PeriodEnd.Date < DateTime.Now.Date)
                {
                    leavePeriod.IsActive = false;
                    leavePeriod.IsLeavePeriodCompleted = true;
                    base.Update(leavePeriod);
                    _context.SaveChanges();
                    flag = false;
                }
                else
                {
                    flag = true;
                }
            }
            return flag;
        }
    }
}
