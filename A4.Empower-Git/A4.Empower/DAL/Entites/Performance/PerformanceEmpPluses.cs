using System;
using System.Collections.Generic;
using System.Text;

namespace A4.DAL.Entites
{
  public class PerformanceEmpPluses : AuditableEntity
    {
        #region Constructor
        public PerformanceEmpPluses()
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
        public string Pluses { get; set; }
        public string ManagerComment { get; set; }
        #endregion
    }
}
