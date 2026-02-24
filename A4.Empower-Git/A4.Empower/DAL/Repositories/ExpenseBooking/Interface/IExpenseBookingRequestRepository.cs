using A4.BAL;
using A4.BAL.ExpenseBooking;
using A4.DAL.Entites;
using DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace A4.DAL.Repositories
{
    public interface IExpenseBookingRequestRepository : IRepository<ExpenseBookingRequest>
    {
        List<ExpenseBookingSubCategory> GetAllSubCategories(Guid categoryId);

        List<ExpenseBookingCategory> GetAllCategories();

        PagedList<ExpenseBookingRequest> GetEmployeeRequest(Guid employeeId, string name = "", int pageIndex = 0, int pageSize = int.MaxValue);

        PagedList<ExpenseBookingListModel> GetApproveRequest(Guid employeeId, string name = "", int pageIndex = 0, int pageSize = int.MaxValue);

        PagedList<ExpenseBookingListModel> GetAllApproveRequest(Guid employeeId, string name = "", int pageIndex = 0, int pageSize = int.MaxValue);


        ExpenseBookingRequest GetDepartment(Guid departmentId);

        List<ExpenseBookingSubCategoryItem> GetAllItem(Guid subCategoryId);

        ExpenseBookingRequest GetRequest(int requestId);

        ExpenseBookingSubCategoryItem GetSubCategoryId(Guid itemId);

        List<ExpenseApproveByLevel> GetUserManageLevelList(Guid employeeId, Guid managerId);

        List<ExpenseBookingListModel> GetAllApproveExcelData();

        List<ExpenseBookingListModel> GetAllApproveExcelDataByManager(Guid employeeId);
    }
}
