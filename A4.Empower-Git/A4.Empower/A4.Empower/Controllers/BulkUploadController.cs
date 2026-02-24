using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using A4.BAL;
using A4.BAL.Maintenance;
using A4.DAL;
using A4.DAL.Entites;
using A4.DAL.Entites.Recruitment;
using A4.Empower.Enum;
using A4.Empower.Helpers;
using DAL;
using DAL.Core.Interfaces;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using ExcelDataReader;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using OfficeOpenXml;

namespace A4.Empower.Controllers
{

    [Route("api/[controller]")]
    public class BulkUploadController : Controller
    {
        readonly IEmailer _emailer;
        readonly ILogger _logger;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private IConfiguration _configuration;
        private readonly ApplicationDbContext _context;
        private readonly IAccountManager _accountManager;
        private readonly IUnitOfWork _unitOfWork;
        public BulkUploadController(ILogger<BulkUploadController> logger, IUnitOfWork unitOfWork, IWebHostEnvironment hostingEnvironment, IAccountManager accountManager, IEmailer emailer, IConfiguration Configuration, ApplicationDbContext context)
        {
            _emailer = emailer;
            _configuration = Configuration;
            _logger = logger;
            _hostingEnvironment = hostingEnvironment;
            _context = context;
            _accountManager = accountManager;
            _unitOfWork = unitOfWork;
        }

        [HttpPost, DisableRequestSizeLimit]
        public IActionResult Upload()
        {
            var file = Request.Form.Files[0];
            if (file.Length > 0)
            {
                _context.Database.ExecuteSqlRaw("uspTruncateBulkUploadData");
                var fileInfo = getFilePath(file, out string fileAddress);
                List<BulkUploadViewModel> employeeList = new List<BulkUploadViewModel>();
                DataTable dt = new DataTable();
                using (ExcelPackage package = new ExcelPackage(fileInfo))
                {
                    ExcelWorksheet workSheet = package.Workbook.Worksheets[0];
                    int totalRows = workSheet.Dimension.Rows;

                    dt.Columns.Add("Id", typeof(Guid));
                    dt.Columns.Add("FullName", typeof(string));
                    dt.Columns.Add("WorkEmailAddress", typeof(string));
                    dt.Columns.Add("FunctionalDepartment", typeof(string));
                    dt.Columns.Add("FunctionalGroup", typeof(string));
                    dt.Columns.Add("Designation", typeof(string));
                    dt.Columns.Add("PersonalEmailID", typeof(string));
                    dt.Columns.Add("Title", typeof(string));
                    dt.Columns.Add("ReportingHeadEmailId", typeof(string));
                    dt.Columns.Add("RollAccess", typeof(string));
                    dt.Columns.Add("Location", typeof(string));
                    dt.Columns.Add("DateofJoining", typeof(string));
                    dt.Columns.Add("ReportingHeadName", typeof(string));
                    dt.Columns.Add("Band", typeof(string));
                    dt.Columns.Add("Status", typeof(string));
                    dt.Columns.Add("ErrorMessage", typeof(string));

                    for (int i = 2; i <= totalRows; i++)
                    {
                        DataRow dr = dt.NewRow();

                        for (int j = 1; j <= dt.Columns.Count; j++)
                        {

                            if (j - 1 == 0)
                            {
                                dr[j - 1] = Guid.NewGuid();
                            }
                            else
                            {
                                dr[j - 1] = workSheet.Cells[i, j - 1].Value != null ? workSheet.Cells[i, j - 1].Value.ToString() : "";
                            }
                        }
                        dt.Rows.Add(dr);
                    }
                }

                int columnCount = dt.Columns.Count;
                for (int i = dt.Rows.Count - 1; i >= 0; i--)
                {
                    bool allNull = true;
                    for (int j = 1; j < columnCount; j++)
                    {
                        if (dt.Rows[i][j].ToString() != "")
                        {
                            allNull = false;
                        }
                    }
                    if (allNull)
                    {
                        dt.Rows[i].Delete();
                    }
                }
                dt.AcceptChanges();
                DataColumn sPassword = new DataColumn("PasswordHash", typeof(System.String));
                sPassword.DefaultValue = "123456";
                dt.Columns.Add(sPassword);
                foreach (DataRow row in dt.Rows)
                {
                    string password = "Password@123";
                    row["PasswordHash"] = HashPassword(password);
                    row.EndEdit();
                    dt.AcceptChanges();
                }
                using (var sqlBulkCopy = new SqlBulkCopy(_configuration["ConnectionStrings:DefaultConnection"]))
                {
                    sqlBulkCopy.DestinationTableName = "ExcelEmployeeData";
                    sqlBulkCopy.ColumnMappings.Add("Id", "Id");
                    sqlBulkCopy.ColumnMappings.Add("FullName", "FullName");
                    sqlBulkCopy.ColumnMappings.Add("WorkEmailAddress", "WorkEmailAddress");
                    sqlBulkCopy.ColumnMappings.Add("PasswordHash", "PasswordHash");
                    sqlBulkCopy.ColumnMappings.Add("FunctionalDepartment", "FunctionalDepartment");
                    sqlBulkCopy.ColumnMappings.Add("FunctionalGroup", "FunctionalGroup");
                    sqlBulkCopy.ColumnMappings.Add("Designation", "Designation");
                    sqlBulkCopy.ColumnMappings.Add("PersonalEmailID", "PersonalEmailID");
                    sqlBulkCopy.ColumnMappings.Add("Title", "Title");
                    sqlBulkCopy.ColumnMappings.Add("ReportingHeadEmailId", "ReportingHeadEmailId");
                    sqlBulkCopy.ColumnMappings.Add("RollAccess", "RollAccess");
                    sqlBulkCopy.ColumnMappings.Add("Location", "Location");
                    sqlBulkCopy.ColumnMappings.Add("DateofJoining", "DateofJoining");
                    sqlBulkCopy.ColumnMappings.Add("ReportingHeadName", "ReportingHeadName");
                    sqlBulkCopy.ColumnMappings.Add("Band", "Band");
                    sqlBulkCopy.ColumnMappings.Add("Status", "Status");
                    sqlBulkCopy.ColumnMappings.Add("ErrorMessage", "ErrorMessage");
                    sqlBulkCopy.WriteToServer(dt);
                }
                _context.Database.ExecuteSqlRaw("uspValidateBulkUpload");
            }
            BulkUploadMails();
            return Ok(getFailedData(0, 10));
        }

