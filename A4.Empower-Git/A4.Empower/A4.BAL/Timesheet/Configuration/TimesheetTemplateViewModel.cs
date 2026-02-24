using A4.BAL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace A4.BAL
{
    public class TimesheetTemplateViewModel
    {
        public TimesheetTemplateViewModel()
        {
            TimesheetTemplateList = new List<TimesheetTemplateModel>();
            TimesheetConfigurationList = new List<DropDownList>();
        }
        public List<TimesheetTemplateModel> TimesheetTemplateList { get; set; }
        public int TotalCount { get; set; }
        public List<DropDownList> TimesheetConfigurationList { get; set; }
    }

    public class TimesheetTemplateModel
    {
        public TimesheetTemplateModel()
        {
            selectedDays = new List<string>();
        }

        public string Id { get; set; }

        public string TemplateName { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string TimesheetConfigurationId { get; set; }

        public Boolean Monday { get; set; }

        public Boolean Tuesday { get; set; }

        public Boolean Wednesday { get; set; }

        public Boolean Thursday { get; set; }

        public Boolean Friday { get; set; }

        public Boolean Saturday { get; set; }

        public Boolean Sunday { get; set; }

        public List<string> selectedDays { get; set; }

    }

    public class TimesheetScheduleViewModel
    {
        public TimesheetScheduleViewModel()
        {
            EmployeeList = new List<EmployeeListModel>();
            UserScheduleList = new List<UserScheduleModel>();
        }
        public List<DropDownList> TimesheetTemplateList { get; set; }
        public List<EmployeeListModel> EmployeeList { get; set; }
        public List<UserScheduleModel> UserScheduleList { get; set; }
        public int EmployeeCount { get; set; }
        public int ScheduleCount { get; set; }
    }
    public class UserScheduleModel
    {
        public string FullName { get; set; }

        public string EmployeeId { get; set; }

        public Boolean Monday { get; set; }

        public Boolean Tuesday { get; set; }

        public Boolean Wednesday { get; set; }

        public Boolean Thursday { get; set; }

        public Boolean Friday { get; set; }

        public Boolean Saturday { get; set; }

        public Boolean Sunday { get; set; }

        public string TimesheetFrequency { get; set; }

        public List<string> selectedDays { get; set; }

    }
    public class TimesheetScheduleModel
    {
        public string Id { get; set; }

        [Required]
        public string TemplateId { get; set; }

        [Required]
        public List<EmployeeListModel> EmployeeList { get; set; }
    }

}
