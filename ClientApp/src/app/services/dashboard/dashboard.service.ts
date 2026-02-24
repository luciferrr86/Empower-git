import { Injectable, Injector } from '@angular/core';
import { EndpointFactory } from '../common/endpoint-factory.service';
import { HttpClient } from '@angular/common/http';
import { ConfigurationService } from '../common/configuration.service';

@Injectable()
export class DashboardService extends EndpointFactory {

  private readonly _getleaveTaskList: string = "/api/Dashboard/leaveTaskList";
  private readonly _getTimesheetTaskList: string = "/api/Dashboard/timesheetTaskList";
  private readonly _getPerformanceTaskList: string = "/api/Dashboard/performanceTaskList";

  private get getLeaveTask() { return this.configurations.baseUrl + this._getleaveTaskList; }
  private get getTimesheetTask() { return this.configurations.baseUrl + this._getTimesheetTaskList; }
  private get getPerformanceTask() { return this.configurations.baseUrl + this._getPerformanceTaskList; }

  constructor(http: HttpClient, configurations: ConfigurationService, injector: Injector) {

    super(http, configurations, injector);
  }

  getLeaveTaskList(id: string) {
    let endpointUrl = `${this.getLeaveTask}/${id}`;
    return this.http.get(endpointUrl, this.getRequestHeaders()).catch(error => {
      return this.handleError(error, () => this.getLeaveTaskList(id));
    });
  }


  getTimesheetTaskList(id: string) {
    let endpointUrl = `${this.getTimesheetTask}/${id}`;
    return this.http.get(endpointUrl, this.getRequestHeaders()).catch(error => {
      return this.handleError(error, () => this.getTimesheetTaskList(id));
    });
  }

  getPerformanceTaskList(id: string) {
    let endpointUrl = `${this.getPerformanceTask}/${id}`;
    return this.http.get(endpointUrl, this.getRequestHeaders()).catch(error => {
      return this.handleError(error, () => this.getLeaveTaskList(id));
    });
  }
}
