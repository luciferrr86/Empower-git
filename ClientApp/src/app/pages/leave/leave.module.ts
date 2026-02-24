import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MyLeaveComponent } from './my-leaves/my-leave/my-leave.component';
import { LeaveCalendarComponent } from './my-leaves/leave-calendar/leave-calendar.component';
import { LeaveInfoComponent } from './my-leaves/leave-info/leave-info.component';
import { LeaveApplyComponent } from './my-leaves/leave-apply/leave-apply.component';
import { LeaveApplliedListComponent } from './my-leaves/leave-appllied-list/leave-appllied-list.component';
import { LeaveApplliedDetailComponent } from './my-leaves/leave-appllied-detail/leave-appllied-detail.component';
import { EmployeeLeaveListComponent } from './manage-leave/employee-leave-list/employee-leave-list.component';
import { EmployeeLeaveCalendarComponent } from './manage-leave/employee-leave-calendar/employee-leave-calendar.component';
import { EmployeeLeaveInfoComponent } from './manage-leave/employee-leave-info/employee-leave-info.component';
import { EmployeeLeaveDetailComponent } from './manage-leave/employee-leave-detail/employee-leave-detail.component';
import { HrEmployeeListComponent } from './hr-view/hr-employee-list/hr-employee-list.component';
import { HrEmployeeLeaveInfoComponent } from './hr-view/hr-employee-leave-info/hr-employee-leave-info.component';
import { Routes, RouterModule } from '../../../../node_modules/@angular/router';
import { MyLeaveService } from '../../services/leave/my-leave.service';
import { SharedModule } from '../../shared/shared.module';
import { DataTableModule } from 'angular2-datatable';
import { SelectModule } from 'ng-select';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgxDatatableModule } from '@swimlane/ngx-datatable';
import { ToastyModule } from 'ng2-toasty';
import { SqueezeBoxModule } from 'squeezebox';
import { ManageLeavesComponent } from './manage-leave/manage-leaves/manage-leaves.component';
import { EmployeeLeaveInfoListComponent } from './manage-leave/employee-leave-info-list/employee-leave-info-list.component';
import { AccountEndpoint } from '../../services/account/account-endpoint.service';
import { AccountService } from '../../services/account/account.service';
import { HrLeaveService } from '../../services/leave/hr-leave.service';
import { ManageLeaveService } from '../../services/leave/manage-leave.service';
import { FullCalendarModule } from 'ng-fullcalendar';
import { CheckConfigComponent } from './check-config/check-config.component';
import { MyLeaveResolver } from '../../services/leave/my-leave-resolve';
import { MyAttendanceComponent } from './my-attendance/my-attendance.component';
import { EmployeeSalaryService } from '../../services/maintenance/employee-salary.service';
import { ManageAttendanceComponent } from './manage-attendance/manage-attendance.component';
import { UploadAttSummaryComponent } from './upload-att-summary/upload-att-summary.component';
import { ExcelUploadComponent } from './upload-att-detail/excel-upload.component';
import { ViewAttSummaryComponent } from './view-att-summary/view-att-summary.component';
import { LeaveEntryComponent } from './leave-entry/leave-entry.component';
import { EmployeeAttendenceComponent } from './employee-attendence/employee-attendence.component';

export const LeaveRoutes: Routes = [
  { path: "my-leave", component: MyLeaveComponent, data: { title: "Vaccany List" }, resolve: { myLeaveData: MyLeaveResolver } },
  { path: 'manage-leaves', component: ManageLeavesComponent },
  { path: 'check-config', component: CheckConfigComponent },
  { path: "hr-viewlist", component: HrEmployeeListComponent, data: { title: "Vaccany List" } },
   
  {
    path: 'attendance',
    data: {
      breadcrumb: 'Attendance',
      status: false
    },
    children: [
      { path: "upload-attendance-summary", component: UploadAttSummaryComponent, data: { title: "Upload Attendance Summary" } },
      { path: "leave-entry", component: LeaveEntryComponent, data: { title: "Attendance Detail'" } },
      { path: "excel-upload", component: ExcelUploadComponent, data: { title: "Upload Attendance Detail" } },
      { path: "view-summary", component: ViewAttSummaryComponent, data: { title: "View Attendence Summary" } },
      { path: "employee-attendence", component: EmployeeAttendenceComponent, data: { title: "Employee Attendence" } },
      { path: "employee-attendence/:id", component: EmployeeAttendenceComponent, data: { title: "Employee Attendence" } },
      { path: "my-attendance", component: MyAttendanceComponent, data: { title: "My Attendance" } },
      { path: "my-attendance/:id", component: MyAttendanceComponent, data: { title: "My Attendance" } },
      { path: "manage-attendance", component: ManageAttendanceComponent, data: { title: "Manage Attendance" } },
    ]
  },
];

@NgModule({
  imports: [
    CommonModule,
    RouterModule.forChild(LeaveRoutes),
    SharedModule,
    DataTableModule,
    SelectModule,
    FormsModule,
    NgxDatatableModule,
    ReactiveFormsModule,
    ToastyModule.forRoot(),
    SqueezeBoxModule,
    FullCalendarModule
  ],
    providers: [MyLeaveService, AccountService, AccountEndpoint, HrLeaveService, ManageLeaveService, MyLeaveResolver, EmployeeSalaryService],
  declarations: [MyLeaveComponent, LeaveCalendarComponent, LeaveInfoComponent, EmployeeLeaveInfoListComponent, LeaveApplyComponent, LeaveApplliedListComponent,
    LeaveApplliedDetailComponent, EmployeeLeaveListComponent, EmployeeLeaveCalendarComponent,
    EmployeeLeaveInfoComponent, EmployeeLeaveDetailComponent, HrEmployeeListComponent,
    HrEmployeeLeaveInfoComponent, ManageLeavesComponent, CheckConfigComponent,
    MyAttendanceComponent, ManageAttendanceComponent, UploadAttSummaryComponent, LeaveEntryComponent, ExcelUploadComponent,
    ViewAttSummaryComponent, EmployeeAttendenceComponent]
})
export class LeaveModule { }
