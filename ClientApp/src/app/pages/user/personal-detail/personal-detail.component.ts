import { Component, OnInit, ViewChild } from '@angular/core';
import { trigger, transition, animate, style } from '@angular/animations';
import { ProfileService } from '../../../services/maintenance/profile.service';
import { PersonalDetail } from '../../../models/maintenance/personal-detail.model';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { AlertService } from '../../../services/common/alert.service';
import { Utilities } from '../../../services/common/utilities';
import { AccountService } from '../../../services/account/account.service';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'personal-detail',
  templateUrl: './personal-detail.component.html',
  styleUrls: ['./personal-detail.component.css'],
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
export class PersonalDetailComponent implements OnInit {

  public isSaving = false;
  private isNew = false;
  public personalEdit: PersonalDetail = new PersonalDetail();


  @ViewChild('editorModal')
  editorModal: ModalDirective;

  constructor(private profileService: ProfileService, private alertService: AlertService, private accountService: AccountService, private datePipe: DatePipe) {

  }

  ngOnInit() {
    this.getPersonalDetail();
  }

  private getPersonalDetail() {
    this.profileService.getPersonal(this.accountService.currentUser.id).subscribe(result => this.onSuccessfulDataLoad(result), error => this.onDataLoadFailed(error));
  }

  public UpdatePersonal() {
    this.isSaving = true;
    this.profileService.updatePersonal(this.personalEdit, this.personalEdit.id).subscribe(sucess => this.saveSuccessHelper(sucess), error => this.saveFailedHelper(error));
  }
  private saveSuccessHelper(result?: string) {
    this.isSaving = false;
    this.alertService.showSucessMessage("Your personal detail has been updated.");
    this.getPersonalDetail();

  }

  private saveFailedHelper(error: any) {
    this.isSaving = false;
    let test = Utilities.getHttpResponseMessage(error);
    this.alertService.showInfoMessage("Please try later" + test[0]);
  }
  onSuccessfulDataLoad(personal: PersonalDetail) {
    (<any>Object).assign(this.personalEdit, personal);
    personal.dob = new Date(this.datePipe.transform(personal.dob, "yyyy-MM-dd"));
  }

  onDataLoadFailed(error: any) {
    this.alertService.showInfoMessage("Please Try Again");
  }

  fillCurrentAddress(event:any) {
    if (event.target.checked) {
      this.personalEdit.currentAddress = this.personalEdit.permanentAddress;
      this.personalEdit.currentCity = this.personalEdit.city;
      this.personalEdit.currentState = this.personalEdit.state;
      this.personalEdit.currentCountry = this.personalEdit.country;
      this.personalEdit.currentZipCode = this.personalEdit.zipCode;
    }
    else {
      this.personalEdit.currentAddress = null;
      this.personalEdit.currentCity = null;
      this.personalEdit.currentState = null;
      this.personalEdit.currentCountry = null;
      this.personalEdit.currentZipCode = null;
    }
  }
}
