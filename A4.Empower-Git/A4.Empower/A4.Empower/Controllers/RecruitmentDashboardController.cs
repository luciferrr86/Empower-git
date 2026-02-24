using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using A4.BAL;
using A4.Empower.ViewModels;
using DAL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace A4.Empower.Controllers
{
    [Authorize(AuthenticationSchemes = OpenIddict.Validation.AspNetCore.OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)]
    [Route("api/[Controller]")]
    public class RecruitmentDashboardController : Controller
    {
        readonly ILogger _logger;
        readonly IUnitOfWork _unitOfWork;
        public RecruitmentDashboardController(ILogger<DashboardController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        [HttpGet("recruitmentData")]
        public IActionResult GetRecruitmentData()
        {
            try
            {
                var result = new RecruitmentDashboardViewModel();
                var jobPublishedList = _unitOfWork.Job.GetAllJobPublishedList();
                var viewModel = new List<JobPublishedViewModel>();
                var jobCount = _unitOfWork.JobVacancy.GetAll().Where(m => m.bIsPublished == true).Count();
                if (jobPublishedList.Count > 0)
                {
                    foreach (var item in jobPublishedList)
                    {
                        viewModel.Add(new JobPublishedViewModel { JobId = item.Id.ToString(), JobTitle = item.JobTitle });
                    }

                }
                return Ok(viewModel);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.GetBaseException().Message);
            }
        }
    }
}