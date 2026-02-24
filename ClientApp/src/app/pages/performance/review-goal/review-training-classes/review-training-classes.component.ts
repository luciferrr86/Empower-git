import { Component, OnInit } from '@angular/core';
import { AccountService } from '../../../../services/account/account.service';
import { TrainingClassesModel } from '../../../../models/performance/common/training-classes.model';
import { AlertService } from '../../../../services/common/alert.service';
import { ReviewGoalService } from '../../../../services/performance/review-goal/review-goal.service';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'review-training-classes',
  templateUrl: './review-training-classes.component.html',
  styleUrls: ['./review-training-classes.component.css']
})
export class ReviewTrainingClassesComponent implements OnInit {
  public trainingClasses: TrainingClassesModel = new TrainingClassesModel();
  constructor(private reviewGoalService: ReviewGoalService, private accountService: AccountService, private alertService: AlertService, private route: ActivatedRoute) { }

  ngOnInit() {
    this.getTrainingClassesList();
  }

  getTrainingClassesList() {
    this.route.queryParams.subscribe(params => {
      if (params['empid']) {
        this.reviewGoalService.getTrainingClassesList(params['empid']).subscribe(result => this.onSuccessfulDataLoad(result), error => this.onDataLoadFailed(error));
      }
    });

  }
  onSuccessfulDataLoad(trainingClasses: TrainingClassesModel) {
    this.trainingClasses = trainingClasses;

  }

  onDataLoadFailed(error: any) {
    this.alertService.showInfoMessage("Unable to retrieve list from the server");
  }
}
