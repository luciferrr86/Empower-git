import { Component, OnInit, ViewChild, TemplateRef } from '@angular/core';
import { Http, Response } from "@angular/http";

import { HttpClient } from 'selenium-webdriver/http';
import { ReviewGoalService } from '../../../../services/performance/review-goal/review-goal.service';
import { AccountService } from '../../../../services/account/account.service';
import { AlertService } from '../../../../services/common/alert.service';
import { Router } from '@angular/router';
import { ReviewEmpDetailComponent } from '../review-emp-detail/review-emp-detail.component';
import { EmployeeDetail } from '../../../../models/performance/common/emp-detail.model';
import { ReviewGoalViewModel } from '../../../../models/performance/review-goal/employee-details-model';

@Component({
  selector: 'app-review-goal',
  templateUrl: './review-goal.component.html',
  styleUrls: ['./review-goal.component.css']
})
export class ReviewGoalComponent implements OnInit {

  columns: any[] = [];
  filterQuery: string = "";
  pageNumber = 0;
  count = 0;
  pageSize = 10;

  row: EmployeeDetail[] = [];
  loadingIndicator: boolean = true;

  @ViewChild('actionsTemplate')
  actionsTemplate: TemplateRef<any>;
  @ViewChild('reviewEmployee')
  reviewEmployee: ReviewEmpDetailComponent;

  constructor(private router: Router, public http: Http, private reviewGoalService: ReviewGoalService, private accountService: AccountService, private alertService: AlertService) { }
  rows = [];
  ngOnInit() {
    this.columns = [
      { prop: 'name', name: 'Employee Name' },
      { prop: 'functionalGroup', name: 'Functional Group' },
      { prop: 'designation', name: 'Designation' },
      { prop: 'status', name: 'Status' },
      { prop: 'Action', name: 'Action', cellTemplate: this.actionsTemplate, resizeable: false, canAutoResize: false, sortable: false, draggable: false }
    ];
    this.getEmployeeList(this.pageNumber, this.pageSize, this.filterQuery);
  }

  viewReview(empId) {
    this.router.navigate(['../performance/review-emp-detail'], { queryParams: { empId: empId }, skipLocationChange: true });
  }

  getEmployeeList(page?: number, pageSize?: number, name?: string) {
    this.reviewGoalService.getEmployeeList(this.accountService.currentUser.id, page, pageSize, name).subscribe(result => this.onSuccessfulDataLoad(result), error => this.onDataLoadFailed(error));
  }
  onSuccessfulDataLoad(emp: ReviewGoalViewModel) {
    this.rows = emp.employeeDetailList;
    emp.employeeDetailList.forEach((employee, index, employees) => {
      (<any>employee).index = index + 1;
    });
    this.count = emp.totalCount;
    this.loadingIndicator = false;
  }

  onDataLoadFailed(error: any) {
    this.alertService.showInfoMessage("Unable to retrieve list from the server");
  }
  getFilteredData(filterString) {
    this.getEmployeeList(this.pageNumber, this.pageSize, this.filterQuery);

  }
  getData(e) {
    this.getEmployeeList(this.pageNumber, e, this.filterQuery);
  }
  setPage(e) {
    this.getEmployeeList(e.offset, this.pageSize, this.filterQuery);
  }
}
