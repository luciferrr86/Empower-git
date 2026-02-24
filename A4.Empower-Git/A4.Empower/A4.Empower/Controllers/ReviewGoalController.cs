using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using A4.BAL;
using DAL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace A4.Empower.Controllers
{

    [Route("api/[controller]")]
    public class ReviewGoalController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;

        public ReviewGoalController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("employeeList/{id}/{page?}/{pageSize?}/{name?}")]
        [Produces(typeof(ReviewGoalViewModel))]
        public IActionResult GetAllEmployees(string id, int? page = null, int? pageSize = null, string name = null)
        {
            try
            {
                var result = new ReviewGoalViewModel();
                var model = new List<EmployeeDetail>();
                var employee = _unitOfWork.Employee.Find(c => c.UserId == id && c.IsActive == true).FirstOrDefault();
                var employeeList = _unitOfWork.Employee.GetEmployeeList(employee.Id, name, Convert.ToInt32(page), Convert.ToInt32(pageSize));
                if(employeeList.Count > 0)
                {
                    foreach (var item in employeeList)
                    {
                        model.Add(new EmployeeDetail {Id=item.Id.ToString(), Name = item.ApplicationUser.FullName, FunctionalDepartment = item.FunctionalGroup.FunctionalDepartment.Name, FunctionalGroup = item.FunctionalGroup.Name, Designation = item.FunctionalDesignation.Name, Title = item.FunctionalTitle.Name, DateReview = DateTime.Now.ToString(),EvaluatorName=item.ManagerName });
                    }
                }
                result.EmployeeDetailList = model;
                result.TotalCount = model.Count;
                return Ok(result);

            }
            catch (Exception ex)
            {

                return BadRequest(ex.GetBaseException().Message);
            }
        }

        [HttpGet("employeeDetails/{id}")]
        public IActionResult GetEmployeeDetails(string id)
        {
            try
            {
                if (id != null)
                {
                    var employeeDetail = new EmployeeDetail();
                    var model = _unitOfWork.PerformanceEmpYearGoal.EmployeeDetails(new Guid(id));
                    if (model != null)
                    {
                        employeeDetail.Id = model.Id.ToString();
                        employeeDetail.Name = model.ApplicationUser.FullName;
                        employeeDetail.FunctionalDepartment = model.FunctionalGroup.FunctionalDepartment.Name;
                        employeeDetail.FunctionalGroup = model.FunctionalGroup.Name;
                        employeeDetail.Title = model.FunctionalTitle.Name;
                        employeeDetail.Designation = model.FunctionalDesignation.Name;
                        employeeDetail.DateReview = DateTime.Now.ToString();
                        employeeDetail.EvaluatorName = model.ManagerName;
                        return Ok(employeeDetail);
                    }
                    return BadRequest("model cannot be null");
                }
                return BadRequest("id cannot be null");
            }
            catch (Exception ex)
            {

                return BadRequest(ex.GetBaseException().Message);
            }
        }

        [HttpGet("getCurrentYearGoal/{id}")]
        public IActionResult GetCurrentYearGoals(string id)
        {
            var userId = _unitOfWork.Employee.Get(new Guid(id)).UserId;
            var currentYearId = _unitOfWork.PerformanceConfig.GetPerformanceYear();
            var empDetail = _unitOfWork.PerformanceApp.EmployeeGoalDetail(userId, currentYearId);
            var empCurrentGoal = new GoalViewModel();
            var midYearEnabled = _unitOfWork.PerformanceApp.GetFeatures().IsMidYearEnabled;
            empCurrentGoal = _unitOfWork.PerformanceEmpGoal.GetCurrentYearGoal(empDetail, midYearEnabled);
            empCurrentGoal.CheckSaveSubmit = _unitOfWork.PerformanceApp.GetSaveSubmit(empDetail.EmployeeId, currentYearId, midYearEnabled);
            return Ok(empCurrentGoal);
        }

        [HttpPost("saveCurrentYearGoal/{id}/{actionType}")]
        public IActionResult SaveCurrentYearGoals(string id,[FromBody]GoalViewModel model, string actionType)
        {
            var userId = _unitOfWork.Employee.Get(new Guid(id)).UserId;
            var currentYearId = _unitOfWork.PerformanceConfig.GetPerformanceYear();
            var empDetail = _unitOfWork.PerformanceApp.EmployeeGoalDetail(userId, currentYearId);
            var employee = _unitOfWork.PerformanceApp.GetEmployeeByUserId(userId);
            var isMidYearEnabled = _unitOfWork.PerformanceApp.GetFeatures().IsMidYearEnabled;
            var isMidYearReviewCompleted = _unitOfWork.PerformanceApp.CheckIsMidYearReviewCompleted(currentYearId);
            var currentGoal = _unitOfWork.PerformanceEmpGoal.SaveMgrCurrentYearGoal(model, empDetail,  isMidYearEnabled, isMidYearReviewCompleted,actionType);
            return Ok(currentGoal);
        }

        [HttpGet("getTrainingClasses/{id}")]
        public IActionResult GetTrainingClasses(string id)
        {
            var trainingClasses = new TrainingClassViewModel();
            var userId = _unitOfWork.Employee.Get(new Guid(id)).UserId;
            var currentYearId = _unitOfWork.PerformanceConfig.GetPerformanceYear();
            var empDetail = _unitOfWork.PerformanceApp.EmployeeGoalDetail(userId, currentYearId);
            var midYearEnabled = _unitOfWork.PerformanceApp.GetFeatures().IsMidYearEnabled;
            trainingClasses = _unitOfWork.PerformanceEmpTrainingClasses.GetTrainingClasses(empDetail);
            var config = _unitOfWork.PerformanceConfig.GetAll().FirstOrDefault();
            string text = "N/A";
            if (config != null)
            {
                text = config.TrainingClassesInstructionText;
            }
            trainingClasses.InstructionText = text;
            trainingClasses.CheckSaveSubmit = _unitOfWork.PerformanceApp.GetSaveSubmit(empDetail.EmployeeId, currentYearId, midYearEnabled);
            return Ok(trainingClasses);
        }

        [HttpGet("getDevelopmentPlan/{id}")]
        public IActionResult GetReviewCareerDevGoal(string id)
        {            
            var devPlan = new CareerDevViewModel();
            var currentYearId = _unitOfWork.PerformanceConfig.GetPerformanceYear();
            var userId = _unitOfWork.Employee.Get(new Guid(id)).UserId;
            var empDetail = _unitOfWork.PerformanceApp.EmployeeGoalDetail(userId, currentYearId);
            var midYearEnabled = _unitOfWork.PerformanceApp.GetFeatures().IsMidYearEnabled;
            devPlan = _unitOfWork.PerformanceEmpDevGoal.GetDevelopmentPlan(empDetail);
            var config = _unitOfWork.PerformanceConfig.GetAll().FirstOrDefault();
            string text = "N/A";
            if (config != null)
            {
                text = config.CareerDevInstructionText;
            }
            devPlan.InstructionText = text;
            devPlan.CheckSaveSubmit = _unitOfWork.PerformanceApp.GetSaveSubmit(empDetail.EmployeeId, currentYearId, midYearEnabled);
            return Ok(devPlan);
        }

        [HttpPost("saveDevelopmentPlan/{id}/{actionType}")]
        public IActionResult Post(string id,string actionType,[FromBody] CareerDevViewModel careerDevViewModel)
        {
            var currentYearId = _unitOfWork.PerformanceConfig.GetPerformanceYear();
            var userId = _unitOfWork.Employee.Get(new Guid(id)).UserId;
            var empDetail = _unitOfWork.PerformanceApp.EmployeeGoalDetail(userId, currentYearId);
            var midYearEnabled = _unitOfWork.PerformanceApp.GetFeatures().IsMidYearEnabled;
            var employee = _unitOfWork.PerformanceApp.GetEmployeeByUserId(id);
            _unitOfWork.PerformanceEmpDevGoal.SaveMgrDevelopmentPlan(careerDevViewModel, empDetail, actionType, midYearEnabled, employee);
            return NoContent();
        }

        [HttpGet("getRating/{id}")]
        public IActionResult GetRating(string id)
        {
            var rating = new RatingModel();
            var currentYearId = _unitOfWork.PerformanceConfig.GetPerformanceYear();
            var userId = _unitOfWork.Employee.Get(new Guid(id)).UserId;
            var empDetail = _unitOfWork.PerformanceApp.EmployeeGoalDetail(userId, currentYearId);
            var midYearEnabled = _unitOfWork.PerformanceApp.GetFeatures().IsMidYearEnabled;
            rating = _unitOfWork.PerformanceEmpGoal.GetRating(empDetail, midYearEnabled);
            var config = _unitOfWork.PerformanceConfig.GetAll().FirstOrDefault();
            string text = "N/A";
            if (config != null)
            {
                text = config.RatingInstructionText;
            }
            rating.InstructionText = text;
            rating.RatingList = _unitOfWork.PerformanceConfigRating.GetRatingList();
            rating.CheckSaveSubmit = _unitOfWork.PerformanceApp.GetSaveSubmit(empDetail.EmployeeId, currentYearId, midYearEnabled);
            return Ok(rating);
        }

        [HttpPost("saveRating/{id}/{actionType}")]
        public IActionResult Post(string id, [FromBody] RatingModel ratingModel, string actionType)
        {
            var currentYearId = _unitOfWork.PerformanceConfig.GetPerformanceYear();
            var employee = _unitOfWork.Employee.Get(new Guid(id));
            var empDetail = _unitOfWork.PerformanceApp.EmployeeGoalDetail(employee.UserId, currentYearId);
            var midYearEnabled = _unitOfWork.PerformanceApp.GetFeatures().IsMidYearEnabled;
            var midYearReviewCompleted = _unitOfWork.PerformanceApp.CheckIsMidYearReviewCompleted(currentYearId);
            _unitOfWork.PerformanceEmpGoal.SaveMgrRating(ratingModel, actionType, empDetail,employee,midYearEnabled, midYearReviewCompleted);
            return NoContent();
        }
    }
}