        [HttpPost("CandidateBulkUpload"), DisableRequestSizeLimit]
        public IActionResult BulkCandidateDataUploadAsync()
        {
            var file = Request.Form.Files[0];
            var excelCandidateList = new List<ExcelCandidateData>();
            List<string> errorsList = new List<string>();

            if (file != null)
            {
                if (file.FileName.EndsWith(".xls") || file.FileName.EndsWith(".xlsx"))
                {
                    try
                    {
                        var fileInfo = getFilePath(file, out string fileAddress);
                        using var stream = System.IO.File.Open(fileAddress, FileMode.Open, FileAccess.Read, FileShare.None);

                        using var reader = ExcelReaderFactory.CreateReader(stream);
                        var rowResult = reader.AsDataSet(new ExcelDataSetConfiguration()
                        {
                            ConfigureDataTable = (_) => new ExcelDataTableConfiguration()
                            {
                                UseHeaderRow = true
                            }
                        });

                        if (rowResult.Tables.Count > 0)
                        {
                            foreach (DataTable sheetData in rowResult.Tables)
                            {
                                if (sheetData.Rows.Count > 0 && sheetData != null)
                                {

                                    foreach (DataRow objDataRow in sheetData.Rows)
                                    {
                                        var serialnumber = objDataRow["S.no"]?.ToString();
                                        var name = objDataRow["CandidateName"]?.ToString();
                                        var jobName = objDataRow["JobTitle"]?.ToString();
                                        var jobTitle = objDataRow["JobType"]?.ToString();
                                        var phoneNumber = objDataRow["PhoneNumber"]?.ToString();
                                        var email = objDataRow["Email"]?.ToString();
                                        var feedback = objDataRow["Feedback"]?.ToString();
                                        var level1ManagerId = objDataRow["Level1_ManagerId"]?.ToString();
                                        var level1Result = objDataRow["Level1_Result"]?.ToString();
                                        var level2ManagerId = objDataRow["Level2_ManagerId"]?.ToString();
                                        var level2Result = objDataRow["Level2_Result"]?.ToString();
                                        var level3ManagerId = objDataRow["Level3_ManagerId"]?.ToString();
                                        var level3Result = objDataRow["Level3_Result"]?.ToString();

                                        //if (string.IsNullOrWhiteSpace(jobName) || string.IsNullOrWhiteSpace(jobTitle)
                                        //    && string.IsNullOrWhiteSpace(email))
                                        //{
                                        //    errorsList.Add($"{serialnumber} on  {sheetData} Job Details and Email or LastInterviewedBy Records are empty");
                                        //    continue;
                                        //}

                                        //var isEmailValid = Regex.IsMatch(email, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                                        //if (!isEmailValid)
                                        //{
                                        //    errorsList.Add($"Record {serialnumber} on {sheetData}, It's {email} email is not in valid format");
                                        //    continue;
                                        //}

                                        excelCandidateList.Add(new ExcelCandidateData()
                                        {
                                            CandidateName = name,
                                            JobName = jobName,
                                            JobTitle = jobTitle,
                                            PhoneNumber = phoneNumber,
                                            Email = email,
                                            Feedback = feedback,
                                            Level1ManagerId = Int32.TryParse(level1ManagerId, out var tempVal) ? tempVal : (int?)null,
                                            Level1Result = level1Result,
                                            Level2ManagerId = Int32.TryParse(level2ManagerId, out var tempVal1) ? tempVal1 : (int?)null,
                                            Level2Result = level2Result,
                                            Level3ManagerId = Int32.TryParse(level3ManagerId, out var tempVal2) ? tempVal2 : (int?)null,
                                            Level3Result = level3Result

                                        });

                                    }
                                }
                                if (excelCandidateList.Any())
                                {
                                    _context.ExcelCandidateData.AddRange(excelCandidateList);
                                    _context.SaveChanges();
                                    return Ok(excelCandidateList);
                                }

                            }

                        }

                    }
                    catch (Exception ex)
                    {
                        throw;
                    }

                }
                return BadRequest("File Is not in excel format");
            }
            return BadRequest();
        }

