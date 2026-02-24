using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using A4.BAL;
using A4.Empower.Helpers;
using DAL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace A4.Empower.Controllers
{
    [Authorize(AuthenticationSchemes = OpenIddict.Validation.AspNetCore.OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    public class SubOrdinateLeaveController : Controller
    {
        readonly ILogger _logger;
        readonly IUnitOfWork _unitOfWork;
        readonly IEmailer _emailer;
        private readonly IConfiguration _configuration;
        Guid periodId;
        public SubOrdinateLeaveController(ILogger<SubOrdinateLeaveController> logger, IUnitOfWork unitOfWork, IEmailer emailer, IConfiguration configuration)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _emailer = emailer;
            _configuration = configuration;
            periodId = _unitOfWork.LeavePeriod.GetLeavePeriodId();
        }

        [HttpGet("checkAllConfig/{id?}")]
        public IActionResult CheckAllConfig(string id)
        {
            try
            {

                if (id.checkString())
                {
                    Guid managerId = _unitOfWork.Employee.GetEmployeeId(id);
                    if (managerId != Guid.Empty)
                    {
                        bool check = _unitOfWork.LeaveModule.AllEmployeeConfig(managerId);
                        if (check == true)
                        {
                            return NoContent();
                        }
                        else
                        {
                            return Ok(1);
                        }


                    }
                }
                return BadRequest("Id cannot be null");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.GetBaseException().Message);
            }
        }

        [HttpGet("list/{id?}/{page?}/{pageSize?}/{name?}")]
        [Produces(typeof(SubordinateLeaveListViewModel))]
        public IActionResult GetSubordinateLeaveList(string id, int? page = null, int? pageSize = null, string name = null)
        {
            var result = new SubordinateLeaveListViewModel();
            var model = new List<SubordinateLeaveListModel>();
            int excludeHolidayCount = 0;
            int weekendDaysCount = 0;
            int noOfDays = 0;


            Guid employeeId = _unitOfWork.Employee.Find(c => c.UserId == id && c.IsActive == true).Select(m => m.Id).FirstOrDefault();
            if (periodId != Guid.Empty)
            {
                if (employeeId != Guid.Empty)
                {
                    var allLeave = _unitOfWork.EmployeeLeaveDetail.GetSubOrdinateLeavesList(employeeId, periodId, name, Convert.ToInt32(page), Convert.ToInt32(pageSize));
                    if (allLeave.Count() > 0)
                    {

                        foreach (var item in allLeave)
                        {
                            excludeHolidayCount = _unitOfWork.LeaveManagement.ExcludeHolidays(item.LeaveStartDate, item.LeaveEndDate);
                            weekendDaysCount = _unitOfWork.LeaveManagement.ExcludeWeekends(item.LeaveStartDate, item.LeaveEndDate);
                            noOfDays = item.LeaveEndDate.Subtract(item.LeaveStartDate).Days + 1 - excludeHolidayCount - weekendDaysCount;
                            model.Add(new SubordinateLeaveListModel { LeaveDeatilId = item.Id.ToString(), StartDate = item.LeaveStartDate, EndDate = item.LeaveEndDate, Status = item.LeaveStatus.Name, NoOfDays = noOfDays.ToString(), EmployeeName = item.Employee.ApplicationUser.FullName });
                        }
                    }

                    result.TotalCount = allLeave.TotalCount;
                    result.SubordinateLeaveListModel = model;
                    return Ok(result);
                }
                return BadRequest("employeeId is empty");
            }
            return BadRequest("leave period is not active");
        }

        [HttpGet("listemployee/{id}/{page?}/{pageSize?}/{name?}")]
        [Produces(typeof(LeaveHrViewModel))]
        public IActionResult GetAllEmployee(string id, int? page = null, int? pageSize = null, string name = null)
        {
            try
            {

                int remainingLeaves = 0;
                var result = new LeaveHrViewModel();
                var viewModel = new List<LeaveHrModel>();
                Guid employeeId = _unitOfWork.Employee.Find(c => c.UserId == id && c.IsActive == true).Select(m => m.Id).FirstOrDefault();
                if (employeeId != null)
                {
                    var employee = _unitOfWork.Employee.GetEmployeeList(employeeId, name, Convert.ToInt32(page), Convert.ToInt32(pageSize));
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
                    return Ok(result);
                }
                return BadRequest("employeeId cannot be null");
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
                if (id != null)
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

        [HttpGet("subordinateleaveDetail/{id?}")]
        [Produces(typeof(SubordinateLeaveDetailModel))]
        public IActionResult GetSubOrdinateLeaveDetail(string id)
        {

            Guid leaveDetailId = new Guid(id);
            if (leaveDetailId != Guid.Empty)
            {
                int excludeHolidayCount = 0;
                int weekendDaysCount = 0;
                int noOfDays = 0;
                var leaveDeatil = _unitOfWork.EmployeeLeaveDetail.GetSubOrdinateLeaveDetail(leaveDetailId);
                if (leaveDeatil != null)
                {
                    excludeHolidayCount = _unitOfWork.LeaveManagement.ExcludeHolidays(leaveDeatil.StartDate, leaveDeatil.EndDate);
                    weekendDaysCount = _unitOfWork.LeaveManagement.ExcludeWeekends(leaveDeatil.StartDate, leaveDeatil.EndDate);
                    noOfDays = leaveDeatil.EndDate.Subtract(leaveDeatil.StartDate).Days + 1 - excludeHolidayCount - weekendDaysCount;
                    leaveDeatil.NoOfDays = noOfDays.ToString();
                    return Ok(leaveDeatil);
                }
                return BadRequest(" leaveDeatil record is empty");
            }
            else
            {
                return BadRequest(" employeeId cannot be null");
            }

        }

        [HttpPost("approveReject")]
        public async Task<IActionResult> ApproveReject([FromBody]SubordinateLeaveDetailModel model)
        {

            if (ModelState.IsValid)
            {
                if (model == null)
                    return BadRequest($"{nameof(model)} cannot be null");

                int noOfDays = 0;
                string leaveStatus = "";
                var leaveDetail = _unitOfWork.EmployeeLeaveDetail.Find(m => m.Id == new Guid(model.Id) && m.IsActive == true).FirstOrDefault();
                if (leaveDetail != null)
                {
                    var status = _unitOfWork.LeaveStatus.Get(leaveDetail.LeaveStatusId);
                    if (status != null)
                    {
                        if (status.Name == "Pending")
                        {
                            var entitlement = _unitOfWork.EmployeeEntitlement.Get(leaveDetail.LeavesEntitlementId);
                            if (entitlement != null)
                            {
                                var checkLeaves = _unitOfWork.SubordinateLeave.CheckLeaves(leaveDetail.LeavesEntitlementId);
                                int reamingleaves = checkLeaves.Item4;
                                noOfDays = _unitOfWork.LeaveManagement.GetCalculateNoOfDays(model.StartDate, model.EndDate);
                             //   if (noOfDays <= reamingleaves && noOfDays != 0)
                              //  {

                                    leaveDetail.Approvedby = new Guid(model.Approvedby);
                                    leaveDetail.ManagerComment = model.ManagerComment;
                                    var empDetail = _unitOfWork.Employee.GetEmployeedetailsByemployeeId(leaveDetail.EmployeeId);

                                    if (empDetail != null)
                                    {
                                        if (model.ButtonType == "1")
                                        {
                                            leaveDetail.LeaveStatusId = _unitOfWork.LeaveStatus.Find(c => c.Name == "Approved").Select(m => m.Id).FirstOrDefault();
                                            _unitOfWork.EmployeeLeaveDetail.Update(leaveDetail);
                                            _unitOfWork.SaveChanges();

                                            entitlement.Approved = (checkLeaves.Item2 + noOfDays).ToString();
                                            _unitOfWork.EmployeeEntitlement.Update(entitlement);
                                            _unitOfWork.SaveChanges();

                                            leaveStatus = "Approved";
                                        }
                                        else if (model.ButtonType == "2")
                                        {
                                            leaveDetail.LeaveStatusId = _unitOfWork.LeaveStatus.Find(c => c.Name == "Rejected").Select(m => m.Id).FirstOrDefault();
                                            _unitOfWork.EmployeeLeaveDetail.Update(leaveDetail);
                                            _unitOfWork.SaveChanges();

                                            entitlement.Rejected = (checkLeaves.Item3 + noOfDays).ToString();
                                            _unitOfWork.EmployeeEntitlement.Update(entitlement);
                                            _unitOfWork.SaveChanges();

                                            leaveStatus = "Rejected";

                                        }

                                        string hrName = _configuration.GetValue<string>("HrDetail:Name");
                                        string hrEmail = _configuration.GetValue<string>("HrDetail:Email");
                                        ///mail send to employee 
                                        string strtDate = model.StartDate.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                                        string endDate = model.EndDate.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                                        string message = LeaveTemplates.LeaveApproveRejectedToEmployee(empDetail.ApplicationUser.FullName, empDetail.ManagerName, strtDate, endDate, leaveStatus);
                                        (bool success, string errorMsg) response = await _emailer.SendEmailAsync(empDetail.ApplicationUser.FullName, empDetail.ApplicationUser.Email, "Leave Application", message);

                                        //mail send to hr 
                                        string message1 = LeaveTemplates.LeaveApproveRejectedToHr(hrName, empDetail.ApplicationUser.FullName, strtDate, endDate, empDetail.ManagerName, leaveStatus);
                                        string subject = "Leave Application " + leaveStatus;
                                        (bool success, string errorMsg) response1 = await _emailer.SendEmailAsync(hrName, hrEmail, subject, message1);
                                        return NoContent();


                                    }
                                    else
                                    {
                                        return BadRequest("employee Detail not found");
                                    }
                              //  }
                            }
                        }
                        else
                        {
                            return BadRequest("This leave has already approved or Rejected");
                        }
                    }
                }
                else
                {
                    return BadRequest("leaveDetails Can not be null");
                }

            }
            return BadRequest(ModelState);
        }

        [HttpGet("managercalender/{id}")]
        [Produces(typeof(List<LeaveCalenderModel>))]
        public IActionResult GetListOfCalenders(string id)
        {
            try
            {
                var model = new List<LeaveCalenderModel>();
                Guid employeeId = _unitOfWork.Employee.Find(c => c.UserId == id && c.IsActive == true).Select(m => m.Id).FirstOrDefault();
                if (periodId != Guid.Empty)
                {
                    if (employeeId != Guid.Empty)
                    {
                        var BelowEmployee = _unitOfWork.Employee.Find(c => c.ManagerId == employeeId && c.IsActive == true).ToList();
                        if (BelowEmployee.Count() > 0)
                        {
                            foreach (var employee in BelowEmployee)
                            {
                                var leaveDetail = _unitOfWork.EmployeeLeaveDetail.GetListofEmployeeLeave(employee.Id, periodId);
                                if (leaveDetail.Count > 0)
                                {
                                    foreach (var item in leaveDetail)
                                    {
                                        if (item.LeaveStatus.Name != "Cancelled" || item.LeaveStatus.Name != "Rejected")
                                        {
                                            var leaveDeatil = "";
                                            string backgroundColor = "";
                                            if (item.LeaveStatus.Name == "Pending")
                                            {
                                                backgroundColor = "#FF4500";
                                                leaveDeatil = item.Employee.ApplicationUser.FullName + " Leave Approval Pending" + item.ReasonForApply;
                                            }
                                            else if (item.LeaveStatus.Name == "Approved")
                                            {
                                                backgroundColor = "#32CD32";
                                                leaveDeatil = item.Employee.ApplicationUser.FullName + " Leave Approved" + item.ReasonForApply;
                                            }


                                            model.Add(new LeaveCalenderModel
                                            {
                                                AllDay = true,
                                                Start = item.LeaveStartDate,
                                                End = item.LeaveEndDate.AddDays(1),
                                                Status = item.LeaveStatus.Name,
                                                BackgroundColor = backgroundColor,
                                                Tooltip = leaveDeatil,
                                                Title = leaveDeatil
                                            });
                                        }
                                    }
                                }
                            }
                        }
                        var holidayList = _unitOfWork.LeaveHolidayList.Find(c => c.IsActive && c.LeavePeriodId == periodId).ToList();
                        if (holidayList.Count > 0)
                        {
                            foreach (var item in holidayList)
                            {
                                model.Add(new LeaveCalenderModel
                                {
                                    AllDay = true,
                                    Start = item.Holidaydate,
                                    Title = item.Name,
                                    BackgroundColor = "#00FFFF"
                                });
                            }
                        }
                        return Ok(model);

                    }
                    return BadRequest("employee id cannot be null");
                }
                return BadRequest("leave period is not active");
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

    }
}