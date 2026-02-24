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
    public class TimesheetTemplateRepository : Repository<TimesheetTemplate>, ITimesheetTemplateRepository
    {
        public TimesheetTemplateRepository(DbContext context) : base(context)
        {
        }
        private ApplicationDbContext _appContext => (ApplicationDbContext)_context;

        public PagedList<TimesheetTemplate> GetAllTemplate(string name = "", int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var listTemplate = _appContext.TimesheetTemplate.AsQueryable().Where(c => c.IsActive);
            if (!string.IsNullOrWhiteSpace(name) && name != "undefined")
                listTemplate = listTemplate.Where(c => c.TempalteName.Contains(name));
            listTemplate = listTemplate.OrderBy(c => c.TempalteName).ThenBy(c => c.Id);
            return new PagedList<TimesheetTemplate>(listTemplate, pageIndex, pageSize);
        }

        public TimesheetConfiguration GetTimeSheetConfiguration(Guid configurationId)
        {
            var configuration = _appContext.TimesheetConfiguration.Where(m => m.Id==configurationId && m.IsActive == true).FirstOrDefault();
            return configuration;
        }

        public PagedList<Employee> GetEmployeeList(string name = "", int pageIndex = 0, int pageSize = int.MaxValue)
        {
   
            var query=from employee in _appContext.Employee.Where(m=>m.IsActive == true)                          
                      select new Employee {  Id = employee.Id, ApplicationUser = new ApplicationUser { FullName = employee.ApplicationUser.FullName } ,FunctionalDesignation = new FunctionalDesignation { Name = employee.FunctionalDesignation.Name } };
            var employeeList = query.AsQueryable();
            if (!string.IsNullOrWhiteSpace(name) && name != "undefined")
                employeeList = employeeList.Where(c => c.ApplicationUser.FullName.Contains(name));
            employeeList = employeeList.OrderBy(c => c.ApplicationUser.FullName).ThenBy(c => c.Id);
            return new PagedList<Employee>(employeeList,pageIndex,pageSize);
        }

        public PagedList<TimesheetUserSchedule> GetEmployeeByTemplateId(Guid templateId, string name = "", int pageIndex = 0, int pageSize = int.MaxValue)
        {

            var query = from employee in _appContext.Employee.Where(m => m.IsActive == true)
                        join userSchedule in _appContext.TimesheetUserSchedule.Where(m => m.TimesheetTemplateId == templateId && m.IsActive == true) on employee.Id equals userSchedule.EmployeeId into us
                        from p in us.DefaultIfEmpty()
                        select new TimesheetUserSchedule
                        {

                            TimesheetTemplateId = p.TimesheetTemplateId == null ? Guid.Parse("00000000-0000-0000-0000-000000000000") : p.TimesheetTemplateId,
                            Employee = new Employee
                            {
                                Id = employee.Id,
                                ApplicationUser = new ApplicationUser { FullName = employee.ApplicationUser.FullName },
                                FunctionalDesignation = new FunctionalDesignation { Name = employee.FunctionalDesignation.Name }
                            }

                        };

            var employeeList = query.ToList();


            if (!string.IsNullOrWhiteSpace(name) && name != "undefined")
            {
                employeeList = query.Where(c => c.Employee.ApplicationUser.FullName.Contains(name)).ToList();
            }

            employeeList = employeeList.OrderBy(c => c.Employee.ApplicationUser.FullName).ThenBy(c => c.EmployeeId).ToList();
            return new PagedList<TimesheetUserSchedule>(employeeList, pageIndex, pageSize);

        }

        public String GetScheduleType(Guid templateId)
        {
            string name = "";
            var query = from Template in _appContext.TimesheetTemplate.Where(m => m.Id == templateId)
                        select Template.TimesheetConfiguration.Name;
            string query1 = _appContext.TimesheetTemplate.Where(m => m.Id == templateId).Select(m=>m.TimesheetConfiguration.Name).FirstOrDefault();
            name = query1.ToString();
            return name;
        }
    }
}
