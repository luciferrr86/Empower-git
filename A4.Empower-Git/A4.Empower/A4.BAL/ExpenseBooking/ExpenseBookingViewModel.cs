using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace A4.BAL
{
    public class ExpenseBookingViewModel
    {
        public ExpenseBookingViewModel()
        {
            CategoryModel = new List<CategoryModel>();
        }
        public List<CategoryModel> CategoryModel { get; set; }

        public int CategoryCount { get; set; }

    }
    public class SubCategoryModel
    {
        public string Id { get; set; }

        public string Name { get; set; }
    }

    public class CategoryModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public List<SubCategoryModel> SubCategoryList { get; set; }
    }
}
