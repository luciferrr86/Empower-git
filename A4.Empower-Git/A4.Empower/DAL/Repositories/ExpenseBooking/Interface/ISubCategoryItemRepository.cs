using A4.BAL;
using A4.DAL.Entites;
using DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace A4.DAL.Repositories
{
   public interface ISubCategoryItemRepository :IRepository<ExpenseBookingSubCategoryItem>
    {
        PagedList<SubCategoryItemModel> GetAll(string name = "", int pageIndex = 0, int pageSize = int.MaxValue);

        PagedList<ExpenseCategoryModel> GetAllCategory(string name = "", int pageIndex = 0, int pageSize = int.MaxValue);

        PagedList<ExpenseSubCategoryModel> GetAllSubCategory(string name = "", int pageIndex = 0, int pageSize = int.MaxValue);

        List<ExpenseBookingSubCategory> GetSubCategory(Guid categoryId);

        List<ExpenseBookingCategory> GetCategory();
    }
}
