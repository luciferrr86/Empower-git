using System;
using System.Collections.Generic;
using System.Text;

namespace A4.DAL.Entites
{
    public class ExpenseBookingInviteApprover : AuditableEntity
    {
        public ExpenseBookingInviteApprover()
        {
            Id = new Guid();
        }

        #region Property
        public Guid Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Guid EmployeeId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Employee Employee { get; set; }



        #endregion

        #region Foreign Key
        public Guid ExpenseBookingApproverId { get; set; }

        public ExpenseBookingApprover ExpenseBookingApprovers { get; set; }
        #endregion

        public ICollection<ExpenseBookingRequestDetailInvite> ExpenseBookingRequestDetailInvites { get; set; }


    }
}
