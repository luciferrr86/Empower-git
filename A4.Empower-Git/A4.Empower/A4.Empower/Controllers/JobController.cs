using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using A4.BAL;
using A4.DAL.Entites;
using A4.Empower.Enum;
using A4.Empower.Helpers;
using A4.Empower.ViewModels;
using DAL;
using DAL.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace A4.Empower.Controllers
{

    [Route("api/[controller]")]
    public class JobController : Controller
    {
        readonly ILogger _logger;
        readonly IUnitOfWork _unitOfWork;
        readonly IEmailer _emailer;
        private readonly IAccountManager _accountManager;
        private readonly IConfiguration _configuration;
        public JobController(ILogger<JobController> logger, IUnitOfWork unitOfWork, IEmailer emailer, IAccountManager accountManager, IConfiguration configuration)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _emailer = emailer;
            _accountManager = accountManager;
            _configuration = configuration;
        }

        [HttpPost("candidateRegister")]
        public async Task<IActionResult> CandidateRegister([FromBody] CandidateViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var appUser = new ApplicationUser();
                    appUser.Email = model.Email;
                    appUser.FullName = model.FullName;
                    appUser.PhoneNumber = model.PhoneNumber;
                    appUser.UserName = model.Email;
                    appUser.EmailConfirmed = true;
                    appUser.IsEnabled = true;

                    var role = await _accountManager.GetRoleLoadRelatedAsync("candidate");
                    if (role == null)
                    {
                        ApplicationRole appRole = new ApplicationRole();
                        appRole.Name = "candidate";
                        List<PermissionViewModel> permission = new List<PermissionViewModel>();
                        permission.Add(new PermissionViewModel { GroupName = "Candidate", Name = "Candidate", Description = "ABCDOK", Value = "candidate.manage" });
                        var resultRole = await _accountManager.CreateRoleAsync(appRole, permission?.Select(p => p.Value).ToArray());
                        if (resultRole.Item1)
                        {
                            string[] roles = new string[] { appRole.Name };
                            model.Roles = roles;
                        }
                    }
                    else
                    {
                        string[] roles = new string[] { "candidate" };
                        model.Roles = roles;
                    }

                    var result = await _accountManager.CreateUserAsync(appUser, model.Roles, model.Password);

                    await _accountManager.AssignRolesToUser(appUser.Id, model.Roles);

                    if (result.Item1)
                    {
                        JobCandidateProfile personal = new JobCandidateProfile
                        {
                            UserId = appUser.Id
                        };
                        _unitOfWork.JobCandidateProfile.CreateCandidateProfile(personal);

                        JobCandidateQualification qualification = new JobCandidateQualification
                        {
                            JobCandidateProfilesId = personal.Id
                        };
                        _unitOfWork.JobCandidateProfile.CreateQualification(qualification);
                        var message = AccountTemplates.GetCandidateEmail(model.FullName, model.Email, model.Password);
                        (bool success, string errorMsg) response = await _emailer.SendEmailAsync(model.FullName, model.Email, "Candidate Registration", message);
                        return NoContent();
                    }

                    else
                    {
                        return BadRequest(result.Item2);
                    }
                }
                return BadRequest(ModelState);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [HttpGet("jobList")]
        public IActionResult GetPublishJobList()
        {
            var jobPublishedList = _unitOfWork.Job.GetAllJobPublishedList();
            var viewModel = new List<JobPublishedViewModel>();
            if (jobPublishedList.Count > 0)
            {
                foreach (var item in jobPublishedList)
                {
                    var jobType = _unitOfWork.JobType.Get(item.JobTypeId);
                    if (jobType != null)
                    {
                        viewModel.Add(new JobPublishedViewModel { JobId = item.Id.ToString(), JobTitle = item.JobTitle, Location = item.JobLocation, PublishedDate = item.PublishedDate, JobType = jobType.Name });
                    }
                    else
                    {
                        return NotFound();
                    }

                }
                return Ok(viewModel);
            }
            else
            {
                return BadRequest("model is empty");
            }
        }

        [HttpGet("details/{id}")]
        public IActionResult GetJobDetails(string id)
        {
            if (id != null)
            {
                var detail = _unitOfWork.JobVacancy.Get(new Guid(id));
                if (detail != null)
                {
                    var jobType = _unitOfWork.JobType.Get(detail.JobTypeId);
                    if (jobType != null)
                    {
                        var viewModel = new JobDetailsViewModel()
                        {
                            JobVacancyId = detail.Id.ToString(),
                            JobTitle = detail.JobTitle,
                            Experience = detail.Experience,
                            NoOfvacancies = detail.NoOfvacancies,
                            JobTypeId = detail.JobTypeId.ToString(),
                            JobType = jobType.Name,
                            JobLocation = detail.JobLocation,
                            SalaryRange = detail.SalaryRange,
                            PublishedDate = detail.PublishedDate,
                            JobDescription = detail.JobDescription,
                            JobRequirements = detail.JobRequirements
                        };
                        return Ok(viewModel);
                    }
                    return NotFound(detail.JobTypeId);

                }
                else
                {
                    return NotFound(id);
                }
            }
            return BadRequest("id cannot be null");
        }

        [HttpGet("applicationData/{id}")]
        public IActionResult GetApplicationData(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                var viewModel = new ApplicationFormViewModel();
                var vacancy = _unitOfWork.JobVacancy.Get(new Guid(id));
                if (vacancy != null)
                {
                    var type = _unitOfWork.JobType.Get(vacancy.JobTypeId);
                    if (type != null)
                    {
                        viewModel.JobTitle = vacancy.JobTitle;
                        viewModel.JobLocation = vacancy.JobLocation;
                        viewModel.PublishedDate = vacancy.PublishedDate;
                        viewModel.JobType = type.Name;

                    }
                    viewModel.QuestionListModel = GetScreeningQuestionList(vacancy.Id);
                    return Ok(viewModel);
                }
                else
                {
                    return NotFound(id);
                }
            }
            return BadRequest("id cannot be null");
        }

        private List<JobScreeningQuestions> GetScreeningQuestionList(Guid jobVacancyId)
        {
            var screeingQuestionList = new List<JobScreeningQuestions>();
            var result = _unitOfWork.JobScreeningQuestion.Find(m => m.JobVacancyId == jobVacancyId).ToList();
            if (result.Count > 0)
            {
                foreach (var item in result)
                {
                    screeingQuestionList.Add(new JobScreeningQuestions { Id = item.Id.ToString(), ControlType = item.ControlType, Question = item.Questions, Option1 = item.Option1, Option2 = item.Option2, Option3 = item.Option3, Option4 = item.Option4, Mandatory = item.bIsRequired, Weightage = item.Weightage });
                }
            }
            return screeingQuestionList;
        }

        [HttpPost("saveApplication")]
        public async Task<IActionResult> SaveApplication([FromBody] ApplicationQuestionModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    if (model == null)
                        return BadRequest($"{nameof(model)} cannot be null");

                    var application = new JobApplication();
                    int score = 0;
                    string positionApplied = "Position";
                    application.ApplicationType = "Online";
                    var candidate = _unitOfWork.JobCandidateProfile.GetJobCandidateProfile(model.CandidateId);
                    if (candidate != null)
                    {
                        var applied = _unitOfWork.JobApplication.Find(m => m.JobCandidateProfileId == candidate.Id && m.JobVacancyId == Guid.Parse(model.JobVacancyId)).FirstOrDefault();
                        if (applied != null)
                            return BadRequest("You have already applied for Job please check status.");

                        application.JobCandidateProfileId = candidate.Id;
                    }
                    application.JobStatus = Convert.ToInt32(JobStatus.Applied);
                    application.JobVacancyId = Guid.Parse(model.JobVacancyId);
                    application.HRComment = "";
                    application.Feedback = "";
                    application.HRScore = "0";
                    application.SkillScore = "0";
                    application.OverallScore = "0";

                    var jobVacancy = _unitOfWork.JobVacancy.Find(x => x.Id == Guid.Parse(model.JobVacancyId)).FirstOrDefault();
                    if (jobVacancy != null)
                    {
                        positionApplied = jobVacancy.JobTitle;
                    }
                    var answer = new List<JobApplicationScreeningQuestion>();
                    int totalQuestion= model.questions.Count();
                    foreach (var item in model.questions)
                    {

                        int ObtainedWeightage = 0;
                        var questions = _unitOfWork.JobScreeningQuestion.Get(new Guid(item.Id));
                        
                        if (questions != null)
                        {

                            var value = new List<string>();
                            if (questions.ChkOption1)
                            {
                                value.Add(questions.Option1);
                            }
                            if (questions.ChkOption2)
                            {
                                value.Add(questions.Option2);
                            }
                            if (questions.ChkOption3)
                            {
                                value.Add(questions.Option3);
                            }
                            if (questions.ChkOption4)
                            {
                                value.Add(questions.Option4);
                            }

                            if (CompareLists2(item.Option, value))
                            {
                                ObtainedWeightage = questions.Weightage;
                            }
                            score = score + Convert.ToInt32(ObtainedWeightage);
                            answer.Add(new JobApplicationScreeningQuestion { JobApplicationId = application.Id, JobScreeningQuestionId = questions.Id, Weightage = questions.Weightage.ToString(), ObtainedWeightage = ObtainedWeightage.ToString() });
                        }


                    }
                    if (totalQuestion != 0)
                    {
                        application.ScreeningScore = score / totalQuestion != 0 ? score.ToString() : "0";
                    }
                    _unitOfWork.JobApplication.Add(application);
                    _unitOfWork.JobApplicationScreeningQuestion.AddRange(answer);
                    _unitOfWork.SaveChanges();
                    UpdateOverAllScoreStatus(application);
                    string hrName = _configuration.GetValue<string>("HrDetail:Name");
                    string hrEmail = _configuration.GetValue<string>("HrDetail:Email");
                    string message = RecruitmentTemplates.GetHrJobApplicationEmail(candidate.ApplicationUser.FullName, hrName, positionApplied, DateTime.Now.ToShortDateString(), candidate.ApplicationUser.Email, candidate.ApplicationUser.PhoneNumber);
                    (bool success, string errorMsg) response1 = await _emailer.SendEmailAsync(candidate.ApplicationUser.FullName, hrEmail, "Job Application", message);
                    return NoContent();

                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Error In JobController -SaveApplication () -", ex.Message.ToString());
                throw;
            }
            return BadRequest(ModelState);
        }

        private bool CompareLists2(List<string> list1, List<string> list2)
        {
            if (list1.Count != list2.Count)
                return false;

            for (int i = 0; i < list1.Count; i++)
            {
                if (list1[i] != list2[i])
                    return false;
            }

            return true;
        }

        private void UpdateOverAllScoreStatus(JobApplication jobApplication)
        {
            var overAllScore = 0.0;
            if (jobApplication != null)
            {
                if (jobApplication.ScreeningScore != null && jobApplication.ScreeningScore != "0" ||
                jobApplication.HRScore != null && jobApplication.HRScore != "0" ||
                jobApplication.SkillScore != null && jobApplication.SkillScore != "0")
                {
                    overAllScore = Convert.ToDouble(jobApplication.ScreeningScore) + Convert.ToDouble(jobApplication.HRScore) + Convert.ToDouble(jobApplication.SkillScore);
                    overAllScore = Math.Round(overAllScore/3, 1);
                }
                jobApplication.OverallScore = overAllScore.ToString();
                _unitOfWork.JobApplication.Update(jobApplication);
                _unitOfWork.SaveChanges();
            }

        }
    }
}