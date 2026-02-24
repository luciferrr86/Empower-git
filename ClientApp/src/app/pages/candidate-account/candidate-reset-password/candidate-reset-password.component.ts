import { Component, OnInit } from '@angular/core';
import { ResetPassword } from '../../../models/account/reset-password.model';
import { ActivatedRoute, Router } from '@angular/router';
import { ForgotPasswordService } from '../../../services/account/forgot-password.service';
import { AlertService } from '../../../services/common/alert.service';

@Component({
  selector: 'app-candidate-reset-password',
  templateUrl: './candidate-reset-password.component.html',
  styleUrls: ['./candidate-reset-password.component.css']
})
export class CandidateResetPasswordComponent implements OnInit {

  public isSaving = false;
  public resetPassword: ResetPassword = new ResetPassword();

  constructor(private route: ActivatedRoute, private router: Router, private forgotPasswordService: ForgotPasswordService, private alertService: AlertService) {
    this.route.queryParams.subscribe(params => {
      alert(params['code']);
      if (params['code'] != undefined && params['email']) {
        this.resetPassword.tokenId = params['code'];
        this.resetPassword.emailId = params['email'];
      }
      else {
      }
    });

  }

  ngOnInit() {
  }

  submit() {
    this.isSaving = true;
    this.forgotPasswordService.ResetPassword(this.resetPassword).subscribe(result => this.onSuccessfulDataLoad(result), error => this.onDataLoadFailed(error));
  }
  onSuccessfulDataLoad(forgot: ResetPassword) {
    this.isSaving = false;
    this.alertService.showInfoMessage("your password update successfully");

  }
  onDataLoadFailed(error: any) {
    this.alertService.showInfoMessage("Unable to retrieve list from the server");
  }
}
