import { CustomMaxDirective } from './../../pipes/custom-max-validator.directive';
import { WizardStepComponent } from './wizard-step/wizard-step.component';
import { WizardComponent } from './wizard/wizard.component';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { SelectModule } from '../../../../node_modules/ng-select';
import { RouterModule, Routes } from '@angular/router';
import { SharedModule } from '../../shared/shared.module';
import { JobCreationComponent } from './job-vacancy/job-creation/job-creation.component';
import { VacancyListComponent } from './job-vacancy/vacancy-list/vacancy-list.component';
import { VacancyService } from '../../services/recruitment/vacancy.service';
import { ScreeningQuestionComponent } from './job-vacancy/screening-question/screening-question.component';
import { HrKpiComponent } from './job-vacancy/hr-kpi/hr-kpi.component';
import { SkillInterviewComponent } from './job-vacancy/skill-interview/skill-interview.component';
import { CandidateListComponent } from './candidate-view/candidate-list/candidate-list.component';
import { JobAppliedListComponent } from './candidate-view/job-applied-list/job-applied-list.component';
import { JobAppliedDetailComponent } from './candidate-view/job-applied-detail/job-applied-detail.component';
import { InterviewScheduleListComponent } from './candidate-view/interview-schedule-list/interview-schedule-list.component';
import { HrAssessmentComponent } from './candidate-view/hr-assessment/hr-assessment.component';
import { JobInterviewPaletteComponent } from './candidate-view/job-interview-palette/job-interview-palette.component';
import { ShortlistedCandidateListComponent } from './manage-interview/shortlisted-candidate-list/shortlisted-candidate-list.component';
import { ManagerAssesmentComponent } from './manage-interview/manager-assesment/manager-assesment.component';
import { ShortlistedCandidateDetailComponent } from './manage-interview/shortlisted-candidate-detail/shortlisted-candidate-detail.component';
import { BulkSchedulingPalleteComponent } from './bulk-scheduling/bulk-scheduling-pallete/bulk-scheduling-pallete.component';
import { InterviewDateTimeComponent } from './bulk-scheduling/interview-date-time/interview-date-time.component';
import { InterviewRoomsComponent } from './bulk-scheduling/interview-rooms/interview-rooms.component';
import { InterviewPanelComponent } from './bulk-scheduling/interview-panel/interview-panel.component';
import { ManualScheduleComponent } from './bulk-scheduling/manual-schedule/manual-schedule.component';
import { AutomaticScheduleComponent } from './bulk-scheduling/automatic-schedule/automatic-schedule.component';
import { ScreeningEditorComponent } from './job-vacancy/screening-editor/screening-editor.component';
import { HrKpiEditorComponent } from './job-vacancy/hr-kpi-editor/hr-kpi-editor.component';
import { SkillInterviewEditorComponent } from './job-vacancy/skill-interview-editor/skill-interview-editor.component';

import { JobVacancyFormDataService } from '../../services/recruitment/job-vacancy-form-data.service';

import { AccountService } from '../../services/account/account.service';
import { AlertService } from '../../services/common/alert.service';

import { AccountEndpoint } from '../../services/account/account-endpoint.service';
import { MassInterviewScheduleComponent } from './bulk-scheduling/mass-interview-schedule/mass-interview-schedule.component';
import { InterviewRoomsEditorComponent } from './bulk-scheduling/interview-rooms-editor/interview-rooms-editor.component';
import { InterviewPanelEditorComponent } from './bulk-scheduling/interview-panel-editor/interview-panel-editor.component';
import { JobInterviewService } from '../../services/recruitment/job-interview.service';
import { CandidateService } from '../../services/candidate/candidate.service';
import { CandidateProfileComponent } from './candidate-view/candidate-profile/candidate-profile.component';
import { BulkInterviewScheduleService } from '../../services/recruitment/bulk-interview-schedule.service';
import { ManageInterviewService } from '../../services/recruitment/manage-interview.service';
import { BulkScheduleingListComponent } from './bulk-scheduling/bulk-scheduleing-list/bulk-scheduleing-list.component';
import { UiSwitchModule } from 'ng2-ui-switch';
import { TagInputModule } from 'ngx-chips';
import { InterviewLevelComponent } from './job-vacancy/interview-level/interview-level.component';
import { InterviewLevelEditorComponent } from './job-vacancy/interview-level-editor/interview-level-editor.component';
import { JobVacancyComponent } from './job-vacancy/job-vacancy/job-vacancy.component';

