import { Component, OnInit } from '@angular/core';
import { AlertService } from '../../../../services/common/alert.service';
import { ReviewGoalService } from '../../../../services/performance/review-goal/review-goal.service';
import { AccountService } from '../../../../services/account/account.service';
import { ActivatedRoute } from '@angular/router';
import { GoalViewModel, GoalMeasure } from '../../../../models/performance/common/performance-goal.model';

@Component({
  selector: 'review-performance-goal',
  templateUrl: './review-performance-goal.component.html',
  styleUrls: ['./review-performance-goal.component.css']
})
export class ReviewPerformanceGoalComponent implements OnInit {

  public currentYearGoal: GoalViewModel = new GoalViewModel();
  public isSaving: boolean;
  public isSubmitting: boolean;
  loadingIndicator: boolean = true;
  empId: null;

  constructor(private route: ActivatedRoute, private accountService: AccountService, private alertService: AlertService, private reviewGoalService: ReviewGoalService) {
    this.route.queryParams.subscribe(params => {
      this.empId = params['empid'];
    });
  }

  ngOnInit() {
    this.getPerformanceGoal();
  }

  getPerformanceGoal() {
    this.reviewGoalService.getEmployeePerformance(this.empId).subscribe(result => this.onSuccessfulDataLoad(result), error => this.onDataLoadFailed(error));
  }

  saveGoal() {
    this.reviewGoalService.saveCurrentYearGoal(this.empId, this.currentYearGoal, "save").subscribe(result => this.onSaveSubmitSuccessfulDataLoad(result, "save"), error => this.onSaveDataLoadFailed(error));
  }

  submitGoal() {
    this.reviewGoalService.saveCurrentYearGoal(this.empId, this.currentYearGoal, "submit").subscribe(result => this.onSaveSubmitSuccessfulDataLoad(result, "submit"), error => this.onSaveDataLoadFailed(error));
  }

  onSaveSubmitSuccessfulDataLoad(res: any, actionType: string) {
    if (actionType == "save") {
      this.isSaving = false;
      this.alertService.showInfoMessage("Goals saved successfully.");
    }
    else {
      this.isSubmitting = false;
      this.alertService.showInfoMessage("Goals submitted successfully.");
    }
    this.getPerformanceGoal();
  }


  onSaveDataLoadFailed(error: any) {
    this.alertService.showInfoMessage("Unable to save the goal list");
  }

  onSuccessfulDataLoad(empDetails: GoalViewModel) {
    this.currentYearGoal = empDetails;
    this.loadingIndicator = false;
  }
  onDataLoadFailed(error: any) {
    this.alertService.showInfoMessage("Unable to retrieve list from the server");
    this.loadingIndicator = false;
  }
}
