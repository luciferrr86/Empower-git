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
   public  class SubordinateLeaveRepository : Repository<EmployeeLeaveDetail>,ISubordinateLeaveRepository
    {
        public SubordinateLeaveRepository(DbContext context) : base(context)
        {

        }

        private ApplicationDbContext _appContext => (ApplicationDbContext)_context;

        public Tuple<int, int, int, int> CheckLeaves(Guid entitlementId)
        {
      
            int allleaves = 0;
            int allreamingleaves = 0;
            int allrejectedleaves = 0;
            int allapproved = 0;
            var query = (from entitlement in _appContext.EmployeeLeavesEntitlement.Where(m => m.Id == entitlementId && m.IsActive == true)
                         select new EmployeeLeavesEntitlement
                         {
                             LeaveRules = new LeaveRules { LeavesPerYear = entitlement.LeaveRules.LeavesPerYear },
                             Pending = entitlement.Pending,
                             Rejected = entitlement.Rejected,
                             Approved = entitlement.Approved
                            
                         }).FirstOrDefault();
            if (query != null)
            {
                allleaves = allleaves + Convert.ToInt32(query.LeaveRules.LeavesPerYear);
                allrejectedleaves = allrejectedleaves + Convert.ToInt32(query.Rejected);
                allapproved = allapproved + Convert.ToInt32(query.Approved);
                allreamingleaves = allleaves - allapproved;
            }
            return Tuple.Create(allleaves, allapproved, allrejectedleaves, allreamingleaves);
         
        }
    }
}
