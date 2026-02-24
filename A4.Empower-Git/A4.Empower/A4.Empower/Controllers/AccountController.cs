
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using A4.Empower.ViewModels;
using AutoMapper;
using DAL.Core.Interfaces;
using A4.Empower.Helpers;
using Microsoft.AspNetCore.JsonPatch;
using A4.DAL.Entites;
using A4.DAL.Core;
using DAL;
using A4.BAL;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using System.Net;

namespace A4.Empower.Controllers
{
    //[Authorize(AuthenticationSchemes = OpenIddict.Validation.AspNetCore.OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)]
    [AllowAnonymous]
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        private readonly IAccountManager _accountManager;
        private readonly IAuthorizationService _authorizationService;
        private readonly IMapper _mapper;
        readonly IEmailer _emailer;
        private const string GetUserByIdActionName = "GetUserById";
        private const string GetRoleByIdActionName = "GetRoleById";
        private readonly UserManager<ApplicationUser> userManager;
        readonly IUnitOfWork _unitOfWork;
        public AccountController(IUnitOfWork unitOfWork, IAccountManager accountManager, IAuthorizationService authorizationService, IEmailer emailer, UserManager<ApplicationUser> userManager, IMapper mapper)
        {

            _emailer = emailer;
            _accountManager = accountManager;
            _authorizationService = authorizationService;
            _unitOfWork = unitOfWork;
            this.userManager = userManager;
            _mapper = mapper;
        }

        [HttpGet("users/me")]
        [Produces(typeof(UserViewModel))]
        public async Task<IActionResult> GetCurrentUser()
        {
            return await GetUserByUserName(this.User.Identity.Name);
        }

        [HttpGet("users/{id}", Name = GetUserByIdActionName)]
        [Produces(typeof(UserViewModel))]
        public async Task<IActionResult> GetUserById(string id)
        {
            if (!(await _authorizationService.AuthorizeAsync(this.User, id)).Succeeded)
                return new ChallengeResult();

            UserModel userVM = await GetUserViewModelHelper(id);

            if (userVM != null)
                return Ok(userVM);
            else
                return NotFound(id);
        }

        [HttpGet("users/username/{userName}")]
        [Produces(typeof(UserViewModel))]
        public async Task<IActionResult> GetUserByUserName(string userName)
        {
            ApplicationUser appUser = await _accountManager.GetUserByUserNameAsync(userName);

            if (!(await _authorizationService.AuthorizeAsync(this.User, appUser?.Id ?? "")).Succeeded)
                return new ChallengeResult();

            if (appUser == null)
                return NotFound(userName);

            return await GetUserById(appUser.Id);
        }

        [HttpGet("users")]
        [Produces(typeof(UserViewModel))]
        public async Task<IActionResult> GetUsers()
        {
            return await GetUsers(-1, -1);
        }

