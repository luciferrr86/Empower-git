import { Component, OnInit } from '@angular/core';
import { ChangePassword } from '../../../models/profile/change-password.model';
import { ChangePasswordService } from '../../../services/profile/change-password.service';
import { AccountService } from '../../../services/account/account.service';
import { AlertService } from '../../../services/common/alert.service';
import { Router } from '@angular/router';

@Component({
  selector: 'change-password',
  templateUrl: './change-password.component.html',
  styleUrls: ['./change-password.component.css']
})
export class ChangePasswordComponent implements OnInit {

  isSaving:boolean=false;
  public changePassword: ChangePassword = new ChangePassword();
  constructor(private router: Router,private changePasswordService:ChangePasswordService,private accountService:AccountService,private alertService:AlertService) { }

  ngOnInit() {
  }

  public save() {
    this.isSaving=true;
    this.changePasswordService.updatePassword(this.accountService.currentUser.id,this.changePassword).subscribe(result => this.onSuccessfulDataLoad(result), error => this.onDataLoadFailed(error));
  }

  onSuccessfulDataLoad(password:ChangePassword) {
    this.isSaving = false;
    this.alertService.showInfoMessage("Password is successfuly changed");
    this.router.navigate(['../account/login']);

  }
  onDataLoadFailed(error: any) {
    this.alertService.showInfoMessage("your password is incorrect Please try again:");
    this.isSaving = false;
  }

}
