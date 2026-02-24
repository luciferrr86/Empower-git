using A4.DAL.Entites;
using DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace A4.DAL.Repositories
{
   public  interface ILeaveRulesRepository : IRepository<LeaveRules>
    {
        PagedList<LeaveRules> GetAllLeaveRules(Guid periodId, string name = "", int pageIndex = 0, int pageSize = int.MaxValue);
        bool CheckLeaveRules(Guid bandId, Guid LeaveTypeId, Guid LeavePeriodId);
        List<LeaveRules> GetLeaveRuleByBandId(Guid bandId, Guid leavePeriodId);
        bool UpdateLeaveRules(Guid periodId);
    }
}