        [HttpGet("users/{page:int}/{pageSize:int}/{name?}")]
        [Produces(typeof(UserViewModel))]
        public async Task<IActionResult> GetUsers(int page, int pageSize, string name = null)
        {
            try
            {
                var managerVM = new List<DropDownList>();
                var roleVM = new List<DropDownList>();
                UserViewModel usersViewModel = new UserViewModel();
                List<UserModel> usersModel = new List<UserModel>();
                int totalCount = 0;
                var usersAndRoles = await _accountManager.GetUsersAndRolesAsync(page, pageSize, name);
                var roles = await _accountManager.GetRolesLoadRelatedAsync(-1, -1, null);
                foreach (var item in roles.Item1)
                {

                    roleVM.Add(new DropDownList { Label = item.Name, Value = item.Id.ToString() });
                }
                var managerList = _unitOfWork.Employee.GetManagerList();
                foreach (var manager in managerList)
                {
                    managerVM.Add(new DropDownList { Label = manager.FullName, Value = manager.Id.ToLower().ToString() });
                }
                var list = GetMaintenanceList();
                foreach (var item in usersAndRoles)
                {
                    var userModelObject = _mapper.Map<UserModel>(item.Item1);
                    userModelObject.Email = item.Item1.Email;
                    userModelObject.IsEnabled = item.Item1.IsEnabled;
                    userModelObject.PhoneNumber = item.Item1.PhoneNumber;
                    userModelObject.Id = item.Item1.Id;
                    userModelObject.FullName = item.Item1.FullName;

                    var emplyee = _unitOfWork.Employee.GetEmployee(item.Item1.Id);
                    if (emplyee != null)
                    {
                        userModelObject.BandId = emplyee.BandId.ToString();
                        userModelObject.DesignationId = emplyee.DesignationId.ToString();
                        if (item.Item2.Length > 0)
                        {
                            userModelObject.RoleId = roles.Item1.Where(m => m.Name == item.Item2[0]).FirstOrDefault().Id;
                        }
                        userModelObject.GroupId = emplyee.GroupId.ToString();
                        userModelObject.TitleId = emplyee.TitleId.ToString();
                        userModelObject.ManagerId = emplyee.ManagerId.ToString();
                        userModelObject.DOJ = emplyee.DOJ.ToShortDateString();
                        userModelObject.Location = emplyee.Location;
                        userModelObject.EmpCode = emplyee.EmpCode;
                        var designaion = _unitOfWork.Designation.Get(emplyee.DesignationId);
                        if (designaion != null)
                        {
                            userModelObject.Designation = designaion.Name;
                        }
                        else
                        {
                            userModelObject.Designation = "N/A";
                        }
                        var group = _unitOfWork.Group.Get(emplyee.GroupId);
                        if (group != null)
                        {
                            userModelObject.Group = group.Name;
                        }
                        else
                        {
                            userModelObject.Group = "N/A";
                        }
                        totalCount++;
                        usersModel.Add(userModelObject);
                    }
                }
                usersViewModel.TotalCount = totalCount;
                usersViewModel.DesignationList = list.Item1;
                usersViewModel.GroupList = list.Item2;
                usersViewModel.BandList = list.Item3;
                usersViewModel.TitleList = list.Item4;
                usersViewModel.ManagerList = managerVM;
                usersViewModel.UserModel = usersModel;
                usersViewModel.RoleList = roleVM;
                return Ok(usersViewModel);
            }
            catch (Exception ex)
            {
                return BadRequest("Unable to get list from server");
            }
        }


        [HttpPut("users/me")]
        public async Task<IActionResult> UpdateCurrentUser([FromBody] UserEditViewModel user)
        {
            return await UpdateUser(Utilities.GetUserId(this.User), user);
        }

