import { Candidate, CandidateModel, InterviewLevelModel } from './../../../../models/recruitment/candidate-view/candidate-list.model';
import { Component, OnInit, ViewChild, TemplateRef } from '@angular/core';
import { AlertService } from '../../../../services/common/alert.service';
import { CandidateService } from '../../../../services/candidate/candidate.service';
import { Router, ActivatedRoute } from '@angular/router';
import { animate, style, transition, trigger } from '@angular/animations';
import { NgbTabset } from '@ng-bootstrap/ng-bootstrap';
@Component({
  selector: 'app-candidate-list',
  templateUrl: './candidate-list.component.html',
  styleUrls: ['./candidate-list.component.css'],
  animations: [
    trigger('fadeInOutTranslate', [
      transition(':enter', [
        style({ opacity: 0 }),
        animate('400ms ease-in-out', style({ opacity: 1 }))
      ]),
      transition(':leave', [
        style({ transform: 'translate(0)' }),
        animate('400ms ease-in-out', style({ opacity: 0 }))
      ])
    ])
  ]
})
export class CandidateListComponent implements OnInit {
  columns: any[] = [];
  filterQuery: string = "";
  pageNumber = 0;
  count = 0;
  t: NgbTabset;
  public jobId: string;
  public position = "";
  public tabActive = "";
  public tabAction = false;
  public levelId = "all";
  public interviewLevelModel = new Array<InterviewLevelModel>();
  pageSize = 10;

  rows: Candidate[] = [];
  loadingIndicator: boolean = true;

  @ViewChild('screeningScoreTemplate')
  screeningScoreTemplate: TemplateRef<any>;

  //@ViewChild('hrScoreTemplate')
  //hrScoreTemplate: TemplateRef<any>;

  @ViewChild('skillScoreTemplate')
  skillScoreTemplate: TemplateRef<any>;

  @ViewChild('overAllScoreTemplate')
  overAllScoreTemplate: TemplateRef<any>;

  @ViewChild('actionsTemplate')
  actionsTemplate: TemplateRef<any>;

  //@ViewChild('resumeTemplate')
  //resumeTemplate: TemplateRef<any>;
  allCandidates: CandidateModel = new CandidateModel();
  filterCandidateData: CandidateModel = new CandidateModel();

  @ViewChild('jobStatusTemplate')
  jobStatusTemplate: TemplateRef<any>;

  constructor(private router: Router, private route: ActivatedRoute, private alertService: AlertService, private candidateService: CandidateService) {
    this.route.queryParams.subscribe(params => {
      if (params['id'] != undefined) {
        this.jobId = params['id'];
        this.getAllVacancy(params['id'], this.pageNumber, this.pageSize, this.filterQuery);
      }
      else {
        this.router.navigate(['/dashboard']);
      }
    });
  }

  ngOnInit() {
    this.columns = [
      { prop: 'name', name: 'Name' },
      { prop: 'email', name: 'Email' },
      { prop: 'phoneNo', name: 'Mobile No.' },
      { prop: 'screeningScore', name: 'Screening<br/>Score', cellTemplate: this.screeningScoreTemplate, resizeable: true, canAutoResize: true, draggable: false },
      //{ prop: 'hrScore', name: 'HR Score', cellTemplate: this.hrScoreTemplate, resizeable: true, canAutoResize: false, sortable: true, draggable: false },
      { prop: 'skillScore', name: 'Skill Score', cellTemplate: this.skillScoreTemplate, resizeable: true, canAutoResize: true, draggable: false },
      { prop: 'overAllScore', name: 'Final Score', cellTemplate: this.overAllScoreTemplate, resizeable: true, canAutoResize: true, draggable: false},
      //{ prop: 'Resume', name: 'Resume', cellTemplate: this.resumeTemplate, resizeable: true, canAutoResize: false, sortable: false, draggable: false },
      { prop: 'Status', name: 'Status', cellTemplate: this.jobStatusTemplate, resizeable: true, canAutoResize: false, sortable: false, draggable: false },
      { prop: 'Action', name: 'Action', cellTemplate: this.actionsTemplate, resizeable: false, canAutoResize: false, sortable: false, draggable: false }
    ];


  }
  getAllVacancy(id?: string, page?: number, pageSize?: number, name?: string) {
    this.loadingIndicator = true;
    this.candidateService.getAllCandidate(this.levelId, id, page, pageSize, name).subscribe(result => {
      this.allCandidates = result
      this.onSuccessfulDataLoad(result)
    }, error => this.onDataLoadFailed());
  }
  getData(e) {
    this.getAllVacancy(this.jobId, this.pageNumber, e, this.filterQuery);
  }
  setPage(e) {
    this.getAllVacancy(this.jobId, e.offset, this.pageSize, this.filterQuery);
  }
  getFilteredData(filterString) {
    this.getAllVacancy(this.jobId, this.pageNumber, this.pageSize, filterString);

  }
  onSuccessfulDataLoad(candidates: CandidateModel) {
    this.position = candidates.position;
    if (!this.tabAction) {
      var level = new InterviewLevelModel();
      level.id = "all";
      level.name = "All Application";
      this.interviewLevelModel.push(level);
      candidates.interviewLevelModel.forEach((level) => {
        this.interviewLevelModel.push(level);
      });
    }
    this.rows = candidates.candidateModel;
    candidates.candidateModel.forEach((candidate, index) => {
      (<any>candidate).index = index + 1;
    });
    this.count = candidates.totalCount;
    this.loadingIndicator = false;

  }

  onDataLoadFailed() {
    this.alertService.showInfoMessage("Please Try Again!");
    this.loadingIndicator = false;
  }

  deleteApplication(appId: string) {
    this.alertService.showConfirm("Are you sure you want to delete?", () => this.deleteVacancyHelper());
  }
  deleteVacancyHelper() {

  }
  viewCandidate(candidateId) {
    this.router.navigate(['../recruitment/candidate-profile'], { queryParams: { id: candidateId } });

  }
  applicationDetail(appId) {
    this.router.navigate(['../recruitment/applied-job-detail'], { queryParams: { id: appId } });
  }
  getApplicationData(levelId) {
    this.tabAction = true;
    this.levelId = levelId;
    if (levelId == 'all') {
      this.getAllVacancy(this.jobId, this.pageNumber, this.pageSize, this.filterQuery);
    }
    else {
      this.filterCandidateData =JSON.parse(JSON.stringify(this.allCandidates)); //deep copy
      this.filterCandidateData.candidateModel = this.filterCandidateData.candidateModel.filter(f => f.levelId.indexOf(levelId) !== -1);
      this.onSuccessfulDataLoad(this.filterCandidateData);

    }
    
  }
}
