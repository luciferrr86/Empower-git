using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace A4.DAL.Entites
{
   public class ExpenseBookingSubCategory : AuditableEntity
    {
        #region Constructor

        public ExpenseBookingSubCategory()
        {
            Id = Guid.NewGuid();
        }

        #endregion

        #region Children

        private ICollection<ExpenseBookingSubCategoryItem> _expenseBookingSubCategoryItems;

        public ICollection<ExpenseBookingSubCategoryItem> ExpenseBookingSubCategoryItems
        {
            get => _expenseBookingSubCategoryItems;
            set => _expenseBookingSubCategoryItems = (value ?? new List<ExpenseBookingSubCategoryItem>()).Where(p => p != null).ToList();
        }

        #endregion

        #region ForeignKeyRelation

        /// <summary>
        /// 
        /// </summary>
        public Guid ExpenseBookingCategoryId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public ExpenseBookingCategory ExpenseBookingCategory { get; set; }

        #endregion

        #region Properties

        /// <summary>
        /// 
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }

        #endregion
    }
}
