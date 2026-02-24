using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace A4.BAL
{
    public class MassSchedulingViewModel
    {

        public InterviewSchedulingModel InterviewSchedulingModel { get; set; }
        public List<RoomModel> ListRoom { get; set; }
        public List<SelectionJobModel> ListSelectionJobModel { get; set; }
        public InterviewPanelModel InterviewPanelModel { get; set; }
    }
}
