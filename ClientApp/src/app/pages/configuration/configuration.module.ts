import { GeneralComponent } from './general/general/general.component';
import { NgModule } from '@angular/core';
import { Route, RouterModule, Routes } from '@angular/router';
import { ConfigurationComponent } from './configuration.component';
import { SqueezeBoxModule } from 'squeezebox';
import { JobTypeComponent } from './recruitment/job-type/job-type.component';
import { InterviewTypeComponent } from './recruitment/interview-type/interview-type.component';
import { CommonModule } from '@angular/common';
import { SharedModule } from '../../shared/shared.module';
import { DataTableModule } from 'angular2-datatable';
import { SelectModule } from 'ng-select';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgxDatatableModule } from '@swimlane/ngx-datatable'; 
import { ToastyModule } from 'ng2-toasty';

import { JobTypeService } from '../../services/configuration/recruitment/job-type.service';
import { InterviewTypeService } from '../../services/configuration/recruitment/interview-type.service';
import { JobTypeInfoComponent } from './recruitment/job-type-info/job-type-info.component';
import { InterviewTypeInfoComponent } from './recruitment/interview-type-info/interview-type-info.component';
import { LeavePeriodComponent } from './leave/leave-period/leave-period.component';
import { LeaveHolidaylistComponent } from './leave/leave-holidaylist/leave-holidaylist.component';
import { LeaveTypeComponent } from './leave/leave-type/leave-type.component';
import { LeavePeriodService } from '../../services/configuration/leave/leave-period.service';
import { LeaveHolidayListService } from '../../services/configuration/leave/leave-holiday-list.service';
import { LeaveTypeService } from '../../services/configuration/leave/leave-type.service';
import { LeavePeriodInfoComponent } from './leave/leave-period-info/leave-period-info.component';
import { LeaveHolidaylistInfoComponent } from './leave/leave-holidaylist-info/leave-holidaylist-info.component';
import { LeaveTypeInfoComponent } from './leave/leave-type-info/leave-type-info.component';
import { TimesheetClientComponent } from './timesheet/timesheet-client/timesheet-client.component';
import { TimesheetConfigurationComponent } from './timesheet/timesheet-configuration/timesheet-configuration.component';
import { TimesheetTemplateComponent } from './timesheet/timesheet-template/timesheet-template.component';
import { TimesheetProjectComponent } from './timesheet/timesheet-project/timesheet-project.component';
import { TimesheetConfigurationService } from '../../services/configuration/timesheet/timesheet-configuration.service';
import { LeaveWorkingdayComponent } from './leave/leave-workingday/leave-workingday.component';
import { LeaveRulesComponent } from './leave/leave-rules/leave-rules.component';
import { LeaveRulesInfoComponent } from './leave/leave-rules-info/leave-rules-info.component';
import { LeaveRulesService } from '../../services/configuration/leave/leave-rules.service';
import { BandService } from '../../services/maintenance/band.service';
import { LeaveWorkingdayService } from '../../services/configuration/leave/leave-workingday.service';
import { TimesheetClientService } from '../../services/configuration/timesheet/timesheet-client.service';
import { TimesheetProjectService } from '../../services/configuration/timesheet/timesheet-project.service';
import { TimesheetClientInfoComponent } from './timesheet/timesheet-client-info/timesheet-client-info.component';
import { TimesheetProjectInfoComponent } from './timesheet/timesheet-project-info/timesheet-project-info.component';
import { TimesheetTemplateInfoComponent } from './timesheet/timesheet-template-info/timesheet-template-info.component';
import { TimesheetTemplateService } from '../../services/configuration/timesheet/timesheet-template.service';
import { TimesheetScheduleComponent } from './timesheet/timesheet-schedule/timesheet-schedule.component';
import { TimesheetScheduleInfoComponent } from './timesheet/timesheet-schedule-info/timesheet-schedule-info.component';
import { TimesheetAssignProjectComponent } from './timesheet/timesheet-assign-project/timesheet-assign-project.component';
import { PerformanceConfigComponent } from './performance/performance-config/performance-config.component';
import { FeedbackEditorComponent } from './performance/feedback-editor/feedback-editor.component';
import { RatingEditorComponent } from './performance/rating-editor/rating-editor.component';
import { PerformaceConfigurationService } from '../../services/configuration/performance/performace-configuration.service';
import { CategoryComponent } from './expense/category/category.component';
import { SubCategoryComponent } from './expense/sub-category/sub-category.component';
import { ExpenseItemComponent } from './expense/expense-item/expense-item.component';
import { ExpenseItemInfoComponent } from './expense/expense-item-info/expense-item-info.component';
import { CategoryInfoComponent } from './expense/category-info/category-info.component';
import { SubCategoryInfoComponent } from './expense/sub-category-info/sub-category-info.component';
import { ExpenseBookingService } from '../../services/expense-booking/expense-booking.service';
import { TitleAmountComponent } from './expense/title-amount/title-amount.component';

