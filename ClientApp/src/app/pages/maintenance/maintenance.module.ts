import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ModalModule} from 'ngx-bootstrap/modal';
import { MaintenanceComponent } from './maintenance.component';
import { RouterModule, Routes } from '@angular/router';
import { SharedModule } from '../../shared/shared.module';
import { EmployeeComponent } from './employee/employee.component';
import { AccountService } from '../../services/account/account.service';
import { AccountEndpoint } from '../../services/account/account-endpoint.service';
import { UserInfoComponent } from './user-info/user-info.component';
import { SelectModule, IOption } from 'ng-select';
import { FunctionalDepartmentComponent } from './functional-department/functional-department.component';
import { FunctionalDepartmentInfoComponent } from './functional-department-info/functional-department-info.component';
import { FunctionalDesignationComponent } from './functional-designation/functional-designation.component';
import { FunctionalDesignationInfoComponent } from './functional-designation-info/functional-designation-info.component';
import { FunctionalTitleComponent } from './functional-title/functional-title.component';
import { FunctionalTitleInfoComponent } from './functional-title-info/functional-title-info.component';
import { FunctionalGroupComponent } from './functional-group/functional-group.component';
import { FunctionalGroupInfoComponent } from './functional-group-info/functional-group-info.component';
import { BandComponent } from './band/band.component';
import { BandInfoComponent } from './band-info/band-info.component';
import { DataTableModule } from 'angular2-datatable';
import { FormsModule } from '@angular/forms';
import { DatepickerModule } from 'angular2-material-datepicker';
import { NgxDatatableModule } from '@swimlane/ngx-datatable';
import { ReactiveFormsModule } from '@angular/forms';
import { ToastyModule, ToastyService } from 'ng2-toasty';
import { DepartmentService } from '../../services/maintenance/department.service';
import { TitleService } from '../../services/maintenance/title.service';
import { GroupByPipe } from '../../pipes/group-by.pipe';
import { GroupService } from '../../services/maintenance/group.service';
import { BulkUploadService } from '../../services/maintenance/bulk-upload.service';
import { BandService } from '../../services/maintenance/band.service';
import { FunctionalDesignationService } from '../../services/maintenance/functional-designation.service';
import { RoleComponent } from './role/role.component';
import { RoleInfoComponent } from './role-info/role-info.component';
import { BulkUploadComponent } from './bulk-upload/bulk-upload.component';
import { NotificationsModule } from '../ui-elements/advance/notifications/notifications.module';
import { CreateExpenseItemService } from '../../services/maintenance/create-expense-item.service';
import { EmployeeDetailsComponent } from './employee-details/employee-details.component';
import { EmployeeSalaryComponent } from './employee-salary/employee-salary.component';
import { EmployeeSalaryService } from '../../services/maintenance/employee-salary.service';
import { EmployeeSalaryDetailComponent } from './employee-salary-detail/employee-salary-detail.component';
import { AddEmpCtcComponent } from './add-emp-ctc/add-emp-ctc.component';
import { ProcessSalaryComponent } from './process-salary/process-salary.component';
import { CheckSalaryComponent } from './check-salary/check-salary.component';
import { ViewSalaryComponent } from './view-salary/view-salary.component';
import { SalaryComponentComponent } from './salary-component/salary-component.component';
//import { LeaveEntryComponent } from './leave-entry/leave-entry.component';
//import { EmployeeAttendenceComponent } from './employee-attendence/employee-attendence.component';
//import { ExcelUploadComponent } from './excel-upload/excel-upload.component';
//import { UploadAttSummaryComponent } from './upload-att-summary/upload-att-summary.component';
import { AllEmpSalComponent } from './all-emp-sal/all-emp-sal.component';
/*import { ViewAttSummaryComponent } from './view-att-summary/view-att-summary.component';*/
import { SalComponentListComponent } from './sal-component-list/sal-component-list.component';

