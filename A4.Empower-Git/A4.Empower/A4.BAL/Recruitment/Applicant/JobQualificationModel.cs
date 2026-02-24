using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace A4.BAL
{
    public class JobQualificationModel
    {
        public string Id { get; set; }
        public string HigherDegreeInstitue { get; set; }
        public string HighestQualification { get; set; }
        public string HDSpecialization { get; set; }
        public string HDPassingYear { get; set; }
        public string HDPercentage { get; set; }
        public string SecondaryInstitue { get; set; }
        public string SecondaryQualification { get; set; }
        public string SDSpecialization { get; set; }
        public string SDPassingYear { get; set; }
        public string SDPercentage { get; set; }
        public string SSCInstitue { get; set; }
        public string SSCQualification { get; set; }
        public string SSCSpecialization { get; set; }
        public string SSCPassingYear { get; set; }
        public string SSCPercentage { get; set; }
        public string ProfileId { get; set; }
    }
}
