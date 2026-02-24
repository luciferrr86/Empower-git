using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using A4.BAL;
using A4.DAL.Entites;
using A4.Empower.Helpers;
using DAL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace A4.Empower.Controllers
{

    [Route("api/[controller]")]
    public class ManageTimesheetController : Controller
    {

        readonly ILogger _logger;
        readonly IUnitOfWork _unitOfWork;
        readonly IEmailer _emailer;
        public ManageTimesheetController(IEmailer emailer, ILogger<ManageTimesheetController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _emailer = emailer;
        }

        [HttpGet("list/{userId?}/{page?}/{pageSize?}/{name?}")]
        [Produces(typeof(EmployeeListViewModel))]
        public IActionResult GetSubOrdinateList(string userId, int? page = null, int? pageSize = null, string name = null)
        {
            try
            {
                if (userId.checkString())
                {

                    Guid managerId = _unitOfWork.Employee.GetEmployeeId(userId);
                    if (managerId != Guid.Empty)
                    {
                        var model = new EmployeeListViewModel();
                        var employeeList = _unitOfWork.Employee.GetEmployeeList(managerId, name, Convert.ToInt32(page), Convert.ToInt32(pageSize));
                        if (employeeList.Count() > 0)
                        {
                            foreach (var item in employeeList)
                            {
                                var check = _unitOfWork.TimeSheetModule.CheckTimesheetConfig(item.Id);
                                model.EmployeeList.Add(new EmployeeListModel { EmployeeId = item.Id.ToString(), FullName = item.ApplicationUser.FullName, Designation = item.FunctionalDesignation.Name, IsConfig = check });
                            }
                        }
                        model.TotalCount = employeeList.Count();
                        return Ok(model);
                    }
                }
                return BadRequest("userId cannot be null");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.GetBaseException().Message);
            }
        }

        [HttpGet("GetEmployeeTimeSheet/{employeeId}")]
        [Produces(typeof(EmployeeTimeSheetModel))]
        public IActionResult GetEmployeeTimeSheet(string employeeId)
        {
            try
            {
                if (employeeId.checkString())
                {
                    var model = new EmployeeTimeSheetModel();
                    model = _unitOfWork.TimeSheetModule.GetEmployeeTimeSheet(Guid.Parse(employeeId), Guid.Parse("00000000-0000-0000-0000-000000000000"));

                    return Ok(model);
                }
                else
                {
                    return BadRequest("employeeId can not be null");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.GetBaseException().Message);
            }
        }

        [HttpPost("Approve")]
        public async Task<IActionResult> Create([FromBody] EmployeeTimeSheetModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (model.DayList.Count() > 0)
                    {
                        SaveApprove(model);
                        return NoContent();
                    }
                    return BadRequest("DayList count can not be zero");
                }
                return BadRequest(ModelState);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.GetBaseException().Message);
            }
        }

        private async void SaveApprove(EmployeeTimeSheetModel model)
        {
            int totalHour = 0;
            for (int i = 0; i < model.DayList.Count(); i++)
            {
                if (!string.IsNullOrWhiteSpace(model.DayList[i].UserDetailId))
                {
                    var userdetail = _unitOfWork.TimesheetUserDetail.Find(m => m.Id == Guid.Parse(model.DayList[i].UserDetailId) && m.IsActive == true).ToList().FirstOrDefault();
                    if (userdetail != null)
                    {
                        totalHour += SaveProjectHour(model, i, userdetail.Id);

                        if (model.type.Equals("Approve"))
                        {
                            SumbitUserDetails(model, i, userdetail);
                        }

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

            if (model.type.Equals("Approve"))
            {
                if (model.Frequency == "Weekly")
                {
                    await SaveTimesheetSpan(model);
                }
                else
                {
                    var checkAllTrue = _unitOfWork.TimesheetUserDetail.Find(m => m.TimesheetUserSpanId == Guid.Parse(model.UserSpanId) && m.IsActive == true).ToList();
                    if (checkAllTrue.Count() > 0)
                    {
                        var check = checkAllTrue.All(m => m.bManagerApproved == true && m.IsActive == true);
                        if (check)
                        {
                            await SaveTimesheetSpan(model);
                        }
                    }

                }
            }


         
        }

        private int CalculateTimeInMinutes(string input)
        {
            DateTime time = DateTime.Parse(input, new CultureInfo("en-US"));
            return (int)(time - time.Date).TotalMinutes;
        }

        private async Task SaveTimesheetSpan(EmployeeTimeSheetModel model)
        {
            var userSpan = _unitOfWork.TimesheetUserSpan.Find(m => m.Id == Guid.Parse(model.UserSpanId) && m.IsActive == true).ToList().FirstOrDefault();
            if (userSpan != null)
            {
                userSpan.bIsManagerApprove = true;
                _unitOfWork.TimesheetUserSpan.Update(userSpan);
                _unitOfWork.SaveChanges();
                string message = TimeSheetTemplates.TimesheetApproveSendMail(model.MangerName, model.EmployeeEmail, model.FullName, userSpan.WeekStartDate.ToShortDateString(), userSpan.WeekEndDate.ToShortDateString());
                (bool success, string errorMsg) response = await _emailer.SendEmailAsync(model.FullName, model.EmployeeEmail, "Timesheet Status", message);
            }
        }

        private void SumbitUserDetails(EmployeeTimeSheetModel model, int i, TimesheetUserDetail userdetail)
        {
            if (model.DayList[i].UserDetailId.checkString())
            {
                if (model.Frequency == "Weekly")
                {
                    userdetail.bManagerApproved = true;

                }
                else
                {
                    if (model.DayList[i].IsAllow == true || model.DayList[i].IsAllotted == false)
                    {
                        userdetail.bManagerApproved = true;
                    }
                }
                userdetail.ApproverlId = Guid.Parse(model.ApproverlId);
                _unitOfWork.TimesheetUserDetail.Update(userdetail);
                _unitOfWork.SaveChanges();
            }

        }

        private int SaveProjectHour(EmployeeTimeSheetModel model, int i, Guid userdeatilId)
        {
            int dayAllProjectHour = 0;
            for (int j = 0; j < model.ProjectList.Count; j++)
            {

                if ((model.ProjectList[j].ProjectHourList[i].IsAllow.Equals(true) && model.ProjectList[j].ProjectHourList[i].Hour != null))
                {
                    if (string.IsNullOrWhiteSpace(model.ProjectList[j].ProjectHourList[i].UserDetailProjectHourId))
                    {
                        var projectHour = new TimesheetUserDetailProjectHour();
                        projectHour.ProjectId = Guid.Parse(model.ProjectList[j].ProjectId);
                        projectHour.TimesheetUserDetailId = userdeatilId;
                        projectHour.Hour = model.ProjectList[j].ProjectHourList[i].Hour;
                        _unitOfWork.TimesheetUserDetailProjectHour.Add(projectHour);
                    }
                    else
                    {
                        var check = _unitOfWork.TimesheetUserDetailProjectHour.Find(m => m.Id == Guid.Parse(model.ProjectList[j].ProjectHourList[i].UserDetailProjectHourId)).FirstOrDefault();
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
    }
}