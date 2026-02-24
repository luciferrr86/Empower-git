using A4.DAL;
using A4.DAL.Entites;
using A4.DAL.Repositories;
using DAL.Repositories.Interfaces;

namespace DAL
{
    public interface IUnitOfWork
    {
        IExcelCandidateDataRepository ExcelCandidateData { get; }
        ISalaryPartRepository SalaryPart { get; }
        ICtcOtherComponentRepository CtcOtherComponent { get; }
        IAttendenceSummaryRepository AttendenceSummary { get; }
        IEmployeeAttendenceRepository EmployeeAttendence { get; }
        ISalaryComponentRepository SalaryComponent { get; }
        IEmployeeCTCRepository EmployeeCTC { get; }
        IEmployeeSalaryRepository EmployeeSalary { get; }
        IEmployeeRepository Employee { get; }
        IDepartmentRepository Department { get; }
        IDesignationRepository Designation { get; }
        IGroupRepository Group { get; }
        ITitleRepository Title { get; }
        IBandRepository Band { get; }
        IProfileRepository Profile { get; }

        IJobTypeRepository JobType { get; }
        IEmailDirectoryRepository EmailDirectoryRepository { get; }
        IJobInterviewTypeRepository JobInterviewType { get; }
        IJobVacancyRepository JobVacancy { get; }
        IJobCandidateProfileRepository JobCandidateProfile { get; }
        IJobApplicationRepository JobApplication { get; }
        IJobRepository Job { get; }
        IFileUploadRepository FileUpload { get; }
        IMassSchedulingRepository MassScheduling { get; }

        IJobApplicationHRQuestionsRepository JobApplicationHRQuestions { get; }
        IJobApplicationScreeningQuestionRepository JobApplicationScreeningQuestion { get; }
        IJobApplicationSkillQuestionRepository JobApplicationSkillQuestion { get; }
        IJobCandidateInterviewRepository JobCandidateInterview { get; }
        IJobCandidateQualificationRepository JobCandidateQualification { get; }
        IJobCandidateWorkExperienceRepository JobCandidateWorkExperience { get; }
        IJobHRQuestionRepository JobHRQuestion { get; }
        IJobScreeningQuestionRepository JobScreeningQuestion { get; }
        IJobSkillQuestionRepository JobSkillQuestion { get; }
        IJobVacancyLevelManagerRepository JobVacancyLevelManager { get; }
        IJobVacancyLevelRepository JobVacancyLevel { get; }
        IJobVacancyLevelSkillQuestionRepository JobVacancyLevelSkillQuestion { get; }

        #region Leave
        ILeavePeriodRepository LeavePeriod { get; }
        ILeaveHolidayListRepository LeaveHolidayList { get; }
        ILeaveWorkingDayRepository LeaveWorkingDay { get; }
        ILeaveTypeRepository LeaveType { get; }
        ILeaveRulesRepository LeaveRules { get; }
        IEmployeeLeavesRepository EmployeeLeaves { get; }
        IEmployeeLeaveDetailRepository EmployeeLeaveDetail { get; }
        IEmployeeEntitlementRepository EmployeeEntitlement { get; }
        ILeaveManagementRepository LeaveManagement { get; }
        ILeaveStatusRepository LeaveStatus { get; }
        ILeaveHrViewRepository LeaveHrView { get; }
        ISubordinateLeaveRepository SubordinateLeave { get; }
        ILeaveModuleRepository LeaveModule { get; }
        #endregion

        IApplicationModuleRepository ApplicationModule { get; }
        IApplicationModuleDetailRepository ApplicationModuleDetail { get; }

        #region TimeSheet

        ITimesheetConfigurationRepository TimesheetConfiguration { get; }
        ITimesheetTemplateRepository TimesheetTemplate { get; }
        ITimesheetUserScheduleRepository TimesheetUserSchedule { get; }
        IClientRepository Client { get; }
        IProjectRepository Project { get; }
        ITimesheetEmployeeProjectRepository TimesheetEmployeeProject { get; }
        ITimesheetUserDetailRepository TimesheetUserDetail { get; }
        ITimesheetUserDetailProjectHourRepository TimesheetUserDetailProjectHour { get; }
        ITimeSheetModuleRepository TimeSheetModule { get; }
        ITimesheetUserSpanRepository TimesheetUserSpan { get; }
        #endregion

