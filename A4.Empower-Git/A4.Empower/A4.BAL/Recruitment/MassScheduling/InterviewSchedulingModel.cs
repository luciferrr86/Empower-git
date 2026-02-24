using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace A4.BAL
{
    public class InterviewSchedulingModel
    {
        public String Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Venue { get; set; }
        public string Address { get; set; }

        public List<TimeModel> ListTime { get; set; }
     
    }

    public class TimeModel
    {
        public string Id { get; set; }
        public DateTime Date { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }

    public class RoomModel
    {
        public string Id { get; set; }
        public string RoomName { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
    }
}
