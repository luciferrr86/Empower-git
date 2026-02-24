using A4.BAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace A4.BAL
{
    public class InterviewerModel
    {
        public InterviewerModel()
        {
            PanleModel = new List<Panel>();

        }
        public List<DropDownList> jobVacancyList { get; set; }
        public List<DropDownList> MangerList { get; set; }
        public List<DropDownList> dateList { get; set; }
        public List<Panel> PanleModel { get; set; }
    }
    public class Panel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string BreakStartTime { get; set; }
        public string BreakEndTime { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string InterViewDate { get; set; }
        public string InterViewDateId { get; set; }
        public string VacancyId { get; set; }
        public string Vacancy { get; set; }
        public List<string> ManagerIdList { get; set; }
    }
    public class CandidateInterviewSlot
    {
        public Guid PanelId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public DateTime InterviewDate { get; set; }
        public Guid JobId { get; set; }
        public Guid CandidateId { get; set; }
        public Guid VaccancyId { get; set; }

    }
    public class MassCandidateList
    {
        public Guid CandidateId { get; set; }

        public Guid VaccancyId { get; set; }

    }
    public class AllCandidateInterviewSlot
    {

        public AllCandidateInterviewSlot()
        {
            CandidateInterviewSlotList = new List<CandidateInterviewSlot>();
        }
        public List<CandidateInterviewSlot> CandidateInterviewSlotList { get; set; }
    }
    public class MassCandidateInterviewSchedule
    {
        public Guid PanelId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public DateTime InterviewDate { get; set; }
        public Guid CandidateId { get; set; }

    }
}
