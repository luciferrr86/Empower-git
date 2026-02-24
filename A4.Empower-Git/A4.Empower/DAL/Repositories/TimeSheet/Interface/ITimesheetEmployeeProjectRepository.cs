using A4.DAL.Entites;
using DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace A4.DAL.Repositories
{
   public interface ITimesheetEmployeeProjectRepository :IRepository<TimesheetEmployeeProject>
    {
   
        PagedList<TimesheetEmployeeProject> GetEmployeeList(Guid projectId, string name = "", int pageIndex = 0, int pageSize = int.MaxValue);

        IEnumerable<TimesheetEmployeeProject> GetProjectByEmployeeId(Guid employeeId);

       
    }
}
