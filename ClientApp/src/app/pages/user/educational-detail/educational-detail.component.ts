import { Component, OnInit } from '@angular/core';
import { trigger, transition, style, animate } from '@angular/animations';
import { ProfileService } from '../../../services/maintenance/profile.service';
import { AlertService } from '../../../services/common/alert.service';
import { Utilities } from '../../../services/common/utilities';
import { EducationlDetail } from '../../../models/maintenance/educationl-detail.model';
import { AccountService } from '../../../services/account/account.service';

@Component({
  selector: 'educational-detail',
  templateUrl: './educational-detail.component.html',
  styleUrls: ['./educational-detail.component.css'],
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
export class EducationalDetailComponent implements OnInit {

  public isSaving = false;
  public educationalEdit: EducationlDetail = new EducationlDetail();
  public serverCallback: () => void;


  constructor(private profileService: ProfileService, private alertService: AlertService, private accountService: AccountService) {

  }

  ngOnInit() {
    this.getQualification();
  }

  private getQualification() {
    this.profileService.getQualification(this.accountService.currentUser.id).subscribe(result => this.onSuccessfulDataLoad(result), error => this.onDataLoadFailed());
  }

  public UpdateEducational() {
    this.isSaving = true;
    this.profileService.updateQualification(this.educationalEdit, this.educationalEdit.id).subscribe(sucess => this.saveSuccessHelper(), error => this.saveFailedHelper(error));
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
  onSuccessfulDataLoad(educationl: EducationlDetail) {
    (<any>Object).assign(this.educationalEdit, educationl);

   }
 
   onDataLoadFailed() {    
     this.alertService.showInfoMessage("Please Try Again");
   }
}
