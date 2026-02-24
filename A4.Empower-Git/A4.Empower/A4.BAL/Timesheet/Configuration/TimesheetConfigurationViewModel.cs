using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace A4.Empower.ViewModels
{
    public class TimesheetConfigurationViewModel
    {
        public string  Id { get; set; }

        [Required]
        public int TimeSheetFrequency { get; set; }

        public string TsimesheetEditUpto { get; set; }

    }
}
