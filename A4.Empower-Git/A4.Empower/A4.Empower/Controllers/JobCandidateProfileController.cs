using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using A4.BAL;
using A4.DAL.Entites;
using A4.Empower.Enum;
using A4.Empower.ViewModels;
using DAL;
using DAL.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OpenIddict.Validation.AspNetCore;
using static A4.Empower.Controllers.FileUploadController;

namespace A4.Empower.Controllers
{
    [Authorize(AuthenticationSchemes = OpenIddict.Validation.AspNetCore.OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    public class JobCandidateProfileController : Controller
    {
        readonly ILogger _logger;
        readonly IUnitOfWork _unitOfWork;
        private readonly IAccountManager _accountManager;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public JobCandidateProfileController(ILogger<JobCandidateProfileController> logger, IUnitOfWork unitOfWork, IAccountManager accountManager, IWebHostEnvironment hostingEnvironment)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _accountManager = accountManager;
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

                throw ex;
            }

        }

        [HttpGet("resume/{id}")]
        public IActionResult GetResume(string id)
        {
            try
            {
                var status = new MyReponse();
                status.success = true;
                var candidateProfile = _unitOfWork.JobCandidateProfile.Find(m => m.UserId == (id.ToString())).FirstOrDefault();
                if (candidateProfile != null)
                {
                    status.pictureId = candidateProfile.ResumeId.ToString();
                    if (candidateProfile.ResumeId != null)
                    {
                        var picture = _unitOfWork.FileUpload.GetPictureById(Convert.ToInt32(candidateProfile.ResumeId));
                        status.imageUrl = _unitOfWork.FileUpload.GetPictureUrl(picture, _hostingEnvironment.WebRootPath);
                    }
                }
                return Ok(status);
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }


        #region CandidateProfile

        [HttpGet("list/{levelId}/{id?}/{page?}/{pageSize?}/{name?}")]
        [Produces(typeof(AllCandidateViewModel))]
        public IActionResult GetAllCandidate(string levelId, string id = null, int? page = null, int? pageSize = null, string name = null)
        {
            try
            {
                var result = new AllCandidateViewModel();
                var viewModel = new List<AllCandidateModel>();
                var model = _unitOfWork.JobCandidateProfile.GetAllCandidateProfile(levelId, id, name, Convert.ToInt32(page), Convert.ToInt32(pageSize));
                foreach (var item in model)
                {
                    viewModel.Add(
                        new AllCandidateModel {
                            ApplicationId = item.ApplicationId.ToString(), 
                            ApplicationType = item.ApplicationType,
                            OverAllScore = item.OverAllScore, 
                            ScreeningScore = item.ScreeningScore, 
                            HrScore = item.HRScore, 
                            SkillScore = item.SkillScore, 
                            CandidateId = item.Id.ToString(), 
                            JobStatus = ((JobStatus)(Convert.ToInt32(item.JobStatus))).ToString(), 
                            JobVacancyId = item.JobVacancyId.ToString(), 
                            VacancyName = item.VacancyName,
                            Name = item.Name, Email = item.Email, 
                            PhoneNo = item.PhoneNo, Resume = GetResume(item.ResumeId) ,
                            LevelId=item.LevelId.ToString()
                        });
                }
                result.CandidateModel = viewModel;
                result.InterviewLevelModel = GetLevel(id);
                if (viewModel.Count > 0)
                {
                    result.Position = viewModel[0].VacancyName;
                }
                result.TotalCount = model.TotalCount;
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.GetBaseException().Message);
            }

        }

        [HttpGet("managerList/{id}/{page?}/{pageSize?}/{name?}")]
        [Produces(typeof(AllCandidateViewModel))]
        public IActionResult GetManagerCandidate(string id, int? page = null, int? pageSize = null, string name = null)
        {
            try
            {
                var result = new AllCandidateViewModel();
                var viewModel = new List<AllCandidateModel>();
                var managerId = _unitOfWork.Employee.Find(m => m.UserId == id).FirstOrDefault();
                if (managerId == null) return BadRequest("Manager does not exist");
                var model = _unitOfWork.JobCandidateProfile.GetManagerandidate(managerId.Id, name, Convert.ToInt32(page), Convert.ToInt32(pageSize));
                foreach (var item in model)
                {
                    viewModel.Add(new AllCandidateModel { Resume = GetResume(item.ResumeId), InterviewId = item.InterviewId.ToString(), InterviewDate = item.InterviewDate, InterviewTime = item.InterviewTime, ApplicationId = item.ApplicationId.ToString(), ApplicationType = item.ApplicationType, OverAllScore = item.OverAllScore, ScreeningScore = item.ScreeningScore, HrScore = item.HRScore, SkillScore = item.SkillScore, CandidateId = item.Id.ToString(), JobStatus = ((JobStatus)(Convert.ToInt32(item.JobStatus))).ToString(), JobVacancyId = item.JobVacancyId.ToString(), VacancyName = item.VacancyName, Name = item.Name, Email = item.Email, PhoneNo = item.PhoneNo });
                }
                result.CandidateModel = viewModel;
                result.TotalCount = model.TotalCount;
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.GetBaseException().Message);
            }

        }

