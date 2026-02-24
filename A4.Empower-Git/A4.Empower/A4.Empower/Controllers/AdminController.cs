using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using A4.DAL.Core;
using A4.DAL.Entites;
using A4.Empower.Helpers;
using A4.Empower.ViewModels;
using DAL;
using DAL.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace A4.Empower.Controllers
{
    [Authorize(AuthenticationSchemes = OpenIddict.Validation.AspNetCore.OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    public class AdminController : Controller
    {
        readonly IUnitOfWork _unitOfWork;
        private readonly IAuthorizationService _authorizationService;
        private readonly IAccountManager _accountManager;
        readonly IEmailer _emailer;
        public AdminController(IUnitOfWork unitOfWork, IAuthorizationService authorizationService, IAccountManager accountManager, IEmailer emailer)
        {
            _unitOfWork = unitOfWork;
            _accountManager = accountManager;
            _authorizationService = authorizationService;
            _emailer = emailer;
        }

        [HttpPost("createClient")]
        public async Task<IActionResult> CreateClient([FromBody] AdminClientModel user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (user == null)
                        return BadRequest($"{nameof(user)} cannot be null");
                    var checkDepartment = _unitOfWork.Department.Find(m => m.Name == user.Department).FirstOrDefault();
                    if (checkDepartment == null)
                    {
                        var checkGroup = _unitOfWork.Group.Find(m => m.Name == user.FunctionalGroup).FirstOrDefault();
                        if (checkGroup == null)
                        {
                            var checkDesignation = _unitOfWork.Designation.Find(m => m.Name == user.Designation).FirstOrDefault();
                            if (checkDesignation == null)
                            {
                                var checkTitle = _unitOfWork.Title.Find(m => m.Name == user.Title).FirstOrDefault();
                                if (checkTitle == null)
                                {
                                    var checkBand = _unitOfWork.Band.Find(m => m.Name == user.Band).FirstOrDefault();
                                    if (checkBand == null)
                                    {
                                        ApplicationUser appUser = new ApplicationUser();
                                        appUser.UserName = user.EmailId;
                                        appUser.FullName = user.FullName;
                                        appUser.Email = user.EmailId;
                                        appUser.IsEnabled = true;
                                        appUser.EmailConfirmed = true;
                                        appUser.PhoneNumberConfirmed = true;
                                        appUser.PhoneNumber = user.ContactNo;

                                        var role = await _accountManager.GetRoleLoadRelatedAsync("administrator");
                                        if (role == null)
                                        {
                                            ApplicationRole appRole = new ApplicationRole();
                                            appRole.Name = "administrator";
                                            var resultRole = await _accountManager.CreateRoleAsync(appRole, GetPermissionValues());
                                            if (resultRole.Item1)
                                            {
                                                user.Roles = new string[] { appRole.Name };
                                            }
                                        }
                                        else
                                        {
                                            user.Roles = new string[] { "administrator" };
                                        }
                                        var result = await _accountManager.CreateUserAsync(appUser, user.Roles, user.Password);
                                        await _accountManager.AssignRolesToUser(appUser.Id, user.Roles);
                                        if (result.Item1)
                                        {

                                            Guid groupId = CreateDepartmentAndGroup(user.Department, user.FunctionalGroup);
                                            Guid designationId = CreateDesignation(user.Designation);
                                            Guid TitleId = CreateTitle(user.Title);
                                            Guid bandId = CreateBand(user.Band);

                                            //Creating Employee
                                            Employee employee = new Employee();
                                            employee.BandId = bandId;
                                            employee.GroupId = groupId;
                                            employee.DesignationId = designationId;
                                            employee.TitleId = TitleId;
                                            employee.ManagerId = employee.Id;
                                            employee.UserId = appUser.Id;
                                            employee.DOJ = DateTime.Now;
                                            employee.Location = "Place Name";
                                            _unitOfWork.Employee.Add(employee);
                                            _unitOfWork.SaveChanges();

                                            Personal personal = new Personal
                                            {
                                                EmployeeId = employee.Id
                                            };
                                            _unitOfWork.Profile.AddPersonalDetail(personal);
                    
                                            Qualification qualification = new Qualification
                                            {
                                                EmployeeId = employee.Id
                                            };
                                            _unitOfWork.Profile.AddQualificationDetail(qualification);

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

                                            //send  mail 
                                            string message1 = AdminTemplates.ClientCreate(user.FullName, user.Password,user.EmailId,url);
                                            (bool success, string errorMsg) response1 = await _emailer.SendEmailAsync(user.FullName, user.EmailId, "Client Registration", message1);
                                            return NoContent();

                                        }
                                        else
                                        {
                                            AddErrors(result.Item2);
                                            return BadRequest(ModelState);
                                        }
                                    }
                                    else
                                    {
                                        return BadRequest("Band name already exists");
                                    }
                                }
                                else
                                {
                                    return BadRequest("Title name already exists");
                                }
                            }
                            else
                            {
                                return BadRequest("Designation name already exists");
                            }

                        }
                        else
                        {
                            return BadRequest("Group name already exists");
                        }
                    }
                    else
                    {
                        return BadRequest("Department name already exists");
                    }
                }
                return BadRequest("model cannot be null");
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        private  string[] GetPermissionValues()
        {
            List<PermissionViewModel> model = new List<PermissionViewModel>();
            var permissionList = ApplicationPermissions.AllPermissions;

            foreach (var item in permissionList)
            {
                model.Add(new PermissionViewModel { GroupName = item.GroupName, Name = item.Name, Value = item.Value });
            }
            var module = _unitOfWork.ApplicationModule.GetAll();
            foreach (var item in module)
            {
                switch (item.Type)
                {
                    case "leave":
                        if (!item.IsActive)
                        {
                            model.RemoveAll(m => m.GroupName == "Leave");
                        }
                        break;

                    case "recruitment":
                        if (!item.IsActive)
                        {
                            model.RemoveAll(m => m.GroupName == "Recruitment");
                        }

                        break;

                    case "timesheet":
                        if (!item.IsActive)
                        {
                            model.RemoveAll(m => m.GroupName == "Timesheet");
                        }

                        break;

                    case "performance":
                        if (!item.IsActive)
                        {
                            model.RemoveAll(m => m.GroupName == "Performance");
                        }

                        break;
                    case "salesMarketing":
                        if (!item.IsActive)
                        {
                            model.RemoveAll(m => m.GroupName == "SalesMarketing");
                        }
                        break;

                    case "expanseManagement":
                        if (!item.IsActive)
                        {
                            model.RemoveAll(m => m.GroupName == "ExpenseBooking");
                        }
                        break;
                    default:
                        break;
                }
            }
            return permissionList.Select(p => p.Value).ToArray();
        }

        [HttpGet("getModule")]
        public IActionResult GetModule()
        {
            var result = new List<ApplicationModuleViewModel>();
            var module = _unitOfWork.ApplicationModule.GetModule();
            foreach (var item in module)
            {
                result.Add(new ApplicationModuleViewModel { Id = item.Id.ToString(), ModuleName = item.ModuleName, IsActive = item.IsActive, ApplicationModuleDetailModel = GetSubModule(item.ApplicationModuleDetail) });
            }
            return Ok(result);
        }

        private List<ApplicationModuleDetailModel> GetSubModule(List<ApplicationModuleDetail> applicationModuleDetails)
        {
            var result = new List<ApplicationModuleDetailModel>();
            if (applicationModuleDetails.Count > 0)
            {
                foreach (var item in applicationModuleDetails)
                {
                    result.Add(new ApplicationModuleDetailModel { Id = item.Id.ToString(), SubModuleName = item.SubModuleName, IsActive = item.IsActive });
                }
            }
            return result;
        }

        [HttpPut("module/{id}")]
        public IActionResult ModuleSetting(string id, [FromBody]bool togVal)
        {
            _unitOfWork.ApplicationModule.SwitchModuleSetting(new Guid(id), togVal);
            return NoContent();
        }

        [HttpPut("submodule/{id}")]
        public IActionResult SubModuleSetting(string id, [FromBody] bool togVal)
        {
            _unitOfWork.ApplicationModuleDetail.SwitchSubModuleSetting(new Guid(id), togVal);
            return NoContent();
        }

        private void AddErrors(IEnumerable<string> errors)
        {
            foreach (var error in errors)
            {
                ModelState.AddModelError(string.Empty, error);
            }
        }

        private async Task EnsureRoleAsync(string roleName, string description, string[] claims)
        {
            if ((await _accountManager.GetRoleByNameAsync(roleName)) == null)
            {
                ApplicationRole applicationRole = new ApplicationRole(roleName, description);

                var result = await this._accountManager.CreateRoleAsync(applicationRole, claims);

                if (!result.Item1)
                    throw new Exception($"Seeding \"{description}\" role failed. Errors: {string.Join(Environment.NewLine, result.Item2)}");
            }
        }

        private Guid CreateDepartmentAndGroup(string department, string group)
        {
            FunctionalDepartment functionalDepartment = new FunctionalDepartment
            {
                Name = department,
            };
            _unitOfWork.Department.Add(functionalDepartment);
            _unitOfWork.SaveChanges();
            FunctionalGroup functionalGroup = new FunctionalGroup
            {
                Name = group,
                DepartmentId = functionalDepartment.Id
            };

            _unitOfWork.Group.Add(functionalGroup);
            _unitOfWork.SaveChanges();
            return functionalGroup.Id;

        }

        private Guid CreateDesignation(string designation)
        {
            FunctionalDesignation functionalDesignation = new FunctionalDesignation
            {
                Name = designation,
            };

            _unitOfWork.Designation.Add(functionalDesignation);
            _unitOfWork.SaveChanges();
            return functionalDesignation.Id;
        }

        private Guid CreateTitle(string title)
        {
            FunctionalTitle functionalTitle = new FunctionalTitle
            {
                Name = title,
            };

            _unitOfWork.Title.Add(functionalTitle);
            _unitOfWork.SaveChanges();
            return functionalTitle.Id;
        }

        private Guid CreateBand(string band)
        {
            Band functionalband = new Band
            {
                Name = band,
            };
            _unitOfWork.Band.Add(functionalband);
            _unitOfWork.SaveChanges();
            return functionalband.Id;
        }

    }
}
