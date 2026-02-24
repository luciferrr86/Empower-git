using System;
using System.Collections.Generic;
using System.Linq;
using A4.DAL.Entites;
using A4.Empower.ViewModels;
using AutoMapper;
using DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using A4.BAL;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using OpenIddict.Validation.AspNetCore;

namespace A4.Empower.Controllers
{
    [Authorize(AuthenticationSchemes = OpenIddict.Validation.AspNetCore.OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    public class DepartmentController : Controller
    {
        readonly ILogger _logger;
        readonly IUnitOfWork _unitOfWork;
        public DepartmentController(ILogger<DepartmentController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        [HttpGet("departmentList/{page?}/{pageSize?}/{name?}")]
        [Produces(typeof(FunctionalDepartmentViewModel))]
        public IActionResult GetAll(int? page = null, int? pageSize = null, string name = null)
        {
            var result = new FunctionalDepartmentViewModel();
            var viewModel = new List<FunctionalDepartmentModel>();
            var model = _unitOfWork.Department.GetAllDepartment(name, Convert.ToInt32(page), Convert.ToInt32(pageSize));
            if (model.Count() > 0)
            {
                foreach (var item in model)
                {
                    viewModel.Add(new FunctionalDepartmentModel { Id = item.Id.ToString(), Name = item.Name });
                }
            }
            result.FunctionalDepartmentModel = viewModel;
            result.TotalCount = model.TotalCount;
            return Ok(result);
        }

        [HttpGet("department/{id}")]
        [Produces(typeof(FunctionalDepartmentModel))]
        public IActionResult GetById(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                var model = _unitOfWork.Department.Get(new Guid(id));
                if (model != null)
                {
                    var viewModel = new FunctionalDepartmentModel();
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
        public IActionResult Create([FromBody]FunctionalDepartmentModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (model == null)
                        return BadRequest($"{nameof(model)} cannot be null");

                    if (string.IsNullOrEmpty(model.Name))
                        return BadRequest(" Namecannot be null");

                    var Model = new FunctionalDepartment();
                    Model.Name = model.Name;
                    _unitOfWork.Department.Add(Model);
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
        public IActionResult Update(string id, [FromBody]FunctionalDepartmentModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (model == null)
                        return BadRequest($"{nameof(model)} cannot be null");

                    if (string.IsNullOrEmpty(model.Name) && string.IsNullOrEmpty(id))
                        return BadRequest("model property cannot be null");
                    var checkdept = _unitOfWork.Department.Get(new Guid(id));
                    if (checkdept != null)
                    {
                        checkdept.Name = model.Name;
                        _unitOfWork.Department.Update(checkdept);
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
                    var checkexist = _unitOfWork.Department.Get(new Guid(id));
                    if (checkexist != null)
                    {
                        _unitOfWork.Department.Remove(checkexist);
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