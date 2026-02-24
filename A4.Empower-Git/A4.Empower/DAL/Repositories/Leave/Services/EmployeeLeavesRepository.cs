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
   public  class EmployeeLeavesRepository : Repository<EmployeeLeaves>, IEmployeeLeavesRepository
    {
        public EmployeeLeavesRepository(DbContext context) : base(context)
        {

        }

        private ApplicationDbContext _appContext => (ApplicationDbContext)_context;

        public override EmployeeLeaves Get(Guid id)
        {
            var employeeLeaves = _appContext.EmployeeLeaves.Where(e => e.Id == id && e.IsActive == true).FirstOrDefault();
            return employeeLeaves;
        }

      
        public EmployeeLeaves GetEmployeeLeavesByEmpId(Guid employeeId ,Guid periodId)
        {
            var employeeLeaves = _appContext.EmployeeLeaves.Where(a => a.EmployeeId == employeeId && a.LeavePeriodId == periodId && a.IsActive == true).FirstOrDefault();
            return employeeLeaves;
        }
    }
}
