using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace A4.BAL
{
    public class LeaveApplyModel
    {
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        [Required]
        public string LeaveTypeId { get; set; }
        [Required]
        public string ReasonForApply { get; set; }

        [Required]
        public string UserId { get; set; }




    }
}
