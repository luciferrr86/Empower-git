using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using A4.BAL;
using A4.DAL.Entites;
using A4.Empower.ViewModels;
using DAL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace A4.Empower.Controllers
{
    [Produces("application/json")]
    [Route("api/PerformanceConfig")]
    public class PerformanceConfigController : Controller
    {
        readonly IUnitOfWork _unitOfWork;
        public PerformanceConfigController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("get")]
        public IActionResult GetConfig()
        {
            var result = new PerformanceConfigViewModel();
            var feedbackList = new List<PerformanceConfigFeebackViewModel>();
            var ratingList = new List<PerformanceConfigRatingViewModel>();
            var getconfig = _unitOfWork.PerformanceConfig.GetAll().FirstOrDefault();
            if (getconfig != null)
            {
                result.Id = getconfig.Id.ToString();
                result.CareerDevInstructionText = getconfig.CareerDevInstructionText;
                result.CurrentYearInstructionText = getconfig.CurrentYearInstructionText;
                result.DeltaInstructionText = getconfig.DeltaInstructionText;
                result.IsPerformanceStart = getconfig.IsPerformanceStart;
                result.MyGoalInstructionText = getconfig.MyGoalInstructionText;
                result.NextYearInstructionText = getconfig.NextYearInstructionText;
                result.PlusesInstructionText = getconfig.PlusesInstructionText;
                result.RatingInstructionText = getconfig.RatingInstructionText;
                result.TrainingClassesInstructionText = getconfig.TrainingClassesInstructionText;
            }

            var getfeedback = _unitOfWork.PerformanceConfigFeedback.GetAll();
            if (getfeedback.Count() > 0)
            {
                foreach (var item in getfeedback)
                {
                    feedbackList.Add(new PerformanceConfigFeebackViewModel { Id = item.Id.ToString(), LabelText = item.LabelText, Description = item.Description });
                }
            }
            result.performanceConfigFeebackViewModel = feedbackList;
            var getrating = _unitOfWork.PerformanceConfigRating.GetAll();
            if (getrating.Count() > 0)
            {
                foreach (var item in getrating)
                {
                    ratingList.Add(new PerformanceConfigRatingViewModel { Id = item.Id.ToString(), RatingName = item.RatingName, RatingDescription = item.RatingDescription });
                }
            }
            result.performanceConfigRatingViewModel = ratingList;
            return Ok(result);
        }

        [HttpPut("save")]
        public IActionResult Post([FromBody]PerformanceConfigViewModel viewModel)
        {
            if (viewModel.Id != null)
            {
                var config = _unitOfWork.PerformanceConfig.Get(new Guid(viewModel.Id));
                if (config != null)
                {
                    config.CareerDevInstructionText = viewModel.CareerDevInstructionText;
                    config.CurrentYearInstructionText = viewModel.CurrentYearInstructionText;
                    config.DeltaInstructionText = viewModel.DeltaInstructionText;
                    config.IsPerformanceStart = viewModel.IsPerformanceStart;
                    config.MyGoalInstructionText = viewModel.MyGoalInstructionText;
                    config.NextYearInstructionText = viewModel.NextYearInstructionText;
                    config.PlusesInstructionText = viewModel.PlusesInstructionText;
                    config.RatingInstructionText = viewModel.RatingInstructionText;
                    config.TrainingClassesInstructionText = viewModel.TrainingClassesInstructionText;
                    _unitOfWork.PerformanceConfig.Update(config);
                }
            }

            else
            {
                var configuration = new PerformanceConfig();
                configuration.CareerDevInstructionText = viewModel.CareerDevInstructionText;
                configuration.CurrentYearInstructionText = viewModel.CurrentYearInstructionText;
                configuration.DeltaInstructionText = viewModel.DeltaInstructionText;
                configuration.IsPerformanceStart = viewModel.IsPerformanceStart;
                configuration.MyGoalInstructionText = viewModel.MyGoalInstructionText;
                configuration.NextYearInstructionText = viewModel.NextYearInstructionText;
                configuration.PlusesInstructionText = viewModel.PlusesInstructionText;
                configuration.RatingInstructionText = viewModel.RatingInstructionText;
                configuration.TrainingClassesInstructionText = viewModel.TrainingClassesInstructionText;
                _unitOfWork.PerformanceConfig.Add(configuration);
            }
            if (viewModel.performanceConfigFeebackViewModel.Count > 0)
            {
                foreach (var item in viewModel.performanceConfigFeebackViewModel)
                {
                    if (item.Id != "")
                    {
                        var feedback = _unitOfWork.PerformanceConfigFeedback.Get(new Guid(item.Id));
                        if (feedback != null)
                        {
                            feedback.LabelText = item.LabelText;
                            feedback.Description = item.Description;
                            _unitOfWork.PerformanceConfigFeedback.Update(feedback);
                        }
                    }
                    else
                    {
                        var feedback = new PerformanceConfigFeedback();
                        feedback.LabelText = item.LabelText;
                        feedback.Description = item.Description;
                        feedback.IsActive = true;
                        _unitOfWork.PerformanceConfigFeedback.Add(feedback);
                    }

                }
            }
            if (viewModel.performanceConfigRatingViewModel.Count > 0)
            {
                foreach (var item in viewModel.performanceConfigRatingViewModel)
                {
                    if (item.Id != "")
                    {
                        var rating = _unitOfWork.PerformanceConfigRating.Get(new Guid(item.Id));
                        if (rating != null)
                        {
                            rating.RatingName = item.RatingName;
                            rating.RatingDescription = item.RatingDescription;
                            _unitOfWork.PerformanceConfigRating.Update(rating);
                        }
                    }

                    else
                    {
                        var rating = new PerformanceConfigRating();
                        rating.RatingName = item.RatingName;
                        rating.RatingDescription = item.RatingDescription;
                        rating.IsActive = true;
                        _unitOfWork.PerformanceConfigRating.Add(rating);
                    }

                }
            }           
            return NoContent();
        }

        [HttpDelete("deleteFeedback/{id}")]
        public IActionResult FeedbackDelete(string id)
        {
            var feedback = _unitOfWork.PerformanceConfigFeedback.Get(new Guid(id));
            if (feedback != null)
            {
                _unitOfWork.PerformanceConfigFeedback.Remove(feedback);
            }
            return NoContent();
        }

        [HttpDelete("deleteRating/{id}")]
        public IActionResult RatingDelete(string id)
        {
            var rating = _unitOfWork.PerformanceConfigRating.Get(new Guid(id));
            if (rating != null)
            {
                _unitOfWork.PerformanceConfigRating.Remove(rating);
            }
            return NoContent();
        }
    }
}
