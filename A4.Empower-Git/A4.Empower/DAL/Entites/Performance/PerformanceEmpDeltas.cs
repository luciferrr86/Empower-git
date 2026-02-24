using System;
using System.Collections.Generic;
using System.Text;

namespace A4.DAL.Entites
{
  public class PerformanceEmpDeltas : AuditableEntity
    {
        #region Constructor
        public PerformanceEmpDeltas()
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
        public string Delta { get; set; }
        public string ManagerComment { get; set; }       
        #endregion
    }
}
