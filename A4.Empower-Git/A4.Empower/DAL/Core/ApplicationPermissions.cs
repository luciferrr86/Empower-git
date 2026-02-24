using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace A4.DAL.Core
{
    public static class ApplicationPermissions
    {
        public static ReadOnlyCollection<ApplicationPermission> AllPermissions;


        public const string MaintenancePermissionGroupName = "Maintenance";

        public static ApplicationPermission ManageEmployee = new ApplicationPermission("Employee", "employee.manage", MaintenancePermissionGroupName, "Permission to create, delete and modify other users account details");
        public static ApplicationPermission ManageDepartment = new ApplicationPermission("Functional Department", "department.manage", MaintenancePermissionGroupName, "Permission to create, delete and modify other users account details");
        public static ApplicationPermission ManageGroup = new ApplicationPermission("Functional Group", "group.manage", MaintenancePermissionGroupName, "Permission to create, delete and modify other users account details");
        public static ApplicationPermission ManageDesignation = new ApplicationPermission("Functional Designation", "designation.manage", MaintenancePermissionGroupName, "Permission to create, delete and modify other users account details");
        public static ApplicationPermission ManageTitle = new ApplicationPermission("Functional Title", "title.manage", MaintenancePermissionGroupName, "Permission to create, delete and modify other users account details");
        public static ApplicationPermission ManageRole = new ApplicationPermission("Role", "role.manage", MaintenancePermissionGroupName, "Permission to create, delete and modify other users account details");
        public static ApplicationPermission ManageBand = new ApplicationPermission("Band", "band.manage", MaintenancePermissionGroupName, "Permission to create, delete and modify other users account details");
        public static ApplicationPermission ManageProcessSalary = new ApplicationPermission("ProcessSalary", "processSalary.manage", MaintenancePermissionGroupName, "Permission to create, delete and modify other users account details");
        public static ApplicationPermission ManageCheckSalary = new ApplicationPermission("CheckSalary", "checkSalary.manage", MaintenancePermissionGroupName, "Permission to create, delete and modify other users account details");
        public static ApplicationPermission ManageSalaryComponent = new ApplicationPermission("SalaryComponent", "salaryComponent.manage", MaintenancePermissionGroupName, "Permission to create, delete and modify other users account details");
        public static ApplicationPermission ManageAllEmployeeSalary = new ApplicationPermission("AllEmployeeSalary", "allEmployeeSalary.manage", MaintenancePermissionGroupName, "Permission to create, delete and modify other users account details");
        public static ApplicationPermission ManageSalaryComponentList = new ApplicationPermission("SalaryComponentList", "salaryComponentList.manage", MaintenancePermissionGroupName, "Permission to create, delete and modify other users account details");

        public const string LeavePermissionGroupName = "Leave";

        public static ApplicationPermission ManageMyLeave = new ApplicationPermission("Manage My Leave", "myleave.manage", LeavePermissionGroupName, "Permission to view available roles");
        public static ApplicationPermission ManageLeave = new ApplicationPermission("Manage Leave", "manageleave.manage", LeavePermissionGroupName, "Permission to view available roles");
        public static ApplicationPermission ManageHrLeave = new ApplicationPermission("Manage Hr Leave", "hrleave.manage", LeavePermissionGroupName, "Permission to view available roles");
        public static ApplicationPermission ManageMyAttendance = new ApplicationPermission("Manage My Attendance", "myattendance.manage", LeavePermissionGroupName, "Permission to view available roles");
        public static ApplicationPermission ManageAttendance = new ApplicationPermission("Manage Attendance", "manageattendance.manage", LeavePermissionGroupName, "Permission to view available roles");
        public static ApplicationPermission ManageAttendanceDetail = new ApplicationPermission("Manage Attendance Detail", "manageattendancedetail.manage", LeavePermissionGroupName, "Permission to view available roles");
        public static ApplicationPermission ManageUploadAttendanceDetail = new ApplicationPermission("Manage Upload Attendance Detail", "manageuploadattendancedetail.manage", LeavePermissionGroupName, "Permission to view available roles");
        public static ApplicationPermission ManageAttendanceSummary = new ApplicationPermission("Manage Attendance Summary", "manageattendancesummary.manage", LeavePermissionGroupName, "Permission to view available roles");
        public static ApplicationPermission ManageUploadAttendanceSummary = new ApplicationPermission("Manage Upload Attendance Summary", "manageuploadattendancesummary.manage", LeavePermissionGroupName, "Permission to view available roles");

        public const string TimeSheetPermissionGroupName = "Timesheet";

        public static ApplicationPermission ManageMyTimesheet = new ApplicationPermission("My Timesheet", "mytimesheet.manage", TimeSheetPermissionGroupName, "Permission to view available roles");
        public static ApplicationPermission ManageTimesheet = new ApplicationPermission("Manage Timesheet", "managetimesheet.manage", TimeSheetPermissionGroupName, "Permission to view available roles");


        public const string PerformancePermissionGroupName = "Performance";

        public static ApplicationPermission ManageHrView = new ApplicationPermission("Hr View", "hrview.manage", PerformancePermissionGroupName, "Permission to view available roles");
        public static ApplicationPermission ManageSetGoal = new ApplicationPermission("Set Goal", "setgoal.manage", PerformancePermissionGroupName, "Permission to view available roles");
        public static ApplicationPermission ManageMyGoal = new ApplicationPermission("My Goal", "mygoal.manage", PerformancePermissionGroupName, "Permission to view available roles");
        public static ApplicationPermission ManageReviewGoal = new ApplicationPermission("Review Goal", "reviewgoal.manage", PerformancePermissionGroupName, "Permission to view available roles");


        public const string RecruitmentPermissionGroupName = "Recruitment";
        public static ApplicationPermission ManageRecruitmentDasboard = new ApplicationPermission("DashBoard", "recruitmentdashboard.manage", RecruitmentPermissionGroupName, "Permission to view available roles");
        public static ApplicationPermission ManageJobVaccancy = new ApplicationPermission("Job Vaccany", "jobvaccany.manage", RecruitmentPermissionGroupName, "Permission to view available roles");
       
        public static ApplicationPermission ManageInterview = new ApplicationPermission("Manage Interview", "manageinterview.manage", RecruitmentPermissionGroupName, "Permission to view available roles");
        public static ApplicationPermission ManageBulkScheduling = new ApplicationPermission("Bulk Scheduling", "bulkscheduling.manage", RecruitmentPermissionGroupName, "Permission to view available roles");
        public static ApplicationPermission ManageJobVaccancyList = new ApplicationPermission("Job Vaccancy List", "jobvaccancylist.manage", RecruitmentPermissionGroupName, "Permission to view available roles");

        public const string ExpenseBookingPermissionGroupName = "ExpenseBooking";
        public static ApplicationPermission ManageExpenseBooking = new ApplicationPermission("Expense Booking", "expensebooking.manage", ExpenseBookingPermissionGroupName, "Permission to view available roles");
        public static ApplicationPermission ManageApprovedBooking = new ApplicationPermission("Approved Booking", "approvedbooking.manage", ExpenseBookingPermissionGroupName, "Permission to view available roles");

        public const string SalesMarketingPermissionGroupName = "SalesMarketing";
        public static ApplicationPermission ManageSalesMarketing = new ApplicationPermission("Sales Marketing", "salesmarketing.manage", SalesMarketingPermissionGroupName, "Permission to view available roles");


        public const string ConfigurationPermissionGroupName = "Configuration";

        public static ApplicationPermission ManageConfiguration = new ApplicationPermission("Configuration", "configuration.manage", ConfigurationPermissionGroupName, "Permission to view available roles");

        public const string BlogPermissionGroupName = "Blog";
        public static ApplicationPermission ManageBlog = new ApplicationPermission("Blog", "blog.manage", BlogPermissionGroupName, "Permission to view available roles");

        static ApplicationPermissions()
        {
            List<ApplicationPermission> allPermissions = new List<ApplicationPermission>()
            {
                ManageEmployee,
                ManageDepartment,
                ManageGroup,
                ManageDesignation,
                ManageTitle,
                ManageRole,
                ManageBand,
                ManageProcessSalary,
                ManageCheckSalary,
                ManageSalaryComponent,
                ManageAllEmployeeSalary,
                ManageSalaryComponentList,

                ManageMyLeave,
                ManageLeave,
                ManageHrLeave,
                ManageMyAttendance,
                ManageAttendance,
                ManageAttendanceDetail,
                ManageUploadAttendanceDetail,
                ManageAttendanceSummary,
                ManageUploadAttendanceSummary,


                ManageMyTimesheet,
                ManageTimesheet,

                ManageMyGoal,
                ManageSetGoal,
                ManageReviewGoal,
                ManageHrView,

                ManageRecruitmentDasboard,
                ManageJobVaccancy,
                ManageInterview,
                ManageBulkScheduling,
                ManageJobVaccancyList,

                ManageExpenseBooking,
                ManageApprovedBooking,

                ManageSalesMarketing,

                ManageConfiguration,
                
                ManageBlog
            };

            AllPermissions = allPermissions.AsReadOnly();
        }

        public static ApplicationPermission GetPermissionByName(string permissionName)
        {
            return AllPermissions.Where(p => p.Name == permissionName).FirstOrDefault();
        }

        public static ApplicationPermission GetPermissionByValue(string permissionValue)
        {
            return AllPermissions.Where(p => p.Value == permissionValue).FirstOrDefault();
        }

        public static string[] GetAllPermissionValues()
        {
            return AllPermissions.Select(p => p.Value).ToArray();
        }

        public static string[] GetAdministrativePermissionValues()
        {
            return new string[] {
                ManageEmployee,
                ManageGroup,
                ManageRole,
                ManageTitle,
                ManageGroup,
                ManageDepartment,
                ManageBand,
                ManageDesignation,


                ManageMyLeave,
                ManageLeave,
                ManageHrLeave,
                ManageMyAttendance,
                ManageAttendance,

                ManageMyTimesheet,
                ManageTimesheet,

                ManageMyGoal,
                ManageSetGoal,
                ManageReviewGoal,
                ManageHrView,

                ManageRecruitmentDasboard,
                ManageInterview,
                ManageJobVaccancy,
                ManageBulkScheduling,
                ManageJobVaccancyList,

                ManageExpenseBooking,
                ManageApprovedBooking,

                ManageSalesMarketing,

                ManageConfiguration,

                ManageBlog
            };
        }
    }



    public class ApplicationPermission
    {
        public ApplicationPermission()
        { }

        public ApplicationPermission(string name, string value, string groupName, string description = null)
        {
            Name = name;
            Value = value;
            GroupName = groupName;
            Description = description;
        }



        public string Name { get; set; }
        public string Value { get; set; }
        public string GroupName { get; set; }
        public string Description { get; set; }


        public override string ToString()
        {
            return Value;
        }


        public static implicit operator string(ApplicationPermission permission)
        {
            return permission.Value;
        }
    }
}
