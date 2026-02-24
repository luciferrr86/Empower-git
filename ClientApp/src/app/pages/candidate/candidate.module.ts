import { NgModule } from '@angular/core';
import { SharedModule } from '../../shared/shared.module';
import { CommonModule } from '@angular/common';
import {  Routes, RouterModule } from '@angular/router';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ToastyModule } from 'ng2-toasty';
import { NotificationsModule } from '../ui-elements/advance/notifications/notifications.module';
import { CandidateService } from '../../services/candidate/candidate.service';
import { AlertService } from '../../services/common/alert.service';
import { CandidateDashboardComponent } from './candidate-dashboard/candidate-dashboard.component';
import { CandidateProfileViewComponent } from './candidate-profile-view/candidate-profile-view.component';
import { CandidatePersonalDetailComponent } from './candidate-personal-detail/candidate-personal-detail.component';
import { CandidateEducationalDetailComponent } from './candidate-educational-detail/candidate-educational-detail.component';
import { CandidateWorkexperienceDetailComponent } from './candidate-workexperience-detail/candidate-workexperience-detail.component';
import { CandidateWorkexperienceDetailEditorComponent } from './candidate-workexperience-detail-editor/candidate-workexperience-detail-editor.component';
import { AccountService } from '../../services/account/account.service';
import { AccountEndpoint } from '../../services/account/account-endpoint.service';
import { CandidateJobsComponent } from './candidate-jobs/candidate-jobs.component';
import { JobApplicationComponent } from './job-application/job-application.component';
import { CandidateJobService } from '../../services/candidate/candidate-job.service';
import { QuestionEditorComponent } from './question-editor/question-editor.component';
import { QuestionControlService } from '../../services/common/question-control.service';
import { ChangePasswordService } from '../../services/profile/change-password.service';
import { CandidateChangePasswordComponent } from './candidate-change-password/candidate-change-password.component';
import { CandidateProfilePictureComponent } from './candidate-profile-picture/candidate-profile-picture.component';
import { CandidateResumeComponent } from './candidate-resume/candidate-resume.component';
import { CandidateVaccancyListComponent } from './candidate-vaccancy-list/candidate-vaccancy-list.component';


export const CandidateRoutes:Routes = [
  {path:"dashboard",component: CandidateDashboardComponent,data:{title:"DashBoard"}},
  {path:"profile",component: CandidateProfileViewComponent,data:{title:"Profile"}},
  {path:"change-password",component: CandidateChangePasswordComponent,data:{title:"Change Password"}},
  {path:"job-list",component: CandidateJobsComponent,data:{title:"Candidate Jobs"}},
  { path: "job-application", component: JobApplicationComponent, data: { title: "Job Application" } },
  { path: "job-vaccancy-list", component: CandidateVaccancyListComponent, data: { title: "Job Vaccancy List" } }
]

@NgModule({
  imports: [
    CommonModule,
    RouterModule.forChild(CandidateRoutes),
    FormsModule,
    ReactiveFormsModule,
    ToastyModule.forRoot(),
    SharedModule,
    NotificationsModule
  ],
  providers :[QuestionControlService,CandidateService,AccountService,AlertService,AccountEndpoint,CandidateJobService,ChangePasswordService],
  declarations: [CandidateDashboardComponent, CandidateProfileViewComponent,    CandidatePersonalDetailComponent,
    CandidateEducationalDetailComponent,
    CandidateWorkexperienceDetailComponent,
    CandidateWorkexperienceDetailEditorComponent, CandidateJobsComponent,
    JobApplicationComponent,
    QuestionEditorComponent,
    CandidateProfilePictureComponent, CandidateVaccancyListComponent,
    CandidateChangePasswordComponent,
    CandidateResumeComponent]
})
export class CandidateModule { }
