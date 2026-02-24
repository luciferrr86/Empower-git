using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using A4.DAL.Entites;
using A4.Empower.ViewModels;
using DAL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace A4.Empower.Controllers
{

    [Route("api/[controller]")]
    public class TimeSheetConfigurationController : Controller
    {
        readonly ILogger _logger;
        readonly IUnitOfWork _unitOfWork;
        public TimeSheetConfigurationController(ILogger<TimeSheetConfigurationController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        [HttpGet("getAll")]
        [Produces(typeof(TimesheetConfigurationViewModel))]
        public IActionResult GetAll()
        {
            try
            {
                var result = new TimesheetConfigurationViewModel();
                var configuration = _unitOfWork.TimesheetConfiguration.GetTimeSheetConfiguration();
                if (configuration != null)
                {
                    result.Id = configuration.Id.ToString();
                    result.TimeSheetFrequency = configuration.TimesheetFrequency;
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.GetBaseException().Message);
            }

        }

        [HttpPost("create")]
        public IActionResult Create([FromBody] TimesheetConfigurationViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (model == null)
                        return BadRequest($"{nameof(model)} cannot be null");
                    var configuration = _unitOfWork.TimesheetConfiguration.GetTimeSheetConfiguration();
                    if (configuration == null)
                    {
                        var result = new TimesheetConfiguration();
                        if (model.TimeSheetFrequency == 1)
                        {
                            result.Name = "Daily";
                        }
                        else
                        {
                            result.Name = "Weekly";
                        }
                        result.TimesheetFrequency = model.TimeSheetFrequency;
                        _unitOfWork.TimesheetConfiguration.Add(result);
                        _unitOfWork.SaveChanges();
                        return NoContent();
                    }
                    return BadRequest("Configuration is already set you can update the Configuration");
                }
                return BadRequest(ModelState);

            }
            catch(Exception ex)
            {
                return BadRequest(ex.GetBaseException().Message);
            }
            
        }

        [HttpPut("update/{id}")]
        public IActionResult Update(string id,[FromBody]TimesheetConfigurationViewModel model)
        {
            try
            {
                if(id != null)
                {
                    if (ModelState.IsValid)
                    {
                        if(model == null)
                            return BadRequest($"{nameof(model)} cannot be null");
                        var checkconfig = _unitOfWork.TimesheetConfiguration.Get(Guid.Parse(id));
                        if(checkconfig != null)
                        {
                            checkconfig.Id = Guid.Parse(model.Id);
                            checkconfig.TimesheetFrequency = model.TimeSheetFrequency;
                            if (model.TimeSheetFrequency == 1)
                            {
                                checkconfig.Name = "Daily";
                            }
                            else
                            {
                                checkconfig.Name = "Weekly";
                            }
                            _unitOfWork.TimesheetConfiguration.Update(checkconfig);
                            _unitOfWork.SaveChanges();
                            return NoContent();
                        }
                        return BadRequest("checkconfig cannot be null");
                    }
                    return BadRequest(ModelState);
                }
                return BadRequest("id cannot be null");
            }
            catch(Exception ex)
            {
                return BadRequest(ex.GetBaseException().Message);
            }
        }

        [HttpDelete("delete/{id}")]
        public IActionResult Delete(string id)
        {
            try
            {
                if(id != null)
                {
                    var checkconfig = _unitOfWork.TimesheetConfiguration.Get(Guid.Parse(id));
                    if(checkconfig != null)
                    {
                        _unitOfWork.TimesheetConfiguration.Remove(checkconfig);
                        _unitOfWork.SaveChanges();
                        return NoContent();
                    }
                    else
                    {
                        return BadRequest("checkConfig cannot be null");
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