        [HttpPost("ImportCandidateBulkData")]
        public async Task<IActionResult> ImportCandidateDataAsync([FromBody] List<ExcelCandidateData> candidateBulks)
        {
            if (candidateBulks.Any())
            {
                foreach (var model in candidateBulks)
                {
                    if (model != null)
                    {
                        var applicationList = new List<JobApplication>();
                        var existingList = new List<JobApplication>();
                        var applicationUser = _accountManager.GetUserByEmailAsync(model.Email).Result;
                        if (applicationUser == null)
                        {
                            var appUser = new ApplicationUser()
                            {
                                Email = model.Email,
                                FullName = model.CandidateName,
                                PhoneNumber = model.PhoneNumber,
                                UserName = model.Email,
                                IsEnabled = true,
                            };

                            var role = await _accountManager.GetRoleLoadRelatedAsync("candidate");
                            var candidateRole = new string[4];
                            if (role != null) candidateRole = new string[] { role.Name };

                            string password = "Password@123";
                            var candidatePassword = HashPassword(password);
                            var result = await _accountManager.CreateUserAsync(appUser, candidateRole, candidatePassword);
                            await _accountManager.AssignRolesToUser(appUser.Id, candidateRole);
                            if (result.Item1) applicationUser = appUser;
                            if (!result.Item1)
                            {
                                model.ErrorMessage = $"{result.Item2.Where(array => array.Any()).Select(array => array.First())}";
                                model.Status = "False";
                                continue;
                            }
                        }

                        if (applicationUser != null)
                        {
                            var getCandidate = _unitOfWork.JobCandidateProfile.GetJobCandidateProfile(applicationUser.Id);
                            if (getCandidate == null)
                            {
                                JobCandidateProfile personal = new JobCandidateProfile
                                {
                                    UserId = applicationUser.Id
                                };
                                _unitOfWork.JobCandidateProfile.CreateCandidateProfile(personal);

                                JobCandidateQualification qualification = new JobCandidateQualification
                                {
                                    JobCandidateProfilesId = personal.Id
                                };
                                _unitOfWork.JobCandidateProfile.CreateQualification(qualification);

                                _unitOfWork.SaveChanges();

                                getCandidate = _unitOfWork.JobCandidateProfile.GetCandidateProfile(personal.Id);
                            }

                            var jobVacancy = _unitOfWork.JobVacancy.GetSingleJobVacanncy(model.JobTitle, model.JobName);
                            if (jobVacancy == null)
                            {
                                model.ErrorMessage = $"Job Vacancy Not found {model.JobTitle}";
                                model.Status = "False";
                                continue;
                            }

                            var application = new JobApplication();
                            Expression<Func<JobApplication, bool>> applicationFilter = e => e.JobCandidateProfileId == getCandidate.Id && e.JobVacancyId == jobVacancy.Id;
                            var existingApplication = _unitOfWork.JobApplication.Get(applicationFilter, null, "").FirstOrDefault();
                            if (existingApplication == null)
                            {
                                application.ApplicationType = "BulkUpload";
                                application.ScreeningScore = "0";
                                application.JobStatus = Convert.ToInt32(JobStatus.Applied);
                                application.JobVacancyId = jobVacancy.Id;
                                application.HRComment = "";
                                application.Feedback = model.Feedback;
                                application.HRScore = "0";
                                application.SkillScore = "0";
                                application.OverallScore = "0";
                                application.JobCandidateProfileId = getCandidate.Id;
                                applicationList.Add(application);
                                existingApplication = application;

                            }
                            else existingList.Add(existingApplication);
                            if (existingList.Any())
                            {
                                _unitOfWork.JobApplication.UpdateRange(existingList);
                                _unitOfWork.SaveChanges();
                            }
                            _unitOfWork.JobApplication.AddRange(applicationList);
                            var vaccancyLevel = _unitOfWork.JobVacancyLevel.Find(m => m.JobVacancyId == jobVacancy.Id).ToList();
                            var levelCounts = 0;
                            if (vaccancyLevel.Any())
                            {
                                foreach (var leveldata in vaccancyLevel)
                                {

                                    switch (levelCounts)
                                    {
                                        case 0:
                                            {
                                                if (model.Level1ManagerId.HasValue)
                                                {
                                                    Expression<Func<Employee, bool>> empSalFilter = e => e.EmployeeCode == model.Level1ManagerId;
                                                    var empId = _unitOfWork.Employee.Get(empSalFilter, null, "").Select(e => e.Id).FirstOrDefault();
                                                    if (empId != null && empId != Guid.Empty)
                                                    {
                                                        var vacancyLevelManager = new JobVacancyLevelManager(){
                                                            JobVacancyLevelId = leveldata.Id,
                                                            EmployeeId = empId
                                                        };
                                                        _unitOfWork.JobVacancyLevelManager.Add(vacancyLevelManager);
                                                        var candidateInterview = new JobCandidateInterview()
                                                        {
                                                            Date = DateTime.Now,
                                                            IsInterviewCompleted = true,
                                                            IsLevelCompleted = true,
                                                            JobInterviewTypeId = _unitOfWork.JobInterviewType.GetAll().FirstOrDefault().Id,
                                                            JobApplicationId = existingApplication.Id,
                                                            JobVacancyLevelManagerId = vacancyLevelManager.Id,
                                                            IsCandidateSelected = !string.IsNullOrWhiteSpace(model.Level1Result) ? Boolean.Parse(model.Level1Result) : false
                                                        };
                                                        _unitOfWork.JobCandidateInterview.Add(candidateInterview);
                                                        
                                                    }
                                                }

                                            }
                                            break;
                                        case 1:
                                            {
                                                if (model.Level2ManagerId.HasValue)
                                                {
                                                    Expression<Func<Employee, bool>> empSalFilter = e => e.EmployeeCode == model.Level2ManagerId;
                                                    var empId = _unitOfWork.Employee.Get(empSalFilter, null, "").Select(e => e.Id).FirstOrDefault();
                                                    if (empId != null && empId != Guid.Empty)
                                                    {
                                                        var vacancyLevelManager = new JobVacancyLevelManager()
                                                        {
                                                            JobVacancyLevelId = leveldata.Id,
                                                            EmployeeId = empId
                                                        };
                                                        _unitOfWork.JobVacancyLevelManager.Add(vacancyLevelManager);
                                                        var candidateInterview = new JobCandidateInterview()
                                                        {
                                                            Date = DateTime.Now,
                                                            IsInterviewCompleted = true,
                                                            IsLevelCompleted = true,
                                                            JobInterviewTypeId = _unitOfWork.JobInterviewType.GetAll().FirstOrDefault().Id,
                                                            JobApplicationId = existingApplication.Id,
                                                            JobVacancyLevelManagerId = vacancyLevelManager.Id,
                                                            IsCandidateSelected = !string.IsNullOrWhiteSpace(model.Level2Result) ? Boolean.Parse(model.Level2Result) : false
                                                        };
                                                        _unitOfWork.JobCandidateInterview.Add(candidateInterview);
                                                        
                                                    }
                                                }

                                            }
                                            break;

                                        case 2:
                                            {
                                                if (model.Level3ManagerId.HasValue)
                                                {
                                                    Expression<Func<Employee, bool>> empSalFilter = e => e.EmployeeCode == model.Level3ManagerId;
                                                    var empId = _unitOfWork.Employee.Get(empSalFilter, null, "").Select(e => e.Id).FirstOrDefault();
                                                    if (empId != null && empId != Guid.Empty)
                                                    {
                                                        leveldata.JobVacancyLevelManagers.Add(new JobVacancyLevelManager
                                                        {
                                                            JobVacancyLevelId = leveldata.Id,
                                                            EmployeeId = empId
                                                        });

                                                        var candidateInterview = new JobCandidateInterview()
                                                        {
                                                            Date = DateTime.Now,
                                                            IsInterviewCompleted = true,
                                                            IsLevelCompleted = true,
                                                            JobInterviewTypeId = _unitOfWork.JobInterviewType.GetAll().FirstOrDefault().Id,
                                                            JobApplicationId = existingApplication.Id,
                                                            JobVacancyLevelManagerId = leveldata.JobVacancyLevelManagers.FirstOrDefault().Id,
                                                            IsCandidateSelected = !string.IsNullOrWhiteSpace(model.Level3Result) ? Boolean.Parse(model.Level3Result) : false
                                                        };
                                                        _unitOfWork.JobCandidateInterview.Add(candidateInterview);
                                                    }
                                                }
                                            }
                                            break;
                                        case 3:
                                            {
                                                if (model.Level4ManagerId.HasValue)
                                                {
                                                    Expression<Func<Employee, bool>> empSalFilter = e => e.EmployeeCode == model.Level4ManagerId;
                                                    var empId = _unitOfWork.Employee.Get(empSalFilter, null, "").Select(e => e.Id).FirstOrDefault();
                                                    if (empId != null && empId != Guid.Empty)
                                                    {
                                                        leveldata.JobVacancyLevelManagers.Add(new JobVacancyLevelManager
                                                        {
                                                            JobVacancyLevelId = leveldata.Id,
                                                            EmployeeId = empId
                                                        });

                                                        var candidateInterview = new JobCandidateInterview()
                                                        {
                                                            Date = DateTime.Now,
                                                            IsInterviewCompleted = true,
                                                            IsLevelCompleted = true,
                                                            JobInterviewTypeId = _unitOfWork.JobInterviewType.GetAll().FirstOrDefault().Id,
                                                            JobApplicationId = existingApplication.Id,
                                                            JobVacancyLevelManagerId = leveldata.JobVacancyLevelManagers.FirstOrDefault().Id,
                                                            IsCandidateSelected = !string.IsNullOrWhiteSpace(model.Level4Result) ? Boolean.Parse(model.Level4Result) : false
                                                        };
                                                        _unitOfWork.JobCandidateInterview.Add(candidateInterview);
                                                       
                                                    }
                                                }
                                            }
                                            break;

                                    }
                                    levelCounts++;
                                }
                            }
                        }

                        _unitOfWork.ExcelCandidateData.UpdateRange(candidateBulks);
                        _unitOfWork.SaveChanges();
                    }



                }
                return Ok(candidateBulks);
            }
            return BadRequest("Model is Empty");
        }

