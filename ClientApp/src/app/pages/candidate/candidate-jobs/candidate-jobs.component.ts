import { Component, OnInit, ViewChild, TemplateRef } from '@angular/core';
import { CandidateJobService } from '../../../services/candidate/candidate-job.service';
import { AlertService } from '../../../services/common/alert.service';
import { CandidateJobList, CandidateJobListModel } from '../../../models/candidate/candidate-job-list.model';
import { AccountService } from '../../../services/account/account.service';
import { Router } from '@angular/router';

@Component({
  selector: 'candidate-jobs',
  templateUrl: './candidate-jobs.component.html',
  styleUrls: ['./candidate-jobs.component.css']
})
export class CandidateJobsComponent implements OnInit {

  columns: any[] = [];
  filterQuery: string = "";
  pageNumber = 0;
  count = 0;
  @ViewChild('applyDateTemplate')
  applyDateTemplate: TemplateRef<any>;
  pageSize = 10;
  loadingIndicator: boolean = false;
  rows: CandidateJobList[] = [];
  editedJobList: CandidateJobList;

  @ViewChild('indexTemplate')
  indexTemplate: TemplateRef<any>;

  constructor(private router: Router, private candidateJobService: CandidateJobService, private alertService: AlertService, private accountService: AccountService) { }

  ngOnInit() {
    this.columns = [
      { prop: "index", name: '#', cellTemplate: this.indexTemplate, canAutoResize: false },
      { prop: 'vacancyName', name: 'Position' },
      { prop: 'jobType', name: 'Job Type' },
      { prop: 'appliedDate', name: 'Applied Date', cellTemplate: this.applyDateTemplate },
      { prop: 'jobStatus', name: 'Job Status' },
    ];
    this.getJobList(this.pageNumber, this.pageSize, this.filterQuery);
  }
  getJobList(page?: number, pageSize?: number, name?: string) {
    this.loadingIndicator = true;
    this.candidateJobService.getJobList(this.accountService.currentUser.id, page, pageSize, name).subscribe(result => this.onSuccessfulDataLoad(result), error => this.onDataLoadFailed(error));
  }

  onSuccessfulDataLoad(job: CandidateJobListModel) {
    this.rows = job.candidateAppliedJobModel;
    job.candidateAppliedJobModel.forEach((jobs, index, job) => {
      (<any>jobs).index = index + 1;
    });
    this.count = job.totalCount;
    this.loadingIndicator = false;
  }

  onDataLoadFailed(error: any) {
    this.router.navigate(['../candidate/profile']);
  }

  getFilteredData(filterString) {
    this.getJobList(this.pageNumber, this.pageSize, this.filterQuery);

  }
  getData(e) {
    this.getJobList(this.pageNumber, e, this.filterQuery);
  }
  setPage(e) {
    this.getJobList(e.offset, this.pageSize, this.filterQuery);
  }
}
