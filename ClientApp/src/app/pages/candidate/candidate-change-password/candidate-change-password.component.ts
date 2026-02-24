import { Component, OnInit } from '@angular/core';
import { ChangePassword } from '../../../models/profile/change-password.model';
import { ChangePasswordService } from '../../../services/profile/change-password.service';
import { AccountService } from '../../../services/account/account.service';
import { AlertService } from '../../../services/common/alert.service';

@Component({
  selector: 'app-candidate-change-password',
  templateUrl: './candidate-change-password.component.html',
  styleUrls: ['./candidate-change-password.component.css']
})
export class CandidateChangePasswordComponent implements OnInit {

  isSaving:boolean=false;
  public changePassword: ChangePassword = new ChangePassword();
  constructor(private changePasswordService:ChangePasswordService,private accountService:AccountService,private alertService:AlertService) { }

  ngOnInit() {
  }

  public save() {
    this.isSaving=true;
    this.changePasswordService.updatePassword(this.accountService.currentUser.id,this.changePassword).subscribe(result => this.onSuccessfulDataLoad(result), error => this.onDataLoadFailed(error));
  }

  onSuccessfulDataLoad(password:ChangePassword) {
    this.isSaving = false;
    this.alertService.showInfoMessage("password is successfuly changed");

  }
  onDataLoadFailed(error: any) {
    this.alertService.showInfoMessage("Unable to retrieve list from the server");
  }


}
