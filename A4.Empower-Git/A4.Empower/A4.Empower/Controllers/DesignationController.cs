using A4.BAL;
using A4.DAL.Entites;
using DAL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace A4.Empower.Controllers
{
    [Authorize(AuthenticationSchemes = OpenIddict.Validation.AspNetCore.OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    public class DesignationController : Controller
    {
        readonly ILogger _logger;
        readonly IUnitOfWork _unitOfWork;
        public DesignationController(ILogger<DesignationController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        [HttpGet("designationList/{page?}/{pageSize?}/{name?}")]
        [Produces(typeof(FunctionalDesignationViewModel))]
        public IActionResult GetAll(int? page = null, int? pageSize = null, string name = null)
        {
            var result = new FunctionalDesignationViewModel();
            var viewModel = new List<FunctionalDesignationModel>();
            var model = _unitOfWork.Designation.GetAllDesignation(name, Convert.ToInt32(page), Convert.ToInt32(pageSize));

            if (model.Count() > 0)
            {

                foreach (var item in model)
                {
                    viewModel.Add(new FunctionalDesignationModel { Id = item.Id.ToString(), Name = item.Name });
                }
            }
            result.FunctionalDesignationModel = viewModel;
            result.TotalCount = model.TotalCount;
            return Ok(result);
        }

        [HttpGet("designation/{id}")]
        [Produces(typeof(FunctionalDesignationModel))]
        public IActionResult GetById(string id)
        {

            if (!string.IsNullOrEmpty(id))
            {
                var model = _unitOfWork.Designation.Get(new Guid(id));
                if (model != null)
                {
                    var viewModel = new FunctionalDesignationModel();
                    viewModel.Id = model.Id.ToString();
                    viewModel.Name = model.Name;
                    return Ok(viewModel);
                }
                else
                {
                    return NotFound(id);
                }
            }

            return BadRequest(" Id cannot be null");
        }

        [HttpPost("create")]
        public IActionResult Create([FromBody]FunctionalDesignationModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (model == null)
                        return BadRequest($"{nameof(model)} cannot be null");

                    if (string.IsNullOrEmpty(model.Name))
                        return BadRequest(" Namecannot be null");


                    var Model = new FunctionalDesignation();
                    Model.Name = model.Name;
                    _unitOfWork.Designation.Add(Model);
                    return NoContent();
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
        public IActionResult Update(string id, [FromBody]FunctionalDesignationModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (model == null)
                        return BadRequest($"{nameof(model)} cannot be null");


                    if (string.IsNullOrEmpty(model.Name) && string.IsNullOrEmpty(id))
                        return BadRequest("model property cannot be null");
                    var checkdesign = _unitOfWork.Designation.Get(new Guid(id));
                    if (checkdesign != null)
                    {
                        checkdesign.Name = model.Name;
                        _unitOfWork.Designation.Update(checkdesign);
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
                    var checkexist = _unitOfWork.Designation.Get(new Guid(id));
                    if (checkexist != null)
                    {
                        _unitOfWork.Designation.Remove(checkexist);
                        return NoContent();
                    }
                    else
                    {
                        return NotFound(id);
                    }
                }
                return NoContent();
            }
            catch (Exception ex)
            {

                return BadRequest(ex.GetBaseException().Message);
            }
        }
    }
}