        private async void BulkUploadMails()
        {
            var host = HttpContext.Request;
            string url = "";
            if (host.IsHttps)
            {
                url = "https://" + host.Host.Value;
            }
            else
            {
                url = "http://" + host.Host.Value;
            }
            var empList = _context.Users.Where(c => c.IsMailSent == false).ToList();
            foreach (var item in empList)
            {
                item.IsMailSent = true;
            }
            _context.Users.UpdateRange(empList);
            _context.SaveChanges();
            foreach (var item in empList)
            {
                string message = AccountTemplates.GetEmployeeEmail(item.FullName, item.Email, "Password@123", url);
                (bool success, string errorMsg) response = await _emailer.SendEmailAsync(item.FullName, item.Email, "Employee Regestration", message);
            }


        }

        private string HashPassword(string password)
        {
            byte[] salt;
            byte[] bytes;
            if (password == null)
            {
                throw new ArgumentNullException("password");
            }
            using (Rfc2898DeriveBytes rfc2898DeriveByte = new Rfc2898DeriveBytes(password, 16, 1000))
            {
                salt = rfc2898DeriveByte.Salt;
                bytes = rfc2898DeriveByte.GetBytes(32);
            }
            byte[] numArray = new byte[49];
            Buffer.BlockCopy(salt, 0, numArray, 1, 16);
            Buffer.BlockCopy(bytes, 0, numArray, 17, 32);
            return Convert.ToBase64String(numArray);
        }

