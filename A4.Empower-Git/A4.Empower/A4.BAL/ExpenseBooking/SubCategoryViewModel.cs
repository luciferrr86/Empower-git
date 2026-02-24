using System.Collections.Generic;

namespace A4.BAL
{
  
    public class CategoryViewModel
    {
        public CategoryViewModel()
        {
            CategoryList = new List<ExpenseCategoryModel>();
        }

        public List<ExpenseCategoryModel> CategoryList { get; set; }
        public int TotalCount { get; set; }
    }

    public class ExpenseCategoryModel
    {

        public string Id { get; set; }

        public string Name { get; set; }
    }


    public class SubCategoryViewModel
    {
        public SubCategoryViewModel()
        {
            SubCategoryList = new List<ExpenseSubCategoryModel>();
        }
        public List<ExpenseSubCategoryModel> SubCategoryList { get; set; }
        public List<DropDownList> CategoryList { get; set; }
        public int TotalCount { get; set; }
    }

    public class ExpenseSubCategoryModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Category { get; set; }

        public string CategoryId { get; set; }


    }


    public class SubCategoryItemViewModel
    {
        public SubCategoryItemViewModel()
        {
            SubCategoryItemList = new List<SubCategoryItemModel>();
        }

        public List<SubCategoryItemModel> SubCategoryItemList { get; set; }
        public List<DropDownList> CategoryList { get; set; }
        public List<DropDownList> SubCategoryList { get; set; }
        public int TotalCount { get; set; }
    }

    public class SubCategoryItemModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string SubCategory { get; set; }

        public string Category { get; set; }

        public string CategoryId { get; set; }

        public string SubCategoryId { get; set; }

    }

}
