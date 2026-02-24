using System;
using System.Collections.Generic;
using System.Text;

namespace A4.DAL.Entites
{
   public class PerformanceInitailRating : AuditableEntity
    {
        #region Constructor
        public PerformanceInitailRating()
        {
            Id = Guid.NewGuid();
        }
        #endregion

        #region Child

        #endregion

        #region ForeignKeyRelation
        public Guid EmployeeId { get; set; }
        public Guid ManagerId { get; set; }
        public Employee Employee { get; set; }     

        public Guid PerformanceConfigRatingId { get; set; }
        public PerformanceConfigRating PerformanceConfigRating { get; set; }

        public Guid PerformanceYearId { get; set; }
        public PerformanceYear PerformanceYear { get; set; }
        #endregion

        #region Properties
        public Guid Id { get; set; }
        public bool IsCEOSignOff { get; set; }
        #endregion
    }
}
