import { Injectable, Injector } from '@angular/core';
import { EndpointFactory } from '../common/endpoint-factory.service';
import { HttpClient } from '@angular/common/http';
import { ConfigurationService } from '../common/configuration.service';
import { Observable } from 'rxjs';

@Injectable()
export class HrLeaveService extends EndpointFactory {

  private readonly _leaveUrl: string = "/api/LeaveHrView/list";
  private readonly _leaveDetailsUrl: string = "/api/LeaveHrView/leaveDetails"

  private get leaveUrl() { return this.configurations.baseUrl + this._leaveUrl; }
  private get leaveDetailUrl() { return this.configurations.baseUrl + this._leaveDetailsUrl; }

  constructor(http: HttpClient, configurations: ConfigurationService, injector: Injector) {
    super(http, configurations, injector);
  }

  getAllEmployees(page?: number, pageSize?: number, name?: string): Observable<any> {
    let endpointUrl = `${this.leaveUrl}/${page}/${pageSize}/${name}`;
    return this.http.get(endpointUrl, this.getRequestHeaders()).
      catch(error => {
        return this.handleError(error, () => this.getAllEmployees(page, pageSize, name));
      });
  }
  getLeaveDetails(employeeId: string) {
    let endpointUrl = `${this.leaveDetailUrl}/${employeeId}`;
    return this.http.get(endpointUrl,this.getRequestHeaders()).catch(error => {
      return this.handleError(error, () => this.getLeaveDetails(employeeId));
    });
  }

}
