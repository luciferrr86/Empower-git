import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { InterviewScheduling, SelectTimeModel, SelectTime } from '../../../../models/recruitment/bulk-scheduling/interview-schedule.model';
import { BulkInterviewScheduleService } from '../../../../services/recruitment/bulk-interview-schedule.service';
import { Utilities } from '../../../../services/common/utilities';
import { AlertService } from '../../../../services/common/alert.service';
import { Router } from '@angular/router';

@Component({
  selector: 'mass-interview-schedule',
  templateUrl: './mass-interview-schedule.component.html',
  styleUrls: ['./mass-interview-schedule.component.css']
})
export class MassInterviewScheduleComponent implements OnInit {

  public isSaving = false;
  startDateData: Date;
  endDateData: Date;
  public interviewSchedule: InterviewScheduling = new InterviewScheduling();
  public serverCallback: () => void;
  public selectTimeModel: SelectTimeModel = new SelectTimeModel();
  @Output()
  change: EventEmitter<string> = new EventEmitter<string>();

  constructor(private route: Router, private bulkInterviewScheduleService: BulkInterviewScheduleService, private alertService: AlertService) { }

  ngOnInit() {
  }

  save() {
    this.isSaving = true;
    this.interviewSchedule.listTime = this.selectTimeModel.datTimeList;
    this.bulkInterviewScheduleService.saveDateTime(this.interviewSchedule).subscribe(sucess => this.saveSuccessHelper(sucess), error => this.saveFailedHelper(error));
  }
  private saveSuccessHelper(result?: string) {
    this.change.emit(result);

    this.isSaving = false;
    this.alertService.showSucessMessage("Saved successfully");
    this.route.navigate(['../recruitment/bulk-scheduling'], { queryParams: { id: result } });

  }

  private saveFailedHelper(error: any) {
    this.isSaving = false;
    let test = Utilities.getHttpResponseMessage(error);
    this.alertService.showInfoMessage("Please try later" + test[0]);
  }
  startDate(dateString: string) {
    if (dateString) {
      this.startDateData = new Date(dateString);
      this.getDateArray(this.startDateData, this.endDateData);
    }
  }
  endDate(dateString: string) {
    if (dateString) {
      this.endDateData = new Date(dateString);
      this.getDateArray(this.startDateData, this.endDateData);
    }
  }
  getDateArray(start, end) {
    this.selectTimeModel = new SelectTimeModel();
    let dt = new Date(start);
    while (dt <= end) {
      var time = new SelectTime()
      time.date = new Date(dt)
      this.selectTimeModel.datTimeList.push(time);
      dt.setDate(dt.getDate() + 1);
    }
  }
  valuechange(event, date) {
    if (this.selectTimeModel.datTimeList.length > 0) {
      let exist = this.selectTimeModel.datTimeList.find(m => m.date == date);
      if (exist != undefined) {
        exist.startTime = event.target.value;

      }
    }

  }
  endDatechange(event, date) {
    if (this.selectTimeModel.datTimeList.length > 0) {
      let exist = this.selectTimeModel.datTimeList.find(m => m.date == date);
      if (exist != undefined) {
        exist.endTime = event.target.value;

      }
    }
  }
}
