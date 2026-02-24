import { Component, OnInit } from '@angular/core';
import { JobQualification } from '../../../models/candidate/candidate-educational.model';
import { AlertService } from '../../../services/common/alert.service';
import { Utilities } from '../../../services/common/utilities';
import { AccountService } from '../../../services/account/account.service';
import { CandidateService } from '../../../services/candidate/candidate.service';

@Component({
  selector: 'candidate-educational-detail',
  templateUrl: './candidate-educational-detail.component.html',
  styleUrls: ['./candidate-educational-detail.component.css']
})
export class CandidateEducationalDetailComponent implements OnInit {

  public isSaving = false;
  public educationalEdit: JobQualification = new JobQualification();
  public serverCallback: () => void;

  constructor(private profileService: CandidateService, private alertService: AlertService, private accountService: AccountService) {

  }

  ngOnInit() {
    this.getQualification();
  }

  private getQualification() {
    this.profileService.getQualification(this.accountService.currentUser.id).subscribe(result => this.onSuccessfulDataLoad(result), error => this.onDataLoadFailed(error));
  }

  public UpdateEducational() {
    this.isSaving = true;
    this.profileService.updateQualification(this.educationalEdit, this.educationalEdit.id).subscribe(sucess => this.saveSuccessHelper(sucess), error => this.saveFailedHelper(error));
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
  onSuccessfulDataLoad(educationl: JobQualification) {
    (<any>Object).assign(this.educationalEdit, educationl);

   }
 
   onDataLoadFailed(error: any) {    
     this.alertService.showInfoMessage("Please Try Again");
   }

}
