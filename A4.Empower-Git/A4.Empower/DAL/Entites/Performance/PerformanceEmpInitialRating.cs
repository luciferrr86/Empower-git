using System;
using System.Collections.Generic;
using System.Text;

namespace A4.DAL.Entites
{
   public class PerformanceEmpInitialRating : AuditableEntity
    {
        #region Constructor
        public PerformanceEmpInitialRating()
        {
            Id = Guid.NewGuid();
        }
        #endregion

        #region Child

        #endregion

        #region ForeignKeyRelation
        public Guid PerformanceYearId { get; set; }
        public PerformanceYear PerformanceYear { get; set; }
        #endregion

        #region Properties
        public Guid Id { get; set; }
        public bool IsApproved { get; set; }
        #endregion
    }
}