export const ConfigurationRoutes: Routes = [
  { path: "general", component: GeneralComponent, data: { title: "General Config" } },
  {
    path: 'recruitment-config',
    data: {
      breadcrumb: 'Recruitment',
      status: false
    },
    children: [
      { path: "job-type", component: JobTypeComponent, data: { title: "Job Type" } },
      { path: "interview-type", component: InterviewTypeComponent, data: { title: "Interview Type" } }

    ]
  },
  {
    path: 'leave-config',
    data: {
      breadcrumb: 'Leave',
      status: false
    },
    children:[
      {path:"leave-period",component:LeavePeriodComponent,data:{title:"Leave Period"}},
      {path:"leave-holiday",component:LeaveHolidaylistComponent,data:{title:"Leave HolidayList"}},
      {path:"leave-workingday",component:LeaveWorkingdayComponent,data:{title:"Leave workingdays"}},
      {path:"leave-type",component:LeaveTypeComponent,data:{title:"Leave Type"}},
      {path:"leave-rules",component:LeaveRulesComponent,data:{title:"Leave Rules"}},
    ]
  },
  {
    path: 'timesheet-config',
    data: {
      breadcrumb: 'Timesheet',
      status: false
    },
    children: [
      { path: "type", component: TimesheetConfigurationComponent, data: { title: "Timesheet Configuration" } },
      { path: "template", component: TimesheetTemplateComponent, data: { title: "Timesheet Template" } },
      { path: "schedule", component: TimesheetScheduleComponent, data: { title: "Timesheet Schedule" } },
      { path: "client", component: TimesheetClientComponent, data: { title: "Timesheet Client" } },
      { path: "project", component: TimesheetProjectComponent, data: { title: "Timesheet Project" } },
      { path: "timesheet-assign-project", component: TimesheetAssignProjectComponent, data: { title: "Timesheet Assign Project" } },
    ]
  },
  {
    path: 'expense-config',
    data: {
      breadcrumb: 'Expense Management',
      status: false
    },
    children: [
      { path: "category", component: CategoryComponent, data: { title: "Category" } },
      { path: "sub-category", component: SubCategoryComponent, data: { title: "Sub Category" } },
      { path: "item", component: ExpenseItemComponent, data: { title: "Expense Item" } },
      { path: "title", component: TitleAmountComponent, data: { title: "Title Amount" } },
    ]
  },
  { path: "performance-config", component: PerformanceConfigComponent, data: { title: "Performance Config" } }
];


@NgModule({
  imports: [
    CommonModule,
    SqueezeBoxModule,
    RouterModule.forChild(ConfigurationRoutes),
    SharedModule,
    DataTableModule,
    SelectModule,
    FormsModule,
    NgxDatatableModule,
    ReactiveFormsModule,
    ToastyModule.forRoot()
  ],
  providers: [JobTypeService, InterviewTypeService,LeaveWorkingdayService,
    BandService,LeaveRulesService, LeavePeriodService, LeaveHolidayListService, 
    LeaveHolidayListService, LeaveTypeService,TimesheetConfigurationService,ExpenseBookingService,
    TimesheetClientService,TimesheetProjectService,PerformaceConfigurationService,TimesheetTemplateService],
  declarations: [
    ConfigurationComponent,
    GeneralComponent,
    JobTypeComponent,
    InterviewTypeInfoComponent,
    JobTypeInfoComponent,
    InterviewTypeComponent,
    LeavePeriodComponent,
    LeaveHolidaylistComponent,
    LeavePeriodInfoComponent,
    LeaveHolidaylistInfoComponent,
    LeaveTypeComponent,
    TimesheetConfigurationComponent,
    TimesheetTemplateComponent,
    TimesheetClientComponent,
    TimesheetClientInfoComponent,
    TimesheetProjectComponent,
    TimesheetProjectInfoComponent,
    TimesheetAssignProjectComponent,
    TimesheetScheduleComponent,
    TimesheetScheduleInfoComponent,
    TimesheetTemplateInfoComponent,
    LeaveTypeInfoComponent,
    LeaveRulesComponent,
    LeaveRulesInfoComponent,
   LeaveWorkingdayComponent,
   PerformanceConfigComponent,
   FeedbackEditorComponent,
   RatingEditorComponent,
   CategoryComponent,
   SubCategoryComponent,
   ExpenseItemComponent,
   ExpenseItemInfoComponent,
   CategoryInfoComponent,
   SubCategoryInfoComponent,
   TitleAmountComponent,
  ]
})
export class ConfigurationModule { }
