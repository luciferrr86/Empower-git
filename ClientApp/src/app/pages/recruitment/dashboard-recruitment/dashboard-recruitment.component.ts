import { Component, OnInit } from '@angular/core';
import { AlertService } from '../../../services/common/alert.service';
import { DashboardRecruitmentService } from '../../../services/recruitment/dashboard-recruitment.service';
import { recruitmentDashboard } from '../../../models/recruitment/dashboard/recruitmentDashboard.model';



@Component({
  selector: 'app-dashboard-recruitment',
  templateUrl: './dashboard-recruitment.component.html',
  styleUrls: ['./dashboard-recruitment.component.css']
})
export class DashboardRecruitmentComponent implements OnInit {
  public editedrecruitment: recruitmentDashboard[] = [];
  constructor(private dashboardService: DashboardRecruitmentService, private alertService: AlertService) { }

  ngOnInit() {
    this.getRecruitmentData();
  }


  getRecruitmentData() {
    this.dashboardService.getDashboardDetails().subscribe(result => this.onSuccessfulDataLoad(result), error => this.onDataLoadFailed());
  }

  onSuccessfulDataLoad(recruitments: recruitmentDashboard[]) {
    this.editedrecruitment = recruitments;
  }


  onDataLoadFailed() {
    this.alertService.showInfoMessage("Unable to retrieve list from the server");
  }

}
