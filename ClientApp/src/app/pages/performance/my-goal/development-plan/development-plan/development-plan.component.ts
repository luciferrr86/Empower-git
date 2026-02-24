import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, FormArray } from '@angular/forms';
import { MyGoalService } from '../../../../../services/performance/my-goal/my-goal.service';
import { AccountService } from '../../../../../services/account/account.service';
import { DevelpomentPlan, CareerDevelopment } from '../../../../../models/performance/common/develpoment-plan.model';
import { AlertService } from '../../../../../services/common/alert.service';

@Component({
  selector: 'development-plan',
  templateUrl: './development-plan.component.html',
  styleUrls: ['./development-plan.component.css']
})
export class DevelopmentPlanComponent implements OnInit {

  developmentForm: FormGroup;
  public developmentPlan: DevelpomentPlan = new DevelpomentPlan();
  public isSaving: boolean;
  public isSubmitting: boolean;
  constructor(private fb: FormBuilder, private myGoalService: MyGoalService, private accountService: AccountService, private alertService: AlertService) { }

  ngOnInit() {
    this.developmentForm = this.fb.group({
      employeeCareerDevList: this.fb.array([
        this.initDevPlan()
      ]),
      managerCareerDevList: this.fb.array([
        this.initDevPlan()
      ])
    });
    this.getDevelopmentGoalDetail();
  }

  initDevPlan() {
    return this.fb.group({
      careerDevId: [''],
      skillText: [''],
      careerInterestText: ['']
    })
  }
  getDevelopmentGoalDetail() {
    this.myGoalService.getDevelopmentPlanList(this.accountService.currentUser.id).subscribe(result => this.onSuccessfulDataLoad(result), error => this.onDataLoadFailed(error));
  }
  onSuccessfulDataLoad(developmentPlan: DevelpomentPlan) {
    this.developmentPlan = developmentPlan;
    this.setDevelopementPlan(this.developmentPlan.employeeCareerDevList, "employee");
    this.setDevelopementPlan(this.developmentPlan.managerCareerDevList, "manager");
  }
  setDevelopementPlan(devPlan: CareerDevelopment[], ctrlFor: string) {
    let arr: FormArray;
    if (ctrlFor == "employee") {
      let control = <FormArray>this.developmentForm.controls['employeeCareerDevList'];
      arr = control;
    }
    else {
      let control = <FormArray>this.developmentForm.controls['managerCareerDevList'];
      arr = control;
    }

    let control = arr;
    if (control.length > 0) {
      for (var i = 0; i <= control.length; i++) {
        control.controls.splice(0);
      }
    }

    devPlan.forEach(x => {
      control.push(this.fb.group({
        careerDevId: x.careerDevId,
        skillText: x.skillText,
        careerInterestText: x.careerInterestText
      }))
    });
  }
  onDataLoadFailed(error: any) {
    this.alertService.showInfoMessage("Unable to retrieve list from the server");
  }

  get employeeCareerDevList() {
    return this.developmentForm.get('employeeCareerDevList') as FormArray;
  }

  get managerCareerDevList() {
    return this.developmentForm.get('managerCareerDevList') as FormArray;
  }

  addEmployeeDevPlan() {
    const control = <FormArray>this.developmentForm.controls['employeeCareerDevList'];
    control.push(this.initDevPlan());
  }

  addManagerDevPlan() {
    const control = <FormArray>this.developmentForm.controls['managerCareerDevList'];
    control.push(this.initDevPlan());
  }

  removeEmployeeDevPlan(i: number) {
    const control = <FormArray>this.developmentForm.controls['employeeCareerDevList'];
    control.removeAt(i);
  }

  removeManagerDevPlan(i: number) {
    const control = <FormArray>this.developmentForm.controls['managerCareerDevList'];
    control.removeAt(i);
  }

  saveDevelopmentPlan() {
    this.isSaving = true;
    this.developmentPlan.employeeCareerDevList = this.developmentForm.controls.employeeCareerDevList.value;
    this.developmentPlan.managerCareerDevList = this.developmentForm.controls.managerCareerDevList.value;
    this.myGoalService.saveDevelopmentPlan(this.accountService.currentUser.id, this.developmentPlan, 'save').subscribe(result => this.onSaveSuccessfulDataLoad(result, 'save'), error => this.onSaveSubmitDataLoadFailed(error));

  }
  submitDevelopmentPlan() {
    this.isSubmitting = true;
    this.developmentPlan.employeeCareerDevList = this.developmentForm.controls.employeeCareerDevList.value;
    this.developmentPlan.managerCareerDevList = this.developmentForm.controls.managerCareerDevList.value;
    this.myGoalService.saveDevelopmentPlan(this.accountService.currentUser.id, this.developmentPlan, 'submit').subscribe(result => this.onSaveSuccessfulDataLoad(result, 'submit'), error => this.onSaveSubmitDataLoadFailed(error));
  }
  onSaveSuccessfulDataLoad(res: any, actionType: string) {

    if (actionType == "save") {
      this.isSaving = false;
      this.alertService.showInfoMessage("Development plan saved successfully.");
    }
    else {
      this.isSubmitting = false;
      this.alertService.showInfoMessage("Development plan submitted successfully.");
    }
    this.getDevelopmentGoalDetail();

  }

  onSaveSubmitDataLoadFailed(error: any) {
    this.isSaving = false;
    this.isSubmitting = false;
    this.alertService.showInfoMessage("Unable to save/submit the development plan");
  }
}
