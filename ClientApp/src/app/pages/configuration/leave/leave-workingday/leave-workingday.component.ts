import { Component, OnInit, ViewChild, TemplateRef } from '@angular/core';
import { WorkingDay } from '../../../../models/configuration/leave/leave-workingday.model';
import { LeaveWorkingdayService } from '../../../../services/configuration/leave/leave-workingday.service';
import { AlertService } from '../../../../services/common/alert.service';
import { Utilities } from '../../../../services/common/utilities';

@Component({
  selector: 'leave-workingday',
  templateUrl: './leave-workingday.component.html',
  styleUrls: ['./leave-workingday.component.css']
})
export class LeaveWorkingdayComponent implements OnInit {
  columns: any[] = [];
  filterQuery: string = "";
  pageNumber = 0;
  count = 0;
  pageSize = 10;
  rows: WorkingDay[] = [];
  loadingIndicator: boolean = true;
  workingdayList: WorkingDay[];
  public isSaving = false;

  @ViewChild('actionsTemplate')
  actionsTemplate: TemplateRef<any>;

  constructor(private workingdayService: LeaveWorkingdayService, private alertService: AlertService) { }

  ngOnInit() {

    this.getAllWorkingDay();
  }

  getAllWorkingDay() {
    this.workingdayService.getAllWorkingDay().subscribe(result => this.onSuccessfulDataLoad(result), error => this.onDataLoadFailed());
  }

  onSuccessfulDataLoad(workingday: WorkingDay[]) {
    this.workingdayList = workingday;
    this.loadingIndicator = false;
  }

  onDataLoadFailed() {
    this.alertService.showInfoMessage("Unable to retrieve list from the server");
    this.loadingIndicator = false;
  }




  changeCheckBoxValue(event: any, id) {
    let test = this.workingdayList.find(m => m.id == id);
    if (test != undefined) {
      if (event.target.checked) {
        test.workingDayValue = '1';
      }
      else {
        test.workingDayValue = '0';
      }
    }
  }

  save() {
    this.workingdayService.update(this.workingdayList).subscribe(sucess => this.saveSuccessHelper(), error => this.saveFailedHelper(error));

  }

  private saveSuccessHelper() {

    this.alertService.showSucessMessage("Updated successfully");
    this.getAllWorkingDay();
  }


  private saveFailedHelper(error: any) {
    this.isSaving = false;
    let test = Utilities.getHttpResponseMessage(error);
    this.alertService.showInfoMessage("Please try later" + test[0]);
  }

}
