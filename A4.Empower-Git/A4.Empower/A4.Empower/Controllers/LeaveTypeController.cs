using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using A4.BAL;
using A4.DAL.Entites;
using A4.Empower.ViewModels;
using DAL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace A4.Empower.Controllers
{
    [Authorize(AuthenticationSchemes = OpenIddict.Validation.AspNetCore.OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    public class LeaveTypeController : Controller
    {
        readonly ILogger _logger;
        readonly IUnitOfWork _unitOfWork;
        public LeaveTypeController(ILogger<LeaveTypeController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        [HttpGet("list/{page?}/{pageSize?}/{name?}")]
        [Produces(typeof(LeaveTypeViewModel))]
        public IActionResult GetAll(int? page = null, int? pageSize = null, string name = null)
        {
            try
            {
                bool flag = false;
                var result = new LeaveTypeViewModel();
                var viewModel = new List<LeaveTypeModel>();
                Guid periodId = _unitOfWork.LeavePeriod.GetLeavePeriodId();
                var model = _unitOfWork.LeaveType.GetAllLeaveType(periodId, name, Convert.ToInt32(page), Convert.ToInt32(pageSize));

                if (model.Count() > 0)
                {
                    foreach (var item in model)
                    {
                        var leaveRules = _unitOfWork.LeaveRules.Find(m => m.LeaveTypeId == item.Id).ToList();
                        if (leaveRules.Count > 0)
                        {
                            flag = true;
                        }
                        else
                        {
                            flag = false;
                        }
                        viewModel.Add(new LeaveTypeModel { Id = item.Id.ToString(), Name = item.Name,  LeavePeriodId = item.LeavePeriodId.ToString() , InUsed = flag });
                    }
                }
                result.TotalCount = model.TotalCount;
                result.LeaveTypeModel = viewModel;
                return Ok(result);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.GetBaseException().Message);
            }
        }

        [HttpGet("leaveType/{id}")]
        [Produces(typeof(LeaveTypeModel))]
        public IActionResult GetById(string id)
        {
            try
            {
                if (!string.IsNullOrEmpty(id))
                {
                    var model = _unitOfWork.LeaveType.Get(new Guid(id));
                    if (model != null)
                    {
                        var viewModel = new LeaveTypeModel()
                        {
                            Id = model.Id.ToString(),
                            Name = model.Name,
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
        public IActionResult Create([FromBody]LeaveTypeModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (model == null)
                        return BadRequest($"{nameof(model)} cannot be null");
                    Guid periodId = _unitOfWork.LeavePeriod.GetLeavePeriodId();
                    if (periodId != Guid.Empty)
                    {
                        var Model = new LeaveType();
                        Model.Name = model.Name;
                        Model.LeavePeriodId = periodId;
                        _unitOfWork.LeaveType.Add(Model);
                        _unitOfWork.SaveChanges();
                        return NoContent();
                    }
                    else
                    {
                        return BadRequest(" Leave period is not active");
                    }
                }
                return BadRequest(ModelState);
            }
            catch (DbUpdateException ex) when (ex.InnerException is SqlException sqlException && (sqlException.Number == 2627 || sqlException.Number == 2601))
            {
                return BadRequest(model.Name + " already exists");
            }
            catch (Exception ex)
            {

                return BadRequest(ex.GetBaseException().Message);
            }
        }

        [HttpPut("update/{id}")]
        public IActionResult Update(string id, [FromBody]LeaveTypeModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (model == null)
                        return BadRequest($"{nameof(model)} cannot be null");

                    if (string.IsNullOrEmpty(id))
                        return BadRequest("Id property cannot be null");
                    var check = _unitOfWork.LeaveType.Get(new Guid(id));
                    if (check != null)
                    {
                        check.Name = model.Name;
                        check.LeavePeriodId = new Guid(model.LeavePeriodId);
                        _unitOfWork.LeaveType.Update(check);
                        _unitOfWork.SaveChanges();
                        return NoContent();

                    }
                    return NotFound(model.Id);
                }
                return BadRequest(ModelState);
            }
            catch (DbUpdateException ex) when (ex.InnerException is SqlException sqlException && (sqlException.Number == 2627 || sqlException.Number == 2601))
            {
                return BadRequest(model.Name + " already exists");
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
                    var checkexist = _unitOfWork.LeaveType.Get(new Guid(id));
                    if (checkexist != null)
                    {
                        _unitOfWork.LeaveType.Remove(checkexist);
                        _unitOfWork.SaveChanges();
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