import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, FormArray } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { ManageInterviewService } from '../../../../services/recruitment/manage-interview.service';
import { AlertService } from '../../../../services/common/alert.service';
import { CandidateJobDetails, ManagerKpi } from '../../../../models/recruitment/manage-interview/shortlisted-candidate-job-detail.model';
import { Utilities } from '../../../../services/common/utilities';

@Component({
  selector: 'shortlisted-candidate-detail',
  templateUrl: './shortlisted-candidate-detail.component.html',
  styleUrls: ['./shortlisted-candidate-detail.component.css']
})
export class ShortlistedCandidateDetailComponent implements OnInit {

  public candidateSkillForm: FormGroup;
  public isSaving = false;
  public IsQuestionList = false;
  public jobDetail: CandidateJobDetails = new CandidateJobDetails();
  public id: string;
  public serverCallback: () => void;
  applicationId: any;

  constructor(private router: Router, private fb: FormBuilder, private route: ActivatedRoute, private manageInterviewService: ManageInterviewService, private alertService: AlertService) {
    this.route.queryParams.subscribe(params => {
      if (params['id'] != undefined) {
        this.id = params['id'];
        this.getCandidateJobDetail(params['id']);
      }
      else {
        this.router.navigate(['/shortlisted-candidate-list']);
      }
    });
  }
  ngOnInit() {
    this.candidateSkillForm = this.fb.group({
      candidateName: [''],
      email: [''],
      mobile: [''],
      appliedFor: [''],
      score: [''],
      comment: [''],
      questionAnswerList: this.fb.array([
        this.initQuestionAnswer()
      ])
    });
  }
  initQuestionAnswer() {
    return this.fb.group({
      jobQuestionId: [''],
      question: [''],
      levelSkillQuestionId: [''],
      obtainedWeightage: [''],
      weightage: ['']
    })
  }

  getCandidateJobDetail(id) {
    this.manageInterviewService.getCandidateJobDetail(id).subscribe(result => this.onSuccessfulDataLoad(result), error => this.onDataLoadFailed());
  }
  onSuccessfulDataLoad(jobinfo: CandidateJobDetails) {
    this.jobDetail = jobinfo
    this.applicationId = this.jobDetail.jobApplicationId;
    this.candidateSkillForm.patchValue({
      candidateName: this.jobDetail.candidateName, email: this.jobDetail.email,
      mobile: this.jobDetail.mobile, appliedFor: this.jobDetail.appliedFor, score: this.jobDetail.score, comment: this.jobDetail.comment
    });
    if (jobinfo.questionAnswerList.length > 0) {
      this.IsQuestionList = true;
    }
    if (jobinfo.questionAnswerList != null && jobinfo.questionAnswerList.length > 0) {
      this.candidateSkillForm.setControl('questionAnswerList', this.setExistingQuestionList(jobinfo.questionAnswerList));
    }
  }
  setExistingQuestionList(question: ManagerKpi[]): FormArray {
    const formArray = new FormArray([]);
    question.forEach(x => {
      formArray.push(this.fb.group({
        jobQuestionId: x.jobQuestionId,
        question: x.question,
        levelSkillQuestionId: x.levelSkillQuestionId,
        obtainedWeightage: x.obtainedWeightage,
        weightage: x.weightage
      }));
    });
    return formArray;
  }
  onDataLoadFailed() {
    this.alertService.showInfoMessage("Unable to retrieve list from the server");
  }
  save() {
    this.isSaving = true;
    this.jobDetail = this.candidateSkillForm.value;
    this.jobDetail.id = this.id;
    this.manageInterviewService.saveManagerKpi(this.jobDetail).subscribe(sucess => this.saveSuccessHelper(), error => this.saveFailedHelper(error));
  }

  private saveSuccessHelper() {
    this.isSaving = false;
    this.alertService.showSucessMessage("Saved successfully");
    this.router.navigate(['../recruitment/applied-job-detail'], { queryParams: { id: this.applicationId } });

  }
  private saveFailedHelper(error: any) {
    this.isSaving = false;
    let test = Utilities.getHttpResponseMessage(error);
    this.alertService.showInfoMessage("Please try later" + test[0]);
  }
}
