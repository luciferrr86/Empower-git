import { Injectable, Injector } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { EndpointFactory } from '../common/endpoint-factory.service';
import { ConfigurationService } from '../common/configuration.service';

@Injectable()
export class ForgotPasswordService extends EndpointFactory {
  private readonly _getUrl: string = "/api/account/users/forgotPassword";
  private readonly _getResetPasswordUrl: string = "/api/account/users/resetPassword"

  private get getUrl() { return this.configurations.baseUrl + this._getUrl; }
  private get getResetPasswordUrl() { return this.configurations.baseUrl + this._getResetPasswordUrl; }

  constructor(http: HttpClient, configurations: ConfigurationService, injector: Injector) {
    super(http, configurations, injector);
  }

  sendForgotPasswordLink(emailId: string) {
    let endpointUrl = `${this.getUrl}/${emailId}`;
    return this.http.post(endpointUrl, this.getRequestHeaders()).
      catch(error => {
        return this.handleError(error, () => this.sendForgotPasswordLink(emailId));
      });
  }
  ResetPassword(reset: any) {
    let endpointUrl = `${this.getResetPasswordUrl}`
    return this.http.post(endpointUrl, JSON.stringify(reset), this.getRequestHeaders()).
      catch(error => {
        return this.handleError(error, () => this.ResetPassword(reset));
      });

  }


}
