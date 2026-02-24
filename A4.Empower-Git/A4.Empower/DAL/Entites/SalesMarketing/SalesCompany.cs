using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace A4.DAL.Entites
{
   public  class SalesCompany : AuditableEntity
    {
        #region Constructor
        public SalesCompany()
        {
            Id = Guid.NewGuid();
        }
        #endregion

        #region Children
        private ICollection<SalesCompanyContact> _salesCompanyContact = new List<SalesCompanyContact>();
        public ICollection<SalesCompanyContact> SalesCompanyContact
        {
            get => _salesCompanyContact;
            set => _salesCompanyContact = (value ?? new List<SalesCompanyContact>()).Where(p => p != null).ToList();
        }

        private ICollection<SalesDailyCall> _salesDailyCall = new List<SalesDailyCall>();
        public ICollection<SalesDailyCall> SalesDailyCall
        {
            get => _salesDailyCall;
            set => _salesDailyCall = (value ?? new List<SalesDailyCall>()).Where(p => p != null).ToList();
        }

        private ICollection<SalesScheduleMeeting> _salesScheduleMeeting = new List<SalesScheduleMeeting>();
        public ICollection<SalesScheduleMeeting> SalesScheduleMeeting
        {
            get => _salesScheduleMeeting;
            set => _salesScheduleMeeting = (value ?? new List<SalesScheduleMeeting>()).Where(p => p != null).ToList();
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
        public string ComapnyName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CompanyAddress { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string State { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Country { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ZipCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string EmailId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Telephone { get; set; }
        #endregion
    }
}
