using System;
using System.Collections.Generic;
using System.Text;

namespace A4.DAL.Entites
{
    public class ExpenseBookingApprover:AuditableEntity
    {
        public ExpenseBookingApprover()
        {
            Id = new Guid();
        }

        #region Property
        public Guid Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int Status { get; set; }

        public int Level { get; set; }

        public bool IsAllow { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Guid ManagerId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Employee Manager { get; set; }



        #endregion

        #region Foreign Key
        public int ExpenseBookingRequestId { get; set; }

        public ExpenseBookingRequest ExpenseBookingRequests { get; set; }
        #endregion



        public ICollection<ExpenseBookingRequestDetail> ExpenseBookingRequestDetails { get; set; }


        public ICollection<ExpenseBookingInviteApprover> ExpenseBookingInviteApprovers { get; set; }
    }
}
