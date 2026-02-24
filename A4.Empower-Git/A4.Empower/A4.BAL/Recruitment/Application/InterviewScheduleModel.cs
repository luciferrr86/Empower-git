using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace A4.BAL
{
    public class InterviewScheduleViewModel
    {
        public InterviewScheduleViewModel()
        {
            InterviewScheduleLevelList = new List<InterviewScheduleLevel>();
            ManagerList = new List<DropDownList>();
            InterviewTypeList = new List<DropDownList>();
        }
        public List<InterviewScheduleLevel> InterviewScheduleLevelList { get; set; }
        public List<DropDownList> InterviewTypeList { get; set; }
        public List<DropDownList> ManagerList { get; set; }
    }
    public class InterviewScheduleLevel
    {
        public InterviewScheduleLevel()
        {
            InterviewScheduleModelList = new List<InterviewScheduleModel>();
        }
        public string LevelId { get; set; }
        public string Level { get; set; }
        public bool IsLevelCompleted { get; set; }
        public string InterviewId { get; set; }
        public bool IsInterviewCompleted { get; set; }
        public List<string> LevelManagerIds { get; set; }
        public List<InterviewScheduleModel> InterviewScheduleModelList { get; set; }
    }
    public class InterviewScheduleModel
    {
        public string InterviewId { get; set; }

        public DateTime Date { get; set; }

        public string Time { get; set; }

        public string ManagerId { get; set; }

        public string InterviewTypeId { get; set; }

        public string ManagerName { get; set; }

        public string Comment { get; set; }

        public bool InterviewStatus { get; set; }


        public bool IsLevelCompleted { get; set; }

        public bool IsCandidateSelected { get; set; } 

    }
     
    
}
