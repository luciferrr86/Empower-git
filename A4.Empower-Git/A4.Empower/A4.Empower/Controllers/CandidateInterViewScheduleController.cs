using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using A4.BAL;
using A4.DAL.Entites;
using A4.Empower.Enum;
using A4.Empower.Helpers;
using DAL;
using DAL.Core.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using static A4.BAL.CandidateApplicationViewModel;

namespace A4.Empower.Controllers
{
    [Route("api/CandidateInterViewSchedule")]
    public class CandidateInterViewScheduleController : Controller
    {
        readonly IUnitOfWork _unitOfWork;
        readonly IEmailer _emailer;
        readonly ILogger _logger;
        private readonly IAccountManager _accountManager;
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _hostingEnvironment;
        public CandidateInterViewScheduleController(ILogger<CandidateInterViewScheduleController> logger, IUnitOfWork unitOfWork, IEmailer emailer, IAccountManager accountManager, IConfiguration configuration, IWebHostEnvironment hostingEnvironment)
        {
            _unitOfWork = unitOfWork;
            _emailer = emailer;
            _accountManager = accountManager;
            _configuration = configuration;
            _hostingEnvironment = hostingEnvironment;
            _logger = logger;
        }

        [HttpGet("applicationDetail/{id}")]
        [Produces(typeof(CandidateApplicationViewModel))]
        public IActionResult GetApplicationDetail(string id)
        {
            try
            {
                if (!String.IsNullOrEmpty(id))
                {
                    var model = new CandidateApplicationViewModel();
                    model.JobInformationModel = GetJobInfo(Guid.Parse(id));
                    model.QuestionAnswerModel = GetQuestionAnswer(Guid.Parse(id));
                    model.InterviewTypeList = GetInterviewType();
                    model.ManagerList = GetManagerList();
                    model.CandidateInterView = GetCandidateDetail(Guid.Parse(id));

                    return Ok(model);
                }
                else
                {
                    return BadRequest(" model is empty");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Error In CandidateInterViewScheduleController - GetApplicationDetail() -", ex.Message.ToString());
                return BadRequest(ex.GetBaseException().Message);

            }
        }

        [HttpGet("interviewdetail/{id}")]
        [Produces(typeof(InterviewScheduleViewModel))]
        public IActionResult GetIterviewDetail(string id)
        {
            try
            {
                if (!String.IsNullOrEmpty(id))
                {
                    var model = new InterviewScheduleViewModel();
                    model.InterviewScheduleLevelList = GetInterviewScheduleList(Guid.Parse(id));
                    model.InterviewTypeList = GetInterviewType();
                    model.ManagerList = GetManagerList();
                    return Ok(model);
                }
                else
                {
                    return BadRequest(" model is empty");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Error In CandidateInterViewScheduleController - SaveTimeSchedule() -", ex.Message.ToString());
                return BadRequest(ex.GetBaseException().Message);
            }
        }

        [HttpPost("saveTimeSchedule")]
        public async Task<IActionResult> SaveTimeSchedule([FromBody] CandidateInterviewScheduleModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (model == null)
                        return BadRequest($"{nameof(model)} cannot be null");

                    if (model.Id == null)
                    {
                        var candidate = _unitOfWork.JobCandidateInterview.GetCandidate(Guid.Parse(model.JobApplicationId));
                        var jobTitle = _unitOfWork.JobCandidateInterview.GetJobInformation(Guid.Parse(model.JobApplicationId));
                        foreach (var item in model.ManagerIdList)
                        {
                            var interviewSchedule = new JobCandidateInterview();
                            interviewSchedule.Date = model.Date;
                            interviewSchedule.Time = model.Time;
                            interviewSchedule.IsInterviewCompleted = false;
                            interviewSchedule.InterviewMode = "";
                            interviewSchedule.JobApplicationId = Guid.Parse(model.JobApplicationId);
                            interviewSchedule.JobVacancyLevelManagerId = GetManagerLevelId(Guid.Parse(item), Guid.Parse(model.LevelId));
                            interviewSchedule.JobInterviewTypeId = Guid.Parse(model.JobInterviewTypeId);
                            _unitOfWork.JobCandidateInterview.Add(interviewSchedule);

                            string mgrId = "";
                            var manager = _unitOfWork.Employee.Find(m => m.Id == Guid.Parse(item)).FirstOrDefault();
                            if (manager != null)
                            {
                                mgrId = manager.UserId.ToString();
                                var user = await _accountManager.GetUserByIdAsync(mgrId);
                                string message = RecruitmentTemplates.ManagerInterviewSchedule(user.FullName, candidate.FullName, jobTitle.JobVacancy.JobTitle, model.Date.ToShortDateString(), model.Time, model.jobCandidateURL);
                                (bool success, string errorMsg) response = await _emailer.SendEmailAsync(user.FullName, user.Email, "Interview Schedule", message);
                            }

                            string message1 = RecruitmentTemplates.CandidateInterviewSchedule(candidate.FullName, jobTitle.JobVacancy.JobTitle, model.Date.ToString(), model.Time);
                            (bool success, string errorMsg) response1 = await _emailer.SendEmailAsync(candidate.FullName, candidate.Email, "Call Letter For Interview", message1);


                        }
                        UpdateApplicationStatus(Guid.Parse(model.JobApplicationId), Convert.ToInt32(JobStatus.Scheduled), "");

                    }
                    else
                    {
                        var interviewSchedule = _unitOfWork.JobCandidateInterview.Get(Guid.Parse(model.Id));
                        if (interviewSchedule != null)
                        {
                            interviewSchedule.Date = model.Date;
                            interviewSchedule.Time = model.Time;
                            interviewSchedule.IsInterviewCompleted = false;
                            interviewSchedule.InterviewMode = "";
                            interviewSchedule.JobInterviewTypeId = Guid.Parse(model.JobInterviewTypeId);
                            _unitOfWork.JobCandidateInterview.Update(interviewSchedule);
                            _unitOfWork.SaveChanges();

                            var levelManager = _unitOfWork.JobVacancyLevelManager.Find(x => x.Id == interviewSchedule.JobVacancyLevelManagerId).FirstOrDefault();
                            if (levelManager != null)
                            {
                                string mgrId = "";
                                var manager = _unitOfWork.Employee.Find(m => m.Id == levelManager.EmployeeId).FirstOrDefault();
                                if (manager != null)
                                {
                                    mgrId = manager.UserId.ToString();
                                    var user = await _accountManager.GetUserByIdAsync(mgrId);
                                    var candidate = _unitOfWork.JobCandidateInterview.GetCandidate(interviewSchedule.JobApplicationId);
                                    if (candidate != null)
                                    {
                                        var jobTitle = _unitOfWork.JobCandidateInterview.GetJobInformation(Guid.Parse(model.JobApplicationId));
                                        string message = RecruitmentTemplates.ManagerInterviewSchedule(user.FullName, candidate.FullName, jobTitle.JobVacancy.JobTitle, model.Date.ToShortDateString(), model.Time);
                                        (bool success, string errorMsg) response = await _emailer.SendEmailAsync(user.FullName, user.Email, "Interview Schedule", message);

                                        string message1 = RecruitmentTemplates.CandidateInterviewSchedule(candidate.FullName, jobTitle.JobVacancy.JobTitle, model.Date.ToString(), model.Time);
                                        (bool success, string errorMsg) response1 = await _emailer.SendEmailAsync(candidate.FullName, candidate.Email, "Call Letter For Interview", message1);

                                    }
                                }
                            }

                        }
                        else
                        {
                            return BadRequest("No detail found , Please delete and create new.");
                        }
                    }
                    return NoContent();
                }
                return BadRequest(ModelState);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error In CandidateInterViewScheduleController - SaveTimeSchedule() -", ex.Message.ToString());

                return BadRequest(ex.GetBaseException().Message);
            }
        }

