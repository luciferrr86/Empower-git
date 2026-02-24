using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace A4.DAL.Entites
{
   public  class SalesStatus : AuditableEntity
    {
        #region Constructor
        public SalesStatus()
        {
            Id = Guid.NewGuid();
        }
        #endregion

        #region Children
        private ICollection<SalesDailyCall> _salesDailyCall = new List<SalesDailyCall>();
        public ICollection<SalesDailyCall> SalesDailyCall
        {
            get => _salesDailyCall;
            set => _salesDailyCall = (value ?? new List<SalesDailyCall>()).Where(p => p != null).ToList();
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
        /// <summary>
        /// 
        /// </summary>
        public string Description { get; set; }
        #endregion
    }
}