        [HttpGet("appliedJob/{id}/{page?}/{pageSize?}/{name?}")]
        [Produces(typeof(CandidateAppliedJobViewModel))]
        public IActionResult CandidateJobList(string id, int? page = null, int? pageSize = null, string name = null)
        {
            try
            {

                var result = new CandidateAppliedJobViewModel();
                var viewModel = new List<CandidateAppliedJobModel>();

                var candidateId = _unitOfWork.JobCandidateProfile.Find(m => m.UserId == id).FirstOrDefault();
                if (candidateId == null)
                    return BadRequest("Candidate is not valid");
                var model = _unitOfWork.JobCandidateProfile.GetCandidateAppliedJob(candidateId.Id, name, Convert.ToInt32(page), Convert.ToInt32(pageSize));
                foreach (var item in model)
                {

                    viewModel.Add(new CandidateAppliedJobModel { ApplicationId = item.ApplicationId.ToString(), AppliedDate = item.AppliedDate, JobType = item.JobType, JobStatus = ((JobStatus)(Convert.ToInt32(item.JobStatus))).ToString(), VacancyName = item.VacancyName });

                }
                result.CandidateAppliedJobModel = viewModel;
                result.TotalCount = model.TotalCount;
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.GetBaseException().Message);
            }

        }


        [HttpGet("candidateProfile/{id}")]
        [Produces(typeof(JobCandidateProfileModel))]
        public IActionResult GetCandidateProfileById(string id)
        {
            try
            {
                if (!string.IsNullOrEmpty(id))
                {
                    var model = _unitOfWork.JobCandidateProfile.GetJobCandidateProfile(id);
                    if (model != null)
                    {
                        var viewModel = new JobCandidateProfileModel();
                        viewModel.Id = model.Id.ToString();
                        viewModel.UserId = model.UserId;
                        viewModel.City = model.City;
                        viewModel.Country = model.Country;
                        viewModel.CurrentAddress = model.CurrentAddress;
                        viewModel.DOB = model.DOB;
                        viewModel.FatherName = model.FatherName;
                        viewModel.Gender = model.Gender;
                        viewModel.IdProofDetail = model.IdProofDetail;
                        viewModel.MaritalStatus = model.MaritalStatus;
                        viewModel.MotherName = model.MotherName;
                        viewModel.Nationality = model.Nationality;
                        viewModel.OfficialContactNo = model.OfficialContactNo;
                        viewModel.PermanentAddress = model.PermanentAddress;
                        viewModel.SkillSet = model.SkillSet;
                        viewModel.State = model.State;
                        viewModel.ZipCode = model.ZipCode;

                        return Ok(viewModel);
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

        [HttpPut("updateCandidateProfile/{id}")]
        public IActionResult UpdateCandidateProfile(string id, [FromBody]JobCandidateProfileModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (model == null)
                        return BadRequest($"{nameof(model)} cannot be null");
                    if (string.IsNullOrEmpty(id))
                        return BadRequest("id property cannot be null");
                    var checkexist = _unitOfWork.JobCandidateProfile.GetCandidateProfile(new Guid(id));
                    if (checkexist != null)
                    {
                        checkexist.Id = new Guid(model.Id);
                        checkexist.UserId = model.UserId;
                        checkexist.Gender = model.Gender;
                        checkexist.FatherName = model.FatherName;
                        checkexist.MotherName = model.MotherName;
                        checkexist.CurrentAddress = model.CurrentAddress;
                        checkexist.PermanentAddress = model.PermanentAddress;
                        checkexist.MaritalStatus = model.MaritalStatus;
                        checkexist.DOB = model.DOB;
                        checkexist.IdProofDetail = model.IdProofDetail;
                        checkexist.City = model.City;
                        checkexist.State = model.State;
                        checkexist.Country = model.Country;
                        checkexist.ZipCode = model.ZipCode;
                        checkexist.Nationality = model.Nationality;
                        checkexist.OfficialContactNo = model.OfficialContactNo;
                        _unitOfWork.JobCandidateProfile.UpdateCandidateProfile(checkexist);
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

        [HttpDelete("deleteCandidateProfile/{id}")]
        public IActionResult DeleteCandidateProfile(string id)
        {
            try
            {
                if (!string.IsNullOrEmpty(id))
                {
                    var checkexist = _unitOfWork.JobCandidateProfile.Get(new Guid(id));
                    if (checkexist != null)
                    {
                        _unitOfWork.JobCandidateProfile.RemoveCandidateProfile(checkexist);
                        return Ok();
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

        #endregion

        #region Qualification

        [HttpGet("jobQulification/{id}")]
        public IActionResult GetByJobQulificationId(string id)
        {
            try
            {
                var profileId = _unitOfWork.JobCandidateProfile.Find(m => m.UserId == id).FirstOrDefault();
                if (profileId != null)
                {
                    var qualification = _unitOfWork.JobCandidateProfile.GetJobQualification(profileId.Id);
                    if (qualification != null)
                    {
                        var viewmodel = new JobQualificationModel();
                        viewmodel.Id = qualification.Id.ToString();
                        viewmodel.ProfileId = profileId.Id.ToString();
                        viewmodel.HDPassingYear = qualification.HDPassingYear;
                        viewmodel.HDPercentage = qualification.HDPercentage;
                        viewmodel.HDSpecialization = qualification.HDSpecialization;
                        viewmodel.HigherDegreeInstitue = qualification.HigherDegreeInstitue;
                        viewmodel.HighestQualification = qualification.HighestQualification;
                        viewmodel.ProfileId = qualification.JobCandidateProfilesId.ToString();
                        viewmodel.SDPassingYear = qualification.SDPassingYear;
                        viewmodel.SDPercentage = qualification.SDPercentage;
                        viewmodel.SDSpecialization = qualification.SDSpecialization;
                        viewmodel.SecondaryQualification = qualification.SecondaryQualification;
                        viewmodel.SecondaryInstitue = qualification.SecondaryInstitue;
                        viewmodel.SSCPassingYear = qualification.SSCPassingYear;
                        viewmodel.SSCPercentage = qualification.SSCPercentage;
                        viewmodel.SSCSpecialization = qualification.SSCSpecialization;
                        viewmodel.SSCInstitue = qualification.SSCInstitue;
                        viewmodel.SSCQualification = qualification.SSCQualification;
                        return Ok(viewmodel);
                    }
                }
                return BadRequest("id cannot be null");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.GetBaseException().Message);
            }
        }

        [HttpPut("updateQualification/{id}")]
        public IActionResult UpdateQualification(string id, [FromBody]JobQualificationModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (model == null)
                        return BadRequest($"{nameof(model)}cannot be null");
                    if (id != null)
                    {
                        var qualification = _unitOfWork.JobCandidateProfile.GetQualification(new Guid(id));
                        if (qualification != null)
                        {
                            qualification.Id = new Guid(model.Id);
                            qualification.HDPassingYear = model.HDPassingYear;
                            qualification.HDPercentage = model.HDPercentage;
                            qualification.HDSpecialization = model.HDSpecialization;
                            qualification.HigherDegreeInstitue = model.HigherDegreeInstitue;
                            qualification.HighestQualification = model.HighestQualification;
                            qualification.JobCandidateProfilesId = new Guid(model.ProfileId);
                            qualification.SDPassingYear = model.SDPassingYear;
                            qualification.SDPercentage = model.SDPercentage;
                            qualification.SDSpecialization = model.SDSpecialization;
                            qualification.SecondaryQualification = model.SecondaryQualification;
                            qualification.SecondaryInstitue = model.SecondaryInstitue;
                            qualification.SSCPassingYear = model.SSCPassingYear;
                            qualification.SSCPercentage = model.SSCPercentage;
                            qualification.SSCSpecialization = model.SSCSpecialization;
                            qualification.SSCInstitue = model.SSCInstitue;
                            qualification.SSCQualification = model.SSCQualification;
                            _unitOfWork.JobCandidateProfile.UpdateQualification(qualification);
                            return NoContent();
                        }
                    }
                    else
                    {
                        return NotFound(model.Id);
                    }
                }
                return BadRequest(ModelState);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.GetBaseException().Message);
            }
        }

        [HttpDelete("deleteQualification/{id}")]
        public IActionResult DeleteQualification(string id)
        {
            try
            {
                if (!string.IsNullOrEmpty(id))
                {
                    var qualification = _unitOfWork.JobCandidateProfile.GetQualification(new Guid(id));
                    if (qualification != null)
                    {
                        _unitOfWork.JobCandidateProfile.RemoveQualification(qualification);
                        return Ok();
                    }
                    else
                    {
                        return NotFound(id);
                    }
                }
                return BadRequest("id cannot be null");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.GetBaseException().Message);
            }

        }

        #endregion

        #region WorkExperience


        [HttpGet("jobWorkExperience/{id}")]
        [Produces(typeof(IEnumerable<JobWorkExperienceModel>))]
        public IActionResult GetjobWorkExperience(string id)
        {
            try
            {
                var candidateProfile = _unitOfWork.JobCandidateProfile.Find(m => m.UserId == id).FirstOrDefault();
                if (candidateProfile != null)
                {
                    var model = _unitOfWork.JobCandidateProfile.GetJobWorkExperience(candidateProfile.Id);
                    var viewModel = new List<JobWorkExperienceModel>();
                    if (model.Count() > 0)
                    {

                        foreach (var item in model)
                        {
                            viewModel.Add(new JobWorkExperienceModel
                            {
                                Id = item.Id.ToString(),
                                CompanyName = item.CompanyName,
                                Designation = item.Designation,
                                Doj = item.DOJ.ToString(),
                                Dor = item.DOR.ToString(),
                            });
                        }
                        return Ok(viewModel);
                    }
                    return Ok(viewModel);
                }
                else
                {
                    return BadRequest(" model is empty");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.GetBaseException().Message);
            }
        }


        [HttpPut("updateWorkExperience/{id}")]
        public IActionResult UpdateWorkExperience(string id, [FromBody]PostJobWorkExperienceModel model)
        {
            try
            {
                var candidate = _unitOfWork.JobCandidateProfile.Find(m => m.UserId == id).FirstOrDefault();
                if (ModelState.IsValid)
                {
                    if (model == null)
                        return BadRequest($"{nameof(model)} cannot be null");
                    foreach (var item in model.professional)
                    {
                        if (Convert.ToDateTime(item.Doj) > Convert.ToDateTime(item.Dor))
                            return BadRequest("End date cannot be before start date ");
                        else if (Convert.ToDateTime(item.Dor) > DateTime.Now.Date)
                            return BadRequest("End date cannot be before current date ");
                        else if (Convert.ToDateTime(item.Doj) > DateTime.Now.Date)
                            return BadRequest("End date cannot be before current date ");
                        if (item.Id == "")
                        {
                            var professional1 = new JobCandidateWorkExperience();
                            professional1.CompanyName = item.CompanyName;
                            professional1.Designation = item.Designation;
                            professional1.DOJ = Convert.ToDateTime(item.Doj);
                            professional1.DOR = Convert.ToDateTime(item.Dor);
                            professional1.JobCandidateProfilesId = candidate.Id;
                            _unitOfWork.JobCandidateProfile.CreateWorkExperience(professional1);
                        }
                        else
                        {
                            var professionalDetail = _unitOfWork.JobCandidateProfile.GetWorkExperience(Guid.Parse(item.Id));
                            if (professionalDetail != null)
                            {
                                professionalDetail.Id = new Guid(item.Id);
                                professionalDetail.CompanyName = item.CompanyName;
                                professionalDetail.Designation = item.Designation;
                                professionalDetail.DOJ = Convert.ToDateTime(item.Doj);
                                professionalDetail.DOR = Convert.ToDateTime(item.Dor);
                                professionalDetail.JobCandidateProfilesId = candidate.Id;
                                _unitOfWork.JobCandidateProfile.UpdateWorkExperience(professionalDetail);
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

        [HttpDelete("deleteWorkExperience/{id}")]
        public IActionResult DeleteWorkExperience(string id)
        {
            try
            {
                if (!string.IsNullOrEmpty(id))
                {
                    var checkexist = _unitOfWork.JobCandidateProfile.GetWorkExperience(new Guid(id));
                    if (checkexist != null)
                    {
                        _unitOfWork.JobCandidateProfile.RemoveWorkExperience(checkexist);
                        return Ok();
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

        #endregion

        private List<InterviewLevelModel> GetLevel(string vacancyId)
        {
            var lstLevel = new List<InterviewLevelModel>();
            Guid guidVacancy= new Guid();
            if (!string.IsNullOrEmpty(vacancyId) && vacancyId != "null")
            {
                guidVacancy = Guid.Parse(vacancyId);
            }
            var vacancyLevels = _unitOfWork.JobVacancyLevel.GetAll().Where(x => x.JobVacancyId == guidVacancy).ToList().OrderBy(o=>o.Level);
            if (vacancyLevels.Count() > 0)
            {
                foreach (var item in vacancyLevels)
                {
                    lstLevel.Add(new InterviewLevelModel { Name = item.Name, Id = item.Id.ToString() });
                }
            }
            return lstLevel;
        }

        private string GetResume(int? resumeId)
        {
            string resume = "";
            if (resumeId != null)
            {
                var picture = _unitOfWork.FileUpload.GetPictureById(Convert.ToInt32(resumeId));
                resume = _unitOfWork.FileUpload.GetPictureUrl(picture, _hostingEnvironment.WebRootPath);
            }
            return resume;
        }
    }
}