using A4.BAL;
using A4.DAL.Entites;
using A4.Empower.Helpers;
using DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace A4.Empower.Controllers
{
    [Route("api/[controller]")]
    public class JobVacancyController : Controller
    {
        readonly ILogger _logger;
        readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;
        readonly IEmailer _emailer;
        public JobVacancyController(ILogger<JobVacancyController> logger, IUnitOfWork unitOfWork, IConfiguration configuration, IEmailer emailer)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _configuration = configuration;
            _emailer = emailer;

        }

        [HttpGet("list/{page?}/{pageSize?}/{name?}")]
        [Produces(typeof(JobVacancyViewModel))]
        public IActionResult GetAll(int? page = null, int? pageSize = null, string name = null)
        {
            try
            {
                var result = new JobVacancyViewModel();
                var model = new List<JobVacancyModel>();
                var jobVacancy = _unitOfWork.JobVacancy.GetAllJobVacancy(name, Convert.ToInt32(page), Convert.ToInt32(pageSize));
                if (jobVacancy.Count() > 0)
                {
                    foreach (var item in jobVacancy)
                    {
                        model.Add(new JobVacancyModel
                        {
                            Id = item.Id.ToString(),
                            JobTitle = item.JobTitle,
                            JobLocation = item.JobLocation,
                            PublishedDate = item.PublishedDate,
                            Experience = item.Experience,
                            bIsPublished = item.bIsPublished,
                            JobDescription = item.JobDescription
                        });
                    }
                }
                result.JobVacancyModel = model;
                result.TotalCount = jobVacancy.TotalCount;
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.GetBaseException().Message);
            }
        }

        [HttpGet("joblist")]
        [Produces(typeof(JobVacancyViewModel))]
        public IActionResult GetJobList(string name = null)
        {
            try
            {
                var result = new JobVacancyViewModel();
                var model = new List<JobVacancyModel>();
                var jobVacancy = _unitOfWork.JobVacancy.GetAllJobVacancies(name);
                if (jobVacancy.Count() > 0)
                {
                    foreach (var item in jobVacancy)
                    {
                        model.Add(new JobVacancyModel { Id = item.Id.ToString(), JobTitle = item.JobTitle, JobLocation = item.JobLocation, JobDescription = item.JobDescription, JobRequirements = item.JobRequirements, SalaryRange = item.SalaryRange, PublishedDate = item.PublishedDate, Experience = item.Experience, bIsPublished = item.bIsPublished });
                    }
                }
                result.JobVacancyModel = model;
                return Ok(model);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error In JobVacancyController -GetJobList -", ex.Message.ToString());
                return BadRequest(ex.GetBaseException().Message);

            }
        }
        [HttpGet("allJobVacancies")]
        [Produces(typeof(JobVacancyViewModel))]
        public IActionResult GetAllJobs(string name = null)
        {
            try
            {
                // var result = new JobVacancyViewModel();
                var model = new List<JobViewModel>();
                var jobVacancy = _unitOfWork.JobVacancy.GetAllJobVacancies(name);
                if (jobVacancy.Count() > 0)
                {
                    foreach (var item in jobVacancy)
                    {
                        model.Add(new JobViewModel { Id = item.Id.ToString(), JobTitle = item.JobTitle, JobType = item.JobType.Name, JobLocation = item.JobLocation, JobDescription = item.JobDescription, JobRequirements = item.JobRequirements, SalaryRange = item.SalaryRange, PublishedDate = item.PublishedDate, Experience = item.Experience, bIsPublished = item.bIsPublished });
                    }
                }
                // result.JobVacancyModel = model;
                return Ok(model);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error In JobVacancyController -GetJobList -", ex.Message.ToString());
                return BadRequest(ex.GetBaseException().Message);

            }
        }

        [HttpPost("applyforjob")]
        public async Task<IActionResult> ApplyForJob()
        {
            try
            {
                if (Request.Form != null && Request.Form.Files.Count() > 0)
                {
                    var jobApplyModel = new JobApplyModel();
                    jobApplyModel = JsonConvert.DeserializeObject<JobApplyModel>(Request.Form["jobApplyModel"].ToString());
                    jobApplyModel.attachment = Request.Form.Files;
                    if (jobApplyModel.jobId != null)
                    {
                        var jobName = _unitOfWork.JobVacancy.GetJobDetail(jobApplyModel.jobId).JobTitle;
                        if (!string.IsNullOrWhiteSpace(jobName))
                        {
                            string hrName = _configuration.GetValue<string>("HrDetail:Name");
                            string hrEmail = _configuration.GetValue<string>("HrDetail:Email");
                            string message = RecruitmentTemplates.GetHrJobApplicationEmail(jobApplyModel.name, hrName, jobName, DateTime.Now.ToShortDateString(), jobApplyModel.email, jobApplyModel.phone);
                            (bool success, string errorMsg) response1 = await _emailer.SendEmailAsync(jobApplyModel.name, hrEmail, "Candidate Job Application", message, null, true, jobApplyModel.attachment);
                            return Ok(response1.success);
                        }
                    }
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError("Error In JobVacancyController -ApplyForJob () -", ex.Message.ToString());
                return BadRequest(ex.GetBaseException().Message);

            }

        }

        [HttpGet("vacancy/{id}")]
        [Produces(typeof(JobVacancyModel))]
        public IActionResult GetById(string id)
        {
            try
            {
                var viewModel = new JobVacancyModel();
                viewModel.ManagerList = GetManagerList();
                viewModel.JobTypeList = GetJobTypeList();
                if (!string.IsNullOrEmpty(id) && id != "null")
                {
                    var vacancy = _unitOfWork.JobVacancy.Get(Guid.Parse(id));
                    if (vacancy != null)
                    {

                        viewModel.Id = vacancy.Id.ToString();
                        viewModel.Currency = vacancy.Currency;
                        viewModel.Experience = vacancy.Experience;
                        viewModel.JobDescription = vacancy.JobDescription;
                        viewModel.JobLocation = vacancy.JobLocation;
                        viewModel.JobRequirements = vacancy.JobRequirements;
                        viewModel.JobTitle = vacancy.JobTitle;
                        viewModel.NoOfvacancies = vacancy.NoOfvacancies;
                        viewModel.PublishedDate = vacancy.PublishedDate;
                        viewModel.SalaryRange = vacancy.SalaryRange;
                        viewModel.bIsClosed = vacancy.bIsClosed;
                        viewModel.bIsPublished = vacancy.bIsPublished;
                        viewModel.JobTypeId = vacancy.JobTypeId.ToString();
                        viewModel.JobVacancyLevel = _unitOfWork.JobVacancy.GetInterviewLevels(vacancy.Id);
                        return Ok(viewModel);
                    }
                    else
                    {
                        return NotFound(id);
                    }
                }
                else
                {
                    return Ok(viewModel);
                }

            }
            catch (Exception ex)
            {
                return BadRequest(ex.GetBaseException().Message);
            }
        }

        [HttpPost("save/{id}")]
        public IActionResult Save(string id, [FromBody] JobCreationModel model)
        {
            try
            {
                if (model == null)
                    return BadRequest($"{nameof(model)} cannot be null");

                if (id != "undefined" && id != null)
                {
                    var vacancy = _unitOfWork.JobVacancy.Get(new Guid(id));
                    if (vacancy != null)
                    {
                        vacancy.Currency = model.Currency;
                        vacancy.Experience = model.Experience;
                        vacancy.JobDescription = model.JobDescription;
                        vacancy.JobLocation = model.JobLocation;
                        vacancy.JobRequirements = model.JobRequirements;
                        vacancy.JobTitle = model.JobTitle;
                        vacancy.JobTypeId = Guid.Parse(model.JobTypeId);
                        vacancy.NoOfvacancies = model.NoOfvacancies;
                        vacancy.SalaryRange = model.SalaryRange;
                        _unitOfWork.JobVacancy.Update(vacancy);
                        UpdateVacancyMangerAndLevel(vacancy.Id, model);
                        return NoContent();
                    }
                }
                else
                {
                    var vacancy = new JobVacancy();
                    vacancy.Currency = model.Currency;
                    vacancy.Experience = model.Experience;
                    vacancy.NoOfvacancies = model.NoOfvacancies;
                    vacancy.JobDescription = model.JobDescription;
                    vacancy.JobLocation = model.JobLocation;
                    vacancy.JobRequirements = model.JobRequirements;
                    vacancy.JobTitle = model.JobTitle;
                    vacancy.JobTypeId = Guid.Parse(model.JobTypeId);
                    vacancy.SalaryRange = model.SalaryRange;
                    vacancy.bIsPublished = false;
                    vacancy.PublishedDate = DateTime.Now.ToShortDateString();
                    _unitOfWork.JobVacancy.Add(vacancy);
                    UpdateVacancyMangerAndLevel(vacancy.Id, model);
                    return Ok(vacancy.Id);
                }
                return NoContent();
            }
            catch (DbUpdateException ex) when (ex.InnerException is SqlException sqlException && (sqlException.Number == 2627 || sqlException.Number == 2601))
            {
                return BadRequest("Level Can't be same");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.GetBaseException().Message);
            }
        }

        [HttpDelete("delete/{id}")]
        public IActionResult Delete(string id)
        {
            try
            {
                if (!string.IsNullOrEmpty(id))
                {

                    var checkexist = _unitOfWork.JobVacancy.Get(new Guid(id));
                    if (checkexist != null)
                    {
                        _unitOfWork.JobVacancy.Remove(checkexist);
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
                return BadRequest(ex.GetBaseException().Message);
            }
        }

        [HttpPost("publish/{id}")]
        public IActionResult Publish(string id)
        {
            try
            {
                if (!string.IsNullOrEmpty(id))
                {
                    var vacancy = _unitOfWork.JobVacancy.Get(new Guid(id));
                    if (vacancy != null)
                    {
                        if (vacancy.bIsPublished)
                        {
                            vacancy.bIsPublished = false;
                        }
                        else
                        {
                            vacancy.bIsPublished = true;
                            vacancy.PublishedDate = DateTime.UtcNow.Date.ToString();
                        }
                        _unitOfWork.JobVacancy.Update(vacancy);
                        return NoContent();
                    }
                }
                return BadRequest(" Id cannot be null");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.GetBaseException().Message);
            }
        }

        [HttpDelete("deleteinterviewlevel/{id}")]
        public IActionResult DeleteInterviewLevel(string id)
        {
            try
            {
                if (!string.IsNullOrEmpty(id))
                {

                    var jobVacancyLevel = _unitOfWork.JobVacancyLevel.Get(new Guid(id));
                    if (jobVacancyLevel != null)
                    {
                        _unitOfWork.JobVacancyLevel.DeleteJobVacancyLevel(jobVacancyLevel);
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
                return BadRequest(ex.GetBaseException().Message);
            }
        }

        [HttpGet("getscreening/{id?}")]
        public IActionResult GetScreeningQuestionList(string id)
        {
            if (!string.IsNullOrEmpty(id) && id != "null")
            {
                var screeingQuestionList = new List<JobScreeningQuestions>();
                var result = _unitOfWork.JobScreeningQuestion.Find(m => m.JobVacancyId == Guid.Parse(id));
                foreach (var item in result)
                {
                    screeingQuestionList.Add(new JobScreeningQuestions { Id = item.Id.ToString(), ControlType = item.ControlType, Question = item.Questions, Option1 = item.Option1, Option2 = item.Option2, Option3 = item.Option3, Option4 = item.Option4, OptChk1 = item.ChkOption1, OptChk2 = item.ChkOption2, OptChk3 = item.ChkOption3, OptChk4 = item.ChkOption4, Mandatory = item.bIsRequired, Weightage = item.Weightage });
                }
                return Ok(screeingQuestionList);
            }
            else
            {
                return NoContent();
            }

        }

        [HttpPost("savescreening/{id}")]
        public IActionResult SaveScreeningQuestion(string id, [FromBody] List<JobScreeningQuestions> jobScreeningQuestions)
        {
            var screeningQuationList = _unitOfWork.JobScreeningQuestion.Find(m => m.JobVacancyId == new Guid(id)).ToList();
            if (screeningQuationList.Count > 0)
            {
                _unitOfWork.JobScreeningQuestion.RemoveRange(screeningQuationList);
            }
            var questionsList = new List<JobScreeningQuestion>();
            foreach (var item in jobScreeningQuestions)
            {
                questionsList.Add(new JobScreeningQuestion
                {
                    Option1 = item.Option1,
                    JobVacancyId = Guid.Parse(id),
                    Option2 = item.Option2,
                    Option3 = item.Option3,
                    Option4 = item.Option4,
                    ChkOption1 = item.OptChk1,
                    ChkOption2 = item.OptChk2,
                    ChkOption3 = item.OptChk3,
                    ChkOption4 = item.OptChk4,
                    Weightage = item.Weightage,
                    bIsRequired = item.Mandatory,
                    Questions = item.Question,
                    ControlType = item.ControlType
                });
            }
            _unitOfWork.JobScreeningQuestion.AddRange(questionsList);
            _unitOfWork.SaveChanges();
            return NoContent();
        }

        [HttpDelete("deletescreening/{id}")]
        public IActionResult DeleteScreening(string id)
        {
            try
            {
                if (!string.IsNullOrEmpty(id))
                {

                    var checkexist = _unitOfWork.JobScreeningQuestion.Get(new Guid(id));
                    if (checkexist != null)
                    {
                        _unitOfWork.JobScreeningQuestion.Remove(checkexist);
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
                return BadRequest(ex.GetBaseException().Message);
            }
        }

        [HttpGet("gethrquestion/{id?}")]
        public IActionResult GetHrQuestionList(string id)
        {
            if (!string.IsNullOrEmpty(id) && id != "null")
            {
                var hrQuationList = new List<JobHRQuestionModel>();
                var result = _unitOfWork.JobHRQuestion.Find(m => m.JobVacancyId == Guid.Parse(id));
                foreach (var item in result)
                {
                    hrQuationList.Add(new JobHRQuestionModel { Id = item.Id.ToString(), Question = item.Question, Weightage = item.Weightage });
                }
                return Ok(hrQuationList);
            }
            else
            {
                return NoContent();
            }
        }

        [HttpPost("savehrquestion/{id}")]
        public IActionResult SaveHrQuestion(string id, [FromBody] List<JobHRQuestionModel> model)
        {
            var hrQuestionList = _unitOfWork.JobHRQuestion.Find(m => m.JobVacancyId == new Guid(id)).ToList();
            if (hrQuestionList.Count > 0)
            {
                _unitOfWork.JobHRQuestion.RemoveRange(hrQuestionList);
            }
            var questionsList = new List<JobHRQuestion>();
            foreach (var item in model)
            {
                questionsList.Add(new JobHRQuestion
                {
                    JobVacancyId = Guid.Parse(id),
                    Question = item.Question,
                    Weightage = item.Weightage
                });
            }
            _unitOfWork.JobHRQuestion.AddRange(questionsList);
            _unitOfWork.SaveChanges();
            return NoContent();
        }

        [HttpDelete("deletehrquestion/{id}")]
        public IActionResult DeleteHrQuestion(string id)
        {
            try
            {
                if (!string.IsNullOrEmpty(id))
                {

                    var result = _unitOfWork.JobHRQuestion.Get(new Guid(id));
                    if (result != null)
                    {
                        _unitOfWork.JobHRQuestion.Remove(result);
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
                return BadRequest(ex.GetBaseException().Message);
            }
        }

        [HttpGet("getskillquestion/{id?}")]
        public IActionResult GetSkillQuestionList(string id)
        {
            try
            {
                var skillQuestionModel = new JobSkillQuestionModel();
                var skillQuestionList = new List<JobSkillQuestions>();
                skillQuestionModel.LevelList = GetLevel(id);
                if (!string.IsNullOrEmpty(id) && id != "null")
                {
                    var result = _unitOfWork.JobSkillQuestion.Find(m => m.JobVacancyId == Guid.Parse(id)).ToList();
                    if (result.Count() > 0)
                    {
                        foreach (var item in result)
                        {
                            var levelIds = GetLevelId(item.Id);
                            skillQuestionList.Add(new JobSkillQuestions { Id = item.Id.ToString(), Question = item.Question, Weightage = item.Weightage, LevelIdList = levelIds });
                        }
                        skillQuestionModel.JobSkillQuestion = skillQuestionList;
                    }
                    return Ok(skillQuestionModel);
                }
                else
                {
                    return Ok(skillQuestionModel);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.GetBaseException().Message);
            }
        }

        [HttpPost("saveskillquestion/{id}")]
        public IActionResult SaveSkillQuestion(string id, [FromBody] List<JobSkillQuestions> model)
        {

            var skillQuestionList = _unitOfWork.JobSkillQuestion.Find(m => m.JobVacancyId == new Guid(id)).ToList();
            if (skillQuestionList.Count > 0)
            {
                _unitOfWork.JobSkillQuestion.RemoveRange(skillQuestionList);
            }

            var questionsList = new List<JobSkillQuestion>();
            foreach (var item in model)
            {

                var jobSkill = new JobSkillQuestion();
                jobSkill.JobVacancyId = Guid.Parse(id);
                jobSkill.Question = item.Question;
                jobSkill.Weightage = item.Weightage;

                _unitOfWork.JobSkillQuestion.Add(jobSkill);

                var levelSkillQuation = _unitOfWork.JobVacancyLevelSkillQuestion.Find(m => m.JobVacancyLevelId == jobSkill.Id).ToList();
                if (levelSkillQuation.Count > 0)
                {
                    _unitOfWork.JobVacancyLevelSkillQuestion.RemoveRange(levelSkillQuation);
                }

                foreach (var levelQuestion in item.LevelIdList)
                {
                    var jobSkillLevelQuation = new JobVacancyLevelSkillQuestion();
                    jobSkillLevelQuation.JobSkillQuestionId = jobSkill.Id;
                    jobSkillLevelQuation.JobVacancyLevelId = Guid.Parse(levelQuestion);
                    _unitOfWork.JobVacancyLevelSkillQuestion.Add(jobSkillLevelQuation);
                }
            }
            _unitOfWork.SaveChanges();
            return NoContent();
        }

        [HttpDelete("deleteskillquestion/{id}")]
        public IActionResult DeleteSkillQuestion(string id)
        {
            try
            {
                if (!string.IsNullOrEmpty(id))
                {

                    var jobSkillQuestion = _unitOfWork.JobSkillQuestion.Get(new Guid(id));
                    if (jobSkillQuestion != null)
                    {
                        _unitOfWork.JobSkillQuestion.DeleteJobSkillQuestion(jobSkillQuestion);
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
                return BadRequest(ex.GetBaseException().Message);
            }
        }

        [HttpPut("updateReason/{id}/{reason}")]
        public IActionResult UpdateReason(string id, string reason)
        {
            try
            {
                if (!string.IsNullOrEmpty(id) && !string.IsNullOrEmpty(reason))
                {

                    var jobVacancy = _unitOfWork.JobVacancy.Get(Guid.Parse(id));
                    if (jobVacancy != null)
                    {
                        jobVacancy.JDReson = reason;
                        _unitOfWork.JobVacancy.Update(jobVacancy);
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
                return BadRequest(ex.GetBaseException().Message);
            }
        }

        [HttpGet("getReason/{id}")]
        public IActionResult GetJDReason(string id)
        {
            try
            {
                if (!string.IsNullOrEmpty(id))
                {
                    var jobVacancy = _unitOfWork.JobVacancy.Get(Guid.Parse(id));
                    if (jobVacancy != null)
                    {

                        return Ok(new { reason = jobVacancy.JDReson });
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


        [HttpPost("sendMail/{id}")]
        public async Task<IActionResult> SendJDRequiremntMailAsync(string id, [FromBody] List<string> directoryIds)
        {
            if (directoryIds.Any())
            {
                Expression<Func<JobVacancy, bool>> jobFilter = e => e.Id == Guid.Parse(id);
                var vacancy = _unitOfWork.JobVacancy.Get(jobFilter, null, "").FirstOrDefault();
                (bool success, string errorMsg) response1 = (false, "");
                foreach (var mail in directoryIds)
                {
                    var client = _unitOfWork.EmailDirectoryRepository.Get(i => i.Id == Guid.Parse(mail)).FirstOrDefault();
                    var message = RecruitmentTemplates.JDRequirement(client.Name, vacancy.JobTitle);
                    response1 = await _emailer.SendEmailAsync(client.Name, client.Email, "Job Description Required", message, null, true, null);
                }
                return Ok(response1);
            }
            return BadRequest();
        }
        private List<DropDownList> GetLevel(string vacancyId)
        {
            var lstLevel = new List<DropDownList>();
            Guid guidVacancy = new Guid();
            if (!string.IsNullOrEmpty(vacancyId) && vacancyId != "null")
            {
                guidVacancy = Guid.Parse(vacancyId);
            }
            var vacancyLevels = _unitOfWork.JobVacancyLevel.GetAll().Where(x => x.JobVacancyId == guidVacancy).ToList().OrderBy(o => o.Level);
            if (vacancyLevels.Count() > 0)
            {
                foreach (var item in vacancyLevels)
                {
                    lstLevel.Add(new DropDownList { Label = item.Name, Value = item.Id.ToString() });
                }
            }
            else
            {
                lstLevel.Add(new DropDownList { Label = "", Value = "" });
            }
            return lstLevel;
        }

        private List<string> GetLevelId(Guid id)
        {
            var levelList = new List<string>();
            var level = _unitOfWork.JobVacancyLevelSkillQuestion.Find(c => c.JobSkillQuestionId == id).ToList();
            foreach (var item in level)
            {
                levelList.Add(item.JobVacancyLevelId.ToString());
            }
            return levelList;
        }

        private List<DropDownList> GetManagerList()
        {
            var managerVm = new List<DropDownList>();
            var manager = _unitOfWork.Employee.GetManagerList();
            if (manager.Count() > 0)
            {
                foreach (var item in manager)
                {
                    managerVm.Add(new DropDownList { Label = item.FullName, Value = item.Id.ToString() });
                }
            }
            return managerVm;
        }

        private List<DropDownList> GetJobTypeList()
        {
            var managerVm = new List<DropDownList>();
            var manager = _unitOfWork.JobType.GetAll();
            if (manager.Count() > 0)
            {
                foreach (var item in manager)
                {
                    managerVm.Add(new DropDownList { Label = item.Name, Value = item.Id.ToString() });
                }
            }
            return managerVm;
        }

        private void UpdateVacancyMangerAndLevel(Guid id, JobCreationModel vacancy)
        {
            var vaccancyLevel = _unitOfWork.JobVacancyLevel.Find(m => m.JobVacancyId == id).ToList();
            if (vaccancyLevel.Count > 0)
            {
                _unitOfWork.JobVacancyLevel.RemoveRange(vaccancyLevel);
            }
            for (int i = 0; i < vacancy.JobVacancyLevel.Count; i++)
            {
                var level = new JobVacancyLevel { JobVacancyId = id, Name = vacancy.JobVacancyLevel[i].Name, Level = i };
                _unitOfWork.JobVacancyLevel.Add(level);
                var vaccancyManager = _unitOfWork.JobVacancyLevelManager.Find(m => m.JobVacancyLevelId == level.Id).ToList();
                if (vaccancyManager.Count > 0)
                {
                    _unitOfWork.JobVacancyLevelManager.RemoveRange(vaccancyManager);
                }
                foreach (var manager in vacancy.JobVacancyLevel[i].ManagerList)
                {
                    var jobvaccancyManager = new JobVacancyLevelManager { JobVacancyLevelId = level.Id, EmployeeId = Guid.Parse(manager) };
                    _unitOfWork.JobVacancyLevelManager.Add(jobvaccancyManager);
                }
                _unitOfWork.SaveChanges();
            }
        }

        private List<JobSkillQuestionModel> GetSkillKpiList(Guid id)
        {
            var skillKpiList = new List<JobSkillQuestionModel>();
            return skillKpiList;
        }

        private List<JobHRKpi> GetHrKpiList(Guid id)
        {
            var hrList = new List<JobHRKpi>();
            return hrList;
        }



    }
}