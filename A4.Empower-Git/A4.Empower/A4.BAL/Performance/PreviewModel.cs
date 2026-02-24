using System;
using System.Collections.Generic;
using System.Text;

namespace A4.BAL
{
   public class PreviewModel
    {
        public EmployeeDetail empDetail { get; set; }
        public TrainingClassViewModel trainingClassViewModel { get; set; }
        public GoalViewModel goalViewModel { get; set; }
        public CareerDevViewModel careerDevViewModel { get; set; }
        public RatingModel ratingModel { get; set; }
    }
}
