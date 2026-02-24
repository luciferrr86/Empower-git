using System;
using System.Collections.Generic;
using System.Text;

namespace A4.DAL.Entites
{
   public class PerformanceEmpDevGoalDoc : AuditableEntity
    {
        #region Constructor
        public PerformanceEmpDevGoalDoc()
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
        public string FileName { get; set; }
        public string FileExtension { get; set; }
        public string Path { get; set; }
        public string FileSize { get; set; }
        #endregion
    }
}
