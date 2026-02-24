import { Component, OnInit, ViewChild } from '@angular/core';
import { animate, style, transition, trigger } from '@angular/animations';
import { HrAssessment } from '../../../../models/recruitment/candidate-view/hr-assessment.model';
import { HrAssessmentComponent } from '../hr-assessment/hr-assessment.component';
import { InterViewSchedule } from '../../../../models/recruitment/candidate-view/interview-schedule.model';
import { JobInterviewService } from '../../../../services/recruitment/job-interview.service';
import { AlertService } from '../../../../services/common/alert.service';
import { CandidateApplicationViewModel, QuestionAnswerModel, QuestionAnswer, InterviewScheduleModel } from '../../../../models/recruitment/candidate-view/candidate-application.model';
import { Router, ActivatedRoute } from '@angular/router';
import { DropDownList } from '../../../../models/common/dropdown';
import { Utilities } from '../../../../services/common/utilities';

@Component({
  selector: 'app-job-applied-detail',
  templateUrl: './job-applied-detail.component.html',
  styleUrls: ['./job-applied-detail.component.css'],
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
export class JobAppliedDetailComponent implements OnInit {

  public hrKpiModel: QuestionAnswer[];
  public screeningModel: QuestionAnswer[];
  public skillModel: QuestionAnswer[];
  public interviewScheduleList: InterviewScheduleModel[];
  public interviewLevel1: InterviewScheduleModel[];
  public interviewLevel2: InterviewScheduleModel[];
  public interviewLevel3: InterviewScheduleModel[];
  public interviewSchedule: InterViewSchedule = new InterViewSchedule();
  public jobInfoModel: CandidateApplicationViewModel = new CandidateApplicationViewModel();
  public questionModel: QuestionAnswerModel = new QuestionAnswerModel();

  managerList: DropDownList[] = [];
  interviewTypeList: DropDownList[] = [];
  interviewModeList: DropDownList[] = [];
  public jobId: string;
  public isStatusButton = true;
  public isLoaded = false;
  applicantName: any;
  applicantMobileNumber: any;
  resumeUrl: any;

  @ViewChild('hrKpiEditor')
  hrKpiEditor: HrAssessmentComponent;

  @ViewChild('interviewListEditor')
  interviewListEditor: HrAssessmentComponent;
   

  constructor(private router: Router, private route: ActivatedRoute, private jobInterviewService: JobInterviewService, private alertService: AlertService) {
    this.route.queryParams.subscribe(params => {
      if (params['id'] != undefined) {
        this.jobId = params['id'];
        this.getJobInfo(params['id']);
      }
      else {
        this.router.navigate(['/candidate-list']);
      }
    });

  }

  ngOnInit() {
  }
  ngAfterViewInit() {
    this.hrKpiEditor.serverCallback = () => {
      this.getJobInfo(this.jobId);
    };
    this.interviewListEditor.serverCallback = () => {
      this.getJobInfo(this.jobId);
    };
  }
  private getJobInfo(id) {
    this.jobInterviewService.getJobInfo(id).subscribe(result => this.onSuccessfulDataLoad(result), error => this.onDataLoadFailed(error));
  }
  onSuccessfulDataLoad(jobinfo: CandidateApplicationViewModel) {
      
    this.isLoaded = true;
    this.applicantName = jobinfo['candidateInterView'].candidateName;
    this.applicantMobileNumber = jobinfo['candidateInterView'].mobile;
    this.resumeUrl = jobinfo['candidateInterView'].resumeUrl;
    this.managerList = jobinfo.managerList;
    this.interviewTypeList = jobinfo.interviewTypeList;
    this.interviewModeList = jobinfo.interviewMode;
    this.jobInfoModel.jobInformationModel = jobinfo.jobInformationModel;
    this.interviewScheduleList = jobinfo.listInterviewScheduleModel;
    this.hrKpiModel = jobinfo.questionAnswerModel.jobHRKpiList;
    this.screeningModel = jobinfo.questionAnswerModel.screeningQuestionList;
    this.skillModel = jobinfo.questionAnswerModel.skillKpiList;
    if (this.jobInfoModel.jobInformationModel.jobStatus == "Hired" || this.jobInfoModel.jobInformationModel.jobStatus == "Rejected") {
      this.isStatusButton = false;
    }
  }
  onDataLoadFailed(error: any) {
    this.alertService.showInfoMessage("Unable to retrieve list from the server");
  }
  hrKpi() {
    if (this.jobInfoModel.jobInformationModel.jobStatus == "Applied") {
      this.hrKpiModel = this.hrKpiEditor.hrKpis(this.hrKpiModel);
    }
    else {
      this.alertService.showInfoMessage("Candidate is already short listed.");
    }

  }
}
