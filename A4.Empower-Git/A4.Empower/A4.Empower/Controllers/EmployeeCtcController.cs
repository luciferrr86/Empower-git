using A4.BAL.DTO;
using A4.BAL.Maintenance;
using A4.DAL.Entites;
using A4.DAL.Entites.Leave;
using A4.DAL.Entites.Maintenance;
using AutoMapper;
using DAL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;

namespace A4.Empower.Controllers
{
    [Authorize(AuthenticationSchemes = OpenIddict.Validation.AspNetCore.OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)]
    [Produces("application/json")]
    [Route("api/EmployeeCtc")]
    public class EmployeeCtcController : Controller
    {
        readonly ILogger _logger;
        readonly IUnitOfWork _unitOfWork;
        private IWebHostEnvironment _hostingEnvironment;


        public EmployeeCtcController(ILogger<TitleController> logger, IUnitOfWork unitOfWork, IWebHostEnvironment hostingEnvironment)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpGet("ctc/{id}")]
        public IActionResult GetEmpCtc(Guid id)
        {
            Expression<Func<Employee, bool>> empFilter = e => e.UserId == id.ToString();
            var emp = _unitOfWork.Employee.Get(empFilter).FirstOrDefault();
            Expression<Func<EmployeeCtc, bool>> empCTCFilter = e => e.EmployeeId == emp.Id;
            var employeeCtc = _unitOfWork.EmployeeCTC.Get(empCTCFilter, null, "Employee,CtcOtherComponent").FirstOrDefault();

            if (employeeCtc == null)
            {
                employeeCtc = new EmployeeCtc();
                var empId = emp.Id;
                employeeCtc.EmployeeId = empId;
            }
            Expression<Func<SalaryComponent, bool>> salComponnet = e => e.IsActive;
            var componentList = _unitOfWork.SalaryComponent.Get(salComponnet).ToList();
            if (employeeCtc.CtcOtherComponent == null)
            {
                employeeCtc.CtcOtherComponent = new List<CtcOtherComponent>();

            }
            foreach (var component in componentList)
            {
                if (!employeeCtc.CtcOtherComponent.Any(q => q.SalaryComponentId == component.Id))
                {
                    employeeCtc.CtcOtherComponent.Add(new CtcOtherComponent() { SalaryComponentId = component.Id });
                }
            }
            return Ok(employeeCtc);
        }

        [HttpPost("managectc")]
        public IActionResult ManageCTC([FromBody]EmployeeCtc employeeCtc)
        {
            try
            {
                if (employeeCtc.Id > 0)
                {
                    
                    Expression<Func<EmployeeCtc, bool>> empCTCFilter = e => e.Id == employeeCtc.Id;
                    var employeeCtcDb = _unitOfWork.EmployeeCTC.Get(empCTCFilter, null, "").FirstOrDefault();
                    employeeCtc.UpdatedDate = DateTime.Now;
                    _unitOfWork.EmployeeCTC.Update(employeeCtc);
                    foreach (var comp in employeeCtc.CtcOtherComponent)
                    {
                        if (comp.Id == 0)
                        {

                            comp.EmployeeCtcId = employeeCtc.Id;
                            comp.CreatedDate = DateTime.Now;
                            comp.UpdatedDate = DateTime.Now;
                            _unitOfWork.CtcOtherComponent.Add(comp);
                        }
                        else
                        {
                            comp.UpdatedDate = DateTime.Now;
                            _unitOfWork.CtcOtherComponent.Update(comp);
                        }

                    }

                    _unitOfWork.SaveChanges();
                    return Ok("Data Updated");
                }

                else if (employeeCtc.Id == 0)
                {
                    employeeCtc.CreatedDate = DateTime.Now;
                    employeeCtc.UpdatedDate = DateTime.Now;
                    _unitOfWork.EmployeeCTC.Add(employeeCtc);
                    _unitOfWork.SaveChanges();
                    return Ok("Data Created");
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