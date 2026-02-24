using A4.DAL.Entites;
using DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace A4.DAL.Repositories
{
   public  interface ILeaveTypeRepository : IRepository<LeaveType>
    {
        PagedList<LeaveType> GetAllLeaveType(Guid periodId, string name = "", int pageIndex = 0, int pageSize = int.MaxValue);
        bool UpdateLeaveType(Guid periodId);
    }
}
