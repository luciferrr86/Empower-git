import { Component, OnInit } from '@angular/core';
import { MyGoalService } from '../../../../services/performance/my-goal/my-goal.service';
import { AccountService } from '../../../../services/account/account.service';
import { AlertService } from '../../../../services/common/alert.service';
import { RatingModel } from '../../../../models/performance/common/rating.model';

@Component({
  selector: 'rating',
  templateUrl: './rating.component.html',
  styleUrls: ['./rating.component.css']
})
export class RatingComponent implements OnInit {

  constructor(private myGoalService: MyGoalService, private accountService: AccountService, private alertService: AlertService) { }

  public rating: RatingModel = new RatingModel();
  public isSaving: boolean;
  public isSubmitting: boolean;
  ngOnInit() {
    this.getRatingDetail();
  }

  getRatingDetail() {
    this.myGoalService.getRating(this.accountService.currentUser.id).subscribe(result => this.onSuccessfulDataLoad(result), error => this.onDataLoadFailed(error));
  }

  onSuccessfulDataLoad(rating: RatingModel) {
    this.rating = rating;
  }

  onDataLoadFailed(error: any) {
    this.alertService.showInfoMessage("Unable to retrieve list from the server");
  }

  saveRating() {
    this.isSaving = true;
    this.myGoalService.saveRating(this.accountService.currentUser.id, this.rating, 'save').subscribe(result => this.onSaveSubmitSuccessfulDataLoad(result, 'save'), error => this.onSaveSubmitDataLoadFailed(error));

  }
  submitRating() {
    this.isSubmitting = true;
    this.myGoalService.saveRating(this.accountService.currentUser.id, this.rating, 'submit').subscribe(result => this.onSaveSubmitSuccessfulDataLoad(result, 'submit'), error => this.onSaveSubmitDataLoadFailed(error));
  }
  onSaveSubmitSuccessfulDataLoad(res: any, actionType: string) {
    if (actionType == "save") {
      this.isSaving = false;
      this.alertService.showInfoMessage("Rating saved successfully.");
    }
    else {
      this.isSubmitting = false;
      this.alertService.showInfoMessage("Rating submitted successfully.");
    }
    this.getRatingDetail();
  }
  onSaveSubmitDataLoadFailed(error: any) {
    this.isSaving = false;
    this.isSubmitting = false;
    this.alertService.showInfoMessage("Unable to save/submit the ratings");
  }
}
