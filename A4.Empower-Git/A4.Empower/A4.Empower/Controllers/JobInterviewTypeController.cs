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
    public class JobInterviewTypeController : Controller
    {
        readonly ILogger _logger;
        readonly IUnitOfWork _unitOfWork;
        public JobInterviewTypeController(ILogger<JobInterviewTypeController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        [HttpGet("interviewTypeList/{page?}/{pageSize?}/{name?}")]
        [Produces(typeof(JobInterviewTypeViewModel))]
        public IActionResult GetAll(int? page = null, int? pageSize = null, string name = null)
        {
            try
            {
                var result = new JobInterviewTypeViewModel();
                var model = new List<JobInterviewTypeModel>();               
                var interviewType = _unitOfWork.JobInterviewType.GetAllJobInterviewType(name, Convert.ToInt32(page), Convert.ToInt32(pageSize));
                if (interviewType.Count() > 0)
                {
                    foreach (var item in interviewType)
                    {
                        model.Add(new JobInterviewTypeModel { Id = item.Id.ToString(), Name = item.Name });
                    }
                }
                result.JobInterviewTypeModel = model;
                result.TotalCount = interviewType.TotalCount;
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.GetBaseException().Message);
            }
        }

        [HttpGet("interviewType/{id}")]
        [Produces(typeof(JobInterviewTypeModel))]
        public IActionResult GetById(string id)
        {
            try
            {
                if (!string.IsNullOrEmpty(id))
                {
                    var model = _unitOfWork.JobInterviewType.Get(new Guid(id));
                    if (model != null)
                    {
                        var viewModel = new JobInterviewTypeModel();
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
        public IActionResult Create([FromBody]JobInterviewTypeModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (model == null)
                        return BadRequest($"{nameof(model)} cannot be null");

                    if (string.IsNullOrEmpty(model.Name))
                        return BadRequest(" Namecannot be null");

                    var Model = new JobInterviewType();
                    Model.Name = model.Name;
                    _unitOfWork.JobInterviewType.Add(Model);
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
        public IActionResult Update(string id, [FromBody]JobInterviewTypeModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (model == null)
                        return BadRequest($"{nameof(model)} cannot be null");

                    if (string.IsNullOrEmpty(model.Name) && string.IsNullOrEmpty(id))
                        return BadRequest("model property cannot be null");
                    var checkdept = _unitOfWork.JobInterviewType.Get(new Guid(id));
                    if (checkdept != null)
                    {
                        checkdept.Name = model.Name;
                        _unitOfWork.JobInterviewType.Update(checkdept);
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
                    var checkexist = _unitOfWork.JobInterviewType.Get(new Guid(id));
                    if (checkexist != null)
                    {
                        _unitOfWork.JobInterviewType.Remove(checkexist);
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