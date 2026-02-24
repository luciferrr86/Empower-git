import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { CompanyListComponent } from './company-list/company-list.component';
import { SalesMarketingService } from '../../services/sales-marketing/sales-marketing.service';
import { SelectModule } from 'ng-select';
import { ReactiveFormsModule } from '@angular/forms';
import { NgxDatatableModule } from '@swimlane/ngx-datatable';
import { CompanyInfoComponent } from './company-info/company-info.component';
import { CompanyContactEditorComponent } from './company-contact-editor/company-contact-editor.component';
import { StatusListComponent } from './status-list/status-list.component';
import { ScheduleListComponent } from './schedule-list/schedule-list.component';
import { SharedModule } from '../../shared/shared.module';
import { ScheduleMeetingComponent } from './schedule-meeting/schedule-meeting.component';
import { MomMeetingComponent } from './mom-meeting/mom-meeting.component';

export const SalesRoutes: Routes = [
  { path: "", component: CompanyListComponent, data: { title: "Sales Tracker" } },
  { path: "companyinfo", component: CompanyInfoComponent, data: { title: "Sales Tracker1" } },
  { path: "statuslist", component: StatusListComponent, data: { title: "Sales Tracker1" } },
  { path: "schedulelist", component: ScheduleListComponent, data: { title: "Sales Tracker1" } },
  { path: "schedule-meeting", component: ScheduleMeetingComponent, data: { title: "Sales Tracker1" } },
  { path: "mom-meeting", component: MomMeetingComponent, data: { title: "Sales Tracker1" } }

];

@NgModule({
  imports: [
    ReactiveFormsModule,
    NgxDatatableModule,
    CommonModule,
    RouterModule.forChild(SalesRoutes),
    SelectModule,
    SharedModule,
  ],

  providers: [SalesMarketingService],
  declarations: [CompanyListComponent, CompanyInfoComponent, CompanyContactEditorComponent, StatusListComponent, ScheduleListComponent, ScheduleMeetingComponent, MomMeetingComponent]
})
export class SalesmarketingModule { }
