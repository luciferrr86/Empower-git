using System;
using System.Collections.Generic;
using System.Text;

namespace A4.DAL.Entites
{
   public class PerformanceEmpGoalPresident : AuditableEntity
    {
        #region Constructor
        public PerformanceEmpGoalPresident()
        {
            Id = Guid.NewGuid();
        }
        #endregion

        #region Child

        #endregion

        #region ForeignKeyRelation
        public Guid PerformanceEmpGoalId { get; set; }
        public PerformanceEmpGoal PerformanceEmpGoal { get; set; }
        #endregion

        #region Properties
        public Guid Id { get; set; }
        public string PresidentSignature { get; set; }
        public string PresidentComment { get; set; }
        public string PresidentYearComment { get; set; }
        public bool PresidentSignOff { get; set; }
        public bool PresidentYearSignOff { get; set; }
        #endregion
    }
}
