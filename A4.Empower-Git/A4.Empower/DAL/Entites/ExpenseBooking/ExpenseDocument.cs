using A4.DAL.Entites;
using System;
using System.Collections.Generic;
using System.Text;

namespace A4.DAL.Entites
{
    public class ExpenseDocument:AuditableEntity
    {
        public ExpenseDocument()
        {
            Id = Guid.NewGuid();
        }
        /// <summary>
        /// Gets or sets the entity identifier
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Gets or sets the product identifier
        /// </summary>
        public int ExpenseBookingId { get; set; }

        /// <summary>
        /// Gets or sets the picture identifier
        /// </summary>
        public int PictureId { get; set; }

        /// <summary>
        /// Gets the picture
        /// </summary>
        public virtual Picture Picture { get; set; }

        /// <summary>
        /// Gets the product
        /// </summary>
        public virtual ExpenseBookingRequest ExpenseBookingRequest { get; set; }
    }
}
