using DAL.Repositories;
using DAL.Repositories.Interfaces;
using A4.DAL.Repositories;
using A4.DAL;

namespace DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        readonly ApplicationDbContext _context;
        IExcelCandidateDataRepository _excelCandidateDataRepository;
        ISalaryPartRepository _salaryPartRepository;
        ICtcOtherComponentRepository _ctcOtherComponentRepository;
        IAttendenceSummaryRepository _attendenceSummaryRepository;
        IEmployeeAttendenceRepository _employeeAttendenceRepository;
        ISalaryComponentRepository _salaryComponentRepository;
        IEmployeeCTCRepository _employeeCTCRepository;
        IEmployeeSalaryRepository _employeeSalary;
        IEmployeeRepository _employee;
        IDepartmentRepository _department; 
        IDesignationRepository _designation; 
        IGroupRepository _group;
        ITitleRepository _title;
        IBandRepository _band;
        IProfileRepository _profile;


        IJobTypeRepository _jobType;
        IEmailDirectoryRepository _emailDirectory;
        IJobInterviewTypeRepository _jobInterviewType;
        IJobVacancyRepository _jobVacancy;
        IJobCandidateProfileRepository _jobCandidateProfile;
        IJobApplicationRepository _jobApplication;
        IJobRepository _job;
        IFileUploadRepository _fileUpload;
        IMassSchedulingRepository _massScheduling;
        IJobApplicationHRQuestionsRepository _jobApplicationHRQuestions;
        IJobApplicationScreeningQuestionRepository _jobApplicationScreeningQuestion;
        IJobApplicationSkillQuestionRepository _jobApplicationSkillQuestion;
        IJobCandidateInterviewRepository _jobCandidateInterview;
        IJobCandidateQualificationRepository _jobCandidateQualification;
        IJobCandidateWorkExperienceRepository _jobCandidateWorkExperience;
        IJobHRQuestionRepository _jobHRQuestion;
        IJobScreeningQuestionRepository _jobScreeningQuestion;
        IJobSkillQuestionRepository _jobSkillQuestion;
        IJobVacancyLevelManagerRepository _jobVacancyLevelManager;
        IJobVacancyLevelRepository _jobVacancyLevel;
        IJobVacancyLevelSkillQuestionRepository _jobVacancyLevelSkillQuestion;


        IApplicationModuleRepository _applicationModule;
        IApplicationModuleDetailRepository _applicationModuleDetail;

        #region Performance
        IPerformanceConfigRepository _performanceConfig;
        IPerformanceConfigRatingRepository _performanceConfigRating;
        IPerformanceConfigFeedbackRepository _performanceConfigFeedback;
        IPerformanceEmpDeltasRepository _performanceEmpDeltas;       
        IPerformanceEmpDevGoalRepository _performanceEmpDevGoal;
        IPerformanceEmpFeedbackDetailRepository _performanceEmpFeedbackDetail;
        IPerformanceEmpFeedbackRepository _performanceEmpFeedback;
        IPerformanceEmpGoalNextYearRepository _performanceEmpGoalNextYear;
        IPerformanceEmpGoalPresidentRepository _performanceEmpGoalPresident;
        IPerformanceEmpGoalRepository _performanceEmpGoal;
        IPerformanceEmpInitialRatingRepository _performanceEmpInitialRating;
        IPerformanceEmpMidYearGoalDetailRepository _performanceEmpMidYearGoalDetail;
        IPerformanceEmpMidYearGoalRepository _performanceEmpMidYearGoal;
        IPerformanceEmpPlusesRepository _performanceEmpPluses;
        IPerformanceEmpTrainingClassesRepository _performanceEmpTrainingClasses;
        IPerformanceEmpYearGoalDetailRepository _performanceEmpYearGoalDetail;
        IPerformanceEmpYearGoalRepository _performanceEmpYearGoal;
        IPerformanceGoalMainRepository _performanceGoalMain;
        IPerformanceGoalMeasureIndivRepository _performanceGoalMeasureIndiv;
        IPerformanceGoalMeasureRepository _performanceGoalMeasure;
        IPerformanceGoalRepository _performanceGoal;
        IPerformanceInitailRatingRepository _performanceInitailRating;
        IPerformancePresidentCouncilRepository _performancePresidentCouncil;
        IPerformanceStatusRepository _performanceStatus;
        IPerformanceYearRepository _performanceYear;
        IPerformanceAppRepository _performanceApp;
        
        #endregion

        #region Leave
        ILeavePeriodRepository _leavePeriod;
        ILeaveHolidayListRepository _leaveHolidayList;
        ILeaveWorkingDayRepository _leaveWorkingDay;
        ILeaveTypeRepository _leaveType;
        ILeaveRulesRepository _leaveRules;
        IEmployeeLeavesRepository _employeeLeaves;
        IEmployeeLeaveDetailRepository _employeeLeaveDetail;
        IEmployeeEntitlementRepository _employeeEntitlement;
        ILeaveManagementRepository _leaveManagement;
        ILeaveStatusRepository _leaveStatus;
        ILeaveHrViewRepository _leaveHrView;
        ISubordinateLeaveRepository _subordinateLeave;
        ILeaveModuleRepository _leaveModule;
        #endregion

        #region TimeSheet
        ITimesheetConfigurationRepository _timesheetConfiguration;
        ITimesheetTemplateRepository _timesheetTemplate;
        ITimesheetUserScheduleRepository _timesheetUserSchedule;
        IClientRepository _client;
        IProjectRepository _project;
        ITimesheetEmployeeProjectRepository _timesheetEmployeeProject;
        ITimesheetUserDetailRepository _timesheetUserDetail;
        ITimesheetUserDetailProjectHourRepository _timesheetUserDetailProjectHour;
        ITimeSheetModuleRepository _timeSheetModule;
        ITimesheetUserSpanRepository _timesheetUserSpan;
        #endregion

        #region SalesMarketing
        ISalesCompanyRepository _salesCompany;
        ISalesCompanyContactRepository _salesCompanyContact;
        ISalesStatusRepository _salesStatus;
        ISalesDailyCallRepository _salesDailyCall;
        ISalesScheduleMeetingExternalRepository _salesScheduleMeetingExternal;
        ISalesScheduleMeetingInternalRepository _salesScheduleMeetingInternal;
        ISalesMinuteMeetingInternalRepository _salesMinuteMeetingInternal;
        ISalesMinuteMeetingRepository _salesMinuteMeeting;
        ISalesScheduleMeetingRepository _salesScheduleMeeting;
        #endregion



        IExpenseBookingRequestRepository _expenseBookingRequest;
        IExpenseBookingRequestDetailRepository _expenseBookingRequestDetail;
        IExpenseBookingTitleAmountRepository  _expenseBookingTitleAmountRepository;

        ISubCategoryItemRepository _subCategoryItem;
        ISubCategoryRepository _subCategory;
        ICategoryRepository _category;
        IExpenseBookingApproverRepository _expenseBookingApprover;
        IExpenseBookingInviteApproverRepository _expenseBookingInviteApprover;
        IExpenseBookingRequestDetailInviteRepository _expenseBookingRequestDetailInvite;
        IExpenseDocument _expenseDocument;

        IBlogRepository _blog;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        public IExcelCandidateDataRepository ExcelCandidateData
        {
            get
            {
                if (_excelCandidateDataRepository == null)
                    _excelCandidateDataRepository = new ExcelCandidateDataRepository(_context);

                return _excelCandidateDataRepository;
            }
        }



        public ISalaryPartRepository SalaryPart
        {
            get
            {
                if (_salaryPartRepository == null)
                    _salaryPartRepository = new SalaryPartRepository(_context);

                return _salaryPartRepository;
            }
        }

        public ICtcOtherComponentRepository CtcOtherComponent
        {
            get
            {
                if (_ctcOtherComponentRepository == null)
                    _ctcOtherComponentRepository = new CtcOtherComponentRepository(_context);

                return _ctcOtherComponentRepository;
            }
        }
        public IAttendenceSummaryRepository AttendenceSummary
        {
            get
            {
                if (_attendenceSummaryRepository == null)
                    _attendenceSummaryRepository = new AttendenceSummaryRepository(_context);

                return _attendenceSummaryRepository;
            }
        }
        public IEmployeeAttendenceRepository EmployeeAttendence
        {
            get
            {
                if (_employeeAttendenceRepository == null)
                    _employeeAttendenceRepository = new EmployeeAttendenceRepository(_context);

                return _employeeAttendenceRepository;
            }
        }
        public ISalaryComponentRepository SalaryComponent
        {
            get
            {
                if (_salaryComponentRepository == null)
                    _salaryComponentRepository = new SalaryComponentRepository(_context);

                return _salaryComponentRepository;
            }
        }
        public IEmployeeCTCRepository EmployeeCTC
        {
            get
            {
                if (_employeeCTCRepository == null)
                    _employeeCTCRepository = new EmployeeCTCRepository(_context);

                return _employeeCTCRepository;
            }
        }

        public IEmployeeSalaryRepository EmployeeSalary
        {
            get
            {
                if (_employeeSalary == null)
                    _employeeSalary = new EmployeeSalaryRepository(_context);

                return _employeeSalary;
            }
        }
        public ITitleRepository Title
        {
            get
            {
                if (_title == null)
                    _title = new TitleRepository(_context);

                return _title;
            }
        }
        public IGroupRepository Group
        {
            get
            {
                if (_group == null)
                    _group = new GroupRepository(_context);

                return _group;
            }
        }
        public IDesignationRepository Designation
        {
            get
            {
                if (_designation == null)
                    _designation = new DesignationRepository(_context);

                return _designation;
            }
        }

        public IDepartmentRepository Department
        {
            get
            {
                if (_department == null)
                    _department = new DepartmentRepository(_context);

                return _department;
            }
        }

        public IEmployeeRepository Employee
        {
            get
            {
                if (_employee == null)
                    _employee = new EmployeeRepository(_context);

                return _employee;
            }
        }
        public IBandRepository Band
        {
            get
            {
                if (_band == null)
                    _band = new BandRepository(_context);

                return _band;
            }
        }
        public IProfileRepository Profile
        {
            get
            {
                if (_profile == null)
                    _profile = new ProfileRepository(_context);

                return _profile;
            }
        }

        public IJobTypeRepository JobType
        {
            get
            {
                if (_jobType == null)
                    _jobType = new JobTypeRepository(_context);

                return _jobType;
            }
        }

        public IJobInterviewTypeRepository JobInterviewType
        {
            get
            {
                if (_jobInterviewType == null)
                    _jobInterviewType = new JobInterviewTypeRepository(_context);

                return _jobInterviewType;
            }
        }
        public IJobVacancyRepository JobVacancy
        {
            get
            {
                if (_jobVacancy == null)
                    _jobVacancy = new JobVacancyRepository(_context);

                return _jobVacancy;
            }
        }


        public IJobApplicationHRQuestionsRepository JobApplicationHRQuestions
        {
            get
            {
                if (_jobApplicationHRQuestions == null)
                    _jobApplicationHRQuestions = new JobApplicationHRQuestionsRepository(_context);

                return _jobApplicationHRQuestions;
            }

        }

        public IJobApplicationScreeningQuestionRepository JobApplicationScreeningQuestion
        {
            get
            {
                if (_jobApplicationScreeningQuestion == null)
                    _jobApplicationScreeningQuestion = new JobApplicationScreeningQuestionRepository(_context);

                return _jobApplicationScreeningQuestion;
            }

        }

        public IJobApplicationSkillQuestionRepository JobApplicationSkillQuestion
        {
            get
            {
                if (_jobApplicationSkillQuestion == null)
                    _jobApplicationSkillQuestion = new JobApplicationSkillQuestionRepository(_context);

                return _jobApplicationSkillQuestion;
            }

        }

        public IJobCandidateInterviewRepository JobCandidateInterview
        {
            get
            {
                if (_jobCandidateInterview == null)
                    _jobCandidateInterview = new JobCandidateInterviewRepository(_context);

                return _jobCandidateInterview;
            }

        }

        public IJobCandidateQualificationRepository JobCandidateQualification
        {
            get
            {
                if (_jobCandidateQualification == null)
                    _jobCandidateQualification = new JobCandidateQualificationRepository(_context);

                return _jobCandidateQualification;
            }

        }

        public IJobCandidateWorkExperienceRepository JobCandidateWorkExperience
        {
            get
            {
                if (_jobCandidateWorkExperience == null)
                    _jobCandidateWorkExperience = new JobCandidateWorkExperienceRepository(_context);

                return _jobCandidateWorkExperience;
            }

        }

        public IJobHRQuestionRepository JobHRQuestion
        {
            get
            {
                if (_jobHRQuestion == null)
                    _jobHRQuestion = new JobHRQuestionRepository(_context);

                return _jobHRQuestion;
            }

        }

        public IJobScreeningQuestionRepository JobScreeningQuestion
        {
            get
            {
                if (_jobScreeningQuestion == null)
                    _jobScreeningQuestion = new JobScreeningQuestionRepository(_context);

                return _jobScreeningQuestion;
            }

        }

        public IJobSkillQuestionRepository JobSkillQuestion
        {
            get
            {
                if (_jobSkillQuestion == null)
                    _jobSkillQuestion = new JobSkillQuestionRepository(_context);

                return _jobSkillQuestion;
            }

        }

        public IJobVacancyLevelManagerRepository JobVacancyLevelManager
        {
            get
            {
                if (_jobVacancyLevelManager == null)
                    _jobVacancyLevelManager = new JobVacancyLevelManagerRepository(_context);

                return _jobVacancyLevelManager;
            }

        }

        public IJobVacancyLevelRepository JobVacancyLevel
        {
            get
            {
                if (_jobVacancyLevel == null)
                    _jobVacancyLevel = new JobVacancyLevelRepository(_context);

                return _jobVacancyLevel;
            }

        }

        public IEmailDirectoryRepository EmailDirectoryRepository
        {
            get
            {
                if (_emailDirectory == null)
                    _emailDirectory = new EmailDirectoryRepository(_context);

                return _emailDirectory;
            }

        }
        public IJobVacancyLevelSkillQuestionRepository JobVacancyLevelSkillQuestion
        {
            get
            {
                if (_jobVacancyLevelSkillQuestion == null)
                    _jobVacancyLevelSkillQuestion = new JobVacancyLevelSkillQuestionRepository(_context);

                return _jobVacancyLevelSkillQuestion;
            }

        }

        public IJobCandidateProfileRepository JobCandidateProfile
        {
            get
            {
                if (_jobCandidateProfile == null)
                    _jobCandidateProfile = new JobCandidateProfileRepository(_context);

                return _jobCandidateProfile;
            }
        }

        public IJobApplicationRepository JobApplication
        {
            get
            {
                if (_jobApplication == null)
                    _jobApplication = new JobApplicationRepository(_context);

                return _jobApplication;
            }
        }

        public IJobRepository Job
        {
            get
            {
                if (_job == null)
                    _job = new JobRepository(_context);
                return _job;
            }
        }

        public IMassSchedulingRepository MassScheduling
        {
            get
            {
                if (_massScheduling == null)
                    _massScheduling = new MassSchedulingRepository(_context);
                return _massScheduling;
            }
        }

        public IFileUploadRepository FileUpload
        {
            get
            {
                if (_fileUpload == null)
                    _fileUpload = new FileUploadRepository(_context);
                return _fileUpload;
            }
        }

        public IApplicationModuleRepository ApplicationModule
        {
            get
            {
                if (_applicationModule == null)
                    _applicationModule = new ApplicationModuleRepository(_context);
                return _applicationModule;
            }
        }

        public IApplicationModuleDetailRepository ApplicationModuleDetail
        {
            get
            {
                if (_applicationModuleDetail == null)
                    _applicationModuleDetail = new ApplicationModuleDetailRepository(_context);
                return _applicationModuleDetail;
            }
        }

        public ILeavePeriodRepository LeavePeriod
        {
            get
            {
                if (_leavePeriod == null)
                    _leavePeriod = new LeavePeriodRepository(_context);

                return _leavePeriod;
            }
        }
        public ILeaveHolidayListRepository LeaveHolidayList
        {
            get
            {
                if (_leaveHolidayList == null)
                    _leaveHolidayList = new LeaveHolidayListRepository(_context);

                return _leaveHolidayList;
            }
        }
        public ILeaveWorkingDayRepository LeaveWorkingDay
        {
            get
            {
                if (_leaveWorkingDay == null)
                    _leaveWorkingDay = new LeaveWorkingDayRepository(_context);

                return _leaveWorkingDay;
            }
        }
        public ILeaveTypeRepository LeaveType
        {
            get
            {
                if (_leaveType == null)
                    _leaveType = new LeaveTypeRepository(_context);

                return _leaveType;
            }
        }
        public ILeaveRulesRepository LeaveRules
        {
            get
            {
                if (_leaveRules == null)
                    _leaveRules = new LeaveRulesRepository(_context);

                return _leaveRules;
            }
        }
        public IEmployeeLeavesRepository EmployeeLeaves
        {
            get
            {
                if (_employeeLeaves == null)
                    _employeeLeaves = new EmployeeLeavesRepository(_context);

                return _employeeLeaves;
            }
        }
        public IEmployeeLeaveDetailRepository EmployeeLeaveDetail
        {
            get
            {
                if (_employeeLeaveDetail == null)
                    _employeeLeaveDetail = new EmployeeLeaveDetailRepository(_context);

                return _employeeLeaveDetail;
            }
        }
        public IEmployeeEntitlementRepository EmployeeEntitlement
        {
            get
            {
                if (_employeeEntitlement == null)
                    _employeeEntitlement = new EmployeeEntitlementRepository(_context);

                return _employeeEntitlement;
            }
        }


        public ILeaveManagementRepository LeaveManagement
        {
            get
            {
                if (_leaveManagement == null)
                    _leaveManagement = new LeaveManagementRepository(_context);

                return _leaveManagement;
            }
        }
        public ISubordinateLeaveRepository SubordinateLeave
        {
            get
            {
                if (_subordinateLeave == null)
                    _subordinateLeave = new SubordinateLeaveRepository(_context);

                return _subordinateLeave;
            }
        }
        public ILeaveHrViewRepository LeaveHrView
        {
            get
            {
                if (_leaveHrView == null)
                    _leaveHrView = new LeaveHrViewRepository(_context);

                return _leaveHrView;
            }
        }

        public ILeaveModuleRepository LeaveModule
        {
            get
            {
                if (_leaveModule == null)
                    _leaveModule = new LeaveModuleRepository(_context);

                return _leaveModule;
            }
        }

        public ITimesheetConfigurationRepository TimesheetConfiguration
        {
            get
            {
                if (_timesheetConfiguration == null)
                    _timesheetConfiguration = new TimesheetConfigurationRepository(_context);
                return _timesheetConfiguration;
            }
        }

        public ILeaveStatusRepository LeaveStatus
        {
            get
            {
                if (_leaveStatus == null)
                    _leaveStatus = new LeaveStatusRepository(_context);
                return _leaveStatus;
            }
        }


        public ITimesheetTemplateRepository TimesheetTemplate
        {
            get
            {
                if (_timesheetTemplate == null)
                    _timesheetTemplate = new TimesheetTemplateRepository(_context);
                return _timesheetTemplate;
            }
        }

        public ITimesheetUserScheduleRepository TimesheetUserSchedule
        {
            get
            {
                if (_timesheetUserSchedule == null)
                    _timesheetUserSchedule = new TimesheetUserScheduleRepository(_context);
                return _timesheetUserSchedule;
            }
        }

        public IClientRepository Client
        {
            get
            {
                if (_client == null)
                    _client = new ClientRepository(_context);
                return _client;
            }
        }

        #region TimeSheet
        public ITimesheetUserDetailRepository TimesheetUserDetail
        {
            get
            {
                if (_timesheetUserDetail == null)
                    _timesheetUserDetail = new TimesheetUserDetailRepository(_context);
                return _timesheetUserDetail;
            }
        }

        public ITimesheetUserDetailProjectHourRepository TimesheetUserDetailProjectHour
        {
            get
            {
                if (_timesheetUserDetailProjectHour == null)
                    _timesheetUserDetailProjectHour = new TimesheetUserDetailProjectHourRepository(_context);
                return _timesheetUserDetailProjectHour;
            }
        }
        public ITimeSheetModuleRepository TimeSheetModule
        {
            get
            {
                if (_timeSheetModule == null)
                    _timeSheetModule = new TimeSheetModuleRepository(_context);
                return _timeSheetModule;

            }
        }
        public ITimesheetUserSpanRepository TimesheetUserSpan
        {
            get
            {
                if (_timesheetUserSpan == null)
                    _timesheetUserSpan = new TimesheetUserSpanRepository(_context);
                return _timesheetUserSpan;

            }
        }
        #endregion

        #region Performance
        public IPerformanceConfigRepository PerformanceConfig
        {
            get
            {
                if (_performanceConfig == null)
                    _performanceConfig = new PerformanceConfigRepository(_context);

                return _performanceConfig;
            }
        }
        public IPerformanceConfigFeedbackRepository PerformanceConfigFeedback
        {
            get
            {
                if (_performanceConfigFeedback == null)
                    _performanceConfigFeedback = new PerformanceConfigFeedbackRepository(_context);

                return _performanceConfigFeedback;
            }
        }
        public IPerformanceConfigRatingRepository PerformanceConfigRating
        {
            get
            {
                if (_performanceConfigRating == null)
                    _performanceConfigRating = new PerformanceConfigRatingRepository(_context);

                return _performanceConfigRating;
            }
        }
        public IPerformanceEmpDeltasRepository PerformanceEmpDeltas
        {
            get
            {
                if (_performanceEmpDeltas == null)
                    _performanceEmpDeltas = new PerformanceEmpDeltasRepository(_context);

                return _performanceEmpDeltas;
            }
        }
        public IPerformanceEmpPlusesRepository PerformanceEmpPluses
        {
            get
            {
                if (_performanceEmpPluses == null)
                    _performanceEmpPluses = new PerformanceEmpPlusesRepository(_context);

                return _performanceEmpPluses;
            }
        }
        public IPerformanceEmpDevGoalRepository PerformanceEmpDevGoal
        {
            get
            {
                if (_performanceEmpDevGoal == null)
                    _performanceEmpDevGoal = new PerformanceEmpDevGoalRepository(_context);

                return _performanceEmpDevGoal;
            }
        }
        
            public IPerformanceEmpFeedbackRepository PerformanceEmpFeedback
        {
            get
            {
                if (_performanceEmpFeedback == null)
                    _performanceEmpFeedback = new PerformanceEmpFeedbackRepository(_context);

                return _performanceEmpFeedback;
            }
        }
        public IPerformanceEmpFeedbackDetailRepository PerformanceEmpFeedbackDetail
        {
            get
            {
                if (_performanceEmpFeedbackDetail == null)
                    _performanceEmpFeedbackDetail = new PerformanceEmpFeedbackDetailRepository(_context);

                return _performanceEmpFeedbackDetail;
            }
        }
        public IPerformanceEmpGoalNextYearRepository PerformanceEmpGoalNextYear
        {
            get
            {
                if (_performanceEmpGoalNextYear == null)
                    _performanceEmpGoalNextYear = new PerformanceEmpGoalNextYearRepository(_context);

                return _performanceEmpGoalNextYear;
            }
        }
        public IPerformanceEmpGoalPresidentRepository PerformanceEmpGoalPresident
        {
            get
            {
                if (_performanceEmpGoalPresident == null)
                    _performanceEmpGoalPresident = new PerformanceEmpGoalPresidentRepository(_context);

                return _performanceEmpGoalPresident;
            }
        }
        public IPerformanceEmpGoalRepository PerformanceEmpGoal
        {
            get
            {
                if (_performanceEmpGoal == null)
                    _performanceEmpGoal = new PerformanceEmpGoalRepository(_context);

                return _performanceEmpGoal;
            }
        }
        public IPerformanceEmpInitialRatingRepository PerformanceEmpInitialRating
        {
            get
            {
                if (_performanceEmpInitialRating == null)
                    _performanceEmpInitialRating = new PerformanceEmpInitialRatingRepository(_context);

                return _performanceEmpInitialRating;
            }
        }
        public IPerformanceEmpMidYearGoalDetailRepository PerformanceEmpMidYearGoalDetail
        {
            get
            {
                if (_performanceEmpMidYearGoalDetail == null)
                    _performanceEmpMidYearGoalDetail = new PerformanceEmpMidYearGoalDetailRepository(_context);

                return _performanceEmpMidYearGoalDetail;
            }
        }
        public IPerformanceEmpMidYearGoalRepository PerformanceEmpMidYearGoal
        {
            get
            {
                if (_performanceEmpMidYearGoal == null)
                    _performanceEmpMidYearGoal = new PerformanceEmpMidYearGoalRepository(_context);

                return _performanceEmpMidYearGoal;
            }
        }
        
        public IPerformanceEmpTrainingClassesRepository PerformanceEmpTrainingClasses
        {
            get
            {
                if (_performanceEmpTrainingClasses == null)
                    _performanceEmpTrainingClasses = new PerformanceEmpTrainingClassesRepository(_context);

                return _performanceEmpTrainingClasses;
            }
        }
        public IPerformanceEmpYearGoalDetailRepository PerformanceEmpYearGoalDetail
        {
            get
            {
                if (_performanceEmpYearGoalDetail == null)
                    _performanceEmpYearGoalDetail = new PerformanceEmpYearGoalDetailRepository(_context);

                return _performanceEmpYearGoalDetail;
            }
        }
        public IPerformanceEmpYearGoalRepository PerformanceEmpYearGoal
        {
            get
            {
                if (_performanceEmpYearGoal == null)
                    _performanceEmpYearGoal = new PerformanceEmpYearGoalRepository(_context);

                return _performanceEmpYearGoal;
            }
        }
        public IPerformanceGoalMainRepository PerformanceGoalMain
        {
            get
            {
                if (_performanceGoalMain == null)
                    _performanceGoalMain = new PerformanceGoalMainRepository(_context);

                return _performanceGoalMain;
            }
        }
        
            public IPerformanceGoalMeasureRepository PerformanceGoalMeasure
        {
            get
            {
                if (_performanceGoalMeasure == null)
                    _performanceGoalMeasure = new PerformanceGoalMeasureRepository(_context);

                return _performanceGoalMeasure;
            }
        }
        public IPerformanceGoalMeasureIndivRepository PerformanceGoalMeasureIndiv
        {
            get
            {
                if (_performanceGoalMeasureIndiv == null)
                    _performanceGoalMeasureIndiv = new PerformanceGoalMeasureIndivRepository(_context);

                return _performanceGoalMeasureIndiv;
            }
        }
        public IPerformanceGoalRepository PerformanceGoal
        {
            get
            {
                if (_performanceGoal == null)
                    _performanceGoal = new PerformanceGoalRepository(_context);

                return _performanceGoal;
            }
        }
        public IPerformanceInitailRatingRepository PerformanceInitailRating
        {
            get
            {
                if (_performanceInitailRating == null)
                    _performanceInitailRating = new PerformanceInitailRatingRepository(_context);

                return _performanceInitailRating;
            }
        }
        public IPerformancePresidentCouncilRepository PerformancePresidentCouncil
        {
            get
            {
                if (_performancePresidentCouncil == null)
                    _performancePresidentCouncil = new PerformancePresidentCouncilRepository(_context);

                return _performancePresidentCouncil;
            }
        }
        public IPerformanceStatusRepository PerformanceStatus
        {
            get
            {
                if (_performanceStatus == null)
                    _performanceStatus = new PerformanceStatusRepository(_context);

                return _performanceStatus;
            }
        }
        public IPerformanceYearRepository PerformanceYear
        {
            get
            {
                if (_performanceYear == null)
                    _performanceYear = new PerformanceYearRepository(_context);

                return _performanceYear;
            }
        }
        public IPerformanceAppRepository PerformanceApp
        {
            get
            {
                if (_performanceApp == null)
                    _performanceApp = new PerformanceAppRepository(_context);

                return _performanceApp;
            }
        }
        #endregion

        public IProjectRepository Project
        {
            get
            {
                if (_project == null)
                    _project = new ProjectRepository(_context);
                return _project;
            }
        }

        public ITimesheetEmployeeProjectRepository TimesheetEmployeeProject
        {
            get
            {
                if (_timesheetEmployeeProject == null)
                    _timesheetEmployeeProject = new TimesheetEmployeeProjectRepository(_context);
                return _timesheetEmployeeProject;
            }
        }

        public IExpenseBookingRequestRepository ExpenseBookingRequest
        {
            get
            {
                if (_expenseBookingRequest == null)
                    _expenseBookingRequest = new ExpenseBookingRequestRepository(_context);
                return _expenseBookingRequest;
            }
        }
        public IExpenseBookingRequestDetailRepository ExpenseBookingRequestDetail
        {
            get
            {
                if (_expenseBookingRequestDetail == null)
                    _expenseBookingRequestDetail = new ExpenseBookingRequestDetailRepository(_context);
                return _expenseBookingRequestDetail;
            }
        }

        public ISubCategoryItemRepository SubCategoryItem
        {
            get
            {
                if (_subCategoryItem == null)
                    _subCategoryItem = new SubCategoryItemRepository(_context);
                return _subCategoryItem;
            }
        }
        public IExpenseBookingApproverRepository ExpenseBookingApprover
        {
            get
            {
                if (_expenseBookingApprover == null)
                    _expenseBookingApprover = new ExpenseBookingApproverRepository(_context);
                return _expenseBookingApprover;
            }
        }

        public IExpenseBookingTitleAmountRepository ExpenseBookingTitleAmount
        {
            get
            {
                if (_expenseBookingTitleAmountRepository == null)
                    _expenseBookingTitleAmountRepository = new ExpenseBookingTitleAmountRepository(_context);
                return _expenseBookingTitleAmountRepository;
            }
        }
        public ISubCategoryRepository SubCategory
        {
            get
            {
                if (_subCategory == null)
                    _subCategory = new SubCategoryRepository(_context);
                return _subCategory;
            }
        }
        public ICategoryRepository Category
        {
            get
            {
                if (_category == null)
                    _category = new CategoryRepository(_context);
                return _category;
            }
        }

        public IExpenseBookingInviteApproverRepository ExpenseBookingInviteApprover
        {
            get
            {
                if (_expenseBookingInviteApprover == null)
                    _expenseBookingInviteApprover = new ExpenseBookingInviteApproverRepository(_context);
                return _expenseBookingInviteApprover;
            }
        }

        public IExpenseBookingRequestDetailInviteRepository ExpenseBookingRequestDetailInvite
        {
            get
            {
                if (_expenseBookingRequestDetailInvite == null)
                    _expenseBookingRequestDetailInvite = new ExpenseBookingRequestDetailInviteRepository(_context);
                return _expenseBookingRequestDetailInvite;
            }
        }

        public IExpenseDocument ExpenseDocument
        {
            get
            {
                if (_expenseDocument == null)
                    _expenseDocument = new ExpenseDocumentRepository(_context);
                return _expenseDocument;
            }
        }

        #region SalesMarketing

        public ISalesCompanyRepository SalesCompany
        {
            get
            {
                if (_salesCompany == null)
                    _salesCompany = new SalesCompanyRepository(_context);
                return _salesCompany;
            }
        }
        public ISalesCompanyContactRepository SalesCompanyContact
        {
            get
            {
                if (_salesCompanyContact == null)
                    _salesCompanyContact = new SalesCompanyContactRepository(_context);
                return _salesCompanyContact;
            }
        }
        public ISalesStatusRepository SalesStatus
        {
            get
            {
                if (_salesStatus == null)
                    _salesStatus = new SalesStatusRepository(_context);
                return _salesStatus;
            }
        }
        public ISalesDailyCallRepository SalesDailyCall
        {
            get
            {
                if (_salesDailyCall == null)
                    _salesDailyCall = new SalesDailyCallRepository(_context);
                return _salesDailyCall;
            }
        }
        public ISalesScheduleMeetingExternalRepository SalesScheduleMeetingExternal
        {
            get
            {
                if (_salesScheduleMeetingExternal == null)
                    _salesScheduleMeetingExternal = new SalesScheduleMeetingExternalRepository(_context);
                return _salesScheduleMeetingExternal;
            }
        }
        public ISalesScheduleMeetingInternalRepository SalesScheduleMeetingInternal
        {
            get
            {
                if (_salesScheduleMeetingInternal == null)
                    _salesScheduleMeetingInternal = new SalesScheduleMeetingInternalRepository(_context);
                return _salesScheduleMeetingInternal;
            }
        }
        public ISalesMinuteMeetingInternalRepository SalesMinuteMeetingInternal
        {
            get
            {
                if (_salesMinuteMeetingInternal == null)
                    _salesMinuteMeetingInternal = new SalesMinuteMeetingInternalRepository(_context);
                return _salesMinuteMeetingInternal;
            }
        }
        public ISalesMinuteMeetingRepository SalesMinuteMeeting
        {
            get
            {
                if (_salesMinuteMeeting == null)
                    _salesMinuteMeeting = new SalesMinuteMeetingRepository(_context);
                return _salesMinuteMeeting;
            }
        }

        public ISalesScheduleMeetingRepository SalesScheduleMeeting
        {
            get
            {
                if (_salesScheduleMeeting == null)
                    _salesScheduleMeeting = new SalesScheduleMeetingRepository(_context);
                return _salesScheduleMeeting;
            }
        }

        #endregion

        public IBlogRepository Blog
        {
            get
            {
                if (_blog == null)
                    _blog = new BlogRepository(_context);
                return _blog;
            }
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }
    }
}
