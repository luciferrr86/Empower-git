import { AuthComponent } from './layout/auth/auth.component';
import { Routes } from '@angular/router';
import { AdminComponent } from './layout/admin/admin.component';
import { AuthGuard, CandidateGuard, UserGuard, AdminGuard } from './services/common/auth-guard.service';
import { CandidateComponent } from './layout/candidate/candidate.component';
import { AuthCandidateComponent } from './layout/auth-candidate/auth-candidate.component';
import { AdministratorComponent } from './layout/administrator/administrator.component';
import { ApplyJobComponent } from './pages/apply-job/apply-job.component';
import { PublicVacancyListComponent } from './pages/job-list/public-vacancy-list.component';
export const AppRoutes: Routes = [
  {
    path: '',
    component: AdminComponent, canActivate: [AuthGuard, UserGuard],
    children: [
      {
        path: '',
        redirectTo: 'dashboard',
        pathMatch: 'full'
      },
      {
        path: 'dashboard',
        loadChildren: './pages/dashboard/dashboard.module#DashboardModule'
      }
      ,
      {
        path: 'user',
        loadChildren: './pages/user/user.module#UserModule'
      },
      {
        path: 'maintenance',
        loadChildren: './pages/maintenance/maintenance.module#MaintenanceModule'
      },
      {
        path: 'recruitment',
        loadChildren: './pages/recruitment/recruitment.module#RecruitmentModule'
      },
      {
        path: 'timesheet',
        loadChildren: './pages/timesheet/timesheet.module#TimesheetModule'
      },
      {
        path: 'performance',
        loadChildren: './pages/performance/performance.module#PerformanceModule'
      },
      {
        path: 'configuration',
        loadChildren: './pages/configuration/configuration.module#ConfigurationModule'
      },
      {
        path: 'leave',
        loadChildren: './pages/leave/leave.module#LeaveModule'
      },
      {
        path: 'expense',
        loadChildren: './pages/expense-booking/expense-booking.module#ExpenseBookingModule'
      },
      {
        path: 'sales-tracker',
        loadChildren: './pages/sales-marketing/salesmarketing.module#SalesmarketingModule'
      },
      {
        path: 'blog',
        loadChildren: './pages/blog/blog.module#BlogModule'
      },
    ]
  }, {
    path: '',
    component: AuthComponent,
    children: [
      {
        path: 'account',
        loadChildren: './pages/account/account.module#AccountModule'
      }
    ]
  },
  {
    path: 'candidate',
    component: AuthCandidateComponent,
    loadChildren: './pages/candidate-account/candidate-account.module#CandidateAccountModule'
  },
  {
    path: 'candidate',
    component: CandidateComponent, canActivate: [AuthGuard, CandidateGuard],
    loadChildren: './pages/candidate/candidate.module#CandidateModule'
  },
  {
    path: 'administrator',
    component: AdministratorComponent, canActivate: [AuthGuard, AdminGuard],
    loadChildren: './pages/admin/admin.module#AdminModule'
  },
  { path: "applyjob/:id", component: ApplyJobComponent },
  { path: "vacancylist", component: PublicVacancyListComponent }
];
