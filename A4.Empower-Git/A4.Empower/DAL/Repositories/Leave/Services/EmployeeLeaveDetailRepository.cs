using A4.BAL;
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
   public  class EmployeeLeaveDetailRepository : Repository<EmployeeLeaveDetail>, IEmployeeLeaveDetailRepository
    {
        public EmployeeLeaveDetailRepository(DbContext context) : base(context)
        {

        }

        private ApplicationDbContext _appContext => (ApplicationDbContext)_context;

        public override EmployeeLeaveDetail Get(Guid id)
        {
            var leaveDetail = _appContext.EmployeeLeaveDetail.Where(e => e.Id == id && e.IsActive == true).FirstOrDefault();
            return leaveDetail;
        }

        public PagedList<EmployeeLeaveDetail> GetAllEmployeeLeave(Guid employeeId, Guid periodId, string name = "", int pageIndex = 0, int pageSize = int.MaxValue)
        {
        
            var query = from empLeaveDetail in _appContext.EmployeeLeaveDetail.Where(c => c.IsActive && c.EmployeeId == employeeId && c.LeavePeriodId == periodId)
                        join emp in _appContext.Employee on empLeaveDetail.EmployeeId equals emp.Id
                        join user in _appContext.Users on emp.UserId equals user.Id
                        join status in _appContext.LeaveStatus on empLeaveDetail.LeaveStatusId equals status.Id
                        select new EmployeeLeaveDetail
                        {
                            Id = empLeaveDetail.Id,
                            Name = empLeaveDetail.Name,
                            Employee = new Employee { ApplicationUser = new ApplicationUser { FullName = user.FullName } },                       
                            LeaveStartDate = empLeaveDetail.LeaveStartDate,
                            LeaveEndDate = empLeaveDetail.LeaveEndDate,
                            LeaveStatus = new LeaveStatus { Name = status.Name },
                            IsSubmitted = empLeaveDetail.IsSubmitted,
                            IsSave = empLeaveDetail.IsSave
                        };
            var listLeaveDetails = query.AsQueryable();
            if (!string.IsNullOrWhiteSpace(name) && name != "undefined")
            listLeaveDetails = listLeaveDetails.Where(c => c.Name.ToLower().Contains(name.ToLower()));
            listLeaveDetails = listLeaveDetails.OrderBy(c => c.Name).ThenBy(c => c.Id);
            return new PagedList<EmployeeLeaveDetail>(listLeaveDetails, pageIndex, pageSize);
        }

        public List<EmployeeLeaveDetail> GetListofEmployeeLeave(Guid employeeId, Guid periodId)
        {
            var query = (from empLeaveDetail in _appContext.EmployeeLeaveDetail.Where(c => c.IsActive && c.EmployeeId == employeeId && c.LeavePeriodId == periodId)
                         join emp in _appContext.Employee on empLeaveDetail.EmployeeId equals emp.Id
                         join user in _appContext.Users on emp.UserId equals user.Id
                         join status in _appContext.LeaveStatus on empLeaveDetail.LeaveStatusId equals status.Id
                         select new EmployeeLeaveDetail
                         {
                             Name = empLeaveDetail.Name,
                             Employee = new Employee { ApplicationUser = new ApplicationUser { FullName = user.FullName } },
                             LeaveStartDate = empLeaveDetail.LeaveStartDate,
                             LeaveEndDate = empLeaveDetail.LeaveEndDate,
                             LeaveStatus = new LeaveStatus { Name = status.Name },
                         }).ToList();

            return query;
        }
        public List<EmployeeLeaveDetailModel> GetLeaveByEmpId(Guid id)
        {
            var query = (from empLeaveDetail in _appContext.EmployeeLeaveDetail.Where(c => c.IsActive && c.EmployeeId == id)
                             //join emp in _appContext.Employee on empLeaveDetail.ManagerId equals emp.Id
                             //join user in _appContext.Users on emp.UserId equals user.Id
                         select new EmployeeLeaveDetailModel
                         {
                             Id = empLeaveDetail.Id.ToString(),
                             ManagerComment = empLeaveDetail.ManagerComment,
                             //   ManagerName = user.FullName,
                             ReasonForApply = empLeaveDetail.ReasonForApply,
                             StartDate = empLeaveDetail.LeaveStartDate,
                             EndDate = empLeaveDetail.LeaveEndDate
                         }).ToList();
            return query;
        }

        public EmployeeLeaveDetailModel GetEmployeeLeaveDetail(Guid leaveDetailId)
        {
            var query = (from empLeaveDetail in _appContext.EmployeeLeaveDetail.Where(c => c.IsActive && c.Id == leaveDetailId)
                         join emp in _appContext.Employee on empLeaveDetail.ManagerId equals emp.Id
                         join user in _appContext.Users on emp.UserId equals user.Id
                         select new EmployeeLeaveDetailModel
                         {
                             Id = empLeaveDetail.Id.ToString(),
                             ManagerComment = empLeaveDetail.ManagerComment,
                             ManagerName = user.FullName ,
                             ReasonForApply = empLeaveDetail.ReasonForApply,
                             StartDate = empLeaveDetail.LeaveStartDate,
                             EndDate = empLeaveDetail.LeaveEndDate
                         }).FirstOrDefault();
            return query;
        }

        public PagedList<EmployeeLeaveDetail> GetSubOrdinateLeavesList(Guid empId, Guid periodId, string name = "", int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var query = (from empLeaveDetail in _appContext.EmployeeLeaveDetail.Where(c => c.IsActive && c.ManagerId == empId && c.LeavePeriodId == periodId && c.IsSubmitted == true)
                         join employee in _appContext.Employee on empLeaveDetail.EmployeeId equals employee.Id
                         join user in _appContext.Users on employee.UserId equals user.Id
                         join status in _appContext.LeaveStatus on empLeaveDetail.LeaveStatusId equals status.Id
                         select new EmployeeLeaveDetail
                         {
                             Id = empLeaveDetail.Id,
                             Name = empLeaveDetail.Name,
                             Employee = new Employee { ApplicationUser = new ApplicationUser { FullName = user.FullName } },
                             LeaveStartDate = empLeaveDetail.LeaveStartDate,
                             LeaveEndDate = empLeaveDetail.LeaveEndDate,
                             LeaveStatus = new LeaveStatus { Name = status.Name },
                         }).ToList();
            var list = query.AsQueryable();
            if (!string.IsNullOrWhiteSpace(name) && name != "undefined")
                list = list.Where(p => p.Employee.ApplicationUser.FullName.ToLower().Contains(name.ToLower()));
           
            list = list.OrderBy(c => c.Employee.ApplicationUser.FullName).ThenBy(c => c.Id);
            return new PagedList<EmployeeLeaveDetail>(list, pageIndex, pageSize);
        }

        public List<EmployeeLeaveDetail> GetListofSubOrdinateLeave(Guid employeeId, Guid periodId)
        {
            var query = (from emp in _appContext.Employee.Where(c => c.IsActive && c.ManagerId == employeeId )
                         join empLeaveDetail in _appContext.EmployeeLeaveDetail on emp.Id equals empLeaveDetail.EmployeeId 
                         join user in _appContext.Users on emp.UserId equals user.Id
                         join status in _appContext.LeaveStatus on empLeaveDetail.LeaveStatusId equals status.Id
                         select new EmployeeLeaveDetail
                         {
                             Name = empLeaveDetail.Name,
                             Employee = new Employee { ApplicationUser = new ApplicationUser { FullName = user.FullName } },
                             LeaveStartDate = empLeaveDetail.LeaveStartDate,
                             LeaveEndDate = empLeaveDetail.LeaveEndDate,
                             LeaveStatus = new LeaveStatus { Name = status.Name },
                         }).ToList();

            return query;
        }

        //public EmployeeLeaveDetail GetSubOrdinateLeaveDetail(Guid leaveDetailId)
        //{
        //    var query = (from empLeaveDetail in _appContext.EmployeeLeaveDetail.Where(c => c.IsActive && c.Id == leaveDetailId)
        //                 join emp in _appContext.Employee on empLeaveDetail.ManagerId equals emp.Id
        //                 join user in _appContext.Users on emp.UserId equals user.Id
        //                 join status in _appContext.LeaveStatus on empLeaveDetail.LeaveStatusId equals status.Id
        //                 select new EmployeeLeaveDetail
        //                 {
        //                     Id = empLeaveDetail.Id,
        //                     ManagerComment = empLeaveDetail.ManagerComment,
        //                     Name = empLeaveDetail.Name,
        //                     Employee = new Employee { ApplicationUser = new ApplicationUser { FullName = user.FullName } },
        //                     ReasonForApply = empLeaveDetail.ReasonForApply,
        //                     LeaveStartDate = empLeaveDetail.LeaveStartDate,
        //                     LeaveEndDate = empLeaveDetail.LeaveEndDate,
        //                     LeaveStatus = new LeaveStatus { Name = status.Name },
        //                     ManagerId = empLeaveDetail.ManagerId
        //                 }).FirstOrDefault();
        //    return query;
        //}

        public SubordinateLeaveDetailModel GetSubOrdinateLeaveDetail(Guid leaveDetailId)
        {
            var query = (from empLeaveDetail in _appContext.EmployeeLeaveDetail.Where(c => c.IsActive && c.Id == leaveDetailId)
                         join emp in _appContext.Employee on empLeaveDetail.ManagerId equals emp.Id
                         join user in _appContext.Users on emp.UserId equals user.Id
                         join status in _appContext.LeaveStatus on empLeaveDetail.LeaveStatusId equals status.Id
                         select new SubordinateLeaveDetailModel
                         {
                             Id = empLeaveDetail.Id.ToString(),
                             ManagerComment = empLeaveDetail.ManagerComment,
                             LeaveType = empLeaveDetail.Name,
                             ManagerName = user.FullName ,
                             ReasonForApply = empLeaveDetail.ReasonForApply,
                             StartDate = empLeaveDetail.LeaveStartDate,
                             EndDate = empLeaveDetail.LeaveEndDate,
                             Status = status.Name ,
                             ManagerId = empLeaveDetail.ManagerId.ToString()
                         }).FirstOrDefault();
            return query;
        }





    }
}
