import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { ForgotPassword } from '../../../models/account/forgot-password.model';
import { ForgotPasswordService } from '../../../services/account/forgot-password.service';
import { AlertService } from '../../../services/common/alert.service';
@Component({
  selector: 'forgot',
  templateUrl: './forgot.component.html',
  styleUrls: ['./forgot.component.css'],
  encapsulation: ViewEncapsulation.None
})
export class ForgotComponent implements OnInit {
  public forgotPassword: ForgotPassword = new ForgotPassword();
  isSaving: boolean = false;
  constructor(private forgotPasswordService: ForgotPasswordService, private alertService: AlertService) { }

  ngOnInit() {
  }
  public submit() {
    this.isSaving = true;
    this.forgotPasswordService.sendForgotPasswordLink(this.forgotPassword.emailId).subscribe(result => this.onSuccessfulDataLoad(result), error => this.onDataLoadFailed(error));
  }

  onSuccessfulDataLoad(forgot:ForgotPassword) {
    this.isSaving = false;
    this.alertService.showSucessMessage("password reset successfuly sent your emailid");

  }
  onDataLoadFailed(error: any) {
    this.isSaving = false;
    this.alertService.showInfoMessage("Unable to retrieve list from the server");
  }
}
