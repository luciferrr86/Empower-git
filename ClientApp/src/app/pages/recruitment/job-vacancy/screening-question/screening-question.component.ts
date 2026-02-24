import { Component, OnInit } from '@angular/core';
import { FormGroup, FormArray, FormBuilder, Validators } from '@angular/forms';
import { JobScreeningQuestions } from '../../../../models/recruitment/job-vacancy/screening-question.model';
import { VacancyService } from '../../../../services/recruitment/vacancy.service';
import { ActivatedRoute } from '@angular/router';
import { AlertService } from '../../../../services/common/alert.service';

@Component({
    selector: 'screening-question',
    templateUrl: './screening-question.component.html',
    styleUrls: ['./screening-question.component.css']
})
export class ScreeningQuestionComponent implements OnInit {

    public screeningForm: FormGroup;
    public screeningArray: JobScreeningQuestions[] = new Array<JobScreeningQuestions>();
    private isNew = false;
    public isSaving = false;
    public vacancyId = "";
    public i: number;

    constructor(private _fb: FormBuilder, private vacancyService: VacancyService, private route: ActivatedRoute, private alertService: AlertService) { }

    ngOnInit() {
        this.screeningForm = this._fb.group({
            jobScreeningQuestion: this._fb.array([
                this.initQuestion(),
            ])
        });
        this.vacancyService.currentVacancyId.subscribe(id => this.vacancyId = id)
        this.getJobScreenigQuestionDetail();
        this.screeningForm.valueChanges.distinctUntilChanged().subscribe(

            () => {

            });
    }
    callScreeningLoad() {
        this.screeningForm = this._fb.group({
            jobScreeningQuestion: this._fb.array([
                this.initQuestion(),
            ])
        });
    }
    initQuestion() {

        return this._fb.group({
            id: [],
            question: ['', Validators.required],
            controlType: ['', Validators.required],
            weightage: ['', Validators.required],
            mandatory: [false],
            option1: ['', Validators.required],
            optChk1: [false],
            optChk2: [false],
            optChk3: [false],
            optChk4: [false],
            option2: ['', Validators.required],
            option3: [''],
            option4: [''],

        });
    }

    addQuestion() {
        const control = <FormArray>this.screeningForm.controls['jobScreeningQuestion'];
        control.push(this.initQuestion());
    }

    removeQuestion($event: any, i: number) {
        if ($event != "" && $event != null) {
            this.vacancyService.deleteScreening($event).subscribe(() => this.deleteSuccessHelper(i), error => this.failedHelper("Couldn't delete successfully."));
        }
        else {
            const control = <FormArray>this.screeningForm.controls['jobScreeningQuestion'];
            control.removeAt(i);
        }

    }
    private deleteSuccessHelper(i: number) {
        const control = <FormArray>this.screeningForm.controls['jobScreeningQuestion'];
        control.removeAt(i);
        this.alertService.showInfoMessage("Deleted Successfully");
    }
    get jobScreeningQuestion() {
        return this.screeningForm.get('jobScreeningQuestion') as FormArray;
    }

    getJobScreenigQuestionDetail() {
        this.route.queryParams.subscribe(params => {
            if (params['id']) {
                this.vacancyService.getJobScreening(params['id']).subscribe(result => this.onSuccessfulDataLoad(result), () => this.onDataLoadFailed());
            }
            else {
                this.isNew = true;

                this.vacancyService.getJobScreening(this.vacancyId).subscribe(result => this.onSuccessfulDataLoad(result), () => this.onDataLoadFailed());
            }
        });
    }

    onSuccessfulDataLoad(screeningQuestions: JobScreeningQuestions[]) {
        this.screeningArray = screeningQuestions;
        if (screeningQuestions.length > 0) {
            this.setScreeningQuestions(this.screeningArray);
        }


    }
    setScreeningQuestions(questions: any[]) {
        let control = <FormArray>this.screeningForm.controls.jobScreeningQuestion;
        if (control.length > 0) {
            for (var i = 0; i <= control.length; i++) {
                control.controls.splice(0);
            }
        }
        questions.forEach(x => {
            control.push(this._fb.group({
                id: x.id,
                question: x.question,
                controlType: x.controlType,
                weightage: x.weightage,
                mandatory: x.mandatory,
                option1: x.option1,
                optChk1: x.optChk1,
                optChk2: x.optChk2,
                optChk3: x.optChk3,
                optChk4: x.optChk4,
                option2: x.option2,
                option3: x.option3,
                option4: x.option4
            }))
        });
    }
    onDataLoadFailed() {
        this.alertService.showInfoMessage("Please Try Again");
    }


    save() {
        this.isSaving = true;
        this.screeningArray = this.screeningForm.controls['jobScreeningQuestion'].value;
        this.route.queryParams.subscribe(params => {
            if (params['id']) {
                this.vacancyService.createScreening(this.screeningArray, params['id']).subscribe(() => this.Savesuccess(), error => this.failedHelper("Please try later"));
            }
            else {
                this.vacancyService.createScreening(this.screeningArray, this.vacancyId).subscribe(() => this.Savesuccess(), error => this.failedHelper("Please try later"));
            }
        });
    }

    private Savesuccess() {
        this.isSaving = false;
        //this.jobVacancyId = result;
        if (this.isNew) {
            this.alertService.showSucessMessage("Saved successfully");
            //this.router.navigate(['../recruitment/job-create'], { queryParams: { id: this.jobVacancyId } });
        } else {
            this.alertService.showSucessMessage("Updated successfully");
        }
        this.getJobScreenigQuestionDetail();

    }
    private failedHelper(errMsg: string) {
        this.isSaving = false;
        this.alertService.showInfoMessage(errMsg);
    }


}

