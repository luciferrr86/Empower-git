using A4.BAL;
using A4.BAL.Leave;
using A4.BAL.Maintenance;
using A4.DAL.Entites;
using A4.DAL.Entites.Leave;
using A4.DAL.Entites.Maintenance;
using AutoMapper;
using DAL;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http.Headers;

namespace A4.Empower.Controllers
{
    //[Authorize(AuthenticationSchemes = OpenIddict.Validation.AspNetCore.OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)]
    [Produces("application/json")]
    [Route("api/ExcelUpload")]

    public class ExcelUploadController : Controller
    {
        readonly ILogger _logger;
        readonly IUnitOfWork _unitOfWork;
        private IWebHostEnvironment _webHostingEnvironment;
        private readonly IMapper _mapper;

        public ExcelUploadController(ILogger<ExcelUploadController> logger, IUnitOfWork unitOfWork, IWebHostEnvironment webHostingEnvironment, IMapper mapper)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _webHostingEnvironment = webHostingEnvironment;
            _mapper = mapper;
        }

        [HttpPost, DisableRequestSizeLimit]
        public IActionResult Upload()
        {
            try
            {
                var file = Request.Form.Files[0];
                var excelType = Request.Form["type"].ToString();
                var folderName = "AttendanceExcel";
                string fullPath = string.Empty;
                string dbPath = string.Empty;
                string rootFolder = _webHostingEnvironment.WebRootPath;
                fullPath = Path.Combine(rootFolder, folderName);

                if (!Directory.Exists(fullPath))
                {
                    Directory.CreateDirectory(fullPath);
                }
                if (file.Length > 0)
                {
                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    string appendDate = string.Format("{0:dd-MM-yyyy_hh-mm-ss}", DateTime.Now);
                    string appfileName = excelType + appendDate + ".XLSX";
                    //var fullPath = Path.Combine(pathToSave, appfileName);

                    if (fileName != null && fileName != "")
                    {
                        dbPath = Path.Combine(fullPath, appfileName);
                        using (var stream = new FileStream(dbPath, FileMode.Create))
                        {
                            file.CopyTo(stream);
                        }
                    }

                    if (excelType == "detail")
                    {
                        ReadExcelDetail(dbPath);
                    }
                    if (excelType == "summary")
                    {
                        ReadExcelSummary(dbPath);
                    }

                    return Ok(new { dbPath });
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }
        private void ReadExcelDetail(string fullPath)
        {
            var empAtt = new List<EmployeeAttendence>();
            try
            {
                using (SpreadsheetDocument doc = SpreadsheetDocument.Open(fullPath, false))
                {
                    var startDay = 1;
                    WorkbookPart wbPart = doc.WorkbookPart;

                    //statement to get the count of the worksheet  
                    int worksheetcount = doc.WorkbookPart.Workbook.Sheets.Count();

                    //statement to get the sheet object  
                    //Sheet mysheet = (Sheet)doc.WorkbookPart.Workbook.Sheets.ChildElements.GetItem(0);
                    Sheet mysheet = doc.WorkbookPart.Workbook.Sheets.Elements<Sheet>().First();

                    //statement to get the worksheet object by using the sheet id  
                    Worksheet Worksheet = ((WorksheetPart)wbPart.GetPartById(mysheet.Id)).Worksheet;

                    //Note: worksheet has 8 children and the first child[1] = sheetviewdimension,....child[4]=sheetdata  
                    int wkschildno = 4;
                    var stringTable = wbPart.GetPartsOfType<SharedStringTablePart>().FirstOrDefault();

                    //statement to get the sheetdata which contains the rows and cell in table  
                    //SheetData sheetData = (SheetData)Worksheet.ChildElements.GetItem(wkschildno);
                    SheetData sheetData = Worksheet.Elements<SheetData>().ElementAt(wkschildno);
                    int j = 0;
                    var holidays = _unitOfWork.LeaveManagement.GetHolidayList().Select(q => q.Holidaydate.ToShortDateString()).ToList();
                    var secondRow = sheetData.Elements<Row>().ElementAt(1);
                    Guid empId = Guid.Empty;
                    int month = 0;
                    int year = 0;
                    int code = 0;
                    if (secondRow != null)
                    {
                        code = Convert.ToInt32(GetCellValue(secondRow, 0, stringTable));
                    }

                    try
                    {
                        foreach (Row row in sheetData.Elements<Row>())
                        {

                            if (j++ == 0)
                                continue;

                            var empAttendance = new EmployeeAttendence();

                            Expression<Func<Employee, bool>> empSalFilter = e => e.EmployeeCode == code;
                            empId = _unitOfWork.Employee.Get(empSalFilter, null, "").Select(e => e.Id).FirstOrDefault();


                            if (empId != null)
                            {

                                string date = GetCellValue(row, 1, stringTable);
                                
                                var monthDate = DateTime.Parse(DateTime.FromOADate(double.Parse(date)).ToString("MM/dd/yyyy"));
                                
                                Expression<Func<EmployeeAttendence, bool>> attDetailFilter = e => e.EmployeeId == empId && e.Date == monthDate;
                                var empAttDetail = _unitOfWork.EmployeeAttendence.Get(attDetailFilter, null, "").FirstOrDefault();

                                if (empAttDetail == null)
                                {

                                    empAttendance.EmployeeId = empId;
                                    if (monthDate != null)
                                    {
                                        empAttendance.Date = monthDate;
                                        month = empAttendance.Date.Value.Month;
                                        year = empAttendance.Date.Value.Year;

                                        string timein = Convert.ToString(GetCellValue(row, 2, stringTable));
                                        if (!string.IsNullOrWhiteSpace(timein))
                                        {
                                            double.TryParse(timein, out double doubleTimeIn);
                                            empAttendance.PunchIn = DateTime.FromOADate(doubleTimeIn).TimeOfDay.ToString();
                                        }

                                        string timeout = Convert.ToString(GetCellValue(row, 3, stringTable));
                                        if (!string.IsNullOrWhiteSpace(timeout))
                                        {
                                            empAttendance.PunchOut = DateTime.FromOADate(double.Parse(timeout)).TimeOfDay.ToString();
                                        }
                                        // List<string> approvedLeaveDates = new List<string>();

                                        //   List<EmployeeLeaveDetail> approvedLeaveDates =  GetLeaves(empId,2,2020,1);
                                        // code to get approved leaves of that month
                                        var leaveStatus = 0;
                                        if (empAttendance.PunchIn != null && empAttendance.PunchOut != null && empAttendance.PunchIn != "00:00:00" && empAttendance.PunchOut != "00:00:00")
                                        {

                                            var duration = DateTime.Parse(empAttendance.PunchOut).Subtract(DateTime.Parse(empAttendance.PunchIn));
                                            if (duration.Hours < 0)
                                            {
                                                duration = DateTime.Parse(empAttendance.PunchOut).AddDays(1).Subtract(DateTime.Parse(empAttendance.PunchIn));
                                                // duration.Hours = duration.Hours + 24;
                                            }
                                            //if (duration.Hours > 9)
                                            //{
                                            //    leaveStatus = (int)LeaveTypes.FullDay;
                                            //}
                                            //else if (duration.Hours > 7)
                                            //{
                                            //    leaveStatus = (int)LeaveTypes.ShortLeave;
                                            //}
                                            //else if (duration.Hours > 4)
                                            //{
                                            //    leaveStatus = (int)LeaveTypes.HalfDay;
                                            //}
                                            if (duration.Hours >= 9)
                                            {
                                                leaveStatus = (int)LeaveTypes.FullDay;
                                            }
                                            else if (duration.Hours >= 5 && duration.Hours < 9)
                                            {
                                                leaveStatus = (int)LeaveTypes.ShortLeave;
                                            }
                                            else if (duration.Hours >= 4 && duration.Hours < 5)
                                            {
                                                leaveStatus = (int)LeaveTypes.HalfDay;
                                            }
                                            else
                                            {
                                                leaveStatus = (int)LeaveTypes.UnpaidLeave;
                                            }
                                        }
                                        else
                                        {
                                            // Create seperate Date check function
                                            leaveStatus = GetIsHoliday(empAttendance.Date.Value, holidays);
                                            if (leaveStatus == 0)
                                            {
                                                var approvedLeaveDates = GetLeaves(empId, month, year, startDay);
                                                var isApprovedLeave = approvedLeaveDates.Where(q => q == empAttendance.Date).Count() > 0;
                                                if (isApprovedLeave)
                                                {
                                                    leaveStatus = (int)LeaveTypes.ApprovedLeave;
                                                }
                                            }
                                        }
                                        empAttendance.LeaveType = leaveStatus;
                                        empAttendance.CreatedDate = DateTime.Now;
                                        empAttendance.UpdatedDate = DateTime.Now;
                                        _unitOfWork.EmployeeAttendence.Add(empAttendance);

                                    }
                                }

                                else
                                {
                                    empAttDetail.EmployeeId = empId;
                                    if (monthDate != null)
                                    {
                                        empAttDetail.Date = monthDate;
                                        month = empAttDetail.Date.Value.Month;
                                        year = empAttDetail.Date.Value.Year;

                                        string timein = Convert.ToString(GetCellValue(row, 2, stringTable));
                                        if (timein == null || timein == "")
                                        {
                                            empAttDetail.PunchIn = null;
                                            // empAttDetail.PunchIn = DateTime.FromOADate(double.Parse(timein)).TimeOfDay.ToString();
                                        }
                                        else
                                        {
                                            empAttDetail.PunchIn = DateTime.FromOADate(double.Parse(timein)).TimeOfDay.ToString();
                                        }
                                        string timeout = Convert.ToString(GetCellValue(row, 3, stringTable));
                                        if (timeout == null || timeout == "")
                                        {
                                            empAttDetail.PunchOut = null;
                                            // empAttDetail.PunchIn = DateTime.FromOADate(double.Parse(timein)).TimeOfDay.ToString();
                                        }
                                        else
                                        {
                                            empAttDetail.PunchOut = DateTime.FromOADate(double.Parse(timeout)).TimeOfDay.ToString();
                                        }
                                        //if (!string.IsNullOrWhiteSpace(timeout))
                                        //{
                                        //    empAttDetail.PunchOut = DateTime.FromOADate(double.Parse(timeout)).TimeOfDay.ToString();
                                        //}
                                        // List<string> approvedLeaveDates = new List<string>();

                                        //   List<EmployeeLeaveDetail> approvedLeaveDates =  GetLeaves(empId,2,2020,1);
                                        // code to get approved leaves of that month
                                        var leaveStatus = 0;
                                        if (empAttDetail.PunchIn != null && empAttDetail.PunchOut != null && empAttDetail.PunchIn != "00:00:00" && empAttDetail.PunchOut != "00:00:00")
                                        {

                                            var duration = DateTime.Parse(empAttDetail.PunchOut).Subtract(DateTime.Parse(empAttDetail.PunchIn));
                                            if (duration.Hours < 0)
                                            {
                                                duration = DateTime.Parse(empAttDetail.PunchOut).AddDays(1).Subtract(DateTime.Parse(empAttDetail.PunchIn));
                                                // duration.Hours = duration.Hours + 24;
                                            }

                                            if (duration.Hours >= 9)
                                            {
                                                leaveStatus = (int)LeaveTypes.FullDay;
                                            }
                                            else if (duration.Hours >= 5 && duration.Hours < 9)
                                            {
                                                leaveStatus = (int)LeaveTypes.ShortLeave;
                                            }
                                            else if (duration.Hours >= 4 && duration.Hours < 5)
                                            {
                                                leaveStatus = (int)LeaveTypes.HalfDay;
                                            }
                                            else
                                            {
                                                leaveStatus = (int)LeaveTypes.UnpaidLeave;
                                            }
                                        }
                                        else
                                        {
                                            // Create seperate Date check function
                                            leaveStatus = GetIsHoliday(empAttDetail.Date.Value, holidays);
                                            //if (leaveStatus == 0)
                                            //{
                                            //    var approvedLeaveDates = GetLeaves(empId, month, year, startDay);
                                            //    var isApprovedLeave = approvedLeaveDates.Where(q => q == empAttDetail.Date).Count() > 0;
                                            //    if (isApprovedLeave)
                                            //    {
                                            //        leaveStatus = (int)LeaveTypes.ApprovedLeave;
                                            //    }
                                            //}
                                        }
                                        empAttDetail.LeaveType = leaveStatus;
                                        //empAttDetail.CreatedDate = DateTime.Now;
                                        empAttDetail.UpdatedDate = DateTime.Now;
                                        _unitOfWork.EmployeeAttendence.Update(empAttDetail);

                                    }
                                }
                            }

                        }
                    }
                    catch (Exception ex)
                    {
                        ex.ToString();
                    }
                    _unitOfWork.SaveChanges();
                    AddLeaveOfEmployee(empId, month, year);
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }

        private void ReadExcelSummary(string fullPath)
        {
            var empAtt = new List<EmployeeAttendence>();
            try
            {
                using (SpreadsheetDocument doc = SpreadsheetDocument.Open(fullPath, false))
                {
                    WorkbookPart wbPart = doc.WorkbookPart;

                    //statement to get the count of the worksheet  
                    int worksheetcount = doc.WorkbookPart.Workbook.Sheets.Count();

                    //statement to get the sheet object  
                    //Sheet mysheet = (Sheet)doc.WorkbookPart.Workbook.Sheets.ChildElements.GetItem(0);
                    Sheet mysheet = doc.WorkbookPart.Workbook.Sheets.Elements<Sheet>().First();

                    //statement to get the worksheet object by using the sheet id  
                    Worksheet Worksheet = ((WorksheetPart)wbPart.GetPartById(mysheet.Id)).Worksheet;

                    //Note: worksheet has 8 children and the first child[1] = sheetviewdimension,....child[4]=sheetdata  
                    int wkschildno = 4;
                    var stringTable = wbPart.GetPartsOfType<SharedStringTablePart>().FirstOrDefault();

                    //statement to get the sheetdata which contains the rows and cell in table  
                    //SheetData sheetData = (SheetData)Worksheet.ChildElements.GetItem(wkschildno);
                    SheetData sheetData = Worksheet.Elements<SheetData>().ElementAt(wkschildno);
                    string dy = string.Empty;
                    string mn = string.Empty;
                    string yy = string.Empty;
                    Guid empId = Guid.Empty;
                    int j = 0;
                    foreach (Row row in sheetData.Elements<Row>())
                    {
                        // header row
                        if (j++ == 0)
                            continue;

                        var attendanceSummary = new AttendenceSummary();

                        int code = Convert.ToInt32(GetCellValue(row, 0, stringTable));
                        Expression<Func<Employee, bool>> empSalFilter = e => e.EmployeeCode == code;
                        empId = _unitOfWork.Employee.Get(empSalFilter, null, "").Select(e => e.Id).FirstOrDefault();


                        string date = GetCellValue(row, 1, stringTable);

                        if (date.Contains("/"))
                        {
                            string[] dateParts = date.Split("/");
                            dy = dateParts[1];
                            mn = dateParts[0];
                            yy = dateParts[2];
                        }
                        else
                        {
                            DateTime datevalue = DateTime.FromOADate(double.Parse(date));
                            dy = datevalue.Day.ToString();
                            mn = datevalue.Month.ToString();
                            yy = datevalue.Year.ToString();
                        }

                        Expression<Func<AttendenceSummary, bool>> attSummFilter = e => e.EmployeeId == empId && e.Month == int.Parse(mn) && e.Year == int.Parse(yy);
                        var empAttSummary = _unitOfWork.AttendenceSummary.Get(attSummFilter, null, "").FirstOrDefault();
                        if (empAttSummary == null)
                        {
                            attendanceSummary.EmployeeId = empId;
                            if (!string.IsNullOrWhiteSpace(mn))
                            {
                                attendanceSummary.Month = int.Parse(mn);
                            }
                            if (!string.IsNullOrWhiteSpace(yy))
                            {
                                attendanceSummary.Year = int.Parse(yy);
                            }

                            string daysWorked = GetCellValue(row, 2, stringTable);
                            if (!string.IsNullOrWhiteSpace(daysWorked))
                            {
                                attendanceSummary.DaysWorked = Int32.Parse(daysWorked);
                            }

                            string leaveTaken = Convert.ToString(GetCellValue(row, 3, stringTable));
                            if (!string.IsNullOrWhiteSpace(leaveTaken))
                            {
                                attendanceSummary.LeaveTaken = Int32.Parse(leaveTaken);
                            }

                            string onDuty = Convert.ToString(GetCellValue(row, 4, stringTable));
                            if (!string.IsNullOrWhiteSpace(onDuty))
                            {
                                attendanceSummary.OnDuty = Int32.Parse(onDuty);
                            }
                            string weeklyOff = Convert.ToString(GetCellValue(row, 5, stringTable));
                            if (!string.IsNullOrWhiteSpace(weeklyOff))
                            {
                                attendanceSummary.WeeklyOff = Int32.Parse(weeklyOff);
                            }
                            string holidays = Convert.ToString(GetCellValue(row, 6, stringTable));
                            if (!string.IsNullOrWhiteSpace(holidays))
                            {
                                attendanceSummary.Holidays = Int32.Parse(holidays);
                            }
                            string unPaid = Convert.ToString(GetCellValue(row, 7, stringTable));
                            if (!string.IsNullOrWhiteSpace(unPaid))
                            {
                                attendanceSummary.Unpaid = Int32.Parse(unPaid);
                            }
                            string paidDays = Convert.ToString(GetCellValue(row, 8, stringTable));
                            if (!string.IsNullOrWhiteSpace(paidDays))
                            {
                                attendanceSummary.PaidDays = Int32.Parse(paidDays);
                            }
                            attendanceSummary.CreatedDate = DateTime.Now;
                            attendanceSummary.UpdatedDate = DateTime.Now;
                            _unitOfWork.AttendenceSummary.Add(attendanceSummary);
                        }
                        else
                        {
                            empAttSummary.EmployeeId = empId;
                            if (!string.IsNullOrWhiteSpace(mn))
                            {
                                empAttSummary.Month = int.Parse(mn);
                            }
                            if (!string.IsNullOrWhiteSpace(yy))
                            {
                                empAttSummary.Year = int.Parse(yy);
                            }

                            string daysWorked = GetCellValue(row, 2, stringTable);
                            if (!string.IsNullOrWhiteSpace(daysWorked))
                            {
                                empAttSummary.DaysWorked = Int32.Parse(daysWorked);
                            }

                            string leaveTaken = Convert.ToString(GetCellValue(row, 3, stringTable));
                            if (!string.IsNullOrWhiteSpace(leaveTaken))
                            {
                                empAttSummary.LeaveTaken = Int32.Parse(leaveTaken);
                            }

                            string onDuty = Convert.ToString(GetCellValue(row, 4, stringTable));
                            if (!string.IsNullOrWhiteSpace(onDuty))
                            {
                                empAttSummary.OnDuty = Int32.Parse(onDuty);
                            }
                            string weeklyOff = Convert.ToString(GetCellValue(row, 5, stringTable));
                            if (!string.IsNullOrWhiteSpace(weeklyOff))
                            {
                                empAttSummary.WeeklyOff = Int32.Parse(weeklyOff);
                            }
                            string holidays = Convert.ToString(GetCellValue(row, 6, stringTable));
                            if (!string.IsNullOrWhiteSpace(holidays))
                            {
                                empAttSummary.Holidays = Int32.Parse(holidays);
                            }
                            string unPaid = Convert.ToString(GetCellValue(row, 7, stringTable));
                            if (!string.IsNullOrWhiteSpace(unPaid))
                            {
                                empAttSummary.Unpaid = Int32.Parse(unPaid);
                            }
                            string paidDays = Convert.ToString(GetCellValue(row, 8, stringTable));
                            if (!string.IsNullOrWhiteSpace(paidDays))
                            {
                                empAttSummary.PaidDays = Int32.Parse(paidDays);
                            }
                            // empAttSummary.CreatedDate = DateTime.Now;
                            empAttSummary.UpdatedDate = DateTime.Now;
                            _unitOfWork.AttendenceSummary.Update(empAttSummary);
                        }


                        //  GetEmpsal(empId, int.Parse(mn), int.Parse(yy));

                    }
                    _unitOfWork.SaveChanges();
                    AddLeaveEmployeeSalary(empId, int.Parse(mn), int.Parse(yy));
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in ExcelUploadController - ReadExcelSummary " + ex.Message);
            }
        }

        private string GetCellValue(Row row, int index, SharedStringTablePart stringTable)
        {
            var val = string.Empty;

            try
            {
                if (row.ChildElements.Count() > index)
                {
                    //Cell currentcell = (Cell)row.ChildElements.GetItem(index);
                    //var value = row.ElementAt(index);
                    Cell currentcell = row.Elements<Cell>().ElementAt(index);
                    var value = currentcell;
                    if (currentcell.DataType != null && currentcell.DataType.Value == CellValues.SharedString)
                    {
                        int.TryParse(value.InnerText, out int valsueString);
                        val = stringTable.SharedStringTable.ElementAt(valsueString).InnerText;
                    }
                    else
                    {
                        val = value.InnerText;
                    }
                }


            }
            catch (Exception ex)
            {
                _logger.LogError("Error in ExcelUploadController - GetCellValue " + ex.Message);
            }
            return val;

        }

        [HttpGet("empMonthlyDetail")]
        public IActionResult GetEmpMontlyDetail(Guid id, int? month, int? year)
        {
            int startDay = 1;
            if (year == null)
            {
                year = DateTime.Today.Year;
            }
            if (month == null)
            {
                month = DateTime.Today.AddMonths(-2).Month;
            }
            Expression<Func<Employee, bool>> empFilter = e => e.UserId == id.ToString();
            var emp = _unitOfWork.Employee.Get(empFilter, null, "").FirstOrDefault();
            var attendence = _unitOfWork.EmployeeAttendence.GetEmployeeMonthlyDetail(emp.Id, month.Value, year.Value).ToList();
            var monthlyAttendence = new MonthlyAttendence();
            monthlyAttendence.MonthDates = GetDates(month.Value, year.Value, startDay);
            monthlyAttendence.Month = month.Value;
            monthlyAttendence.Year = year.Value;
            monthlyAttendence.StartDay = startDay;
            monthlyAttendence.EmployeeAttendenceVM = _mapper.Map<IEnumerable<EmployeeAttendence>, IEnumerable<EmployeeAttendenceViewModel>>(attendence);
            monthlyAttendence.ApprovedLeavedates = null;
            return Ok(monthlyAttendence);
        }

        [HttpGet("empAllMonthlyDetail")]
        public IActionResult GetAllEmpMontlyDetail(int? month, int? year)
        {
            int startDay = 1;
            var date = DateTime.Today.AddMonths(-1);
            if (month == null)
            {
                month = date.Month;
            }
            if (year == null)
            {

                year = date.Year;

            }
            var attendence = _unitOfWork.EmployeeAttendence.GetEmployeeMonthlyDetail(null, month.Value, year.Value).ToList();

            var monthlyAttendence = new MonthlyAttendence();
            monthlyAttendence.MonthDates = GetDates(month.Value, year.Value, startDay);
            monthlyAttendence.Month = month.Value;
            monthlyAttendence.Year = year.Value;
            monthlyAttendence.StartDay = startDay;
            monthlyAttendence.EmployeeAttendenceVM = _mapper.Map<IEnumerable<EmployeeAttendence>, IEnumerable<EmployeeAttendenceViewModel>>(attendence);

            return Ok(monthlyAttendence);
        }

        [HttpPost("UpdateEmpAtt")]
        public IActionResult ManageEmployeeAttendance(Guid? empid, string punchin, string punchout, DateTime date, int status)
        {
            Expression<Func<EmployeeAttendence, bool>> empFilter = e => e.EmployeeId == empid && e.Date == date;
            var employeeAtt = _unitOfWork.EmployeeAttendence.Get(empFilter, null, "").FirstOrDefault();
            if (employeeAtt == null)
            {
                employeeAtt = new EmployeeAttendence();
                employeeAtt.CreatedDate = DateTime.Now;
                //  employee.CreatedBy = 1;
            }
            employeeAtt.PunchIn = punchin;
            employeeAtt.PunchOut = punchout;
            employeeAtt.EmployeeId = empid.Value;
            employeeAtt.UpdatedDate = DateTime.Now;
            employeeAtt.LeaveType = status;
            //employee.UpdatedBy = 1;
            try
            {

                if (employeeAtt.Id != 0)
                {
                    _unitOfWork.EmployeeAttendence.Update(employeeAtt);
                    _unitOfWork.SaveChanges();
                    return Ok("Employee Attendence Updated");
                }
                else if (empid == null)
                {
                    _unitOfWork.EmployeeAttendence.Add(employeeAtt);
                    _unitOfWork.SaveChanges();
                    return Ok("EmployeeAttendence Added");
                }
                return BadRequest(ModelState);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.GetBaseException().Message);
            }

        }

        [HttpPost("attSummary")]
        public IActionResult AttendanceSummary(Guid empid, int month, int year)
        {
            GetAttendenceSummary(empid, month, year);
            return Ok();
        }

        [HttpPost("saveAllEmpAttSummary")]
        public IActionResult ManageAllEmpAttSummary(int month, int year)
        {
            GetAttendenceSummary(null, month, year);
            return Ok();
        }

        [HttpGet("getEmpAttSummary")]
        public IActionResult GetAllEmpAttendenceSummary(int? month, int? year)
        {

            var date = DateTime.Today.AddMonths(-1);
            if (month == null)
            {
                month = date.Month;
            }
            if (year == null)
            {

                year = date.Year;

            }

            Expression<Func<Employee, bool>> empFilter = e => true;
            var empList = _unitOfWork.Employee.Get(empFilter, null, "").ToList();
            var employeesList = new List<AttendenceSummaryViewModel>();
            foreach (var emp in empList)
            {
                Expression<Func<AttendenceSummary, bool>> empAttSummaryFilter = e => e.EmployeeId == emp.Id && e.Month == month && e.Year == year;
                var empSummaryList = _unitOfWork.AttendenceSummary.Get(empAttSummaryFilter, null, "Employee.ApplicationUser").FirstOrDefault();
                if (empSummaryList != null)
                {
                    var viewModel = new AttendenceSummaryViewModel();
                    viewModel.EmployeeName = empSummaryList.Employee.ApplicationUser.FullName;//emp.ApplicationUser.FullName;

                    viewModel.DaysWorked = empSummaryList.DaysWorked;
                    viewModel.LeaveTaken = empSummaryList.LeaveTaken;
                    viewModel.OnDuty = empSummaryList.OnDuty;
                    viewModel.WeeklyOff = empSummaryList.WeeklyOff;
                    viewModel.Holidays = empSummaryList.Holidays;
                    viewModel.Unpaid = empSummaryList.Unpaid;
                    viewModel.PaidDays = empSummaryList.PaidDays;
                    employeesList.Add(viewModel);
                }


            }
            return Ok(employeesList);
        }

        private void GetAttendenceSummary(Guid? empId, int month, int year)
        {
            var attendenceList = _unitOfWork.EmployeeAttendence.GetEmployeeMonthlyDetail(empId, month, year).ToList();
            var employees = _unitOfWork.Employee.GetAllEmployee();
            if (empId.HasValue)
            {
                employees = employees.Where(q => q.Id == empId.Value);
            }
            var empList = employees.ToList();
            foreach (var emp in empList)
            {
                var empid = emp.Id;

                var attendence = attendenceList.Where(q => q.EmployeeId == empid).ToList();
                var attSumary = _unitOfWork.AttendenceSummary.GetEmployeeSummary(empid, month, year);
                if (attSumary == null)
                {
                    attSumary = new AttendenceSummary();
                }
                attSumary.EmployeeId = empid;
                attSumary.Month = month;
                attSumary.Year = year;

                attSumary.Unpaid = attendence.Where(q => q.LeaveType == (int)LeaveTypes.UnpaidLeave).Count();
                attSumary.WeeklyOff = attendence.Where(q => q.LeaveType == (int)LeaveTypes.WeeklyOff).Count();
                attSumary.Holidays = attendence.Where(q => q.LeaveType == (int)LeaveTypes.HoliDay).Count();
                var fullDay = attendence.Where(q => q.LeaveType == (int)LeaveTypes.FullDay).Count();
                var halfDay = attendence.Where(q => q.LeaveType == (int)LeaveTypes.HalfDay).Count();
                attSumary.LeaveTaken = attendence.Where(q => q.LeaveType == (int)LeaveTypes.ApprovedLeave).Count() + attSumary.Unpaid + (halfDay / 2);
                attSumary.DaysWorked = Convert.ToDecimal(fullDay + halfDay / 2.0);

                if (attSumary.Id > 0)
                {
                    // update
                    attSumary.UpdatedDate = DateTime.Now;
                    _unitOfWork.AttendenceSummary.Update(attSumary);

                }
                else
                {
                    // insert
                    attSumary.CreatedDate = DateTime.Now;
                    _unitOfWork.AttendenceSummary.Add(attSumary);
                }
                EmployeeSalary empSal = _unitOfWork.EmployeeSalary.Get(q => q.EmployeeId == empid && q.Month == month && q.Year == year).FirstOrDefault();
                EmployeeCtc empCtc = _unitOfWork.EmployeeCTC.Get(q => q.EmployeeId == empid).FirstOrDefault();

                if (empSal == null)
                {
                    empSal = new EmployeeSalary();
                    empSal.Month = month;
                    empSal.Year = year;
                    empSal.LeaveTaken = attSumary.LeaveTaken;
                    empSal.EmployeeCtcId = empCtc.Id;
                    empSal.EmployeeId = empid;
                    if (empCtc != null)
                    {
                        SetCTCPart(empSal, empCtc);
                    }
                    _unitOfWork.EmployeeSalary.Add(empSal);
                }
                else
                {
                    empSal.LeaveTaken = attSumary.LeaveTaken;
                    _unitOfWork.EmployeeSalary.Update(empSal);
                }

            }
            _unitOfWork.SaveChanges();

        }

        private void AddLeaveEmployeeSalary(Guid empId, int month, int year)
        {
            var attSumary = _unitOfWork.AttendenceSummary.GetEmployeeSummary(empId, month, year);
            EmployeeSalary empSal = _unitOfWork.EmployeeSalary.Get(q => q.EmployeeId == empId && q.Month == month && q.Year == year).FirstOrDefault();

            EmployeeCtc empCtc = _unitOfWork.EmployeeCTC.Get(q => q.EmployeeId == empId).FirstOrDefault();

            Expression<Func<Employee, bool>> empFilter = e => e.Id == empId;
            var employee = _unitOfWork.Employee.Get(empFilter, null, "").FirstOrDefault();

            int unpaidDays = 0;
            decimal unpaid = 0;
            decimal salDeductionAmount = 0;
            var leavesPerYear = _unitOfWork.LeaveRules.Get(q => q.BandId == employee.BandId && q.IsActive == true).ToList();
            var totalLeavesPerYear = leavesPerYear.Select(x => x.LeavesPerYear).ToList().Sum(x => Convert.ToInt32(x));
            var monthlyAllowedLeaves = totalLeavesPerYear / 12;
            var perDaySalary = GetPerDaySalary(month, year, empCtc.BasicSalary);
            if (empCtc != null)
            {
                if (empSal == null)
                {
                    empSal = new EmployeeSalary();
                    empSal.Month = month;
                    empSal.Year = year;
                    //  empSal.LeaveTaken = attSumary.LeaveTaken;
                    empSal.EmployeeCtcId = empCtc.Id;
                    empSal.EmployeeId = empId;
                    if (attSumary.LeaveTaken != 0)
                    {
                        empSal.LeaveTaken = attSumary.LeaveTaken;
                        unpaidDays = Convert.ToInt32(empSal.LeaveTaken) - monthlyAllowedLeaves;
                        if (unpaidDays > 0)
                        {
                            unpaid = perDaySalary * unpaidDays;
                            salDeductionAmount = Convert.ToDecimal(String.Format("{0:0.00}", unpaid));
                            empSal.UnpaidDays = salDeductionAmount;
                        }
                        _unitOfWork.EmployeeSalary.Update(empSal);
                        _unitOfWork.SaveChanges();
                    }
                    if (empCtc != null)
                    {
                        SetCTCPart(empSal, empCtc);
                    }
                    _unitOfWork.EmployeeSalary.Add(empSal);
                }
                else
                {
                    // empSal.LeaveTaken = attSumary.LeaveTaken;
                    if (attSumary.LeaveTaken != 0)
                    {
                        empSal.LeaveTaken = attSumary.LeaveTaken;
                        unpaidDays = Convert.ToInt32(empSal.LeaveTaken) - monthlyAllowedLeaves;
                        if (unpaidDays > 0)
                        {
                            unpaid = perDaySalary * unpaidDays;
                            salDeductionAmount = Convert.ToDecimal(String.Format("{0:0.00}", unpaid));
                            empSal.UnpaidDays = salDeductionAmount;
                        }
                        _unitOfWork.EmployeeSalary.Update(empSal);
                    }

                }
                _unitOfWork.SaveChanges();
            }
        }

        private void AddLeaveOfEmployee(Guid empId, int month, int year)
        {
            var att = _unitOfWork.EmployeeAttendence.GetEmployeeMonthlyDetail(empId, month, year);
            EmployeeSalary empSal = _unitOfWork.EmployeeSalary.Get(q => q.EmployeeId == empId && q.Month == month && q.Year == year).FirstOrDefault();

            Expression<Func<Employee, bool>> empFilter = e => e.Id == empId;
            var employee = _unitOfWork.Employee.Get(empFilter, null, "").FirstOrDefault();
            Expression<Func<EmployeeCtc, bool>> empCTCFilter = e => e.EmployeeId == employee.Id;
            var employeeCtc = _unitOfWork.EmployeeCTC.Get(empCTCFilter, null, "").FirstOrDefault();
            int unpaidDays = 0;
            decimal unpaid = 0;
            decimal salDeductionAmount = 0;
            var leavesPerYear = _unitOfWork.LeaveRules.Get(q => q.BandId == employee.BandId && q.IsActive == true).ToList();
            var totalLeavesPerYear = leavesPerYear.Select(x => x.LeavesPerYear).ToList().Sum(x => Convert.ToInt32(x));
            var monthlyAllowedLeaves = totalLeavesPerYear / 12;
            var perDaySalary = employeeCtc!=null ? GetPerDaySalary(month, year, employeeCtc.BasicSalary): 0;

            if (empSal != null)
            {
                //var leave = att.Where(q => q.LeaveType == (int)LeaveTypes.ApprovedLeave).Count();
                var leave = att.Where(q => q.LeaveType == 0).Count();
                if (leave != 0)
                {
                    empSal.LeaveTaken = leave;
                    unpaidDays = Convert.ToInt32(empSal.LeaveTaken) - monthlyAllowedLeaves;
                    if (unpaidDays > 0)
                    {
                        unpaid = perDaySalary * unpaidDays;
                        salDeductionAmount = Convert.ToDecimal(String.Format("{0:0.00}", unpaid));
                        empSal.UnpaidDays = salDeductionAmount;
                    }
                    _unitOfWork.EmployeeSalary.Update(empSal);
                    _unitOfWork.SaveChanges();
                }


            }

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
        private void SetCTCPart(EmployeeSalary empSal, EmployeeCtc empCtc)
        {
            // empSal.Ctc = empCtc.CTC / 12;
            empSal.BasicSalary = empCtc.BasicSalary / 12;
            empSal.DA = empCtc.DA / 12;
            empSal.HRA = empCtc.HRA / 12;
            empSal.Conveyance = empCtc.Conveyance / 12;
            empSal.MedicalExpenses = empCtc.MedicalExpenses / 12;
            empSal.Special = empCtc.Special / 12;
            empSal.Bonus = empCtc.Bonus / 12;
            empSal.Total = empCtc.Total / 12;
        }

        private List<DateTime> GetLeaves(Guid empid, int month, int year, int startDay)
        {
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
                    }
                    startDate = startDate.AddDays(1);
                }
            }
            return leaveDates;
        }

        private List<MonthDate> GetDates(int month, int year, int startDay)
        {
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
            var dateList = new List<MonthDate>();
            while (date <= monthEndDay)
            {
                var monthDate = new MonthDate() { MDate = date.ToString("M/d/yyyy"), MDay = date.DayOfWeek.ToString() };
                monthDate.MDayStatus = GetIsHoliday(date);
                dateList.Add(monthDate);
                date = date.AddDays(1);
            }
            return dateList;

        }

        private int GetIsHoliday(DateTime date, List<string> holidays = null)
        {
            var status = 0;
            if (date.DayOfWeek.ToString() == "Saturday" || date.DayOfWeek.ToString() == "Sunday")
            {
                status = (int)LeaveTypes.WeeklyOff;
            }
            else
            {

                var isHoliday = false;
                // check for holiday
                if (holidays == null)
                {
                    holidays = _unitOfWork.LeaveManagement.GetHolidayList().Select(q => q.Holidaydate.ToShortDateString()).ToList();

                }// var holidays = new List<DateTime>(); // _appContext.LeaveHolidayList.Where(c => c.IsActive == true && c.LeavePeriodId == periodId).ToList();
                //Filter for that month
                isHoliday = holidays.Where(q => q == date.ToShortDateString()).Count() > 0;
                // isHoliday = holidays.Where(q => q.Holidaydate == date).Count() > 0;
                if (isHoliday)
                {
                    status = (int)LeaveTypes.HoliDay;
                }

            }
            return status;

        }

    }
    //private void GetEmpsal(Guid empId, int month, int year)
    //{

    //}
}

