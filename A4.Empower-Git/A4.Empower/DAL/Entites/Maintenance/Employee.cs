using A4.DAL.Entites.Leave;
using A4.DAL.Entites.Maintenance;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace A4.DAL.Entites
{
   public class Employee: AuditableEntity
    {
        #region Constructor
        public Employee()
        {
            Id = Guid.NewGuid();
        }
        #endregion

        #region Children 

        //private Personal _personal = new Personal();

        //public Personal Personal
        //{
        //    get => _personal;
        //    set => _personal = (value ?? new Personal());
        //}

        //private ICollection<Professional> _professional = new List<Professional>();

        //public ICollection<Professional> Professional
        //{
        //    get => _professional;
        //    set => _professional = (value ?? new List<Professional>());
        //}

        //private Qualification _qualification = new Qualification();

        //public Qualification Qualification
        //{
        //    get => _qualification;
        //    set => _qualification = (value ?? new Qualification());
        //}

        public Personal Personal { get; set; }
        public ICollection<Professional> Professional { get; set; }
        public Qualification Qualification { get; set; }

        private ICollection<PerformanceGoalMeasureIndiv> _performanceGoalMeasureIndiv = new List<PerformanceGoalMeasureIndiv>();

        public ICollection<PerformanceGoalMeasureIndiv> PerformanceGoalMeasureIndiv
        {
            get => _performanceGoalMeasureIndiv;
            set => _performanceGoalMeasureIndiv = (value ?? new List<PerformanceGoalMeasureIndiv>()).Where(p => p != null).ToList();
        }

        private ICollection<PerformanceGoalMain> _performanceGoalMain = new List<PerformanceGoalMain>();

        public ICollection<PerformanceGoalMain> PerformanceGoalMain
        {
            get => _performanceGoalMain;
            set => _performanceGoalMain = (value ?? new List<PerformanceGoalMain>()).Where(p => p != null).ToList();
        }

        private ICollection<PerformanceGoal> _performanceGoal = new List<PerformanceGoal>();

        public ICollection<PerformanceGoal> PerformanceGoal
        {
            get => _performanceGoal;
            set => _performanceGoal = (value ?? new List<PerformanceGoal>()).Where(p => p != null).ToList();
        }

        private ICollection<TimesheetUserSchedule> _timesheetUserSchedule = new List<TimesheetUserSchedule>();

        public ICollection<TimesheetUserSchedule> TimesheetUserSchedule
        {
            get => _timesheetUserSchedule;
            set => _timesheetUserSchedule = (value ?? new List<TimesheetUserSchedule>()).Where(p => p != null).ToList();
        }

        private ICollection<TimesheetProject> _timesheetProject = new List<TimesheetProject>();

        public ICollection<TimesheetProject> TimesheetProject
        {
            get => _timesheetProject;
            set => _timesheetProject = (value ?? new List<TimesheetProject>()).Where(p => p != null).ToList();
        }

        private ICollection<TimesheetEmployeeProject> _timesheetEmployeeProject = new List<TimesheetEmployeeProject>();

        public ICollection<TimesheetEmployeeProject> TimesheetEmployeeProject
        {
            get => _timesheetEmployeeProject;
            set => _timesheetEmployeeProject = (value ?? new List<TimesheetEmployeeProject>()).Where(p => p != null).ToList();
        }

        private ICollection<TimesheetUserSpan> _timesheetUserSpan = new List<TimesheetUserSpan>();

        public ICollection<TimesheetUserSpan> TimesheetUserSpan
        {
            get => _timesheetUserSpan;
            set => _timesheetUserSpan = (value ?? new List<TimesheetUserSpan>()).Where(p => p != null).ToList();
        }

        private ICollection<TimesheetUserDetail> _timesheetUserDetail = new List<TimesheetUserDetail>();

        public ICollection<TimesheetUserDetail> TimesheetUserDetail
        {
            get => _timesheetUserDetail;
            set => _timesheetUserDetail = (value ?? new List<TimesheetUserDetail>()).Where(p => p != null).ToList();
        }

        private ICollection<PerformanceEmpGoal> _performanceEmpGoal = new List<PerformanceEmpGoal>();

        public ICollection<PerformanceEmpGoal> PerformanceEmpGoal
        {
            get => _performanceEmpGoal;
            set => _performanceEmpGoal = (value ?? new List<PerformanceEmpGoal>()).Where(p => p != null).ToList();
        }

        #endregion

        #region ForeignKeyRelation
        /// <summary>
        /// 
        /// </summary>
        public Guid TitleId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public FunctionalTitle FunctionalTitle { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Guid GroupId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public FunctionalGroup FunctionalGroup { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Guid DesignationId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public FunctionalDesignation FunctionalDesignation { get; set; }

              /// <summary>
        /// 
        /// </summary>
        public Guid BandId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Band Band { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public ApplicationUser ApplicationUser { get; set; }

        #endregion

        #region Navigation Property
        public virtual ICollection<AttendenceSummary> AttendenceSummary { get; set; }
        public virtual ICollection<EmployeeAttendence> EmployeeAttendence { get; set; }
        public virtual ICollection<EmployeeSalary> EmployeeSalary { get; set; }
        public virtual ICollection<EmployeeCtc> EmployeeCtc { get; set; }
        public virtual ICollection<EmployeeLeaves> EmployeeLeaves { get; set; }
        public virtual ICollection<EmployeeLeaveDetail> EmployeeLeaveDetailManager { get; set; }
        public virtual ICollection<EmployeeLeaveDetail> EmployeeLeaveDetailEmployee { get; set; }

        //public virtual ExpenseBookingRequest ExpenseBookingRequest { get; set; }
        public virtual ICollection<ExpenseBookingRequest> ExpenseBookingRequestEmployee { get; set; }
        public virtual ICollection<ExpenseBookingApprover> ExpenseBookingRequestManager { get; set; }
        public virtual ICollection<ExpenseBookingInviteApprover> ExpenseBookingInviteManager { get; set; }
        public virtual ICollection<SalesMinuteMeetingInternal> SalesMinuteMeetingInternal { get; set; }
        public virtual ICollection<SalesScheduleMeetingInternal> SalesScheduleMeetingInternal { get; set; }
        public virtual ICollection<EmployeeProxy> EmpProxyFor { get; set; }
        public virtual ICollection<EmployeeProxy> EmpProxy { get; set; }
        #endregion


        #region Properties

         public Guid Id { get; set; }
        public string Location { get; set; }
       
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EmployeeCode { get; set; }
        public DateTime DOJ { get; set; }
        public Guid ManagerId { get; set; }
        public string EmpCode { get; set; }
        #endregion

        [NotMapped]
        public string ManagerName { get; set; }
        [NotMapped]
        public string ManagerEmail { get; set; }
    }
}
