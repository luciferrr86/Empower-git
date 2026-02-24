import { Component, OnInit, ViewChild, Input } from '@angular/core';
import { FormGroup, FormBuilder, FormArray } from '@angular/forms';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { QuestionAnswer, QuestionAnswerModel } from '../../../../models/recruitment/candidate-view/candidate-application.model';
import { JobInterviewService } from '../../../../services/recruitment/job-interview.service';
import { AlertService } from '../../../../services/common/alert.service';
import { Utilities } from '../../../../services/common/utilities';

@Component({
  selector: 'hr-assessment',
  templateUrl: './hr-assessment.component.html',
  styleUrls: ['./hr-assessment.component.css']
})
export class HrAssessmentComponent implements OnInit {

  public candidateHRForm: FormGroup;
  public isSaving = false;
  public IsHRQuestionList = false;

  public hrKpiEdit: QuestionAnswer[];
  public hrQuestion: QuestionAnswerModel = new QuestionAnswerModel();
  public serverCallback: () => void;
  @Input() appId: string;

  @ViewChild('editorModal')
  editorModal: ModalDirective;

  constructor(private jobInterviewService: JobInterviewService, private fb: FormBuilder, private alertService: AlertService) { }

  ngOnInit() {
    this.candidateHRForm = this.fb.group({
      questionAnswerList: this.fb.array([
        this.initQuestionAnswer()
      ])
    });
  }
  initQuestionAnswer() {
    return this.fb.group({
      id: "",
      jobApplicationId: [''],
      question: [''],
      jobHRQuestionId: [''],
      obtainedWeightage: [''],
      weightage: ['']
    })
  }
  hrKpis(hrKpiList: QuestionAnswer[]) {
    if (hrKpiList != null && hrKpiList.length > 0) {
      this.candidateHRForm.setControl('questionAnswerList', this.setExistingQuestionList(hrKpiList));
    }
    if (hrKpiList.length > 0) {
      this.IsHRQuestionList = true;
    }
    this.editorModal.show();
    return this.hrKpiEdit;
  }
  setExistingQuestionList(question: QuestionAnswer[]): FormArray {
    const formArray = new FormArray([]);
    question.forEach(x => {
      formArray.push(this.fb.group({
        id: "",
        jobApplicationId: x.jobApplicationId,
        jobHRQuestionId: x.jobHRQuestionId,
        question: x.question,
        obtainedWeightage: x.obtainedWeightage,
        weightage: x.weightage
      }));
    });
    return formArray;
  }


  save() {
    this.isSaving = true;
    this.jobInterviewService.savehrKpi(this.candidateHRForm.get('questionAnswerList').value).subscribe(sucess => this.saveSuccessHelper(), error => this.saveFailedHelper(error));
  }
  private saveSuccessHelper() {
    this.isSaving = false;
    this.alertService.showSucessMessage("Saved successfully");
    this.editorModal.hide();
    this.serverCallback();
  }

  private saveFailedHelper(error: any) {
    this.isSaving = false;
    let test = Utilities.getHttpResponseMessage(error);
    this.alertService.showInfoMessage("Please try later" + test[0]);
  }
}
