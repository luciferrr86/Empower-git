using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace A4.BAL
{
    public class JobCandidateProfileModel
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string Gender { get; set; }
        public string FatherName { get; set; }
        public string MotherName { get; set; }
        public string CurrentAddress { get; set; }
        public string PermanentAddress { get; set; }
        public string MaritalStatus { get; set; }
        public DateTime DOB { get; set; }
        public string IdProofDetail { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }
        public string Nationality { get; set; }
        public string OfficialContactNo { get; set; }
        public string SkillSet { get; set; }
        public string CandidateId { get; set; }
        public bool Iscompleted { get; set; }
        public string Resume { get; set; }
        public string CoverLetter { get; set; }
        public string picture { get; set; }
        public string ProfilePic { get; set; }
    }
}
