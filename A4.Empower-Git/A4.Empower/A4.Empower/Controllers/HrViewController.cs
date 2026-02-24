using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using A4.BAL;
using A4.Empower.Helpers;
using DAL;
using Microsoft.AspNetCore.Mvc;

namespace A4.Empower.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class HrViewController : Controller
    {
        readonly IUnitOfWork _unitOfWork;
        readonly IEmailer _emailer;
        public HrViewController(IUnitOfWork unitOfWork, IEmailer emailer)
        {
            _unitOfWork = unitOfWork;
            _emailer = emailer;
        }

        [HttpGet("hrViewPreCheck")]
        public IActionResult HrViewPreCheck()
        {
            var hrViewModel = new HrViewModel();
            var yearId = _unitOfWork.PerformanceConfig.GetPerformanceYear();
            hrViewModel.IsPerformanceStart = _unitOfWork.PerformanceApp.CheckPerformanceStart();
            var config = _unitOfWork.PerformanceConfig.GetAll().FirstOrDefault();
            if (config!=null)
            {
                hrViewModel.IsConfigurationSet = true;
            }
            hrViewModel.IsMidYearEnabled = _unitOfWork.PerformanceApp.GetFeatures().IsMidYearEnabled;
            hrViewModel.IsMidYearReviewCompleted = _unitOfWork.PerformanceApp.CheckIsMidYearReviewCompleted(yearId);
            return Ok(hrViewModel);
        }

        [HttpGet("getAllManager/{page:int}/{pageSize:int}/{name?}")]
        public IActionResult Get(int page, int pageSize, string name = null)
        {
            var hrViewModel = new HrViewModel();
            var mgrList = new List<HrViewList>();
            hrViewModel.IsPerformanceStart = _unitOfWork.PerformanceApp.CheckPerformanceStart();
            var yearId = _unitOfWork.PerformanceConfig.GetPerformanceYear();
            var lstMgr = _unitOfWork.Employee.GetAllManager(yearId, name, page, pageSize);
            if (lstMgr.Count > 0)
            {
                foreach (var item in lstMgr)
                {
                    mgrList.Add(new HrViewList
                    {
                        Id=item.Employee.ApplicationUser.Id,
                        Name = item.Employee.ApplicationUser.FullName,
                        Designation = item.Employee.FunctionalDesignation.Name,
                        Group = item.Employee.FunctionalGroup.Name,
                        Status = item.PerformanceStatus.StatusText
                    });
                }
            }
            hrViewModel.lstManager = mgrList;
            hrViewModel.totalCount = lstMgr.Count();
            return Ok(hrViewModel);
        }

        [HttpGet("getAllEmployee/{page:int}/{pageSize:int}/{name?}")]
        public IActionResult GetEmployees(int page, int pageSize, string name = null)
        {
            var hrViewModel = new HrViewModel();
            var empList = new List<HrViewList>();
            var yearId = _unitOfWork.PerformanceConfig.GetPerformanceYear();
            var lstEmp = _unitOfWork.Employee.GetAllEmployee(yearId, name, page, pageSize);
            if (lstEmp.Count > 0)
            {
                foreach (var item in lstEmp)
                {
                    empList.Add(new HrViewList
                    {
                        Id=item.Employee.ApplicationUser.Id,
                        Name = item.Employee.ApplicationUser.FullName,
                        Designation = item.Employee.FunctionalDesignation.Name,
                        Group = item.Employee.FunctionalGroup.Name,
                        Status = item.PerformanceStatus.StatusText
                    });
                }
            }
            hrViewModel.lstManager = empList;
            hrViewModel.totalCount = lstEmp.Count();
            return Ok(hrViewModel);
        }

        [HttpGet("performanceInvitation")]
        public async Task<IActionResult> PerformanceInvitation()
        {
            try
            {
                var invitation = _unitOfWork.PerformanceYear.PerformanceInvitation();
                if(invitation.Count > 0)
                {
                    foreach (var item in invitation)
                    {
                        string message = PerformanceTemplates.Invitation(item.MailID, item.Name);
                        (bool success, string errorMsg) response = await _emailer.SendEmailAsync(item.Name, item.MailID, "Set goals for direct reportees", message);
                    }
                    return NoContent();
                }
                return BadRequest("employee count cannotbe null");
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex);
                throw ex;
            }
          
        }

        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        [HttpGet("managerInvitation/{id}")]
        public async Task<ActionResult> SendReminderManagerInvitation(string id)
        {
            try
            {
                if(id != null)
                {
                    var employee = _unitOfWork.Employee.GetEmployeeDetail(id);
                    if (employee != null)
                    {
                        string message = PerformanceTemplates.Invitation(employee.ApplicationUser.Email, employee.ApplicationUser.FullName);
                        (bool success, string errorMsg) response = await _emailer.SendEmailAsync(employee.ApplicationUser.FullName, employee.ApplicationUser.Email, "Reminder to release goal", message);
                    }
                    return NoContent();
                }
                return BadRequest("id cannot be null");
               
            }
            catch (Exception ex)
            {
                return BadRequest(ex.GetBaseException().Message);
            }
        }

        [HttpGet("employeeInvitation/{id}")]
        public async Task<ActionResult> SendReminderEmployeeInvitation(string id)
        {
            try
            {
                if (id != null)
                {
                    var employee = _unitOfWork.Employee.GetEmployeeDetail(id);
                    if (employee != null)
                    {
                        string message = PerformanceTemplates.Invitation(employee.ApplicationUser.Email, employee.ApplicationUser.FullName);
                        (bool success, string errorMsg) response = await _emailer.SendEmailAsync(employee.ApplicationUser.FullName, employee.ApplicationUser.Email, "Reminder to enter accomplishments/development goals", message);
                    }
                    return NoContent();
                }
                return BadRequest("id cannot be null");

            }
            catch (Exception ex)
            {
                return BadRequest(ex.GetBaseException().Message);
            }
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
