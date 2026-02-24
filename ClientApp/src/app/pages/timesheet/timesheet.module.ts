import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Routes, RouterModule } from '@angular/router';
import { EmployeeTimesheetComponent } from './my-timesheet/employee-timesheet/employee-timesheet.component';
import { TimesheetEmployeeListComponent } from './manage-timesheet/timesheet-employee-list/timesheet-employee-list.component';
import { ReviewEmployeeTimesheetComponent } from './manage-timesheet/review-employee-timesheet/review-employee-timesheet.component';
import { SharedModule } from '../../shared/shared.module';
import { SelectModule } from 'ng-select';
import { ReactiveFormsModule } from '@angular/forms';
import { FormWizardModule } from 'angular2-wizard';
import { MyTimesheetService } from '../../services/timesheet/my-timesheet.service';
import { ManageTimesheetService } from '../../services/timesheet/manage-timesheet.service';

export const TimesheetRoutes: Routes = [
  { path: "my-timesheet", component: EmployeeTimesheetComponent},
  { path: 'manage-timesheet', component: TimesheetEmployeeListComponent },
  { path: 'review-employee-timesheet', component: ReviewEmployeeTimesheetComponent },


];

@NgModule({
  imports: [
    CommonModule,
    RouterModule.forChild(TimesheetRoutes),
    SharedModule,
    FormWizardModule,
    SelectModule,
    ReactiveFormsModule,
  ],
  providers :[MyTimesheetService,ManageTimesheetService],
  declarations: [EmployeeTimesheetComponent,TimesheetEmployeeListComponent,ReviewEmployeeTimesheetComponent]
})
export class TimesheetModule { }
