using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace A4.DAL.Entites
{
    public class MassInterview : AuditableEntity
    {
        #region Constructor

        public MassInterview()
        {
            Id = Guid.NewGuid();
        }

        #endregion

        #region Properties

        public Guid Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Venue { get; set; }
        public string Address { get; set; }


        #endregion

        #region Children
    
        public ICollection<MassInterviewDetail> MassInterviewDetail { get; set; }
        #endregion
    }
}
