import { Injectable, Injector } from '@angular/core';
import { EndpointFactory } from '../common/endpoint-factory.service';
import { HttpClient } from '@angular/common/http';
import { ConfigurationService } from '../common/configuration.service';

@Injectable()
export class MyTimesheetService extends EndpointFactory {

  private readonly _getmyTimesheet: string = "/api/MyTimesheet/myTimesheet";
  private readonly _getweeklyTimesheet: string = "/api/MyTimesheet/weeklyTimesheet";
  private readonly _createUrl: string = "/api/MyTimesheet/create";
  private readonly _submitUrl: string = "/api/MyTimesheet/summit";

  private get getmyTimesheet() { return this.configurations.baseUrl + this._getmyTimesheet; }
  private get getweeklyTimesheet() { return this.configurations.baseUrl + this._getweeklyTimesheet; }
  private get createUrl() { return this.configurations.baseUrl + this._createUrl; }
  private get submitUrl() { return this.configurations.baseUrl + this._submitUrl; }

  constructor(http: HttpClient, configurations: ConfigurationService, injector: Injector) {
    super(http, configurations, injector);
  }

  getAll(id: string) {
    let endpointUrl = `${this.getmyTimesheet}/${id}`;
    return this.http.get(endpointUrl, this.getRequestHeaders()).catch(error => {
   
      return this.handleError(error, () => this.getAll(id));
    })
  }

  getWeekly(id: string ,spanId : string) {
    let endpointUrl = `${this.getweeklyTimesheet}/${id}/${spanId}`;
    return this.http.get(endpointUrl, this.getRequestHeaders()).catch(error => {
      return this.handleError(error, () => this.getWeekly(id,spanId));
    })
  }
  create(timesheet: any) {
    return this.http.post(this.createUrl, JSON.stringify(timesheet), this.getRequestHeaders()).catch(error => { return this.handleError(error, () => this.create(timesheet)); })
  }
  submit(timesheet: any){
    return this.http.post(this.submitUrl, JSON.stringify(timesheet), this.getRequestHeaders()).catch(error => { return this.handleError(error, () => this.submit(timesheet)); })
  }
}
