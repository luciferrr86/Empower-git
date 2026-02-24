import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { DashboardComponent } from './dashboard.component';
import { SharedModule } from '../../shared/shared.module';
import { FilterPipe } from '../../pipes/fliter.pipe';
import { DashboardService } from '../../services/dashboard/dashboard.service';

export const dashboardRoutes: Routes = [
  {
    path: '',
    component: DashboardComponent,
    children: [
      {
        path: '',
        loadChildren: './dashboard-default/dashboard-default.module#DashboardDefaultModule'
      }
    ]
  }
];

@NgModule({
  imports: [
    CommonModule,
    RouterModule.forChild(dashboardRoutes),
    SharedModule
  ],
  providers: [DashboardService],
  declarations: [DashboardComponent, FilterPipe,]
})
export class DashboardModule { }