import { DashboardRecruitmentComponent } from './dashboard-recruitment/dashboard-recruitment.component';
import { DashboardService } from '../../services/dashboard/dashboard.service';
import { DashboardRecruitmentService } from '../../services/recruitment/dashboard-recruitment.service';
import { CustomMinDirective } from '../../pipes/custom-min-validator.directive';
import { CandidateBulkUploadComponent } from './candidate-bulk-upload/candidate-bulk-upload.component';
import { BulkUploadService } from '../../services/maintenance/bulk-upload.service';
import { JobReasonComponent } from './job-vacancy/job-reason/job-reason.component';
import { ManageDirectoryComponent } from './job-vacancy/directory/directory.component';
import { DirectoryListComponent } from './job-vacancy/directory-list/directory-list.component';



export const RecruitmentRoutes: Routes = [
  { path: "job-vaccancy", component: VacancyListComponent, data: { title: "Vaccany List" } },
  { path: 'job-create', component: JobVacancyComponent },
  { path: 'recruitment-dashboard', component: DashboardRecruitmentComponent },
  { path: "candidate-list", component: CandidateListComponent, data: { title: "Vaccany List" } },
  { path: "candidate-bulkupload", component: CandidateBulkUploadComponent, data: { title: "Candidate Bulk Upload" } },

  //CandidateBulkUploadComponent{ path: "candidate-list/:id", component: CandidateListComponent, data: { title: "Vaccany List" } },
  { path: "candidate-profile", component: CandidateProfileComponent, data: { title: "Candidate Profile" } },
  { path: "applied-job-detail", component: JobAppliedDetailComponent, data: { title: "Job Applied Detail" } },

  { path: "manage-interview", component: ShortlistedCandidateListComponent, data: { title: "Manage Interview" } },
  { path: "job-reason/:id", component: JobReasonComponent, data: { title: "Reason" } },
  { path: "directory-list", component: DirectoryListComponent, data: { title: "Directory List" } },
  { path: "shortlisted-candidate-detail", component: ShortlistedCandidateDetailComponent, data: { title: "Shortlisted Candidate" } },
  { path: "bulk-scheduling", component: BulkSchedulingPalleteComponent, data: { title: "Bulk Scheduling" } },
  { path: "bulk-scheduleing-list", component: BulkScheduleingListComponent, data: { title: "Bulk Schedule List" } }


];

@NgModule({
  imports: [
    CommonModule,
    RouterModule.forChild(RecruitmentRoutes),
    SharedModule,
    SelectModule,
    ReactiveFormsModule,
    UiSwitchModule,
    TagInputModule
  ],
  providers: [ManageInterviewService, CandidateService, BulkUploadService, VacancyService, JobVacancyFormDataService, AccountService, AlertService, AccountEndpoint, JobInterviewService, BulkInterviewScheduleService, DashboardRecruitmentService],
  declarations: [
    JobCreationComponent,
    VacancyListComponent,
    ScreeningQuestionComponent,
    HrKpiComponent,
    WizardStepComponent,
    WizardComponent,
    SkillInterviewComponent,
    CustomMinDirective,
    CustomMaxDirective,
    CandidateListComponent,
    JobAppliedListComponent,
    JobAppliedDetailComponent,
    InterviewScheduleListComponent,
    HrAssessmentComponent,
    JobInterviewPaletteComponent,
    ShortlistedCandidateListComponent,
    ManagerAssesmentComponent,
    ShortlistedCandidateDetailComponent,
    BulkSchedulingPalleteComponent,
    InterviewDateTimeComponent,
    InterviewRoomsComponent,
    InterviewPanelComponent,
    ManualScheduleComponent,
    AutomaticScheduleComponent,
    ScreeningEditorComponent,
    HrKpiEditorComponent,
    SkillInterviewEditorComponent,
    MassInterviewScheduleComponent,
    InterviewRoomsEditorComponent,
    InterviewPanelEditorComponent,
    CandidateProfileComponent,
    BulkScheduleingListComponent,
    InterviewLevelComponent,
    InterviewLevelEditorComponent,
    JobVacancyComponent,
    DashboardRecruitmentComponent,
    CandidateBulkUploadComponent,
    JobReasonComponent,
    ManageDirectoryComponent,
    DirectoryListComponent
  ]
})
export class RecruitmentModule { }
