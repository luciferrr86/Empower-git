using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using A4.DAL.Entites;
using Microsoft.EntityFrameworkCore;
using DAL;
using System.Linq;
using System.Linq.Expressions;

namespace A4.DAL.Repositories
{
    public class EmployeeRepository : Repository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(DbContext context) : base(context)
        {

        }

        private ApplicationDbContext _appContext => (ApplicationDbContext)_context;

       

      
        public void CreateEmployee(Employee employee, string userId)
        {
            employee.UserId = userId;

            var employeeData = _appContext.Employee.Where(m => m.UserId == userId).FirstOrDefault();
            if (employeeData != null)
            {
                _appContext.Employee.Add(employee);


                var personalData = _appContext.Personal.Where(m => m.EmployeeId == employee.Id).FirstOrDefault();
                if (personalData != null)
                {
                    var personal = new Personal();
                    personal.EmployeeId = employee.Id;
                    _appContext.Personal.Add(personal);
                }

                var professionalData = _appContext.Professional.Where(m => m.EmployeeId == employee.Id).FirstOrDefault();
                if (professionalData != null)
                {
                    var professional = new Professional();
                    professional.EmployeeId = employee.Id;
                    _appContext.Professional.Add(professional);
                }

                var qualificationData = _appContext.Qualification.Where(m => m.EmployeeId == employee.Id).FirstOrDefault();
                if (qualificationData != null)
                {

                    var qualification = new Qualification();
                    qualification.EmployeeId = employee.Id;
                    _appContext.Qualification.Add(qualification);
                }

            }
            _appContext.SaveChanges();
        }

        //get record of single employee
        public Employee GetEmployee(string userId)
        {

            var employee = _appContext.Employee.Where(m => m.UserId == userId).FirstOrDefault();
            return employee;
        }

        public Employee GetEmployeeDetail(string userId)
        {
            var query = from employee in _appContext.Employee.Where(m => m.UserId == userId && m.IsActive == true)
                        join user in _appContext.Users on employee.UserId equals user.Id
                        select new Employee { ApplicationUser = new ApplicationUser { FullName = user.FullName, Email = user.Email } };
            return query.FirstOrDefault();
        }

        public PagedList<PerformanceEmpGoal> GetAllEmployee(Guid yearId, string name = "", int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var query = from empgoal in _appContext.PerformanceEmpGoal
                               join emp in _appContext.Employee on empgoal.EmployeeId equals emp.Id
                               join appUser in _appContext.Users on emp.UserId equals appUser.Id
                               join status in _appContext.PerformanceStatus on empgoal.PerformanceStatusId equals status.Id
                               join designation in _appContext.FunctionalDesignation on emp.DesignationId equals designation.Id
                               join funcGroup in _appContext.FunctionalGroup on emp.GroupId equals funcGroup.Id
                               where emp.IsActive == true & empgoal.PerformanceYearId == yearId
                               select new PerformanceEmpGoal { PerformanceStatus = new PerformanceStatus { StatusText = status.StatusText, ColorCode = status.ColorCode }, Employee = new Employee { Id = emp.Id, ApplicationUser = new ApplicationUser { Id = appUser.Id, FullName = appUser.FullName, Email = appUser.Email }, FunctionalGroup = new FunctionalGroup { Name = funcGroup.Name }, FunctionalDesignation = new FunctionalDesignation { Name = designation.Name } } };
            var listEmployee = query.ToList();
            if (listEmployee.Count > 0)
            {
                if (!string.IsNullOrWhiteSpace(name) && name != "undefined")
                    listEmployee = query.Where(c => c.Employee.ApplicationUser.FullName.Contains(name)).ToList();
                listEmployee = listEmployee.OrderBy(c => c.Employee.ApplicationUser.FullName).ThenBy(c => c.Id).ToList();
            }
            return new PagedList<PerformanceEmpGoal>(listEmployee, pageIndex, pageSize);
        }

        public IQueryable<Employee> GetAllEmployee()
        {
           
            Expression<Func<Employee, bool>> empFilter = e => e.IsActive ;
            var emplist = _appContext.Employee.Where(empFilter);
            return emplist;
        }

        public PagedList<PerformanceGoalMain> GetAllManager(Guid yearId, string name = "", int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var query=
            //(from emp1 in _appContext.Employee
            //                   join emp in _appContext.Employee on emp1.Id equals emp.ManagerId
            //                   join user in _appContext.Users on emp.UserId equals user.Id
            //                   where emp1.IsActive == true
            //select new PerformanceGoalMain { }).Distinct();
            from goalmain in _appContext.PerformanceGoalMain
            join emp in _appContext.Employee on goalmain.ManagerId equals emp.Id
            join appUser in _appContext.Users on emp.UserId equals appUser.Id
            join status in _appContext.PerformanceStatus on goalmain.PerformanceStatusId equals status.Id
            join designation in _appContext.FunctionalDesignation on emp.DesignationId equals designation.Id
            join funcGroup in _appContext.FunctionalGroup on emp.GroupId equals funcGroup.Id
            where emp.IsActive == true & goalmain.PerformanceYearId == yearId
            select new PerformanceGoalMain { Employee = new Employee { Id = emp.Id, ApplicationUser = new ApplicationUser { Id = appUser.Id, FullName = appUser.FullName, Email = appUser.Email }, FunctionalGroup = new FunctionalGroup { Name = funcGroup.Name }, FunctionalDesignation = new FunctionalDesignation { Name = designation.Name } }, IsManagerReleased = goalmain.IsManagerReleased, PerformanceStatus = new PerformanceStatus { StatusText = status.StatusText, ColorCode = status.ColorCode } };
            var listManager = query.ToList();
            if (!string.IsNullOrWhiteSpace(name) && name != "undefined")
                listManager = query.Where(c => c.Employee.ApplicationUser.FullName.Contains(name)).ToList();
            listManager = listManager.OrderBy(c => c.Employee.ApplicationUser.FullName).ThenBy(c => c.Id).ToList();
            return new PagedList<PerformanceGoalMain>(listManager, pageIndex, pageSize);

        }

