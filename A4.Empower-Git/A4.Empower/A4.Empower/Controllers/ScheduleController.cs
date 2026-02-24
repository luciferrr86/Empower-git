using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using A4.BAL;
using A4.DAL.Entites;
using A4.Empower.Helpers;
using A4.Empower.ViewModels;
using DAL;
using DAL.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace A4.Empower.Controllers
{
    [Authorize(AuthenticationSchemes = OpenIddict.Validation.AspNetCore.OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    public class ScheduleController : Controller
    {
        readonly ILogger _logger;
        readonly IUnitOfWork _unitOfWork;
        readonly IEmailer _emailer;
        private IWebHostEnvironment _hostingEnvironment;
        private readonly IAccountManager _accountManager;
        public ScheduleController(ILogger<ScheduleController> logger, IUnitOfWork unitOfWork, IEmailer emailer, IWebHostEnvironment hostingEnvironment, IAccountManager accountManager)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _emailer = emailer;
            _hostingEnvironment = hostingEnvironment;
            _accountManager = accountManager;
        }

        [HttpGet("list/{id?}/{page?}/{pageSize?}/{name?}")]
        [Produces(typeof(ScheduleViewModel))]
        public IActionResult GetCompanyList(string id = null, int? page = null, int? pageSize = null, string name = null)
        {
            try
            {
                var model = new ScheduleViewModel();
                var salesCompany = _unitOfWork?.SalesCompany?.Find(m => m.Id == Guid.Parse(id) && m.IsActive == true).FirstOrDefault();
                if (salesCompany != null)
                {
                    model.StatusCompanyModel.CompanyId = salesCompany.Id.ToString();
                    model.StatusCompanyModel.CompanyName = salesCompany.ComapnyName;
                    model.StatusCompanyModel.CompanyAddress = salesCompany.CompanyAddress;
                    model.StatusCompanyModel.CompanyTelePhoneNo = salesCompany.Telephone;
                    model.StatusCompanyModel.EmailId = salesCompany.EmailId;
                    var lsScheduleMeeting = new List<ScheduleModel>();
                    var scheduleMeeting = _unitOfWork?.SalesScheduleMeeting?.GetAllSalesScheduleMeeting(salesCompany.Id, name, Convert.ToInt32(page), Convert.ToInt32(pageSize));
                    if (scheduleMeeting.Count() > 0)
                    {
                        foreach (var item in scheduleMeeting)
                        {
                            lsScheduleMeeting.Add(new ScheduleModel { ScheduleId = item.Id.ToString(), MettingDate = item.ScheduleDate.ToShortDateString(), Subject = item.Subject, Venue = item.Venue, Agenda = item.Agenda, File = GetSalesDoumentFile(item.Document) });
                        }
                    }
                    model.TotalCount = scheduleMeeting.TotalCount;
                    model.lstSchedule = lsScheduleMeeting;
                    return Ok(model);
                }
                else
                {
                    return BadRequest("SalesCompany can not be Empty");
                }

            }
            catch (Exception ex)
            {
                return BadRequest(ex.GetBaseException().Message);
            }
        }

        [HttpGet("meetingschedule/{id}")]
        [Produces(typeof(ScheduleModel))]
        public IActionResult GetMeetingSchedule(string id)
        {
            var model = new ScheduleModel();
            var inetrnalEmployeeList = new List<DropDownList>();
            var copmanyContactList = new List<DropDownList>();
            var managerList = _unitOfWork.Employee.GetManagerList();
            foreach (var manager in managerList)
            {
                inetrnalEmployeeList.Add(new DropDownList { Label = manager.FullName, Value = manager.Id.ToLower().ToString() });
            }
            var companyContact = _unitOfWork.SalesCompanyContact.Find(m => m.SalesCompanyId == Guid.Parse(id));
            if (companyContact.Count() > 0)
            {
                foreach (var item in companyContact)
                {
                    copmanyContactList.Add(new DropDownList { Label = item.FirstName + " " + item.LastName, Value = item.Id.ToString().ToLower() });
                }

            }
            model.SelectInternalPerson = inetrnalEmployeeList;
            model.SelectClientPerson = copmanyContactList;
            return Ok(model);
        }


        [HttpGet("getMom/{id}")]
        [Produces(typeof(MinutesOfMeetingModel))]
        public IActionResult GetMOMData(string id)
        {
            var model = new MinutesOfMeetingModel();
            var salesScheduleMeeting = _unitOfWork?.SalesScheduleMeeting.Find(m => m.Id == Guid.Parse(id) && m.IsActive == true).FirstOrDefault();

            if (salesScheduleMeeting != null)
            {
                var inetrnalEmployeeList = new List<DropDownList>();
                var inetrnalEmployeeId = new List<string>();
                var inetrnalEmployeeNextId = new List<string>();
                model.MettingDate = salesScheduleMeeting.ScheduleDate.ToShortDateString();
                model.Agenda = salesScheduleMeeting.Agenda;
                model.Venue = salesScheduleMeeting.Venue;
                model.Writer = salesScheduleMeeting.Writer;
                model.Subject = salesScheduleMeeting.Subject;

                var managerList = _unitOfWork.Employee.GetManagerList();
                foreach (var manager in managerList)
                {
                    inetrnalEmployeeList.Add(new DropDownList { Label = manager.FullName, Value = manager.Id.ToLower().ToString() });
                }
                model.SelectInternalPerson = inetrnalEmployeeList;
                model.InternalPerson = inetrnalEmployeeId;

                var salesInetrnalEmployee = _unitOfWork.SalesScheduleMeetingInternal.Find(m => m.SalesScheduleMeetingId == salesScheduleMeeting.Id);
                if (salesInetrnalEmployee.Count() > 0)
                {
                    foreach (var item in salesInetrnalEmployee)
                    {
                        inetrnalEmployeeId.Add(item.EmployeeId.ToString());
                    }
                }
                var meeting = _unitOfWork?.SalesMinuteMeeting.Find(m => m.SalesScheduleMeetingId == Guid.Parse(id) && m.IsActive == true).FirstOrDefault();
                if (meeting != null)
                {
                    var salesInetrnalEmployeeNext = _unitOfWork?.SalesMinuteMeetingInternal.Find(m => m.SalesMinuteMeetingId == meeting.Id && m.IsActive == true);
                    if (salesInetrnalEmployeeNext.Count() > 0)
                    {
                        foreach (var item in salesInetrnalEmployeeNext)
                        {
                            inetrnalEmployeeNextId.Add(item.EmployeeId.ToString());
                        }
                    }
                    model.Id = meeting.Id.ToString();
                    model.MomDescription = meeting.Description;
                    model.NextActionDescription = meeting.ActionDescription;
                    model.NextActionStatus = meeting.Status;
                    model.NextActionDueDate = meeting.DueDate;
                    model.NextActionInternalPerson = inetrnalEmployeeNextId;
                }
            }
            return Ok(model);
        }

        [HttpPost("saveMom")]
        public IActionResult SaveMinutesOfMeeting([FromBody]MinutesOfMeetingModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (model == null)
                        return BadRequest($"{nameof(model)} cannot be null");

                    var meeting = _unitOfWork?.SalesMinuteMeeting.Find(m => m.SalesScheduleMeetingId == Guid.Parse(model.MeetingId) && m.IsActive == true).FirstOrDefault();
                    if (meeting == null)
                    {

                        var Model = new SalesMinuteMeeting()
                        {
                            Description = model.MomDescription,
                            DueDate = model.NextActionDueDate,
                            Status = model.NextActionStatus,
                            ActionDescription = model.NextActionDescription,
                            SalesScheduleMeetingId = Guid.Parse(model.MeetingId),

                        };
                        _unitOfWork.SalesMinuteMeeting.Add(Model);

                        if (model.NextActionInternalPerson != null && model.NextActionInternalPerson.Count > 0)
                        {
                            var salesMinuteMeetingInternal = new List<SalesMinuteMeetingInternal>();
                            foreach (var item in model.NextActionInternalPerson)
                            {
                                salesMinuteMeetingInternal.Add(new SalesMinuteMeetingInternal { SalesMinuteMeetingId = Model.Id, EmployeeId = Guid.Parse(item), IsActive = true });
                            }
                            _unitOfWork.SalesMinuteMeetingInternal.AddRange(salesMinuteMeetingInternal);
                        }
                        _unitOfWork.SaveChanges();
                    }
                    else
                    {
                        meeting.Description = model.MomDescription;
                        meeting.DueDate = model.NextActionDueDate;
                        meeting.Status = model.NextActionStatus;
                        meeting.ActionDescription = model.NextActionDescription;
                        _unitOfWork.SalesMinuteMeeting.Update(meeting);
                        if (model.NextActionInternalPerson != null && model.NextActionInternalPerson.Count > 0)
                        {
                            var salesInetrnalEmployeeNext = _unitOfWork?.SalesMinuteMeetingInternal.Find(m => m.SalesMinuteMeetingId == meeting.Id && m.IsActive == true);
                            if (salesInetrnalEmployeeNext.Count() > 0)
                            {

                                _unitOfWork.SalesMinuteMeetingInternal.RemoveRange(salesInetrnalEmployeeNext);
                            }
                            var salesMinuteMeetingInternal = new List<SalesMinuteMeetingInternal>();
                            foreach (var item in model.NextActionInternalPerson)
                            {
                                salesMinuteMeetingInternal.Add(new SalesMinuteMeetingInternal { SalesMinuteMeetingId = meeting.Id, EmployeeId = Guid.Parse(item), IsActive = true });
                            }
                            _unitOfWork.SalesMinuteMeetingInternal.AddRange(salesMinuteMeetingInternal);
                        }
                        _unitOfWork.SaveChanges();
                    }

                    return NoContent();
                }
                else
                {
                    return BadRequest("model Is Invalid");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.GetBaseException().Message);
            }
        }

        [HttpPost("saveSchedule")]
        public async Task<IActionResult> SaveSchedule([FromBody]ScheduleModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (model == null)
                        return BadRequest($"{nameof(model)} cannot be null");
                    var ScheduleMeeting = new SalesScheduleMeeting()
                    {
                        ScheduleDate = Convert.ToDateTime(model.MettingDate),
                        Venue = model.Venue,
                        Writer = model.Writer,
                        Subject = model.Subject,
                        Agenda = model.Agenda,
                        Description = model.Description,
                        Document = model.fileId,
                        SalesCompanyId = Guid.Parse(model.CompanyId),

                    };
                    _unitOfWork.SalesScheduleMeeting.Add(ScheduleMeeting);
                    string clientName = "";
                    var salesCompany = _unitOfWork.SalesCompany.Get(Guid.Parse(model.CompanyId));
                    if (salesCompany != null)
                    {
                        clientName = salesCompany.ComapnyName;
                    }
     
                    if (model.ClientPerson != null && model.ClientPerson.Count > 0)
                    {
                        var salesScheduleMeetingExternal = new List<SalesScheduleMeetingExternal>();

                        foreach (var item in model.ClientPerson)
                        {
                            if (model.isChecked)
                            {
                                var salesCompanyContact = _unitOfWork.SalesCompanyContact.Get(Guid.Parse(item));
                                if (salesCompanyContact != null)
                                {
           
                                    string message = SalesMarketingTemplates.ClientMeetingSchedule(clientName, Convert.ToDateTime(model.MettingDate), model.Subject, model.Venue, model.Agenda);
                                    (bool success, string errorMsg) response = await _emailer.SendEmailAsync(clientName, salesCompanyContact.EmailId, "Meeting Schedule", message);

                                }
                            }
                            _unitOfWork.SalesScheduleMeetingExternal.AddRange(salesScheduleMeetingExternal);
                            salesScheduleMeetingExternal.Add(new SalesScheduleMeetingExternal { SalesScheduleMeetingId = ScheduleMeeting.Id, SalesCompanyContactId = Guid.Parse(item), IsActive = true });
                        }


                    }

                    if (model.InternalPerson != null && model.InternalPerson.Count > 0)
                    {
                        var salesScheduleMeetingInternal = new List<SalesScheduleMeetingInternal>();
                        foreach (var item in model.InternalPerson)
                        {
                            salesScheduleMeetingInternal.Add(new SalesScheduleMeetingInternal { SalesScheduleMeetingId = ScheduleMeeting.Id, EmployeeId = Guid.Parse(item), IsActive = true });
                            var empDetail = GetEmpDetail(Guid.Parse(item));
                            if (empDetail != null)
                            {
                                string message = SalesMarketingTemplates.EmployeeMeetingSchedule(empDetail.FullName, Convert.ToDateTime(model.MettingDate), model.Subject, model.Venue, clientName, model.Agenda);
                                (bool success, string errorMsg) response = await _emailer.SendEmailAsync(empDetail.FullName, empDetail.Email, "Meeting Schedule", message);
                            }
                        }
                        _unitOfWork.SalesScheduleMeetingInternal.AddRange(salesScheduleMeetingInternal);
                    }
                    _unitOfWork.SaveChanges();
                    return NoContent();
                }
                else
                {
                    return BadRequest("model Is Invalid");
                }
            }
            catch (Exception ex)
            {

                return BadRequest(ex.GetBaseException().Message);
            }
        }

        private string GetSalesDoumentFile(int fileId)
        {
            string file = "";
            if (fileId != 0)
            {
                var picture = _unitOfWork.FileUpload.GetPictureById(Convert.ToInt32(fileId));
                file = _unitOfWork.FileUpload.GetSalesDocumentUrl(picture, _hostingEnvironment.WebRootPath);
            }
            return file;


        }

        private ApplicationUser GetEmpDetail(Guid empId)
        {
            ApplicationUser userDetail = null;
            var employee = _unitOfWork.Employee.Get(empId);
            if (employee != null)
            {
                var user = _accountManager.GetUserByIdAsync(employee.UserId).Result;
                if (user != null)
                {
                    userDetail = user;
                }
            }
            return userDetail;
        }

    }
}