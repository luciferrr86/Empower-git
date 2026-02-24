using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace A4.DAL.Entites
{
   public class ExpenseBookingTitleAmount : AuditableEntity
    {
        #region Constuctor 
        public ExpenseBookingTitleAmount()
        {
            Id = Guid.NewGuid();
        }
        #endregion

        #region ForeignKeyRelation

        /// <summary>
        /// 
        /// </summary>
        public Guid TitleID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [ForeignKey("TilteID")]
        public FunctionalTitle FunctionalTitle { get; set; }



        #endregion

        #region Properties

        /// <summary>
        /// 
        /// </summary>
        public Guid Id { get; set; }


        /// <summary>
        /// 
        /// </summary>
        public string Amount { get; set; }


        #endregion

  
    }
}
