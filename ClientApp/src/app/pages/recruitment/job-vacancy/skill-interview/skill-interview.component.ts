import { Component, OnInit } from '@angular/core';
import { FormGroup, FormArray, FormBuilder } from '@angular/forms';
import { SkillInterview, QuestionOption } from '../../../../models/recruitment/job-vacancy/skill-interview.model';
import { VacancyService } from '../../../../services/recruitment/vacancy.service';
import { AlertService } from '../../../../services/common/alert.service';
import { ActivatedRoute } from '@angular/router';
import { Utilities } from '../../../../services/common/utilities';
import { DropDownList } from '../../../../models/common/dropdown';



@Component({
    selector: 'skill-interview',
    templateUrl: './skill-interview.component.html',
    styleUrls: ['./skill-interview.component.css']
})
export class SkillInterviewComponent implements OnInit {

    public skillQuestionForm: FormGroup;
    public skillQuestionArray: QuestionOption[] = [];
    public isNew: boolean;
    public isSaving: boolean;
    public vacancyId: any;
    public levelList: DropDownList[];

    constructor(private _fb: FormBuilder, private vacancyService: VacancyService, private alertService: AlertService, private route: ActivatedRoute) { }

    ngOnInit() {
        this.skillQuestionForm = this._fb.group({
            skillQuestion: this._fb.array([
                this.initSkillQuestion(),
            ])
        });
        this.vacancyService.currentVacancyId.subscribe(id => this.vacancyId = id)
        this.getSkillQuestionDetail();
    }

    initSkillQuestion() {
        return this._fb.group({
            id: [],
            question: [''],
            weightage: [''],
            levelIdList: [[]]
        });
    }



    addSkillQuestion() {
        const control = <FormArray>this.skillQuestionForm.controls['skillQuestion'];
        control.push(this.initSkillQuestion());
    }

    getSkillQuestionDetail() {
        this.route.queryParams.subscribe(params => {
            if (params['id']) {
                this.vacancyService.getSkillQuestion(params['id']).subscribe(result => this.onSuccessfulDataLoad(result), error => this.onDataLoadFailed());
            }
            else {
                this.isNew = true;
                this.vacancyService.getSkillQuestion(this.vacancyId).subscribe(result => this.onSuccessfulDataLoad(result), error => this.onDataLoadFailed());
            }
        });
    }

    onSuccessfulDataLoad(skillInterview: SkillInterview) {
        this.levelList = skillInterview.levelList;
        this.skillQuestionArray = skillInterview.jobSkillQuestion;
        if (skillInterview.jobSkillQuestion.length > 0) {
            this.setSkillQuestions(this.skillQuestionArray);
        }
    }
    setSkillQuestions(questions: any[]) {
        let control = <FormArray>this.skillQuestionForm.controls.skillQuestion;
        if (control.length > 0) {
            for (var i = 0; i <= control.length; i++) {
                control.controls.splice(0);
            }
        }
        questions.forEach(x => {
            control.push(this._fb.group({
                id: x.id,
                question: x.question,
                weightage: x.weightage,
                levelIdList: [x.levelIdList]
            }))
        });

    }
    onDataLoadFailed() {
        this.alertService.showInfoMessage("Please Try Again");
    }
    removeSkillQuestion($event, i) {
        if ($event != "" && $event != null) {
            this.vacancyService.deleteSkillQuestion($event).subscribe(sucess => this.deleteSuccessHelper(i), error => this.failedHelper(error, "Couldn't delete successfully."));
        }
        else {
            const control = <FormArray>this.skillQuestionForm.controls['skillQuestion'];
            control.removeAt(i);
        }

    }

    private deleteSuccessHelper(i: number) {
        const control = <FormArray>this.skillQuestionForm.controls['skillQuestion'];
        control.removeAt(i);
        this.alertService.showInfoMessage("Deleted Successfully");
    }

    get skillQuestion() {
        return this.skillQuestionForm.get('skillQuestion') as FormArray;
    }

    save() {
        this.isSaving = true;
        this.skillQuestionArray = this.skillQuestionForm.controls['skillQuestion'].value;
        this.route.queryParams.subscribe(params => {
            if (params['id']) {
                this.vacancyService.createSkillQuestion(this.skillQuestionArray, params['id']).subscribe(sucess => this.Savesuccess(), error => this.failedHelper(error, "Please try later"));
            }
            else {
                this.vacancyService.createSkillQuestion(this.skillQuestionArray, this.vacancyId).subscribe(sucess => this.Savesuccess(), error => this.failedHelper(error, "Please try later"));
            }

        });
    }

    private Savesuccess() {
        this.isSaving = false;
        if (this.isNew) {
            this.alertService.showSucessMessage("Saved successfully");

        } else {
            this.alertService.showSucessMessage("Updated successfully");
        }
        this.getSkillQuestionDetail();

    }
    private failedHelper(error: any, errMsg: string) {
        this.isSaving = false;
        this.alertService.showInfoMessage(errMsg);
    }
}
