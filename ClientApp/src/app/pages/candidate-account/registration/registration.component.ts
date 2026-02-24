import { Component, OnInit, Input } from '@angular/core';
import { CandidateViewModel } from '../../../models/candidate/candidate.registration.model';
import { CandidateService } from '../../../services/candidate/candidate.service';
import { Utilities } from '../../../services/common/utilities';
import { AlertService, DialogType, MessageSeverity } from '../../../services/common/alert.service';
import { Router } from '@angular/router';
import { AuthService } from '../../../services/common/auth.service';
import { ConfigurationService } from '../../../services/common/configuration.service';

@Component({
  selector: 'registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.css']
})
export class RegistrationComponent implements OnInit {

  constructor(private configurations: ConfigurationService, private authService: AuthService, private router: Router, private candidateService: CandidateService, private alertService: AlertService) { }

  public isSaving = false;
  private isNew = false;
  isLoading = false;
  formResetToggle = true;
  modalClosedCallback: () => void;
  @Input()
  isModal = false;
  public candidateRegister: CandidateViewModel = new CandidateViewModel();
  private serverCallback: () => void;
  //private loginPage: () => "http://localhost:4200/candidate/login";

  ngOnInit() {
  }

  save() {
    this.isSaving = true;
    this.candidateService.create(this.candidateRegister).subscribe(sucess => this.saveSuccessHelper(sucess), error => this.saveFailedHelper(error));
  }
  private saveSuccessHelper(result?: string) {
    this.isSaving = false;
    // if (this.isNew) {
    // this.alertService.showSucessMessage("Register successfully");
    this.login();
    // }
    this.serverCallback();
  }

  private saveFailedHelper(error: any) {
    this.isSaving = false;
    let test = Utilities.getHttpResponseMessage(error);
    console.log(test);
    this.alertService.showInfoMessage("Please try later" + test[0]);
  }
  login() {
    this.isLoading = true;
    this.alertService.startLoadingMessage("", "Attempting login...");
    this.authService.login(this.candidateRegister.email, this.candidateRegister.password, this.candidateRegister.rememberMe)
      .subscribe(
        user => {
          setTimeout(() => {
            this.alertService.stopLoadingMessage();
            this.isLoading = false;
            this.reset();

            if (!this.isModal) {
              this.router.navigate(['../candidate/profile']);
              //this.alertService.showMessage("Login", `Welcome ${user.userName}!`, MessageSeverity.success);
            }
            else {
              this.alertService.showMessage("Login", `Session for ${user.userName} restored!`, MessageSeverity.success);
              setTimeout(() => {
                this.alertService.showStickyMessage("Session Restored", "Please try your last operation again", MessageSeverity.default);
              }, 500);

              this.closeModal();
            }
          }, 500);
        },
        error => {

          this.alertService.stopLoadingMessage();

          if (Utilities.checkNoNetwork(error)) {
            this.alertService.showStickyMessage(Utilities.noNetworkMessageCaption, Utilities.noNetworkMessageDetail, MessageSeverity.error, error);
            this.offerAlternateHost();
          }
          else {
            this.alertService.showInfoMessage("please enter valid username and password");
            let errorMessage = Utilities.findHttpResponseMessage("error_description", error);
            if (errorMessage)
              this.alertService.showStickyMessage("Unable to login", errorMessage, MessageSeverity.error, error);
            else
              this.alertService.showStickyMessage("Unable to login", "An error occured whilst logging in, please try again later.\nError: " + error.statusText || error.status, MessageSeverity.error, error);
          }

          setTimeout(() => {
            this.isLoading = false;
          }, 500);
        });
  }
  closeModal() {
    if (this.modalClosedCallback) {
      this.modalClosedCallback();
    }
  }
  offerAlternateHost() {

    if (Utilities.checkIsLocalHost(location.origin) && Utilities.checkIsLocalHost(this.configurations.baseUrl)) {
      this.alertService.showDialog("Dear Developer!\nIt appears your backend Web API service is not running...\n" +
        "Would you want to temporarily switch to the online Demo API below?(Or specify another)",
        DialogType.prompt,
        (value: string) => {
          this.configurations.baseUrl = value;
          this.alertService.showStickyMessage("API Changed!", "The target Web API has been changed to: " + value, MessageSeverity.warn);
        },
        null,
        null,
        null,
        this.configurations.fallbackBaseUrl);
    }
  }
  reset() {
    this.formResetToggle = false;

    setTimeout(() => {
      this.formResetToggle = true;
    });
  }
}
