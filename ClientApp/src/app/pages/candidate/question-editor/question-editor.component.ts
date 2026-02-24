import { ApplicationQuestion, ApplicationQuestionModel } from '../../../models/dynamic-form/question-base.model';
import { Component, Input, OnInit } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { CandidateJobService } from '../../../services/candidate/candidate-job.service';
import { AlertService } from '../../../services/common/alert.service';
import { Utilities } from '../../../services/common/utilities';
import { AuthService } from '../../../services/common/auth.service';
import { Router } from '@angular/router';
import { CandidateService } from '../../../services/candidate/candidate.service';

@Component({
    selector: 'question-editor',
    templateUrl: './question-editor.component.html',
    styleUrls: ['./question-editor.component.css'],

})

export class QuestionEditorComponent {
    applicationQuestion: ApplicationQuestionModel = new ApplicationQuestionModel();
  constructor(private authService: AuthService, private profileService: CandidateService, private jobDetailService: CandidateJobService, private alertService: AlertService, private router: Router) {

    }
    radio: string;
    @Input() model: any;

    @Input() jobVaccancyId: any;
    form: FormGroup;
    payLoad = null;
  image: any;
    ngOnInit() {
        this.form = this.model.toGroup();
    }
    onCheckboxChange(id, option, event) {
        if (event.target.checked) {
            let test = this.applicationQuestion.questions.find(m => m.id == id);
            if (test != undefined) {
                test.option.push(option.value);
            } else {
                let appQuest = new ApplicationQuestion();
                appQuest.id = id;
                appQuest.option.push(option.value);
                this.applicationQuestion.questions.push(appQuest);
            }
        } else {
            let test = this.applicationQuestion.questions.find(m => m.id == id);
            if (test != undefined) {
                const index: number = test.option.indexOf(option.value);
                if (index !== -1) {
                    test.option.splice(index, 1);
                }

            }
        }
    }
    onRadioChange(id, option, event) {
        let test = this.applicationQuestion.questions.find(m => m.id == id);
        if (test != undefined) {
            test.option = [];
            test.option.push(option.value);

        } else {
            let appQuest = new ApplicationQuestion();
            appQuest.id = id;
            appQuest.option.push(option.value);
            this.applicationQuestion.questions.push(appQuest);
        }
    }
  onSubmit() {
    this.profileService.getResume(this.authService.currentUser.id).subscribe((result) => {
      this.image = result.imageUrl;
      if (this.image == null) {
        this.alertService.showSucessMessage("Please upload resume.");
        this.router.navigate(['candidate/profile']);
      }
      else {
        this.applicationQuestion.candidateId = this.authService.currentUser.id;
        this.applicationQuestion.jobVacancyId = this.jobVaccancyId;
        this.jobDetailService.saveApplication(this.applicationQuestion).subscribe(sucess => this.saveSuccessHelper(sucess), error => this.saveFailedHelper(error));

      }
    });
    }
    
    private saveSuccessHelper(result?: string) {
        this.alertService.showSucessMessage("Your job application submitted sucessfully.");
      this.router.navigate(['candidate/dashboard']);
    }

  private saveFailedHelper(error: any) {
      let test = Utilities.getHttpResponseMessage(error);
    if (error['status'] == '400') {
      this.alertService.showInfoMessage(test[0].split(":")[1]);
    }
    else {
      this.alertService.showInfoMessage("Please try later" + test[0]);
    }
   }


}

