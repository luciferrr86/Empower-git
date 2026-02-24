import { Component, OnInit, ViewChild } from '@angular/core';
import { ExternalFeedback } from '../../../../models/performance/my-goal/external-feedback.model';
import { ExternalFeedbackComponent } from '../external-feedback/external-feedback.component';
import { MyGoalService } from '../../../../services/performance/my-goal/my-goal.service';
import { AlertService } from '../../../../services/common/alert.service';
import { AccountService } from '../../../../services/account/account.service';
import { EmployeeDetail } from '../../../../models/performance/common/emp-detail.model';

@Component({
  selector: 'emp-detail',
  templateUrl: './emp-detail.component.html',
  styleUrls: ['./emp-detail.component.css']
})
export class EmpDetailComponent implements OnInit {

  feedback: ExternalFeedback;
  public empDetail: EmployeeDetail = new EmployeeDetail();

  @ViewChild("feedbackInfo")
  feedbackInfo: ExternalFeedbackComponent
  constructor(private myGoalService: MyGoalService, private alertService: AlertService, private accountService: AccountService) { }

  ngOnInit() {
    this.getEmployeeDetail();
  }

  getEmployeeDetail() {
    this.myGoalService.getEmployeeDetail(this.accountService.currentUser.id).subscribe(result => this.onSuccessfulDataLoad(result), error => this.onDataLoadFailed());
  }
  onSuccessfulDataLoad(employeeDetail: EmployeeDetail) {
    this.empDetail = employeeDetail;
  }

  onDataLoadFailed() {
    this.alertService.showInfoMessage("Unable to retrieve list from the server");
  }
  addFeedback() {
    this.feedback = this.feedbackInfo.addFeddback();
  }
}
