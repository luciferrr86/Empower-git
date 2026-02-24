using A4.BAL;
using A4.Empower.Helpers;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using A4.Empower.Enum;

namespace A4.Empower.Controllers
{
    [Route("api/[controller]")]
    public class ClientController : Controller
    {
        //readonly ILogger _logger;
        //readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;
        readonly IEmailer _emailer;
        public ClientController(/*ILogger<ClientController> logger, IUnitOfWork unitOfWork,*/ IConfiguration configuration, IEmailer emailer)
        {
            //_logger = logger;
            //_unitOfWork = unitOfWork;
            _configuration = configuration;
            _emailer = emailer;

        }

        [HttpPost("contact")]
        public async Task<IActionResult> Contact([FromBody] Contact model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (model == null)
                        return BadRequest($"{nameof(model)} cannot be null");
                    string hrName = _configuration.GetValue<string>("HrDetail:Name");
                    string hrEmail = _configuration.GetValue<string>("HrDetail:Email");
                    DateTime dt = DateTime.Now;
                    String date = dt.ToShortDateString();
                    if (!model.CaseId.HasValue)
                    {
                        string message = RecruitmentTemplates.ContactUsEmail(model.Name, hrName, model.Email, model.ContactNumber, model.Message, date);
                        (bool success, string errorMsg) response1 = await _emailer.SendEmailAsync(model.Name, hrEmail, "Contact Us User Detail ", message, null);

                        return Ok(response1.success);
                    }
                    else
                    {
                       
                        var caseStudyName = (CaseStudy)model.CaseId;
                        string message = RecruitmentTemplates.CaseStudyEmail(model.Name, hrName, caseStudyName.ToString(), model.Email, model.ContactNumber, model.Message, date);
                        (bool success, string errorMsg) response1 = await _emailer.SendEmailAsync(model.Name, hrEmail, "Case Study Download Detail ", message, null);

                        return Ok(response1.success);
                    }
                }
                return BadRequest(ModelState);
            }

            catch (Exception ex)
            {
                return BadRequest(ex.GetBaseException().Message);
            }
        }




    }
}