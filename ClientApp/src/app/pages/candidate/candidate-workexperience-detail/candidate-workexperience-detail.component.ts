import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators, FormArray } from '@angular/forms';
import { AlertService } from '../../../services/common/alert.service';
import { AccountService } from '../../../services/account/account.service';
import { Utilities } from '../../../services/common/utilities';
import { JobWorkExperience } from '../../../models/candidate/candidate-workexperience.model';
import { CandidateService } from '../../../services/candidate/candidate.service';

@Component({
  selector: 'candidate-workexperience-detail',
  templateUrl: './candidate-workexperience-detail.component.html',
  styleUrls: ['./candidate-workexperience-detail.component.css']
})
export class CandidateWorkexperienceDetailComponent implements OnInit {

  public isSaving = false;
  private isNew = false;
  public professionalEdit: JobWorkExperience[];
  public professionalArray: JobWorkExperience[] = [];
  public serverCallback: () => void;
  public myForm: FormGroup;

  constructor(private profileService: CandidateService, private alertService: AlertService, private _fb: FormBuilder, private accountService: AccountService) {

  }

  ngOnInit() {
    this.getProfessional();
    this.myForm = this._fb.group({
      professional: this._fb.array([
        this.initAddress(),
      ])
    });
  }

  initAddress() {

    return this._fb.group({
      id: [''],
      companyName: ['', Validators.required],
      designation: ['', Validators.required],
      doj: ['', Validators.required],
      dor: ['', Validators.required],
      empId: ['']

    });
  }
  private getProfessional() {
    this.profileService.getprofessional(this.accountService.currentUser.id).subscribe(result => this.onSuccessfulDataLoad(result), error => this.onDataLoadFailed(error));
  }
  onSuccessfulDataLoad(professional: JobWorkExperience[]) {

    this.professionalEdit = professional;

    let control = <FormArray>this.myForm.controls.professional;
    this.professionalEdit.forEach(x => {
      control.push(this._fb.group({
        id: x.id,
        companyName: x.companyName,
        designation: x.designation,
        doj: this.getDateFormat(x.doj),
        dor: this.getDateFormat(x.dor),
        empId: x.empID
      }))
    });
  }
  getDateFormat(date: Date) {
    const currentDate = new Date(date);
    return currentDate.toISOString().substring(0, 10);
  }

  onDataLoadFailed(error: any) {
    this.alertService.showInfoMessage("Please Try Again");
  }
  save() {
    this.isSaving = true;
    this.professionalArray = this.myForm.value;
    this.profileService.updateProfessional(this.professionalArray, this.accountService.currentUser.id).subscribe(sucess => this.saveSuccessHelper(sucess), error => this.saveFailedHelper(error));
  }
  addProfessional() {
    const control = <FormArray>this.myForm.controls['professional'];
    control.push(this.initAddress());
  }
  get professional() {
    return this.myForm.get('professional') as FormArray;
  }

  removeProfessional(i: number) {
    const control = <FormArray>this.myForm.controls['professional'];
    control.removeAt(i);
  }
  private saveSuccessHelper(result?: string) {
    this.isSaving = false;
    this.alertService.showSucessMessage("Updated successfully");
    this.serverCallback();
  }

  private saveFailedHelper(error: any) {
    this.isSaving = false;
    let test = Utilities.getHttpResponseMessage(error);
    this.alertService.showInfoMessage("Please try later" + test[0]);
  }
}
