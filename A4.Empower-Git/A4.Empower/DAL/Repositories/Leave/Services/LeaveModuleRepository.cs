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
   public  class LeaveModuleRepository : Repository<LeavePeriod>, ILeaveModuleRepository
    {
        public LeaveModuleRepository(DbContext context) : base(context)
        {

        }

        private ApplicationDbContext _appContext => (ApplicationDbContext)_context;

        public bool EmployeeLeaveConfig(string id)
        {
         
            var emp = _appContext.Employee.Where(m => m.UserId == id && m.IsActive == true).FirstOrDefault();
            if (emp != null)
            {
                var IsLeavePeriodExist = _appContext.LeavePeriod.Where(m => m.IsActive == true && m.IsLeavePeriodCompleted == false).FirstOrDefault();
                if (IsLeavePeriodExist != null)
                {
                    var IsLeaveWorkingDayExist = _appContext.LeaveWorkingDay.Where(m=>m.IsActive && m.LeavePeriodId == IsLeavePeriodExist.Id).ToList();
                    if (IsLeaveWorkingDayExist.Count() > 0)
                    {
                        var IsLeaveType = _appContext.LeaveType.Where(m => m.IsActive && m.LeavePeriodId == IsLeavePeriodExist.Id).ToList();
                        if (IsLeaveType.Count > 0 )
                        {
                            foreach (var item in IsLeaveType)
                            {

                                var IsLeaveRulesExist = _appContext.LeaveRules.Where(m => m.BandId == emp.BandId && m.IsActive == true && m.LeaveTypeId == item.Id && m.LeavePeriodId == item.LeavePeriodId).ToList();
                                if (IsLeaveRulesExist.Count() > 0)
                                {
                                    return true;
                                }

                            }
                        }
                    }
                }
            }
            return false;

        }

        public bool AllEmployeeConfig(Guid id)
        {
            int count = 0;
            var emp = _appContext.Employee.Where(m => m.ManagerId == id && m.IsActive == true).ToList();
            if (emp.Count > 0)
            {
                
                var query = (from period in _appContext.LeavePeriod.Where(m => m.IsActive == true)
                             join workingDay in _appContext.LeaveWorkingDay on period.Id equals workingDay.LeavePeriodId
                             join type in _appContext.LeaveType on period.Id equals type.LeavePeriodId                         
                             select new LeaveType
                             {
                                 LeavePeriodId = period.Id,
                                 Id = type.Id

                             }).ToList();
                foreach (var item in emp)
                {
                    foreach (var config in query)
                    {
                        var rule = _appContext.LeaveRules.Where(m => m.BandId == item.BandId && m.IsActive == true && m.LeaveTypeId == config.Id && m.LeavePeriodId == config.LeavePeriodId).ToList();
                        if (rule.Count() > 0)
                        {
                            count++;
                        }
                    }
                }
                if (count != 0)
                {
                    return true;
                }
            }
            return false;

        }
    }
}
