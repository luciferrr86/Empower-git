using System;
using System.Collections.Generic;
using System.Text;

namespace A4.BAL
{ 
  public  class DayListModel
    {
        public string UserDetailId { get; set; }

        public string Day { get; set; }

        public DateTime Date { get; set; }

        public string UserSpanId { get; set; }

        public string TotalHour { get; set; }

        public bool IsUserSaved { get; set; }

        public bool IsUserSubmit { get; set; }

        public bool IsManagerApproved { get; set; }

        public bool IsAllotted { get; set; }

        public bool IsAllow { get; set; }
    }
}
