using A4.BAL;
using A4.DAL.Entites;
using DAL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OpenIddict.Validation.AspNetCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace A4.Empower.Controllers
{
    [Authorize(AuthenticationSchemes = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    public class EmailDirectoryController : Controller
    {
        readonly ILogger _logger;
        readonly IUnitOfWork _unitOfWork;
        public EmailDirectoryController(ILogger<EmailDirectoryController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        [HttpGet("list/{page?}/{pageSize?}/{name?}")]
        [Produces(typeof(DirectoryViewModel))]
        public IActionResult GetAll(int? page = null, int? pageSize = null, string name = null)
        {
            var result = new DirectoryViewModel ();
            var viewModel = new List<DirectoryModel>();
            var model = _unitOfWork.EmailDirectoryRepository.GetAllDirectory(name, Convert.ToInt32(page), Convert.ToInt32(pageSize));

            if (model.Count() > 0)
            {

                foreach (var item in model)
                {
                    viewModel.Add(new DirectoryModel
                    { 
                        Id = item.Id,
                         Name = item.Name , 
                         Email=item.Email,
                         Designation=item.Designation,
                         PhoneNumber=item.PhoneNumber
                    });
                }
            }
            result.DirectoryListModel = viewModel;
            result.TotalCount = model.TotalCount;
            return Ok(result);
        }

        [HttpGet("{id}")]
        [Produces(typeof(EmailDirectory))]
        public IActionResult GetById(string id)
        {

            if (!string.IsNullOrEmpty(id))
            {
                var model = _unitOfWork.EmailDirectoryRepository.Get(Guid.Parse(id));
                if (model != null)
                {
                    var viewModel = new EmailDirectory
                    {
                        Id = model.Id,
                        Name = model.Name
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

        [HttpPost("create")]
        public IActionResult Create([FromBody] EmailDirectory model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (model.Email == null)
                        return BadRequest($"Email cannot be null");

                    if (string.IsNullOrEmpty(model.Name))
                        return BadRequest(" Name cannot be null");

                    _unitOfWork.EmailDirectoryRepository.Add(model);
                    _unitOfWork.SaveChanges();
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

        [HttpPut]
        public IActionResult Update( [FromBody] EmailDirectory model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (model.Email == null)
                        return BadRequest($"{nameof(model)} cannot be null");
                    
                    if (string.IsNullOrEmpty(model.Name) && model.Id!=null)
                        return BadRequest("Model property cannot be null");
                    
                    var dir = _unitOfWork.EmailDirectoryRepository.Get(model.Id);
                    if (dir != null)
                    {
                        dir.Email = model.Email;
                        dir.Designation = model.Designation;
                        dir.Name = model.Name;
                        dir.PhoneNumber = dir.PhoneNumber;
                        _unitOfWork.EmailDirectoryRepository.Update(dir);
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
                    var checkexist = _unitOfWork.EmailDirectoryRepository.Get(Guid.Parse(id));
                    if (checkexist != null)
                    {
                        _unitOfWork.EmailDirectoryRepository.Remove(checkexist);
                        _unitOfWork.SaveChanges();
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

        [HttpGet]
        public ActionResult<List<DropDownList>> GetDirectoryList()
        {

            var dropDownLists = new List<DropDownList>();
            var data = _unitOfWork.EmailDirectoryRepository.Find(m => m.IsActive).ToList();
            if (data.Count() > 0)
            {
                foreach (var item in data)
                {
                    dropDownLists.Add(new DropDownList { Label = item.Email, Value = item.Id.ToString() });
                }
            }
            return dropDownLists;
        }
    }
}

