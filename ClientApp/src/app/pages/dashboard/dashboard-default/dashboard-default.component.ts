import { Component, OnInit } from '@angular/core';
import { DashboardService } from '../../../services/dashboard/dashboard.service';
import { AlertService } from '../../../services/common/alert.service';
import { Router } from '@angular/router';
import { AccountService } from '../../../services/account/account.service';
import { DashboardModel, PerformanceTask } from '../../../models/dashboard/dashboard.model';
import { AuthService } from '../../../services/common/auth.service';

@Component({
  selector: 'app-dashboard-default',
  templateUrl: './dashboard-default.component.html',
  styleUrls: [
    './dashboard-default.component.css',
    '../../../../assets/icon/svg-animated/svg-weather.css'
  ]
})
export class DashboardDefaultComponent implements OnInit {


  constructor(private authService: AuthService, private accountService: AccountService, private router: Router, private dashboardService: DashboardService, private alertService: AlertService) { }
  leaveTaskList: DashboardModel[];
  timesheetTaskList: DashboardModel[];
  performanceTaskList: PerformanceTask[];
  loadingIndicator: boolean = true;
  public isLeave: boolean = false;
  public isPerformance: boolean = false;
  public isRecruitment: boolean = false;
  public isTimesheet: boolean = false;


  ngOnInit() {
    if (this.authService.module.isLeave) {
      this.isLeave = true;
      this.getAllLeaveTask();
    }
    if (this.authService.module.isTimesheet) {
      this.isTimesheet = true;
      this.getAllTimesheetTask();
    }
    if (this.authService.module.isPerformance) {
      this.isPerformance = true;
      this.getAllPerformanceTask();
    }

    if (this.authService.module.isRecruitment) {
      this.isRecruitment = true;

    }

  }

  getAllPerformanceTask() {
    this.dashboardService.getPerformanceTaskList(this.accountService.currentUser.id).subscribe(result => this.onSuccessfulPerformanceDataLoad(result), error => this.onDataLoadFailed(error, "per"));
  }
  getAllLeaveTask() {
    this.dashboardService.getLeaveTaskList(this.accountService.currentUser.id).subscribe(result => this.onSuccessfulLeaveDataLoad(result), error => this.onDataLoadFailed(error, "lea"));
  }

  onSuccessfulLeaveDataLoad(dashboard: DashboardModel[]) {
    this.leaveTaskList = dashboard;
    this.loadingIndicator = false;
  }

  onDataLoadFailed(error: any, frm: string) {
    this.alertService.showInfoMessage("Unable to retrieve list from the server" + frm);
    this.loadingIndicator = false;
  }


  getAllTimesheetTask() {
    this.dashboardService.getTimesheetTaskList(this.accountService.currentUser.id).subscribe(result => this.onSuccessfulTimesheetDataLoad(result), error => this.onDataLoadFailed(error, "tim"));
  }
  onSuccessfulTimesheetDataLoad(dashboard: DashboardModel[]) {
    this.timesheetTaskList = dashboard;
    this.loadingIndicator = false;
  }

  onSuccessfulPerformanceDataLoad(performanceTask: PerformanceTask[]) {
    if (performanceTask)
      this.performanceTaskList = performanceTask;
    this.loadingIndicator = false;
  }
}
