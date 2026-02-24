using A4.BAL;
using A4.DAL.Entites;
using DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace A4.DAL.Repositories
{
   public  interface IEmployeeLeaveDetailRepository : IRepository<EmployeeLeaveDetail>
    {
     
        PagedList<EmployeeLeaveDetail> GetAllEmployeeLeave(Guid employeeId, Guid periodId, string name = "", int pageIndex = 0, int pageSize = int.MaxValue);
        List<EmployeeLeaveDetail> GetListofEmployeeLeave(Guid employeeId, Guid periodId);
        EmployeeLeaveDetailModel GetEmployeeLeaveDetail(Guid leaveDetailId);
        PagedList<EmployeeLeaveDetail> GetSubOrdinateLeavesList(Guid managerId, Guid periodId, string name = "", int pageIndex = 0, int pageSize = int.MaxValue);
        SubordinateLeaveDetailModel GetSubOrdinateLeaveDetail(Guid leaveDetailId);
        List<EmployeeLeaveDetailModel> GetLeaveByEmpId(Guid id);
    }
}
