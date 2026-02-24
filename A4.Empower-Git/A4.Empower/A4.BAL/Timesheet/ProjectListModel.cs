using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace A4.BAL
{
    public class ProjectListModel
    {
        public string ProjectId { get; set; }
        public string Name { get; set; }
        public string TotalProjectHour { get; set; }
        public List<ProjectHour> ProjectHourList { get; set; }
    }

    public class ProjectHour
    {
        public string UserDetailProjectHourId { get; set; }
        public bool IsAllotted { get; set; }
        public bool IsAllow { get; set; }
       [RegularExpression("^([0-9]|0[0-9]|1[0-9]|2[0-3]):[0-5][0-9]$", ErrorMessage = "You time format must be in hh:mm")]
       public string Hour { get; set; }
    }
}