        [HttpGet("delete/{id}")]
        public IActionResult Delete(string id)
        {
            try
            {
                if (!string.IsNullOrEmpty(id))
                {

                    var checkexist = _unitOfWork.JobCandidateInterview.Get(new Guid(id));
                    if (checkexist != null)
                    {
                        _unitOfWork.JobCandidateInterview.Remove(checkexist);
                        UpdateApplicationStatus(checkexist.JobApplicationId, Convert.ToInt32(1), "");
                        return NoContent();
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
                _logger.LogError("Error In CandidateInterViewScheduleController - Delete() -", ex.Message.ToString());

                return BadRequest(ex.GetBaseException().Message);
            }
        }

        [HttpGet("interviewfeedback/{id}")]
        [Produces(typeof(CandidateInterViewModel))]
        public IActionResult GetInterviewFeedback(string id)
        {
            try
            {
                if (!String.IsNullOrEmpty(id))
                {
                    var model = new CandidateInterViewModel();
                    var interviewInfo = _unitOfWork.JobCandidateInterview.Get(Guid.Parse(id));
                    if (interviewInfo != null)
                    {
                        model.Comment = interviewInfo.InterviewerComment;
                        model.QuestionAnswerList = GetSkillKpiManager(Guid.Parse(id));
                    }

                    return Ok(model);
                }
                else
                {
                    return BadRequest(" model is empty");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Error In CandidateInterViewScheduleController - GetInterviewFeedback() -", ex.Message.ToString());
                return BadRequest(ex.GetBaseException().Message);
            }
        }

        [HttpGet("interviewcandidateDetail/{id}")]
        [Produces(typeof(CandidateInterViewModel))]
        public IActionResult GetInterviewDetail(string id)
        {
            try
            {
                if (!String.IsNullOrEmpty(id))
                {
                    var model = new CandidateInterViewModel();
                    var candidateInfo = _unitOfWork.JobCandidateProfile.GetCandidateJobInfo(Guid.Parse(id));
                    if (candidateInfo != null)
                    {
                        model.CandidateName = candidateInfo.Name;
                        model.Email = candidateInfo.Email;
                        model.Mobile = candidateInfo.PhoneNo;
                        model.AppliedFor = candidateInfo.VacancyName;
                        model.Score = candidateInfo.ScreeningScore;
                        model.QuestionAnswerList = GetSkillKpiManager(Guid.Parse(id));
                        model.JobApplicationId = candidateInfo.ApplicationId.ToString();
                    }

                    return Ok(model);
                }
                else
                {
                    return BadRequest(" model is empty");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Error In CandidateInterViewScheduleController - GetInterviewDetail() -", ex.Message.ToString());
                return BadRequest(ex.GetBaseException().Message);
            }
        }

        [HttpPost("saveskillkpi")]
        public async Task<IActionResult> SaveSkillKPI([FromBody] CandidateInterViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (model == null)
                        return BadRequest($"{nameof(model)} cannot be null");
                    var answer = new List<JobApplicationSkillQuestion>();
                    var interview = _unitOfWork.JobCandidateInterview.Get(Guid.Parse(model.Id));
                    if (interview != null)
                    {
                        interview.InterviewerComment = model.Comment;
                        interview.IsInterviewCompleted = true;
                        interview.IsLevelCompleted = true;
                        _unitOfWork.JobCandidateInterview.Update(interview);
                    }

                    foreach (var item in model.QuestionAnswerList)
                    {
                        if (item.JobQuestionId != null)
                        {
                            answer.Add(new JobApplicationSkillQuestion { JobVacancyLevelSkillQuestionId = Guid.Parse(item.LevelSkillQuestionId), JobCandidateInterviewId = interview.Id, ObtainedWeightage = item.ObtainedWeightage.ToString(), Weightage = item.Weightage });
                        }
                    }
                    _unitOfWork.JobApplicationSkillQuestion.AddRange(answer);
                    _unitOfWork.SaveChanges();
                    if (interview != null)
                    {
                        UpdateSkillKpiScoreStatus(interview.JobApplicationId);
                    }

                    string hrName = _configuration.GetValue<string>("HrDetail:Name");
                    string hrEmail = _configuration.GetValue<string>("HrDetail:Email");
                    var candidateName = _unitOfWork.JobCandidateInterview.GetCandidate(interview.JobApplicationId);
                    string message = RecruitmentTemplates.GetManagerFeedbackToHrEmail(candidateName == null ? "" : candidateName.FullName, hrName);
                    (bool success, string errorMsg) response1 = await _emailer.SendEmailAsync(hrName, hrEmail, "Manager Interview feedback", message);
                    return NoContent();
                }
                return BadRequest(ModelState);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error In CandidateInterViewScheduleController - SaveSkillKPI() -", ex.Message.ToString());
                return BadRequest(ex.GetBaseException().Message);
            }
        }

        [HttpPost("savehrkpi")]
        public IActionResult SaveSkillHrKPI([FromBody] List<HRKpiModel> questionAnswerList)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (questionAnswerList == null)
                        return BadRequest($"{nameof(questionAnswerList)} cannot be null");
                    var hrQuestionList = new List<JobApplicationHRQuestions>();
                    string jobAppId = "";
                    foreach (var item in questionAnswerList)
                    {
                        hrQuestionList.Add(new JobApplicationHRQuestions { JobApplicationId = new Guid(item.JobApplicationId), JobHRQuestionId = new Guid(item.JobHRQuestionId), ObtainedWeightage = item.ObtainedWeightage.ToString(), Weightage = item.Weightage.ToString() });
                        jobAppId = item.JobApplicationId;

                    }
                    _unitOfWork.JobApplicationHRQuestions.AddRange(hrQuestionList);
                    _unitOfWork.SaveChanges();
                    UpdateApplicationStatus(Guid.Parse(jobAppId), Convert.ToInt32(JobStatus.Shortlisted), "");
                    UpdateHrKpiScoreStatus(Guid.Parse(jobAppId));
                    return NoContent();
                }
                return BadRequest(ModelState);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error In CandidateInterViewScheduleController - SaveSkillHrKPI() -", ex.Message.ToString());
                return BadRequest(ex.GetBaseException().Message);
            }
        }

