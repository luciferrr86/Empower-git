using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace A4.DAL.Entites
{
   public class ExpenseBookingCategory : AuditableEntity
    {
        #region Constructor

        public ExpenseBookingCategory()
        {
            Id = Guid.NewGuid();
        }

        #endregion

        #region Children

        private ICollection<ExpenseBookingSubCategory> _expenseBookingSubCategories = new List<ExpenseBookingSubCategory>();

        public ICollection<ExpenseBookingSubCategory> ExpenseBookingSubCategories
        {
            get => _expenseBookingSubCategories;
            set => _expenseBookingSubCategories = (value ?? new List<ExpenseBookingSubCategory>()).Where(p => p != null).ToList();
        } 

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
