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
    public class TimeSheetClientController : Controller
    {
        readonly ILogger _logger;
        readonly IUnitOfWork _unitOfWork;
        public TimeSheetClientController(ILogger<TimeSheetClientController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        [HttpGet("list/{page?}/{pageSize?}/{name?}")]
        [Produces(typeof(ClientViewModel))]
        public IActionResult GetAll(int? page=null,int?pageSize=null,string name=null)
        {
            var result = new ClientViewModel();
            var viewModel = new List<ClientModel>();
            var model = _unitOfWork.Client.GetAllClient(name, Convert.ToInt32(page), Convert.ToInt32(pageSize));
            if (model.Count() > 0)
            {
                foreach (var item in model)
                {
                    viewModel.Add(new ClientModel { Id = item.Id.ToString(), Name = item.Name, EmailId = item.EmailId, Contact = item.Contact, Address = item.Address });
                }
            }
            result.ClientList = viewModel;
            result.TotalCount = model.TotalCount;
            return Ok(result);
        }

        [HttpPost("create")]
        public IActionResult Create([FromBody]ClientModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (model == null)
                        return BadRequest($"{nameof(model)} cannot be null");
                    if (string.IsNullOrEmpty(model.Name))
                        return BadRequest("model property cannot be null");
                    var client = new TimesheetClient();
                    client.Name = model.Name;
                    client.EmailId = model.EmailId;
                    client.Address = model.Address;
                    client.Contact = model.Contact;
                    _unitOfWork.Client.Add(client);
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

        [HttpPut("update/{id}")]
        public IActionResult Update(string id,[FromBody]ClientModel model)
        {
            try
            {
                if (id != null)
                {
                    if (model == null)
                        return BadRequest($"{nameof(model)} cannot be null");
                    if (string.IsNullOrEmpty(model.Name))
                        return BadRequest("model property cannot be null");
                    var client = _unitOfWork.Client.Get(Guid.Parse(id));
                    if(client != null)
                    {
                        client.Name = model.Name;
                        client.EmailId = model.EmailId;
                        client.Address = model.Address;
                        client.Contact = model.Contact;
                        _unitOfWork.Client.Update(client);
                        _unitOfWork.SaveChanges();
                        return NoContent();
                    }
                    return BadRequest("client cannot be null");
                }
                return BadRequest("Id cannot be null");
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
                if (id != null)
                {
                    var checkClient = _unitOfWork.Client.Get(Guid.Parse(id));
                    if (checkClient != null)
                    {
                        _unitOfWork.Client.Remove(checkClient);
                        _unitOfWork.SaveChanges();
                        return NoContent();
                    }
                    else
                    {
                        return BadRequest("checkClient cannot be null");
                    }
                }
                return BadRequest("id cannot be null");
            }
            catch (Exception ex)
            {

                return BadRequest(ex.GetBaseException().Message);
            }
        }
    }
}