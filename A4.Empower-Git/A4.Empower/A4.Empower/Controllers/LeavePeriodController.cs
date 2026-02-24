using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using A4.DAL.Entites;
using Microsoft.Extensions.Logging;
using DAL;
using A4.Empower.ViewModels;
using A4.BAL;
using Microsoft.AspNetCore.Authorization;

namespace A4.Empower.Controllers
{

    [Authorize(AuthenticationSchemes = OpenIddict.Validation.AspNetCore.OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    public class LeavePeriodController : Controller
    {
        readonly ILogger _logger;
        readonly IUnitOfWork _unitOfWork;
        public LeavePeriodController(ILogger<LeavePeriodController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        [HttpGet("list/{page?}/{pageSize?}/{name?}")]
        [Produces(typeof(LeavePeriodViewModel))]
        public IActionResult GetAll(int? page = null, int? pageSize = null, string name = null)
        {
          
            try
            {
                var result = new LeavePeriodViewModel();
                var viewModel = new List<LeavePeriodModel>();
                var model = _unitOfWork.LeavePeriod.GetAllLeavePeriod(name, Convert.ToInt32(page), Convert.ToInt32(pageSize));
                if (model.Count() > 0)
                {
                   
                    foreach (var item in model)
                    {
                        bool isedit = false;
                        var checkEmployeeLeaves = _unitOfWork.EmployeeLeaves.Find(c => c.IsActive &&c.LeavePeriodId == item.Id).ToList();
                        if (checkEmployeeLeaves.Count() == 0)
                        {
                            isedit = true;
                        }

                        viewModel.Add(new LeavePeriodModel { Id = item.Id.ToString(), Name = item.Name, PeriodStart = item.PeriodStart, PeriodEnd = item.PeriodEnd, IsLeavePeriodCompleted = item.IsLeavePeriodCompleted , IsEdit = isedit });
                    }
                }
                result.LeavePeriodModel = viewModel;
                result.TotalCount = model.TotalCount;
                return Ok(result);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.GetBaseException().Message);
            }
        }

        [HttpGet("leavePeriod/{id}")]
        [Produces(typeof(LeavePeriodModel))]
        public IActionResult GetById(string id)
        {
            try
            {
                if (!string.IsNullOrEmpty(id))
                {
                    var model = _unitOfWork.LeavePeriod.Get(new Guid(id));
                    if (model != null)
                    {
                        var viewModel = new LeavePeriodModel() {
                        Id = model.Id.ToString(),
                        Name = model.Name,
                        PeriodStart = model.PeriodStart,
                        PeriodEnd = model.PeriodEnd,
                        IsLeavePeriodCompleted = model.IsLeavePeriodCompleted
                    };
                        return Ok(viewModel);
                    }
                    else
                    {
                        return NotFound(id);
                    }
                }
                return BadRequest(" Id cannot be null");
            }
            catch (Exception ex)
            {

                return BadRequest(ex.GetBaseException().Message);
            }
        }

        [HttpPost("create")]
        public IActionResult Create([FromBody]LeavePeriodModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (model == null)
                        return BadRequest($"{nameof(model)} cannot be null");

                    if (model.PeriodStart.Date > model.PeriodEnd.Date  )
                        return BadRequest("End date cannot be before start date ");
                    else if (model.PeriodEnd.Date < DateTime.Now.Date && model.PeriodStart.Date < DateTime.Now.Date)
                        return BadRequest("End date cannot be before current date && start date cannot be after current date ");
                    //else if (model.PeriodStart.Date < DateTime.Now.Date)
                    //    return BadRequest("start date cannot be after current date ");
                    else if (model.PeriodEnd.Date < DateTime.Now.Date)
                        return BadRequest("End date cannot be before current date ");
                 

                    bool check = _unitOfWork.LeavePeriod.CheckLeavePeriod();
                    if (!check)
                    {
                        var Model = new LeavePeriod()
                        {
                            Name = model.Name,
                            PeriodStart = model.PeriodStart,
                            PeriodEnd = model.PeriodEnd,
                            IsLeavePeriodCompleted = false
                        };
                        _unitOfWork.LeavePeriod.Add(Model);
                        _unitOfWork.SaveChanges();
                        bool isLeaveWorkingDay = _unitOfWork.LeaveWorkingDay.CheckLeaveWorkingDay();
                       
                        if (isLeaveWorkingDay)
                        {
                            _unitOfWork.LeaveWorkingDay.Save(Model.Id);
                        }
                        bool holidayList = _unitOfWork.LeaveHolidayList.CheckHolidayList();
                        bool leaveRule = _unitOfWork.LeaveRules.UpdateLeaveRules(Model.Id);
                        bool leaveType = _unitOfWork.LeaveType.UpdateLeaveType(Model.Id);
                        return NoContent();
                    }
                    else
                    {
                        return BadRequest("Leave period already active");
                    }
                 
                }
                return BadRequest(ModelState);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.GetBaseException().Message);
            }
        }

        [HttpPut("update/{id}")]
        public IActionResult Update(string id, [FromBody]LeavePeriodModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (model == null)
                        return BadRequest($"{nameof(model)} cannot be null");

                    if (string.IsNullOrEmpty(model.Name) && string.IsNullOrEmpty(id))
                        return BadRequest("model property cannot be null");
                    else if (model.PeriodStart.Date > model.PeriodEnd.Date)
                        return BadRequest("End date cannot be before start date ");
                    else if (model.PeriodEnd.Date < DateTime.Now.Date && model.PeriodStart.Date < DateTime.Now.Date)
                        return BadRequest("End date cannot be before current date && start date cannot be after current date ");
                    //else if (model.PeriodStart.Date < DateTime.Now.Date)
                    //    return BadRequest("start date cannot be after current date ");
                    else if (model.PeriodEnd.Date < DateTime.Now.Date)
                        return BadRequest("End date cannot be before current date ");

                  
                    var check = _unitOfWork.LeavePeriod.Get(new Guid(id));
                    if (check != null)
                    {
                        check.Name = model.Name;
                        check.PeriodStart = model.PeriodStart;
                        check.PeriodEnd = model.PeriodEnd;
                        _unitOfWork.LeavePeriod.Update(check);
                        _unitOfWork.SaveChanges();
                        return NoContent();

                    }
                    return NotFound(model.Id);
                }
                return BadRequest(ModelState);
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
                if (!string.IsNullOrEmpty(id))
                {
                    var checkexist = _unitOfWork.LeavePeriod.Get(new Guid(id));
                    if (checkexist != null)
                    {
                        _unitOfWork.LeavePeriod.Remove(checkexist);
                        _unitOfWork.SaveChanges();
                        return NoContent();
                    }
                    else
                    {
                        return NotFound(id);
                    }
                }
                return BadRequest(" Id cannot be null");
            }
            catch (Exception ex)
            {

                return BadRequest(ex.GetBaseException().Message);
            }
        }
    }
}