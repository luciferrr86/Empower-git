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
    public class TimesheetUserScheduleRepository : Repository<TimesheetUserSchedule>, ITimesheetUserScheduleRepository
    {
        public TimesheetUserScheduleRepository(DbContext context) : base(context)
        {
        }
        private ApplicationDbContext _appContext => (ApplicationDbContext)_context;

        public List<TimesheetUserSchedule> ScheduleList(Guid templateId)
        {
            var list = _appContext.TimesheetUserSchedule.Where(m => m.TimesheetTemplateId == templateId).ToList();
            return list;
        }

        public PagedList<TimesheetUserSchedule> GetScheduleList(string name = "", int pageIndex = 0, int pageSize = int.MaxValue)
        {
      
            var query = from employee in _appContext.Employee.Where(m => m.IsActive == true)
            join userSchedule in _appContext.TimesheetUserSchedule.Where(m=> m.IsActive == true) on employee.Id equals userSchedule.EmployeeId into us
            from p in us.DefaultIfEmpty()
            select new TimesheetUserSchedule
            {

                TimesheetTemplateId = p.TimesheetTemplateId == null ? Guid.Parse("00000000-0000-0000-0000-000000000000") : p.TimesheetTemplateId,
                Employee = new Employee
                {
                    ApplicationUser = new ApplicationUser { FullName = employee.ApplicationUser.FullName },                  
                }

            };

            var query1 = from employee in query                      
                        join template in _appContext.TimesheetTemplate on employee.TimesheetTemplateId equals template.Id into tp
                        from temp in tp.DefaultIfEmpty()                   
                        select new TimesheetUserSchedule
                        {
                            Employee = new Employee
                            {
                                ApplicationUser = new ApplicationUser { FullName = employee.Employee.ApplicationUser.FullName }
                            },
                        
                            TimesheetTemplate = temp.Id == null  ?  new TimesheetTemplate {  Sunday = false, Monday = false, Tuesday = false, Wednesday = false, Thursday = false ,Friday = false,Saturday = false 
                            } : new TimesheetTemplate
                                             {
                                                 Sunday = temp.Sunday,
                                                 Monday = temp.Monday,
                                                 Tuesday = temp.Tuesday,
                                                 Wednesday = temp.Wednesday,
                                                 Thursday = temp.Thursday,
                                                 Friday = temp.Friday,
                                                 Saturday = temp.Saturday,
                                TimesheetConfiguration = new TimesheetConfiguration { Name = temp .TimesheetConfiguration.Name}

                            }


                        };

       
            var employeeList = query1.AsQueryable();
            if (!string.IsNullOrWhiteSpace(name) && name != "undefined")
                employeeList = employeeList.Where(c => c.Employee.ApplicationUser.FullName.Contains(name));
            employeeList = employeeList.OrderBy(c => c.Employee.ApplicationUser.FullName).ThenBy(c => c.Id);
            return new PagedList<TimesheetUserSchedule>(employeeList, pageIndex, pageSize);
        }

       
    }
}