        [HttpGet("failedUpload/{page?}/{pageSize?}/{name?}")]
        public IActionResult FailedUpload(int? page = null, int? pageSize = null, string name = null)
        {
            var getFailedData = this.getFailedData(Convert.ToInt32(page), Convert.ToInt32(pageSize), name);
            return Ok(getFailedData);
        }

        private BulkUploadViewModel getFailedData(int? page = null, int? pageSize = null, string name = null)
        {
            var result = new BulkUploadViewModel();
            var model = new List<BulkUpload>();
            var excelEmployeeData = _context.ExcelEmployeeData.AsQueryable().Where(m => m.Status == "Invalid");
            if (!string.IsNullOrWhiteSpace(name) && name != "undefined")
                excelEmployeeData = excelEmployeeData.Where(c => c.FullName.Contains(name) || c.PersonalEmailID.Contains(name) || c.ReportingHeadEmailId.Contains(name)
                || c.WorkEmailAddress.Contains(name) || c.Status.Contains(name) || c.ErrorMessage.Contains(name));
            excelEmployeeData = excelEmployeeData.OrderBy(c => c.FullName).ThenBy(c => c.Id);
            var filterdData = new PagedList<ExcelEmployeeData>(excelEmployeeData, Convert.ToInt32(page), Convert.ToInt32(pageSize));
            if (filterdData.Count > 0)
            {
                foreach (var item in excelEmployeeData)
                {
                    model.Add(new BulkUpload
                    {
                        Band = item.Band,
                        DateofJoining = item.DateofJoining,
                        Designation = item.Designation,
                        ErrorMessage = item.ErrorMessage,
                        FullName = item.FullName,
                        FunctionalDepartment = item.FunctionalDepartment,
                        FunctionalGroup = item.FunctionalGroup,
                        Location = item.Location,
                        PersonalEmailID = item.PersonalEmailID,
                        ReportingHeadEmailId = item.ReportingHeadEmailId,
                        ReportingHeadName = item.ReportingHeadName,
                        RollAccess = item.RollAccess,
                        Status = item.Status,
                        Title = item.Title,
                        WorkEmailAddress = item.WorkEmailAddress
                    });
                }
            }
            result.BulkUploadModel = model;
            result.TotalCount = model.Count;
            return result;
        }

