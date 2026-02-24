import { Component, OnInit } from '@angular/core';
import { Router, NavigationExtras } from "@angular/router";
import { ActivatedRoute } from "@angular/router";
import { JobDetailModel } from '../../../models/candidate/candidate-job-detail.model';
import { AlertService } from '../../../services/common/alert.service';
import { CandidateJobService } from '../../../services/candidate/candidate-job.service';


@Component({
  selector: 'app-job-detail',
  templateUrl: './job-detail.component.html',
  styleUrls: ['./job-detail.component.css']
})
export class JobDetailComponent implements OnInit {

  public jobDetail: JobDetailModel = new JobDetailModel();
  public jobId:string;
  constructor(private route: ActivatedRoute, private router: Router, private jobDetailService: CandidateJobService, private alertService: AlertService) {
    this.route.queryParams.subscribe(params => {

      if (params['id'] != undefined) {
        this.jobId=params['id'];
        this.jobDetailService.getJobDetail(params['id']).subscribe(result => this.onSuccessfulDataLoad(result), error => this.onDataLoadFailed(error));
      }
      else {
        window.location.href = 'http://demo.a4technology.com/careers.html'
      }
    });
  }

  ngOnInit() {

  }
  onSuccessfulDataLoad(jobDetails: JobDetailModel) {
    this.jobDetail = jobDetails;
  }

  onDataLoadFailed(error: any) {
    this.alertService.showInfoMessage("Unable to retrieve list from the server");
  }
  applyJob(){
    this.router.navigate(['../candidate/login'],{ queryParams: { id: this.jobId }});
  }
}
