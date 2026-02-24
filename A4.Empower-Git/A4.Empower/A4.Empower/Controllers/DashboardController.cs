using System;
using System.Collections.Generic;
using System.Linq;
using A4.Empower.Helpers;
using A4.Empower.ViewModels;
using DAL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace A4.Empower.Controllers
{
    [Authorize(AuthenticationSchemes = OpenIddict.Validation.AspNetCore.OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    public class DashboardController : Controller
    {
        readonly ILogger _logger;
        readonly IUnitOfWork _unitOfWork;
        readonly IEmailer _emailer;
        public DashboardController(ILogger<DashboardController> logger, IUnitOfWork unitOfWork, IEmailer emailer)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _emailer = emailer;
        }

        [HttpGet("leaveTaskList/{id?}")]
        public IActionResult GetLeaveTaskList(string id)
        {
            var listModel = new List<DashboardViewModel>();
            if (id.checkString())
            {
                Guid leaveStatusId = _unitOfWork.LeaveStatus.Find(m => m.Name == "Pending").Select(m => m.Id).FirstOrDefault();
                Guid employeeId = _unitOfWork.Employee.GetEmployeeId(id);
                if (leaveStatusId != null && employeeId != null)
                {
                    var leaveDetail = _unitOfWork.EmployeeLeaveDetail.Find(m => m.ManagerId == employeeId && m.IsActive == true && m.IsSubmitted == true && m.LeaveStatusId == leaveStatusId).ToList();
                    if (leaveDetail.Count > 0)
                    {
                        foreach (var detail in leaveDetail)
                        {
                            var empDetail = _unitOfWork.Employee.GetEmployeedetailsByemployeeId(detail.EmployeeId);
                            if (empDetail != null)
                            {
                                listModel.Add(new DashboardViewModel { EmployeeId = detail.EmployeeId.ToString(), TaskName = "Leave applied by " + empDetail.ApplicationUser.FullName });
                            }
                        }
                        return Ok(listModel);
                    }
                    else
                    {
                        return Ok(listModel);

                    }

                }
                else
                {
                    return Ok(listModel);
                }
            }
            else
            {
                return Ok(listModel);
            }

        }

        [HttpGet("timesheetTaskList/{id?}")]
        public IActionResult GetTimesheetTaskList(string id)
        {
            var listModel = new List<DashboardViewModel>();
            if (id.checkString())
            {
                Guid employeeId = _unitOfWork.Employee.GetEmployeeId(id);
                if (employeeId != null)
                {
                    var userSpan = _unitOfWork.TimesheetUserSpan.Find(m => m.bIsUserSubmit == false && m.IsActive == true && m.bIsManagerApprove == false && m.EmployeeId == employeeId).ToList();
                    if (userSpan.Count > 0)
                    {
                        foreach (var item in userSpan)
                        {
                            var timesheetUserDetail = _unitOfWork.TimesheetUserDetail.Find(m => m.TimesheetUserSpanId == item.Id).ToList();
                            if (timesheetUserDetail.Count > 0)
                            {
                                var userSchedule = _unitOfWork.TimesheetUserSchedule.Find(m => m.EmployeeId == employeeId).FirstOrDefault();
                                if (userSchedule != null)
                                {
                                    if (userSchedule.ScheduleType == "Daily")
                                    {
                                        var lstSavedNotSubmited = timesheetUserDetail.Where(x => x.bIsUserSaved == true && x.bIsUserSubmit == false).ToList();
                                        if (lstSavedNotSubmited.Count > 0)
                                        {
                                            listModel.Add(new DashboardViewModel { TaskName = "Submit your Timesheet for " + item.WeekStartDate.ToString("MM-dd-yyyy") + " to " + item.WeekEndDate.ToString("MM-dd-yyyy"), EmployeeId = employeeId.ToString(), IsManager = false });
                                        }
                                    }
                                    else
                                    {
                                        if (timesheetUserDetail.Any(x => x.bIsUserSubmit == false))
                                            listModel.Add(new DashboardViewModel { TaskName = "Submit your Timesheet for week  " + item.WeekStartDate.ToString("MM-dd-yyyy") + " to " + item.WeekEndDate.ToString("MM-dd-yyyy"), EmployeeId = employeeId.ToString(), IsManager = false });
                                    }
                                }
                            }
                        }
                    }

                    var timesheetuser = _unitOfWork.Employee.Find(m => m.ManagerId == employeeId && m.IsActive == true).ToList();
                    if (timesheetuser.Count > 0)
                    {
                        foreach (var user in timesheetuser)
                        {
                            var timeSheetSpan = _unitOfWork.TimesheetUserSpan.Find(m => m.bIsManagerApprove == false && m.EmployeeId == user.Id && m.IsActive == true).ToList();
                            if (timeSheetSpan.Count > 0)
                            {
                                foreach (var timespan in timeSheetSpan)
                                {
                                    var timesheetUserDetail = _unitOfWork.TimesheetUserDetail.Find(m => m.TimesheetUserSpanId == timespan.Id);
                                    var empdetails = _unitOfWork.Employee.GetEmployeedetails(user.Id);
                                    if (empdetails != null)
                                    {
                                        if (timesheetUserDetail.Any(x => x.bManagerApproved == false))
                                        {
                                            var lstSubmittedDays = timesheetUserDetail.Where(x => x.TimeSheetDate.Date <= DateTime.Now.Date).ToList();
                                            if (!lstSubmittedDays.Any(m => m.bIsUserSubmit == false))
                                            {
                                                listModel.Add(new DashboardViewModel { EmployeeId = user.Id.ToString(), TaskName = "Approve Timesheet applied by " + empdetails.ApplicationUser.FullName + " for week  " + timespan.WeekStartDate.ToString("MM-dd-yyyy") + "to" + timespan.WeekEndDate.ToString("MM-dd-yyyy"), IsManager = true });
                                            }
                                        }
                                    }
                                }

                            }
                        }
                    }
                    return Ok(listModel);
                }
                else
                {
                    return Ok(listModel);
                }
            }
            else
            {
                return Ok(listModel);
            }

        }

        [HttpGet("performanceTaskList/{id?}")]
        public IActionResult GetPerformanceTaskList(string id)
        {
            var listModel = new List<DashboardViewModel>();
            if (id.checkString())
            {
                var yearId = _unitOfWork.PerformanceConfig.GetPerformanceYear();
                if (yearId == Guid.Empty)
                {
                    return Ok(listModel);
                }
                else
                {
                    var taskList = _unitOfWork.PerformanceApp.GetPerformanceTaskList(id, yearId);
                    return Ok(listModel);

                }

            }
            else
            {
                return Ok(listModel);
            }
        }

        [HttpGet("searchEmployee/{term?}")]
        public IActionResult SearchEmployee(string term)
        {
            try
            {
                var employee = _unitOfWork.Employee.SearchEmployee(term);
                return Ok(employee);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}