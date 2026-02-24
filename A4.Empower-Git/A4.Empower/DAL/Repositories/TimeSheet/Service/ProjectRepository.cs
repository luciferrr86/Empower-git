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
    public class ProjectRepository : Repository<TimesheetProject>, IProjectRepository
    {
        public ProjectRepository(DbContext context) : base(context)
        {
        }
        private ApplicationDbContext _appContext => (ApplicationDbContext)_context;

        public PagedList<TimesheetProject> GetAllProject(string name = "", int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var listProject = _appContext.TimesheetProject.AsQueryable().Where(c => c.IsActive);
            if (!string.IsNullOrWhiteSpace(name) && name != "undefined")
                listProject = listProject.Where(c => c.ProjectName.Contains(name));
            listProject = listProject.OrderBy(c => c.ProjectName).ThenBy(c => c.Id);
            return new PagedList<TimesheetProject>(listProject, pageIndex, pageSize);
        }

        public List<ApplicationUser> GetManagerList()
        {
    
            var managerList = (from appuser in _appContext.Users
                               join employee in _appContext.Employee on appuser.Id equals employee.UserId
                               where appuser.IsActive == true
                               select new ApplicationUser { Id = employee.Id.ToString(), FullName = appuser.FullName }).ToList();

            return managerList;
        }

        public List<TimesheetClient> GetClientList()
        {
            var clientList = _appContext.TimesheetClient.Where(m => m.IsActive == true).ToList();       
            return clientList;
        }

    }
}
