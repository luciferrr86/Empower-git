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
    [Route("api/[controller]")]
    public class LeaveHrViewController : Controller
    {
        readonly ILogger _logger;
        readonly IUnitOfWork _unitOfWork;
        Guid periodId;

        public LeaveHrViewController(ILogger<LeaveHrViewController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            periodId = _unitOfWork.LeavePeriod.GetLeavePeriodId();
        }

        [HttpGet("list/{page?}/{pageSize?}/{name?}")]
        [Produces(typeof(LeaveHrViewModel))]
        public IActionResult GetAllEmployee(int? page = null, int? pageSize = null, string name = null)
        {
            try
            {
                int remainingLeaves = 0;
                var result = new LeaveHrViewModel();
                var viewModel = new List<LeaveHrModel>();
                var isConfigSet = _unitOfWork.LeaveHrView.chkConfigSet();
                if (isConfigSet)
                {
                    var employee = _unitOfWork.LeaveHrView.GetAllEmployees(name, Convert.ToInt32(page), Convert.ToInt32(pageSize));
                    if (employee.Count() > 0)
                    {
                        foreach (var item in employee)
                        {
                            var leaveEntitlement = _unitOfWork.LeaveHrView.GetLeaveEntitlement(item.Id, periodId);
                            if (leaveEntitlement.Count() > 0)
                            {
                                int leavesPerYear = 0;
                                int approved = 0;
                                foreach (var item1 in leaveEntitlement)
                                {
                                    leavesPerYear = leavesPerYear + Convert.ToInt32(item1.LeaveRules.LeavesPerYear);
                                    approved = approved + Convert.ToInt32(item1.Approved);
                                }
                                remainingLeaves = leavesPerYear - approved;
                                viewModel.Add(new LeaveHrModel { EmployeeId = item.Id.ToString(), EmployeeName = item.ApplicationUser.FullName, Department = item.FunctionalGroup.FunctionalDepartment.Name, RemainingLeave = remainingLeaves });
                            }
                            else
                            {
                                viewModel.Add(new LeaveHrModel { EmployeeId = item.Id.ToString(), EmployeeName = item.ApplicationUser.FullName, Department = item.FunctionalGroup.FunctionalDepartment.Name, RemainingLeave = 0 });
                            }

                        }
                    }
                    result.LeaveEmployeeList = viewModel;
                    result.TotalCount = employee.TotalCount;
                }
                result.IsConfigSet = isConfigSet;               
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.GetBaseException().Message);
            }
        }

        [HttpGet("leaveDetails/{id}")]
        public IActionResult GetEmployeeLeaveDetails(string id)
        {
            try
            {
                if(id != null)
                {
                    int remainingLeaves = 0;
                    int leavesPerYear = 0;
                    int approved = 0;
                    var result = new List<EmployeeLeaveDetails>();
                    var employee = _unitOfWork.LeaveHrView.EmpLeaves(Guid.Parse(id));
                    if (employee != null)
                    {
                        var leaveDetails = _unitOfWork.LeaveHrView.EmployeeLeaveDetails(employee.EmployeeId,periodId);
                        if (leaveDetails.Count() > 0)
                        {
                            foreach (var item in leaveDetails)
                            {
                                leavesPerYear = Convert.ToInt32(item.LeaveRules.LeavesPerYear);
                                approved = Convert.ToInt32(item.Approved);
                                remainingLeaves = leavesPerYear - approved;
                                result.Add(new EmployeeLeaveDetails { LeaveType = item.LeaveRules.LeaveType.Name, AllottedLeaves = leavesPerYear, TakenLeaves = approved, RemainingLeave = remainingLeaves });
                            }
                        }

                    }
                    return Ok(result);
                }
                return BadRequest("id cannot be null");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.GetBaseException().Message);
            }

        }
    }
}