        [HttpGet("candidateExcelUpload/{page?}/{pageSize?}/{name?}")]
        public IActionResult CandidateList(int? page = null, int? pageSize = null, string name = null)
        {
            var getList = this.getCandidateBulkDataList(Convert.ToInt32(page), Convert.ToInt32(pageSize), name);
            return Ok(getList);
        }
        private CandidateBulkViewModel getCandidateBulkDataList(int? page = null, int? pageSize = null, string name = null)
        {
            var result = new CandidateBulkViewModel();
            var model = new List<CandidateBulkModel>();
            var applicationType = "BulkUpload";
            var excelCandidateData = _unitOfWork.JobCandidateProfile.GetExcelCandidatesProfile(applicationType, name);
            //var excelCandidateData = _context.JobCandidateProfile.AsQueryable().Where(m => m.JobApplication.Contains(j=>j.));// == "Invalid");
            //if (!string.IsNullOrWhiteSpace(name) && name != "undefined")
            //    excelEmployeeData = excelEmployeeData.Where(c => c.FullName.Contains(name) || c.PersonalEmailID.Contains(name) || c.ReportingHeadEmailId.Contains(name)
            //    || c.WorkEmailAddress.Contains(name) || c.Status.Contains(name) || c.ErrorMessage.Contains(name));
            //excelEmployeeData = excelEmployeeData.OrderBy(c => c.FullName).ThenBy(c => c.Id);
            var filterdData = new PagedList<JobCandidateProfile>(excelCandidateData, Convert.ToInt32(page), Convert.ToInt32(pageSize));
            if (filterdData.Count > 0)
            {
                foreach (var item in excelCandidateData)
                {
                    model.Add(new CandidateBulkModel
                    {
                        CandidateName = item.Name,
                        Email = item.Email,
                        JobName = item.VacancyName,
                        JobTitle = item.VacancyName,
                        PhoneNumber = item.PhoneNo,
                        //LastInterviewedBy = ""
                    });
                }
            }
            result.CandidateBulkUploadModel = model;
            result.TotalCount = model.Count;
            return result;
        }

