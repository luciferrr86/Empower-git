import { Component, OnInit } from '@angular/core';
import { RatingModel } from '../../../../models/performance/common/rating.model';
import { ReviewGoalService } from '../../../../services/performance/review-goal/review-goal.service';
import { ActivatedRoute } from '@angular/router';
import { AlertService } from '../../../../services/common/alert.service';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'review-review-rating',
  templateUrl: './review-review-rating.component.html',
  styleUrls: ['./review-review-rating.component.css']
})
export class ReviewReviewRatingComponent implements OnInit {
  public rating: RatingModel = new RatingModel();
  public isSaving: boolean;
  public isSubmitting: boolean;
  public empId: string;
  public selectedRating: string;
  public ratingInfoForm: NgForm;
  constructor(private reviewGoalService: ReviewGoalService, private route: ActivatedRoute, private alertService: AlertService) { }

  ngOnInit() {
    this.route.queryParams.subscribe(params => {
      this.empId = params['empid'];
    });
    this.getRatingDetail();
  }
  getRatingDetail() {
    this.reviewGoalService.getRating(this.empId).subscribe(result => this.onSuccessfulDataLoad(result), error => this.onDataLoadFailed(error));
  }

  onSuccessfulDataLoad(rating: RatingModel) {
    this.rating = rating;
    // if (this.rating.checkSaveSubmit.enableMidYear && !this.rating.checkSaveSubmit.isMidYearReviewCompleted) {
    //   if (this.rating.checkSaveSubmit.isMgrRatingSubmit)

    // }
    // else{
    //   if (this.rating.checkSaveSubmit.isMgrYearRatingSubmit)

    // }

  }
  onDataLoadFailed(error: any) {
    this.alertService.showInfoMessage("Unable to retrieve list from the server");
  }

  onYearRatingSelectionChange(id) {
    this.rating.endYearRating.ratingId = id;
  }
  onMidYearRatingSelectionChange(id) {
    this.rating.midYearRating.ratingId = id;
  }
  saveRating() {
    this.isSaving = true;
    this.reviewGoalService.saveRating(this.empId, this.rating, 'save').subscribe(result => this.onSaveSubmitSuccessfulDataLoad(result, 'save'), error => this.onSaveSubmitDataLoadFailed(error));

  }
  submitRating() {
    this.isSubmitting = true;
    this.reviewGoalService.saveRating(this.empId, this.rating, 'submit').subscribe(result => this.onSaveSubmitSuccessfulDataLoad(result, 'submit'), error => this.onSaveSubmitDataLoadFailed(error));
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
