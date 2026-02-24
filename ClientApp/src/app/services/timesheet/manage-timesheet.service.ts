import { Injectable, Injector } from '@angular/core';
import { EndpointFactory } from '../common/endpoint-factory.service';
import { HttpClient } from '@angular/common/http';
import { ConfigurationService } from '../common/configuration.service';
import { Observable } from '../../../../node_modules/rxjs';

@Injectable()
export class ManageTimesheetService extends EndpointFactory {

  private readonly _getAllUrl: string = "/api/ManageTimesheet/list";
  private readonly _getEmployeeUrl: string = "/api/ManageTimesheet/GetEmployeeTimeSheet";
  private readonly _approveUrl: string = "/api/ManageTimesheet/approve";
  private get getAllUrl() { return this.configurations.baseUrl + this._getAllUrl; }
  private get getEmployeeUrl() { return this.configurations.baseUrl + this._getEmployeeUrl; }
  private get approveUrl() { return this.configurations.baseUrl + this._approveUrl; }

  constructor(http: HttpClient, configurations: ConfigurationService, injector: Injector) {
    super(http, configurations, injector);
  }

  getAll(userId?: string, page?: number, pageSize?: number, name?: string): Observable<any> {
    let endpointUrl = `${this.getAllUrl}/${userId}/${page}/${pageSize}/${name}`;
    return this.http.get(endpointUrl, this.getRequestHeaders()).catch(error => {
      return this.handleError(error, () => this.getAll(userId, page, pageSize, name));
    })
  }

  getEmployeeTimesheet(id?: string) {
    let endpointUrl = `${this.getEmployeeUrl}/${id}`;
    return this.http.get(endpointUrl, this.getRequestHeaders()).catch(error => {
      return this.handleError(error, () => this.getEmployeeTimesheet(id));
    })
  }
  approve(timesheet: any) {
    return this.http.post(this.approveUrl, JSON.stringify(timesheet), this.getRequestHeaders()).catch(error => { return this.handleError(error, () => this.approve(timesheet)); })
  }
}
