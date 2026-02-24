using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace A4.DAL.Entites
{
   public class PerformanceGoalMeasure : AuditableEntity
    {
        #region Constructor
        public PerformanceGoalMeasure()
        {
            Id = Guid.NewGuid();
        }
        #endregion

        #region Child
        private ICollection<PerformanceGoalMeasureFunc> _performanceGoalMeasureFunc = new List<PerformanceGoalMeasureFunc>();

        public ICollection<PerformanceGoalMeasureFunc> PerformanceGoalMeasureFunc
        {
            get => _performanceGoalMeasureFunc;
            set => _performanceGoalMeasureFunc = (value ?? new List<PerformanceGoalMeasureFunc>()).Where(p => p != null).ToList();
        }

        private ICollection<PerformanceGoalMeasureIndiv> _performanceGoalMeasureIndiv = new List<PerformanceGoalMeasureIndiv>();

        public ICollection<PerformanceGoalMeasureIndiv> PerformanceGoalMeasureIndiv
        {
            get => _performanceGoalMeasureIndiv;
            set => _performanceGoalMeasureIndiv = (value ?? new List<PerformanceGoalMeasureIndiv>()).Where(p => p != null).ToList();
        }

        private ICollection<PerformanceEmpYearGoalDetail> _performanceEmpYearGoalDetail = new List<PerformanceEmpYearGoalDetail>();

        public ICollection<PerformanceEmpYearGoalDetail> PerformanceEmpYearGoalDetail
        {
            get => _performanceEmpYearGoalDetail;
            set => _performanceEmpYearGoalDetail = (value ?? new List<PerformanceEmpYearGoalDetail>()).Where(p => p != null).ToList();
        }

        private ICollection<PerformanceEmpMidYearGoalDetail> _performanceEmpMidYearGoalDetail = new List<PerformanceEmpMidYearGoalDetail>();

        public ICollection<PerformanceEmpMidYearGoalDetail> PerformanceEmpMidYearGoalDetail
        {
            get => _performanceEmpMidYearGoalDetail;
            set => _performanceEmpMidYearGoalDetail = (value ?? new List<PerformanceEmpMidYearGoalDetail>()).Where(p => p != null).ToList();
        }

        private ICollection<PerformanceEmpGoalNextYear> _performanceEmpGoalNextYear = new List<PerformanceEmpGoalNextYear>();

        public ICollection<PerformanceEmpGoalNextYear> PerformanceEmpGoalNextYear
        {
            get => _performanceEmpGoalNextYear;
            set => _performanceEmpGoalNextYear = (value ?? new List<PerformanceEmpGoalNextYear>()).Where(p => p != null).ToList();
        }
        #endregion

        #region ForeignKeyRelation
        public Guid PerformanceGoalId { get; set; }
        public PerformanceGoal PerformanceGoal { get; set; }

        public Guid PerformanceGoalMainId { get; set; }
        public PerformanceGoalMain PerformanceGoalMain { get; set; }
        #endregion

        #region Properties
        public Guid Id { get; set; }
        public string MeasureText { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }       
        #endregion
    }
}
