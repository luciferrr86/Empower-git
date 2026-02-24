import { Component, OnInit } from '@angular/core';
import { trigger, transition, animate, style } from '@angular/animations';
import { ProfessionalDetail } from '../../../models/maintenance/professional-detail.model';
import { Utilities } from '../../../services/common/utilities';
import { ProfileService } from '../../../services/maintenance/profile.service';
import { AlertService } from '../../../services/common/alert.service';
import { FormGroup, FormBuilder, Validators, FormArray } from '@angular/forms';
import { AccountService } from '../../../services/account/account.service';

@Component({
  selector: 'professional-detail',
  templateUrl: './professional-detail.component.html',
  styleUrls: ['./professional-detail.component.css'],
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
export class ProfessionalDetailComponent implements OnInit {
  public isSaving = false;
  public professionalEdit: ProfessionalDetail[];
  public professionalArray: ProfessionalDetail[] = [];
  public serverCallback: () => void;
  public myForm: FormGroup;

  constructor(private profileService: ProfileService, private alertService: AlertService, private _fb: FormBuilder, private accountService: AccountService) {

  }

  ngOnInit() {
    this.myForm = this._fb.group({
      professional: this._fb.array([
        this.initAddress(),
      ])
    });
    this.getProfessional();
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
    this.profileService.getprofessional(this.accountService.currentUser.id).subscribe(result => this.onSuccessfulDataLoad(result), error => this.onDataLoadFailed());
  }
  onSuccessfulDataLoad(professional: ProfessionalDetail[]) {
    this.professionalEdit = professional;
    let control = <FormArray>this.myForm.controls.professional;

    if (professional.length > 0) {
      if (control.length > 0) {
        for (var i = 0; i <= control.length; i++) {
          control.controls.splice(0);
        }
      }
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

  }

  getDateFormat(date: Date) {
    const currentDate = new Date(date);
    return currentDate.toISOString().substring(0, 10);
  }

  onDataLoadFailed() {
    this.alertService.showInfoMessage("Please Try Again");
  }
  save() {
    this.isSaving = true;
    this.professionalArray = this.myForm.value;
    this.profileService.updateProfessional(this.professionalArray, this.accountService.currentUser.id).subscribe(sucess => this.saveSuccessHelper(), error => this.saveFailedHelper(error));

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
  private saveSuccessHelper() {
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
