using A4.BAL;
using A4.DAL.Entites;
using DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace A4.DAL.Repositories
{
   public interface ILeaveManagementRepository : IRepository<EmployeeLeaves>
    {
        List<LeaveInfoModel> GetAllLeaveForMonth(Guid employeeId, int month, int year);
        List<LeaveInfoModel> GetAllLeaveEntitlement(Guid EmployeeId, Guid periodId);

        EmployeeLeavesEntitlement GetLeaveEntitlement(Guid EmployeeId, Guid periodId, Guid leaveTypeId);

        void RetractEmployeeLeave(Guid leaveDetailsId);
        LeaveHolidayList GetHolidayByDate(DateTime date);
        List<LeaveHolidayList> GetHolidayList();
        int ExcludeHolidays(DateTime startDate, DateTime endDate);

        int ExcludeWeekends(DateTime startDate, DateTime endDate);

        bool CheckDaterange(DateTime startdate, DateTime enddate, string loginId);

        int GetCalculateNoOfDays(DateTime StartDate, DateTime EndDate);

        int GetLeaveNoOfDays(DateTime startDate, DateTime endDate, int noOfDays);

    }
}
