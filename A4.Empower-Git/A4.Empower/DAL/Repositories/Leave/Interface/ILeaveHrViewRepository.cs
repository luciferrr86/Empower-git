using A4.DAL.Entites;
using DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace A4.DAL.Repositories
{
   public interface ILeaveHrViewRepository : IRepository<EmployeeLeavesEntitlement>
    {
        PagedList<Employee> GetAllEmployees(string name = "", int pageIndex = 0, int pageSize = int.MaxValue);

        List<EmployeeLeavesEntitlement> EmployeeLeaveDetails(Guid employeeId,Guid periodId);

        List<EmployeeLeavesEntitlement> GetLeaveEntitlement(Guid EmployeeId, Guid periodId);

        EmployeeLeaves EmpLeaves(Guid employeeId);

        bool chkConfigSet();
    }
}
