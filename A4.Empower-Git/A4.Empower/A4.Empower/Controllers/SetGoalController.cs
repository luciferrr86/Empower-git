using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using A4.BAL;
using A4.Empower.Helpers;
using DAL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace A4.Empower.Controllers
{
   
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = OpenIddict.Validation.AspNetCore.OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)]
    public class SetGoalController : Controller
    {
        readonly IUnitOfWork _unitOfWork;
        readonly IEmailer _emailer;
        public SetGoalController(IUnitOfWork unitOfWork,IEmailer emailer)
        {
            _unitOfWork = unitOfWork;
            _emailer = emailer;
        }

        [HttpGet("getlist/{id}/{val}")]
        public IActionResult Get(string id,string val)
        {
            var setGoalModel = new GetSetGoalModel();
            var isCEO = _unitOfWork.PerformanceApp.CheckIsCEO(id);
            var yearId = _unitOfWork.PerformanceConfig.GetPerformanceYear();
            if (yearId != Guid.Empty)
            {
                var qudrant = _unitOfWork.PerformanceApp.GetQuadrantList();
                var goalName = _unitOfWork.PerformanceApp.GetPerformanceGoalNames(id, yearId);               
                if (isCEO)
                {
                    setGoalModel = _unitOfWork.PerformanceGoalMain.GetSetGoalDetail(yearId, id, val, qudrant, goalName);
                }
                else
                {
                    var isMgrReleased = _unitOfWork.PerformanceApp.CheckManagerRelease(id, yearId);
                    if (isMgrReleased)
                    {                        
                        setGoalModel = _unitOfWork.PerformanceGoalMain.GetSetGoalDetail(yearId, id, val, qudrant, goalName);
                        setGoalModel.IsManagerRealeased = isMgrReleased;
                    }
                }
               
                setGoalModel.IsPerformanceStarted = true;                
            }
            setGoalModel.IsCEO = isCEO;
            return Ok(setGoalModel);
        }

        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        [HttpPost("savelist/{id}")]
        public IActionResult Post(string id,[FromBody]PostSetGoalModel setGoalModel)
        {
            var yearId = _unitOfWork.PerformanceConfig.GetPerformanceYear();
            var isSaved = _unitOfWork.PerformanceGoalMain.SaveGoal(setGoalModel, id,yearId);
            return NoContent();
        }

        [HttpPost("savegoal/{id}")]
        public IActionResult Post([FromBody]Goal goal , string id)
        {
            var yearId= _unitOfWork.PerformanceConfig.GetPerformanceYear();
            var emp = _unitOfWork.PerformanceApp.GetEmployeeByUserId(id);
           var goalId= _unitOfWork.PerformanceGoal.AddGoalName(goal.GoalName,yearId, emp.Id);
            return Ok(goalId);
        }

        [HttpGet("releaseGoal/{id}")]
        public async Task<IActionResult> ReleaseGoal(string id)
        {
            try
            {
                var yearId = _unitOfWork.PerformanceConfig.GetPerformanceYear();
                var emp = _unitOfWork.PerformanceApp.GetEmployeeByUserId(id);
                var features = _unitOfWork.PerformanceApp.GetFeatures();
                var releaseGoal = _unitOfWork.PerformanceEmpGoal.ReleaseGoal(emp.Id, yearId, features);
                if (releaseGoal.mailList!=null && releaseGoal.mailList.Count > 0)
                {
                    foreach (var item in releaseGoal.mailList)
                    {
                        string message = PerformanceTemplates.Invitation(item.MailID, item.Name);
                        (bool success, string errorMsg) response = await _emailer.SendEmailAsync(item.Name, item.MailID, "Set goals for direct reportees", message);
                    }
                }
                if (releaseGoal.mailListCEO != null && releaseGoal.mailListCEO.Count > 0)
                {
                    foreach (var item in releaseGoal.mailListCEO)
                    {
                        string message = PerformanceTemplates.Invitation(item.MailID, item.Name);
                        (bool success, string errorMsg) response = await _emailer.SendEmailAsync(item.Name, item.MailID, "Review Initial Rating", message);
                    }
                }
                return Ok(releaseGoal);
            }
            catch (Exception ex)
            {
              return BadRequest(ex.GetBaseException().Message);
            }
          
        }

        [HttpDelete("deleteMeasure/{id}")]
        public IActionResult Delete(string id)
        {
            _unitOfWork.PerformanceGoalMeasure.DeleteGoalMeasure(id, false);
            return NoContent();
        }
    }
}
