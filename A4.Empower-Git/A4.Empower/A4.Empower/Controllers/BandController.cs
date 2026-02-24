using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using A4.Empower.ViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using DAL;
using A4.DAL.Entites;
using A4.BAL;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;

namespace A4.Empower.Controllers
{
    [Route("api/[controller]")]
    public class BandController : Controller
    {
        readonly ILogger _logger;
        readonly IUnitOfWork _unitOfWork;
        public BandController(ILogger<BandController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        [HttpGet("bandList/{page?}/{pageSize?}/{name?}")]
        [Produces(typeof(BandViewModel))]
        public IActionResult GetAll(int? page = null, int? pageSize = null, string name = null)
        {
            var result = new BandViewModel();
            var viewModel = new List<BandModel>();
            var model = _unitOfWork.Band.GetAllBand(name, Convert.ToInt32(page), Convert.ToInt32(pageSize));

            if (model.Count() > 0)
            {

                foreach (var item in model)
                {
                    viewModel.Add(new BandModel { Id = item.Id.ToString(), Name = item.Name });
                }
            }
            result.BandModel = viewModel;
            result.TotalCount = model.TotalCount;
            return Ok(result);
        }

        [HttpGet("band/{id}")]
        [Produces(typeof(BandModel))]
        public IActionResult GetById(string id)
        {
            if (id != "")
            {
                var model = _unitOfWork.Band.Get(new Guid(id));
                if (model != null)
                {
                    var viewModel = new BandModel();
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
        public IActionResult Create([FromBody]BandModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (model == null)
                        return BadRequest($"{nameof(model)} cannot be null");

                    if (string.IsNullOrEmpty(model.Name))
                        return BadRequest(" Namecannot be null");

                    var Model = new Band();
                    Model.Name = model.Name;
                    _unitOfWork.Band.Add(Model);
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
        public IActionResult Update(string id, [FromBody]BandModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (model == null)
                        return BadRequest($"{nameof(model)} cannot be null");

                    if (string.IsNullOrWhiteSpace(model.Name) && string.IsNullOrEmpty(model.Name) && model.Id != "")
                        return BadRequest("model property cannot be null");
                    var checkexist = _unitOfWork.Band.Get(new Guid(id));
                    if (checkexist != null)
                    {
                        checkexist.Name = model.Name;
                        _unitOfWork.Band.Update(checkexist);
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
                    var checkexist = _unitOfWork.Band.Get(new Guid(id));
                    if (checkexist != null)
                    {
                        _unitOfWork.Band.Remove(checkexist);
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