using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace A4.DAL.Entites
{
   public class TimesheetProject : AuditableEntity
    {
        #region Constructor

        public TimesheetProject()
        {
            Id = Guid.NewGuid();
        }

        #endregion

        #region ForeignKeyRelation

        /// <summary>
        /// 
        /// </summary>
        public Guid ManagerId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Employee Employee { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Guid ClientId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public TimesheetClient TimesheetClient { get; set; }

        #endregion

        #region Children

        private ICollection<TimesheetEmployeeProject> _timesheetEmployeeProject = new List<TimesheetEmployeeProject>();

        public ICollection<TimesheetEmployeeProject> TimesheetEmployeeProject
        {
            get => _timesheetEmployeeProject;
            set => _timesheetEmployeeProject = (value ?? new List<TimesheetEmployeeProject>()).Where(p => p != null).ToList();
        }

        private ICollection<TimesheetUserDetailProjectHour> _timesheetUserDetailProjectHour = new List<TimesheetUserDetailProjectHour>();

        public ICollection<TimesheetUserDetailProjectHour> TimesheetUserDetailProjectHour
        {
            get => _timesheetUserDetailProjectHour;
            set => _timesheetUserDetailProjectHour = (value ?? new List<TimesheetUserDetailProjectHour>()).Where(p => p != null).ToList();
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
        public string ProjectName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime EndDate { get; set; }

        #endregion
    }
}
