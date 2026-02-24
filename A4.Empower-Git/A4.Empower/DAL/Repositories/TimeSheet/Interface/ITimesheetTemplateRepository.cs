using A4.DAL.Entites;
using DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace A4.DAL.Repositories
{
   public interface ITimesheetTemplateRepository :IRepository<TimesheetTemplate>
    {
        PagedList<TimesheetTemplate> GetAllTemplate(string name = "", int pageIndex = 0, int pageSize = int.MaxValue);
        TimesheetConfiguration GetTimeSheetConfiguration(Guid configurationId);
        PagedList<Employee> GetEmployeeList(string name = "", int pageIndex = 0, int pageSize = int.MaxValue);   
        PagedList<TimesheetUserSchedule> GetEmployeeByTemplateId(Guid templateId, string name = "", int pageIndex = 0, int pageSize = int.MaxValue);
        String GetScheduleType(Guid templateId);
    }
}
