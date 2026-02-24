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
    public class SubCategoryItemRepository : Repository<ExpenseBookingSubCategoryItem>, ISubCategoryItemRepository
    {
        public SubCategoryItemRepository(DbContext context) : base(context)
        {
        }

        private ApplicationDbContext _appContext => (ApplicationDbContext)_context;

        public PagedList<ExpenseCategoryModel> GetAllCategory(string name = "", int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var query = from category in _appContext.ExpenseBookingCategory.Where(m => m.IsActive)
                        select new ExpenseCategoryModel
                        {
                            Id = category.Id.ToString(),
                            Name = category.Name,
                        };
            if (!string.IsNullOrWhiteSpace(name) && name != "undefined")
                query = query.Where(c => c.Name.Contains(name));
            query = query.OrderBy(c => c.Name).ThenBy(c => c.Id);
            return new PagedList<ExpenseCategoryModel>(query, pageIndex, pageSize);
        }

        public PagedList<ExpenseSubCategoryModel> GetAllSubCategory(string name = "", int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var query = from subCategroy in _appContext.ExpenseBookingSubCategory.Where(m => m.IsActive)
                        join category in _appContext.ExpenseBookingCategory.Where(m => m.IsActive) on subCategroy.ExpenseBookingCategoryId equals category.Id
                        select new ExpenseSubCategoryModel
                        {
                            Id=subCategroy.Id.ToString(),
                            Name = subCategroy.Name,
                            Category = category.Name,
                            CategoryId = category.Id.ToString(),
                        };
            if (!string.IsNullOrWhiteSpace(name) && name != "undefined")
                query = query.Where(c => c.Name.Contains(name));
            query = query.OrderBy(c => c.Name).ThenBy(c => c.Id);
            return new PagedList<ExpenseSubCategoryModel>(query, pageIndex, pageSize);
        }

        public PagedList<SubCategoryItemModel> GetAll(string name = "", int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var query = from subCategoryItem in _appContext.ExpenseBookingSubCategoryItem.Where(m => m.IsActive)
                        join subCategroy in _appContext.ExpenseBookingSubCategory.Where(m => m.IsActive) on subCategoryItem.ExpenseBookingSubCategoryId equals subCategroy.Id
                        join category in _appContext.ExpenseBookingCategory.Where(m => m.IsActive) on subCategroy.ExpenseBookingCategoryId equals category.Id
                        select new SubCategoryItemModel
                        {
                            Id = subCategoryItem.Id.ToString(),
                            Name = subCategoryItem.Name,
                            Category = category.Name,
                            CategoryId = category.Id.ToString(),
                            SubCategory = subCategroy.Name,
                            SubCategoryId = subCategroy.Id.ToString()
                        };
            if (!string.IsNullOrWhiteSpace(name) && name != "undefined")
                query = query.Where(c => c.Name.Contains(name));
            query = query.OrderBy(c => c.Name).ThenBy(c => c.Id);
            return new PagedList<SubCategoryItemModel>(query, pageIndex, pageSize);
        }

        public List<ExpenseBookingSubCategory> GetSubCategory(Guid categoryId)
        {
            var query = _appContext.ExpenseBookingSubCategory.Where(m => m.IsActive && m.ExpenseBookingCategoryId == categoryId).ToList();
            return query;
        }

        public List<ExpenseBookingCategory> GetCategory()
        {
            var query = _appContext.ExpenseBookingCategory.Where(m => m.IsActive == true).ToList();
            return query;
        }

    }
}
