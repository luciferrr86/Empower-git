using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using DAL;
using A4.DAL.Entites;
using Microsoft.AspNetCore.Authorization;
using static A4.Empower.Controllers.FileUploadController;
using A4.BAL;
using Microsoft.AspNetCore.Hosting;

namespace A4.Empower.Controllers
{
    [Authorize(AuthenticationSchemes = OpenIddict.Validation.AspNetCore.OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    public class ProfileController : Controller
    {
        readonly ILogger _logger;
        readonly IUnitOfWork _unitOfWork;
         IWebHostEnvironment _hostingEnvironment;
        public ProfileController(ILogger<ProfileController> logger, IUnitOfWork unitOfWork, IWebHostEnvironment hostingEnvironment)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpGet("profilePic/{id}")]
        public IActionResult GetProfilePicture(string id)
        {
            try
            {
                var status = new MyReponse();
                status.success = true;
                var picId = _unitOfWork.Profile.GetProfilePicId(id);
                status.pictureId = picId.ToString();
                var picture = _unitOfWork.FileUpload.GetPictureById(picId);
                status.imageUrl = _unitOfWork.FileUpload.GetPictureUrl(picture, _hostingEnvironment.WebRootPath);
                return Ok(status);
            }
            catch (Exception ex)
            {
                //throw ex;
                return BadRequest(ex.GetBaseException().Message);
            }          
        }       

        [HttpGet("personal/{id}")]
        [Produces(typeof(PersonalDetail))]
        public IActionResult Personals(string id)
        {
            try
            {
                var personalDetail = new PersonalDetail();
                if (id != "")
                {
                    var model = _unitOfWork.Profile.GetPersonalDetail(id);
                    if (model == null)
                    {
                        var employee = _unitOfWork.Employee.GetEmployee(id);
                        if (employee != null)
                        {
                            Personal personal = new Personal
                            {
                                EmployeeId = employee.Id
                            };
                            _unitOfWork.Profile.AddPersonalDetail(personal);

                            Qualification qualification = new Qualification
                            {
                                EmployeeId = employee.Id
                            };
                            _unitOfWork.Profile.AddQualificationDetail(qualification);
                            _unitOfWork.SaveChanges();
                        }
                        model = _unitOfWork.Profile.GetPersonalDetail(id);
                    }
                    if (model != null)
                    {
                        personalDetail.FullName = model.Employee.ApplicationUser.FullName;
                        personalDetail.City = model.City;
                        personalDetail.ContactNo = model.ContactNo;
                        personalDetail.Country = model.Country;
                        personalDetail.CurrentAddress = model.CurrentAddress;
                        personalDetail.DOB = model.DOB;
                        personalDetail.EmailId = model.EmailId;
                        personalDetail.WorkEmailId = model.Employee.ApplicationUser.Email;
                        personalDetail.EmergencyContactName = model.EmergencyContactName;
                        personalDetail.EmergencyContactNo = model.EmergencyContactNo;
                        personalDetail.EmergencyContactRelation = model.EmergencyContactRelation;
                        personalDetail.EmpID = model.EmployeeId;
                        personalDetail.FatherName = model.FatherName;
                        personalDetail.Gender = model.Gender;
                        personalDetail.Id = model.Id;
                        personalDetail.IdProofDetail = model.IdProofDetail;
                        personalDetail.MaritalStatus = model.MaritalStatus;
                        personalDetail.MotherName = model.MotherName;
                        personalDetail.Nationality = model.Nationality;
                        personalDetail.OfficialContactNo = model.OfficialContactNo;
                        personalDetail.PermanentAddress = model.PermanentAddress;
                        personalDetail.PersonalEmailId = model.EmailId;
                        personalDetail.State = model.State;
                        personalDetail.ZipCode = model.ZipCode;
                        personalDetail.PanNumber = model.PanNumber;
                        personalDetail.CurrentCity = model.CurrentCity;
                        personalDetail.CurrentState = model.CurrentState;
                        personalDetail.CurrentCountry = model.CurrentCountry;
                        personalDetail.CurrentZipCode = model.CurrentZipCode;
                        return Ok(personalDetail);
                    }
                    else
                    {
                        return NotFound(id);
                    }
                }
                return BadRequest(" Id cannot be null");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.GetBaseException().Message);
            }           
        }
 
        [HttpPost("CreatePersonalDetail")]
        public IActionResult CreatePersonalDetail([FromBody]PersonalDetail model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (model == null)
                        return BadRequest($"{nameof(model)} cannot be null");

                    if (!string.IsNullOrWhiteSpace(model.WorkEmailId) && !string.IsNullOrEmpty(model.WorkEmailId) && !string.IsNullOrWhiteSpace(model.ContactNo) && !string.IsNullOrEmpty(model.ContactNo))
                        return BadRequest("model property cannot be null");
                    var personal = new Personal();
                    personal.Gender = model.Gender;
                    personal.FatherName = model.FatherName;
                    personal.MotherName = model.MotherName;
                    personal.CurrentAddress = model.CurrentAddress;
                    personal.PermanentAddress = model.PermanentAddress;
                    personal.MaritalStatus = model.MaritalStatus;
                    personal.DOB = model.DOB;
                    personal.IdProofDetail = model.IdProofDetail;
                    personal.City = model.City;
                    personal.State = model.State;
                    personal.Country = model.Country;
                    personal.ZipCode = model.ZipCode;
                    personal.Nationality = model.Nationality;
                    personal.ContactNo = model.ContactNo;
                    personal.OfficialContactNo = model.OfficialContactNo;
                    personal.EmailId = model.EmailId;
                    personal.EmergencyContactNo = model.EmergencyContactNo;
                    personal.EmergencyContactName = model.EmergencyContactName;
                    personal.EmergencyContactRelation = model.EmergencyContactRelation;
                    personal.PanNumber = model.PanNumber;
                    personal.CurrentCity = model.CurrentCity;
                    personal.CurrentState = model.CurrentState;
                    personal.CurrentCountry = model.CurrentCountry;
                    personal.CurrentZipCode = model.CurrentZipCode;
                    _unitOfWork.Profile.AddPersonalDetail(personal);
                }
                return BadRequest(ModelState);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.GetBaseException().Message);
            }           
        }


        [HttpPut("updatePersonalDetail/{id}")]
        public IActionResult UpdatePersonalDetail(string id, [FromBody]PersonalDetail model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (model == null)
                        return BadRequest($"{nameof(model)} cannot be null");

                    var updatePersonal = _unitOfWork.Profile.GetPersonal(new Guid(id));
                    if (updatePersonal != null)
                    {
                        updatePersonal.Id = model.Id;
                        updatePersonal.Gender = model.Gender;
                        updatePersonal.FatherName = model.FatherName;
                        updatePersonal.MotherName = model.MotherName;
                        updatePersonal.CurrentAddress = model.CurrentAddress;
                        updatePersonal.PermanentAddress = model.PermanentAddress;
                        updatePersonal.MaritalStatus = model.MaritalStatus;
                        updatePersonal.DOB = model.DOB;
                        updatePersonal.IdProofDetail = model.IdProofDetail;
                        updatePersonal.City = model.City;
                        updatePersonal.State = model.State;
                        updatePersonal.Country = model.Country;
                        updatePersonal.ZipCode = model.ZipCode;
                        updatePersonal.Nationality = model.Nationality;
                        updatePersonal.ContactNo = model.ContactNo;
                        updatePersonal.OfficialContactNo = model.OfficialContactNo;
                        updatePersonal.EmailId = model.EmailId;
                        updatePersonal.EmergencyContactNo = model.EmergencyContactNo;
                        updatePersonal.EmergencyContactName = model.EmergencyContactName;
                        updatePersonal.EmergencyContactRelation = model.EmergencyContactRelation;
                        updatePersonal.PanNumber = model.PanNumber;
                        updatePersonal.CurrentCity = model.CurrentCity;
                        updatePersonal.CurrentState = model.CurrentState;
                        updatePersonal.CurrentCountry = model.CurrentCountry;
                        updatePersonal.CurrentZipCode = model.CurrentZipCode;
                        _unitOfWork.Profile.UpdatePersonalDetail(updatePersonal);
                        return NoContent();
                    }
                    return NotFound(model.Id);
                }
                return BadRequest(ModelState);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.GetBaseException().Message);
            }            
        }


        [HttpGet("Professional/{id}")]
        [Produces(typeof(IEnumerable<ProfessionalDetail>))]
        public IActionResult GetProfessionalById(string id)
        {
            try
            {
                var employee = _unitOfWork.Employee.Find(m => m.UserId == id).FirstOrDefault();
                if (employee != null)
                {
                    var model = _unitOfWork.Profile.GetProfessionalDetail(employee.Id);
                    var viewModel = new List<ProfessionalDetail>();
                    if (model.Count() > 0)
                    {
                        
                        foreach (var item in model)
                        {
                            viewModel.Add(new ProfessionalDetail
                            {
                                Id = item.Id.ToString(),
                                CompanyName = item.CompanyName,
                                Designation = item.Designation,
                                Doj = item.DOJ.ToString(),
                                Dor = item.DOR.ToString(),
                                EmpID = employee.Id.ToString()

                            });
                        }
                        return Ok(viewModel);
                    }
                    else
                    {
                        return Ok(viewModel);
                    }
                }
                return BadRequest(" Id cannot be null");
            }
            catch (Exception ex)
            {

                return BadRequest(ex.GetBaseException().Message);
            }

        }

        [HttpPut("updateProfessional/{id}")]
        public IActionResult UpdateProfessional(string id, [FromBody]PostProfessionalDetail model)
        {
            try
            {
                var employee = _unitOfWork.Employee.Find(m => m.UserId == id).FirstOrDefault();
                if (ModelState.IsValid)
                {
                    if (model == null)
                        return BadRequest($"{nameof(model)} cannot be null");
                    foreach (var item in model.professional)
                    {
                        if (Convert.ToDateTime(item.Doj) > Convert.ToDateTime(item.Dor))
                            return BadRequest("End date cannot be before start date ");
                       else if(Convert.ToDateTime(item.Dor) > DateTime.Now.Date)
                            return BadRequest("End date cannot be before current date ");
                        else if(Convert.ToDateTime(item.Doj) > DateTime.Now.Date)
                                 return BadRequest("End date cannot be before current date ");
                        if (item.Id == "")
                        {
                            var professional1 = new Professional();
                            professional1.CompanyName = item.CompanyName;
                            professional1.Designation = item.Designation;
                            professional1.DOJ = Convert.ToDateTime(item.Doj);
                            professional1.DOR = Convert.ToDateTime(item.Dor);
                            professional1.EmployeeId = employee.Id;
                            _unitOfWork.Profile.AddProfessionalDetail(professional1);
                        }
                        else
                        {
                            var professionalDetail = _unitOfWork.Profile.GetProfessional(Guid.Parse(item.Id));
                            if (professionalDetail != null)
                            {
                                professionalDetail.Id = new Guid(item.Id);
                                professionalDetail.CompanyName = item.CompanyName;
                                professionalDetail.Designation = item.Designation;
                                professionalDetail.DOJ = Convert.ToDateTime(item.Doj);
                                professionalDetail.DOR = Convert.ToDateTime(item.Dor);
                                professionalDetail.EmployeeId = employee.Id;
                                _unitOfWork.Profile.UpdateProfessionalDetail(professionalDetail);
                            }
                        }
                    }
                    return NoContent();
                }
                return BadRequest(ModelState);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.GetBaseException().Message);
            }
           
        }


        [HttpGet("qualification/{id}")]
        public IActionResult GetQualiDetailById(string id)
        {
            try
            {
                var employeeId = _unitOfWork.Employee.Find(m => m.UserId == id).FirstOrDefault();
                if (employeeId != null)
                {
                    var model = _unitOfWork.Profile.GetQualificationDetail(employeeId.Id);
                    if (model != null)
                    {
                        var qualification = new EducationlDetail();
                        qualification.Id = model.Id;
                        qualification.HigherDegreeInstitue = model.HigherDegreeInstitue;
                        qualification.HighestQualification = model.HighestQualification;
                        qualification.HDSpecialization = model.HDSpecialization;
                        qualification.HDPassingYear = model.HDPassingYear;
                        qualification.HDPercentage = model.HDPercentage;
                        qualification.SecondaryInstitute = model.SecondaryInstitue;
                        qualification.SecondaryQualification = model.SecondaryQualification;
                        qualification.SDSpecialization = model.SDSpecialization;
                        qualification.SDPassingYear = model.SDPassingYear;
                        qualification.SDPercentage = model.SDPercentage;
                        qualification.SSCInstitue = model.SSCInstitue;
                        qualification.SSCQualification = model.SSCQualification;
                        qualification.SSCSpecialization = model.SSCSpecialization;
                        qualification.SSCPassingYear = model.SSCPassingYear;
                        qualification.SSCPercentage = model.SSCPercentage;
                        qualification.EmpID = employeeId.Id;
                        return Ok(qualification);
                    }

                    else
                        return NotFound(id);
                }
                return BadRequest(" Id cannot be null");
            }
            catch (Exception ex)
            {

                return BadRequest(ex.GetBaseException().Message);
            }
          
        }

        [HttpPut("updateQualiDetail/{id}")]
        public IActionResult UpdateQualiDetail(string id, [FromBody]EducationlDetail model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (model == null)
                        return BadRequest($"{nameof(model)} cannot be null");

                    var updatequalification = _unitOfWork.Profile.GetQualification(new Guid(id));
                    if (updatequalification != null)
                    {
                        updatequalification.Id = model.Id;
                        updatequalification.HigherDegreeInstitue = model.HigherDegreeInstitue;
                        updatequalification.HighestQualification = model.HighestQualification;
                        updatequalification.HDSpecialization = model.HDSpecialization;
                        updatequalification.HDPassingYear = model.HDPassingYear;
                        updatequalification.HDPercentage = model.HDPercentage;
                        updatequalification.SecondaryInstitue = model.SecondaryInstitute;
                        updatequalification.SecondaryQualification = model.SecondaryQualification;
                        updatequalification.SDSpecialization = model.SDSpecialization;
                        updatequalification.SDPassingYear = model.SDPassingYear;
                        updatequalification.SDPercentage = model.SDPercentage;
                        updatequalification.SSCInstitue = model.SSCInstitue;
                        updatequalification.SSCQualification = model.SSCQualification;
                        updatequalification.SSCSpecialization = model.SSCSpecialization;
                        updatequalification.SSCPassingYear = model.SSCPassingYear;
                        updatequalification.SSCPercentage = model.SSCPercentage;
                        updatequalification.EmployeeId = model.EmpID;
                        _unitOfWork.Profile.UpdateQualificationDetail(updatequalification);
                        return NoContent();
                    }
                    return NotFound(model.Id);
                }
                return BadRequest(ModelState);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.GetBaseException().Message);
            }

        }

    }
}