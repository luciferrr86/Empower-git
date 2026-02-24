import { Component, OnInit } from '@angular/core';
import { IOption } from 'ng-select';
import { ScheduleCompany } from '../../../models/sales-marketing/Schedule-view.model';
import { ActivatedRoute, Router } from '@angular/router';
import { SalesMarketingService } from '../../../services/sales-marketing/sales-marketing.service';
import { AlertService } from '../../../services/common/alert.service';
import { Utilities } from '../../../services/common/utilities';

@Component({
  selector: 'app-schedule-meeting',
  templateUrl: './schedule-meeting.component.html',
  styleUrls: ['./schedule-meeting.component.css']
})
export class ScheduleMeetingComponent implements OnInit {
  public companyId = "";
  public isSaving = false;
  public url: string;
  schedule: ScheduleCompany = new ScheduleCompany();
  internalEmployee: Array<IOption> = [];
  externalClient: Array<IOption> = [];
  constructor(private route: ActivatedRoute, private router: Router, private salesMarketingService: SalesMarketingService, private alertService: AlertService) { }
  ngOnInit() {
    this.getCompanyMeeting();

  }
  getCompanyMeeting() {
    this.route.queryParams.subscribe(params => {
      if (params['id']) {
        this.companyId = params['id'];
        this.salesMarketingService.getMeetingSchedule(this.companyId).subscribe(result => this.onSuccessfulDataLoad(result), error => this.onDataLoadFailed(error));
      }
    });
  }
  onSuccessfulDataLoad(result: any) {
    this.internalEmployee = result.selectInternalPerson;
    this.externalClient = result.selectClientPerson;
  }

  onDataLoadFailed(error: any) {

    this.alertService.showInfoMessage("Unable to retrieve list from the server");

  }
  public save() {
    this.isSaving = true;
    this.schedule.companyId = this.companyId
    this.salesMarketingService.saveMeetingSchedule(this.schedule).subscribe(sucess => this.saveSuccessHelper(sucess), error => this.saveFailedHelper(error));
  }
  refreshDocument(status: any) {
    this.url = status.imageUrl;
    this.schedule.fileId = status.pictureId;
  }
  checkValue(event: any) {

  }

  private saveSuccessHelper(result?: string) {
    this.isSaving = false;
    this.alertService.showSucessMessage("Schedule saved successfully");

  }

  private saveFailedHelper(error: any) {
    this.isSaving = false;
    let test = Utilities.getHttpResponseMessage(error);
    this.alertService.showInfoMessage("Please try later" + test[0]);
  }

}
