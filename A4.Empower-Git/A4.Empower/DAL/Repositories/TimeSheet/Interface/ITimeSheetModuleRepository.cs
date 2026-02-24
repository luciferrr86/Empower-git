using A4.BAL;
using A4.DAL.Entites;
using DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;

namespace A4.DAL.Repositories
{
   public interface ITimeSheetModuleRepository : IRepository<TimesheetUserDetail>
    {
        Tuple<DateTime, DateTime> GetDateOfWeek();

        Tuple<TimesheetUserConfigModel, List<AllottedDaysModel>> GetTimesheetConfig(Guid userId, DateTime startingDate, DateTime endingDate);

        List<DateTime> GetDateList(DateTime startingDate, DateTime endingDate);

        List<TimesheetEmployeeProject> GetddlProject(Guid userId);

        Tuple<List<DayListModel>, List<ProjectListModel>> GetTimeSheetData(List<DateTime> dateLists, List<AllottedDaysModel> lstAllottedDays, Employee employee, bool frequency, bool IsManager, Guid timeSheetSpanId);       

        bool CheckTimesheetConfig(Guid employyeId);

        EmployeeTimeSheetModel GetEmployeeTimeSheet(Guid employeeId, Guid spanId);
        List<TimesheetUserDetail> GetEmployeeWorkingDays(Guid employeeId, int month, int year);

        List<DropDownList> GetTimesheetUserWeeks(Guid employeeId);

        EmployeeTimeSheetModel GetWeeklyTimeSheet(Guid employeeId, Guid spanId);

        Guid AddTimesheetUserDetails(string type, DayListModel item, Guid userSpanId, Guid ApproverlId);


    }
}