export const MaintenanceRoutes: Routes = [
    { path: "functional-department", component: FunctionalDepartmentComponent, data: { title: "Functional Department" } },
    { path: "functional-designation", component: FunctionalDesignationComponent, data: { title: "Functional Designation" } },
    { path: "functional-title", component: FunctionalTitleComponent, data: { title: "Functional Title" } },
    { path: "functional-group", component: FunctionalGroupComponent, data: { title: "Functional Group" } },
    { path: "band", component: BandComponent, data: { title: "Band" } },
    { path: "role", component: RoleComponent, data: { title: "Role" } },

    {
        path: 'employee',
        data: {
            breadcrumb: 'Employee',
            status: false
        },
        children: [
            { path: "list", component: EmployeeComponent, data: { title: "Employee" } },
            { path: "bulk-upload", component: BulkUploadComponent, data: { title: "Bulk Employee Add" } }

        ]
    },
    { path: "employee-salary/:id", component: EmployeeSalaryComponent, data: { title: "Employee Salary" } },
    { path: "employee-salary-detail/:id", component: EmployeeSalaryDetailComponent, data: { title: "Employee Salary Detail" } },
    { path: "employee-ctc/:id", component: AddEmpCtcComponent, data: { title: "Employee Ctc" } },
    {
        path: 'process-salary',
        data: {
            breadcrumb: 'Salary',
            status: false
        },
        children: [
            { path: "empsalary", component: ProcessSalaryComponent, data: { title: "Process Salary" } },
            { path: "check-salary", component: CheckSalaryComponent, data: { title: "Check Salary" } },
            { path: "salary-component", component: SalaryComponentComponent, data: { title: "Salary Component" } },
            { path: "salary-component/:id", component: SalaryComponentComponent, data: { title: "Salary Component" } },
            //{ path: "employee-attendence", component: EmployeeAttendenceComponent, data: { title: "Employee Attendence" } },
            //{ path: "employee-attendence/:id", component: EmployeeAttendenceComponent, data: { title: "Employee Attendence" } },
            { path: "all-emp-sal", component: AllEmpSalComponent, data: { title: "All Employee Salary" } },
            { path: "sal-com-list", component: SalComponentListComponent, data: { title: "Salary Component List" } },
        ]
    },
    { path: "view-salary/:id", component: ViewSalaryComponent, data: { title: "View Salary" } },
    //{
    //    path: 'upload-att-summary',
    //    data: {
    //        breadcrumb: 'Attendance',
    //        status: false
    //    },
    //    children: [
    //        { path: "upload-attendance-summary", component: UploadAttSummaryComponent, data: { title: "Upload Attendance Summary" } },
    //        { path: "leave-entry", component: LeaveEntryComponent, data: { title: "Attendance Detail'" } },
    //        { path: "excel-upload", component: ExcelUploadComponent, data: { title: "Upload Attendance Detail" } },
    //        { path: "view-summary", component: ViewAttSummaryComponent, data: { title: "View Attendence Summary" } }
    //    ]
    //},
];


@NgModule({
  imports: [
    CommonModule,
        RouterModule.forChild(MaintenanceRoutes),
    SharedModule,
    DataTableModule,
    SelectModule,
    FormsModule,
    DatepickerModule,
    NgxDatatableModule,
    ReactiveFormsModule,
        ToastyModule.forRoot(),
        ModalModule.forRoot(),
    NotificationsModule
  ],
    providers: [AccountService, CreateExpenseItemService, ToastyService, AccountEndpoint, DepartmentService, TitleService, GroupService, BandService, FunctionalDesignationService, BulkUploadService, EmployeeSalaryService],
  declarations: [
    MaintenanceComponent,
    EmployeeComponent,
    UserInfoComponent,
    FunctionalDepartmentComponent,
    FunctionalDepartmentInfoComponent,
    FunctionalDesignationComponent,
    FunctionalDesignationInfoComponent,
    FunctionalTitleComponent, FunctionalTitleInfoComponent,
    FunctionalGroupComponent, FunctionalGroupInfoComponent,
    BandComponent,
    BandInfoComponent,
    RoleComponent,
    RoleInfoComponent,
    BulkUploadComponent,
    GroupByPipe,
    EmployeeDetailsComponent,
    EmployeeSalaryComponent,
    EmployeeSalaryDetailComponent,
    AddEmpCtcComponent,
    ProcessSalaryComponent,
    CheckSalaryComponent,
    ViewSalaryComponent,
    SalaryComponentComponent,
    //LeaveEntryComponent,
    //EmployeeAttendenceComponent,
    //ExcelUploadComponent,
    //UploadAttSummaryComponent,
    AllEmpSalComponent,
   /* ViewAttSummaryComponent,*/
    SalComponentListComponent
  ]
})
export class MaintenanceModule { }
