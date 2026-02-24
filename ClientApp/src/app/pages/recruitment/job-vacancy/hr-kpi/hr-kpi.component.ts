import { HrKpi } from './../../../../models/recruitment/job-vacancy/hr-kpi.model';
import { Component, OnInit } from '@angular/core';
import { FormGroup, FormArray, FormBuilder } from '@angular/forms';
import { VacancyService } from '../../../../services/recruitment/vacancy.service';
import { ActivatedRoute } from '@angular/router';
import { AlertService } from '../../../../services/common/alert.service';
import { Utilities } from '../../../../services/common/utilities';


@Component({
    selector: 'hr-kpi',
    templateUrl: './hr-kpi.component.html',
    styleUrls: ['./hr-kpi.component.css']
})
export class HrKpiComponent implements OnInit {

    public hrKpiForm: FormGroup;
    public hrKpiArray: HrKpi[] = [];
    public isNew: boolean;
    public isSaving: boolean;
    public vacancyId: string;
    constructor(private _fb: FormBuilder, private vacancyService: VacancyService, private route: ActivatedRoute, private alertService: AlertService) { }

    ngOnInit() {
        this.hrKpiForm = this._fb.group({
            hrKpi: this._fb.array([
                this.initQuestion(),
            ])
        });
        this.vacancyService.currentVacancyId.subscribe(id => this.vacancyId = id)
        this.getHrKpiDetails();
    }

    initQuestion() {

        return this._fb.group({
            id: [],
            question: [''],
            weightage: ['']

        });
    }

    addHrKpi() {
        const control = <FormArray>this.hrKpiForm.controls['hrKpi'];
        control.push(this.initQuestion());
    }

    getHrKpiDetails() {
        this.route.queryParams.subscribe(params => {
            if (params['id']) {
                this.vacancyService.getHrQuestion(params['id']).subscribe(result => this.onSuccessfulDataLoad(result), error => this.onDataLoadFailed());
            }
            else {
                this.isNew = true;
                this.vacancyService.getHrQuestion(this.vacancyId).subscribe(result => this.onSuccessfulDataLoad(result), error => this.onDataLoadFailed());
            }
        });
    }


    onSuccessfulDataLoad(hrQuestions: HrKpi[]) {
        this.hrKpiArray = hrQuestions;
        if (hrQuestions.length > 0) {
            this.setHrQuestions(this.hrKpiArray);
        }
    }
    setHrQuestions(questions: any[]) {
        let control = <FormArray>this.hrKpiForm.controls.hrKpi;
        if (control.length > 0) {
            for (var i = 0; i <= control.length; i++) {
                control.controls.splice(0);
            }
        }
        questions.forEach(x => {
            control.push(this._fb.group({
                id: x.id,
                question: x.question,
                weightage: x.weightage
            }))
        });

    }
    onDataLoadFailed() {
        this.alertService.showInfoMessage("Please Try Again");
    }

    removeHrKpi($event, i) {
        if ($event != "" && $event != null) {
            this.vacancyService.deleteHrQuestion($event).subscribe(sucess => this.deleteSuccessHelper(i), error => this.failedHelper(error, "Couldn't delete successfully."));
        }
        else {
            const control = <FormArray>this.hrKpiForm.controls['hrKpi'];
            control.removeAt(i);
        }
    }

    private deleteSuccessHelper(i: number) {
        const control = <FormArray>this.hrKpiForm.controls['hrKpi'];
        control.removeAt(i);
        this.alertService.showInfoMessage("Deleted Successfully");
    }

    get hrKpi() {
        return this.hrKpiForm.get('hrKpi') as FormArray;
    }

    save() {
        this.isSaving = true;
        this.hrKpiArray = this.hrKpiForm.controls['hrKpi'].value;
        this.route.queryParams.subscribe(params => {
            if (params['id']) {
                this.vacancyService.createHrQuestion(this.hrKpiArray, params['id']).subscribe(sucess => this.Savesuccess(), error => this.failedHelper(error, "Please try later"));
            }
            else {
                this.vacancyService.createHrQuestion(this.hrKpiArray, this.vacancyId).subscribe(sucess => this.Savesuccess(), error => this.failedHelper(error, "Please try later"));
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
        this.getHrKpiDetails();

    }
    private failedHelper(error: any, errMsg: string) {
        this.isSaving = false;
        this.alertService.showInfoMessage(errMsg);
    }
}