        [HttpGet("completelevel/{id}/{levelId}/{statusId}")]
        public async Task<IActionResult> CompleteInterviewLevelAsync(string id, string levelId, string statusId)
        {
            try
            {
                if (!String.IsNullOrEmpty(id))
                {
                    var vaccancy = _unitOfWork.JobApplication.Get(Guid.Parse(id));
                    var vaccancyLevel = _unitOfWork.JobVacancyLevel.Get(Guid.Parse(levelId));

                    if (vaccancyLevel != null)
                    {
                        var vaccancymanager = _unitOfWork.JobVacancyLevelManager.Find(m => m.JobVacancyLevelId == vaccancyLevel.Id).ToList();
                        var anyInterviewSchduled = _unitOfWork.JobCandidateInterview.GetAll().Where(m => vaccancymanager.Any(t => t.Id.Equals(m.JobVacancyLevelManagerId))
                           && m.JobApplicationId == Guid.Parse(id)).ToList();

                        if (anyInterviewSchduled.Any())
                        {
                            var jobDetails = _unitOfWork.JobVacancy.Find(i => i.Id == vaccancy.JobVacancyId).FirstOrDefault();
                            var candidate = _unitOfWork.JobCandidateInterview.GetCandidate(vaccancy.Id);

                            foreach (var candidateInterview in anyInterviewSchduled)
                            {

                                var candidateLevelStatus = (Enum.JobStatus)int.Parse(statusId);
                                if (candidateInterview != null)
                                {

                                    var skillList = _unitOfWork.JobVacancyLevelSkillQuestion.Find(m => m.JobVacancyLevelId == Guid.Parse(levelId)).ToList();
                                    if (!skillList.Any())
                                    {
                                        candidateInterview.IsLevelCompleted = true;
                                    }
                                    candidateInterview.IsInterviewCompleted = true;

                                    if (candidateLevelStatus == Enum.JobStatus.Hired)
                                    {
                                        candidateInterview.IsCandidateSelected = true;
                                        string message1 = RecruitmentTemplates.CandidateLevelSelection(candidate.FullName, vaccancyLevel.Name, jobDetails.JobTitle);
                                        (bool success, string errorMsg) response1 = await _emailer.SendEmailAsync(candidate.FullName, candidate.Email, "Job Interview Process Detail", message1);

                                    }
                                    else
                                    {
                                        string message2 = RecruitmentTemplates.CandidateLevelReject(candidate.FullName, vaccancyLevel.Name, jobDetails.JobTitle);
                                        (bool success, string errorMsg) response2 = await _emailer.SendEmailAsync(candidate.FullName, candidate.Email, "Job Interview Process Detail ", message2);
                                    }

                                    _unitOfWork.JobCandidateInterview.Update(candidateInterview);

                                }
                                _unitOfWork.SaveChanges();
                            }

                            if (vaccancy != null)
                            {
                                vaccancy.LevelId = vaccancyLevel.Id;
                                _unitOfWork.JobApplication.Update(vaccancy);
                                _unitOfWork.SaveChanges();
                            }

                            return NoContent();
                        }
                        return BadRequest("Please Schdule the interview ");
                    }
                    return NoContent();
                }
                else
                {
                    return BadRequest(" model is empty");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Error In CandidateInterViewScheduleController - CompleteInterviewLevel() -", ex.Message.ToString());
                return BadRequest(ex.GetBaseException().Message);
            }
        }

        [HttpGet("jobStatus/{id}/{statusId}")]
        public async Task<IActionResult> ChangeStatus(string id, int statusId)
        {
            try
            {
                if (!String.IsNullOrEmpty(id))
                {

                    UpdateApplicationStatus(Guid.Parse(id), statusId, "");

                    var candidate = _unitOfWork.JobCandidateInterview.GetCandidate(Guid.Parse(id));


                    if (statusId == 6)
                    {
                        string message = RecruitmentTemplates.GetRejectedCandidateToCandidateEmail(candidate == null ? "" : candidate.FullName);
                        (bool success, string errorMsg) response1 = await _emailer.SendEmailAsync(candidate == null ? "" : candidate.FullName, candidate == null ? "" : candidate.Email, "Application Status", message);
                    }
                    else if (statusId == 5)
                    {
                        string message = RecruitmentTemplates.GetSelectedCandidateToCandidateEmail(candidate == null ? "" : candidate.FullName);
                        (bool success, string errorMsg) response1 = await _emailer.SendEmailAsync(candidate == null ? "" : candidate.FullName, candidate == null ? "" : candidate.Email, "Application Status", message);
                    }

                    return NoContent();
                }
                else
                {
                    return BadRequest(" model is empty");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Error In CandidateInterViewScheduleController - ChnageStatus() -", ex.Message.ToString());
                return BadRequest(ex.GetBaseException().Message);
            }
        }


        private void UpdateApplicationStatus(Guid applicationId, int status, string levelId)
        {
            var jobapp = _unitOfWork.JobApplication.Get(applicationId);
            if (jobapp != null)
            {
                jobapp.JobStatus = status;
                if (levelId != "")
                {
                    jobapp.LevelId = Guid.Parse(levelId);
                }
                jobapp.UpdatedDate = DateTime.Now;
                _unitOfWork.JobApplication.Update(jobapp);
            }

        }

        private void UpdateHrKpiScoreStatus(Guid applicationId)
        {
            var jobApplicationHrQuestion = _unitOfWork.JobApplicationHRQuestions.Find(m => m.JobApplicationId == applicationId).ToList();
            if (jobApplicationHrQuestion.Count > 0)
            {
                var hrScore = 0;
                var obtained = 0;
                var weightage = 0;

                foreach (var item in jobApplicationHrQuestion)
                {
                    obtained = obtained + Convert.ToInt32(item.ObtainedWeightage);
                    weightage = weightage + Convert.ToInt32(item.Weightage);
                }
                hrScore = (obtained * 100) / weightage;

                var jobapplication = _unitOfWork.JobApplication.Get(applicationId);
                if (jobapplication != null)
                {
                    jobapplication.HRScore = hrScore.ToString();
                }
                UpdateOverAllScoreStatus(jobapplication);
            }

        }

        private void UpdateSkillKpiScoreStatus(Guid applicationId)
        {
            var candidateInterview = _unitOfWork.JobCandidateInterview.Find(m => m.JobApplicationId == applicationId).ToList();
            if (candidateInterview.Count > 0)
            {
                var skillScore = 0;
                var obtained = 0;
                var weightage = 0;

                foreach (var item in candidateInterview)
                {

                    var skillList = _unitOfWork.JobApplicationSkillQuestion.Find(m => m.JobCandidateInterviewId == item.Id).ToList();
                    if (skillList.Count > 0)
                    {
                        foreach (var itemSkill in skillList)
                        {
                            obtained = obtained + Convert.ToInt32(itemSkill.ObtainedWeightage);
                            weightage = weightage + Convert.ToInt32(itemSkill.Weightage);
                        }
                    }
                }
                skillScore = (obtained * 100) / weightage;
                var jobapplication = _unitOfWork.JobApplication.Get(applicationId);
                if (jobapplication != null)
                {
                    jobapplication.SkillScore = skillScore.ToString();

                }
                UpdateOverAllScoreStatus(jobapplication);
            }

        }

        private void UpdateOverAllScoreStatus(JobApplication jobApplication)
        {
            if (jobApplication != null)
            {
                var overAllScore = 0.0;
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

        private List<DropDownList> GetManagerList()
        {
            var managerList = new List<DropDownList>();
            var employeeList = _unitOfWork.Employee.GetManagerList();
            foreach (var item in employeeList)
            {
                managerList.Add(new DropDownList { Label = item.FullName, Value = item.Id.ToString() });
            }
            return managerList;
        }

        private CandidateInterViewModel GetCandidateDetail(Guid id)
        {
            Expression<Func<JobApplication, bool>> jobFilter = e => e.Id == id;
            var data = _unitOfWork.JobApplication.Get(jobFilter, null, "JobCandidateProfiles.ApplicationUser").FirstOrDefault();
            var candidateInformation = _unitOfWork.JobCandidateProfile.GetJobCandidateProfile(data?.JobCandidateProfiles?.ApplicationUser?.Id.ToString());

            var candidateData = new CandidateInterViewModel()
            {
                CandidateName = data.JobCandidateProfiles.ApplicationUser.FullName,
                Mobile = data.JobCandidateProfiles.ApplicationUser.PhoneNumber,
                ResumeUrl = GetResume(candidateInformation.ResumeId) != null ? GetResume(candidateInformation.ResumeId) : string.Empty

            };
            return candidateData;
        }

        private List<DropDownList> GetInterviewType()
        {
            var interviewType = new List<DropDownList>();
            var interviewTypes = _unitOfWork.JobInterviewType.GetAll();
            foreach (var item in interviewTypes)
            {
                interviewType.Add(new DropDownList { Label = item.Name, Value = item.Id.ToString() });
            }
            return interviewType;
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

        private List<InterviewScheduleModel> GetInterviewSchedule(Guid applicationId, Guid levelId)
        {
            var result = _unitOfWork.JobCandidateInterview.GetInterviewScheduleList(applicationId, levelId);
            return result;
        }

        private QuestionAnswerModel GetQuestionAnswer(Guid appId)
        {

            try
            {
                var question = new QuestionAnswerModel();
                var jobApplication = _unitOfWork.JobApplication.Find(m => m.Id == appId).FirstOrDefault();
                if (jobApplication == null) return question;

                question.ScreeningQuestionList = GetScreening(jobApplication.JobVacancyId, appId);
                question.JobHRKpiList = GetHRKpi(jobApplication.JobVacancyId, appId);
                return question;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error In CandidateInterViewScheduleController - GetQuestionAnswer() -", ex.Message.ToString());
                throw;
            }


        }

        private List<QuestionAnswer> GetScreening(Guid vaccancyId, Guid appId)
        {

            var question = new List<QuestionAnswer>();

            var reult = _unitOfWork.JobCandidateInterview.GetAllScreening(vaccancyId, appId);

            if (reult.Count > 0)
            {
                foreach (var item in reult)
                {
                    question.Add(new QuestionAnswer { JobQuestionId = item.JobScreeningQuestionId.ToString(), Question = item.Question, Weightage = item.Weightage, ObtainedWeightage = item.ObtainedWeightage });

                }
            }
            return question;


        }

        private List<HRKpiModel> GetHRKpi(Guid vaccancyId, Guid appId)
        {
            return _unitOfWork.JobCandidateInterview.GetHRKpi(vaccancyId, appId);
        }

        private List<QuestionAnswer> GetSkillKpiManager(Guid interviewId)
        {
            var reult = _unitOfWork.JobCandidateInterview.GetSkillKpiManager(interviewId);
            return reult;
        }

        private JobInformationModel GetJobInfo(Guid appId)
        {
            try
            {
                var jobInfo = new JobInformationModel();
                var result = _unitOfWork.JobCandidateInterview.GetJobInformation(appId);
                if (result != null)
                {
                    jobInfo.JobTitle = result.JobVacancy.JobTitle;
                    jobInfo.ApplicationType = result.ApplicationType;
                    jobInfo.JobStatus = ((Enum.JobStatus)result.JobStatus).ToString(); ;
                    jobInfo.AppliedDate = result.CreatedDate.ToShortDateString();
                }
                return jobInfo;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error In CandidateInterViewScheduleController - GetJobInfo() -", ex.Message.ToString());
                throw;
            }
        }

        private List<InterviewScheduleLevel> GetInterviewScheduleList(Guid appId)
        {
            var interViewList = new List<InterviewScheduleLevel>();
            try
            {
                var vaccancy = _unitOfWork.JobApplication.Get(appId);
                if (vaccancy != null)
                {
                    var level = _unitOfWork.JobVacancyLevel.Find(m => m.JobVacancyId == vaccancy.JobVacancyId).OrderBy(o => o.Level).ToList();

                    if (level.Count > 0)
                    {
                        foreach (var item in level)
                        {
                            var list = GetInterviewSchedule(appId, item.Id);
                            var levelManagerIds = _unitOfWork.JobVacancyLevelManager.Find(f => f.JobVacancyLevelId == item.Id).Select(i => i.EmployeeId.ToString()).ToList();
                            var skillQuestionList = _unitOfWork.JobVacancyLevelSkillQuestion.Find(m => m.JobVacancyLevelId == item.Id).ToList();
                            var interviewId = skillQuestionList.Any() ? list.Select(i => i.InterviewId).FirstOrDefault() : string.Empty;
                            var completed = false;
                            if (list.Count > 0)
                            {
                                completed = !list.Any(m => m.IsLevelCompleted == false);
                            }

                            interViewList.Add(
                                new InterviewScheduleLevel
                                {
                                    LevelId = item.Id.ToString(),
                                    Level = item.Name,
                                    InterviewScheduleModelList = list,
                                    IsLevelCompleted = completed,
                                    IsInterviewCompleted = list.Select(s => s.InterviewStatus).FirstOrDefault(),
                                    InterviewId = interviewId,
                                    LevelManagerIds = levelManagerIds
                                });
                        }
                    }
                }
                return interViewList;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error In CandidateInterViewScheduleController - GetInterviewScheduleList() -", ex.Message.ToString());

                throw;
            }
        }

        private Guid GetManagerLevelId(Guid managerId, Guid levelId)
        {

            var vaccacnyLevelManager = _unitOfWork.JobVacancyLevelManager.Find(m => m.JobVacancyLevelId == levelId && m.EmployeeId == managerId).FirstOrDefault();
            if (vaccacnyLevelManager != null)
            {
                return vaccacnyLevelManager.Id;
            }
            else
            {
                var jobVacManager = new JobVacancyLevelManager();
                jobVacManager.JobVacancyLevelId = levelId;
                jobVacManager.EmployeeId = managerId;
                jobVacManager.IsActive = true;
                _unitOfWork.JobVacancyLevelManager.Add(jobVacManager);
                _unitOfWork.SaveChanges();
                return jobVacManager.Id;
            }
        }

    }
}