using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using A4.BAL;
using A4.DAL.Entites;
using A4.Empower.Helpers;
using A4.Empower.ViewModels;
using DAL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace A4.Empower.Controllers
{
    [Authorize(AuthenticationSchemes = OpenIddict.Validation.AspNetCore.OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    public class LeaveManagementController : Controller
    {
        readonly ILogger _logger;
        readonly IUnitOfWork _unitOfWork;
        readonly IEmailer _emailer;
        private readonly IConfiguration _configuration;
        Guid periodId;
        public LeaveManagementController(ILogger<LeaveManagementController> logger, IUnitOfWork unitOfWork, IEmailer emailer, IConfiguration configuration)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _emailer = emailer;
            _configuration = configuration;
            periodId = _unitOfWork.LeavePeriod.GetLeavePeriodId();
        }

        [HttpGet("list/{userId?}/{page?}/{pageSize?}/{name?}")]
        [Produces(typeof(EmployeeLeaveListViewModel))]
        public IActionResult GetAllEmployeeLeave(string userId, int? page = null, int? pageSize = null, string name = null)
        {
            try
            {
                var result = new EmployeeLeaveListViewModel();
                var model = new List<EmployeeLeaveListModel>();
                bool IscancelPeriodEnd = false;
                Guid employeeId = _unitOfWork.Employee.Find(c => c.UserId == userId && c.IsActive == true).Select(m => m.Id).FirstOrDefault();
                if (employeeId != Guid.Empty && periodId != Guid.Empty)
                {
                    var allLeave = _unitOfWork.EmployeeLeaveDetail.GetAllEmployeeLeave(employeeId, periodId, name, Convert.ToInt32(page), Convert.ToInt32(pageSize));
                    if (allLeave.Count() > 0)
                    {
                        foreach (var item in allLeave)
                        {

                            if (item.LeaveStatus.Name == "Approved" && item.LeaveStartDate.Date > DateTime.Now.Date)
                            {
                                IscancelPeriodEnd = true;
                            }
                            else
                            {
                                IscancelPeriodEnd = false;
                            }

                            model.Add(new EmployeeLeaveListModel { LeaveDeatilId = item.Id.ToString(), LeaveType = item.Name, StartDate = item.LeaveStartDate, EndDate = item.LeaveEndDate, IscancelPeriodEnd = IscancelPeriodEnd, Status = item.LeaveStatus.Name, IsSubmitted = item.IsSubmitted, IsSave = item.IsSave });
                        }
                    }
                    result.TotalCount = allLeave.TotalCount;
                    result.EmployeeLeaveListModel = model;
                    return Ok(result);
                }
                return BadRequest("property cannot be null");
            }
            catch (Exception ex)
            {

                return BadRequest(ex.GetBaseException().Message);
            }
        }

        [HttpGet("leaveDetail/{id?}")]
        [Produces(typeof(EmployeeLeaveDetailModel))]
        public IActionResult GetEmployeeLeaveDetail(string id)
        {

            Guid leaveDetailId = new Guid(id);
            if (leaveDetailId != Guid.Empty)
            {
                int excludeHolidayCount = 0;
                int weekendDaysCount = 0;
                int noOfDays = 0;
                var leaveDeatil = _unitOfWork.EmployeeLeaveDetail.GetEmployeeLeaveDetail(leaveDetailId);
                if (leaveDeatil != null)
                {
                    excludeHolidayCount = _unitOfWork.LeaveManagement.ExcludeHolidays(leaveDeatil.StartDate, leaveDeatil.EndDate);
                    weekendDaysCount = _unitOfWork.LeaveManagement.ExcludeWeekends(leaveDeatil.StartDate, leaveDeatil.EndDate);
                    noOfDays = leaveDeatil.EndDate.Subtract(leaveDeatil.StartDate).Days + 1 - excludeHolidayCount - weekendDaysCount;
                    leaveDeatil.noOfDays = noOfDays.ToString();
                    return Ok(leaveDeatil);
                }
                return BadRequest(" leaveDeatil record is empty");
            }
            else
            {
                return BadRequest(" employeeId cannot be null");
            }

        }

        [HttpGet("createEntitlement/{id}")]
        [Produces(typeof(MyLeaveModel))]
        public IActionResult CreateEntitlement(string id)
        {
            try
            {

                var model = new MyLeaveModel();
                bool check = _unitOfWork.LeaveModule.EmployeeLeaveConfig(id);
                if (check)
                {

                    var employee = _unitOfWork.Employee.Find(c => c.UserId == id && c.IsActive == true).FirstOrDefault();
                    if (periodId != Guid.Empty && employee.Id != Guid.Empty && employee.BandId != Guid.Empty)
                    {
                        var empleaves = _unitOfWork.EmployeeLeaves.GetEmployeeLeavesByEmpId(employee.Id, periodId);
                        if (empleaves == null)
                        {
                            Guid employeeLeavesId = SaveEmployeeLeaves(employee.Id);
                            if (employeeLeavesId != Guid.Empty)
                            {
                                var entitlement = _unitOfWork.EmployeeEntitlement.GetEmployeeEntitlement(employeeLeavesId);
                                if (entitlement.Count == 0)
                                {
                                    SaveEntitlement(employee.BandId, employeeLeavesId);
                                }

                            }
                        }

                        model.ddlleaveType = GetLeaveTypeList(periodId);
                        model.IsSet = true;
                        return Ok(model);
                    }
                    else
                    {
                        return BadRequest("model cannot be null");
                    }
                }
                else
                {
                    model.IsSet = false;
                    return Ok(model);
                }
            }
            catch (Exception ex)
            {

                return BadRequest(ex.GetBaseException().Message);
            }
        }

        [HttpGet("listLeaveInfo/{id}")]
        [Produces(typeof(LeaveEntitlementModel))]
        public IActionResult GetAllLeaveInfo(string id)
        {
            try
            {
                int reamingLeaves = 0;
                int allleaves = 0;
                int allreamingleaves = 0;
                int allrejectedleaves = 0;
                int allapproved = 0;
                var model = new LeaveEntitlementModel();
                var employee = _unitOfWork.Employee.Find(c => c.UserId == id && c.IsActive == true).FirstOrDefault();
                if (employee != null)
                {
                    var lstleaveEntitle = _unitOfWork.LeaveManagement.GetAllLeaveEntitlement(employee.Id, periodId);
                    ////var lstleaveEntitleforMonth = _unitOfWork.LeaveManagement.GetAllLeaveForMonth(employee.Id, DateTime.Today.Month, DateTime.Today.Year);
                    if (lstleaveEntitle.Count() > 0)
                    {
                        foreach (var item in lstleaveEntitle)
                        {
                            reamingLeaves = Convert.ToInt32(item.NoOfDays) - Convert.ToInt32(item.Approved);
                            item.Remaining = reamingLeaves.ToString();
                            allleaves = allleaves + Convert.ToInt32(item.NoOfDays);
                            allrejectedleaves = allrejectedleaves + Convert.ToInt32(item.Rejected);
                            allapproved = allapproved + Convert.ToInt32(item.Approved);
                        }
                        allreamingleaves = allleaves - allapproved;
                        model.AllLeave = allleaves;
                        model.Remaining = allreamingleaves;
                        model.Approved = allapproved;
                        model.Rejected = allrejectedleaves;
                        model.ListLeaveInfo = lstleaveEntitle;
                        return Ok(model);
                    }
                    else
                    {
                        return BadRequest(" Entitlement infomartion  cannot be null");
                    }

                }
                else
                {
                    return BadRequest(" employee  cannot be null");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        [HttpGet("calenders/{id}")]
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
                        var leaveDetail = _unitOfWork.EmployeeLeaveDetail.GetListofEmployeeLeave(employeeId, periodId);
                        if (leaveDetail.Count > 0)
                        {
                            foreach (var item in leaveDetail)
                            {
                                var leaveDeatil = "";
                                if (item.LeaveStatus.Name != "Cancelled" && item.LeaveStatus.Name != "Rejected")
                                {
                                    string backgroundColor = "";
                                    if (item.LeaveStatus.Name == "Pending")
                                    {
                                        backgroundColor = "#FF4500";
                                        leaveDeatil = item.Employee.ApplicationUser.FullName + " Leave Approval Pending" + item.ReasonForApply;
                                    }
                                    else if (item.LeaveStatus.Name == "Approved")
                                    {
                                        backgroundColor = "#32CD32";
                                        leaveDeatil = item.Employee.ApplicationUser.FullName + " Leave Approved " + item.ReasonForApply;
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

        [HttpGet("retract/{id}")]
        public async Task<IActionResult> RetractLeave(string id)
        {
            try
            {

                if (!string.IsNullOrEmpty(id))
                {
                    _unitOfWork.LeaveManagement.RetractEmployeeLeave(Guid.Parse(id));

                    var leaveDetail = _unitOfWork.EmployeeLeaveDetail.Find(c => c.Id == Guid.Parse(id) && c.IsActive == true).FirstOrDefault();
                    if (leaveDetail != null)
                    {
                        leaveDetail.IsActive = false;
                        leaveDetail.IsSubmitted = false;
                        _unitOfWork.EmployeeLeaveDetail.Update(leaveDetail);
                        _unitOfWork.SaveChanges();
                        var empDetail = _unitOfWork.Employee.GetEmployeedetails(leaveDetail.EmployeeId);
                        if (empDetail != null)
                        {
                            string hrName = _configuration.GetValue<string>("HrDetail:Name");
                            string hrEmail = _configuration.GetValue<string>("HrDetail:Email");

                            string strtDate = leaveDetail.LeaveStartDate.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                            string endDate = leaveDetail.LeaveStartDate.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                            string message = LeaveTemplates.LeaveRetractedToManager(empDetail.ApplicationUser.FullName, strtDate, endDate, empDetail.ManagerName);
                            (bool success, string errorMsg) response = await _emailer.SendEmailAsync(empDetail.ManagerName, empDetail.ManagerEmail, "Leave Application Status", message);

                            string message1 = LeaveTemplates.LeaveRetractedToHr(hrName, empDetail.ApplicationUser.FullName, strtDate, endDate);
                            (bool success, string errorMsg) response1 = await _emailer.SendEmailAsync(hrName, hrEmail, "Leave Application Status", message1);

                        }
                    }



                    return NoContent();
                }
                return BadRequest(" Id cannot be null");

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost("createLeave")]
        public async Task<IActionResult> CreateLeave([FromBody]LeaveApplyModel model)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    if (model == null)
                        return BadRequest($"{nameof(model)} cannot be null");

                    int pendingLeave = 0;
                    int pendingday = 0;
                    int noOfDays = 0;
                    int remaning = 0;
                    int approve = 0;
                    var employee = _unitOfWork.Employee.Find(c => c.UserId == model.UserId && c.IsActive == true).FirstOrDefault();
                    Guid leaveStatusId = _unitOfWork.LeaveStatus.Find(c => c.Name == "Pending").Select(m => m.Id).FirstOrDefault();
                    var leavePeriod = _unitOfWork.LeavePeriod.GetleavePeriodRecord();
                    if (leavePeriod != null)
                    {
                        if (employee != null && leaveStatusId != Guid.Empty)
                        {
                            if (model.StartDate > model.EndDate)
                            {
                                return BadRequest("End date cannot be before start date");
                            }
                            else if (model.StartDate < leavePeriod.PeriodStart || model.EndDate > leavePeriod.PeriodEnd)
                            {

                                return BadRequest("Leaves not allowed on these dates as per Company's leave period policy. The Leave Period Start Date is " + model.StartDate.ToShortDateString() + " and End date is " + model.EndDate.ToShortDateString());
                            }
                            else if ((model.StartDate < leavePeriod.PeriodStart) && (model.EndDate > leavePeriod.PeriodEnd))
                            {
                                return BadRequest("Leaves not allowed on these dates as per Company's leave period policy. The Leave Period Start Date is " + model.StartDate.ToShortDateString() + " and End date is " + model.EndDate.ToShortDateString());
                            }
                            else
                            {
                                bool checkdate = _unitOfWork.LeaveManagement.CheckDaterange(model.StartDate, model.EndDate, employee.Id.ToString());
                                if (!checkdate)
                                {
                                    return BadRequest("Leaves already have applied on these dates");
                                }
                                var entitlement = _unitOfWork.LeaveManagement.GetLeaveEntitlement(employee.Id, periodId, new Guid(model.LeaveTypeId));
                                if (entitlement != null)
                                {
                                    var lstEmpLeaveDetails = _unitOfWork.EmployeeLeaveDetail.Find(c => c.IsActive && c.EmployeeId == employee.Id && c.LeavePeriodId == periodId && c.IsSubmitted == true && c.LeaveStatusId == leaveStatusId).ToList();
                                    if (lstEmpLeaveDetails.Count > 0)
                                    {
                                        foreach (var item in lstEmpLeaveDetails)
                                        {
                                            if (item.LeavesEntitlementId == entitlement.Id)
                                            {

                                                var noofdays = _unitOfWork.LeaveManagement.GetCalculateNoOfDays(item.LeaveStartDate, item.LeaveEndDate);
                                                pendingday = pendingday + noofdays;
                                            }
                                        }

                                        pendingLeave = pendingday;
                                    }
                                    noOfDays = _unitOfWork.LeaveManagement.GetCalculateNoOfDays(model.StartDate, model.EndDate);

                                    if (noOfDays > Convert.ToInt32(entitlement.LeaveRules.LeavesPerYear))
                                    {
                                        return BadRequest("You don't have enough leave balance");
                                    }

                                    approve = approve + Convert.ToInt32(entitlement.Approved);
                                    remaning = Convert.ToInt32(entitlement.LeaveRules.LeavesPerYear) - approve;
                                    int totalAlleave = remaning - pendingLeave;
                                 //   if ((noOfDays == 0) || (noOfDays > totalAlleave))
                                 //    {
                                 //       return BadRequest("You don't have enough leave balance");
                                 //   }
                                 //    else
                                 //    {
                                        var empLeaveDetails = _unitOfWork.EmployeeLeaveDetail.Find(c => c.IsActive && c.EmployeeId == employee.Id && c.LeavePeriodId == periodId && c.IsSubmitted == true && c.LeaveStatusId == leaveStatusId && c.LeaveStartDate == model.StartDate && c.LeaveEndDate == model.EndDate).FirstOrDefault();
                                        if (empLeaveDetails == null)
                                        {
                                            var Model = new EmployeeLeaveDetail()
                                            {
                                                EmployeeId = employee.Id,
                                                IsSave = true,
                                                IsSubmitted = true,
                                                LeavePeriodId = leavePeriod.Id,
                                                LeavesEntitlementId = entitlement.Id,
                                                LeaveStatusId = leaveStatusId,
                                                LeaveStartDate = model.StartDate,
                                                LeaveEndDate = model.EndDate,
                                                ReasonForApply = model.ReasonForApply,
                                                ManagerId = employee.ManagerId,
                                                Name = entitlement.LeaveRules.LeaveType.Name
                                            };
                                            _unitOfWork.EmployeeLeaveDetail.Add(Model);
                                            _unitOfWork.SaveChanges();

                                            var empDetail = _unitOfWork.Employee.GetEmployeedetails(employee.Id);
                                            if (empDetail != null)
                                            {

                                                string hrName = _configuration.GetValue<string>("HrDetail:Name");
                                                string hrEmail = _configuration.GetValue<string>("HrDetail:Email");

                                                string strtDate = model.StartDate.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                                                string endDate = model.EndDate.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                                                string message = LeaveTemplates.LeaveApplicationToManager(empDetail.ApplicationUser.FullName, strtDate, endDate, empDetail.ManagerName);
                                                (bool success, string errorMsg) response = await _emailer.SendEmailAsync(empDetail.ManagerName, empDetail.ManagerEmail, "Leave Application", message);

                                                string message1 = LeaveTemplates.LeaveApplicationToHr(empDetail.ApplicationUser.FullName, hrName, strtDate, endDate, empDetail.ManagerName);
                                                (bool success, string errorMsg) response1 = await _emailer.SendEmailAsync(hrName, hrEmail, "Leave Application", message1);

                                                return NoContent();
                                            }
                                            else
                                            {
                                                return BadRequest("employee Detail not found");
                                            }

                                        }
                                        else
                                        {
                                            return BadRequest("You have already apply leave on this date ");
                                        }
                                 //    }

                                }
                            }
                        }
                        else
                        {
                            return BadRequest(" employee id and leavestatusid cannot be null");
                        }


                    }
                    return BadRequest(ModelState);
                }
                else
                {
                    return BadRequest("leave cofig is not set");
                }
            }
            catch (Exception ex)
            {

                return BadRequest(ex.GetBaseException().Message);
            }
        }

        [HttpGet("cancel/{id}")]
        public async Task<IActionResult> EmployeeCancelLeave(string id)
        {
            try
            {
                if (!string.IsNullOrEmpty(id))
                {
                    int noOfDays = 0;
                    var leaveDetail = _unitOfWork.EmployeeLeaveDetail.Find(m => m.Id == new Guid(id) && m.IsActive == true).FirstOrDefault();
                    if (leaveDetail != null)
                    {
                        var status = _unitOfWork.LeaveStatus.Get(leaveDetail.LeaveStatusId);
                        if (status != null)
                        {
                            if (status.Name != "Cancelled")
                            {
                                var entitlement = _unitOfWork.EmployeeEntitlement.Get(leaveDetail.LeavesEntitlementId);
                                if (entitlement != null)
                                {
                                    var checkLeaves = _unitOfWork.SubordinateLeave.CheckLeaves(leaveDetail.LeavesEntitlementId);
                                    int reamingleaves = checkLeaves.Item4;
                                    noOfDays = _unitOfWork.LeaveManagement.GetCalculateNoOfDays(leaveDetail.LeaveStartDate, leaveDetail.LeaveEndDate);
                                    if (reamingleaves >= 0 && noOfDays != 0)
                                    {
                                        leaveDetail.LeaveStatusId = _unitOfWork.LeaveStatus.Find(c => c.Name == "Cancelled").Select(m => m.Id).FirstOrDefault();
                                        _unitOfWork.EmployeeLeaveDetail.Update(leaveDetail);
                                        _unitOfWork.SaveChanges();

                                        var days = Convert.ToInt32(entitlement.Approved) - noOfDays;
                                        entitlement.Approved = days.ToString();
                                        _unitOfWork.EmployeeEntitlement.Update(entitlement);
                                        _unitOfWork.SaveChanges();


                                        var empDetail = _unitOfWork.Employee.GetEmployeedetails(leaveDetail.EmployeeId);
                                        if (empDetail != null)
                                        {
                                            string hrName = _configuration.GetValue<string>("HrDetail:Name");
                                            string hrEmail = _configuration.GetValue<string>("HrDetail:Email");
                                            string strtDate = leaveDetail.LeaveStartDate.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                                            string endDate = leaveDetail.LeaveStartDate.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                                            string message = LeaveTemplates.LeaveCancelledToManager(empDetail.ApplicationUser.FullName, strtDate, endDate, empDetail.ManagerName);
                                            (bool success, string errorMsg) response = await _emailer.SendEmailAsync(empDetail.ManagerName, empDetail.ManagerEmail, "Leave Application Status", message);

                                            string message1 = LeaveTemplates.LeaveCancelledToHr(hrName, empDetail.ApplicationUser.FullName, strtDate, endDate);
                                            (bool success, string errorMsg) response1 = await _emailer.SendEmailAsync(hrName, hrEmail, "Leave Application Status", message1);

                                        }

                                        return NoContent();
                                    }
                                    else
                                    {
                                        return BadRequest("Applied leave days is more than available days");
                                    }
                                }
                            }
                            else
                            {
                                return BadRequest("This leave has already approved or rejected");
                            }
                        }
                    }
                    else
                    {
                        return BadRequest("leaveDetails Can not be null");
                    }
                }
                return BadRequest(" Id cannot be null");

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private Guid SaveEmployeeLeaves(Guid employeeId)
        {
            var employeeLeaves = new EmployeeLeaves();
            employeeLeaves.EmployeeId = employeeId;
            employeeLeaves.LeavePeriodId = periodId;
            _unitOfWork.EmployeeLeaves.Add(employeeLeaves);
            _unitOfWork.SaveChanges();
            return employeeLeaves.Id;
        }

        private void SaveEntitlement(Guid bandId, Guid employeeLeavesId)
        {

            if (bandId != Guid.Empty && periodId != Guid.Empty && employeeLeavesId != Guid.Empty)
            {
                var leaveRules = _unitOfWork.LeaveRules.GetLeaveRuleByBandId(bandId, periodId);
                if (leaveRules.Count() > 0)
                {
                    foreach (var item in leaveRules)
                    {
                        var leaveEntitle = new EmployeeLeavesEntitlement();
                        leaveEntitle.Approved = "0";
                        leaveEntitle.Pending = "0";
                        leaveEntitle.Rejected = "0";
                        leaveEntitle.EmployeeLeavesId = employeeLeavesId;
                        leaveEntitle.LeaveRulesId = item.Id;
                        _unitOfWork.EmployeeEntitlement.Add(leaveEntitle);
                        _unitOfWork.SaveChanges();
                    }
                }
            }
        }

        private List<DropDownList> GetLeaveTypeList(Guid periodId)
        {
            var LeaveTypeVM = new List<DropDownList>();
            var LeaveTypeList = _unitOfWork.LeaveType.Find(m => m.LeavePeriodId == periodId && m.IsActive).ToList();
            if (LeaveTypeList.Count() > 0)
            {
                foreach (var item in LeaveTypeList)
                {
                    LeaveTypeVM.Add(new DropDownList { Label = item.Name, Value = item.Id.ToString() });
                }
            }
            return LeaveTypeVM;
        }
    }
}