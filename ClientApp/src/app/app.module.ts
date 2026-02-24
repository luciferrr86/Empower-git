import { AuthComponent } from './layout/auth/auth.component';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { ToastyModule, ToastyService, ToastyConfig } from 'ng2-toasty';
import { AppRoutes } from './app.routing';
import { AppComponent } from './app.component';
import { AdminComponent } from './layout/admin/admin.component';
import { ClickOutsideModule } from 'ng-click-outside';
import { SharedModule } from './shared/shared.module';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { BreadcrumbsComponent } from './layout/admin/breadcrumbs/breadcrumbs.component';
import { TitleComponent } from './layout/admin/title/title.component';
import { AuthGuard, AdminGuard, CandidateGuard, UserGuard } from './services/common/auth-guard.service';
import { NotificationsModule } from './pages/ui-elements/advance/notifications/notifications.module';
import { SweetAlertService } from 'ngx-sweetalert2';
import { CandidateComponent } from './layout/candidate/candidate.component';
import { HttpModule } from '@angular/http';
import { AdministratorComponent } from './layout/administrator/administrator.component';
import { AdministatorMenuItems } from './shared/menu-items/menu-items';
import { AccountService } from './services/account/account.service';
import { AccountEndpoint } from './services/account/account-endpoint.service';
import { ProfileService } from './services/maintenance/profile.service';
import { FileUploadService } from './services/file-upload/file-upload.service';
import { ProfileModule } from './pages/user/profile/profile.module';
import { AuthCandidateComponent } from './layout/auth-candidate/auth-candidate.component';
import { ApplyJobComponent } from './pages/apply-job/apply-job.component';
import { VacancyService } from './services/recruitment/vacancy.service';
import { PublicVacancyListComponent } from './pages/job-list/public-vacancy-list.component';
import { QuillModule } from 'ngx-quill';

@NgModule({
  declarations: [
    AppComponent,
    AdminComponent,
    BreadcrumbsComponent,
    TitleComponent,
    AuthComponent,
    CandidateComponent,
    AdministratorComponent,
    AuthCandidateComponent,
    ApplyJobComponent,
    PublicVacancyListComponent
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    RouterModule.forRoot(AppRoutes),
    QuillModule,
    ClickOutsideModule,
    SharedModule,
    NotificationsModule,
    HttpModule,
    HttpModule

  ],

  providers: [
    AdminGuard, AuthGuard, ToastyService, AccountService, ProfileService, FileUploadService, AccountEndpoint, ToastyConfig, SweetAlertService, CandidateGuard, UserGuard, AdministatorMenuItems, VacancyService, RouterModule
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
