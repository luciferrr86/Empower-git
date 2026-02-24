using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using A4.BAL;
using A4.DAL.Entites;
using DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace A4.Empower.Controllers
{

    [Route("api/[controller]")]
    public class TimeSheetTemplateController : Controller
    {
        readonly ILogger _logger;
        readonly IUnitOfWork _unitOfWork;
        public TimeSheetTemplateController(ILogger<TimeSheetTemplateController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        [HttpGet("list/{page?}/{pageSize?}/{name?}")]
        [Produces(typeof(TimesheetTemplateViewModel))]
        public IActionResult GetAll(int? page = null, int? pageSize = null, string name = null)
        {
            try
            {
                var result = new TimesheetTemplateViewModel();
                var viewModel = new List<TimesheetTemplateModel>();
                var configuration = new List<DropDownList>();
                var model = _unitOfWork.TimesheetTemplate.GetAllTemplate(name, Convert.ToInt32(page), Convert.ToInt32(pageSize));
                var configurationList = _unitOfWork.TimesheetConfiguration.Find(m => m.IsActive).ToList();
                if (configurationList.Count() > 0)
                {
                    foreach (var item in configurationList)
                    {

                        configuration.Add(new DropDownList { Label = item.Name, Value = item.Id.ToString() });
                    }
                }

                if (model.Count() > 0)
                {
                    foreach (var item in model)
                    {
                        var selected = new List<string>();
                        if (item.Monday)
                        {
                            selected.Add("monday");
                        }
                        if (item.Tuesday)
                        {
                            selected.Add("tuesday");
                        }
                        if (item.Wednesday)
                        {
                            selected.Add("wednesday");
                        }
                        if (item.Thursday)
                        {
                            selected.Add("thursday");
                        }
                        if (item.Friday)
                        {
                            selected.Add("friday");
                        }
                        if (item.Saturday)
                        {
                            selected.Add("saturday");
                        }
                        if (item.Sunday)
                        {
                            selected.Add("sunday");
                        }
                        viewModel.Add(new TimesheetTemplateModel
                        {
                            Id = item.Id.ToString(),
                            TemplateName = item.TempalteName,
                            StartDate = item.StartDate,
                            EndDate = item.EndDate,
                            Monday = item.Monday,
                            Tuesday = item.Tuesday,
                            Wednesday = item.Wednesday,
                            Thursday = item.Thursday,
                            Friday = item.Friday,
                            Saturday = item.Saturday,
                            Sunday = item.Sunday,
                            TimesheetConfigurationId = item.TimesheetFrequencyId.ToString(),
                            selectedDays = selected
                        });

                    }
                }
                result.TimesheetTemplateList = viewModel;
                result.TimesheetConfigurationList = configuration;
                result.TotalCount = model.TotalCount;
                return Ok(result);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.GetBaseException().Message);
            }

        }

        [HttpPost("create")]
        public IActionResult Create([FromBody] TimesheetTemplateModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (model == null)
                        return BadRequest($"{nameof(model)} cannot be null");
                    if (string.IsNullOrEmpty(model.TemplateName) && string.IsNullOrEmpty(model.TimesheetConfigurationId))
                        return BadRequest("model property cannot be null");
                    var template = new TimesheetTemplate();
                    template.TempalteName = model.TemplateName;
                    template.StartDate = model.StartDate;
                    template.EndDate = model.EndDate;
                    template.TimesheetFrequencyId = Guid.Parse(model.TimesheetConfigurationId);
                    var configuration = _unitOfWork.TimesheetTemplate.GetTimeSheetConfiguration(Guid.Parse(model.TimesheetConfigurationId));
                    template.Monday = false;
                    template.Tuesday = false;
                    template.Wednesday = false;
                    template.Thursday = false;
                    template.Friday = false;
                    template.Saturday = false;
                    template.Sunday = false;
                    if (configuration.TimesheetFrequency == 1)
                    {
                        foreach (var item in model.selectedDays)
                        {
                            switch (item)
                            {
                                case "monday":
                                    template.Monday = true;
                                    break;
                                case "tuesday":
                                    template.Tuesday = true;
                                    break;
                                case "wednesday":
                                    template.Wednesday = true;
                                    break;
                                case "thursday":
                                    template.Thursday = true;
                                    break;
                                case "friday":
                                    template.Friday = true;
                                    break;
                                case "saturday":
                                    template.Saturday = true;
                                    break;
                                case "sunday":
                                    template.Sunday = true;
                                    break;
                            }
                        }
                        template.ScheduleType = "Daily";
                    }
                    if (configuration.TimesheetFrequency == 2)
                    {
                        template.Monday = true;
                        template.Tuesday = true;
                        template.Wednesday = true;
                        template.Thursday = true;
                        template.Friday = true;
                        template.Saturday = false;
                        template.Sunday = false;
                        template.ScheduleType = "Weekly";
                    }
                    _unitOfWork.TimesheetTemplate.Add(template);
                    _unitOfWork.SaveChanges();
                    return NoContent();
                }
                return BadRequest(ModelState);
            }
            catch (DbUpdateException ex) when (ex.InnerException is SqlException sqlException && (sqlException.Number == 2627 || sqlException.Number == 2601))
            {
                return BadRequest(model.TemplateName + " already exists");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.GetBaseException().Message);
            }
        }

        [HttpPut("update/{id}")]
        public IActionResult Update(string id, [FromBody] TimesheetTemplateModel model)
        {
            try
            {
                if (id != null)
                {
                    if (ModelState.IsValid)
                    {
                        if (model == null)
                            return BadRequest($"{nameof(model)} cannot be null");
                        if (string.IsNullOrEmpty(model.TemplateName) && string.IsNullOrEmpty(model.TimesheetConfigurationId))
                            return BadRequest("model property cannot be null");
                        var template = _unitOfWork.TimesheetTemplate.Get(Guid.Parse(id));
                        if (template != null)
                        {
                            template.TempalteName = model.TemplateName;
                            template.StartDate = model.StartDate;
                            template.EndDate = model.EndDate;
                            template.TimesheetFrequencyId = Guid.Parse(model.TimesheetConfigurationId);
                            var configuration = _unitOfWork.TimesheetTemplate.GetTimeSheetConfiguration(Guid.Parse(model.TimesheetConfigurationId));
                            template.Monday = false;
                            template.Tuesday = false;
                            template.Wednesday = false;
                            template.Thursday = false;
                            template.Friday = false;
                            template.Saturday = false;
                            template.Sunday = false;
                            if (configuration.TimesheetFrequency == 1)
                            {
                                foreach (var item in model.selectedDays)
                                {
                                    switch (item)
                                    {
                                        case "monday":
                                            template.Monday = true;
                                            break;
                                        case "tuesday":
                                            template.Tuesday = true;
                                            break;
                                        case "wednesday":
                                            template.Wednesday = true;
                                            break;
                                        case "thursday":
                                            template.Thursday = true;
                                            break;
                                        case "friday":
                                            template.Friday = true;
                                            break;
                                        case "saturday":
                                            template.Saturday = true;
                                            break;
                                        case "sunday":
                                            template.Sunday = true;
                                            break;
                                    }
                                }
                            }
                            if (configuration.TimesheetFrequency == 2)
                            {
                                template.Monday = true;
                                template.Tuesday = true;
                                template.Wednesday = true;
                                template.Thursday = true;
                                template.Friday = true;
                                template.Saturday = false;
                                template.Sunday = false;
                            }
                            _unitOfWork.TimesheetTemplate.Update(template);
                            _unitOfWork.SaveChanges();
                            return NoContent();
                        }
                        return BadRequest("template cannot be null");
                    }
                    return BadRequest(ModelState);
                }
                return BadRequest("id cannot be null");
            }
            catch (DbUpdateException ex) when (ex.InnerException is SqlException sqlException && (sqlException.Number == 2627 || sqlException.Number == 2601))
            {
                return BadRequest(model.TemplateName + " already exists");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.GetBaseException().Message);
            }
        }

        [HttpDelete("delete/{id}")]
        public IActionResult Delete(string id)
        {
            try
            {
                if (id != null)
                {
                    var existSchedule = _unitOfWork.TimesheetUserSchedule.Find(m => m.TimesheetTemplateId == Guid.Parse(id)).ToList();
                    if (existSchedule.Count() > 0)
                    {
                        return BadRequest("Already proceesing in..");
                    }
                    else
                    {
                        var checkTemplate = _unitOfWork.TimesheetTemplate.Get(Guid.Parse(id));
                        if (checkTemplate != null)
                        {
                            _unitOfWork.TimesheetTemplate.Remove(checkTemplate);
                            _unitOfWork.SaveChanges();
                            return NoContent();
                        }
                        else
                        {
                            return BadRequest("checkTemplate cannot be null");
                        }
                    }

                }
                return BadRequest("id cannot be null");
            }
            catch (Exception ex)
            {

                return BadRequest(ex.GetBaseException().Message);
            }
        }


        [HttpGet("getAllSchedule/{page?}/{pageSize?}/{name?}")]
        [Produces(typeof(TimesheetScheduleViewModel))]
        public IActionResult GetAllSchedule(int? page = null, int? pageSize = null, string name = null)
        {
            try
            {
                var result = new TimesheetScheduleViewModel();
                var employeeModel = new List<EmployeeListModel>();
                var userModel = new List<UserScheduleModel>();
                var template = _unitOfWork.TimesheetTemplate.Find(m => m.IsActive == true).FirstOrDefault();
                var employeeList = _unitOfWork.TimesheetTemplate.GetEmployeeList(name, Convert.ToInt32(page), Convert.ToInt32(pageSize));
                if (employeeList.Count() > 0)
                {
                    foreach (var item in employeeList)
                    {
                        employeeModel.Add(new EmployeeListModel { EmployeeId = item.Id.ToString(), FullName = item.ApplicationUser.FullName, Designation = item.FunctionalDesignation.Name, TemplateId = "0" });


                        if (template == null)
                        {
                            userModel.Add(new UserScheduleModel
                            {
                                EmployeeId = item.Id.ToString(),
                                FullName = item.ApplicationUser.FullName,
                                Sunday = false,
                                Monday = false,
                                Tuesday = false,
                                Wednesday = false,
                                Thursday = false,
                                Friday = false,
                                Saturday = false,
                                TimesheetFrequency = ""
                            });
                        }
                        else
                        {
                            var userSchedule = _unitOfWork.TimesheetUserSchedule.Find(m => m.IsActive == true && m.EmployeeId == item.Id).FirstOrDefault();
                            if (userSchedule != null)
                            {

                                var templateUser = _unitOfWork.TimesheetTemplate.Get(userSchedule.TimesheetTemplateId);
                                if (templateUser != null)
                                {
                                    userModel.Add(new UserScheduleModel
                                    {
                                        EmployeeId = item.Id.ToString(),
                                        FullName = item.ApplicationUser.FullName,
                                        Sunday = templateUser.Sunday,
                                        Monday = templateUser.Monday,
                                        Tuesday = templateUser.Tuesday,
                                        Wednesday = templateUser.Wednesday,
                                        Thursday = templateUser.Thursday,
                                        Friday = templateUser.Friday,
                                        Saturday = templateUser.Saturday,
                                        TimesheetFrequency = templateUser.ScheduleType
                                    });
                                }
                            }
                            else
                            {
                                userModel.Add(new UserScheduleModel
                                {
                                    EmployeeId = item.Id.ToString(),
                                    FullName = item.ApplicationUser.FullName,
                                    Sunday = template.Sunday,
                                    Monday = template.Monday,
                                    Tuesday = template.Tuesday,
                                    Wednesday = template.Wednesday,
                                    Thursday = template.Thursday,
                                    Friday = template.Friday,
                                    Saturday = template.Saturday,
                                    TimesheetFrequency = template.ScheduleType
                                });
                            }
                        }


                    }
                }
                result.TimesheetTemplateList = GetddlTemplate();
                result.EmployeeList = employeeModel;
                result.EmployeeCount = employeeList.Count();
                result.UserScheduleList = userModel;
                result.ScheduleCount = employeeList.Count();
                return Ok(result);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.GetBaseException().Message);
            }

        }

        [HttpPost("setSchedule")]
        public IActionResult SetSchedule([FromBody]TimesheetScheduleModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (model == null)
                        return BadRequest($"{nameof(model)} cannot be null");
                    if (string.IsNullOrEmpty(model.TemplateId))
                        return BadRequest("model property cannot be null");
                    string name = _unitOfWork.TimesheetTemplate.GetScheduleType(Guid.Parse(model.TemplateId));
                    var check = _unitOfWork.TimesheetUserSchedule.Find(m => m.IsActive == true && m.TimesheetTemplateId == Guid.Parse(model.TemplateId)).ToList();
                    if (check.Count > 0)
                    {
                        _unitOfWork.TimesheetUserSchedule.RemoveRange(check);
                        _unitOfWork.SaveChanges();
                    }
                    if (model.EmployeeList.Count != 0)
                    {
                        var uniqueEmployee = model.EmployeeList.GroupBy(m => m.EmployeeId).Select(l => l.First());
                        foreach (var item in uniqueEmployee)
                        {

                            var userschedule = _unitOfWork.TimesheetUserSchedule.Find(m => m.IsActive == true && m.EmployeeId == Guid.Parse(item.EmployeeId)).FirstOrDefault();

                            if (userschedule != null)
                            {

                                userschedule.TimesheetTemplateId = Guid.Parse(model.TemplateId);
                                userschedule.ScheduleType = name.ToString();
                                _unitOfWork.TimesheetUserSchedule.Update(userschedule);
                            }
                            else
                            {
                                var schedule = new TimesheetUserSchedule();
                                schedule.TimesheetTemplateId = Guid.Parse(model.TemplateId);
                                schedule.EmployeeId = Guid.Parse(item.EmployeeId);
                                schedule.ScheduleType = name.ToString();
                                _unitOfWork.TimesheetUserSchedule.Add(schedule);
                            }
                            _unitOfWork.SaveChanges();
                        }
                    }
                    return NoContent();
                }
                return BadRequest(ModelState);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.GetBaseException().Message);
            }
        }

        [HttpGet("getEmployee/{id}/{page?}/{pageSize?}/{name?}")]
        [Produces(typeof(EmployeeListViewModel))]
        public IActionResult GetEmployeeByTemplateId(string id, int? page = null, int? pageSize = null, string name = null)
        {
            try
            {
                var viewModel = new EmployeeListViewModel();
                var model = new List<EmployeeListModel>();
                var result = _unitOfWork.TimesheetTemplate.GetEmployeeByTemplateId(Guid.Parse(id), name, Convert.ToInt32(page), Convert.ToInt32(pageSize));
                if (result.Count() > 0)
                {
                    foreach (var item in result)
                    {
                        string TimesheetTemplateId = "";
                        if (item.TimesheetTemplateId.ToString() == "00000000-0000-0000-0000-000000000000")
                        {
                            TimesheetTemplateId = "0";
                        }
                        else
                        {
                            TimesheetTemplateId = item.TimesheetTemplateId.ToString();
                        }
                        model.Add(new EmployeeListModel
                        {
                            EmployeeId = item.Employee.Id.ToString(),
                            FullName = item.Employee.ApplicationUser.FullName,
                            TemplateId = TimesheetTemplateId.ToString()
                        });
                    }
                }
                viewModel.EmployeeList = model;
                viewModel.TotalCount = result.TotalCount;
                return Ok(viewModel);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.GetBaseException().Message);
            }

        }

        private List<DropDownList> GetddlTemplate()
        {
            try
            {
                var viewModel = new List<DropDownList>();
                var templateList = _unitOfWork.TimesheetTemplate.GetAll();
                if (templateList.Count() > 0)
                {
                    foreach (var item in templateList)
                    {
                        viewModel.Add(new DropDownList { Label = item.TempalteName, Value = item.Id.ToString() });
                    }
                }
                return viewModel;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
    }
}