import { Component, OnInit, Input } from '@angular/core';
import { ApplicationForm, JobScreeningQuestion } from '../../../models/candidate/candidate-application.model';
import { ActivatedRoute, Router } from '@angular/router';
import { CandidateJobService } from '../../../services/candidate/candidate-job.service';
import { AlertService } from '../../../services/common/alert.service';
import { FormGroup } from '@angular/forms';
import { QuestionBase, CheckBoxQuestion, RadioButtonQuestion } from '../../../models/dynamic-form/question-base.model';
import { QuestionModel } from '../../../models/dynamic-form/question.model';


@Component({
  selector: 'app-job-application',
  templateUrl: './job-application.component.html',
  styleUrls: ['./job-application.component.css']
})
export class JobApplicationComponent implements OnInit {

  public applicationForm: ApplicationForm = new ApplicationForm();
  public questionList: JobScreeningQuestion[];
  public jobId: string;
  @Input() questions: QuestionBase<any>[] = [];
  public questionModel: QuestionModel= new QuestionModel();
  form: FormGroup;
  payLoad = '';

  constructor(private route: ActivatedRoute, private router: Router, private jobDetailService: CandidateJobService, private alertService: AlertService) {
    this.route.queryParams.subscribe(params => {
      if (params['id'] != undefined) {
        this.jobId = params['id'];
        this.jobDetailService.getApplication(this.jobId).subscribe(result => this.onSuccessfulDataLoad(result), error => this.onDataLoadFailed());
      }
      else {
        this.router.navigate(['candidate/dashboard']);
      }
    });
  }

  ngOnInit() {
    
  }
  onSuccessfulDataLoad(jobDetails: ApplicationForm) {
    this.applicationForm = jobDetails;
    this.questionList = jobDetails.questionListModel;
    if (jobDetails.questionListModel.length > 0) {
      jobDetails.questionListModel.forEach((question, index) => {
        index++;
        if (question.controlType == "2") {
          let radioQuestion = new RadioButtonQuestion();
          radioQuestion.key = question.id;
          radioQuestion.text = question.question;
          radioQuestion.required = true;
          radioQuestion.options.push({ key: question.option1, value: question.option1 });
          radioQuestion.options.push({ key: question.option2, value: question.option2 });
          radioQuestion.options.push({ key: question.option3, value: question.option3 });
          radioQuestion.options.push({ key: question.option4, value: question.option4 });
          radioQuestion.order = index;
          this.questionModel.questions.push(radioQuestion);
        } else if (question.controlType == "1") {

          let chkQuestion = new CheckBoxQuestion();
          chkQuestion.key = question.id;
          chkQuestion.text = question.question;
          chkQuestion.value = question.option1;
          chkQuestion.required = true;
          chkQuestion.options.push({ key: question.option1, value: question.option1 });
          chkQuestion.options.push({ key: question.option2, value: question.option2 });
          chkQuestion.options.push({ key: question.option3, value: question.option3 });
          chkQuestion.options.push({ key: question.option4, value: question.option4 });
          chkQuestion.order = index;
          this.questionModel.questions.push(chkQuestion);
        }
      });
      this.questionModel.questions.sort((a, b) => a.order - b.order);
      this.questionModel.isDataAvailable = true;
    }

    
  }

  onDataLoadFailed() {
    this.alertService.showInfoMessage("Unable to retrieve list from the server");
  }
}
