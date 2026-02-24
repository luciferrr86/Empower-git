using System;
using System.Collections.Generic;
using System.Linq;
using A4.BAL;
using A4.DAL.Entites;
using A4.Empower.Helpers;
using DAL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace A4.Empower.Controllers
{
    [Authorize(AuthenticationSchemes = OpenIddict.Validation.AspNetCore.OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    public class SalesMarketingController : Controller
    {
        readonly ILogger _logger;
        readonly IUnitOfWork _unitOfWork;
        readonly IEmailer _emailer;
        public SalesMarketingController(ILogger<SalesMarketingController> logger, IUnitOfWork unitOfWork, IEmailer emailer)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _emailer = emailer;
        }

        [HttpGet("list/{page?}/{pageSize?}/{name?}")]
        [Produces(typeof(CompanyViewModel))]
        public IActionResult GetCompanyList(int? page = null, int? pageSize = null, string name = null)
        {
            try
            {
                var result = new CompanyViewModel();
                var viewModel = new List<CompanyModel>();
                var model = _unitOfWork?.SalesCompany?.GetAllSalesCompany(name, Convert.ToInt32(page), Convert.ToInt32(pageSize));
                if (model.Count() > 0)
                {
                    foreach (var item in model)
                    {
                        viewModel.Add(new CompanyModel
                        {
                            Id = item.Id.ToString(),
                            ComapnyName = !string.IsNullOrEmpty(item.ComapnyName) ? item.ComapnyName : string.Empty,
                            CompanyAddress = item.CompanyAddress,
                            City = item.City,
                            Country = item.Country,
                            State = item.State,
                            EmailId = item.EmailId,
                            Telephone = item.Telephone,
                            ZipCode = item.ZipCode
                        });
                    }
                }
                result.TotalCount = model.TotalCount;
                result.listCompany = viewModel;
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.GetBaseException().Message);
            }
        }

        [HttpGet("company/{id?}")]
        [Produces(typeof(CompanyModel))]
        public IActionResult GetCompany(string id)
        {
            try
            {
                if (id.checkString())
                {
                    var model = new CompanyModel();
                    Guid companyId = Guid.Parse(id);
                    if (companyId != Guid.Empty)
                    {
                        var company = _unitOfWork?.SalesCompany?.Find(m => m.Id == companyId).FirstOrDefault();
                        if (company != null)
                        {
                            model.Id = company.Id.ToString();
                            model.ComapnyName = company.ComapnyName;
                            model.CompanyAddress = company.CompanyAddress;
                            model.City = company.City;
                            model.State = company.State;
                            model.Country = company.Country;
                            model.ZipCode = company.ZipCode;
                            model.EmailId = company.EmailId;
                            model.Telephone = company.Telephone;
                            var companyContact = _unitOfWork?.SalesCompanyContact?.Find(m => m.SalesCompanyId == company.Id).ToList();
                            if (companyContact.Count() > 0)
                            {
                                foreach (var item in companyContact)
                                {
                                    model.lstCompanyContacts.Add(new CompanyContactsModel
                                    {
                                        Id = item.Id.ToString(),
                                        FirstName = item.FirstName,
                                        LastName = item.LastName,
                                        EmailId = item.EmailId,
                                        Telephone = item.Telephone,
                                        MobileNo = item.MobileNo,
                                        Designation = item.Designation,
                                        SalesCompanyId = item.SalesCompanyId.ToString()

                                    });
                                }

                            }
                            return Ok(model);
                        }
                        else
                        {
                            return BadRequest("company data  can not be null");
                        }
                    }
                    else
                    {
                        return BadRequest("companyId can not be Empty");
                    }
                }
                else
                {
                    return BadRequest("companyId can be Guid");
                }

            }
            catch (Exception ex)
            {

                return BadRequest(ex.GetBaseException().Message);
            }

        }

        [HttpPost("createCompany")]
        public IActionResult CreateCompany([FromBody]CompanyModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (model == null)
                        return BadRequest($"{nameof(model)} cannot be null");

                    if (!string.IsNullOrEmpty(model.Id))
                    {
                        var check = _unitOfWork?.SalesCompany?.Get(Guid.Parse(model.Id));
                        if (check != null)
                        {
                            check.ComapnyName = model.ComapnyName;
                            check.CompanyAddress = model.CompanyAddress;
                            check.City = model.City;
                            check.State = model.State;
                            check.Country = model.Country;
                            check.ZipCode = model.ZipCode;
                            check.EmailId = model.EmailId;
                            check.Telephone = model.Telephone;
                            _unitOfWork.SalesCompany.Update(check);
                            if (model.lstCompanyContacts.Count() > 0)
                            {
                                var checkContact = _unitOfWork?.SalesCompanyContact?.Find(m => m.SalesCompanyId == check.Id).ToList();
                                if (checkContact.Count() > 0)
                                {
                                    _unitOfWork.SalesCompanyContact.RemoveRange(checkContact);
                                }
                                var listContact = new List<SalesCompanyContact>();
                                foreach (var item in model.lstCompanyContacts)
                                {
                                    if (!string.IsNullOrEmpty(item.Id))
                                    {
                                        var data = _unitOfWork?.SalesCompanyContact?.Get(Guid.Parse(item.Id));
                                        if (data != null)
                                        {
                                            data.FirstName = item.FirstName;
                                            data.LastName = item.LastName;
                                            data.MobileNo = item.MobileNo;
                                            data.EmailId = item.EmailId;
                                            data.Telephone = item.Telephone;
                                            data.Designation = item.Designation;
                                            _unitOfWork.SalesCompanyContact.Update(data);

                                        }

                                    }
                                    else
                                    {
                                        var contact = new SalesCompanyContact();
                                        contact.FirstName = item.FirstName;
                                        contact.LastName = item.LastName;
                                        contact.MobileNo = item.MobileNo;
                                        contact.EmailId = item.EmailId;
                                        contact.Telephone = item.Telephone;
                                        contact.Designation = item.Designation;
                                        contact.SalesCompanyId = check.Id;
                                        _unitOfWork.SalesCompanyContact.Add(contact);
                                    }

                                    _unitOfWork.SaveChanges();


                                }

                            }

                        }
                    }
                    else
                    {


                        var Model = new SalesCompany()
                        {
                            ComapnyName = model.ComapnyName,
                            CompanyAddress = model.CompanyAddress,
                            City = model.City,
                            State = model.State,
                            Country = model.Country,
                            ZipCode = model.ZipCode,
                            EmailId = model.EmailId,
                            Telephone = model.Telephone
                        };
                        _unitOfWork.SalesCompany.Add(Model);
                        if (model.lstCompanyContacts.Count() > 0)
                        {
                            var listContact = new List<SalesCompanyContact>();
                            foreach (var item in model.lstCompanyContacts)
                            {
                                listContact.Add(new SalesCompanyContact { FirstName = item.FirstName, LastName = item.LastName, EmailId = item.EmailId, Telephone = item.Telephone, MobileNo = item.MobileNo, Designation = item.Designation, SalesCompanyId = Model.Id });

                            }
                            _unitOfWork.SalesCompanyContact.AddRange(listContact);
                        }
                    }
                    _unitOfWork.SaveChanges();
                    return NoContent();
                }
                else
                {
                    return BadRequest("model Is Invalid");
                }
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
                    var checkexist = _unitOfWork.SalesCompany.Get(new Guid(id));
                    if (checkexist != null)
                    {
                        _unitOfWork.SalesCompany.Remove(checkexist);
                        var contacts = _unitOfWork?.SalesCompanyContact?.Find(m => m.SalesCompanyId == checkexist.Id).ToList();
                        if (contacts.Count > 0)
                        {
                            _unitOfWork.SalesCompanyContact.RemoveRange(contacts);
                        }
                        _unitOfWork.SaveChanges();
                        return NoContent();
                    }
                    else
                    {
                        return NotFound(id);
                    }
                }
                return BadRequest("Id cannot be null");
            }
            catch (Exception ex)
            {

                return BadRequest(ex.GetBaseException().Message);
            }
        }

        [HttpDelete("deletecontact/{id}")]
        public IActionResult DeleteCompanyContact(string id)
        {
            try
            {
                if (!string.IsNullOrEmpty(id))
                {

                    var contacts = _unitOfWork?.SalesCompanyContact?.Get(Guid.Parse(id));
                    if (contacts != null)
                    {
                        _unitOfWork.SalesCompanyContact.Remove(contacts);
                        _unitOfWork.SaveChanges();
                    }
                    else
                    {
                        return NotFound(id);
                    }
                    return NoContent();
                }
                return BadRequest("Id cannot be null");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.GetBaseException().Message);
            }
        }

        [HttpPut("update/{id}")]
        public IActionResult Update(string id, [FromBody]CompanyModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (model == null)
                        return BadRequest($"{nameof(model)} cannot be null");

                    if (string.IsNullOrEmpty(id))
                        return BadRequest("Id property cannot be null");
                    var check = _unitOfWork?.SalesCompany?.Get(new Guid(id));
                    if (check != null)
                    {
                        check.ComapnyName = model.ComapnyName;
                        check.CompanyAddress = model.CompanyAddress;
                        check.City = model.City;
                        check.State = model.State;
                        check.Country = model.Country;
                        check.ZipCode = model.ZipCode;
                        check.EmailId = model.EmailId;
                        check.Telephone = model.Telephone;
                        _unitOfWork.SalesCompany.Update(check);
                        _unitOfWork.SaveChanges();
                        UpdateSalesComapnyContact(model, check);
                        return NoContent();

                    }
                    return NotFound(model.Id);
                }
                return BadRequest(ModelState);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.GetBaseException().Message);
            }
        }

        [HttpPost("createStatus")]
        public IActionResult CreateStatus([FromBody]DailyCallModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (model == null)
                        return BadRequest($"{nameof(model)} cannot be null");
                    var Model = new SalesDailyCall()
                    {
                        Description = model.Description,
                        CallDateTime = model.CallDateTime,
                        SalesCompanyId = Guid.Parse(model.SalesCompanyId),
                        SalesStatusId = Guid.Parse(model.SalesStatusId)
                    };
                    _unitOfWork.SalesDailyCall.Add(Model);
                    _unitOfWork.SaveChanges();
                    return NoContent();
                }
                else
                {
                    return BadRequest("model is invalid");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.GetBaseException().Message);
            }
        }

        [HttpGet("statusList/{id?}")]
        public IActionResult GetStatusList(string id)
        {
            if (id.checkString())
            {
                var model = new StatusViewModel();
                Guid companyId = Guid.Parse(id);
                if (companyId != Guid.Empty)
                {
                    var salesCompany = _unitOfWork?.SalesCompany?.Find(m => m.Id == companyId && m.IsActive == true).FirstOrDefault();
                    if (salesCompany != null)
                    {
                        model.StatusCompanyModel.CompanyId = salesCompany.Id.ToString();
                        model.StatusCompanyModel.CompanyName = salesCompany.ComapnyName;
                        model.StatusCompanyModel.CompanyAddress = salesCompany.CompanyAddress;
                        model.StatusCompanyModel.CompanyTelePhoneNo = salesCompany.Telephone;
                        model.StatusCompanyModel.EmailId = salesCompany.EmailId;
                        var lstDailyCall = new List<DailyCallModel>();
                        var salesDailyCall = _unitOfWork?.SalesDailyCall?.Find(m => m.SalesCompanyId == salesCompany.Id).ToList();
                        if (salesDailyCall.Count() > 0)
                        {
                            foreach (var item in salesDailyCall)
                            {
                                lstDailyCall.Add(new DailyCallModel { Name = GetStatusName(item.SalesStatusId), Id = item.Id.ToString(), CallDateTime = item.CallDateTime, Description = item.Description });
                            }
                        }
                        model.DailyCallModel = lstDailyCall;
                        model.DdlSaleStatus = GetStatusNameList();
                        return Ok(model);
                    }
                    else
                    {
                        return BadRequest("SalesCompany can not be Empty");
                    }
                }
                else
                {
                    return BadRequest("CompanyId can not be Empty");
                }
            }
            else
            {
                return BadRequest("CompanyId can be Guid");
            }
        }

        private void UpdateSalesComapnyContact(CompanyModel model, SalesCompany check)
        {
            var checkContact = _unitOfWork?.SalesCompanyContact?.Find(m => m.SalesCompanyId == check.Id).ToList();
            if (checkContact.Count() > 0)
            {
                _unitOfWork.SalesCompanyContact.RemoveRange(checkContact);
                _unitOfWork.SaveChanges();
            }
            foreach (var item in model.lstCompanyContacts)
            {
                var contacts = new SalesCompanyContact()
                {
                    FirstName = item.FirstName,
                    LastName = item.LastName,
                    EmailId = item.EmailId,
                    Telephone = item.Telephone,
                    MobileNo = item.MobileNo,
                    Designation = item.Designation,
                    SalesCompanyId = check.Id,
                };
                _unitOfWork.SalesCompanyContact.Add(contacts);
                _unitOfWork.SaveChanges();
            }
        }

        private List<DropDownList> GetStatusNameList()
        {
            var lstStatus = new List<DropDownList>();
            var salesStatus = _unitOfWork?.SalesStatus.Find(m => m.IsActive == true).ToList();
            if (salesStatus.Count() > 0)
            {
                foreach (var item in salesStatus)
                {
                    lstStatus.Add(new DropDownList
                    {
                        Value = item.Id.ToString(),
                        Label = item.Name
                    });
                }
            }
            return lstStatus;
        }

        private string GetStatusName(Guid statusId)
        {
            var statusName = "";
            var salesStatus = _unitOfWork?.SalesStatus.Get(statusId);
            if (salesStatus != null)
            {
                statusName = salesStatus.Name;
            }
            return statusName;
        }
    }
}