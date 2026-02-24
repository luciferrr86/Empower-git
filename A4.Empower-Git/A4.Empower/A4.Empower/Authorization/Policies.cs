using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A4.Empower.Authorization
{
    public class Policies
    {
        ///<summary>Policy to allow viewing all user records.</summary>
        public const string ManageEmployeePolicy = "Manage Employee";

        ///<summary>Policy to allow adding, removing and updating all user records.</summary>
        public const string ManageDepartmentPolicy = "Manage Functional Department";

        /// <summary>Policy to allow viewing details of all roles.</summary>
        public const string ManageRolePolicy = "Manage Roles";

        /// <summary>Policy to allow viewing details of all or specific roles (Requires roleName as parameter).</summary>
        public const string ManageDesignationPolicy = "Manage Designation";

        /// <summary>Policy to allow adding, removing and updating all roles.</summary>
        public const string ManageTitlePolicy = "Manage Title";

        /// <summary>Policy to allow assigning roles the user has access to (Requires new and current roles as parameter).</summary>
        public const string ManageBandPolicy = "Manage Band";

        public const string ManageGroupPolicy = "Manage Group";

        public const string ManageProcessSalaryPolicy = "Manage Process Salary";
        public const string ManageCheckSalaryPolicy = "Manage Check Salary";
        public const string ManageSalaryComponentPolicy = "Manage Salary Component";
        public const string ManageAllEmployeeSalaryPolicy = "Manage All Employee Salary";
        public const string ManageSalaryComponentListPolicy = "Manage Salary Component List";

        public const string ManageExpenseItemPolicy = "Manage Expense Item";

        public const string ManageMyLeavePolicy = "Manage My Leave";
        public const string ManageLeavePolicy = "Manage Leave";
        public const string ManageHrLeavePolicy = "Manage Hr Leave";
        public const string ManageMyAttendancePolicy = "Manage My Attendance";
        public const string ManageAttendancePolicy = "Manage Attendance";
        public const string ManageAttendanceDetailPolicy = "Manage Attendance Detail";
        public const string ManageUploadAttendanceDetailPolicy = "Manage Upload Attendance Detail";
        public const string ManageAttendanceSummaryPolicy = "Manage Attendance summary";
        public const string ManageUploadAttendanceSummaryPolicy = "Manage Upload Attendance Summary";

        public const string ManageMyTimeshhetPolicy = "Manage My Timesheet";
        public const string ManageTimesheetPolicy = "Manage Timesheet";

        public const string ManageHrViewPolicy = "Manage Hr View";
        public const string ManageSetGoalPolicy = "Manage Set Goal";
        public const string ManageMyGoalPolicy = "Manage My Goal";
        public const string ManageReviewGoalPolicy = "Manage Review Goal";

        public const string ManageRecruitmentDashBoardPolicy = "Manage Recruitment DashBoard";

        public const string ManageJobVaccancyPolicy = "Manage Job Vaccancy";
        public const string ManageCandidatePolicy = "Manage Candidate View";
        public const string ManageInterviewPolicy = "Manage Interview";
        public const string ManageBulkSchedulingPolicy = "Manage Bulk Scheduling";
        public const string ManageJobVacancyListPolicy = "Manage Job Vacancy List";

        public const string ManageExpenseBookingPolicy = "Manage Expense Booking";
        public const string ManageApprovedBookingPolicy = "Manage Approved Booking";

        public const string ManageSalesMarketingPolicy = "Manage Sales Marketing";

        public const string ManageConfigurationPolicy = "Manage Configuration";

        public const string ManageBlogPolicy = "Manage Blog";
    }

}
