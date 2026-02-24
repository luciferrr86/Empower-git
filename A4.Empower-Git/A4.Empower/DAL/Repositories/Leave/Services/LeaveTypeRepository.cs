using A4.DAL.Entites;
using DAL;
using DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A4.DAL.Repositories
{
   public  class LeaveTypeRepository : Repository<LeaveType>, ILeaveTypeRepository
    {
        public LeaveTypeRepository(DbContext context) : base(context)
        {

        }

        private ApplicationDbContext _appContext => (ApplicationDbContext)_context;

        public override LeaveType Get(Guid id)
        {
            var leaveType = _appContext.LeaveType.Where(e => e.Id == id && e.IsActive == true).FirstOrDefault();
            return leaveType;
        }
        
        public PagedList<LeaveType> GetAllLeaveType(Guid periodId, string name = "", int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var listLeaveType = _appContext.LeaveType.AsQueryable().Where(c => c.IsActive && c.LeavePeriodId == periodId);
            if (!string.IsNullOrWhiteSpace(name) && name != "undefined")
                listLeaveType = listLeaveType.Where(c => c.Name.Contains(name));
            listLeaveType = listLeaveType.OrderBy(c => c.Name).ThenBy(c => c.Id);
            return new PagedList<LeaveType>(listLeaveType, pageIndex, pageSize);

        }
        public bool UpdateLeaveType(Guid periodId)
        {
            bool flag = false;
            var leaveType = _appContext.LeaveType.Where(e => e.IsActive == true).ToList();
            if (leaveType != null)
            {
                leaveType.ForEach(s => s.IsActive = false);
                base.UpdateRange(leaveType);
                _context.SaveChanges();
                flag = true;
            }
            return flag;
        }

    }
}
