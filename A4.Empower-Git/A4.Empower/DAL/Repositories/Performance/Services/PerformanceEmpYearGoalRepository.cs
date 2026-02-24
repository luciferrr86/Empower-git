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
   public class PerformanceEmpYearGoalRepository : Repository<PerformanceEmpYearGoal>, IPerformanceEmpYearGoalRepository
    {
        public PerformanceEmpYearGoalRepository(DbContext context) : base(context)
        {

        }
        private ApplicationDbContext _appContext => (ApplicationDbContext)_context;

        public Employee EmployeeDetails(Guid employeeId)
        {
            var query = from employee in _appContext.Employee.Where(m=>m.IsActive && m.Id == employeeId)join
                        mgr in _appContext.Employee.Where(m=>m.IsActive == true) on employee.ManagerId equals mgr.Id
                        select new Employee { Id = employee.Id, ApplicationUser = new ApplicationUser { FullName = employee.ApplicationUser.FullName }, GroupId = employee.GroupId, FunctionalGroup = new FunctionalGroup {Name=employee.FunctionalGroup.Name, DepartmentId = employee.FunctionalGroup.DepartmentId, FunctionalDepartment = new FunctionalDepartment { Name = employee.FunctionalGroup.FunctionalDepartment.Name } }
                                              ,FunctionalDesignation=new FunctionalDesignation {Name=employee.FunctionalDesignation.Name},FunctionalTitle= new FunctionalTitle {Name =employee.FunctionalTitle.Name },ManagerName=mgr.ApplicationUser.FullName };
            return query.FirstOrDefault();
        }
    }
}
