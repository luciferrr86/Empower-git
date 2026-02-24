using System;
using System.Linq;
using System.Threading.Tasks;
using A4.BAL;
using A4.Empower.Helpers;
using DAL;
using Microsoft.AspNetCore.Mvc;

namespace A4.Empower.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class MyGoalController : Controller
    {

        readonly IUnitOfWork _unitOfWork;
        readonly IEmailer _emailer;
        public MyGoalController(IUnitOfWork unitOfWork, IEmailer emailer)
        {
            _unitOfWork = unitOfWork;
            _emailer = emailer;
        }

        [HttpGet("getPreCheck/{id}")]
        public IActionResult PreCheck(string id)
        {
            var preChek = new PreCheck();
            var isPerformanceStarted = _unitOfWork.PerformanceApp.CheckPerformanceStart();
            var yearId = _unitOfWork.PerformanceConfig.GetPerformanceYear();
            var isManagerReleased = _unitOfWork.PerformanceApp.CheckManagerRelease(id, yearId);
            preChek.IsPerformanceStarted = isPerformanceStarted;
            preChek.IsManagerReleased = isManagerReleased;
            return Ok(preChek);
        }

        [HttpGet("getEmpDetail/{id}")]
        public IActionResult Get(string id)
        {
            var currentYearId = _unitOfWork.PerformanceConfig.GetPerformanceYear();
            bool checkPerformanceStart = _unitOfWork.PerformanceApp.CheckEmployeePerformanceStart(id);
            bool checkInitialRatingEnabled = _unitOfWork.PerformanceApp.GetFeatures().IsInitailRatingEnabled;
            bool checkCEORatingSignOffEnabled = _unitOfWork.PerformanceApp.GetFeatures().IsCEOSignOffEnabled;
            bool checkRatingSignOff = _unitOfWork.PerformanceApp.CheckIsRatingSignedOff("", id, currentYearId);
            var myGoal = new EmployeeDetail();
                var empDetail = _unitOfWork.PerformanceApp.EmployeeGoalDetail(id, currentYearId);
                myGoal = _unitOfWork.PerformanceApp.GetEmployeeDetail(id);
                return Ok(myGoal);
        }

        [HttpGet("getCurrentYearGoal/{id}")]
        public IActionResult GetCurrentYearGoal(string id)
        {
            var empCurrentGoal = new GoalViewModel();
            var currentYearId = _unitOfWork.PerformanceConfig.GetPerformanceYear();
            var empDetail = _unitOfWork.PerformanceApp.EmployeeGoalDetail(id, currentYearId);
            var midYearEnabled = _unitOfWork.PerformanceApp.GetFeatures().IsMidYearEnabled;
            empCurrentGoal = _unitOfWork.PerformanceEmpGoal.GetCurrentYearGoal(empDetail, midYearEnabled);
            empCurrentGoal.CheckSaveSubmit = _unitOfWork.PerformanceApp.GetSaveSubmit(empDetail.EmployeeId, currentYearId, midYearEnabled);
            return Ok(empCurrentGoal);
        }

        [HttpPost("saveCurrentYearGoal/{id}/{actionType}")]
        public async Task<IActionResult> Post(string id, [FromBody]GoalViewModel goalViewModel, string actionType)
        {
            var currentYearId = _unitOfWork.PerformanceConfig.GetPerformanceYear();
            var empDetail = _unitOfWork.PerformanceApp.EmployeeGoalDetail(id, currentYearId);
            var employee = _unitOfWork.PerformanceApp.GetEmployeeByUserId(id);
            var isMidYearEnabled = _unitOfWork.PerformanceApp.GetFeatures().IsMidYearEnabled;
            var isMidYearCompleted = _unitOfWork.PerformanceApp.CheckIsMidYearReviewCompleted(currentYearId);
            var saveGoal = _unitOfWork.PerformanceEmpGoal.SaveCurrentYearGoal(goalViewModel, empDetail, employee, isMidYearEnabled, isMidYearCompleted, actionType);
            if (isMidYearEnabled && isMidYearCompleted)
            {
                var manager = _unitOfWork.PerformanceApp.GetEmployeeByUserId(employee.ManagerId.ToString());
                string message = PerformanceTemplates.AccomplishmentNotification(manager.ManagerName);
                (bool success, string errorMsg) response = await _emailer.SendEmailAsync(manager.ManagerName, manager.ManagerEmail, "Review Accomplishments/Development goals", message);
            }
            return NoContent();
        }

        [HttpGet("getTrainingClasses/{id}")]
        public IActionResult GetTrainingClasses(string id)
        {
            var trainingClasses = new TrainingClassViewModel();
            var currentYearId = _unitOfWork.PerformanceConfig.GetPerformanceYear();
            var empDetail = _unitOfWork.PerformanceApp.EmployeeGoalDetail(id, currentYearId);
            var midYearEnabled = _unitOfWork.PerformanceApp.GetFeatures().IsMidYearEnabled;
            trainingClasses = _unitOfWork.PerformanceEmpTrainingClasses.GetTrainingClasses(empDetail);
            trainingClasses.CheckSaveSubmit = _unitOfWork.PerformanceApp.GetSaveSubmit(empDetail.EmployeeId, currentYearId, midYearEnabled);
            return Ok(trainingClasses);
        }

        [HttpPost("saveTrainingClasses/{id}/{actionType}")]
        public IActionResult Post(string id, [FromBody]TrainingClassViewModel trainingClassViewModel, string actionType)
        {
            var currentYearId = _unitOfWork.PerformanceConfig.GetPerformanceYear();
            var empDetail = _unitOfWork.PerformanceApp.EmployeeGoalDetail(id, currentYearId);
            _unitOfWork.PerformanceEmpTrainingClasses.SaveTrainingClasses(trainingClassViewModel, empDetail, actionType);
            return NoContent();
        }

        [HttpDelete("deleteTrainingClasses/{id}")]
        public IActionResult DeleteTrainingClasses(string id)
        {
            var trainingClasses = _unitOfWork.PerformanceEmpTrainingClasses.Get(Guid.Parse(id));
            if (trainingClasses!=null)
            {
                _unitOfWork.PerformanceEmpTrainingClasses.Remove(trainingClasses);
                _unitOfWork.SaveChanges();
            }            
            return NoContent();
        }
        [HttpGet("getDevelopmentPlan/{id}")]
        public IActionResult GetDevelopmentPlan(string id)
        {
            var devPlan = new CareerDevViewModel();
            var currentYearId = _unitOfWork.PerformanceConfig.GetPerformanceYear();
            var empDetail = _unitOfWork.PerformanceApp.EmployeeGoalDetail(id, currentYearId);
            var midYearEnabled = _unitOfWork.PerformanceApp.GetFeatures().IsMidYearEnabled;
            devPlan = _unitOfWork.PerformanceEmpDevGoal.GetDevelopmentPlan(empDetail);
            devPlan.CheckSaveSubmit = _unitOfWork.PerformanceApp.GetSaveSubmit(empDetail.EmployeeId, currentYearId, midYearEnabled);
            return Ok(devPlan);
        }

        [HttpPost("saveDevelopmentPlan/{id}/{actionType}")]
        public async Task<IActionResult> Post(string id, [FromBody]CareerDevViewModel careerDevViewModel, string actionType)
        {
            var currentYearId = _unitOfWork.PerformanceConfig.GetPerformanceYear();
            var empDetail = _unitOfWork.PerformanceApp.EmployeeGoalDetail(id, currentYearId);
            var midYearEnabled = _unitOfWork.PerformanceApp.GetFeatures().IsMidYearEnabled;
            var employee = _unitOfWork.PerformanceApp.GetEmployeeByUserId(id);
            _unitOfWork.PerformanceEmpDevGoal.SaveDevelopmentPlan(careerDevViewModel, empDetail, actionType, midYearEnabled, employee);
            if (actionType== "submit")
            {
                var manager = _unitOfWork.PerformanceApp.GetEmployeeByUserId(employee.ManagerId.ToString());
                string message = PerformanceTemplates.AccomplishmentNotification(manager.ManagerName);
                (bool success, string errorMsg) response = await _emailer.SendEmailAsync(manager.ManagerName, manager.ManagerEmail, "Review Accomplishments/Development goals", message);
            }            
            return NoContent();
        }

        [HttpGet("getRating/{id}")]
        public IActionResult GetRating(string id)
        {
            var rating = new RatingModel();
            var currentYearId = _unitOfWork.PerformanceConfig.GetPerformanceYear();
            var empDetail = _unitOfWork.PerformanceApp.EmployeeGoalDetail(id, currentYearId);
            var midYearEnabled = _unitOfWork.PerformanceApp.GetFeatures().IsMidYearEnabled;
            rating = _unitOfWork.PerformanceEmpGoal.GetRating(empDetail, midYearEnabled);
            rating.RatingList = _unitOfWork.PerformanceConfigRating.GetRatingList();
            rating.CheckSaveSubmit = _unitOfWork.PerformanceApp.GetSaveSubmit(empDetail.EmployeeId, currentYearId, midYearEnabled);
            return Ok(rating);
        }

        [HttpPost("saveRating/{id}/{actionType}")]
        public IActionResult Post(string id, [FromBody] RatingModel ratingModel, string actionType)
        {
            var currentYearId = _unitOfWork.PerformanceConfig.GetPerformanceYear();
            var empDetail = _unitOfWork.PerformanceApp.EmployeeGoalDetail(id, currentYearId);
            var midYearEnabled = _unitOfWork.PerformanceApp.GetFeatures().IsMidYearEnabled;
            var midYearReviewCompleted = _unitOfWork.PerformanceApp.CheckIsMidYearReviewCompleted(currentYearId);
            _unitOfWork.PerformanceEmpGoal.SaveRating(ratingModel, actionType, empDetail, midYearEnabled, midYearReviewCompleted);
            return NoContent();
        }

        [HttpGet("getPreview/{id}")]
        public IActionResult Preview(string id)
        {
            var preview = new PreviewModel();
            var currentYearId = _unitOfWork.PerformanceConfig.GetPerformanceYear();
            var empDetail = _unitOfWork.PerformanceApp.EmployeeGoalDetail(id, currentYearId);
            var midYearEnabled = _unitOfWork.PerformanceApp.GetFeatures().IsMidYearEnabled;
            preview.empDetail = _unitOfWork.PerformanceApp.GetEmployeeDetail(id);
            preview.goalViewModel = _unitOfWork.PerformanceEmpGoal.GetCurrentYearGoal(empDetail, midYearEnabled);
            preview.trainingClassViewModel = _unitOfWork.PerformanceEmpTrainingClasses.GetTrainingClasses(empDetail);
            preview.careerDevViewModel = _unitOfWork.PerformanceEmpDevGoal.GetDevelopmentPlan(empDetail);
            preview.ratingModel = _unitOfWork.PerformanceEmpGoal.GetRating(empDetail, midYearEnabled);
            var config = _unitOfWork.PerformanceConfig.GetAll().FirstOrDefault();
            string text = "N/A";
            if (config != null)
            {
                text = config.RatingInstructionText;
            }
            preview.ratingModel.InstructionText = text;
            preview.ratingModel.RatingList = _unitOfWork.PerformanceConfigRating.GetRatingList();
            return Ok(preview);
        }
    }
}
