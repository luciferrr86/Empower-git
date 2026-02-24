using System;
using System.Collections.Generic;
using System.Linq;

namespace A4.DAL.Entites
{
    public class FunctionalDepartment : AuditableEntity
    {
        #region Constructor
        public FunctionalDepartment()
        {
            Id = new Guid();
            //IsActive = true;
        }
        #endregion

        #region Children

        private ICollection<FunctionalGroup> _group = new List<FunctionalGroup>();

        public ICollection<FunctionalGroup> Group
        {
            get => _group;
            set => _group = (value ?? new List<FunctionalGroup>()).Where(p => p != null).ToList();
        }

        private ICollection<ExpenseBookingRequest> _expenseBookingRequest = new List<ExpenseBookingRequest>();

        public ICollection<ExpenseBookingRequest> ExpenseBookingRequest
        {
            get => _expenseBookingRequest;
            set => _expenseBookingRequest = (value ?? new List<ExpenseBookingRequest>()).Where(p => p != null).ToList();
        }
        #endregion

        //#region Navigation Property
        //public virtual ExpenseBookingRequest ExpenseBookingRequest { get; set; }
        //public virtual ICollection<ExpenseBookingRequest> ExpenseBookingRequests { get; set; }
        //#endregion

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
