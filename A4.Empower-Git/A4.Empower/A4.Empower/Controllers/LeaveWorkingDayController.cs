using System;
using System.Collections.Generic;
using System.Linq;
using A4.BAL;
using A4.DAL.Entites;
using A4.Empower.ViewModels;
using DAL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace A4.Empower.Controllers
{
    [Authorize(AuthenticationSchemes = OpenIddict.Validation.AspNetCore.OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    public class LeaveWorkingDayController : Controller
    {
        readonly ILogger _logger;
        readonly IUnitOfWork _unitOfWork;
        public LeaveWorkingDayController(ILogger<LeaveWorkingDayController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        [HttpGet("list")]
        [Produces(typeof(List<LeaveWorkingDayModel>))]
        public IActionResult GetAll()
        {
            try
            {

                var model = new List<LeaveWorkingDayModel>();
                Guid periodId = _unitOfWork.LeavePeriod.GetLeavePeriodId();
                var workingDay = _unitOfWork.LeaveWorkingDay.GetAllWorkingDay();
                if (workingDay.Count() > 0)
                {
                    foreach (var item in workingDay)
                    {
                        model.Add(new LeaveWorkingDayModel { Id = item.Id.ToString(), WorkingDay = item.WorkingDay, WorkingDayValue = item.WorkingDayValue, LeavePeriodId = item.LeavePeriodId.ToString() });
                    }
                }

                return Ok(model);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.GetBaseException().Message);
            }
        }

        [HttpPost("create")]
        public IActionResult Create()
        {
            try
            {
                    Guid periodId = _unitOfWork.LeavePeriod.GetLeavePeriodId();
                    if (periodId != Guid.Empty)
                    {
                     _unitOfWork.LeaveWorkingDay.Save(periodId);
                    _unitOfWork.SaveChanges();
                    return NoContent();
                    }
                    else
                    {
                        return BadRequest(" Leave period is not active");
                    }
               
            }
            catch (Exception ex)
            {

                return BadRequest(ex.GetBaseException().Message);
            }
        }

        [HttpPut("update")]
        public IActionResult Update( [FromBody]List<LeaveWorkingDayModel> model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (model == null)
                        return BadRequest($"{nameof(model)} cannot be null");
                    var selected = model.All(m => m.WorkingDayValue == "0");
                    if (!selected)
                    {
                        if (model.Count() > 0)
                        {
                            foreach (var item in model)
                            {
                                var check = _unitOfWork.LeaveWorkingDay.Get(new Guid(item.Id));
                                if (check != null)
                                {
                                    Guid periodId = _unitOfWork.LeavePeriod.GetLeavePeriodId();
                                    if (periodId != Guid.Empty)
                                    {
                                        check.WorkingDay = item.WorkingDay;
                                        check.WorkingDayValue = item.WorkingDayValue;
                                        check.LeavePeriodId = periodId;
                                        _unitOfWork.LeaveWorkingDay.Update(check);
                                        _unitOfWork.SaveChanges();
                                    }
                                    else
                                    {
                                        return BadRequest(" Leave period is not active");
                                    }
                                }
                                else
                                {
                                    return NotFound(item.Id);
                                }
                            }
                            return NoContent();
                        }
                    }
                    else
                    {
                        return BadRequest("Please select day");
                    }
                   
                }
                return BadRequest(ModelState);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.GetBaseException().Message);
            }
        }
    }
}