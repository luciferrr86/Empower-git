using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace A4.BAL
{
    public class JobCandidateProfileViewModel : BasicDetails
    {
        public string Id { get; set; }
        [Required]
        public string Gender { get; set; }
        public string FatherName { get; set; }
        public string MotherName { get; set; }
        [Required]
        public string MartialStatus { get; set; }
        [Required]
        public DateTime DOB { get; set; }
        [Required]
        public string CurrentAddress { get; set; }
        public string PermanentAddress { get; set; }
        [Required]
        public string IdProofDetail { get; set; }
        [Required]
        public string Country { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string State { get; set; }
        [Required]
        public string ZipCode { get; set; }
        [Required]
        public string Nationality { get; set; }
        public string OfficialContactNo { get; set; }
        [Required]
        public string SkillSet { get; set; }
        [Required]
        public string JobApplicationId { get; set; }
        //public string WorkEmailId { get; set; }
        //public string PersonalEmailId { get; set; }
        //public string ProfilePic { get; set; }
    }
}
