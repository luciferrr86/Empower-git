using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using A4.BAL;
using A4.DAL.Entites;
using A4.Empower.ViewModels;
using AutoMapper;
using DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace A4.Empower.Controllers
{
    [Route("api/[controller]")]
    public class TitleController : Controller
    {
        readonly ILogger _logger;
        readonly IUnitOfWork _unitOfWork;
        public TitleController( ILogger<TitleController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        [HttpGet("titleList/{page?}/{pageSize?}/{name?}")]
        [Produces(typeof(FunctionalTitleViewModel))]
        public IActionResult GetAll(int? page = null, int? pageSize = null, string name = null)
        {
            var result = new FunctionalTitleViewModel();
            var viewModel = new List<FunctionalTitleModel>();
            var model = _unitOfWork.Title.GetAllTitle(name, Convert.ToInt32(page), Convert.ToInt32(pageSize));

            if (model.Count() > 0)
            {
                foreach (var item in model)
                {
                    viewModel.Add(new FunctionalTitleModel { Id = item.Id.ToString(), Name = item.Name });
                }
            }
            result.FunctionalTitleModel = viewModel;
            result.TotalCount = model.TotalCount;
            return Ok(result);
        }

        [HttpGet("title/{id}")]
        [Produces(typeof(FunctionalTitleModel))]
        public IActionResult GetById(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                var model = _unitOfWork.Title.Get(new Guid(id));
                if (model != null)
                {
                    var viewModel = new FunctionalTitleModel();
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
        public IActionResult Create([FromBody]FunctionalTitleModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (model == null)
                        return BadRequest($"{nameof(model)} cannot be null");

                    if (string.IsNullOrEmpty(model.Name))
                        return BadRequest(" Name cannot be null");

                    var Model = new FunctionalTitle();
                    Model.Name = model.Name;
                    _unitOfWork.Title.Add(Model);
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
        public IActionResult Update(string id, [FromBody]FunctionalTitleModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (model == null)
                        return BadRequest($"{nameof(model)} cannot be null");

                    if (string.IsNullOrEmpty(model.Name) && string.IsNullOrEmpty(id))
                        return BadRequest("model property cannot be null");
                    var checktitle = _unitOfWork.Title.Get(new Guid(id));
                    if (checktitle != null)
                    {
                        checktitle.Name = model.Name;
                        _unitOfWork.Title.Update(checktitle);
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
                    var checkexist = _unitOfWork.Title.Get(Guid.Parse(id));
                    if (checkexist != null)
                    {
                        _unitOfWork.Title.Remove(checkexist);
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