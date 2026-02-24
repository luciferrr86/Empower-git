using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace A4.DAL.Entites
{
   public class PerformanceEmpDevGoal : AuditableEntity
    {
        #region Constructor
        public PerformanceEmpDevGoal()
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
        public string SkillDevelopment { get; set; }
        public string CareerInterest { get; set; }
        [MaxLength(256)]
        public string DevGoalBy { get; set; }
        #endregion
    }
}
