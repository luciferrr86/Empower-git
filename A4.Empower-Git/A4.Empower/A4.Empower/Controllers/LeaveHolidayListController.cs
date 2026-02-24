using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using A4.BAL;
using A4.DAL.Entites;
using A4.Empower.ViewModels;
using DAL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace A4.Empower.Controllers
{
    [Authorize(AuthenticationSchemes = OpenIddict.Validation.AspNetCore.OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)]
    [Route("api/LeaveHolidayList")]
    public class LeaveHolidayListController : Controller
    {
        readonly ILogger _logger;
        readonly IUnitOfWork _unitOfWork;
        public LeaveHolidayListController(ILogger<LeaveHolidayListController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }


        [HttpGet("list/{page?}/{pageSize?}/{name?}")]
        [Produces(typeof(LeaveHolidayListViewModel))]
        public IActionResult GetAll(int? page = null, int? pageSize = null, string name = null)
        {
            try
            {
                var result = new LeaveHolidayListViewModel();
                var viewModel = new List<LeaveHolidayListModel>();
                Guid periodId = _unitOfWork.LeavePeriod.GetLeavePeriodId();
                var model = _unitOfWork.LeaveHolidayList.GetAllHolidayList(periodId, name, Convert.ToInt32(page), Convert.ToInt32(pageSize));

                if (model.Count() > 0)
                {
                    foreach (var item in model)
                    {
                        viewModel.Add(new LeaveHolidayListModel { Id = item.Id.ToString(), Name = item.Name, HolidayDate = item.Holidaydate.Date, LeavePeriodId = item.LeavePeriodId.ToString() });
                    
                    }
                }
                result.TotalCount = model.TotalCount;
                result.LeaveHolidayListModel = viewModel;
                return Ok(result);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.GetBaseException().Message);
            }
        }

        [HttpGet("leaveHoliday/{id}")]
        [Produces(typeof(LeaveHolidayListModel))]
        public IActionResult GetById(string id)
        {
            try
            {
                if (!string.IsNullOrEmpty(id))
                {
                    var model = _unitOfWork.LeaveHolidayList.Get(new Guid(id));
                    if (model != null)
                    {
                        var viewModel = new LeaveHolidayListModel()
                        {
                            Id = model.Id.ToString(),
                            Name = model.Name,
                            HolidayDate = model.Holidaydate,
                           LeavePeriodId = model.LeavePeriodId.ToString()
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
        public IActionResult Create( [FromBody]LeaveHolidayListModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (model == null)
                        return BadRequest($"{nameof(model)} cannot be null");

                    var leaveperiod = _unitOfWork.LeavePeriod.GetleavePeriodRecord();
                    if (leaveperiod != null)
                    {
                        if (model.HolidayDate.Date >= leaveperiod.PeriodStart.Date && model.HolidayDate.Date <= leaveperiod.PeriodEnd.Date)
                        {
                            var check = _unitOfWork.LeaveHolidayList.Find(c => c.Holidaydate == model.HolidayDate && c.Name == model.Name && c.IsActive == true).FirstOrDefault();
                            if (check == null)
                            {
                                var Model = new LeaveHolidayList()
                                {
                                    Name = model.Name,
                                    Holidaydate = model.HolidayDate,
                                    LeavePeriodId = leaveperiod.Id
                                };
                                _unitOfWork.LeaveHolidayList.Add(Model);
                                return NoContent();
                            }
                            else
                            {
                                return BadRequest("This holiday name and date already exists");
                            }
                           
                        }
                        else
                        {
                            return BadRequest("Leaves not allowed on these dates as per companies leave period policy.");
                        }
                    }
                    else
                    {
                        return BadRequest(" Leave period is not active");
                    }

                 
                }
                return BadRequest(ModelState);
            }
            catch (Exception ex)
            {
                var error = ex.GetBaseException().Message.ToString();             
                if (error == "Cannot insert duplicate key row in object 'dbo.LeaveHolidayList' with unique index 'IX_LeaveHolidayList_Name'. The duplicate key value is ("+model.Name+").\r\nThe statement has been terminated.")
                {
                    return BadRequest("This holiday name already exists");
                }
                else
                {
                    return BadRequest(ex.GetBaseException().Message);
                }
            }
        }

        [HttpPut("update/{id}")]
        public IActionResult Update(string id, [FromBody]LeaveHolidayListModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (model == null)
                        return BadRequest($"{nameof(model)} cannot be null");

                    if (string.IsNullOrEmpty(id))
                        return BadRequest("model property cannot be null");

                    var leaveperiod = _unitOfWork.LeavePeriod.GetleavePeriodRecord();
                    if (leaveperiod != null)
                    {
                        if (model.HolidayDate.Date >= leaveperiod.PeriodStart.Date && model.HolidayDate.Date <= leaveperiod.PeriodEnd.Date)
                        {
                            var check = _unitOfWork.LeaveHolidayList.Get(new Guid(id));
                            if (check != null)
                            {
                                check.Name = model.Name;
                                check.Holidaydate = model.HolidayDate;
                                _unitOfWork.LeaveHolidayList.Update(check);
                                return NoContent();

                            }
                            return NotFound(model.Id);
                        }
                        else
                        {
                            return BadRequest("Leaves not allowed on these dates as per companies leave period policy.");
                        }
                    }
                    else
                    {
                        return BadRequest(" Leave period is not active");
                    }

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
                    var checkexist = _unitOfWork.LeaveHolidayList.Get(new Guid(id));
                    if (checkexist != null)
                    {
                        _unitOfWork.LeaveHolidayList.Remove(checkexist);

                        return NoContent();
                    }
                    else
                    {
                        return NotFound(id);
                    }
                }
                return BadRequest("Id cannot be null");
            }
            catch (Exception ex)
            {

                return BadRequest(ex.GetBaseException().Message);
            }
        }
    }
}