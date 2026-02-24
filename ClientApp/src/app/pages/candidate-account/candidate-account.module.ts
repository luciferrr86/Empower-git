import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ForgotComponent } from './forgot/forgot.component';
import { LoginComponent } from './login/login.component';
import { RegistrationComponent } from './registration/registration.component';
import {  Routes, RouterModule } from '@angular/router';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ToastyModule } from 'ng2-toasty';
import { NotificationsModule } from '../ui-elements/advance/notifications/notifications.module';
import { CandidateService } from '../../services/candidate/candidate.service';
import { AlertService } from '../../services/common/alert.service';
import { JobDetailComponent } from './job-detail/job-detail.component';
import { CandidateJobService } from '../../services/candidate/candidate-job.service';
import { ForgotPasswordService } from '../../services/account/forgot-password.service';
import { CandidateResetPasswordComponent } from './candidate-reset-password/candidate-reset-password.component';
import { Vacancy } from '../../models/recruitment/job-vacancy/vacancy-list.model';
import { VacancyService } from '../../services/recruitment/vacancy.service';

export const CandidateAccountRoutes:Routes = [
  {path:"forgot",component: ForgotComponent,data:{title:"Forgot Password"}},
  {path:"login",component: LoginComponent,data:{title:"Login"}},
  {path:"registration",component: RegistrationComponent,data:{title:"Registration"}},
  {path:"job-detail",component: JobDetailComponent,data:{title:"Job Detail"}},
  {path:"reset-password",component: CandidateResetPasswordComponent,data:{title:"Reset Password"}}
]

@NgModule({
  imports: [
    CommonModule,
    RouterModule.forChild(CandidateAccountRoutes),
    FormsModule,
    ReactiveFormsModule,
    ToastyModule.forRoot(),
    NotificationsModule
  ],
  providers: [CandidateService, AlertService, CandidateJobService, ForgotPasswordService],
  declarations: [ForgotComponent, LoginComponent, RegistrationComponent, JobDetailComponent, CandidateResetPasswordComponent]
})
export class CandidateAccountModule { }
