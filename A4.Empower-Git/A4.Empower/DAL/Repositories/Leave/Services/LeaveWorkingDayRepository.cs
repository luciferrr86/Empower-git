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
   public  class LeaveWorkingDayRepository : Repository<LeaveWorkingDay>, ILeaveWorkingDayRepository
    {
        public LeaveWorkingDayRepository(DbContext context) : base(context)
        {

        }

        private ApplicationDbContext _appContext => (ApplicationDbContext)_context;

        public override LeaveWorkingDay Get(Guid id)
        {
            var leaveWorkingDay = _appContext.LeaveWorkingDay.Where(e => e.Id == id && e.IsActive == true).FirstOrDefault();
            return leaveWorkingDay;
        }

        public void Save(Guid periodId)
        {
            var list = new List<LeaveWorkingDay>();
            list.Add(new LeaveWorkingDay { WorkingDay = "Sunday", IsActive = true, WorkingDayValue = "0", LeavePeriodId = periodId,LocalOrder="1" });
            list.Add(new LeaveWorkingDay { WorkingDay = "Monday", IsActive = true, WorkingDayValue = "1", LeavePeriodId = periodId, LocalOrder = "2" });
            list.Add(new LeaveWorkingDay { WorkingDay = "Tuesday", IsActive = true, WorkingDayValue = "1", LeavePeriodId = periodId, LocalOrder = "3" });
            list.Add(new LeaveWorkingDay { WorkingDay = "Wednesday", IsActive = true, WorkingDayValue = "1", LeavePeriodId = periodId, LocalOrder = "4" });
            list.Add(new LeaveWorkingDay { WorkingDay = "Thursday", IsActive = true, WorkingDayValue = "1", LeavePeriodId = periodId, LocalOrder = "5" });
            list.Add(new LeaveWorkingDay { WorkingDay = "Friday", IsActive = true, WorkingDayValue = "1", LeavePeriodId = periodId, LocalOrder = "6" });
            list.Add(new LeaveWorkingDay { WorkingDay = "Saturday", IsActive = true, WorkingDayValue = "0", LeavePeriodId = periodId, LocalOrder = "7" });
            base.AddRange(list);
            _context.SaveChanges();
        }
        public bool CheckLeaveWorkingDay()
        {
            bool flag = false;
            var leaveWorkingDay = _appContext.LeaveWorkingDay.Where(e => e.IsActive == true).ToList();
            if (leaveWorkingDay != null)
            {
                leaveWorkingDay.ForEach(s => s.IsActive = false);
                base.UpdateRange(leaveWorkingDay);
                _context.SaveChanges();
                flag = true;
                //if (leavePeriod.PeriodEnd.Date < DateTime.Now.Date)
                //{
                //    leavePeriod.IsActive = false;
                //    leavePeriod.IsLeavePeriodCompleted = true;
                //    base.Update(leavePeriod);
                //    _context.SaveChanges();
                //    flag = false;
                //}
                //else
                //{
                //    flag = true;
                //}
            }
            return flag;
        }
        public List<LeaveWorkingDay> GetAllWorkingDay()
        {
            var listWorkingDay = new List<LeaveWorkingDay>();
            listWorkingDay = _appContext.LeaveWorkingDay.AsQueryable().Where(c => c.IsActive).OrderBy(c=>Convert.ToInt32(c.LocalOrder)).ToList();          
            return listWorkingDay;

        }

    }
}
