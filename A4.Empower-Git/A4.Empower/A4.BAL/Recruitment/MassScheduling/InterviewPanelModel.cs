using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace A4.BAL
{
    public class InterviewPanelModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public DateTime StartTime { get; set; }
        [Required]
        public DateTime EndTime { get; set; }
        [Required]
        public DateTime BreakTime { get; set; }
        [Required]
        public string VacancyId { get; set; }
        [Required]
        public List<string> ManagerIdList { get; set; }
    }
}
