import { Component, OnInit } from '@angular/core';
import { AccountService } from '../../../../services/account/account.service';
import { MyGoalService } from '../../../../services/performance/my-goal/my-goal.service';
import { AlertService } from '../../../../services/common/alert.service';
import { GoalViewModel } from '../../../../models/performance/common/performance-goal.model';


@Component({
  selector: 'performance-goal',
  templateUrl: './performance-goal.component.html',
  styleUrls: ['./performance-goal.component.css']
})
export class PerformanceGoalComponent implements OnInit {

  public currentYearGoal: GoalViewModel = new GoalViewModel();
  constructor(private accountService: AccountService, private myGoalService: MyGoalService, private alertService: AlertService) { }

  ngOnInit() {
    this.getCurrentYearGoal();
  }

  getCurrentYearGoal() {
    this.myGoalService.getCurrentYearGoal(this.accountService.currentUser.id).subscribe(result => this.onSuccessfulDataLoad(result), error => this.onDataLoadFailed(error));
  }

  onSuccessfulDataLoad(currentYearGoal: GoalViewModel) {
    this.currentYearGoal = currentYearGoal;
  }

  onDataLoadFailed(error: any) {
    this.alertService.showInfoMessage("Unable to retrieve list from the server");
  }

  saveGoal() {
    this.myGoalService.saveCurrentYearGoal(this.accountService.currentUser.id, this.currentYearGoal, 'save').subscribe(result => this.onSaveSuccessfulDataLoad(result, 'save'), error => this.onSaveDataLoadFailed(error));
  }

  onSaveSuccessfulDataLoad(res: any, actionType: string) {
    this.getCurrentYearGoal();
    if (actionType == "save") {
      this.alertService.showInfoMessage("Goals saved successfully.");
    }
    else {
      this.alertService.showInfoMessage("Goals submitted successfully.");
    }

  }

  onSaveDataLoadFailed(error: any) {
    this.alertService.showInfoMessage("Unable to save the goal list");
  }
  valuechange(event, id) {
    if (this.currentYearGoal.endYearGoalMeasureList.length > 0) {
      let exist = this.currentYearGoal.endYearGoalMeasureList.find(m => m.goalId == id);
      if (exist != undefined) {
        exist.accomplishment = event.target.value;
      }
    }
  }
  submitGoal() {
    this.myGoalService.saveCurrentYearGoal(this.accountService.currentUser.id, this.currentYearGoal, 'submit').subscribe(result => this.onSaveSuccessfulDataLoad(result, 'submit'), error => this.onSaveDataLoadFailed(error));
  }
}
