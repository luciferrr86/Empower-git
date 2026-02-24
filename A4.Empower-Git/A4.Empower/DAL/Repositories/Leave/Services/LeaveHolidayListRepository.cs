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
   public  class LeaveHolidayListRepository : Repository<LeaveHolidayList>, ILeaveHolidayListRepository
    {
        public LeaveHolidayListRepository(DbContext context) : base(context)
        {

        }

        private ApplicationDbContext _appContext => (ApplicationDbContext)_context;

        public override LeaveHolidayList Get(Guid id)
        {
            var leaveHolidayList = _appContext.LeaveHolidayList.Where(e => e.Id == id && e.IsActive == true).FirstOrDefault();
            return leaveHolidayList;
        }

        public override void Add(LeaveHolidayList entity)
        {
            base.Add(entity);
            _context.SaveChanges();
        }

        public override void Update(LeaveHolidayList entity)
        {
            base.Update(entity);
            _context.SaveChanges();
        }

        public override void Remove(LeaveHolidayList entity)
        {
            base.Remove(entity);
            _context.SaveChanges();
        }

   
        public PagedList<LeaveHolidayList> GetAllHolidayList(Guid periodId, string name = "", int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var listHoliday = _appContext.LeaveHolidayList.AsQueryable().Where(c => c.IsActive && c.LeavePeriodId == periodId);
            if (!string.IsNullOrWhiteSpace(name) && name != "undefined")
                listHoliday = listHoliday.Where(c => c.Name.Contains(name));
            listHoliday = listHoliday.OrderBy(c => c.Name).ThenBy(c => c.Id);
            return new PagedList<LeaveHolidayList>(listHoliday, pageIndex, pageSize);

        }
     
        public bool CheckHolidayList()
        {
            bool flag = false;
            var holidayList = _appContext.LeaveHolidayList.Where(e => e.IsActive == true).ToList();
            if (holidayList != null)
            {
                holidayList.ForEach(s => s.IsActive = false);
                base.UpdateRange(holidayList);
                _context.SaveChanges();
                flag = true;
               
            }
            return flag;
        }
    }
}