        public List<ApplicationUser> GetManagerList()
        {
            var managerList = new List<ApplicationUser>();
            var employeeList = from appuser in _appContext.Users
                               join employee in _appContext.Employee on appuser.Id equals employee.UserId
                               where appuser.IsActive == true
                               select new ApplicationUser { Id = employee.Id.ToString(), FullName = appuser.FullName };

            return employeeList.ToList();
        }

        // get EmployeeByBandId
        public Employee GetEmployeeByBandId(Guid EmployeeId)
        {
            var employee = _appContext.Employee.Where(m => m.Id == EmployeeId).FirstOrDefault();
            return employee;
        }

        //get EmployeeName and manger Name 
        public Employee GetEmployeedetails(Guid EmployeeId)
        {
            var query = from e1 in _appContext.Employee
                        join e2 in _appContext.Employee on e1.ManagerId equals e2.Id
                        join appuser1 in _appContext.Users on e1.UserId equals appuser1.Id
                        join appuser2 in _appContext.Users on e2.UserId equals appuser2.Id
                        where e1.Id == EmployeeId && e1.IsActive == true
                        select new Employee { Id = e1.Id, ApplicationUser = new ApplicationUser { FullName = appuser1.FullName, Email = appuser1.Email }, ManagerId = e2.Id, ManagerName = appuser2.FullName, ManagerEmail = appuser2.Email, FunctionalDesignation = new FunctionalDesignation { Name = e1.FunctionalDesignation.Name } };
            return query.FirstOrDefault();
        }

        //get EmployeeName 
        public Employee GetEmployeedetailsByemployeeId(Guid employeeId)
        {
            var query = from emp in _appContext.Employee
                        where emp.Id == employeeId && emp.IsActive == true
                        join appuser in _appContext.Users on emp.UserId equals appuser.Id
                        select new Employee { Id = emp.Id, ApplicationUser = new ApplicationUser { FullName = appuser.FullName, Email = appuser.Email } };
            return query.FirstOrDefault();
        }

        //get EmployeeId
        public Guid GetEmployeeId(string userId)
        {
            Guid employeeId = _appContext.Employee.Where(c => c.UserId == userId && c.IsActive == true).Select(m => m.Id).FirstOrDefault();
            return employeeId;
        }

        // Get employee list by mangerId
        public PagedList<Employee> GetEmployeeList(Guid managerId, string name = "", int pageIndex = 0, int pageSize = int.MaxValue)
        {

            var query = from employee in _appContext.Employee.Where(m => m.ManagerId == managerId && m.IsActive == true && m.Id != managerId)
                        join
mgr in _appContext.Employee.Where(m => m.Id == managerId && m.IsActive == true) on employee.ManagerId equals mgr.Id
                        select new Employee
                        {
                            Id = employee.Id,
                            ApplicationUser = new ApplicationUser { FullName = employee.ApplicationUser.FullName },
                            GroupId = employee.GroupId,
                            FunctionalGroup = new FunctionalGroup { Name = employee.FunctionalGroup.Name, DepartmentId = employee.FunctionalGroup.DepartmentId, FunctionalDepartment = new FunctionalDepartment { Name = employee.FunctionalGroup.FunctionalDepartment.Name } }
                                              ,
                            FunctionalDesignation = new FunctionalDesignation { Name = employee.FunctionalDesignation.Name },
                            FunctionalTitle = new FunctionalTitle { Name = employee.FunctionalTitle.Name },
                            ManagerName = mgr.ApplicationUser.FullName,
                            ManagerEmail = mgr.ApplicationUser.Email
                        };
            if (!string.IsNullOrWhiteSpace(name) && name != "undefined")
                query = query.Where(c => c.ApplicationUser.FullName.Contains(name));
            query = query.OrderBy(c => c.ApplicationUser.FullName).ThenBy(c => c.Id);
            return new PagedList<Employee>(query, pageIndex, pageSize);
        }

        public List<KeyValuePair<string, string>> SearchEmployee(string term)
        {
            var result = new List<KeyValuePair<string, string>>();
            var query = from emp in _appContext.Employee
                        where emp.IsActive == true
                        join appuser in _appContext.Users on emp.UserId equals appuser.Id
                        select new Employee
                        {
                            Id = emp.Id,
                            ApplicationUser = new ApplicationUser { FullName = appuser.FullName }

                        };
            if (query.Count() > 0)
            {
                foreach (var item in query)
                {
                    result.Add(new KeyValuePair<string, string>(item.Id.ToString(), item.ApplicationUser.FullName));
                }
            }

            var listEmployee = result.Where(s => s.Value.ToLower().StartsWith(term.ToLower())).Select(w => w).ToList();
            return result;
        }





    }
}
