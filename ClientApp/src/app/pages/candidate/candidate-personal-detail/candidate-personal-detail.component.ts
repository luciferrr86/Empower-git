import { Component, OnInit, trigger, transition, style, animate } from '@angular/core';
import { AlertService } from '../../../services/common/alert.service';
import { AccountService } from '../../../services/account/account.service';
import { Utilities } from '../../../services/common/utilities';
import { JobCandidateProfile } from '../../../models/candidate/candidate-personal.model';
import { CandidateService } from '../../../services/candidate/candidate.service';

@Component({
  selector: 'candidate-personal-detail',
  templateUrl: './candidate-personal-detail.component.html',
  styleUrls: ['./candidate-personal-detail.component.css'],
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
export class CandidatePersonalDetailComponent implements OnInit {

  public isSaving = false;
  public personalEdit: JobCandidateProfile = new JobCandidateProfile();
  public serverCallback: () => void;

  constructor(private profileService: CandidateService, private alertService: AlertService, private accountService: AccountService) {
  }

  ngOnInit() {
    this.getPersonalDetail();
  }
  private getPersonalDetail() {

    this.profileService.getPersonal(this.accountService.currentUser.id).subscribe(result => this.onSuccessfulDataLoad(result), error => this.onDataLoadFailed(error));
  }
  onSuccessfulDataLoad(personal: JobCandidateProfile) {

    (<any>Object).assign(this.personalEdit, personal);
    this.personalEdit.fullName = this.accountService.currentUser.fullName;
    this.personalEdit.contactNo = this.accountService.currentUser.phoneNumber;
    this.personalEdit.emailId = this.accountService.currentUser.email;
  }

  onDataLoadFailed(error: any) {
    this.alertService.showInfoMessage("Please Try Again");
  }

  UpdatePersonal() {
    this.isSaving = true;
    this.profileService.updatePersonal(this.personalEdit, this.personalEdit.id).subscribe(sucess => this.saveSuccessHelper(sucess), error => this.saveFailedHelper(error));
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
