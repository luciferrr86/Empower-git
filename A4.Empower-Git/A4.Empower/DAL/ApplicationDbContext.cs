using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using A4.DAL.Entites;
using A4.DAL.EntityMap;
using A4.BAL;
using A4.BAL.ExpenseBooking;
using A4.DAL.Entites.Maintenance;
using A4.DAL.Entites.Leave;
using Microsoft.EntityFrameworkCore.Metadata;
using A4.DAL.Entites.Recruitment;
using A4.DAL.Entites.Blog;
using System.Reflection.Emit;
//using A4.DAL.EntityMap.Blog;

namespace DAL
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {
        #region Admin
        public DbSet<ApplicationModule> ApplicationModule { get; set; }
        public DbSet<ApplicationModuleDetail> ApplicationModuleDetail { get; set; }
        #endregion

        #region Maintenance
        public string CurrentUserId { get; set; }
        public DbSet<Employee> Employee { get; set; }
        public DbSet<EmployeeProxy> EmployeeProxy { get; set; }
        public DbSet<FunctionalDepartment> FunctionalDepartment { get; set; }
        public DbSet<FunctionalDesignation> FunctionalDesignation { get; set; }
        public DbSet<FunctionalGroup> FunctionalGroup { get; set; }
        public DbSet<FunctionalTitle> FunctionalTitle { get; set; }
        public DbSet<Band> Band { get; set; }
        public DbSet<Personal> Personal { get; set; }
        public DbSet<Professional> Professional { get; set; }
        public DbSet<Qualification> Qualification { get; set; }
        public DbSet<ExcelEmployeeData> ExcelEmployeeData { get; set; }

        public DbSet<EmployeeCtc> EmployeeCtc { get; set; }
        public DbSet<EmployeeSalary> EmployeeSalary { get; set; }
        public DbSet<SalaryComponent> SalaryComponent { get; set; }
        public DbSet<SalaryPart> SalaryPart { get; set; }
        public DbSet<CtcOtherComponent> CtcOtherComponent { get; set; }

        #endregion

        public DbSet<TaskListModel> TaskList { get; set; }
        public DbSet<EmployeeByLevel> EmployeeByLevel { get; set; }
        public DbSet<ExpenseApproveByLevel> ExpenseApproveByLevel { get; set; }
        public DbSet<EmpPerformanceGoalMeasure> EmpPerformanceGoalMeasure { get; set; }
        public DbSet<Picture> Picture { get; set; }

        #region SalesMarketing

        public DbSet<SalesCompany> SalesCompany { get; set; }
        public DbSet<SalesCompanyContact> SalesCompanyContact { get; set; }
        public DbSet<SalesDailyCall> SalesDailyCall { get; set; }
        public DbSet<SalesMinuteMeetingInternal> SalesMinuteMeetingInternal { get; set; }
        public DbSet<SalesMinuteMeeting> SalesMinuteMeeting { get; set; }
        public DbSet<SalesScheduleMeetingExternal> SalesScheduleMeetingExternal { get; set; }
        public DbSet<SalesScheduleMeetingInternal> SalesScheduleMeetingInternal { get; set; }
        public DbSet<SalesScheduleMeeting> SalesScheduleMeeting { get; set; }
        public DbSet<SalesStatus> SalesStatus { get; set; }
        #endregion

        #region Recruitment
        public DbSet<JobType> JobType { get; set; }
        public DbSet<JobInterviewType> JobInterviewType { get; set; }
        public DbSet<JobVacancy> JobVacancy { get; set; }
        public DbSet<JobVacancyLevel> JobVacancyLevel { get; set; }
        public DbSet<JobVacancyLevelManager> JobVacancyLevelManager { get; set; }
        public DbSet<JobScreeningQuestion> JobScreeningQuestion { get; set; }
        public DbSet<JobHRQuestion> JobHRQuestion { get; set; }
        public DbSet<JobSkillQuestion> JobSkillQuestion { get; set; }
        public DbSet<JobVacancyLevelSkillQuestion> JobVacancyLevelSkillQuestion { get; set; }
        public DbSet<JobApplication> JobApplication { get; set; }
        public DbSet<JobApplicationScreeningQuestion> JobApplicationScreeningQuestion { get; set; }
        public DbSet<JobApplicationHRQuestions> JobApplicationHRQuestions { get; set; }
        public DbSet<JobCandidateInterview> JobCandidateInterview { get; set; }
        public DbSet<JobApplicationSkillQuestion> JobApplicationSkillQuestion { get; set; }
        public DbSet<JobCandidateProfile> JobCandidateProfile { get; set; }

        public DbSet<JobCandidateQualification> JobCandidateQualification { get; set; }
        public DbSet<JobCandidateWorkExperience> JobCandidateWorkExperience { get; set; }

        public DbSet<JobDocument> JobDocument { get; set; }
        public DbSet<MassInterview> MassInterview { get; set; }
        public DbSet<MassInterviewDetail> MassInterviewDetail { get; set; }
        public DbSet<MassInterviewRoom> MassInterviewRoom { get; set; }
        public DbSet<MassInterviewPanel> MassInterviewPanel { get; set; }
        public DbSet<MassInterviewPanelVacancy> MassInterviewPanelVacany { get; set; }
        public DbSet<MassInterviewScheduleCandidate> MassInterviewScheduleCandidate { get; set; }
        public DbSet<ExcelCandidateData> ExcelCandidateData { get; set; }
        public DbSet<EmailDirectory> EmailDirectory { get; set; }

        #endregion

        #region Performance
        public DbSet<PerformanceConfig> PerformanceConfig { get; set; }
        public DbSet<PerformanceConfigFeedback> PerformanceConfigFeedback { get; set; }
        public DbSet<PerformanceConfigRating> PerformanceConfigRating { get; set; }
        public DbSet<PerformanceStatus> PerformanceStatus { get; set; }
        public DbSet<PerformanceYear> PerformanceYear { get; set; }
        public DbSet<PerformanceGoal> PerformanceGoal { get; set; }
        public DbSet<PerformanceGoalMain> PerformanceGoalMain { get; set; }
        public DbSet<PerformanceGoalMeasure> PerformanceGoalMeasure { get; set; }
        public DbSet<PerformanceGoalMeasureFunc> PerformanceGoalMeasureFunc { get; set; }
        public DbSet<PerformanceGoalMeasureIndiv> PerformanceGoalMeasureIndiv { get; set; }
        public DbSet<PerformanceInitailRating> PerformanceInitailRating { get; set; }
        public DbSet<PerformanceEmpGoal> PerformanceEmpGoal { get; set; }
        public DbSet<PerformanceEmpMidYearGoal> PerformanceEmpMidYearGoal { get; set; }
        public DbSet<PerformanceEmpMidYearGoalDetail> PerformanceEmpMidYearGoalDetail { get; set; }
        public DbSet<PerformanceEmpYearGoal> PerformanceEmpYearGoal { get; set; }
        public DbSet<PerformanceEmpYearGoalDetail> PerformanceEmpYearGoalDetail { get; set; }
        public DbSet<PerformanceEmpDeltas> PerformanceEmpDeltas { get; set; }
        public DbSet<PerformanceEmpDevGoal> PerformanceEmpDevGoal { get; set; }
        public DbSet<PerformanceEmpDevGoalDoc> PerformanceEmpDevGoalDoc { get; set; }
        public DbSet<PerformanceEmpFeedback> PerformanceEmpFeedback { get; set; }
        public DbSet<PerformanceEmpFeedbackDetail> PerformanceEmpFeedbackDetail { get; set; }
        public DbSet<PerformanceEmpPluses> PerformanceEmpPluses { get; set; }
        public DbSet<PerformanceEmpTrainingClasses> PerformanceEmpTrainingClasses { get; set; }
        public DbSet<PerformanceEmpGoalPresident> PerformanceEmpGoalPresident { get; set; }
        public DbSet<PerformancePresidentCouncil> PerformancePresidentCouncil { get; set; }
        public DbSet<PerformanceEmpGoalNextYear> PerformanceEmpGoalNextYear { get; set; }
        public DbSet<PerformanceEmpInitialRating> PerformanceEmpInitialRating { get; set; }
        #endregion

        #region Leave
        public DbSet<LeavePeriod> LeavePeriod { get; set; }
        public DbSet<LeaveHolidayList> LeaveHolidayList { get; set; }
        public DbSet<LeaveWorkingDay> LeaveWorkingDay { get; set; }
        public DbSet<LeaveStatus> LeaveStatus { get; set; }
        public DbSet<LeaveType> LeaveType { get; set; }
        public DbSet<LeaveRules> LeaveRules { get; set; }
        public DbSet<EmployeeLeaves> EmployeeLeaves { get; set; }
        public DbSet<EmployeeLeavesEntitlement> EmployeeLeavesEntitlement { get; set; }
        public DbSet<EmployeeLeaveDetail> EmployeeLeaveDetail { get; set; }

        public DbSet<EmployeeAttendence> EmployeeAttendence { get; set; }
        public DbSet<AttendenceSummary> AttendenceSummary { get; set; }
        #endregion

        #region TimeSheet

        public DbSet<TimesheetConfiguration> TimesheetConfiguration { get; set; }
        public DbSet<TimesheetTemplate> TimesheetTemplate { get; set; }
        public DbSet<TimesheetUserSchedule> TimesheetUserSchedule { get; set; }
        public DbSet<TimesheetClient> TimesheetClient { get; set; }
        public DbSet<TimesheetProject> TimesheetProject { get; set; }
        public DbSet<TimesheetEmployeeProject> TimesheetEmployeeProject { get; set; }
        public DbSet<TimesheetWorkingDays> TimesheetWorkingDays { get; set; }
        public DbSet<TimesheetUserSpan> TimesheetUserSpan { get; set; }
        public DbSet<TimesheetUserDetail> TimesheetUserDetail { get; set; }
        public DbSet<TimesheetUserDetailProjectHour> TimesheetUserDetailProjectHour { get; set; }

        #endregion

        #region ExpenceBooking

        public DbSet<ExpenseBookingCategory> ExpenseBookingCategory { get; set; }
        public DbSet<ExpenseBookingSubCategory> ExpenseBookingSubCategory { get; set; }
        public DbSet<ExpenseBookingTitleAmount> ExpenseBookingTitleAmount { get; set; }
        public DbSet<ExpenseBookingApprover> ExpenseBookingApprover { get; set; }
        public DbSet<ExpenseBookingSubCategoryItem> ExpenseBookingSubCategoryItem { get; set; }
        public DbSet<ExpenseBookingRequest> ExpenseBookingRequest { get; set; }
        public DbSet<ExpenseBookingInviteApprover> ExpenseBookingInviteApprover { get; set; }
        public DbSet<ExpenseBookingRequestDetailInvite> ExpenseBookingRequestDetailInvite { get; set; }
        public DbSet<ExpenseDocument> ExpenseDocument { get; set; }

        #endregion

        #region Blog
        public DbSet<Blog> Blog { get; set; }
        //public DbSet<BlogCategory> BlogCategory { get; set; }
        //public DbSet<BlogTag> BlogTag { get; set; }
        //public DbSet<Blog_BlogTag> Blog_BlogTag { get; set; }
        #endregion

        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.HasDefaultSchema("dbo");
            base.OnModelCreating(builder);

            //Ignore the second autogenerated property from updation as record get updated 
            builder.Entity<Employee>(builder =>
            {
                builder.Property(e => e.EmployeeCode).ValueGeneratedOnAdd()
                    .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);
            });



            builder.Entity<EmpPerformanceGoalMeasure>(builder =>
             {
                 builder.HasNoKey();
                 builder.ToTable("EmpPerformanceGoalMeasure");
             });
            builder.Entity<EmployeeByLevel>(builder =>
            {
                builder.HasNoKey();
                builder.ToTable("EmployeeByLevel");
            });
            builder.Entity<TaskListModel>(builder =>
            {
                builder.HasNoKey();
                builder.ToTable("TaskListModel");
            });
            builder.Entity<ExpenseApproveByLevel>(builder =>
            {
                builder.HasNoKey();
                builder.ToTable("ExpenseApproveByLevel");
            });

            #region Admin
            builder.ApplyConfiguration(new ApplicationModuleMap());
            builder.ApplyConfiguration(new ApplicationModuleDetailMap());
            #endregion

            #region Maintenance

            builder.Entity<ApplicationUser>().HasMany(u => u.Claims).WithOne().HasForeignKey(c => c.UserId).IsRequired().OnDelete(DeleteBehavior.Cascade);
            builder.Entity<ApplicationUser>().HasMany(u => u.Roles).WithOne().HasForeignKey(r => r.UserId).IsRequired().OnDelete(DeleteBehavior.Cascade);

            builder.Entity<ApplicationRole>().HasMany(r => r.Claims).WithOne().HasForeignKey(c => c.RoleId).IsRequired().OnDelete(DeleteBehavior.Cascade);
            builder.Entity<ApplicationRole>().HasMany(r => r.Users).WithOne().HasForeignKey(r => r.RoleId).IsRequired().OnDelete(DeleteBehavior.Cascade);


            builder.ApplyConfiguration(new EmployeeMap());
            builder.ApplyConfiguration(new EmployeeProxyMap());
            builder.ApplyConfiguration(new PersonalMap());
            builder.ApplyConfiguration(new ProfessionalMap());
            builder.ApplyConfiguration(new QualificationMap());
            builder.ApplyConfiguration(new FunctionalDepartmentMap());
            builder.ApplyConfiguration(new FunctionalDesignationMap());
            builder.ApplyConfiguration(new FunctionalGroupMap());
            builder.ApplyConfiguration(new FunctionalTitleMap());
            builder.ApplyConfiguration(new BandMap());
            builder.ApplyConfiguration(new ExcelEmployeeDataMap());
            #endregion

            #region Recruitment
            builder.ApplyConfiguration(new JobApplicationMap());
            builder.ApplyConfiguration(new JobDocumentMap());
            builder.ApplyConfiguration(new JobTypeMap());
            builder.ApplyConfiguration(new JobInterviewTypeMap());
            builder.ApplyConfiguration(new JobCandidateProfileMap());
            builder.ApplyConfiguration(new JobCandidateQualificationMap());
            builder.ApplyConfiguration(new JobApplicationHRQuestionsMap());
            builder.ApplyConfiguration(new JobApplicationScreeningQuestionMap());
            builder.ApplyConfiguration(new JobCandidateInterviewMap());
            builder.ApplyConfiguration(new JobVacancyLevelSkillQuestionMap());
            builder.ApplyConfiguration(new JobApplicationSkillQuestionMap());
            builder.ApplyConfiguration(new MassInterviewRoomMap());
            builder.ApplyConfiguration(new JobVacancyMap());
            builder.ApplyConfiguration(new JobSkillQuestionMap());
            builder.ApplyConfiguration(new MassInterviewMap());
            builder.ApplyConfiguration(new MassInterviewDetailMap());
            builder.ApplyConfiguration(new MassInterviewRoomMap());
            builder.ApplyConfiguration(new MassInterviewPanelMap());
            builder.ApplyConfiguration(new MassInterviewPanelVacancyMap());
            builder.ApplyConfiguration(new MassInterviewScheduleCandidateMap());
            #endregion

            #region Performance
            builder.ApplyConfiguration(new PerformanceConfigMap());
            builder.ApplyConfiguration(new PerformanceConfigFeedbackMap());
            builder.ApplyConfiguration(new PerformanceConfigRatingMap());
            builder.ApplyConfiguration(new PerformanceStatusMap());
            builder.ApplyConfiguration(new PerformanceYearMap());
            builder.ApplyConfiguration(new PerformanceGoalMap());
            builder.ApplyConfiguration(new PerformanceGoalMainMap());
            builder.ApplyConfiguration(new PerformanceGoalMeasureMap());
            builder.ApplyConfiguration(new PerformanceGoalMeasureFuncMap());
            builder.ApplyConfiguration(new PerformanceGoalMeasureIndivMap());
            builder.ApplyConfiguration(new PerformanceInitailRatingMap());
            builder.ApplyConfiguration(new PerformanceEmpGoalMap());
            builder.ApplyConfiguration(new PerformanceEmpMidYearGoalMap());
            builder.ApplyConfiguration(new PerformanceEmpMidYearGoalDetailMap());
            builder.ApplyConfiguration(new PerformanceEmpYearGoalMap());
            builder.ApplyConfiguration(new PerformanceEmpYearGoalDetailMap());
            builder.ApplyConfiguration(new PerformanceEmpDeltasMap());
            builder.ApplyConfiguration(new PerformanceEmpDevGoalMap());
            builder.ApplyConfiguration(new PerformanceEmpDevGoalDocMap());
            builder.ApplyConfiguration(new PerformanceEmpFeedbackMap());
            builder.ApplyConfiguration(new PerformanceEmpFeedbackDetailMap());
            builder.ApplyConfiguration(new PerformanceEmpPlusesMap());
            builder.ApplyConfiguration(new PerformanceEmpTrainingClassesMap());
            builder.ApplyConfiguration(new PerformanceEmpGoalPresidentMap());
            builder.ApplyConfiguration(new PerformancePresidentCouncilMap());
            builder.ApplyConfiguration(new PerformanceEmpGoalNextYearMap());
            builder.ApplyConfiguration(new PerformanceEmpInitialRatingMap());
            #endregion

            #region Leave
            builder.ApplyConfiguration(new LeavePeriodMap());
            builder.ApplyConfiguration(new LeaveHolidayListMap());
            builder.ApplyConfiguration(new LeaveWorkingDayMap());
            builder.ApplyConfiguration(new LeaveStatusMap());
            builder.ApplyConfiguration(new LeaveTypeMap());
            builder.ApplyConfiguration(new LeaveRulesMap());
            builder.ApplyConfiguration(new EmployeeLeavesMap());
            builder.ApplyConfiguration(new EmployeeLeavesEntitlementMap());
            builder.ApplyConfiguration(new EmployeeLeaveDetailMap());
            #endregion

            #region TimeSheet

            builder.ApplyConfiguration(new TimesheetConfigurationMap());
            builder.ApplyConfiguration(new TimesheetTemplateMap());
            builder.ApplyConfiguration(new TimesheetUserScheduleMap());
            builder.ApplyConfiguration(new TimesheetClientMap());
            builder.ApplyConfiguration(new TimesheetProjectMap());
            builder.ApplyConfiguration(new TimesheetEmployeeProjectMap());
            builder.ApplyConfiguration(new TimesheetWorkingDaysMap());
            builder.ApplyConfiguration(new TimesheetUserSpanMap());
            builder.ApplyConfiguration(new TimesheetUserDetailMap());
            builder.ApplyConfiguration(new TimesheetUserDetailProjectHourMap());

            #endregion

            #region ExpenceBooking

            builder.ApplyConfiguration(new ExpenseBookingCategoryMap());
            builder.ApplyConfiguration(new ExpenseBookingSubCategoryMap());
            builder.ApplyConfiguration(new ExpenseBookingSubCategoryItemMap());
            builder.ApplyConfiguration(new ExpenseBookingRequestMap());
            builder.ApplyConfiguration(new ExpenseBookingApproverMap());
            builder.ApplyConfiguration(new ExpenseBookingInviteApproverMap());
            builder.ApplyConfiguration(new ExpenseBookingTitleAmountMap());
            builder.ApplyConfiguration(new ExpenseBookingRequestDetailMap());
            builder.ApplyConfiguration(new ExpenseBookingRequestDetailInviteMap());
            builder.ApplyConfiguration(new ExpenseDocumentMap());
            #endregion

            #region Blog
            builder.ApplyConfiguration(new BlogMap());
            //builder.ApplyConfiguration(new BlogCategoryMap());
            //builder.ApplyConfiguration(new BlogTagMap());
            //builder.ApplyConfiguration(new Blog_BlogTagMap());
            #endregion

        }

        public override int SaveChanges()
        {
            UpdateAuditEntities();
            return base.SaveChanges();
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            UpdateAuditEntities();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            UpdateAuditEntities();
            return base.SaveChangesAsync(cancellationToken);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
        {
            UpdateAuditEntities();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        private void UpdateAuditEntities()
        {
            var modifiedEntries = ChangeTracker.Entries()
                .Where(x => x.Entity is IAuditableEntity && (x.State == EntityState.Added || x.State == EntityState.Modified));


            foreach (var entry in modifiedEntries)
            {
                var entity = (IAuditableEntity)entry.Entity;
                DateTime now = DateTime.UtcNow;

                if (entry.State == EntityState.Added)
                {
                    entity.CreatedDate = now;
                    entity.IsActive = true;
                    entity.CreatedBy = CurrentUserId;
                }
                else
                {
                    base.Entry(entity).Property(x => x.CreatedBy).IsModified = false;
                    base.Entry(entity).Property(x => x.CreatedDate).IsModified = false;
                }

                entity.UpdatedDate = now;
                entity.UpdatedBy = CurrentUserId;
            }
        }
    }
}
