using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using A4.BAL;
using A4.DAL.Entites;
using DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace A4.Empower.Controllers
{
    [Route("api/[controller]")]
    public class GroupController : Controller
    {
        readonly ILogger _logger;
        readonly IUnitOfWork _unitOfWork;
        public GroupController(ILogger<GroupController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        [HttpGet("groupList/{page?}/{pageSize?}/{name?}")]
        [Produces(typeof(FunctionalGroupViewModel))]
        public IActionResult GetAll(int? page = null, int? pageSize = null, string name = null)
        {
            var result = new FunctionalGroupViewModel();
            var viewModel = new List<FunctionalGroupModel>();
            var model = _unitOfWork.Group.GetAllGroup(name, Convert.ToInt32(page), Convert.ToInt32(pageSize));
            if (model.Count() > 0)
            {
                foreach (var item in model)
                {
                    viewModel.Add(new FunctionalGroupModel { Id = item.Id.ToString(), Name = item.Name, DepartmentId = item.DepartmentId.ToString()});
                }
            }
            var departmentVM = new List<DropDownList>();
            var departmentist = _unitOfWork.Department.Find(m => m.IsActive).ToList();
            if (departmentist.Count() > 0)
            {
                foreach (var item in departmentist)
                {
                    departmentVM.Add(new DropDownList { Label = item.Name, Value = item.Id.ToString() });

                }
            }
            result.DepartmentList = departmentVM;
            result.FunctionalGroupModel = viewModel;
            result.TotalCount = model.TotalCount;
            return Ok(result);
        }

        [HttpGet("group/{id}")]
        [Produces(typeof(FunctionalGroupModel))]
        public IActionResult GetById(string id)
        {

            if (!string.IsNullOrEmpty(id))
            {
                var model = _unitOfWork.Group.Get(new Guid(id));
                if (model != null)
                {
                    var viewModel = new FunctionalGroupModel();
                    viewModel.Id = model.Id.ToString();
                    viewModel.Name = model.Name;
                    viewModel.DepartmentId = model.DepartmentId.ToString();
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
        public IActionResult Create([FromBody]FunctionalGroupModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (model == null)
                        return BadRequest($"{nameof(model)} cannot be null");

                    if (string.IsNullOrEmpty(model.Name) && string.IsNullOrEmpty(model.DepartmentId))
                        return BadRequest(" model Property cannot be null");
                    var FunctionalGroup = new FunctionalGroup();
                    FunctionalGroup.Name = model.Name;
                    FunctionalGroup.DepartmentId = Guid.Parse(model.DepartmentId);
                    _unitOfWork.Group.Add(FunctionalGroup);
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
        public IActionResult Update(string id, [FromBody]FunctionalGroupModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (model == null)
                        return BadRequest($"{nameof(model)} cannot be null");

                    if (string.IsNullOrEmpty(id))
                        return BadRequest(" Id cannot be null");

                    if (string.IsNullOrEmpty(model.Name) && string.IsNullOrEmpty(id) && string.IsNullOrEmpty(model.DepartmentId))
                        return BadRequest("model property cannot be null");


                    var checkgroup = _unitOfWork.Group.Get(new Guid(id));
                    if (checkgroup != null)
                    {
                        checkgroup.DepartmentId = new Guid(model.DepartmentId);
                        checkgroup.Name = model.Name;
                        _unitOfWork.Group.Update(checkgroup);
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
                    var checkexist = _unitOfWork.Group.Get(new Guid(id));
                    if (checkexist != null)
                    {
                        _unitOfWork.Group.Remove(checkexist);
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