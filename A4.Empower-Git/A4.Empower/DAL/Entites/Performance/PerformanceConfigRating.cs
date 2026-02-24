using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace A4.DAL.Entites
{
    public class PerformanceConfigRating : AuditableEntity
    {
        #region Constructor
        public PerformanceConfigRating()
        {
            Id = Guid.NewGuid();
        }
        #endregion

        #region Child
        private ICollection<PerformanceInitailRating> _performanceInitailRating = new List<PerformanceInitailRating>();

        public ICollection<PerformanceInitailRating> PerformanceInitailRating
        {
            get => _performanceInitailRating;
            set => _performanceInitailRating = (value ?? new List<PerformanceInitailRating>()).Where(p => p != null).ToList();
        }
        #endregion

        #region ForeignKeyRelation

        #endregion

        #region Properties
        public Guid Id { get; set; }
        public string RatingName { get; set; }
        public string RatingDescription { get; set; }
        #endregion
    }
}
