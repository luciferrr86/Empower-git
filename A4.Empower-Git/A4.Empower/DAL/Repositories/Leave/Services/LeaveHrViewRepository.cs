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
    public class LeaveHrViewRepository : Repository<EmployeeLeavesEntitlement>, ILeaveHrViewRepository
    {
        public LeaveHrViewRepository(DbContext context) : base(context)
        {
        }

        private ApplicationDbContext _appContext => (ApplicationDbContext)_context;

        public PagedList<Employee> GetAllEmployees(string name = "", int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var query = from user in _appContext.Users
                        join employee in _appContext.Employee.Where(m => m.IsActive == true) on user.Id equals employee.UserId
                        join functionalGroup in _appContext.FunctionalGroup on employee.GroupId equals functionalGroup.Id
                        join department in _appContext.FunctionalDepartment on functionalGroup.DepartmentId equals department.Id
                        select new Employee { Id = employee.Id, ApplicationUser = new ApplicationUser { FullName = user.FullName }, GroupId = employee.GroupId, FunctionalGroup = new FunctionalGroup { DepartmentId = department.Id, FunctionalDepartment = new FunctionalDepartment { Name = department.Name } } };


            var employeeList = query.AsQueryable();
            if (!string.IsNullOrWhiteSpace(name) && name != "undefined")
                employeeList = employeeList.Where(c => c.ApplicationUser.FullName.Contains(name));
            employeeList = employeeList.OrderBy(c => c.ApplicationUser.FullName).ThenBy(c => c.Id);
            return new PagedList<Employee>(employeeList, pageIndex, pageSize);
           
        }

        public List<EmployeeLeavesEntitlement> GetLeaveEntitlement(Guid EmployeeId, Guid periodId)
        {
            var entitlement = new List<EmployeeLeavesEntitlement>();
            var query = (from empLeaves in _appContext.EmployeeLeaves.Where(c => c.EmployeeId == EmployeeId && c.IsActive == true && c.LeavePeriodId == periodId)
                         join leaveentitlement in _appContext.EmployeeLeavesEntitlement.Where(c => c.IsActive == true) on empLeaves.Id equals leaveentitlement.EmployeeLeavesId
                         join rules in _appContext.LeaveRules.Where(c => c.IsActive) on leaveentitlement.LeaveRulesId equals rules.Id
                         where leaveentitlement.IsActive == true
                         select new EmployeeLeavesEntitlement { Id = leaveentitlement.Id, LeaveRules = new LeaveRules { LeavesPerYear = rules.LeavesPerYear }, Approved = leaveentitlement.Approved, Pending = leaveentitlement.Pending, Rejected = leaveentitlement.Rejected }).ToList();
            entitlement = query;
            return entitlement;
        }

        public List<EmployeeLeavesEntitlement> EmployeeLeaveDetails(Guid employeeId, Guid periodId)
        {
            var leaveDetails = new List<EmployeeLeavesEntitlement>();
            var query = from employeeLeaves in _appContext.EmployeeLeaves.Where(m => m.EmployeeId == employeeId && m.LeavePeriodId==periodId)
                        join leavesEntitlement in _appContext.EmployeeLeavesEntitlement on employeeLeaves.Id equals leavesEntitlement.EmployeeLeavesId
                        join leaveRule in _appContext.LeaveRules on leavesEntitlement.LeaveRulesId equals leaveRule.Id
                        join leaveType in _appContext.LeaveType on leaveRule.LeaveTypeId equals leaveType.Id
                        select new EmployeeLeavesEntitlement { Approved = leavesEntitlement.Approved, Pending = leavesEntitlement.Pending, Rejected = leavesEntitlement.Rejected, LeaveRules = new LeaveRules { LeaveTypeId = leaveType.Id, LeavesPerYear = leaveRule.LeavesPerYear, LeaveType = new LeaveType { Name = leaveType.Name } } };
            leaveDetails = query.ToList();
            return leaveDetails;
        }
        //public List<EmployeeLeaveDetail> EmployeeLeaveDetailsByMonth(Guid employeeId)
        //{
        //    var empleaveDetails = new List<EmployeeLeaveDetail>();
        //    var query = from employeeLeaves in _appContext.EmployeeLeaveDetail.Where(m => m.EmployeeId == employeeId)
        //                join leaveStatus in _appContext.LeaveStatus on employeeLeaves.LeaveStatusId equals leaveStatus.Id
        //                join leavesEntitlement in _appContext.EmployeeLeavesEntitlement on employeeLeaves.Id equals leavesEntitlement.EmployeeLeavesId
        //                join leaveRule in _appContext.LeaveRules on leavesEntitlement.LeaveRulesId equals leaveRule.Id
        //                join leaveType in _appContext.LeaveType on leaveRule.LeaveTypeId equals leaveType.Id
        //                join employeeLeaveDetail in _appContext.EmployeeLeaveDetail on leavesEntitlement.Id equals employeeLeaveDetail.LeavesEntitlementId
        //                select new EmployeeLeaveDetail { Approved = leavesEntitlement.Approved, Pending = leavesEntitlement.Pending, Rejected = leavesEntitlement.Rejected, LeaveRules = new LeaveRules { LeaveTypeId = leaveType.Id, LeavesPerYear = leaveRule.LeavesPerYear, LeaveType = new LeaveType { Name = leaveType.Name } } };
        //    empleaveDetails = query.ToList();
        //    return empleaveDetails;
        //}

        public EmployeeLeaves EmpLeaves(Guid employeeId)
        {
            var employee = _appContext.EmployeeLeaves.Where(m => m.EmployeeId == employeeId && m.IsActive == true).FirstOrDefault();
            return employee;
        }

        public bool chkConfigSet()
        {
            var flag = false;

            var query = (from period in _appContext.LeavePeriod.Where(m => m.IsActive == true && m.IsLeavePeriodCompleted == false)
                         join rules in _appContext.LeaveRules on period.Id equals rules.LeavePeriodId
                         join type in _appContext.LeaveType on period.Id equals type.LeavePeriodId
                         select new LeavePeriod
                         {
                             Id = period.Id
                         }).ToList();
            if (query.Count()>0)
            {
                flag = true;
            }

            return flag;
        }
    }
}
