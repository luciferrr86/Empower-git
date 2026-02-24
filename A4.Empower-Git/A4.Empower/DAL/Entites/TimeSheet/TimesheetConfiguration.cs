using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace A4.DAL.Entites
{
   public class TimesheetConfiguration : AuditableEntity
    {
        #region Constructor

        public TimesheetConfiguration()
        {
            Id = Guid.NewGuid();
        }

        #endregion

        #region Children
        private ICollection<TimesheetTemplate> _timesheetTemplate = new List<TimesheetTemplate>();

        public ICollection<TimesheetTemplate> TimesheetTemplate
        {
            get => _timesheetTemplate;
            set => _timesheetTemplate = (value ?? new List<TimesheetTemplate>()).Where(p => p != null).ToList();
        }
        #endregion

        #region Properties

        /// <summary>
        /// 
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int TimesheetFrequency { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string TsimesheetEditUpto { get; set; }

        #endregion
    }
}