        [HttpGet("candidateExcelData")]
        public IActionResult ExcelCandidateData()
        {
            var candidateExcelList = GetExcelCandidateList();
            return Ok(candidateExcelList);
        }
        [HttpPost("updateCandidateExcelData")]
        public IActionResult UpdateExcelCandidateData(int id, string candidateName, string jobName, string jobTitle, string email, int level1ManagerId)
        {
            try
            {
                var candidateData = _unitOfWork.ExcelCandidateData.GetById(id);
                if (candidateData != null)
                {
                    candidateData.CandidateName = candidateName;
                    candidateData.JobName = jobName;
                    candidateData.JobTitle = jobTitle;
                    candidateData.Email = email;
                    candidateData.Level1ManagerId = level1ManagerId;
                    _unitOfWork.ExcelCandidateData.Update(candidateData);
                    _unitOfWork.SaveChanges();
                    return Ok();
                }
                return BadRequest(ModelState);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.GetBaseException().Message);
            }
        }
        private FileInfo getFilePath(IFormFile file, out string fileAddress)
        {
            string path = createFilePath("BulkUpload");
            string fileName = Path.GetExtension(file.FileName).ToString();
            string fullPath = Path.Combine(path, Path.GetFileNameWithoutExtension(file.FileName) + "_" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + fileName);
            fileAddress = fullPath;
            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                file.CopyTo(stream);
            }
            FileInfo fileInfo = new FileInfo(fullPath);
            return fileInfo;
        }
        private string createFilePath(string folderName)
        {
            var path = "";
            string rootFolder = _hostingEnvironment.WebRootPath;
            string filepath = Path.Combine(rootFolder, folderName);
            path = filepath;
            if (!Directory.Exists(filepath))
            {
                Directory.CreateDirectory(filepath);
            }
            return path;
        }

        private IList<ExcelCandidateData> GetExcelCandidateList()
        {
            var list = _unitOfWork.ExcelCandidateData.GetExcelCandidateData().ToList();
            return list;
        }
    }
}
