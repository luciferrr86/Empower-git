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
   public  class EmployeeEntitlementRepository : Repository<EmployeeLeavesEntitlement>, IEmployeeEntitlementRepository
    {
        public EmployeeEntitlementRepository(DbContext context) : base(context)
        {

        }

        private ApplicationDbContext _appContext => (ApplicationDbContext)_context;

        public override EmployeeLeavesEntitlement Get(Guid id)
        {
            var entitlement = _appContext.EmployeeLeavesEntitlement.Where(e => e.Id == id && e.IsActive == true).FirstOrDefault();
            return entitlement;
        }

      
        public List<EmployeeLeavesEntitlement> GetEmployeeEntitlement(Guid EmployeeLeavesId)
        {
            var entitlement = _appContext.EmployeeLeavesEntitlement.Where(e => e.EmployeeLeavesId == EmployeeLeavesId && e.IsActive == true).ToList();
            return entitlement;
        }

    }
}
