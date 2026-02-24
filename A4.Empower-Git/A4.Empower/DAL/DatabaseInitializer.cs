using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using DAL.Core;
using DAL.Core.Interfaces;
using A4.DAL.Entites;
using A4.DAL.Core;
using DAL;
using System.Linq;
using System.Collections.Generic;

namespace A4.DAL
{
    public interface IDatabaseInitializer
    {
        Task SeedAsync();
    }

    public class DatabaseInitializer : IDatabaseInitializer
    {
        private readonly ApplicationDbContext _context;
        private readonly IAccountManager _accountManager;
        private readonly ILogger _logger;

        public DatabaseInitializer(ApplicationDbContext context, IAccountManager accountManager, ILogger<DatabaseInitializer> logger)
        {
            _accountManager = accountManager;
            _context = context;
            _logger = logger;
        }

        public async Task SeedAsync()
        {
            try
            {
                await _context.Database.MigrateAsync().ConfigureAwait(false);

                if (!await _context.Users.AnyAsync())
                {
                    _logger.LogInformation("Generating inbuilt accounts");

                    const string adminRoleName = "administrator";
                    await EnsureRoleAsync(adminRoleName, "Adminstrator", ApplicationPermissions.GetAllPermissionValues());

                    await CreateUserAsync("admin", "A4Tech@12345", "Empower Admin", "admin@a4technology.com", "9000000000", new string[] { adminRoleName },true);

                    CreateLeaveStatus();

                    CreateExpenseBookingCategory();

                    CreateExpenseBookingSubCategory();

                    CreatePerformanceStatus();

                    CreateApplicationModule();

                    CreateApplicationModuleDetail();

                    _logger.LogInformation("Inbuilt account generation completed");
                }
            }
            catch (Exception ex)
            {

                throw;
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

        private async Task<ApplicationUser> CreateUserAsync(string username, string password, string fullName, string email, string phoneNumber, string[] roles,bool IsSuperAdmin)
        {
            ApplicationUser applicationUser = new ApplicationUser
            {
                UserName = username,
                FullName = fullName,
                Email = email,
                PhoneNumber = phoneNumber,
                EmailConfirmed = true,
                IsSuperAdmin=true,
                IsEnabled = true
            };

            var result = await _accountManager.CreateUserAsync(applicationUser, roles, password);
            if (result.Item1)
            {
                _context.SaveChanges();
            }
            _context.SaveChanges();
            if (!result.Item1)
                throw new Exception($"Seeding \"{fullName}\" user failed. Errors: {string.Join(Environment.NewLine, result.Item2)}");

            return applicationUser;
        }

        private void CreateLeaveStatus()
        {
            List<LeaveStatus> status = new List<LeaveStatus>();
            status.Add(new LeaveStatus() { Name = "Pending" });
            status.Add(new LeaveStatus() { Name = "Approved" });
            status.Add(new LeaveStatus() { Name = "Rejected" });
            status.Add(new LeaveStatus() { Name = "Cancelled" });
            _context.LeaveStatus.AddRange(status);
            _context.SaveChanges();
        }

        private void CreateExpenseBookingCategory()
        {
            List<ExpenseBookingCategory>category = new List<ExpenseBookingCategory>();
            category.Add(new ExpenseBookingCategory() { Name = "Office Supplies" });
            category.Add(new ExpenseBookingCategory() { Name = "Travel Cost" });
            category.Add(new ExpenseBookingCategory() { Name = "Utility Cost" });
            category.Add(new ExpenseBookingCategory() { Name = "Business Engagement Cost" });
            _context.ExpenseBookingCategory.AddRange(category);
            _context.SaveChanges();
        }

        private void CreateExpenseBookingSubCategory()
        {
            List<ExpenseBookingSubCategory> subCategory = new List<ExpenseBookingSubCategory>();
            var category = _context.ExpenseBookingCategory.Where(m => m.IsActive == true).ToList();
            if (category.Count() > 0)
            {
                foreach (var item in category)
                {
                    if (item.Name == "Office Supplies")
                    {
                        subCategory.Add(new ExpenseBookingSubCategory() { Name = "Stationary", ExpenseBookingCategoryId = item.Id });
                        subCategory.Add(new ExpenseBookingSubCategory() { Name = "Pantry", ExpenseBookingCategoryId = item.Id });
                        subCategory.Add(new ExpenseBookingSubCategory() { Name = "Toiletries", ExpenseBookingCategoryId = item.Id });
                    }else if (item.Name == "Travel Cost")
                    {
                        subCategory.Add(new ExpenseBookingSubCategory() { Name = "Fare", ExpenseBookingCategoryId = item.Id });
                        subCategory.Add(new ExpenseBookingSubCategory() { Name = "Lodging", ExpenseBookingCategoryId = item.Id });
                        subCategory.Add(new ExpenseBookingSubCategory() { Name = "Boarding", ExpenseBookingCategoryId = item.Id });
                    }
                    else if (item.Name == "Utility Cost")
                    {
                        subCategory.Add(new ExpenseBookingSubCategory() { Name = "Rent", ExpenseBookingCategoryId = item.Id });
                        subCategory.Add(new ExpenseBookingSubCategory() { Name = "Communication Cost", ExpenseBookingCategoryId = item.Id });
                        subCategory.Add(new ExpenseBookingSubCategory() { Name = "Technology Utilities", ExpenseBookingCategoryId = item.Id });
                    }
                    else if (item.Name == "Business Engagement Cost")
                    {
                        subCategory.Add(new ExpenseBookingSubCategory() { Name = "Employee Engagement Cost", ExpenseBookingCategoryId = item.Id });
                        subCategory.Add(new ExpenseBookingSubCategory() { Name = "Vendors/Channel Partners", ExpenseBookingCategoryId = item.Id });
                        subCategory.Add(new ExpenseBookingSubCategory() { Name = "Gifts", ExpenseBookingCategoryId = item.Id });
                    }
                }
                _context.ExpenseBookingSubCategory.AddRange(subCategory);
                _context.SaveChanges();
            }
          
        }

        private void CreatePerformanceStatus()
        {
            List<PerformanceStatus> status = new List<PerformanceStatus>();
            status.Add(new PerformanceStatus() { ColorCode = "#00B0F0" ,StatusText = "Rating Signed Off" ,Type = "RatingSignedOff" });
            status.Add(new PerformanceStatus() { ColorCode = "#00B0F0", StatusText = "Manager review is in progress", Type = "Managersavedgoals" });
            status.Add(new PerformanceStatus() { ColorCode = "#00B0F0", StatusText = "Employee self assesment in progress", Type = "EmployeeSaved" });
            status.Add(new PerformanceStatus() { ColorCode = "#00B0F0", StatusText = "Awating to set initial rating", Type = "Notstarted" });
            status.Add(new PerformanceStatus() { ColorCode = "#00B0F0", StatusText = "Awaiting  to release goal", Type = "AwaitingToReleaseGoal" });
            status.Add(new PerformanceStatus() { ColorCode = "#00b050", StatusText = "Not started", Type = "GoalReleased" });
            status.Add(new PerformanceStatus() { ColorCode = "#00b050", StatusText = "Final Review is submitted to president council", Type = "ManagerSignsoff" });
            status.Add(new PerformanceStatus() { ColorCode = "#00B0F0", StatusText = "Awaiting manager review", Type = "EmployeeSubmitted" });
            status.Add(new PerformanceStatus() { ColorCode = "#00b050", StatusText = "Final review submitted to HR", Type = "Presidentscouncilsignsoff" });
            status.Add(new PerformanceStatus() { ColorCode = "#00b050", StatusText = "Final review submitted to manager", Type = "EmployeeSignsoff" });
            status.Add(new PerformanceStatus() { ColorCode = "#00b050", StatusText = "Released", Type = "Allmangerreleasedprocessforemployee" });
            status.Add(new PerformanceStatus() { ColorCode = "#00b050", StatusText = "Final review assigned ", Type = "RatingsassignedandsavedbyManager" });
            status.Add(new PerformanceStatus() { ColorCode = "#ff0000", StatusText = "Released & Awating for CEO Signoff", Type = "AwaitingCEOSignOff" });
            _context.PerformanceStatus.AddRange(status);
            _context.SaveChanges();
        }

        private void CreateApplicationModule()
        {
            List<ApplicationModule> module = new List<ApplicationModule>();
            module.Add(new ApplicationModule() { ModuleName  = "Timesheet",Type= "timesheet" });
            module.Add(new ApplicationModule() { ModuleName = "Leave", Type = "leave" });
            module.Add(new ApplicationModule() { ModuleName = "Performance", Type = "performance" });
            module.Add(new ApplicationModule() { ModuleName = "Expense Booking", Type = "expanseManagement" });
            module.Add(new ApplicationModule() { ModuleName = "Recruitment", Type = "recruitment" });
            module.Add(new ApplicationModule() { ModuleName = "Sales & Marketing", Type = "salesMarketing" });
            _context.ApplicationModule.AddRange(module);
            _context.SaveChanges();
        }

        private void CreateApplicationModuleDetail()
        {
            List<ApplicationModuleDetail> moduleDetail = new List<ApplicationModuleDetail>();
            var module = _context.ApplicationModule.Where(m => m.IsActive == true && m.ModuleName == "Performance").FirstOrDefault();
            if (module != null)
            {
                moduleDetail.Add(new ApplicationModuleDetail() { SubModuleName = "Enable President council" ,ApplicationModuleId = module.Id,ConfigType= "EnablePresidentCouncil" });
                moduleDetail.Add(new ApplicationModuleDetail() { SubModuleName = "Enable Initial rating", ApplicationModuleId = module.Id,ConfigType= "EnableIntialRating" });
                moduleDetail.Add(new ApplicationModuleDetail() { SubModuleName = "Enable Delta and Pluses", ApplicationModuleId = module.Id, ConfigType = "EnableDeltaAndPluses" });
                moduleDetail.Add(new ApplicationModuleDetail() { SubModuleName = "Enable MidYear", ApplicationModuleId = module.Id, ConfigType = "EnableMidYear" });
                moduleDetail.Add(new ApplicationModuleDetail() { SubModuleName = "Enable Training & Classes", ApplicationModuleId = module.Id, ConfigType = "EnableTrainingAndClasses" });
                moduleDetail.Add(new ApplicationModuleDetail() { SubModuleName = "Enable Super Admin", ApplicationModuleId = module.Id, ConfigType = "EnableSuperAdmin" });
                moduleDetail.Add(new ApplicationModuleDetail() { SubModuleName = "Enable Next Year", ApplicationModuleId = module.Id, ConfigType = "EnableNextYear" });
            }
            _context.ApplicationModuleDetail.AddRange(moduleDetail);
            _context.SaveChanges();
        }

    }
}
