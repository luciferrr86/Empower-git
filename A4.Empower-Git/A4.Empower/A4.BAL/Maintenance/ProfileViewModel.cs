using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace A4.BAL
{

    public class PersonalDetail
    {

        public Guid Id { get; set; }
        public Guid EmpID { get; set; }
        public string FullName { get; set; }
        public string Gender { get; set; }
        public string FatherName { get; set; }
        public string MotherName { get; set; }
        public string WorkEmailId { get; set; }
        public string PersonalEmailId { get; set; }
        public string CurrentAddress { get; set; }
        public string PermanentAddress { get; set; }
        public string MaritalStatus { get; set; }
        public string IdProofType { get; set; }
        public string IdProofDetail { get; set; }
        public DateTime DOB { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string Country { get; set; }
        public string Nationality { get; set; }
        public string ContactNo { get; set; }
        public string OfficialContactNo { get; set; }
        public string EmailId { get; set; }
        public string EmergencyContactNo { get; set; }
        public string EmergencyContactName { get; set; }
        public string EmergencyContactRelation { get; set; }
        public string PanNumber { get; set; }
        public string AadhaarNumber { get; set; }
        public string CurrentCity { get; set; }
        public string CurrentState { get; set; }
        public string CurrentCountry { get; set; }
        public string CurrentZipCode { get; set; }
    }

    public class PostProfessionalDetail
    {
       public List<ProfessionalDetail> professional { get; set; }
    }

    public class ProfessionalDetail
    {

        public string Id { get; set; }
        public string EmpID { get; set; }
        public string CompanyName { get; set; }
        public string Designation { get; set; }
        public string Doj { get; set; }
        public string Dor { get; set; }
    }



    public class EducationlDetail
    {
        public Guid Id { get; set; }
        public Guid EmpID { get; set; }
        public string HigherDegreeInstitue { get; set; }
        public string HighestQualification { get; set; }
        public string HDSpecialization { get; set; }
        public string HDPassingYear { get; set; }
        public string HDPercentage { get; set; }
        public string SecondaryInstitute { get; set; }
        public string SecondaryQualification { get; set; }
        public string SDSpecialization { get; set; }
        public string SDPassingYear { get; set; }
        public string SDPercentage { get; set; }
        public string SSCInstitue { get; set; }
        public string SSCQualification { get; set; }
        public string SSCSpecialization { get; set; }
        public string SSCPassingYear { get; set; }
        public string SSCPercentage { get; set; }
    }

    public class ProfilePicture
    {
        public Guid EmpID { get; set; }
        public string ProfilePic { get; set; }
    }
}

