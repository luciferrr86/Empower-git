using A4.DAL.Entites;
using DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace A4.DAL.Repositories
{
    public interface IEmployeeRepository : IRepository<Employee>
    {
        void CreateEmployee(Employee employee, string userId);

        Employee GetEmployee(string userId);

        PagedList<PerformanceEmpGoal> GetAllEmployee(Guid yearId, string name = "", int pageIndex = 0, int pageSize = int.MaxValue);

        PagedList<PerformanceGoalMain> GetAllManager(Guid yearId, string name = "", int pageIndex = 0, int pageSize = int.MaxValue);

        IQueryable<Employee> GetAllEmployee();

        List<ApplicationUser> GetManagerList();

        Employee GetEmployeeByBandId(Guid EmployeeId);

        Employee GetEmployeedetails(Guid EmployeeId);

        Guid GetEmployeeId(string userId);

        Employee GetEmployeeDetail(string userId);

        PagedList<Employee> GetEmployeeList(Guid managerId, string name = "", int pageIndex = 0, int pageSize = int.MaxValue);

        Employee GetEmployeedetailsByemployeeId(Guid employeeId);

        List<KeyValuePair<string, string>> SearchEmployee(string term);

    }
}
