using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace A4.BAL
{
    public class JobQualificationViewModel
    {
        public string Id { get; set; }
        [Required]
        public string HigherDegreeInstitue { get; set; }
        [Required]
        public string HighestQualification { get; set; }
        [Required]
        public string HDSpecialization { get; set; }
        [Required]
        public string HDPassingYear { get; set; }
        [Required]
        public string HDPercentage { get; set; }
        [Required]
        public string SecondaryInstitue { get; set; }
        [Required]
        public string SecondaryQualification { get; set; }
        [Required]
        public string SDSpecialization { get; set; }
        [Required]
        public string SDPassingYear { get; set; }
        [Required]
        public string SDPercentage { get; set; }
        [Required]
        public string SSCInstitue { get; set; }
        [Required]
        public string SSCQualification { get; set; }
        [Required]
        public string SSCSpecialization { get; set; }
        [Required]
        public string SSCPassingYear { get; set; }
        [Required]
        public string SSCPercentage { get; set; }
        [Required]
        public string ProfileId { get; set; }
    }
}
