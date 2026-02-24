import { Component, OnInit, ViewChild, TemplateRef } from '@angular/core';
import { AlertService } from '../../../../services/common/alert.service';
import { Candidate } from '../../../../models/candidate/candidate-list.model';
import { ShortListedCandidateModel } from '../../../../models/recruitment/manage-interview/shortlisted-candidate.model';
import { ManageInterviewService } from '../../../../services/recruitment/manage-interview.service';
import { ActivatedRoute, Router } from '@angular/router';
import { AccountService } from '../../../../services/account/account.service';

@Component({
  selector: 'shortlisted-candidate-list',
  templateUrl: './shortlisted-candidate-list.component.html',
  styleUrls: ['./shortlisted-candidate-list.component.css']
})
export class ShortlistedCandidateListComponent implements OnInit {
  columns: any[] = [];
  filterQuery: string = "";
  pageNumber = 0;
  count = 0;
  pageSize = 10;

  rows: Candidate[] = [];
  rowsCache: Candidate[] = [];
  loadingIndicator: boolean = true;
  @ViewChild('actionsTemplate')
  actionsTemplate: TemplateRef<any>;

  @ViewChild('interviewDateTemplate')
  interviewDateTemplate: TemplateRef<any>;


  @ViewChild('resumeTemplate')
  resumeTemplate: TemplateRef<any>;

  constructor(private accountService: AccountService, private router: Router, private alertService: AlertService, private manageInterviewService: ManageInterviewService) { }

  ngOnInit() {
    this.columns = [
      { prop: 'name', name: 'Name' },
      { prop: 'phoneNo', name: 'Moblie No' },
      { prop: 'vacancyName', name: 'Applied For' },
      { prop: 'interviewDate', name: 'Date', cellTemplate: this.interviewDateTemplate },
      { prop: 'interviewTime', name: 'Time' },
      { prop: 'screeningScore', name: 'Screening Score' },
      { prop: 'Resume', name: 'Resume', cellTemplate: this.resumeTemplate, resizeable: true, canAutoResize: false, sortable: false, draggable: false },
      { prop: 'Action', name: 'Action', cellTemplate: this.actionsTemplate, resizeable: false, canAutoResize: false, sortable: false, draggable: false }
    ];

    this.getCandidateList(this.pageNumber, this.pageSize, this.filterQuery);
  }
  getCandidateList(page?: number, pageSize?: number, name?: string) {
    this.loadingIndicator = true;
    this.manageInterviewService.getShortListCandidate(this.accountService.currentUser.id, page, pageSize, name).subscribe(result => this.onSuccessfulDataLoad(result), error => this.onDataLoadFailed());
  }
  getData(e) {
    this.getCandidateList(this.pageNumber, e, this.filterQuery);
  }
  setPage(e) {
    this.getCandidateList(e.offset, this.pageSize, this.filterQuery);
  }
  getFilteredData(filterString) {
    this.getCandidateList(this.pageNumber, this.pageSize, filterString);

  }
  onSuccessfulDataLoad(candidate: ShortListedCandidateModel) {
    this.rows = candidate.candidateModel;
    candidate.candidateModel.forEach((candidate, index) => {
      (<any>candidate).index = index + 1;
    });
    this.count = candidate.totalCount;
    this.loadingIndicator = false;
  }

  onDataLoadFailed() {
    this.alertService.showInfoMessage("Please try again!");
  }
  getCandidateDetail(interviewid) {
    this.router.navigate(['../recruitment/shortlisted-candidate-detail'], { queryParams: { id: interviewid } });
  }

}
