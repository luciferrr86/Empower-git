using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace A4.DAL.Entites
{
   public class ExpenseBookingSubCategoryItem : AuditableEntity
    {
        #region Constructor

        public ExpenseBookingSubCategoryItem()
        {
            Id = Guid.NewGuid();
        }

        #endregion

        #region Children

        private ICollection<ExpenseBookingRequest> _expenseBookingRequest;

        public ICollection<ExpenseBookingRequest> ExpenseBookingRequest
        {
            get => _expenseBookingRequest;
            set => _expenseBookingRequest = (value ?? new List<ExpenseBookingRequest>()).Where(p => p != null).ToList();
        }

        #endregion

        #region foreignKeyRelation

        /// <summary>
        /// 
        /// </summary>
        public Guid ExpenseBookingSubCategoryId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public ExpenseBookingSubCategory ExpenseBookingSubCategory { get; set; }

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
