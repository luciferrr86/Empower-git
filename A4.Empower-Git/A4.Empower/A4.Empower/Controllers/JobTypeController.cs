using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using A4.BAL;
using A4.DAL.Entites;
using A4.Empower.ViewModels;
using DAL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace A4.Empower.Controllers
{
    [Authorize(AuthenticationSchemes = OpenIddict.Validation.AspNetCore.OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    public class JobTypeController : Controller
    {
        readonly ILogger _logger;
        readonly IUnitOfWork _unitOfWork;
        public JobTypeController(ILogger<JobTypeController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        [HttpGet("list/{page?}/{pageSize?}/{name?}")]
        [Produces(typeof(JobTypeViewModel))]
        public IActionResult GetAll(int? page = null, int? pageSize = null, string name = null)
        {
            try
            {
                var result = new JobTypeViewModel();
                var model = new List<JobTypeModel>();
                var jobType = _unitOfWork.JobType.GetAllJobType(name, Convert.ToInt32(page), Convert.ToInt32(pageSize));
                if (jobType.Count() > 0)
                {
                    foreach (var item in jobType)
                    {
                        model.Add(new JobTypeModel { Id = item.Id.ToString(), Name = item.Name });
                    }
                }
                result.JobTypeModel = model;
                result.TotalCount = jobType.TotalCount;
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.GetBaseException().Message);
            }
        }

        [HttpGet("jobType/{id}")]
        [Produces(typeof(JobTypeModel))]
        public IActionResult GetById(string id)
        {
            try
            {
                if (!string.IsNullOrEmpty(id))
                {
                    var model = _unitOfWork.JobType.Get(new Guid(id));
                    if (model != null)
                    {
                        var viewModel = new JobTypeModel();
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
            catch (Exception ex)
            {
                return BadRequest(ex.GetBaseException().Message);
            }
        }

        [HttpPost("create")]
        public IActionResult Create([FromBody]JobTypeModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (model == null)
                        return BadRequest($"{nameof(model)} cannot be null");

                    if (string.IsNullOrEmpty(model.Name))
                        return BadRequest(" Name cannot be null");

                    var Model = new JobType();
                    Model.Name = model.Name;
                    _unitOfWork.JobType.Add(Model);
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
        public IActionResult Update(string id, [FromBody]JobTypeModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (model == null)
                        return BadRequest($"{nameof(model)} cannot be null");

                    if (string.IsNullOrEmpty(model.Name) && string.IsNullOrEmpty(id))
                        return BadRequest("model property cannot be null");
                    var checkdept = _unitOfWork.JobType.Get(new Guid(id));
                    if (checkdept != null)
                    {
                        checkdept.Name = model.Name;
                        _unitOfWork.JobType.Update(checkdept);
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
                    var checkexist = _unitOfWork.JobType.Get(new Guid(id));
                    if (checkexist != null)
                    {
                        _unitOfWork.JobType.Remove(checkexist);
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