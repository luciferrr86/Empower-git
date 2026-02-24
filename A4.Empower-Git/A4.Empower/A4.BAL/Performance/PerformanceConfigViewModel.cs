using System;
using System.Collections.Generic;
using System.Text;

namespace A4.BAL
{
   public class PerformanceConfigViewModel
    {
        public string Id { get; set; }
        public string MyGoalInstructionText { get; set; }
        public string PlusesInstructionText { get; set; }
        public string DeltaInstructionText { get; set; }
        public string TrainingClassesInstructionText { get; set; }
        public string CurrentYearInstructionText { get; set; }
        public string NextYearInstructionText { get; set; }
        public string CareerDevInstructionText { get; set; }
        public string RatingInstructionText { get; set; }
        public bool IsPerformanceStart { get; set; }
        public List<PerformanceConfigRatingViewModel> performanceConfigRatingViewModel { get; set; }
        public List<PerformanceConfigFeebackViewModel> performanceConfigFeebackViewModel { get; set; }
    }

    public class PerformanceConfigRatingViewModel
    {
        public string Id { get; set; }
        public string RatingName { get; set; }
        public string RatingDescription { get; set; }
    }

    public class PerformanceConfigFeebackViewModel
    {
        public string Id { get; set; }
        public string LabelText { get; set; }
        public string Description { get; set; }
    }
}
