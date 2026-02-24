export type PermissionNames =
  "Manage Employee" | "Manage Department " | "Manage Group" | "Manage Designation" | "Manage Title" | "Manage Role" | "Manage Band" |
  "Manage Process Salary" | "Manage Check Salary" | "Manage Salary Component" | "Manage All Employee Salary" | "Manage Salary Component List" |
  "Manage My Leave" | "Manage Leave" | "Manage Hr Leave" | "Manage My Attendance" | "Manage Attendance" | "Manage Attendance Detail" |
  "Manage Upload Attendance Detail" | "Manage Attendance Summary" | "Manage Upload Attendance Summary" |
  "Manage My Timesheet" | "Manage Timesheet" |
  "Manage Hr View" | "Manage Set Goal" | "Manage My Goal" | "Manage Review Goal" |
  "Manage Recruitment Dashboard" | "Manage Job Vacancy" | "Manage Candidate View " | "Manage Interview" | "Manage Bulk Scheduling" | "Manage Job Vaccancy List" |
  "Manage Expense Booking" |
  "Manage Sales Marketing" |
  "Manage Configuration" |
  "Manage Blog";

export type PermissionValues =
  "employee.manage" | "department.manage" | "group.manage" | "designation.manage" | "title.manage" | "role.manage" | "band.manage" | "expenseitem.manage" |
  "processSalary.manage" | "checkSalary.manage" | "salaryComponent.manage" | "allEmployeeSalary.manage" | "salaryComponentList.manage" |
  "myleave.manage" | "manageleave.manage" | "hrleave.manage" | "myattendance.manage" | "manageattendance.manage" |
  "manageattendancedetail.manage" | "manageuploadattendancedetail.manage" | "manageattendancesummary.manage" | "manageuploadattendancesummary.manage" |
  "mytimesheet.manage" | "managetimesheet.manage" |
  "hrview.manage" | "setgoal.manage" | "mygoal.manage" | "reviewgoal.manage" |
  "recruitmentdashboard.manage" | "jobvaccany.manage" | "candidate.manage" | "manageinterview.manage" | "bulkscheduling.manage" | "managejobvaccancylist.manage" |
  "expensebooking.manage" |
  "approvedbooking.manage" |
  "salesmarketing.manage" |
  "configuration.manage" |
  "blog.manage";

export class Permission
{

  public static readonly manageEmployee: PermissionValues = "employee.manage";
  public static readonly manageDepartment: PermissionValues = "department.manage";
  public static readonly manageGroup: PermissionValues = "group.manage";
  public static readonly manageDesignation: PermissionValues = "designation.manage";
  public static readonly manageTitle: PermissionValues = "title.manage";
  public static readonly manageRole: PermissionValues = "role.manage";
  public static readonly manageBand: PermissionValues = "band.manage";

  public static readonly manageProcessSalary: PermissionValues = "processSalary.manage";
  public static readonly manageCheckSalary: PermissionValues = "checkSalary.manage";
  public static readonly manageSalaryComponent: PermissionValues = "salaryComponent.manage";
  public static readonly manageAllEmployeeSalary: PermissionValues = "allEmployeeSalary.manage";
  public static readonly manageSalaryComponentList: PermissionValues = "salaryComponentList.manage";

  public static readonly manageExpenseItem: PermissionValues = "expenseitem.manage";

  public static readonly manageMyLeave: PermissionValues = "myleave.manage";
  public static readonly manageLeave: PermissionValues = "manageleave.manage";
  public static readonly manageHrLeave: PermissionValues = "hrleave.manage";
  public static readonly manageMyAttendance: PermissionValues = "myattendance.manage";
  public static readonly manageAttendance: PermissionValues = "manageattendance.manage";
  public static readonly manageAttendanceDetail: PermissionValues = "manageattendancedetail.manage";
  public static readonly manageUploadAttendanceDetail: PermissionValues = "manageuploadattendancedetail.manage";
  public static readonly manageAttendanceSummary: PermissionValues = "manageattendancesummary.manage";
  public static readonly manageUploadAttendanceSummary: PermissionValues = "manageuploadattendancesummary.manage";

  public static readonly manageMyTimesheet: PermissionValues = "mytimesheet.manage";
  public static readonly manageTimesheet: PermissionValues = "managetimesheet.manage";

  public static readonly manageHrView: PermissionValues = "hrview.manage";
  public static readonly manageSetGoal: PermissionValues = "setgoal.manage";
  public static readonly manageMyGoal: PermissionValues = "mygoal.manage";
  public static readonly manageReviewGoal: PermissionValues = "reviewgoal.manage";


  public static readonly manageRecruitmentDashBoard: PermissionValues = "recruitmentdashboard.manage";
  public static readonly manageJobVaccancy: PermissionValues = "jobvaccany.manage";
  public static readonly manageCandidateView: PermissionValues = "candidate.manage";
  public static readonly manageInterview: PermissionValues = "manageinterview.manage";
  public static readonly manageBulkScheduling: PermissionValues = "bulkscheduling.manage";
  public static readonly manageJobVaccancyList: PermissionValues = "managejobvaccancylist.manage";

  public static readonly manageExpenseBooking: PermissionValues = "expensebooking.manage";
  public static readonly manageExpenseApproved: PermissionValues = "approvedbooking.manage";

  public static readonly manageSalesMarketing: PermissionValues = "salesmarketing.manage";


  public static readonly manageConfiguration: PermissionValues = "configuration.manage";

  public static readonly manageBlog: PermissionValues = "blog.manage";

  constructor(name?: PermissionNames, value?: PermissionValues, groupName?: string, description?: string)
  {
    this.name = name;
    this.value = value;
    this.groupName = groupName;
    this.description = description;
  }

  public name: PermissionNames;
  public value: PermissionValues;
  public groupName: string;
  public description: string;
}
