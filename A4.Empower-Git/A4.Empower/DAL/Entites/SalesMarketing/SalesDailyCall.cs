using System;
using System.Collections.Generic;
using System.Text;

namespace A4.DAL.Entites
{
   public  class SalesDailyCall : AuditableEntity
    {
        #region Constructor
        public SalesDailyCall()
        {
            Id = Guid.NewGuid();
        }
        #endregion

        #region ForeignKeyRelation
        /// <summary>
        /// 
        /// </summary>
        public Guid SalesCompanyId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public SalesCompany SalesCompany { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Guid SalesStatusId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public SalesStatus SalesStatus { get; set; }
        #endregion

        #region Properties
        /// <summary>
        /// 
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime CallDateTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Description { get; set; }
        #endregion
    }
}
