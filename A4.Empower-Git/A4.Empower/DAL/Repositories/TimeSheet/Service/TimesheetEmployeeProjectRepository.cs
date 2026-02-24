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
    public class TimesheetEmployeeProjectRepository : Repository<TimesheetEmployeeProject>, ITimesheetEmployeeProjectRepository
    {
        public TimesheetEmployeeProjectRepository(DbContext context) : base(context)
        {
        }
        private ApplicationDbContext _appContext => (ApplicationDbContext)_context;


        public PagedList<TimesheetEmployeeProject> GetEmployeeList(Guid projectId, string name = "", int pageIndex = 0, int pageSize = int.MaxValue)
        {


            var query = from employee in _appContext.Employee.Where(m => m.IsActive == true)
                        join employeeProject in _appContext.TimesheetEmployeeProject.Where(m => m.TimeSheetProjectId == projectId && m.IsActive == true) 
                        on employee.Id equals employeeProject.EmployeeId into ep
                        from p in ep.DefaultIfEmpty()
                        select new TimesheetEmployeeProject
                        {

                            TimeSheetProjectId = p.TimeSheetProjectId == null ? Guid.Parse("00000000-0000-0000-0000-000000000000") : p.TimeSheetProjectId,
                            Employee = new Employee
                            {
                                Id = employee.Id,
                                ApplicationUser = new ApplicationUser { FullName = employee.ApplicationUser.FullName },
                                FunctionalDesignation = new FunctionalDesignation { Name = employee.FunctionalDesignation.Name }
                            }

                        };

            var employeeList = query.ToList();
            if (!string.IsNullOrWhiteSpace(name) && name != "undefined")
                employeeList = query.Where(c => c.Employee.ApplicationUser.FullName.Contains(name)).ToList();
            employeeList = employeeList.OrderBy(c => c.Employee.ApplicationUser.FullName).ThenBy(c => c.EmployeeId).ToList(); ;
            return new PagedList<TimesheetEmployeeProject>(employeeList, pageIndex, pageSize);

        }

        public IEnumerable<TimesheetEmployeeProject> GetProjectByEmployeeId(Guid employeeId)
        {
            var query = _appContext.TimesheetEmployeeProject.Where(m => m.EmployeeId == employeeId && m.IsActive == true);
            return query;
        }


    }
}