        [HttpPut("users/{id}")]
        public async Task<IActionResult> UpdateUser(string id, [FromBody] UserEditViewModel user)
        {
            try
            {
                ApplicationUser appUser = await _accountManager.GetUserByIdAsync(id);
                string[] currentRoles = appUser != null ? (await _accountManager.GetUserRolesAsync(appUser)).ToArray() : null;

                var manageUsersPolicy = _authorizationService.AuthorizeAsync(this.User, id, Authorization.Policies.ManageEmployeePolicy);
                var assignRolePolicy = _authorizationService.AuthorizeAsync(this.User, Tuple.Create(user.Roles, currentRoles), Authorization.Policies.ManageRolePolicy);


                if ((await Task.WhenAll(manageUsersPolicy, assignRolePolicy)).Any(r => !r.Succeeded))
                    return new ChallengeResult();


                if (ModelState.IsValid)
                {
                    if (user == null)
                        return BadRequest($"{nameof(user)} cannot be null");

                    if (!string.IsNullOrWhiteSpace(user.Id) && id != user.Id)
                        return BadRequest("Conflicting user id in parameter and model data");

                    if (appUser == null)
                        return NotFound(id);

                    _mapper.Map<UserModel, ApplicationUser>(user, appUser);
                    appUser.UserName = user.Email;
                    appUser.NormalizedUserName = user.Email;
                    var result = await _accountManager.UpdateUserAsync(appUser, user.Roles);
                    if (result.Item1)
                    {
                        var employee = _unitOfWork.Employee.Find(m => m.UserId == id).FirstOrDefault();
                        if (employee != null)
                        {
                            //Updating Employee
                            employee.BandId = Guid.Parse(user.BandId);
                            employee.GroupId = Guid.Parse(user.GroupId);
                            employee.DesignationId = Guid.Parse(user.DesignationId);
                            employee.TitleId = Guid.Parse(user.TitleId);
                            employee.ManagerId = Guid.Parse(user.ManagerId);
                            employee.UserId = appUser.Id;
                            employee.Location = user.Location;
                            employee.DOJ = Convert.ToDateTime(user.DOJ);
                            employee.EmpCode = user.EmpCode;
                            _unitOfWork.Employee.Update(employee);
                            _unitOfWork.SaveChanges();
                            return NoContent();
                        }
                        return BadRequest("employee cannot be null");
                    }

                    AddErrors(result.Item2);
                }

                return BadRequest(ModelState);
            }
            catch (DbUpdateException ex) when (ex.InnerException is SqlException sqlException && (sqlException.Number == 2627 || sqlException.Number == 2601))
            {
                return BadRequest(user.Email + "already exists");
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }

        [HttpPatch("users/me")]
        public async Task<IActionResult> UpdateCurrentUser([FromBody] JsonPatchDocument<UserPatchViewModel> patch)
        {
            return await UpdateUser(Utilities.GetUserId(this.User), patch);
        }

        [HttpPatch("users/{id}")]
        public async Task<IActionResult> UpdateUser(string id, [FromBody] JsonPatchDocument<UserPatchViewModel> patch)
        {
            if (!(await _authorizationService.AuthorizeAsync(this.User, id)).Succeeded)
                return new ChallengeResult();


            if (ModelState.IsValid)
            {
                if (patch == null)
                    return BadRequest($"{nameof(patch)} cannot be null");


                ApplicationUser appUser = await _accountManager.GetUserByIdAsync(id);

                if (appUser == null)
                    return NotFound(id);


                UserPatchViewModel userPVM = _mapper.Map<UserPatchViewModel>(appUser);
                patch.ApplyTo(userPVM, ModelState);


                if (ModelState.IsValid)
                {
                    _mapper.Map<UserPatchViewModel, ApplicationUser>(userPVM, appUser);

                    var result = await _accountManager.UpdateUserAsync(appUser);
                    if (result.Item1)
                    {
                        var employee = _unitOfWork.Employee.Find(m => m.UserId == appUser.Id).FirstOrDefault();
                        if (employee != null)
                        {

                        }
                        return NoContent();
                    }



                    AddErrors(result.Item2);
                }
            }

            return BadRequest(ModelState);
        }

        [HttpPost("users")]
        public async Task<IActionResult> Register([FromBody] UserEditViewModel user)
        {
            try
            {
                var host = HttpContext.Request;
                if (ModelState.IsValid)
                {
                    if (user == null)
                        return BadRequest($"{nameof(user)} cannot be null");
                    ApplicationUser appUser = _mapper.Map<ApplicationUser>(user);
                    string password = Utilities.GenerateRandomPassword();
                    appUser.UserName = user.Email;
                    appUser.Id = Guid.NewGuid().ToString();
                    var result = await _accountManager.CreateUserAsync(appUser, user.Roles, password);
                    await _accountManager.AssignRolesToUser(appUser.Id, user.Roles);
                    if (result.Item1)
                    {
                        //Creating Employee
                        Employee employee = new Employee();
                        employee.BandId = new Guid(user.BandId);
                        employee.GroupId = new Guid(user.GroupId);
                        employee.DesignationId = new Guid(user.DesignationId);
                        employee.TitleId = new Guid(user.TitleId);
                        employee.ManagerId = new Guid(user.ManagerId);
                        employee.UserId = appUser.Id;
                        employee.Location = user.Location;
                        employee.DOJ = Convert.ToDateTime(user.DOJ);
                        employee.EmpCode = user.EmpCode;
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
                        _unitOfWork.SaveChanges();
                        string url = "";
                        if (host.IsHttps)
                        {
                            url = "https://" + host.Host.Value;
                        }
                        else
                        {
                            url = "http://" + host.Host.Value;
                        }
                        string message = AccountTemplates.GetEmployeeEmail(user.FullName, user.Email, password, url);
                        (bool success, string errorMsg) response = await _emailer.SendEmailAsync(user.FullName, user.Email, "Employee Registeration", message);

                        return NoContent();
                    }
                    if (result.Item2.Length > 1)
                    {
                        return BadRequest(result.Item2[1].ToString());
                    }
                    else
                    {
                        return BadRequest(result.Item2);
                    }
                }

                return BadRequest(ModelState);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        [HttpDelete("users/{id}")]
        [Produces(typeof(UserViewModel))]
        public async Task<IActionResult> DeleteUser(string id)
        {
            try
            {
                UserModel userVM = null;
                ApplicationUser appUser = await this._accountManager.GetUserByIdAsync(id);

                if (appUser != null)
                    userVM = await GetUserViewModelHelper(appUser.Id);


                if (userVM == null)
                    return NotFound(id);
                var emp = _unitOfWork.Employee.Find(x => x.UserId == appUser.Id).FirstOrDefault();
                if (emp != null)
                {
                    _unitOfWork.Employee.Remove(emp);
                }
                var result = await this._accountManager.DeleteUserAsync(appUser);
                if (!result.Item1)
                    throw new Exception("The following errors occurred whilst deleting user: " + string.Join(", ", result.Item2));


                return Ok(userVM);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        [HttpGet("roles/{id}", Name = GetRoleByIdActionName)]
        [Produces(typeof(RoleViewModel))]
        public async Task<IActionResult> GetRoleById(string id)
        {
            var appRole = await _accountManager.GetRoleByIdAsync(id);

            if (!(await _authorizationService.AuthorizeAsync(this.User, appRole?.Name ?? "")).Succeeded)
                return new ChallengeResult();

            if (appRole == null)
                return NotFound(id);

            return await GetRoleByName(appRole.Name);
        }

        [HttpGet("roles/name/{name}")]
        [Produces(typeof(RoleViewModel))]
        public async Task<IActionResult> GetRoleByName(string name)
        {
            if (!(await _authorizationService.AuthorizeAsync(this.User, name, Authorization.Policies.ManageRolePolicy)).Succeeded)
                return new ChallengeResult();


            RoleModel roleVM = await GetRoleViewModelHelper(name);

            if (roleVM == null)
                return NotFound(name);

            return Ok(roleVM);
        }

        [HttpGet("roles")]
        [Produces(typeof(List<RoleViewModel>))]
        [Authorize(Authorization.Policies.ManageRolePolicy)]
        public async Task<IActionResult> GetRoles()
        {
            return await GetRoles(-1, -1);
        }

        [HttpGet("roles/{page:int}/{pageSize:int}/{name?}")]
        [Produces(typeof(RoleViewModel))]
        public async Task<IActionResult> GetRoles(int page, int pageSize, string name = null)
        {
            var roleViewModel = new RoleViewModel();
            var roles = await _accountManager.GetRolesLoadRelatedAsync(page, pageSize, name);

            var rolesList = _mapper.Map<List<RoleModel>>(roles.Item1);
            roleViewModel.RoleModel = rolesList;
            roleViewModel.TotalCount = roles.Item2;
            return Ok(roleViewModel);
        }

        [HttpPut("roles/{id}")]
        public async Task<IActionResult> UpdateRole(string id, [FromBody] RoleModel role)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (role == null)
                        return BadRequest($"{nameof(role)} cannot be null");

                    if (!string.IsNullOrWhiteSpace(role.Id) && id != role.Id)
                        return BadRequest("Conflicting role id in parameter and model data");



                    ApplicationRole appRole = await _accountManager.GetRoleByIdAsync(id);

                    if (appRole == null)
                        return NotFound(id);


                    _mapper.Map<RoleModel, ApplicationRole>(role, appRole);

                    var result = await _accountManager.UpdateRoleAsync(appRole, role.Permissions?.Select(p => p.Value).ToArray());
                    if (result.Item1)
                        return NoContent();

                    AddErrors(result.Item2);

                }

                return BadRequest(ModelState);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.GetBaseException().Message);
            }
        }

        [HttpPost("roles")]
        public async Task<IActionResult> CreateRole([FromBody] RoleModel role)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (role == null)
                        return BadRequest($"{nameof(role)} cannot be null");


                    ApplicationRole appRole = _mapper.Map<ApplicationRole>(role);
                    appRole.Id = Guid.NewGuid().ToString();
                    appRole.Name.ToLower();
                    var result = await _accountManager.CreateRoleAsync(appRole, role.Permissions?.Select(p => p.Value).ToArray());
                    if (result.Item1)
                    {
                        RoleModel roleVM = await GetRoleViewModelHelper(appRole.Name);
                        return CreatedAtAction(GetRoleByIdActionName, new { id = roleVM.Id }, roleVM);
                    }

                    AddErrors(result.Item2);
                }
                return BadRequest(ModelState);
            }

            catch (DbUpdateException ex) when (ex.InnerException is SqlException sqlException && (sqlException.Number == 2627 || sqlException.Number == 2601))
            {
                return BadRequest(role.Name + " already exists");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.GetBaseException().Message);

            }


        }

        [HttpDelete("roles/{id}")]
        [Produces(typeof(RoleViewModel))]
        public async Task<IActionResult> DeleteRole(string id)
        {
            if (!await _accountManager.CanDeleteRoleAsync(id))
                return BadRequest("Role cannot be deleted. Remove all users from this role and try again");

            RoleModel roleVM = null;
            ApplicationRole appRole = await this._accountManager.GetRoleByIdAsync(id);

            if (appRole != null)
                roleVM = await GetRoleViewModelHelper(appRole.Name);


            if (roleVM == null)
                return NotFound(id);

            var result = await this._accountManager.DeleteRoleAsync(appRole);
            if (!result.Item1)
                throw new Exception("The following errors occurred whilst deleting role: " + string.Join(", ", result.Item2));


            return Ok(roleVM);
        }

        [HttpGet("permissions")]
        [Produces(typeof(List<PermissionViewModel>))]
        public IActionResult GetAllPermissions()
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

            return Ok(model);
        }

        [HttpPut("changepassword/{id}")]
        public async Task<IActionResult> ChangePassword(string id, [FromBody] ChangePasswordModel model)
        {
            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(id))
                    return View();
                ApplicationUser appUser = await _accountManager.GetUserByIdAsync(id);
                var passwordUpdated = await this._accountManager.UpdatePasswordAsync(appUser, model.CurrentPassword, model.NewPassword);
                if (passwordUpdated.Item1)
                {
                    return NoContent();
                }
                return BadRequest($"No Content");
            }
            return BadRequest("The password and confirmation password do not match.");
        }

        [AllowAnonymous]
        [HttpPost("users/ForgotPassword/{emailId}")]
        public async Task<IActionResult> ForgotPassword(string emailId)
        {
            var host = HttpContext.Request;

            if (string.IsNullOrEmpty(emailId))
                return View();

            var user = await this.userManager.FindByEmailAsync(emailId);
            if (user != null)
            {
                var confrimationCode =
                  await this.userManager.GenerateChangePhoneNumberTokenAsync(user, user.PhoneNumber);

                var code = WebUtility.UrlEncode(Utilities.UrlSafeEncode(confrimationCode));

                UserModel userVM = await GetUserViewModelHelper(user.Id);
                string url = "";
                if (host.IsHttps)
                {
                    url = "https://" + host.Host.Value;
                }
                else
                {
                    url = "http://" + host.Host.Value;
                }

                string link = url + "/account/reset-password?code=" + code + "&email=" + userVM.Email;
                string message = AccountTemplates.ForgotPassword(userVM.FullName, link);
                (bool success, string errorMsg) response = await _emailer.SendEmailAsync(userVM.FullName, userVM.Email, "Reset Password Link", message);
                return CreatedAtAction(GetUserByIdActionName, new { id = userVM.Id }, userVM);
            }
            else
            {
                return BadRequest("user cannot be null");
            }

        }

        [AllowAnonymous]
        [HttpPost("users/resetPassword")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordModel model)
        {
            var user = await this.userManager.FindByEmailAsync(model.EmailId);

            if (user != null)
            {
                var result = await userManager.VerifyChangePhoneNumberTokenAsync(user, model.TokenId, user.PhoneNumber);
                if (result)
                {
                    var newPassword = userManager.PasswordHasher.HashPassword(user, model.NewPassword);
                    user.PasswordHash = newPassword;
                    var res = await userManager.UpdateAsync(user);
                    if (res.Succeeded)
                    {
                        return NoContent();
                    }
                    else
                    {
                        return BadRequest("Please try after some time.");
                    }
                }
                else
                {
                    return BadRequest("Unable to update password.");
                }
            }

            return BadRequest("Email id does not exist");


        }

        [HttpGet("getUserEmployeeData/{id}")]
        public IActionResult GetUserEmployeeData(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                var employee = _unitOfWork.Employee.GetEmployee(id);
                return Ok(employee.Id);
            }
            return NoContent();
        }

        private async Task<UserModel> GetUserViewModelHelper(string userId)
        {
            var userAndRoles = await _accountManager.GetUserAndRolesAsync(userId);
            if (userAndRoles == null)
                return null;

            var userVM = _mapper.Map<UserModel>(userAndRoles.Item1);
            userVM.Roles = userAndRoles.Item2;

            return userVM;
        }

        private async Task<RoleModel> GetRoleViewModelHelper(string roleName)
        {
            var role = await _accountManager.GetRoleLoadRelatedAsync(roleName);
            if (role != null)
                return _mapper.Map<RoleModel>(role);


            return null;
        }

        private void AddErrors(IEnumerable<string> errors)
        {
            foreach (var error in errors)
            {
                ModelState.AddModelError(string.Empty, error);
            }
        }

        private Tuple<List<DropDownList>, List<DropDownList>, List<DropDownList>, List<DropDownList>> GetMaintenanceList()
        {
            var desinationVM = new List<DropDownList>();
            var designationList = _unitOfWork.Designation.GetAll();
            foreach (var item in designationList)
            {
                desinationVM.Add(new DropDownList { Label = item.Name, Value = item.Id.ToString() });
            }

            var groupVM = new List<DropDownList>();
            var groupList = _unitOfWork.Group.GetAll();
            foreach (var item in groupList)
            {
                groupVM.Add(new DropDownList { Label = item.Name, Value = item.Id.ToString() });
            }

            var bandVM = new List<DropDownList>();
            var bandList = _unitOfWork.Band.GetAll();
            foreach (var item in bandList)
            {
                bandVM.Add(new DropDownList { Label = item.Name, Value = item.Id.ToString() });
            }

            var titleVM = new List<DropDownList>();
            var titleList = _unitOfWork.Title.GetAll();
            foreach (var item in titleList)
            {
                titleVM.Add(new DropDownList { Label = item.Name, Value = item.Id.ToString() });
            }
            return Tuple.Create(desinationVM, groupVM, bandVM, titleVM);
        }

    }
}
