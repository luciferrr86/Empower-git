import { Component, OnInit } from '@angular/core';
import { ResetPassword } from '../../../models/account/reset-password.model';
import { ForgotPasswordService } from '../../../services/account/forgot-password.service';
import { AlertService } from '../../../services/common/alert.service';
import { error } from 'util';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'reset-password',
  templateUrl: './reset-password.component.html',
  styleUrls: ['./reset-password.component.css']
})
export class ResetPasswordComponent implements OnInit {

  public isSaving = false;
  public resetPassword: ResetPassword = new ResetPassword();

  constructor(private route: ActivatedRoute, private router: Router, private forgotPasswordService: ForgotPasswordService, private alertService: AlertService) {
    this.route.queryParams.subscribe(params => {
      if (params['code'] != undefined && params['email']) {
        this.resetPassword.tokenId = params['code'];
        this.resetPassword.emailId = params['email'];
      }
      else {
        this.router.navigate(['../account/login']);
      }
    });
   

  }

  ngOnInit() {
  }

  submit() {
    this.forgotPasswordService.ResetPassword(this.resetPassword).subscribe(result => this.onSuccessfulDataLoad(result), error => this.onDataLoadFailed(error));
  }
  onSuccessfulDataLoad(forgot: ResetPassword) {
    this.isSaving = false;
    this.alertService.showInfoMessage("your password update successfully");

  }
  onDataLoadFailed(error: any) {
    this.alertService.showInfoMessage("Unable to update your password.");
  }
}
