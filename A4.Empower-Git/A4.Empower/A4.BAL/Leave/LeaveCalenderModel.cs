using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace A4.BAL
{
    public class LeaveCalenderModel
    {
        public string Title { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public bool AllDay { get; set; }
        public string BackgroundColor { get; set; }
        public string BorderColor { get; set; }
        public string Tooltip { get; set; }
        public string Status { get; set; }
    }
}
