import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ExpenceManagementComponent } from './expence-management/expence-management.component';
import { EmployeesRequestComponent } from './employees-request/employees-request.component';
import { ApproveRequestComponent } from './approve-request/approve-request.component';
import { Routes, RouterModule } from '@angular/router';
import { SharedModule } from '../../shared/shared.module';
import { ExpenseBookingRequestComponent } from './expense-booking-request/expense-booking-request.component';
import { ExpenseBookingService } from '../../services/expense-booking/expense-booking.service';
import { AccountService } from '../../services/account/account.service';
import { ToastyModule } from 'ng2-toasty';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { NgxDatatableModule } from '@swimlane/ngx-datatable';
import { SelectModule } from 'ng-select';
import { DataTableModule } from 'angular2-datatable';
import { SqueezeBoxModule } from 'squeezebox';
import { ExpenseBookingUploadComponent } from './expense-booking-upload/expense-booking-upload.component';
import { ApproveRejectDetailComponent } from './approve-reject-detail/approve-reject-detail.component';
import { ApprovedRejectInviteDetailComponent } from './approved-reject-invite-detail/approved-reject-invite-detail.component';
import { ApprovedBookingComponent } from './approved-booking/approved-booking.component';
import { ApprovedBookingDetailComponent } from './approved-booking-detail/approved-booking-detail.component';



export const ExpenseBookingRoutes: Routes = [

      { path: "expense-booking", component: ExpenceManagementComponent, data: { title: "Expence Management" } },
      { path: 'employees-request', component: EmployeesRequestComponent },
      { path: "approve-request", component: ApproveRequestComponent },
      { path: "request-detail", component: ApproveRejectDetailComponent },
      { path: "invite-request-detail", component: ApprovedRejectInviteDetailComponent },
      { path: "expense-booking-request", component: ExpenseBookingRequestComponent },
      { path: "approved-booking", component: ApprovedBookingComponent },
      { path: "approved-booking-detail", component: ApprovedBookingDetailComponent }
  
];

@NgModule({
  imports: [
    CommonModule,
    SqueezeBoxModule,
    RouterModule.forChild(ExpenseBookingRoutes),
    SharedModule,
    DataTableModule,
    SelectModule,
    FormsModule,
    NgxDatatableModule,
    ReactiveFormsModule,
    ToastyModule.forRoot()
  ],
  providers: [ExpenseBookingService, AccountService],
  declarations: [ExpenceManagementComponent, EmployeesRequestComponent, ApproveRequestComponent, ExpenseBookingRequestComponent, ExpenseBookingUploadComponent, ApproveRejectDetailComponent, ApprovedRejectInviteDetailComponent, ApprovedBookingComponent, ApprovedBookingDetailComponent]
})
export class ExpenseBookingModule { }
