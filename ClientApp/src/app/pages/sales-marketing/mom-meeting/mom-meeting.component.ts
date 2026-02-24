import { Component, OnInit } from '@angular/core';
import { IOption } from 'ng-select';
import { MomMeetingDetail } from '../../../models/sales-marketing/Schedule-view.model';
import { ActivatedRoute, Router } from '@angular/router';
import { SalesMarketingService } from '../../../services/sales-marketing/sales-marketing.service';
import { AlertService } from '../../../services/common/alert.service';
import { Utilities } from '../../../services/common/utilities';

@Component({
  selector: 'app-mom-meeting',
  templateUrl: './mom-meeting.component.html',
  styleUrls: ['./mom-meeting.component.css']
})
export class MomMeetingComponent implements OnInit {

  public meetingId = "";
  public companyId = "";
  public isSaving = false;
  momMeeting: MomMeetingDetail = new MomMeetingDetail();
  internalEmployee: Array<IOption> = [];
  externalClient: Array<IOption> = [];
  constructor(private route: ActivatedRoute, private salesMarketingService: SalesMarketingService, private alertService: AlertService) { }
  ngOnInit() {
    this.getMomMeeting();

  }
  getMomMeeting() {
    this.route.queryParams.subscribe(params => {
      if (params['id']) {
        if (params['companyid']) {
          this.companyId = params['companyid'];
        }
        this.meetingId = params['id'];
        this.salesMarketingService.getMeetingMom(this.meetingId).subscribe(result => this.onSuccessfulDataLoad(result), error => this.onDataLoadFailed());
      }
    });
  }
  onSuccessfulDataLoad(result: MomMeetingDetail) {
    this.momMeeting = result;
    this.internalEmployee = result.selectInternalPerson;
    this.externalClient = result.selectInternalPerson;
  }

  onDataLoadFailed() {

    this.alertService.showInfoMessage("Unable to retrieve list from the server");

  }
  public save() {
    this.isSaving = true;
    this.momMeeting.meetingId = this.meetingId
    this.salesMarketingService.saveMeetingMom(this.momMeeting).subscribe(sucess => this.saveSuccessHelper(), error => this.saveFailedHelper(error));
  }


  private saveSuccessHelper() {
    this.isSaving = false;
    this.alertService.showSucessMessage("Schedule saved successfully");

  }

  private saveFailedHelper(error: any) {
    this.isSaving = false;
    let test = Utilities.getHttpResponseMessage(error);
    this.alertService.showInfoMessage("Please try later" + test[0]);
  }


}
