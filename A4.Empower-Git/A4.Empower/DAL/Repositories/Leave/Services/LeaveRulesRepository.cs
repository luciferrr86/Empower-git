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
   public  class LeaveRulesRepository : Repository<LeaveRules>, ILeaveRulesRepository
    {
        public LeaveRulesRepository(DbContext context) : base(context)
        {

        }

        private ApplicationDbContext _appContext => (ApplicationDbContext)_context;

        public override LeaveRules Get(Guid id)
        {
            var leaveRules = _appContext.LeaveRules.Where(e => e.Id == id && e.IsActive == true).FirstOrDefault();
            return leaveRules;
        }

        public PagedList<LeaveRules> GetAllLeaveRules(Guid periodId, string name = "", int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var listLeaveRules = _appContext.LeaveRules.AsQueryable().Where(c => c.IsActive && c.LeavePeriodId == periodId);
            if (!string.IsNullOrWhiteSpace(name) && name != "undefined")
                listLeaveRules = listLeaveRules.Where(c => c.Name.Contains(name));
            listLeaveRules = listLeaveRules.OrderBy(c => c.Name).ThenBy(c => c.Id);
            return new PagedList<LeaveRules>(listLeaveRules, pageIndex, pageSize);
        }

        public bool CheckLeaveRules(Guid bandId, Guid LeaveTypeId, Guid LeavePeriodId)
        {
            bool flag = false;
            var Check = _appContext.LeaveRules.Where(C => C.BandId == bandId && C.LeaveTypeId == LeaveTypeId && C.LeavePeriodId == LeavePeriodId).FirstOrDefault();
            if (Check != null)
            {
                flag = true;
            }
            return flag;
        }

        public List<LeaveRules> GetLeaveRuleByBandId(Guid bandId , Guid leavePeriodId)
        {
            var leaveRules = _appContext.LeaveRules.Where(e => e.BandId == bandId && e.LeavePeriodId == leavePeriodId  && e.IsActive == true).ToList();
            return leaveRules;
        }
        public bool UpdateLeaveRules(Guid periodId)
        {
            bool flag = false;
            var leaveRules = _appContext.LeaveRules.Where(e => e.IsActive == true).ToList();
            if (leaveRules != null)
            {
                if (periodId != null)
                {
                    leaveRules.ForEach(s => s.IsActive = false);
                    base.UpdateRange(leaveRules);
                    _context.SaveChanges();
                    flag = true;
                }
                else
                {
                    flag = false;
                }
            }
            return flag;
        }
    }
}