        #region Performance

        IPerformanceConfigRepository PerformanceConfig { get; }
        IPerformanceConfigRatingRepository PerformanceConfigRating { get; }
        IPerformanceConfigFeedbackRepository PerformanceConfigFeedback { get; }
        IPerformanceEmpDeltasRepository PerformanceEmpDeltas { get; }
        IPerformanceEmpDevGoalRepository PerformanceEmpDevGoal { get; }
        IPerformanceEmpFeedbackDetailRepository PerformanceEmpFeedbackDetail { get; }
        IPerformanceEmpFeedbackRepository PerformanceEmpFeedback { get; }
        IPerformanceEmpGoalNextYearRepository PerformanceEmpGoalNextYear { get; }
        IPerformanceEmpGoalPresidentRepository PerformanceEmpGoalPresident { get; }
        IPerformanceEmpGoalRepository PerformanceEmpGoal { get; }
        IPerformanceEmpInitialRatingRepository PerformanceEmpInitialRating { get; }
        IPerformanceEmpMidYearGoalDetailRepository PerformanceEmpMidYearGoalDetail { get; }
        IPerformanceEmpMidYearGoalRepository PerformanceEmpMidYearGoal { get; }
        IPerformanceEmpPlusesRepository PerformanceEmpPluses { get; }
        IPerformanceEmpTrainingClassesRepository PerformanceEmpTrainingClasses { get; }
        IPerformanceEmpYearGoalDetailRepository PerformanceEmpYearGoalDetail { get; }
        IPerformanceEmpYearGoalRepository PerformanceEmpYearGoal { get; }
        IPerformanceGoalMainRepository PerformanceGoalMain { get; }
        IPerformanceGoalMeasureIndivRepository PerformanceGoalMeasureIndiv { get; }
        IPerformanceGoalMeasureRepository PerformanceGoalMeasure { get; }
        IPerformanceGoalRepository PerformanceGoal { get; }
        IPerformanceInitailRatingRepository PerformanceInitailRating { get; }
        IPerformancePresidentCouncilRepository PerformancePresidentCouncil { get; }
        IPerformanceStatusRepository PerformanceStatus { get; }
        IPerformanceYearRepository PerformanceYear { get; }
        IPerformanceAppRepository PerformanceApp { get; }
        #endregion

        #region SalesMarketing
        ISalesCompanyRepository SalesCompany { get; }
        ISalesCompanyContactRepository SalesCompanyContact { get; }
        ISalesStatusRepository SalesStatus { get; }
        ISalesDailyCallRepository SalesDailyCall { get; }
        ISalesScheduleMeetingExternalRepository SalesScheduleMeetingExternal { get; }
        ISalesScheduleMeetingInternalRepository SalesScheduleMeetingInternal { get; }
        ISalesMinuteMeetingInternalRepository SalesMinuteMeetingInternal { get; }
        ISalesMinuteMeetingRepository SalesMinuteMeeting { get; }
        ISalesScheduleMeetingRepository SalesScheduleMeeting { get; }
        #endregion

        ICategoryRepository Category { get; }

        ISubCategoryRepository SubCategory { get; }

        ISubCategoryItemRepository SubCategoryItem { get; }

        IExpenseBookingTitleAmountRepository ExpenseBookingTitleAmount { get; }

        IExpenseBookingRequestRepository ExpenseBookingRequest { get; }

        IExpenseBookingApproverRepository ExpenseBookingApprover { get; }

        IExpenseBookingRequestDetailRepository ExpenseBookingRequestDetail { get; }

        IExpenseBookingInviteApproverRepository ExpenseBookingInviteApprover { get; }

        IExpenseBookingRequestDetailInviteRepository ExpenseBookingRequestDetailInvite { get; }


        IExpenseDocument ExpenseDocument { get; }

        IBlogRepository Blog { get; }

        int SaveChanges();
    }
}
