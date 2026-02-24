using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace A4.BAL
{
    public class ScheduleViewModel
    {
        public ScheduleViewModel()
        {
            lstSchedule = new List<ScheduleModel>();
            StatusCompanyModel = new StatusCompanyModel();
        }
        public StatusCompanyModel StatusCompanyModel { get; set; }
        public List<ScheduleModel> lstSchedule { get; set; }
        public int TotalCount { get; set; }
    }

    public class ScheduleModel
    {
        public string ScheduleId { get; set; }
        public string CompanyId { get; set; }
        public int fileId { get; set; }
        public bool isChecked { get; set; }
        public string File { get; set; }
        [Required(ErrorMessage = "Please enter date & Time")]
        public string MettingDate { get; set; }
        [Required(ErrorMessage = "Please enter Venue")]
        public string Venue { get; set; }
        [Required(ErrorMessage = "Please enter Writer")]
        public string Writer { get; set; }
        [Required(ErrorMessage = "Please enter Agenda")]
        public string Subject { get; set; }
        [Required(ErrorMessage = "Please enter Description")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Please enter Agenda")]
        public string Agenda { get; set; }
        public List<DropDownList> SelectClientPerson { get; set; }
        public List<string> ClientPerson { get; set; }
        public List<DropDownList> SelectInternalPerson { get; set; }
        public List<string> InternalPerson { get; set; }
    }

    public class MinutesOfMeetingModel
    {
        public string Id { get; set; }

        public string MeetingId { get; set; }

        public string MettingDate { get; set; }

        public string Venue { get; set; }

        public string Writer { get; set; }

        public string Subject { get; set; }

        public string Agenda { get; set; }

        public string MomDescription { get; set; }

        public List<DropDownList> SelectInternalPerson { get; set; }

        public List<string> InternalPerson { get; set; }

        [Required(ErrorMessage = "Please enter date & Time")]
        public DateTime NextActionDueDate { get; set; }

        [Required(ErrorMessage = "Please enter NextAction Status")]
        public string NextActionStatus { get; set; }

        public List<string> NextActionInternalPerson { get; set; }

        [Required(ErrorMessage = "Please enter NextAction Description")]
        public string NextActionDescription { get; set; }

    }

    public class ScheduleDropdownList : DropDownList
    {
        public bool Ischecked { get; set; }
    }
}
