using A4.BAL;
using A4.BAL.ExpenseBooking;
using A4.DAL.Entites;
using DAL;
using DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace A4.DAL.Repositories
{
    public class ExpenseBookingRequestRepository : Repository<ExpenseBookingRequest>, IExpenseBookingRequestRepository
    {
        public ExpenseBookingRequestRepository(DbContext context) : base(context)
        {

        }

        private ApplicationDbContext _appContext => (ApplicationDbContext)_context;

        public List<ExpenseBookingSubCategory> GetAllSubCategories(Guid categoryId)
        {
            var subCategory = _appContext.ExpenseBookingSubCategory.Where(m => m.IsActive && m.ExpenseBookingCategoryId == categoryId).ToList();
            return subCategory;
        }

        public List<ExpenseBookingCategory> GetAllCategories()
        {
            var category = _appContext.ExpenseBookingCategory.Where(m => m.IsActive == true).ToList();
            return category;
        }

        public PagedList<ExpenseBookingRequest> GetEmployeeRequest(Guid employeeId, string name = "", int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var listRequest = _appContext.ExpenseBookingRequest.AsQueryable().Where(c => c.IsActive && c.EmployeeId == employeeId);
            return new PagedList<ExpenseBookingRequest>(listRequest, pageIndex, pageSize);
        }

        public PagedList<ExpenseBookingListModel> GetApproveRequest(Guid employeeId, string name = "", int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var listRequest = (from request in _appContext.ExpenseBookingRequest.AsQueryable().Where(c => c.IsActive && (c.Status == 1 || c.Status == 2))
                               join department in _appContext.FunctionalDepartment on request.DepartmentID equals department.Id
                               join employee in _appContext.Employee on request.EmployeeId equals employee.Id
                               from approver in _appContext.ExpenseBookingApprover.Where(m => m.ManagerId == employeeId && (m.IsAllow == true || (m.Status == 2 && request.Status == 2)))
                               where request.Id == approver.ExpenseBookingRequestId
                               select new
                               {
                                   BookingId = request.BookingId,
                                   ApprovedOrRejectedDate = request.ApprovedOrRejectedDate.ToShortDateString(),
                                   RequestedDate = request.RequestDate.ToShortDateString(),
                                   EmployeeName = employee.ApplicationUser.FullName,
                                   IsInvite = false,
                                   Id = request.Id.ToString(),
                                   Amount = request.Amount,
                                   ExpensePeriod = request.FromDate.ToShortDateString() + " To " + request.ToDate.ToShortDateString(),
                                   Status = request.Status,
                                   Department = department.Name,
                                   File = request.File
                               }
                              ).ToList().Select(
                x => new ExpenseBookingListModel
                {
                    BookingId = x.BookingId,
                    ApprovedOrRejectedDate = x.ApprovedOrRejectedDate,
                    RequestedDate = x.RequestedDate,
                    EmployeeName = x.EmployeeName,
                    IsInvite = x.IsInvite,
                    Id = x.Id.ToString(),
                    Amount = x.Amount,
                    ExpensePeriod = x.ExpensePeriod,
                    Status = (Enum.GetName(typeof(ExpenseStatus), x.Status)).ToString(),
                    Department = x.Department,
                    File = x.File
                }).ToList();

            var inviteRequest = (from request in _appContext.ExpenseBookingRequest.AsQueryable().Where(c => c.IsActive && c.Status == 1)
                                 join department in _appContext.FunctionalDepartment on request.DepartmentID equals department.Id
                                 join employee in _appContext.Employee on request.EmployeeId equals employee.Id
                                 from approver in _appContext.ExpenseBookingInviteApprover.Where(m => m.EmployeeId == employeeId && m.Status == 5)
                                 join mainApprover in _appContext.ExpenseBookingApprover on approver.ExpenseBookingApproverId equals mainApprover.Id
                                 where request.Id == mainApprover.ExpenseBookingRequestId
                                 select new
                                 {
                                     BookingId = request.BookingId,
                                     ApprovedOrRejectedDate = request.ApprovedOrRejectedDate.ToShortDateString(),
                                     RequestedDate = request.RequestDate.ToShortDateString(),
                                     EmployeeName = employee.ApplicationUser.FullName,
                                     IsInvite = true,
                                     Id = request.Id.ToString(),
                                     Amount = request.Amount,
                                     ExpensePeriod = request.FromDate.ToShortDateString() + " To " + request.ToDate.ToShortDateString(),
                                     Status = request.Status,
                                     Department = request.Name,
                                     File = request.File
                                 }).
                                     ToList().Select(x => new ExpenseBookingListModel
                                     {
                                         BookingId = x.BookingId,
                                         ApprovedOrRejectedDate = x.ApprovedOrRejectedDate,
                                         RequestedDate = x.RequestedDate,
                                         EmployeeName = x.EmployeeName,
                                         IsInvite = true,
                                         Id = x.Id.ToString(),
                                         Amount = x.Amount,
                                         ExpensePeriod = x.ExpensePeriod,
                                         Status = (Enum.GetName(typeof(ExpenseStatus), x.Status)).ToString(),
                                         Department = x.Department,
                                         File = x.File
                                     }).ToList();


            if (listRequest.Count() > 0)
            {
                listRequest.Union(inviteRequest);
                return new PagedList<ExpenseBookingListModel>(listRequest, pageIndex, pageSize);
            }
            else
            {
                return new PagedList<ExpenseBookingListModel>(inviteRequest, pageIndex, pageSize);
            }


        }

        public PagedList<ExpenseBookingListModel> GetAllApproveRequest(Guid employeeId, string name = "", int pageIndex = 0, int pageSize = int.MaxValue)
        {

            var listRequest = (from request in _appContext.ExpenseBookingRequest.AsQueryable().Where(c => c.IsActive && c.Status == 2)
                               join department in _appContext.FunctionalDepartment on request.DepartmentID equals department.Id
                               join employee in _appContext.Employee on request.EmployeeId equals employee.Id
                               //from approver in _appContext.ExpenseBookingApprover.Where(m => m.ManagerId == employeeId && m.IsAllow == true)
                               //where request.Id == approver.ExpenseBookingRequestId
                               select new
                               {
                                   BookingId = request.BookingId,
                                   EmployeeName = employee.ApplicationUser.FullName,
                                   IsInvite = false,
                                   ApprovedOrRejectedDate = request.ApprovedOrRejectedDate.ToShortDateString(),
                                   RequestedDate = request.RequestDate.ToShortDateString(),
                                   Id = request.Id.ToString(),
                                   Amount = request.Amount,
                                   ExpensePeriod = request.FromDate.ToShortDateString() + " To " + request.ToDate.ToShortDateString(),
                                   Status = request.Status,
                                   Department = department.Name,
                                   File = request.File
                               }).ToList().Select(x =>
                                new ExpenseBookingListModel
                                {
                                    BookingId = x.BookingId,
                                    Id=x.Id,
                                    EmployeeName = x.EmployeeName,
                                    IsInvite = x.IsInvite,
                                    ApprovedOrRejectedDate = x.ApprovedOrRejectedDate,
                                    RequestedDate = x.RequestedDate,
                                    Amount = x.Amount,
                                    ExpensePeriod = x.ExpensePeriod,
                                    Status = (Enum.GetName(typeof(ExpenseStatus), x.Status)).ToString(),
                                    Department = x.Department,
                                    File = x.File
                                }).ToList();

            return new PagedList<ExpenseBookingListModel>(listRequest, pageIndex, pageSize);


        }
        public List<ExpenseBookingListModel> GetAllApproveExcelData()
        {
            var listRequest =( from request in _appContext.ExpenseBookingRequest.AsQueryable().Where(c => c.IsActive && c.Status == 2)
                              join department in _appContext.FunctionalDepartment on request.DepartmentID equals department.Id
                              join employee in _appContext.Employee on request.EmployeeId equals employee.Id

                              select new  { 
                                  BookingId = request.BookingId,
                                  EmployeeName = employee.ApplicationUser.FullName,
                                  IsInvite = false, 
                                  ApprovedOrRejectedDate = request.ApprovedOrRejectedDate.ToShortDateString(),
                                  RequestedDate = request.RequestDate.ToShortDateString(), 
                                  Id = request.Id.ToString(),
                                  Amount = request.Amount, 
                                  ExpensePeriod = request.FromDate.ToShortDateString() + " To " + request.ToDate.ToShortDateString(), 
                                  Status =  request.Status,
                                  Department = department.Name, 
                                  File = request.File }).ToList().Select(x=> new ExpenseBookingListModel
                                  {
                                      BookingId = x.BookingId,
                                      EmployeeName = x.EmployeeName,
                                      IsInvite = x.IsInvite,
                                      ApprovedOrRejectedDate = x.ApprovedOrRejectedDate,
                                      RequestedDate = x.RequestedDate,
                                      Id = x.Id,
                                      Amount = x.Amount,
                                      ExpensePeriod = x.ExpensePeriod,
                                      Status = (Enum.GetName(typeof(ExpenseStatus), x.Status)).ToString(),
                                      Department = x.Department,
                                      File = x.File
                                  }).ToList();

            return listRequest.ToList();
        }
        public List<ExpenseBookingListModel> GetAllApproveExcelDataByManager(Guid employeeId)
        {
            var listRequest = from request in _appContext.ExpenseBookingRequest.AsQueryable().Where(c => c.IsActive && c.Status == 2)
                              join department in _appContext.FunctionalDepartment on request.DepartmentID equals department.Id
                              join employee in _appContext.Employee on request.EmployeeId equals employee.Id
                              from approver in _appContext.ExpenseBookingApprover.Where(m => m.ManagerId == employeeId)
                              where request.Id == approver.ExpenseBookingRequestId
                              select new ExpenseBookingListModel { BookingId = request.BookingId, EmployeeName = employee.ApplicationUser.FullName, IsInvite = false, ApprovedOrRejectedDate = request.ApprovedOrRejectedDate.ToShortDateString(), RequestedDate = request.RequestDate.ToShortDateString(), Id = request.Id.ToString(), Amount = request.Amount, ExpensePeriod = request.FromDate.ToShortDateString() + " To " + request.ToDate.ToShortDateString(), Status = Enum.GetName(typeof(ExpenseStatus), request.Status), Department = department.Name, File = request.File };

            return listRequest.ToList();
        }

        public ExpenseBookingRequest GetDepartment(Guid departmentId)
        {
            var query = from expenseBooking in _appContext.ExpenseBookingRequest.Where(c => c.IsActive && c.DepartmentID == departmentId)
                        select new ExpenseBookingRequest
                        { FunctionalDepartment = new FunctionalDepartment { Id = expenseBooking.FunctionalDepartment.Id, Name = expenseBooking.FunctionalDepartment.Name } };
            return query.FirstOrDefault();
        }

        public List<ExpenseBookingSubCategoryItem> GetAllItem(Guid subCategoryId)
        {
            var query = from subcategory in _appContext.ExpenseBookingSubCategory.Where(c => c.IsActive && c.Id == subCategoryId)
                        join subcategoryItem in _appContext.ExpenseBookingSubCategoryItem on subcategory.Id equals subcategoryItem.ExpenseBookingSubCategoryId
                        select new ExpenseBookingSubCategoryItem { Id = subcategoryItem.Id, Name = subcategoryItem.Name };
            return query.ToList();
        }

        public ExpenseBookingRequest GetRequest(int requestId)
        {
            var query = from bookingRequest in _appContext.ExpenseBookingRequest.Where(c => c.IsActive && c.Id == requestId)
                        select new ExpenseBookingRequest
                        {
                            Id = bookingRequest.Id,
                            DepartmentID = bookingRequest.DepartmentID,
                            ExpenseBookingSubCategoryItemId = bookingRequest.ExpenseBookingSubCategoryItemId,
                            FromDate = bookingRequest.FromDate,
                            ToDate = bookingRequest.ToDate,
                            Amount = bookingRequest.Amount,
                            EmployeeId = bookingRequest.EmployeeId,
                            Status = bookingRequest.Status,
                            BookingId = bookingRequest.BookingId,
                            ExpenseBookingSubCategoryItem = new ExpenseBookingSubCategoryItem { Name = bookingRequest.ExpenseBookingSubCategoryItem.Name },
                            FunctionalDepartment = new FunctionalDepartment { Name = bookingRequest.FunctionalDepartment.Name }
                        };
            return query.FirstOrDefault();
        }

        public ExpenseBookingSubCategoryItem GetSubCategoryId(Guid itemId)
        {
            var query = _appContext.ExpenseBookingSubCategoryItem.Where(m => m.IsActive && m.Id == itemId).FirstOrDefault();
            return query;
        }

        public List<ExpenseApproveByLevel> GetUserManageLevelList(Guid employeeId, Guid managerId)
        {

            //var emplevelDetail = cPerformanceGoalMain.GetAboveLevelEmployee(loginUserInfo.EmployeeId, loginUserInfo.ReportingHead).AsEnumerable().ToList();
            List<SqlParameter> a = new List<SqlParameter>();
            a.Add(new SqlParameter("@ReportingHead", SqlDbType.UniqueIdentifier));
            a[a.Count - 1].Value = managerId;
            a.Add(new SqlParameter("@LoginUserID", SqlDbType.UniqueIdentifier));
            a[a.Count - 1].Value = employeeId;
            var emplevelDetail = _appContext.ExpenseApproveByLevel.FromSqlRaw("exec uspGetExpenseLevelEmployee @ReportingHead,@LoginUserID", a.ToArray()).ToList();

            var userListEmp = from empLevel in emplevelDetail
                              select new ExpenseApproveByLevel { EmpID = empLevel.EmpID, EmpLevel = empLevel.EmpLevel, ManagerID = empLevel.ManagerID, TitleId = empLevel.TitleId };

            //var userLevelEmp = (from empLevel in emplevelDetail.GroupBy(x => x.EmpLevel).Select(y => y.First())
            //                    select new DropDownList { Value = empLevel.EmpLevel.ToString(), Label = "Level" + empLevel.EmpLevel });
            //return Tuple.Create(userListEmp.ToList(), userLevelEmp.ToList());
            return userListEmp.ToList();
        }
    }
}
