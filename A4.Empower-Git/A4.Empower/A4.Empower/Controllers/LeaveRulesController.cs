using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using A4.BAL;
using A4.DAL.Entites;
using DAL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace A4.Empower.Controllers
{
    [Authorize(AuthenticationSchemes = OpenIddict.Validation.AspNetCore.OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    public class LeaveRulesController : Controller
    {
        readonly ILogger _logger;
        readonly IUnitOfWork _unitOfWork;
        public LeaveRulesController(ILogger<LeaveRulesController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        [HttpGet("list/{page?}/{pageSize?}/{name?}")]
        [Produces(typeof(LeaveRulesViewModel))]
        public IActionResult GetAll(int? page = null, int? pageSize = null, string name = null)
        {
            try
            {
                var result = new LeaveRulesViewModel();
                var viewModel = new List<LeaveRulesModel>();
                Guid periodId = _unitOfWork.LeavePeriod.GetLeavePeriodId();
                var model = _unitOfWork.LeaveRules.GetAllLeaveRules(periodId, name, Convert.ToInt32(page), Convert.ToInt32(pageSize));
                if (model.Count() > 0)
                {

                    foreach (var item in model)
                    {
                        viewModel.Add(new LeaveRulesModel { Id = item.Id.ToString(), Name = item.Name, BandId = item.BandId.ToString(), LeavePeriodId = item.LeavePeriodId.ToString(), LeaveTypeId = item.LeaveTypeId.ToString(), LeavesPerYear = item.LeavesPerYear, BandList = GetBandList(), LeaveTypeList = GetLeaveTypeList(item.LeavePeriodId) });
                    }
                }
                else
                {
                    viewModel.Add(new LeaveRulesModel { BandList = GetBandList(), LeaveTypeList = GetLeaveTypeList(periodId) });
                }
                result.TotalCount = model.TotalCount;
                result.LeaveRulesModel = viewModel;
                return Ok(result);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.GetBaseException().Message);
            }
        }

        [HttpGet("leaveRules/{id}")]
        [Produces(typeof(LeaveRulesModel))]
        public IActionResult GetById(string id)
        {
            try
            {
                if (!string.IsNullOrEmpty(id))
                {
                    var model = _unitOfWork.LeaveRules.Get(new Guid(id));
                    if (model != null)
                    {
                        var viewModel = new LeaveRulesModel()
                        {
                            Id = model.Id.ToString(),
                            Name = model.Name,
                            LeavesPerYear = model.LeavesPerYear,
                            BandId = model.BandId.ToString(),
                            LeaveTypeId = model.LeaveTypeId.ToString(),
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
        public IActionResult Create([FromBody]LeaveRulesModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (model == null)
                        return BadRequest($"{nameof(model)} cannot be null");

                    if (string.IsNullOrEmpty(model.Name))
                        return BadRequest(" Namecannot be null");
                    Guid periodId = _unitOfWork.LeavePeriod.GetLeavePeriodId();
                    if (periodId != Guid.Empty)
                    {
                       var checkleaves = _unitOfWork.EmployeeLeaves.Find(m => m.LeavePeriodId == periodId && m.IsActive == true).ToList();
                        //if (checkleaves.Count() == 0)
                        //{
                            bool check = _unitOfWork.LeaveRules.CheckLeaveRules(new Guid(model.BandId), new Guid(model.LeaveTypeId), periodId);
                            if (!check)
                            {
                                var Model = new LeaveRules()
                                {
                                    Name = model.Name,
                                    LeavesPerYear = model.LeavesPerYear,
                                    BandId = new Guid(model.BandId),
                                    LeaveTypeId = new Guid(model.LeaveTypeId),
                                    LeavePeriodId = periodId
                                };
                                _unitOfWork.LeaveRules.Add(Model);
                                _unitOfWork.SaveChanges();
                                return NoContent();
                            }
                            else
                            {
                                return BadRequest("Leave Rules already exists");
                            }
                        //}
                        //else
                        //{
                        //    return BadRequest("NO new leave rules can be added");
                        //}

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
        public IActionResult Update(string id, [FromBody]LeaveRulesModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (model == null)
                        return BadRequest($"{nameof(model)} cannot be null");

                    if (string.IsNullOrEmpty(id))
                        return BadRequest("Id property cannot be null");


                    var check = _unitOfWork.LeaveRules.Get(new Guid(id));
                    if (check != null)
                    {
                        bool exist = _unitOfWork.LeaveRules.CheckLeaveRules(new Guid(model.BandId), new Guid(model.LeaveTypeId), new Guid(model.LeavePeriodId));
                        if (!exist)
                        {
                            check.Name = model.Name;
                            check.LeavesPerYear = model.LeavesPerYear;
                            check.BandId = new Guid(model.BandId);
                            check.LeaveTypeId = new Guid(model.LeaveTypeId);
                            _unitOfWork.LeaveRules.Update(check);
                            _unitOfWork.SaveChanges();
                            return NoContent();
                        }
                        else if ((check.LeavesPerYear != model.LeavesPerYear) || (check.Name != model.Name) || (check.LeavesPerYear != model.LeavesPerYear && check.Name != model.Name))
                        {
                            check.Name = model.Name;
                            check.LeavesPerYear = model.LeavesPerYear;
                            _unitOfWork.LeaveRules.Update(check);
                            _unitOfWork.SaveChanges();
                            return NoContent();
                        }
                        else
                        {
                            return BadRequest("Leave Rules already exists");
                        }

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
                    var checkexist = _unitOfWork.LeaveRules.Get(new Guid(id));
                    if (checkexist != null)
                    {
                        _unitOfWork.LeaveRules.Remove(checkexist);
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

        private List<DropDownList> GetBandList()
        {
            var BandVM = new List<DropDownList>();
            var BandList = _unitOfWork.Band.Find(m => m.IsActive).ToList();
            if (BandList.Count > 0)
            {
                foreach (var item in BandList)
                {
                    BandVM.Add(new DropDownList { Label = item.Name, Value = item.Id.ToString() });
                }
            }
            return BandVM;
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