using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace A4.DAL.Entites
{
   public  class SalesCompanyContact : AuditableEntity
    {
        #region Constructor
        public SalesCompanyContact()
        {
            Id = Guid.NewGuid();
        }
        #endregion

        #region Children
        private ICollection<SalesScheduleMeetingExternal> _salesScheduleMeetingExternal = new List<SalesScheduleMeetingExternal>();
        public ICollection<SalesScheduleMeetingExternal> SalesScheduleMeetingExternal
        {
            get => _salesScheduleMeetingExternal;
            set => _salesScheduleMeetingExternal = (value ?? new List<SalesScheduleMeetingExternal>()).Where(p => p != null).ToList();
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
        #endregion

        #region Properties

        /// <summary>
        /// 
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string EmailId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string MobileNo { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Telephone { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Designation { get; set; }
        #endregion


    }
}
