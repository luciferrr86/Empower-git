using A4.DAL.Entites;
using DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace A4.DAL.Repositories
{
   public interface IProjectRepository :IRepository<TimesheetProject>
    {
        PagedList<TimesheetProject> GetAllProject(string name = "", int pageIndex = 0, int pageSize = int.MaxValue);
        List<TimesheetClient> GetClientList();
        List<ApplicationUser> GetManagerList();
    }
}
