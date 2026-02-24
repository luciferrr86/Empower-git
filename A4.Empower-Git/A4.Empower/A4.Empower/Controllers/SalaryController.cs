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
    [Route("api/Salary")]
    public class SalaryController : Controller
    {
        readonly ILogger _logger;
        readonly IUnitOfWork _unitOfWork;
        private IWebHostEnvironment _hostingEnvironment;
        private readonly IMapper _mapper;


        public SalaryController(ILogger<TitleController> logger, IUnitOfWork unitOfWork, IWebHostEnvironment hostingEnvironment, IMapper mapper)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _hostingEnvironment = hostingEnvironment;
            _mapper = mapper;
        }




        [HttpGet("salaryDetail/{id}")]
        public IActionResult GetSalaryDetail(Guid id)
        {
            Expression<Func<EmployeeSalary, bool>> empFilter = e => e.EmployeeId == id;
            var empSalDetail = _unitOfWork.EmployeeSalary.Get().FirstOrDefault();
            return Ok(empSalDetail);
        }

        [HttpGet("{id}")]
        public IActionResult GetSalaryEmployee(Guid id, int month = 0, int year = 0)
        {
            month = month == 0 ? DateTime.Now.Month - 1 : month;
            year = year == 0 ? DateTime.Now.Year : year;

            var date = DateTime.Today.AddMonths(-1);
            if (month == 0)
            {
                month = date.Month;
            }
            if (year == 0)
            {
                year = date.Year;
            }
            var empSalVM = GetEmployeeSalary(id, month, year);

            return Ok(empSalVM);
        }

        [HttpPost("salary")]
        public IActionResult ManageSalary([FromBody] EmployeeSalary employeeSalary)
        {
            try
            {
                if (employeeSalary.Id > 0)
                {
                    //  var employeeSalary = _mapper.Map<EmployeeSalaryViewModel, EmployeeSalary>(empSalVM);
                    employeeSalary.UpdatedDate = DateTime.Now;
                    _unitOfWork.EmployeeSalary.Update(employeeSalary);
                    foreach (var comp in employeeSalary.SalaryPart)
                    {
                        if (comp.Id == 0)
                        {

                            comp.EmployeeSalaryId = employeeSalary.Id;
                            comp.CreatedDate = DateTime.Now;
                            comp.UpdatedDate = DateTime.Now;
                            _unitOfWork.SalaryPart.Add(comp);
                        }
                        else
                        {
                            comp.UpdatedDate = DateTime.Now;
                            _unitOfWork.SalaryPart.Update(comp);
                        }

                    }

                    _unitOfWork.SaveChanges();
                    return Ok("Data Updated");

                }
                else if (employeeSalary.Id == 0)
                {
                    employeeSalary.CreatedDate = DateTime.Now;
                    employeeSalary.UpdatedDate = DateTime.Now;
                    _unitOfWork.EmployeeSalary.Add(employeeSalary);
                    _unitOfWork.SaveChanges();
                    return Ok("Salary Added");
                }
                return BadRequest(ModelState);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.GetBaseException().Message);
            }
        }

        [HttpPost("saveAllEmpSalDetail")]
        public IActionResult ManageAllSalaryEmployee([FromBody] EmployeeSalaryViewModel empSalViewModel)
        {
            var month = empSalViewModel.Month == 0 ? DateTime.Now.Month : empSalViewModel.Month;
            var year = empSalViewModel.Year == 0 ? DateTime.Now.Year : empSalViewModel.Year;

            var employees = _unitOfWork.Employee.GetAll().ToList();
            foreach (var emp in employees)
            {
                var empSalVM = SaveAllEmployeeSalary(Guid.Parse(emp.UserId), month, year);
            }

            return Ok("Salary Processed");
        }

        private EmployeeSalaryDTO SaveAllEmployeeSalary(Guid id, int month, int year)
        {

            Expression<Func<Employee, bool>> empFilter = e => e.UserId == id.ToString();
            var employee = _unitOfWork.Employee.Get(empFilter, null, "ApplicationUser").FirstOrDefault();
            Expression<Func<EmployeeCtc, bool>> empCTCFilter = e => e.EmployeeId == employee.Id;
            var employeeCtc = _unitOfWork.EmployeeCTC.Get(empCTCFilter, null, "CtcOtherComponent").FirstOrDefault();
            var empSal = _unitOfWork.EmployeeSalary.Get(q => q.EmployeeId == employee.Id && q.Month == month && q.Year == year, null, "SalaryPart").FirstOrDefault();
            EmployeeSalaryDTO employeeSalaryDTO = new EmployeeSalaryDTO();
            employeeSalaryDTO.SalaryComponents = _unitOfWork.SalaryComponent.Get().ToList();
            employeeSalaryDTO.EmployeeCtc = employeeCtc;
            int totalDaysInMonth = DateTime.DaysInMonth(year, month);
            var leavesPerYear = _unitOfWork.LeaveRules.Get(q => q.BandId == employee.BandId && q.IsActive == true).ToList();
            var totalLeavesPerYear = leavesPerYear.Select(x => x.LeavesPerYear).ToList().Sum(x => Convert.ToInt32(x));
            var monthlyAllowedLeaves = totalLeavesPerYear / 12;
            int startDay = 1;
            var leaveTaken = GetLeaves(employee.Id, month, year, startDay);
            List<int> totalLeaves = new List<int>();
            var empLeaveTaken = _unitOfWork.EmployeeSalary.Get(q => q.EmployeeId == employee.Id).ToList();
            var totalLeavesTakenTillDate = empLeaveTaken.Select(t => t.LeaveTaken).ToList().Sum(t => Convert.ToInt32(t));
            var totalUnpaidDays = empLeaveTaken.Select(t => t.UnpaidDays).ToList().Sum(t => Convert.ToInt32(t));
            int unpaidDays = 0;
            decimal salDeductionAmount = 0;
            decimal unpaid = 0;
            decimal perDaySalary = 0;
            var allowedLeavesTillDate = totalLeavesTakenTillDate - totalUnpaidDays;
            var leavesAllowedTillDate = monthlyAllowedLeaves * month;
            if (employeeCtc != null && empSal == null)
            {
                perDaySalary = GetPerDaySalary(month, year, employeeCtc.BasicSalary);
                employeeSalaryDTO.EmployeeSalary = new EmployeeSalary();
                employeeSalaryDTO.EmployeeSalary.BasicSalary = employeeCtc.BasicSalary;
                employeeSalaryDTO.EmployeeSalary.DA = employeeCtc.DA;
                employeeSalaryDTO.EmployeeSalary.HRA = employeeCtc.HRA;
                employeeSalaryDTO.EmployeeSalary.Conveyance = employeeCtc.Conveyance;
                employeeSalaryDTO.EmployeeSalary.MedicalExpenses = employeeCtc.MedicalExpenses;
                employeeSalaryDTO.EmployeeSalary.Special = employeeCtc.Special;
                employeeSalaryDTO.EmployeeSalary.Bonus = employeeCtc.Bonus;
                employeeSalaryDTO.EmployeeSalary.EmployeeCtcId = employeeCtc.Id;
                employeeSalaryDTO.EmployeeSalary.Month = month;
                employeeSalaryDTO.EmployeeSalary.Year = year;
                employeeSalaryDTO.EmployeeSalary.TotalDaysOfMonth = totalDaysInMonth;
                employeeSalaryDTO.EmployeeSalary.AllowedLeave = monthlyAllowedLeaves;
                employeeSalaryDTO.EmployeeSalary.LeaveTaken = leaveTaken;

                if (leaveTaken != 0)
                {
                    unpaidDays = leaveTaken - monthlyAllowedLeaves;
                    if (unpaidDays > 0)
                    {
                        salDeductionAmount = perDaySalary * unpaidDays;
                        unpaid = Convert.ToDecimal(String.Format("{0:0.00}", salDeductionAmount));

                        unpaidDays = Convert.ToInt32(leaveTaken) - monthlyAllowedLeaves;
                        if (unpaidDays > 0)
                        {
                            unpaid = perDaySalary * unpaidDays;
                        }


                    }
                }

                employeeSalaryDTO.EmployeeSalary.UnpaidDays = unpaid;
                employeeSalaryDTO.EmployeeSalary.EmployeeId = employee.Id;
                employeeSalaryDTO.EmployeeSalary.CreatedDate = DateTime.Now;
                employeeSalaryDTO.EmployeeSalary.UpdatedDate = DateTime.Now;
                _unitOfWork.EmployeeSalary.Add(employeeSalaryDTO.EmployeeSalary);
                _unitOfWork.SaveChanges();

            }
            if (employeeCtc != null && empSal != null)
            {
                perDaySalary = GetPerDaySalary(month, year, employeeCtc.BasicSalary);
               // employeeSalaryDTO.EmployeeSalary = new EmployeeSalary();
                empSal.BasicSalary = employeeCtc.BasicSalary;
                empSal.DA = employeeCtc.DA;
                empSal.HRA = employeeCtc.HRA;
                empSal.Conveyance = employeeCtc.Conveyance;
                empSal.MedicalExpenses = employeeCtc.MedicalExpenses;
                empSal.Special = employeeCtc.Special;
                empSal.Bonus = employeeCtc.Bonus;
                empSal.EmployeeCtcId = employeeCtc.Id;
                empSal.Month = month;
                empSal.Year = year;
                empSal.TotalDaysOfMonth = totalDaysInMonth;
                empSal.AllowedLeave = monthlyAllowedLeaves;
                empSal.LeaveTaken = leaveTaken;

                if (leaveTaken != 0)
                {
                    unpaidDays = leaveTaken - monthlyAllowedLeaves;
                    if (unpaidDays > 0)
                    {
                        salDeductionAmount = perDaySalary * unpaidDays;
                        unpaid = Convert.ToDecimal(String.Format("{0:0.00}", salDeductionAmount));

                        unpaidDays = Convert.ToInt32(leaveTaken) - monthlyAllowedLeaves;
                        if (unpaidDays > 0)
                        {
                            unpaid = perDaySalary * unpaidDays;
                        }


                    }
                }

                empSal.UnpaidDays = unpaid;
                empSal.EmployeeId = employee.Id;
                empSal.UpdatedDate = DateTime.Now;
                _unitOfWork.EmployeeSalary.Update(empSal);
                _unitOfWork.SaveChanges();

            }
            else
            {
                employeeSalaryDTO.EmployeeSalary = null;
            }

            return employeeSalaryDTO;
        }

        [HttpGet("empSalList")]
        public IActionResult GetEmpSal(int month, int year)
        {
            Expression<Func<EmployeeSalary, bool>> empSalaryFilter = e => true && e.Month == month && e.Year == year;
            var empSalList = _unitOfWork.EmployeeSalary.Get(empSalaryFilter, null, "Employee.ApplicationUser,EmployeeCtc").OrderBy(q => q.Id).ToList();

            var monthlySalaries = new List<CheckSalaryViewModel>();
            foreach (var empSal in empSalList)
            {
                var salaryViewModel = new CheckSalaryViewModel();
                salaryViewModel.EmployeeName = empSal.Employee.ApplicationUser.FullName;
                salaryViewModel.Id = empSal.Id;
                salaryViewModel.EmployeeId = empSal.EmployeeId;
                salaryViewModel.UserId = empSal.Employee.UserId;
                salaryViewModel.LeaveTaken = empSal.LeaveTaken;
                salaryViewModel.ProfessionalTaxes = empSal.ProfessionTax;
                salaryViewModel.Tds = empSal.TDS;
                salaryViewModel.Total = empSal.Total;
                salaryViewModel.NetPayable = empSal.NetPayable;
                salaryViewModel.TotalDeductions = (empSal.ContributionToPf + empSal.TDS + empSal.ProfessionTax + empSal.UnpaidDays);
                monthlySalaries.Add(salaryViewModel);
            }

            return Ok(monthlySalaries);
        }

        [HttpGet("getEmpSal")]
        public IActionResult GetEmpSalary(int month, int year, string employeeName, int pageNo, int pageSize, string sortedBy)
        {

            Expression<Func<EmployeeSalary, bool>> empSalaryFilter = e => true && e.Month == month && e.Year == year;
            var empSalList = _unitOfWork.EmployeeSalary.Get(empSalaryFilter, null, "Employee.ApplicationUser,EmployeeCtc").OrderBy(q => q.Id).ToList();
            if (!string.IsNullOrWhiteSpace(employeeName))
            {
                empSalList = empSalList.Where(t => t.Employee.ApplicationUser.FullName.Trim().ToLower() == employeeName.Trim().ToLower()).ToList();
            }
            decimal dPageSize = pageSize;
            var noOfPages = (int)Math.Ceiling(empSalList.Count() / dPageSize);

            if (sortedBy == "default")
            {
                empSalList = empSalList.OrderBy(q => q.Id).ToList();
            }
            int skipRecords = (pageNo - 1) * pageSize;


            empSalList = empSalList.Skip(skipRecords).Take(pageSize).ToList();


            var monthlySalaries = new List<CheckSalaryViewModel>();
            foreach (var empSal in empSalList)
            {
                var salaryViewModel = new CheckSalaryViewModel();
                salaryViewModel.EmployeeName = empSal.Employee.ApplicationUser.FullName;
                salaryViewModel.Id = empSal.Id;
                salaryViewModel.EmployeeId = empSal.EmployeeId;
                salaryViewModel.UserId = empSal.Employee.UserId;
                salaryViewModel.LeaveTaken = empSal.LeaveTaken;
                salaryViewModel.ProfessionalTaxes = empSal.ProfessionTax;
                salaryViewModel.Tds = empSal.TDS;
                salaryViewModel.Total = empSal.Total;
                salaryViewModel.NetPayable = empSal.NetPayable;
                salaryViewModel.TotalDeductions = (empSal.ContributionToPf + empSal.TDS + empSal.ProfessionTax + empSal.UnpaidDays);
                salaryViewModel.NoOfPages = noOfPages;
                monthlySalaries.Add(salaryViewModel);
            }

            return Ok(monthlySalaries);
        }

        [HttpGet("allEmpSal")]
        public IActionResult GetAllEmpSalary(int month = 0, int year = 0)
        {
            month = month == 0 ? DateTime.Now.Month - 1 : month;
            year = year == 0 ? DateTime.Now.Year : year;

            var date = DateTime.Today.AddMonths(-1);
            if (month == 0)
            {
                month = date.Month;
            }
            if (year == 0)
            {

                year = date.Year;

            }
            Expression<Func<Employee, bool>> empFilter = e => e.IsActive;
            var empList = _unitOfWork.Employee.Get(empFilter, null, "").ToList();

            var employeesList = new List<EmployeeSalaryViewModel>();
            foreach (var emp in empList)
            {
                Expression<Func<EmployeeSalary, bool>> empSalaryFilter = e => e.EmployeeId == emp.Id && e.Month == month && e.Year == year;
                var empSalList = _unitOfWork.EmployeeSalary.Get(empSalaryFilter, null, "Employee.ApplicationUser").FirstOrDefault();
                if (empSalList != null)
                {
                    var viewModel = new EmployeeSalaryViewModel();
                    viewModel.EmployeeName = empSalList.Employee.ApplicationUser.FullName;
                    viewModel.EmployeeId = empSalList.Employee.Id;
                    viewModel.EmployeeCode = empSalList.Employee.EmployeeCode;
                    viewModel.AllowedLeave = empSalList.AllowedLeave;
                    viewModel.LeaveTaken = empSalList.LeaveTaken;
                    viewModel.TDS = empSalList.TDS;
                    viewModel.Total = empSalList.Total;
                    viewModel.NetPayable = empSalList.NetPayable;
                    employeesList.Add(viewModel);
                }


            }
            return Ok(employeesList);
        }

        [HttpPost("updateEmpSal")]
        public IActionResult UpdateEmployeeSalary(Guid? empid, int? allowedLeaves, int? tds)
        {
            Expression<Func<EmployeeSalary, bool>> empFilter = e => e.EmployeeId == empid;
            var employeeSal = _unitOfWork.EmployeeSalary.Get(empFilter, null, "").FirstOrDefault();
            if (employeeSal == null)
            {
                employeeSal = new EmployeeSalary();
                employeeSal.CreatedDate = DateTime.Now;
                //  employee.CreatedBy = 1;
            }
            employeeSal.AllowedLeave = allowedLeaves.Value;
            employeeSal.TDS = tds.Value;
            employeeSal.EmployeeId = empid.Value;
            employeeSal.UpdatedDate = DateTime.Now;
            try
            {

                if (employeeSal.Id != 0)
                {
                    _unitOfWork.EmployeeSalary.Update(employeeSal);
                    _unitOfWork.SaveChanges();
                    return Ok("Employee Salary Updated");
                }
                else if (empid == null)
                {
                    _unitOfWork.EmployeeSalary.Add(employeeSal);
                    _unitOfWork.SaveChanges();
                    return Ok("Employee salary Added");
                }
                return BadRequest(ModelState);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.GetBaseException().Message);
            }
        }
        [HttpGet("empListForLeave")]
        public IActionResult GetEmpListForLeaveEntry()
        {
            Expression<Func<Employee, bool>> empFilter = e => true;
            var empList = _unitOfWork.Employee.Get(empFilter, null, "ApplicationUser").OrderBy(q => q.Id).ToList();
            var employeesList = new List<CheckSalaryViewModel>();
            foreach (var empSal in empList)
            {
                var viewModel = new CheckSalaryViewModel();
                viewModel.EmployeeName = empSal.ApplicationUser.FullName;
                viewModel.EmployeeId = empSal.Id;
                viewModel.EmployeeCode = empSal.EmployeeCode;
                viewModel.UserId = empSal.UserId;
                employeesList.Add(viewModel);
            }
            return Ok(employeesList);
        }

        [HttpGet("empAtt/{id}")]
        public IActionResult GetEmpById(Guid id)
        {
            Expression<Func<Employee, bool>> empFilter = e => e.UserId == id.ToString();
            var emp = _unitOfWork.Employee.Get(empFilter, null, "ApplicationUser").FirstOrDefault();
            var employee = new List<CheckSalaryViewModel>();
            var viewModel = new CheckSalaryViewModel();
            viewModel.EmployeeName = emp.ApplicationUser.FullName;
            viewModel.EmployeeId = emp.Id;
            viewModel.EmployeeCode = emp.EmployeeCode;
            viewModel.UserId = emp.UserId;
            return Ok(viewModel);
        }

        [HttpGet("viewSalary/{id}")]
        public IActionResult GetEmpSalaryDetail(int id)
        {
            Expression<Func<EmployeeSalary, bool>> empFilter = e => e.Id == id;
            var empSalDetail = _unitOfWork.EmployeeSalary.Get(empFilter, null, "SalaryPart.CtcOtherComponent.SalaryComponent").FirstOrDefault();
            var empId = empSalDetail.EmployeeId;
            Expression<Func<Employee, bool>> empDetailFilter = e => e.Id == empId;
            var empDetail = _unitOfWork.Employee.Get(empDetailFilter, null, "ApplicationUser,FunctionalDesignation,Personal").FirstOrDefault();
            DateTime date = new DateTime(empSalDetail.Year, empSalDetail.Month, 1);
            var monthName = date.ToString("MMMM");
            var paySlipVM = new PaySlipViewModel();
            var totalEarning = empSalDetail.BasicSalary + empSalDetail.TA + empSalDetail.DA + empSalDetail.HRA + empSalDetail.MedicalExpenses + empSalDetail.Conveyance + empSalDetail.Special + empSalDetail.Bonus;
            var totalDeduction = empSalDetail.ContributionToPf + empSalDetail.TDS + empSalDetail.UnpaidDays + empSalDetail.NetPayable + empSalDetail.ProfessionTax + empSalDetail.SalaryAdvance;  //  + empSalDetail.MedicalBillAmount + unpaidDaysAmount
            paySlipVM.EmployeeId = empId;
            paySlipVM.AllowedLeave = empSalDetail.AllowedLeave;
            paySlipVM.BasicSalary = empSalDetail.BasicSalary;
            paySlipVM.Bonus = empSalDetail.Bonus;
            paySlipVM.ContributionToPf = empSalDetail.ContributionToPf;
            paySlipVM.Conveyance = empSalDetail.Conveyance;
            paySlipVM.Special = empSalDetail.Special;
            paySlipVM.TA = empSalDetail.TA;
            paySlipVM.TDS = empSalDetail.TDS;
            paySlipVM.DA = empSalDetail.DA;
            paySlipVM.HRA = empSalDetail.HRA;
            paySlipVM.LeaveTaken = empSalDetail.LeaveTaken;
            //paySlipVM.MedicalBillAmount = empSalDetail.MedicalBillAmount;
            paySlipVM.MedicalExpenses = empSalDetail.MedicalExpenses;
            paySlipVM.ContributionToPf = empSalDetail.ContributionToPf;
            paySlipVM.Month = monthName;
            paySlipVM.Total = empSalDetail.Total;
            paySlipVM.NetPayable = empSalDetail.NetPayable;
            paySlipVM.TotalDaysOfMonth = empSalDetail.TotalDaysOfMonth;
            paySlipVM.Year = empSalDetail.Year;
            paySlipVM.AllowedLeave = empSalDetail.AllowedLeave;
            paySlipVM.WorkedDays = empSalDetail.WorkedDays;
            paySlipVM.ProfessionTax = empSalDetail.ProfessionTax;
            paySlipVM.SalaryAdvance = empSalDetail.SalaryAdvance;
            paySlipVM.Location = empDetail.Location;
            paySlipVM.DOJ = empDetail.DOJ;
            paySlipVM.EmployeeName = empDetail.ApplicationUser.FullName;
            paySlipVM.EmpDesignation = empDetail.FunctionalDesignation.Name;
            paySlipVM.EmpCode = empDetail.EmpCode;
            paySlipVM.PanNumber = empDetail.Personal.PanNumber;
            paySlipVM.Unpaid = empSalDetail.UnpaidDays;
            paySlipVM.TotalEarning = totalEarning;
            paySlipVM.TotalDeduction = totalDeduction;
            return Ok(paySlipVM);

        }

        [HttpGet("empDetail/{id}")]
        public IActionResult GetEmpDetail(Guid id)
        {
            Expression<Func<Employee, bool>> empFilter = e => e.Id == id;
            var empDetail = _unitOfWork.Employee.Get(empFilter).FirstOrDefault();
            return Ok(empDetail);
        }

        [HttpPost("salComp")]
        public IActionResult ManageSalaryComponent([FromBody] SalaryCompViewModel salaryCompVM)
        {
            try
            {
                if (salaryCompVM.Id > 0 && ModelState.IsValid)
                {
                    var salaryComp = _mapper.Map<SalaryCompViewModel, SalaryComponent>(salaryCompVM);
                    _unitOfWork.SalaryComponent.Update(salaryComp);
                    _unitOfWork.SaveChanges();
                    return Ok("Data Updated");
                }
                else if (salaryCompVM.Id == 0)
                {
                    var salaryComp = _mapper.Map<SalaryCompViewModel, SalaryComponent>(salaryCompVM);
                    _unitOfWork.SalaryComponent.Add(salaryComp);
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

        [HttpGet("getComponentById/{id}")]
        public IActionResult GetComponentById(int id)
        {
            Expression<Func<SalaryComponent, bool>> compFilter = e => e.Id == id && e.IsActive;
            var component = _unitOfWork.SalaryComponent.Get(compFilter).FirstOrDefault();
            return Ok(component);
        }

        [HttpPost("saveFilename")]
        public IActionResult SaveFile(string filename)
        {
            if (filename != null && filename != "")
            {
                string fullPath = Path.Combine(Directory.GetCurrentDirectory(), @"ClientApp\src\assets\pdf", filename);
                //  string path = fullPath;
                FileInfo fi1 = new FileInfo(fullPath);
                using (FileStream fs = fi1.Create()) { }
            }
            return Ok();
        }

        [HttpGet("dateList")]
        public IActionResult GetDateList(int month, int year)
        {
            var dateList = GetDates(month, year);
            var weekList = GetWeekList();
            // var weeks = GetWeeks();
            //var startDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            //var lastDayOfMonth = DateTime.DaysInMonth(startDate.Year, startDate.Month);
            //var endDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, lastDayOfMonth);
            return Ok(dateList);
        }

        public static List<DateTime> GetWeekList()
        {
            DateTime date = DateTime.Today;
            // first generate all dates in the month of 'date'
            var dates = Enumerable.Range(1, DateTime.DaysInMonth(date.Year, date.Month)).Select(n => new DateTime(date.Year, date.Month, n));
            // then filter the only the start of weeks
            var weekends = from d in dates
                           where d.DayOfWeek == DayOfWeek.Monday
                           select d;
            return weekends.ToList();
        }

        public static List<KeyValuePair<string, string>> GetDates(int month, int year)
        {
            int startDay = 1;
            // int endDay = 0;
            var monthStartDate = new DateTime(year, month, startDay);
            DateTime monthEndDay;
            if (startDay == 1)
            {
                monthEndDay = new DateTime(year, month, DateTime.DaysInMonth(year, month));
            }
            else
            {
                monthEndDay = monthStartDate.AddMonths(1).AddDays(-1);
            }
            var date = monthStartDate;
            var dateList = new List<KeyValuePair<string, string>>();
            while (date <= monthEndDay)
            {
                var datePair = new KeyValuePair<string, string>(date.ToShortDateString(), date.DayOfWeek.ToString());
                dateList.Add(datePair);
                date = date.AddDays(1);
            }
            return dateList;
            //for(int i= monthStartDay)
            //return Enumerable.Range(1, DateTime.DaysInMonth(year, month))  // Days: 1, 2 ... 31 etc.
            //                 .Select(day => new DateTime(year, month, day)) // Map each day to a date
            //                 .ToList(); // Load dates into a list
        }

        [HttpPost("upload")]
        public ActionResult UploadFile()
        {

            string webRootPath = _hostingEnvironment.WebRootPath;
            var file = Request.Form.Files[0];
            var empAttendance = new EmployeeAttendence();
            //string pathToExcelFile = Path.Combine(webRootPath, "PendingOrderDetails.XLSX");
            string appendDate = string.Format("{0:dd-MM-yyyy_hh-mm-ss}", DateTime.Now);
            string fileName = "PendingOrders_" + appendDate + ".XLSX";

            // string newPath = Path.Combine(webRootPath, folderName);
            string newPath = webRootPath + "\\Files";
            string fullPath = Path.Combine(newPath, fileName);
            HttpContext.Session.SetString("keyname", fileName);
            try
            {
                if (!Directory.Exists(newPath))
                {
                    Directory.CreateDirectory(newPath);
                }
                if (file.Length > 0)
                {
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                }
            }
            catch
            {

            }
            return Json("Upload Successful.");
        }

        private EmployeeSalaryDTO GetEmployeeSalary(Guid id, int month, int year)
        {
            Expression<Func<Employee, bool>> empFilter = e => e.UserId == id.ToString();
            var employee = _unitOfWork.Employee.Get(empFilter, null, "ApplicationUser").FirstOrDefault();
            Expression<Func<EmployeeCtc, bool>> empCTCFilter = e => e.EmployeeId == employee.Id;
            var employeeCtc = _unitOfWork.EmployeeCTC.Get(empCTCFilter, null, "CtcOtherComponent").FirstOrDefault();
            var empSal = _unitOfWork.EmployeeSalary.Get(q => q.EmployeeId == employee.Id && q.Month == month && q.Year == year, null, "SalaryPart").FirstOrDefault();
            EmployeeSalaryDTO employeeSalaryDTO = new EmployeeSalaryDTO();
            Expression<Func<SalaryComponent, bool>> salCompFilter = e => e.IsActive;
            employeeSalaryDTO.SalaryComponents = _unitOfWork.SalaryComponent.Get(salCompFilter,null,"").ToList();
            employeeSalaryDTO.EmployeeCtc = employeeCtc;

            if (empSal != null)
            {
                employeeSalaryDTO.EmployeeSalary = empSal;
            }
            else
            {
                if (employeeCtc != null)
                {
                    employeeSalaryDTO.EmployeeSalary = new EmployeeSalary();
                    employeeSalaryDTO.EmployeeSalary.BasicSalary = employeeCtc.BasicSalary;
                    employeeSalaryDTO.EmployeeSalary.DA = employeeCtc.DA;
                    employeeSalaryDTO.EmployeeSalary.HRA = employeeCtc.HRA;
                    employeeSalaryDTO.EmployeeSalary.Conveyance = employeeCtc.Conveyance;
                    employeeSalaryDTO.EmployeeSalary.MedicalExpenses = employeeCtc.MedicalExpenses;
                    employeeSalaryDTO.EmployeeSalary.EmployeeCtcId = employeeCtc.Id;
                    employeeSalaryDTO.EmployeeSalary.Month = month;
                    employeeSalaryDTO.EmployeeSalary.Year = year;
                }
                else
                {
                    employeeSalaryDTO.EmployeeSalary = null;
                }
            }

            if (employeeSalaryDTO.EmployeeSalary != null)
            {
                if (employeeSalaryDTO.EmployeeSalary.SalaryPart == null)
                {
                    employeeSalaryDTO.EmployeeSalary.SalaryPart = new List<SalaryPart>();

                }
                foreach (var component in employeeCtc.CtcOtherComponent)
                {
                    if (!employeeSalaryDTO.EmployeeSalary.SalaryPart.Any(q => q.CtcOtherComponentId == component.Id))
                    {
                        employeeSalaryDTO.EmployeeSalary.SalaryPart.Add(new SalaryPart() { CtcOtherComponentId = component.Id });
                    }
                }
            }
            if (employee != null)
            {
                employeeSalaryDTO.EmployeeName = employee.ApplicationUser.FullName;
                employeeSalaryDTO.EmployeeCode = employee.EmployeeCode;
                employeeSalaryDTO.EmployeeId = employee.Id;
            }
            return employeeSalaryDTO;
        }

        [HttpGet("getSalComponentList")]
        public IActionResult GetSalaryComponentList()
        {
            Expression<Func<SalaryComponent, bool>> salComponnet = e => e.IsActive;
            var componentList = _unitOfWork.SalaryComponent.Get(salComponnet, null,"").ToList();
            return Ok(componentList);
        }

        private int GetLeaves(Guid empid, int month, int year, int startDay)
        {
            var monthStartDate = new DateTime(year, month, startDay);
            DateTime monthEndDay;
            int excludeHolidayCount = 0;
            int weekendDaysCount = 0;
            int noOfDays = 0;
            //  int totalNoOfDays = 0;
            List<int> totalNoOfDays = new List<int>();
            if (startDay == 1)
            {
                monthEndDay = new DateTime(year, month, DateTime.DaysInMonth(year, month));
            }
            else
            {
                monthEndDay = monthStartDate.AddMonths(1).AddDays(-1);
            }
            var leaveDates = new List<DateTime>();
            Expression<Func<EmployeeLeaveDetail, bool>> empLeaveFilter = e => e.EmployeeId == empid && e.LeaveStartDate < monthEndDay && e.LeaveEndDate > monthStartDate;
            var leaveDetails = _unitOfWork.EmployeeLeaveDetail.Get(empLeaveFilter, null, "").ToList();
            foreach (var leaveDetail in leaveDetails)
            {
                var startDate = leaveDetail.LeaveStartDate;
                var endDate = leaveDetail.LeaveEndDate;

                while (startDate <= endDate)
                {
                    if (startDate >= monthStartDate && startDate <= monthEndDay)
                    {
                        leaveDates.Add(startDate);
                        excludeHolidayCount = _unitOfWork.LeaveManagement.ExcludeHolidays(startDate, endDate);
                        weekendDaysCount = _unitOfWork.LeaveManagement.ExcludeWeekends(startDate, endDate);
                        noOfDays = endDate.Subtract(startDate).Days + 1 - excludeHolidayCount - weekendDaysCount;
                        totalNoOfDays.Add(noOfDays);
                    }
                    startDate = startDate.AddDays(1);
                }
            }
            return totalNoOfDays.Count();
        }

        private decimal GetPerDaySalary(int month, int year, decimal basicSalary)
        {
            int daysInMonth = 0;
            int days = DateTime.DaysInMonth(year, month);
            for (int i = 1; i <= days; i++)
            {
                DateTime day = new DateTime(year, month, i);
                if (day.DayOfWeek != DayOfWeek.Sunday && day.DayOfWeek != DayOfWeek.Saturday)
                {
                    daysInMonth++;
                }

            }
            var perdaySalary = basicSalary / daysInMonth;
            return perdaySalary;
        }
        private void SetCTCPart(EmployeeSalaryViewModel empSal, EmployeeCtc empCtc, Employee employee)
        {
            //  var lstleaveEntitleforMonth = _unitOfWork.LeaveManagement.GetAllLeaveForMonth(employee.Id, DateTime.Today.Month, DateTime.Today.Year);
            // empSal.Ctc = empCtc.CTC / 12;
            empSal.BasicSalary = empCtc.BasicSalary;
            empSal.DA = empCtc.DA;
            empSal.HRA = empCtc.HRA;
            empSal.Conveyance = empCtc.Conveyance;
            empSal.MedicalExpenses = empCtc.MedicalExpenses;
            empSal.Special = empCtc.Special;
            empSal.Bonus = empCtc.Bonus;
            empSal.Total = empCtc.Total;
        }



    }
}