import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { AlertService } from '../../../../services/common/alert.service';
import { ReviewGoalService } from '../../../../services/performance/review-goal/review-goal.service';
import { EmployeeDetail } from '../../../../models/performance/common/emp-detail.model';

@Component({
  selector: 'review-emp-detail',
  templateUrl: './review-emp-detail.component.html',
  styleUrls: ['./review-emp-detail.component.css']
})
export class ReviewEmpDetailComponent implements OnInit {

  employeeDetail: EmployeeDetail = new EmployeeDetail();
  loadingIndicator: boolean = true;

  constructor(private route: ActivatedRoute, private alertService: AlertService, private reviewGoalService: ReviewGoalService) {
    this.route.queryParams.subscribe(params => {
      if (params['empid']) {
        this.reviewGoalService.getEmployeeDetails(params['empid']).subscribe(result => this.onSuccessfulDataLoad(result), error => this.onDataLoadFailed(error));
      }
    });
  }

  ngOnInit() {
  }
  addFeedback() { }

  onSuccessfulDataLoad(empDetail: EmployeeDetail) {
    this.employeeDetail = empDetail;
    this.loadingIndicator = false;
  }
  onDataLoadFailed(error: any) {
    this.alertService.showInfoMessage("Unable to retrieve list from the server");
    this.loadingIndicator = false;
  }
}
