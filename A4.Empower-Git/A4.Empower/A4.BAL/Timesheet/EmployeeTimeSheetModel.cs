using System;
using System.Collections.Generic;
using System.Text;

namespace A4.BAL
{
   public  class EmployeeTimeSheetModel
    {
        public EmployeeTimeSheetModel()
        {
            DayList = new List<DayListModel>();
            ProjectList = new List<ProjectListModel>();
            lstUserWeeks = new List<DropDownList>();
        }

        public List<DropDownList> lstUserWeeks { get; set; }
        public List<DayListModel> DayList { get; set; }
        public List<ProjectListModel> ProjectList { get; set; }
        public string type { get; set; }
        public TimesheetUserConfigModel TimesheetConfig { get; set; }
        public string UserSpanId { get; set; }
        public string FullName { get; set; }
        public string Designation { get; set; }
        public string ApproverlId { get; set; }
        public bool IsManagerApproved { get; set; }
        public bool IsUserSaved { get; set; }
        public bool IsUserSubmit { get; set; }
        public string Frequency { get; set; }
        public string EmployeeId { get; set; }
        public string EmployeeEmail { get; set; }
        public string MangerName { get; set; }
        public string MangerEmail{ get; set; }
        public string TotalHour { get; set; }
    }

}
