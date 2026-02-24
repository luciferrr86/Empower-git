import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {RouterModule, Routes} from '@angular/router';
import { SqueezeBoxModule } from 'squeezebox';
import { ModuleSettingComponent } from './module-setting/module-setting.component';
import { TagInputModule } from 'ngx-chips';
import { UiSwitchModule } from 'ng2-ui-switch/dist';
import { SharedModule } from '../../shared/shared.module';
import { AdminConfigService } from '../../services/admin/admin-config.service';
import { AdministratorDashboardComponent } from './administrator-dashboard/administrator-dashboard.component';
import { ClientComponent } from './client/client.component';
import { AdministratorChangePasswordComponent } from './administrator-change-password/administrator-change-password.component';
import { AlertService } from '../../services/common/alert.service';

export const AdminRoutes: Routes = [
  { path: "dashboard", component: AdministratorDashboardComponent, data: { title: "Dashboard" }},
  { path: "client", component: ClientComponent, data: { title: "Client" }},
  { path: "module-setting", component: ModuleSettingComponent, data: { title: "Module Settting" }},
  { path: "change-password", component: AdministratorChangePasswordComponent, data: { title: "Change Password" }}
];
@NgModule({
  imports: [
    CommonModule,
    SqueezeBoxModule,
    RouterModule.forChild(AdminRoutes),
    UiSwitchModule,
    TagInputModule,
    CommonModule,
    SharedModule 
  ],
  providers:[AdminConfigService,AlertService],
  declarations: [ModuleSettingComponent, AdministratorDashboardComponent, ClientComponent, AdministratorChangePasswordComponent]
})

export class AdminModule { }
