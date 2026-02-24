using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using A4.BAL;
using A4.DAL.Entites;
using A4.Empower.Helpers;
using DAL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace A4.Empower.Controllers
{

    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = OpenIddict.Validation.AspNetCore.OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)]
    public class MyTimesheetController : Controller
    {

        readonly ILogger _logger;
        readonly IUnitOfWork _unitOfWork;
        readonly IEmailer _emailer;
        public MyTimesheetController(IEmailer emailer, ILogger<MyTimesheetController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _emailer = emailer;
        }

        [HttpGet("myTimesheet/{userId?}")]
        [Produces(typeof(EmployeeTimeSheetModel))]
        public IActionResult GetMyTimesheet(string userId)
        {

            if (userId.checkString())
            {
                Guid employeeId = _unitOfWork.Employee.Find(c => c.UserId == userId && c.IsActive == true).Select(m => m.Id).FirstOrDefault();

                if (employeeId != Guid.Empty)
                {
                    var check = _unitOfWork.TimeSheetModule.CheckTimesheetConfig(employeeId);
                    if (check)
                    {
                        var model = _unitOfWork.TimeSheetModule.GetEmployeeTimeSheet(employeeId, Guid.Parse("00000000-0000-0000-0000-000000000000"));
                      
                        return Ok(model);
                    }
                    return BadRequest("set config");
                }
                else
                {
                    return BadRequest("employeeId cannot be null");
                }
            }
            return BadRequest("property cannot be null");

        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody]EmployeeTimeSheetModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (model == null)
                        return BadRequest($"{nameof(model)} cannot be null");

                    if (model.DayList.Count() > 0)
                    {
                        SaveSumbit(model);
                        return NoContent();
                    }
                }
                return BadRequest(ModelState);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.GetBaseException().Message);
            }
        }

        [HttpGet("weeklyTimesheet/{userId?}/{spanId?}")]
        [Produces(typeof(EmployeeTimeSheetModel))]
        public IActionResult GetWeeklyTimesheet(string userId, string spanId)
        {

            if (userId.checkString())
            {
                Guid employeeId = _unitOfWork.Employee.Find(c => c.UserId == userId && c.IsActive == true).Select(m => m.Id).FirstOrDefault();
                if (employeeId != Guid.Empty && Guid.Parse(spanId) != Guid.Empty)
                {
                    var model = _unitOfWork.TimeSheetModule.GetEmployeeTimeSheet(employeeId, Guid.Parse(spanId));
                    return Ok(model);
                }
                else
                {
                    return BadRequest("employeeId  && spanId cannot be null");
                }
            }
            return BadRequest("property cannot be null");

        }

        private async void SaveSumbit(EmployeeTimeSheetModel model)
        {
            int totalHour = 0;
            for (int i = 0; i < model.DayList.Count(); i++)
            {
                Guid userdeatilId;
                if (model.DayList[i].UserDetailId == "")
                {
                    userdeatilId = _unitOfWork.TimeSheetModule.AddTimesheetUserDetails(model.type, model.DayList[i], Guid.Parse(model.UserSpanId), Guid.Parse(model.ApproverlId));
                }
                else
                {
                    var check = _unitOfWork.TimesheetUserDetail.Find(m => m.Id == Guid.Parse(model.DayList[i].UserDetailId) && m.IsActive == true).FirstOrDefault();
                    if (check != null)
                    {
                        check.TimeSheetDate = model.DayList[i].Date;
                        check.Day = model.DayList[i].Day;
                        check.TimesheetUserSpanId = Guid.Parse(model.UserSpanId);
                        check.ApproverlId = Guid.Parse(model.ApproverlId);

                        if (model.type.Equals("Submit"))
                        {
                            if (model.Frequency == "Weekly")
                            {
                                check.bIsUserSaved = true;
                                check.bIsUserSubmit = true;

                            }
                            else
                            {
                                if (model.DayList[i].IsAllow == true || model.DayList[i].IsAllotted == false)
                                {
                                    check.bIsUserSaved = true;
                                    check.bIsUserSubmit = true;
                                }
                            }
                        }
                        else
                        {
                            if (model.DayList[i].IsAllow == true || model.DayList[i].IsAllotted == false)
                            {
                                check.bIsUserSaved = true;
                            }
                        }

                        _unitOfWork.TimesheetUserDetail.Update(check);
                        _unitOfWork.SaveChanges();
                    }
                    userdeatilId = Guid.Parse(model.DayList[i].UserDetailId);
                }
                if (userdeatilId != Guid.Empty)
                {

                    if (model.ProjectList.Count() > 0)
                    {
                        totalHour += SaveProjectHour(model, i, userdeatilId);
                    }
                }

            }
            var userSpanObject = _unitOfWork.TimesheetUserSpan.Get(Guid.Parse(model.UserSpanId));
            userSpanObject.TotalHour = string.Format("{0:00}:{1:00}", (int)TimeSpan.FromMinutes(totalHour).TotalHours, TimeSpan.FromMinutes(totalHour).Minutes);

            if (userSpanObject != null && model.type.Equals("Save"))
            {
                  _unitOfWork.TimesheetUserSpan.Update(userSpanObject);
                _unitOfWork.SaveChanges();
            }

            if (model.type.Equals("Submit"))
            {
                if (model.Frequency == "Weekly")
                {
                    await SaveUserSpan(userSpanObject, model.MangerName, model.MangerEmail, model.FullName);
                }
                else
                {
                    var checkAllTrue = _unitOfWork.TimesheetUserDetail.Find(m => m.TimesheetUserSpanId == Guid.Parse(model.UserSpanId) && m.IsActive == true).ToList();
                    if (checkAllTrue.Count() > 0)
                    {
                        var check = checkAllTrue.All(m => m.bIsUserSaved == true && m.bIsUserSubmit == true);
                        if (check)
                        {
                            await SaveUserSpan(userSpanObject, model.MangerName, model.MangerEmail, model.FullName);
                        }

                    }

                }
            }

        }

        private async Task SaveUserSpan(TimesheetUserSpan userSpan, string mangerName, string mangerEmail, string fullname)
        {
            if (userSpan != null)
            {
                userSpan.bIsUserSubmit = true;
                _unitOfWork.TimesheetUserSpan.Update(userSpan);
                _unitOfWork.SaveChanges();

                string message = TimeSheetTemplates.TimesheetSubmitMail(mangerName, mangerEmail, fullname, userSpan.WeekStartDate.ToShortDateString(), userSpan.WeekEndDate.ToShortDateString());
                (bool success, string errorMsg) response = await _emailer.SendEmailAsync(mangerName, mangerEmail, "Timesheet Approve Request", message);
            }
        }

        
        private int SaveProjectHour(EmployeeTimeSheetModel model, int i, Guid userdeatilId)
        {
            int dayAllProjectHour = 0;
            for (int j = 0; j < model.ProjectList.Count; j++)
            {

                if ((model.ProjectList[j].ProjectHourList[i].IsAllow.Equals(true) && model.ProjectList[j].ProjectHourList[i].Hour != ""))
                {
                    if (model.ProjectList[j].ProjectHourList[i].UserDetailProjectHourId == "")
                    {
                        var projectHour = new TimesheetUserDetailProjectHour();
                        projectHour.ProjectId = Guid.Parse(model.ProjectList[j].ProjectId);
                        projectHour.TimesheetUserDetailId = userdeatilId;
                        projectHour.Hour = model.ProjectList[j].ProjectHourList[i].Hour;
                        _unitOfWork.TimesheetUserDetailProjectHour.Add(projectHour);
                    }
                    else
                    {
                        var check = _unitOfWork.TimesheetUserDetailProjectHour.Find(m => m.Id == Guid.Parse(model.ProjectList[j].ProjectHourList[i].UserDetailProjectHourId)).ToList().FirstOrDefault();
                        check.ProjectId = Guid.Parse(model.ProjectList[j].ProjectId);
                        check.TimesheetUserDetailId = userdeatilId;
                        check.Hour = model.ProjectList[j].ProjectHourList[i].Hour;
                        _unitOfWork.TimesheetUserDetailProjectHour.Update(check);
                    }
                    dayAllProjectHour += CalculateTimeInMinutes(model.ProjectList[j].ProjectHourList[i].Hour);
                    _unitOfWork.SaveChanges();
                }
            }
            return dayAllProjectHour;
        }

        private int CalculateTimeInMinutes(string input)
        {
            DateTime time = DateTime.Parse(input, new CultureInfo("en-US"));
            return (int)(time - time.Date).TotalMinutes;
        }
    }
}