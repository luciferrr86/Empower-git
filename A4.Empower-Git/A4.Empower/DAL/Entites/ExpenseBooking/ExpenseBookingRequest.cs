using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace A4.DAL.Entites
{
    public class ExpenseBookingRequest : AuditableEntity
    {
        #region Constuctor 
        //public ExpenseBookingRequest()
        //{
        //    Id = Guid.NewGuid();
        //}
        #endregion

        #region ForeignKeyRelation

        /// <summary>
        /// 
        /// </summary>
        public Guid DepartmentID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public FunctionalDepartment FunctionalDepartment { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Guid EmployeeId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Employee Employee { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Guid ExpenseBookingSubCategoryItemId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public ExpenseBookingSubCategoryItem ExpenseBookingSubCategoryItem { get; set; }

        #endregion

        #region Properties

        /// <summary>
        /// 
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }

        public string BookingId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime FromDate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime ToDate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime RequestDate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime ApprovedOrRejectedDate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Amount { get; set; }


        public int Status { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string File { get; set; }

        #endregion

        #region Children

        public ICollection<ExpenseBookingApprover> ExpenseBookingApprovers { get; set; }

        public ICollection<ExpenseDocument> ExpenseDocument { get; set; }

        #endregion
    }
}
