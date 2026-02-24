using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using A4.BAL;
using A4.BAL.ExpenseBooking;
using A4.DAL.Entites;
using A4.Empower.Helpers;
using DAL;
using DAL.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace A4.Empower.Controllers
{
    [Authorize(AuthenticationSchemes = OpenIddict.Validation.AspNetCore.OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    public class ExpenseBookingController : Controller
    {
        readonly ILogger _logger;
        readonly IUnitOfWork _unitOfWork;
        private IWebHostEnvironment _hostingEnvironment;
        private readonly IAccountManager _accountManager;
        readonly IEmailer _emailer;

        public ExpenseBookingController(IAccountManager accountManager, ILogger<ExpenseBookingController> logger, IEmailer emailer, IUnitOfWork unitOfWork, IWebHostEnvironment hostingEnvironment)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _accountManager = accountManager;
            _emailer = emailer;
            _hostingEnvironment = hostingEnvironment;
        }

        #region Expense DashBoard
        [HttpGet("list")]
        [Produces(typeof(ExpenseBookingViewModel))]
        public IActionResult GetAll()
        {
            try
            {
                var result = new ExpenseBookingViewModel();
                var category = new List<CategoryModel>();
                var model = _unitOfWork.ExpenseBookingRequest.GetAllCategories();
                if (model.Count > 0)
                {
                    foreach (var item in model)
                    {
                        category.Add(new CategoryModel { Id = item.Id.ToString(), Name = item.Name, SubCategoryList = GetAllSubCategory(item.Id) });
                    }

                }
                result.CategoryModel = category;
                result.CategoryCount = category.Count;
                return Ok(result);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.GetBaseException().Message);
            }
        }

        #endregion
        [HttpGet("requestList/{id}/{page?}/{pageSize?}/{name?}")]
        [Produces(typeof(ExpenseBookingRequestViewModel))]
        public IActionResult GetEmploeeRequest(string id, int? page = null, int? pageSize = null, string name = null)
        {
            try
            {

                string expensePeriod = "";
                var result = new ExpenseBookingRequestViewModel();
                var employee = _unitOfWork.Employee.Find(c => c.UserId == id && c.IsActive == true).FirstOrDefault();
                var model = _unitOfWork.ExpenseBookingRequest.GetEmployeeRequest(employee.Id, name, Convert.ToInt32(page), Convert.ToInt32(pageSize));
                result.ExpenseBookingListModel = GetRequestList(expensePeriod, model);
                result.ExpenseBookingCount = model.TotalCount;
                return Ok(result);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.GetBaseException().Message);
            }

        }

        [HttpGet("approveList/{id}/{page?}/{pageSize?}/{name?}")]
        [Produces(typeof(ExpenseBookingRequestViewModel))]
        public IActionResult GetApproveRequest(string id, int? page = null, int? pageSize = null, string name = null)
        {
            try
            {
                var result = new ExpenseBookingRequestViewModel();
                var request = new List<ExpenseBookingListModel>();
                var employee = _unitOfWork.Employee.Find(c => c.UserId == id && c.IsActive == true).FirstOrDefault();
                var model = _unitOfWork.ExpenseBookingRequest.GetApproveRequest(employee.Id, name, Convert.ToInt32(page), Convert.ToInt32(pageSize));
                if (model.Count > 0)
                {
                    foreach (var item in model)
                    {
                        request.Add(new ExpenseBookingListModel { 
                            BookingId = item.BookingId, 
                            ApprovedOrRejectedDate = item.ApprovedOrRejectedDate != "1/1/0001" ? item.ApprovedOrRejectedDate : "N/A",
                            RequestedDate = item.RequestedDate != "1/1/0001" ? item.RequestedDate : "N/A",
                            EmployeeName = item.EmployeeName,
                            IsInvite = item.IsInvite, Id = item.Id.ToString(), 
                            Amount = item.Amount, 
                            File = item.File != null ? GetDoumentFile(item.File) : "",
                            Status = item.Status,
                            ExpensePeriod = item.ExpensePeriod, 
                            Department = item.Department 
                        });
                    }
                }
                result.ExpenseBookingListModel = request;
                result.ExpenseBookingCount = model.TotalCount;
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.GetBaseException().Message);
            }

        }

        [HttpGet("allapproveList/{id}/{page?}/{pageSize?}/{name?}")]
        [Produces(typeof(ExpenseBookingRequestViewModel))]
        public IActionResult GetAllApproveRequest(string id, int? page = null, int? pageSize = null, string name = null)
        {
            try
            {
                var result = new ExpenseBookingRequestViewModel();
                var request = new List<ExpenseBookingListModel>();
                var employee = _unitOfWork.Employee.Find(c => c.UserId == id && c.IsActive == true).ToList().FirstOrDefault();
                var model = _unitOfWork.ExpenseBookingRequest.GetAllApproveRequest(employee.Id, name, Convert.ToInt32(page), Convert.ToInt32(pageSize));
                if (model.Count > 0)
                {
                    foreach (var item in model)
                    {
                        request.Add(
                            new ExpenseBookingListModel
                            {
                                BookingId = item.BookingId!=null? item.BookingId  :"0",
                                ApprovedOrRejectedDate = item.ApprovedOrRejectedDate != "1/1/0001" ? item.ApprovedOrRejectedDate : "N/A",
                                RequestedDate = item.RequestedDate != "1/1/0001" ? item.RequestedDate : "N/A",
                                EmployeeName = item.EmployeeName,
                                IsInvite = item.IsInvite,
                                Id = item.Id !=null ? item.Id.ToString() : "0",
                                Amount = item.Amount,
                                File = item.File != null ? GetDoumentFile(item.File) : "",
                                Status = item.Status,
                                ExpensePeriod = item.ExpensePeriod,
                                Department = item.Department
                            });
                    }
                }
                result.ExpenseBookingListModel = request;
                result.ExpenseBookingCount = model.TotalCount;
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.GetBaseException().Message);
            }

        }

        [HttpGet("approvedmanager/{id}")]
        [Produces(typeof(ExpenseBookingExcelViewModel))]
        public IActionResult GetApprovedListByManagerExcel(string id)
        {
            try
            {
                var data = new ExpenseBookingExcelViewModel();
                var result = new List<ExpenseBookingExcel>();
                var employee = _unitOfWork.Employee.Find(c => c.UserId == id && c.IsActive == true).FirstOrDefault();
                if (employee != null)
                {
                    var model = _unitOfWork.ExpenseBookingRequest.GetAllApproveExcelDataByManager(employee.Id);
                    foreach (var item in model)
                    {
                        result.Add(new ExpenseBookingExcel { BookingId = item.BookingId, Name = item.EmployeeName, Amount = item.Amount, ExpensePeriod = item.ExpensePeriod, Department = item.Department, ApprovedDate = item.ApprovedOrRejectedDate, RequestDate = item.RequestedDate });
                    }
                }
                data.ExpenseBookingExcel = result;
                return Ok(data);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.GetBaseException().Message);
            }

        }

        [HttpGet("approvedall")]
        [Produces(typeof(ExpenseBookingExcelViewModel))]
        public IActionResult GetApprovedListExcel()
        {
            try
            {
                var data = new ExpenseBookingExcelViewModel();
                var result = new List<ExpenseBookingExcel>();
                var model = _unitOfWork.ExpenseBookingRequest.GetAllApproveExcelData();
                foreach (var item in model)
                {
                    result.Add(new ExpenseBookingExcel { BookingId = item.BookingId !=null? item.BookingId:"", Name = item.EmployeeName, Amount = item.Amount, ExpensePeriod = item.ExpensePeriod, Department = item.Department, ApprovedDate = item.ApprovedOrRejectedDate, RequestDate = item.RequestedDate });
                }

                data.ExpenseBookingExcel = result;
                return Ok(data);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.GetBaseException().Message);
            }

        }

        [HttpGet("getAddRequest/{id}")]
        [Produces(typeof(ExpenseBookingModel))]
        public IActionResult GetAddRequest(string id)
        {
            var result = new ExpenseBookingModel();
            result.DepartmentList = GetDepartmentList();
            result.SubCategoryItems = GetSubCategoryItemList(id);
            var subCategoryObject = _unitOfWork.SubCategory.Get(Guid.Parse(id));
            if (subCategoryObject != null)
            {
                result.SubCategory = subCategoryObject.Name;
                var categoryObject = _unitOfWork.Category.Get(subCategoryObject.ExpenseBookingCategoryId);
                if (categoryObject != null)
                {
                    result.Category = categoryObject.Name;
                }
            }
            result.Status = "New";
            return Ok(result);
        }

        [HttpPost("addRequest/{id}")]
        public IActionResult AddRequest(string id, [FromBody] ExpenseBookingModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (model == null)
                        return BadRequest($"{nameof(model)} cannot be null");
                    if (string.IsNullOrEmpty(model.SubCategoryItemId))
                        return BadRequest(" SubCategoryItemId cannot be null");
                    var employee = _unitOfWork.Employee.Find(c => c.UserId == id && c.IsActive == true).ToList().FirstOrDefault();
                    var Model = new ExpenseBookingRequest()
                    {
                        ExpenseBookingSubCategoryItemId = new Guid(model.SubCategoryItemId),
                        FromDate = model.FromDate,
                        ToDate = model.ToDate,
                        Amount = model.Amount,
                        EmployeeId = employee.Id,
                        DepartmentID = new Guid(model.DepartmentId),
                        //File = model.File,
                        Status = (int)ExpenseStatus.Saved

                    };
                    _unitOfWork.ExpenseBookingRequest.Add(Model);
                    _unitOfWork.SaveChanges();
                    List<ExpenseBookingApprover> approverList = new List<ExpenseBookingApprover>();
                    var managerTitle = _unitOfWork.Employee.Get(employee.ManagerId);
                    var managerIds = GetMangerListForApprovel(model.Amount, employee.ManagerId, managerTitle != null ? managerTitle.TitleId : Guid.NewGuid(), employee.Id);
                    if (managerIds.Count > 0)
                    {

                        foreach (var item in managerIds)
                        {
                            approverList.Add(
                                new ExpenseBookingApprover
                                {
                                    ExpenseBookingRequestId = Model.Id,
                                    Level = item.Level,
                                    IsAllow = item.Level == 0 ? true : false,
                                    ManagerId = item.ManagerId,
                                    Status = (int)ExpenseStatus.Pending
                                });
                        }
                        _unitOfWork.ExpenseBookingApprover.AddRange(approverList);
                        _unitOfWork.SaveChanges();
                        List<ExpenseBookingRequestDetail> expenseBookingRequestDetails = new List<ExpenseBookingRequestDetail>();
                        foreach (var item in approverList)
                        {
                            expenseBookingRequestDetails.Add(
                                new ExpenseBookingRequestDetail
                                {
                                    ExpenseBookingApproverId = item.Id,
                                    EmployeeComment = model.Remarks,
                                    ApproverComment = "",
                                    IsNew = true,
                                    IsActive = true
                                });
                        }
                        _unitOfWork.ExpenseBookingRequestDetail.AddRange(expenseBookingRequestDetails);

                    }
                    if (model.File != null && model.File.Count > 0)
                    {
                        List<ExpenseDocument> expenseDocumentList = new List<ExpenseDocument>();
                        foreach (var item in model.File)
                        {
                            expenseDocumentList.Add(new ExpenseDocument { PictureId = Convert.ToInt32(item), ExpenseBookingId = Model.Id });
                        }
                        _unitOfWork.ExpenseDocument.AddRange(expenseDocumentList);
                    }
                    _unitOfWork.SaveChanges();
                    return NoContent();
                }
                return BadRequest(ModelState);

            }
            catch (Exception ex)
            {

                return BadRequest(ex.GetBaseException().Message);
            }
        }

        [HttpPut("update/{id}")]
        public IActionResult UpdateRequest(string id, [FromBody] ExpenseBookingModel model)
        {
            try
            {
                if (id != null)
                {
                    if (ModelState.IsValid)
                    {
                        if (model == null)
                            return BadRequest($"{nameof(model)} cannot be null");
                        var checkRequest = _unitOfWork.ExpenseBookingRequest.GetById(Convert.ToInt32(id));
                        if (checkRequest != null)
                        {
                            checkRequest.ExpenseBookingSubCategoryItemId = new Guid(model.SubCategoryItemId);
                            checkRequest.FromDate = model.FromDate;
                            checkRequest.ToDate = model.ToDate;
                            checkRequest.Amount = model.Amount;
                            checkRequest.Status = (int)ExpenseStatus.Saved;

                            checkRequest.DepartmentID = new Guid(model.DepartmentId);
                            _unitOfWork.ExpenseBookingRequest.Update(checkRequest);
                            var exbookDetail = _unitOfWork.ExpenseBookingRequestDetail.Find(m => m.ExpenseBookingApproverId == Guid.Parse(model.ApproverId) && m.IsNew == true).FirstOrDefault();
                            if (exbookDetail != null)
                            {
                                exbookDetail.EmployeeComment = model.Remarks;
                                _unitOfWork.ExpenseBookingRequestDetail.Update(exbookDetail);
                            }
                            else
                            {
                                var employeeBookingDetail = new ExpenseBookingRequestDetail();
                                employeeBookingDetail.EmployeeComment = model.Remarks;
                                employeeBookingDetail.ExpenseBookingApproverId = Guid.Parse(model.ApproverId);
                                employeeBookingDetail.IsNew = true;
                                _unitOfWork.ExpenseBookingRequestDetail.Add(employeeBookingDetail);
                            }
                            if (model.File != null && model.File.Count > 0)
                            {
                                List<ExpenseDocument> expenseDocumentList = new List<ExpenseDocument>();
                                foreach (var item in model.File)
                                {
                                    expenseDocumentList.Add(new ExpenseDocument { PictureId = Convert.ToInt32(item), ExpenseBookingId = checkRequest.Id });
                                }
                                _unitOfWork.ExpenseDocument.AddRange(expenseDocumentList);
                            }
                            _unitOfWork.SaveChanges();
                            return NoContent();
                        }
                        return NotFound("checkRequest cannot be null");
                    }
                }
                return BadRequest("id cannot be null");
            }
            catch (Exception ex)
            {

                return BadRequest(ex.GetBaseException().Message);
            }
        }

        [HttpGet("viewRequest/{id}")]
        public IActionResult ViewRequest(string id)
        {
            try
            {
                if (id != null)
                {
                    if (!string.IsNullOrEmpty(id))
                    {
                        var checkRequest = _unitOfWork.ExpenseBookingRequest.GetRequest(Convert.ToInt32(id));
                        if (checkRequest != null)
                        {
                            var viewModel = new ExpenseBookingModel();
                            viewModel.Id = checkRequest.Id.ToString();
                            viewModel.FromDate = checkRequest.FromDate;
                            viewModel.ToDate = checkRequest.ToDate;
                            viewModel.Amount = checkRequest.Amount;
                            viewModel.Status = checkRequest.Status.ToString();
                            viewModel.DepartmentId = checkRequest.DepartmentID.ToString();
                            viewModel.SubCategoryItemId = checkRequest.ExpenseBookingSubCategoryItemId.ToString();
                            var result = GetCategorySubCategoryName(checkRequest.ExpenseBookingSubCategoryItemId);
                            viewModel.Category = result.Item1;
                            viewModel.SubCategory = result.Item2;
                            viewModel.SubCategoryItem = checkRequest.ExpenseBookingSubCategoryItem.Name;
                            viewModel.Department = checkRequest.FunctionalDepartment.Name;
                            viewModel.DepartmentList = GetDepartmentList();
                            var approverList = _unitOfWork.ExpenseBookingApprover.Find(m => m.ExpenseBookingRequestId == checkRequest.Id).ToList();
                            if (approverList.Count() > 0)
                            {
                                var expenseBookingApproverModals = new List<ExpenseBookingApproverModal>();
                                foreach (var item in approverList)
                                {
                                    expenseBookingApproverModals.Add(
                                        new ExpenseBookingApproverModal
                                        {
                                            Id = item.Id.ToString(),
                                            Level = item.Level,
                                            Status = System.Enum.GetName(typeof(ExpenseStatus), item.Status),
                                            ExpenseBookingDetailApproverList = GetBookingDetailApprover(item.Id),
                                            ExpenseBookingIviteApproverList = GetBookingDetailApproverInvite(item.Id)
                                        });
                                    if (item.IsAllow == true)
                                    {
                                        viewModel.ApproverId = item.Id.ToString();
                                    }
                                }
                                viewModel.ExpenseBookingDetail = expenseBookingApproverModals;
                            }
                            var itemlist = _unitOfWork.ExpenseBookingRequest.GetSubCategoryId(checkRequest.ExpenseBookingSubCategoryItemId);
                            viewModel.SubCategoryItems = GetSubCategoryItemList(itemlist.ExpenseBookingSubCategoryId.ToString());

                            var documentList = _unitOfWork.ExpenseDocument.Find(m => m.ExpenseBookingId == checkRequest.Id);
                            if (documentList.Count() > 0)
                            {

                                var listModel = new List<ExpenseDocumentList>();
                                foreach (var item in documentList)
                                {
                                    var docFile = GetExpenseDoumentFile(item.PictureId.ToString());
                                    listModel.Add(new ExpenseDocumentList { ExpenseDocumentId = item.Id.ToString(), FileUrl = docFile.Item1, FileName = docFile.Item2 });
                                }
                            }
                            return Ok(viewModel);
                        }
                        else
                        {
                            return NotFound(id);
                        }
                    }
                }
                return BadRequest("id cannot be null");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.GetBaseException().Message);
            }
        }

        [HttpGet("viewRequestApprover/{id}")]
        public IActionResult ViewRequestApprover(string id)
        {
            try
            {
                if (id != null)
                {
                    if (!string.IsNullOrEmpty(id))
                    {
                        var checkRequest = _unitOfWork.ExpenseBookingRequest.GetRequest(Convert.ToInt32(id));
                        if (checkRequest != null)
                        {
                            var viewModel = new ExpenseBookingModel();
                            viewModel.Id = checkRequest.Id.ToString();
                            viewModel.FromDate = checkRequest.FromDate;
                            viewModel.ToDate = checkRequest.ToDate;
                            viewModel.Amount = checkRequest.Amount;
                            viewModel.Status = checkRequest.Status.ToString();
                            viewModel.DepartmentId = checkRequest.DepartmentID.ToString();
                            var result = GetCategorySubCategoryName(checkRequest.ExpenseBookingSubCategoryItemId);
                            viewModel.Category = result.Item1;
                            viewModel.SubCategory = result.Item2;
                            viewModel.SubCategoryItemId = checkRequest.ExpenseBookingSubCategoryItemId.ToString();
                            viewModel.SubCategoryItem = checkRequest.ExpenseBookingSubCategoryItem.Name;
                            viewModel.Department = checkRequest.FunctionalDepartment.Name;
                            viewModel.DepartmentList = GetDepartmentList();
                            var approverList = _unitOfWork.ExpenseBookingApprover.Find(m => m.ExpenseBookingRequestId == checkRequest.Id);
                            if (approverList.Count() > 0)
                            {
                                approverList = approverList.ToList();

                                var expenseBookingApproverModals = new List<ExpenseBookingApproverModal>();
                                foreach (var item in approverList)
                                {
                                    expenseBookingApproverModals.Add(new ExpenseBookingApproverModal { Name = EmployeeName(item.ManagerId), Id = item.Id.ToString(), Level = item.Level, Status = System.Enum.GetName(typeof(ExpenseStatus), item.Status), ExpenseBookingDetailApproverList = GetBookingDetailApprover(item.Id), ExpenseBookingIviteApproverList = GetBookingDetailApproverInvite(item.Id) });
                                    if (item.IsAllow == true)
                                    {
                                        viewModel.ApproverId = item.Id.ToString();
                                    }
                                }
                                viewModel.ExpenseBookingDetail = expenseBookingApproverModals;
                            }
                            if (viewModel.ApproverId != null)
                            {
                                var aaproverDetail = _unitOfWork.ExpenseBookingRequestDetail.Find(m => m.ExpenseBookingApproverId == Guid.Parse(viewModel.ApproverId) && m.IsNew == true).ToList().FirstOrDefault();
                                if (aaproverDetail != null)
                                {
                                    viewModel.Remarks = aaproverDetail.EmployeeComment;
                                }
                            }
                            //CHeck Invite Aprroved:-

                            if (viewModel.ApproverId == null)
                            {
                                var approverManager = _unitOfWork.ExpenseBookingApprover.Find(m => m.ExpenseBookingRequestId == checkRequest.Id && m.Level == 0).FirstOrDefault();
                                if (approverManager != null)
                                {
                                    var inviteApproved = _unitOfWork.ExpenseBookingInviteApprover.Find(m => m.ExpenseBookingApproverId == approverManager.ManagerId);
                                    if (inviteApproved != null)
                                    {
                                        var check = inviteApproved.Any(m => m.Status == 5);
                                        if (check)
                                        {
                                            viewModel.IsInviteApproved = false;
                                        }
                                    }
                                }

                            }
                            else
                            {

                                var inviteApproved = _unitOfWork.ExpenseBookingInviteApprover.Find(m => m.ExpenseBookingApproverId == Guid.Parse(viewModel.ApproverId)).ToList();
                                if (inviteApproved != null)
                                {
                                    var check = inviteApproved.Any(m => m.Status == 5);
                                    if (check)
                                    {
                                        viewModel.IsInviteApproved = false;
                                    }
                                }
                            }
                            var itemlist = _unitOfWork.ExpenseBookingRequest.GetSubCategoryId(checkRequest.ExpenseBookingSubCategoryItemId);
                            viewModel.SubCategoryItems = GetSubCategoryItemList(itemlist.ExpenseBookingSubCategoryId.ToString());

                            var employeeList = GetInviteEmployeeList().Where(m => !approverList.ToList().Any(p2 => p2.ManagerId.ToString() == m.Value)).ToList();
                            viewModel.InviteEmployeeList = employeeList.Where(m => m.Value != checkRequest.EmployeeId.ToString()).ToList();

                            var documentList = _unitOfWork.ExpenseDocument.Find(m => m.ExpenseBookingId == checkRequest.Id);
                            if (documentList.Count() > 0)
                            {

                                var listModel = new List<ExpenseDocumentList>();
                                foreach (var item in documentList)
                                {
                                    var docFile = GetExpenseDoumentFile(item.PictureId.ToString());
                                    listModel.Add(new ExpenseDocumentList { ExpenseDocumentId = item.Id.ToString(), FileUrl = docFile.Item1, FileName = docFile.Item2 });
                                }
                                viewModel.ExpenseDocumentList = listModel;
                            }
                            return Ok(viewModel);
                        }
                        else
                        {
                            return NotFound(id);
                        }
                    }
                }
                return BadRequest("id cannot be null");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.GetBaseException().Message);
            }
        }

        [HttpPut("approveReject/{id}")]
        public async Task<IActionResult> ApproveReject(string id, [FromBody] ExpenseBookingStatusModel model)
        {
            try
            {
                if (id != null)
                {
                    if (!string.IsNullOrEmpty(id))
                    {

                        var approver = _unitOfWork.ExpenseBookingApprover.Get(new Guid(id));
                        if (approver != null)
                        {
                            var checkRequest = _unitOfWork.ExpenseBookingRequest.GetById(approver.ExpenseBookingRequestId);
                            if (checkRequest != null)
                            {
                                var requestedBy = _unitOfWork.Employee.GetEmployeedetails(checkRequest.EmployeeId);
                                var exbookDetail = _unitOfWork.ExpenseBookingRequestDetail.Find(m => m.ExpenseBookingApproverId == approver.Id && m.IsNew == true).FirstOrDefault();
                                if (exbookDetail != null)
                                {
                                    exbookDetail.ApproverComment = model.Comment;
                                    exbookDetail.IsNew = false;
                                    _unitOfWork.ExpenseBookingRequestDetail.Update(exbookDetail);
                                }
                                if (model.ButtonType == 1)
                                {
                                    approver.Status = (int)ExpenseStatus.Approved;
                                    approver.IsAllow = false;
                                    var nextApprover = _unitOfWork.ExpenseBookingApprover.Find(m => m.IsAllow == false && m.Level == approver.Level + 1 && m.ExpenseBookingRequestId == approver.ExpenseBookingRequestId).FirstOrDefault();
                                    if (nextApprover != null)
                                    {
                                        nextApprover.IsAllow = true;
                                        _unitOfWork.ExpenseBookingApprover.Update(nextApprover);
                                        //Send Email TO Next Manager

                                        var nextApproverDetail = _unitOfWork.Employee.GetEmployeedetails(nextApprover.ManagerId);
                                        if (nextApproverDetail != null)
                                        {
                                            string message = ExpenseBookingTemplates.ManagerToManager(nextApproverDetail.ApplicationUser.FullName, requestedBy.ApplicationUser.FullName, checkRequest.RequestDate.ToShortDateString());
                                            (bool success, string errorMsg) response = await _emailer.SendEmailAsync(nextApproverDetail.ApplicationUser.FullName, nextApproverDetail.ApplicationUser.Email, "Expense Request Status", message);
                                        }

                                    }
                                    else
                                    {
                                        checkRequest.Status = (int)ExpenseStatus.Approved;
                                        checkRequest.ApprovedOrRejectedDate = DateTime.Now;
                                        if (requestedBy != null)
                                        {
                                            string message = ExpenseBookingTemplates.ManagerToEmployee(requestedBy.ApplicationUser.FullName, System.Enum.GetName(typeof(ExpenseStatus), checkRequest.Status));
                                            (bool success, string errorMsg) response = await _emailer.SendEmailAsync(requestedBy.ApplicationUser.FullName, requestedBy.ApplicationUser.Email, "Expense Request Status", message);
                                        }
                                    }
                                }
                                else if (model.ButtonType == 2)
                                {
                                    approver.Status = (int)ExpenseStatus.Rejected;
                                    checkRequest.Status = (int)ExpenseStatus.Rejected;
                                    checkRequest.ApprovedOrRejectedDate = DateTime.Now;
                                    if (requestedBy != null)
                                    {
                                        string message = ExpenseBookingTemplates.ManagerToEmployee(requestedBy.ApplicationUser.FullName, System.Enum.GetName(typeof(ExpenseStatus), checkRequest.Status));
                                        (bool success, string errorMsg) response = await _emailer.SendEmailAsync(requestedBy.ApplicationUser.FullName, requestedBy.ApplicationUser.Email, "Expense Request Status", message);
                                    }
                                }
                                else if (model.ButtonType == 3)
                                {
                                    approver.Status = (int)ExpenseStatus.Resubmit;
                                    checkRequest.Status = (int)ExpenseStatus.Resubmit;
                                    checkRequest.ApprovedOrRejectedDate = DateTime.Now;
                                    if (requestedBy != null)
                                    {
                                        string message = ExpenseBookingTemplates.ManagerToEmployee(requestedBy.ApplicationUser.FullName, System.Enum.GetName(typeof(ExpenseStatus), checkRequest.Status));
                                        (bool success, string errorMsg) response = await _emailer.SendEmailAsync(requestedBy.ApplicationUser.FullName, requestedBy.ApplicationUser.Email, "Expense Request Status", message);
                                    }
                                }
                                _unitOfWork.ExpenseBookingApprover.Update(approver);
                                _unitOfWork.ExpenseBookingRequest.Update(checkRequest);
                                _unitOfWork.SaveChanges();
                                return NoContent();
                            }
                            else
                            {
                                return NotFound(id);
                            }
                        }
                        else
                        {
                            return NotFound(id);
                        }
                    }
                }
                return BadRequest("id cannot be null");
            }
            catch (Exception ex)
            {

                return BadRequest(ex.GetBaseException().Message);
            }
        }

        [HttpPut("inviteApproveReject/{id}/{employeeId}")]
        public async Task<IActionResult> InviteApproveReject(string id, string employeeId, [FromBody] ExpenseBookingStatusModel model)
        {
            try
            {
                if (id != null && employeeId != null)
                {
                    if (!string.IsNullOrEmpty(id))
                    {
                        var empId = _unitOfWork.Employee.Find(m => m.UserId == employeeId).FirstOrDefault();
                        if (empId != null)
                        {
                            var bookingApprover = _unitOfWork.ExpenseBookingApprover.Get(Guid.Parse(id));
                            var chkRequest = _unitOfWork.ExpenseBookingRequest.GetById(bookingApprover.ExpenseBookingRequestId);
                            var inviteApprover = _unitOfWork.ExpenseBookingInviteApprover.Find(m => m.ExpenseBookingApproverId == Guid.Parse(id) && m.EmployeeId == empId.Id).ToList().FirstOrDefault();
                            if (inviteApprover != null)
                            {
                                var exbookDetail = _unitOfWork.ExpenseBookingRequestDetailInvite.Find(m => m.ExpenseBookingInviteApproverId == inviteApprover.Id && m.IsNew == true).FirstOrDefault();
                                if (exbookDetail != null)
                                {
                                    exbookDetail.ApproverComment = model.Comment;
                                    exbookDetail.IsNew = false;
                                    _unitOfWork.ExpenseBookingRequestDetailInvite.Update(exbookDetail);
                                }
                                if (model.ButtonType == 1)
                                {
                                    inviteApprover.Status = (int)ExpenseStatus.Approved;
                                    var requestedBy = _unitOfWork.Employee.GetEmployeedetails(chkRequest.EmployeeId);
                                    var bookingApproverManager = _unitOfWork.Employee.GetEmployeedetails(bookingApprover.ManagerId);
                                    var inviteEmployeeName = _unitOfWork.Employee.GetEmployeedetails(inviteApprover.EmployeeId);
                                    if (requestedBy != null)
                                    {
                                        string message = ExpenseBookingTemplates.InviteEmployeeToManager(bookingApproverManager.ApplicationUser.FullName, requestedBy.ApplicationUser.FullName, System.Enum.GetName(typeof(ExpenseStatus), inviteApprover.Status), inviteEmployeeName != null ? inviteEmployeeName.ApplicationUser.FullName : "N/A");
                                        (bool success, string errorMsg) response = await _emailer.SendEmailAsync(bookingApproverManager.ApplicationUser.FullName, bookingApproverManager.ApplicationUser.Email, "Expense Request Status", message);
                                    }
                                }
                                else if (model.ButtonType == 2)
                                {
                                    inviteApprover.Status = (int)ExpenseStatus.Rejected;

                                    if (bookingApprover != null)
                                    {
                                        if (chkRequest != null)
                                        {
                                            chkRequest.Status = (int)ExpenseStatus.Rejected;
                                            chkRequest.ApprovedOrRejectedDate = DateTime.Now;
                                            _unitOfWork.ExpenseBookingRequest.Update(chkRequest);

                                            var requestedBy = _unitOfWork.Employee.GetEmployeedetails(chkRequest.EmployeeId);
                                            if (requestedBy != null)
                                            {
                                                string message = ExpenseBookingTemplates.ManagerToEmployee(requestedBy.ApplicationUser.FullName, System.Enum.GetName(typeof(ExpenseStatus), chkRequest.Status));
                                                (bool success, string errorMsg) response = await _emailer.SendEmailAsync(requestedBy.ApplicationUser.FullName, requestedBy.ApplicationUser.Email, "Expense Request Status", message);
                                            }
                                        }
                                    }
                                }
                                //else if (model.ButtonType == 3)
                                //{
                                //    approver.Status = (int)ExpenseStatus.Resubmit;
                                //    checkRequest.Status = (int)ExpenseStatus.Resubmit;
                                //}
                                _unitOfWork.ExpenseBookingInviteApprover.Update(inviteApprover);
                                _unitOfWork.SaveChanges();
                                //ToDo: Send EMail to Reportee
                                //var empDetail = _unitOfWork.Employee.GetEmployeedetails(checkRequest.EmployeeId);
                                //if (empDetail != null)
                                //{
                                //    string message = ExpenseBookingTemplates.ApproveRejectRequestToEmployee(empDetail.ApplicationUser.FullName, System.Enum.GetName(typeof(ExpenseStatus), 2));
                                //    (bool success, string errorMsg) response = await _emailer.SendEmailAsync(empDetail.ApplicationUser.FullName, empDetail.ApplicationUser.Email, "Expense Request Status", message);
                                //}
                                return NoContent();
                            }
                            else
                            {
                                return NotFound(id);
                            }
                        }
                        else
                        {
                            return NotFound(employeeId);
                        }

                    }
                }
                return BadRequest("id cannot be null");
            }
            catch (Exception ex)
            {

                return BadRequest(ex.GetBaseException().Message);
            }
        }

        [HttpPut("inviteApprover/{id}")]
        public async Task<IActionResult> InviteApprover(string id, [FromBody] InviteApproverModel model)
        {
            try
            {
                if (id != null)
                {
                    if (!string.IsNullOrEmpty(id))
                    {
                        var approver = _unitOfWork.ExpenseBookingApprover.Get(new Guid(id));
                        if (approver != null)
                        {
                            if (model.SelectedApprover != null && model.SelectedApprover.Count > 0)
                            {
                                var exbookDetail = _unitOfWork.ExpenseBookingRequestDetail.Find(m => m.ExpenseBookingApproverId == approver.Id && m.IsNew == true).FirstOrDefault();
                                List<ExpenseBookingInviteApprover> approverList = new List<ExpenseBookingInviteApprover>();
                                foreach (var item in model.SelectedApprover)
                                {
                                    var checkExist = _unitOfWork.ExpenseBookingInviteApprover.Find(m => m.ExpenseBookingApproverId == approver.Id && m.EmployeeId == Guid.Parse(item)).FirstOrDefault();
                                    if (checkExist == null)
                                    {
                                        approverList.Add(new ExpenseBookingInviteApprover { IsActive = true, ExpenseBookingApproverId = approver.Id, EmployeeId = Guid.Parse(item), Status = (int)ExpenseStatus.Pending });
                                    }
                                }
                                _unitOfWork.ExpenseBookingInviteApprover.AddRange(approverList);
                                _unitOfWork.SaveChanges();
                                List<ExpenseBookingRequestDetailInvite> expenseBookingRequestDetails = new List<ExpenseBookingRequestDetailInvite>();
                                foreach (var item in approverList)
                                {

                                    expenseBookingRequestDetails.Add(new ExpenseBookingRequestDetailInvite { ExpenseBookingInviteApproverId = item.Id, EmployeeComment = exbookDetail != null ? exbookDetail.EmployeeComment : "", ApproverComment = "", IsNew = true, IsActive = true });
                                }
                                _unitOfWork.ExpenseBookingRequestDetailInvite.AddRange(expenseBookingRequestDetails);
                                _unitOfWork.SaveChanges();

                                //Send Email To each Approver:
                                var checkRequest = _unitOfWork.ExpenseBookingRequest.GetById(approver.ExpenseBookingRequestId);
                                var requestedBy = _unitOfWork.Employee.GetEmployeedetails(checkRequest.EmployeeId);
                                foreach (var item in approverList)
                                {
                                    var inviteEmployee = _unitOfWork.Employee.GetEmployeedetails(item.EmployeeId);
                                    if (requestedBy != null)
                                    {
                                        string message = ExpenseBookingTemplates.ManagerToInviteEmployee(inviteEmployee.ApplicationUser.FullName, requestedBy.ApplicationUser.FullName);
                                        (bool success, string errorMsg) response = await _emailer.SendEmailAsync(inviteEmployee.ApplicationUser.FullName, inviteEmployee.ApplicationUser.Email, "Invite for Expense Booking Request", message);
                                    }
                                }
                                if (approverList.Count() == 0)
                                {
                                    return NotFound("Invitation already sent.");
                                }
                            }

                            return NoContent();

                        }
                        else
                        {
                            return NotFound(id);
                        }
                    }
                }
                return BadRequest("id cannot be null");
            }
            catch (Exception ex)
            {

                return BadRequest(ex.GetBaseException().Message);
            }
        }

        [HttpDelete("deleteRequest/{id}")]
        public IActionResult DeleteRequest(string id)
        {
            try
            {
                if (id != null)
                {
                    if (!string.IsNullOrEmpty(id))
                    {
                        var checkexist = _unitOfWork.ExpenseBookingRequest.GetById(Convert.ToInt32(id));
                        if (checkexist != null)
                        {
                            _unitOfWork.ExpenseBookingRequest.Remove(checkexist);
                            return NoContent();
                        }
                        else
                        {
                            return NotFound(id);
                        }
                    }
                }
                return BadRequest("id cannot be null");
            }
            catch (Exception ex)
            {

                return BadRequest(ex.GetBaseException().Message);
            }
        }

        [HttpDelete("deletedocument/{id}")]
        public IActionResult DeleteExpenseDocument(string id)
        {
            try
            {
                if (id != null)
                {
                    if (!string.IsNullOrEmpty(id))
                    {
                        var checkexist = _unitOfWork.ExpenseDocument.Get(Guid.Parse(id));
                        if (checkexist != null)
                        {
                            _unitOfWork.ExpenseDocument.Remove(checkexist);

                            var picture = _unitOfWork.FileUpload.GetPictureById(checkexist.PictureId);
                            if (picture != null)
                            {
                                _unitOfWork.FileUpload.Remove(picture);
                            }
                            _unitOfWork.SaveChanges();
                            return NoContent();
                        }
                        else
                        {
                            return NotFound(id);
                        }
                    }
                }
                return BadRequest("id cannot be null");
            }
            catch (Exception ex)
            {

                return BadRequest(ex.GetBaseException().Message);
            }
        }

        [HttpGet("submitRequest/{id}")]
        public async Task<IActionResult> SubmitRequest(string id)
        {
            try
            {
                if (id != null)
                {
                    var check = _unitOfWork.ExpenseBookingRequest.GetById(Convert.ToInt32(id));
                    if (check != null)
                    {
                        check.Status = (int)ExpenseStatus.Submitted;
                        check.RequestDate = DateTime.Now;
                        _unitOfWork.ExpenseBookingRequest.Update(check);
                        _unitOfWork.SaveChanges();

                        //TODO:Email Functionality to manager
                        var empDetail = _unitOfWork.Employee.GetEmployeedetails(check.EmployeeId);
                        if (empDetail != null)
                        {
                            string message = ExpenseBookingTemplates.EmployeeToManager(empDetail.ManagerName, empDetail.ApplicationUser.FullName, check.RequestDate.ToShortDateString());
                            (bool success, string errorMsg) response = await _emailer.SendEmailAsync(empDetail.ApplicationUser.FullName, empDetail.ManagerEmail, "Expense Request Booking", message).ConfigureAwait(false);

                        }
                        return NoContent();
                    }
                }
                return BadRequest("id cannot be null");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.GetBaseException().Message);
            }
        }

        private List<SubCategoryModel> GetAllSubCategory(Guid id)
        {
            var category = new List<SubCategoryModel>();
            var categoryList = _unitOfWork.ExpenseBookingRequest.GetAllSubCategories(id);
            if (categoryList.Count > 0)
            {
                foreach (var item in categoryList)
                {
                    category.Add(new SubCategoryModel { Id = item.Id.ToString(), Name = item.Name });
                }
            }
            return category;
        }

        private List<DropDownList> GetDepartmentList()
        {
            var departmentList = new List<DropDownList>();
            var department = _unitOfWork.Department.Find(m => m.IsActive).ToList();
            if (department.Count > 0)
            {
                foreach (var item in department)
                {
                    departmentList.Add(new DropDownList { Value = item.Id.ToString(), Label = item.Name });
                }
            }
            return departmentList;
        }

        private List<DropDownList> GetSubCategoryItemList(string id)
        {
            var categoryItemList = new List<DropDownList>();
            var categoryItems = _unitOfWork.ExpenseBookingRequest.GetAllItem(new Guid(id));
            if (categoryItems.Count > 0)
            {
                foreach (var item in categoryItems)
                {
                    categoryItemList.Add(new DropDownList { Label = item.Name, Value = item.Id.ToString() });
                }
            }
            return categoryItemList;
        }

        private List<DropDownList> GetInviteEmployeeList()
        {
            var employeeList = new List<DropDownList>();
            var managerList = _unitOfWork.Employee.GetManagerList();
            foreach (var manager in managerList)
            {
                employeeList.Add(new DropDownList { Label = manager.FullName, Value = manager.Id.ToLower().ToString() });
            }
            return employeeList;
        }

        private string GetDoumentFile(string fileId)
        {
            string file = "";
            if (fileId != null && fileId != "")
            {
                var picture = _unitOfWork.FileUpload.GetPictureById(Convert.ToInt32(fileId));
                file = _unitOfWork.FileUpload.GetDocumentUrl(picture, _hostingEnvironment.WebRootPath);
            }
            return file;
        }

        private Tuple<string, string> GetExpenseDoumentFile(string fileId)
        {
            string url = "";
            string fileName = "";
            if (fileId != null && fileId != "")
            {
                var picture = _unitOfWork.FileUpload.GetPictureById(Convert.ToInt32(fileId));
                fileName = picture.AltAttribute;
                url = _unitOfWork.FileUpload.GetDocumentUrl(picture, _hostingEnvironment.WebRootPath);
            }
            return Tuple.Create(url, fileName);
        }

        private Tuple<string, string> GetUserName(Guid empId, Guid mgrId)
        {

            var name = "";
            var managerName = "";
            var userId = _unitOfWork.Employee.Get(empId);
            if (userId != null)
            {

                var appUser = this._accountManager.GetUserByIdAsync(userId.UserId).Result;
                if (appUser != null)
                {
                    name = appUser.FullName;
                }
            }
            var userIdManager = _unitOfWork.Employee.Get(mgrId);
            if (userIdManager != null)
            {

                var appUserManager = this._accountManager.GetUserByIdAsync(userIdManager.UserId).Result;
                if (appUserManager != null)
                {
                    managerName = appUserManager.FullName;
                }
            }
            return Tuple.Create(name, managerName);
        }

        private List<ExpenseBookingListModel> GetRequestList(string expensePeriod, DAL.PagedList<ExpenseBookingRequest> model)
        {
            var request = new List<ExpenseBookingListModel>();
            if (model.Count > 0)
            {
                foreach (var item in model)
                {
                    var department = _unitOfWork.ExpenseBookingRequest.GetDepartment(item.DepartmentID);
                    expensePeriod = item.FromDate.ToShortDateString() + " To " + item.ToDate.ToShortDateString();
                    request.Add(new ExpenseBookingListModel { BookingId = item.BookingId, ApprovedOrRejectedDate = item.ApprovedOrRejectedDate.ToShortDateString() != "1/1/0001" ? item.ApprovedOrRejectedDate.ToShortDateString() : "N/A", RequestedDate = item.RequestDate.ToShortDateString() != "1/1/0001" ? item.RequestDate.ToShortDateString() : "N/A", Id = item.Id.ToString(), Amount = item.Amount, File = item.File != null ? GetDoumentFile(item.File) : "", Status = System.Enum.GetName(typeof(ExpenseStatus), item.Status), ExpensePeriod = expensePeriod, Department = department.FunctionalDepartment.Name });
                }
            }

            return request;
        }

        private List<ManagerLevel> GetMangerListForApprovel(string amount, Guid managerId, Guid titleId, Guid employeeId)
        {
            List<ManagerLevel> managerList = new List<ManagerLevel>();
            var result = _unitOfWork.ExpenseBookingTitleAmount.Find(m => m.TitleID == titleId).FirstOrDefault();
            if (result != null && Convert.ToInt32(result.Amount) < Convert.ToInt32(amount))
            {
                var managerLevelList = _unitOfWork.ExpenseBookingRequest.GetUserManageLevelList(employeeId, managerId);
                if (managerLevelList != null && managerLevelList.Count > 0)
                {
                    foreach (var item in managerLevelList)
                    {
                        var titleIDEMployee = _unitOfWork.Employee.Get(item.ManagerID).TitleId;
                        var resultTitle = _unitOfWork.ExpenseBookingTitleAmount.Find(m => m.TitleID == titleIDEMployee).FirstOrDefault();
                        if (resultTitle != null)
                        {
                            if (Convert.ToInt32(resultTitle.Amount) < Convert.ToInt32(amount))
                            {
                                managerList.Add(new ManagerLevel { Level = item.EmpLevel, ManagerId = item.ManagerID, TitleId = titleIDEMployee, Amount = Convert.ToInt32(resultTitle.Amount) });
                            }
                            else
                            {
                                managerList.Add(new ManagerLevel { Level = item.EmpLevel, ManagerId = item.ManagerID, TitleId = titleIDEMployee, Amount = Convert.ToInt32(resultTitle.Amount) });
                                break;
                            }
                        }


                    }

                }
            }
            else
            {
                managerList.Add(new ManagerLevel { Level = 0, ManagerId = managerId, TitleId = titleId });
            }
            return managerList;
        }

        private List<ExpenseBookingDetail> GetBookingDetailApprover(Guid approverID)
        {
            List<ExpenseBookingDetail> expenseBookingDetails = new List<ExpenseBookingDetail>();
            var remarks = "";
            var bookingDetail = _unitOfWork.ExpenseBookingRequestDetail.Find(m => m.ExpenseBookingApproverId == approverID);
            if (bookingDetail.Count() > 0)
            {
                foreach (var item in bookingDetail)
                {
                    expenseBookingDetails.Add(new ExpenseBookingDetail { Id = item.Id.ToString(), EmployeeComment = item.EmployeeComment != null ? item.EmployeeComment : "", ManagerComment = item.ApproverComment != null ? item.ApproverComment : "" });
                }
            }
            return expenseBookingDetails;
        }

        private List<ExpenseBookingIviteApprover> GetBookingDetailApproverInvite(Guid approverID)
        {
            List<ExpenseBookingIviteApprover> expenseBookIngnvite = new List<ExpenseBookingIviteApprover>();
            var inviteApproverList = _unitOfWork.ExpenseBookingInviteApprover.Find(m => m.ExpenseBookingApproverId == approverID).ToList();
            if (inviteApproverList.Count() > 0)
            {
                foreach (var item in inviteApproverList)
                {
                    expenseBookIngnvite.Add(new ExpenseBookingIviteApprover { Name = EmployeeName(item.EmployeeId), Id = item.Id.ToString(), Status = System.Enum.GetName(typeof(ExpenseStatus), item.Status), ExpenseBookingDetailIviteApproverList = GetBookingDetailApproverCommentList(item.Id) });
                }
            }

            return expenseBookIngnvite;
        }

        private List<ExpenseBookingDetail> GetBookingDetailApproverCommentList(Guid approverInviteId)
        {
            List<ExpenseBookingDetail> expenseBookingDetails = new List<ExpenseBookingDetail>();
            var bookingDetail = _unitOfWork.ExpenseBookingRequestDetailInvite.Find(m => m.ExpenseBookingInviteApproverId == approverInviteId);
            if (bookingDetail.Count() > 0)
            {
                foreach (var item in bookingDetail)
                {
                    expenseBookingDetails.Add(new ExpenseBookingDetail { Id = item.Id.ToString(), EmployeeComment = item.EmployeeComment != null ? item.EmployeeComment : "", ManagerComment = item.ApproverComment != null ? item.ApproverComment : "" });
                }
            }
            return expenseBookingDetails;
        }

        private string EmployeeName(Guid empId)
        {
            var employeeName = "";
            var employee = _unitOfWork.Employee.Get(empId);
            if (employee != null)
            {
                var user = _accountManager.GetUserByIdAsync(employee.UserId).Result;
                if (user != null)
                {
                    employeeName = user.FullName;
                }
            }
            return employeeName;
        }

        private Tuple<string, string> GetCategorySubCategoryName(Guid id)
        {
            var category = "";
            var subCategory = "";
            var data = _unitOfWork.SubCategoryItem.Get(id);
            if (data != null)
            {
                var subCategoryObject = _unitOfWork.SubCategory.Get(data.ExpenseBookingSubCategoryId);
                if (subCategoryObject != null)
                {
                    subCategory = subCategoryObject.Name;
                    var categoryObject = _unitOfWork.Category.Get(subCategoryObject.ExpenseBookingCategoryId);
                    if (categoryObject != null)
                    {
                        category = categoryObject.Name;
                    }
                }
            }
            return Tuple.Create(category, subCategory);
        }

    }
}