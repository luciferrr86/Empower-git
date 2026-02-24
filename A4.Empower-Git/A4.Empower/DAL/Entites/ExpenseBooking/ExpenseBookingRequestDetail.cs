using System;
using System.Collections.Generic;
using System.Text;

namespace A4.DAL.Entites
{
    public class ExpenseBookingRequestDetail : AuditableEntity
    {
        public int Id { get; set;}

        public string ApproverComment { get; set; }

        public string EmployeeComment { get; set; }

        public bool IsNew { get; set; }

        #region Foreign Key
        public Guid ExpenseBookingApproverId { get; set; }

        public ExpenseBookingApprover ExpenseBookingApprovers { get; set; }
        #endregion

    }
}
