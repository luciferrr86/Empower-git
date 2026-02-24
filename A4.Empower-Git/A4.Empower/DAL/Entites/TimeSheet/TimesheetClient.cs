using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace A4.DAL.Entites
{
   public class TimesheetClient : AuditableEntity
    {
        #region Constructor

        public TimesheetClient()
        {
            Id = Guid.NewGuid();
        }

        #endregion

        #region Children

        private ICollection<TimesheetProject> _timesheetProject = new List<TimesheetProject>();

        public ICollection<TimesheetProject> TimesheetProject
        {
            get => _timesheetProject;
            set => _timesheetProject = (value ?? new List<TimesheetProject>()).Where(p => p != null).ToList();
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
        public string Contact { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string EmailId { get; set; }

        #endregion
    }
}
