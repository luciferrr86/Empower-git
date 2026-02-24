import { Injectable, Injector } from '@angular/core';
import { EndpointFactory } from '../../common/endpoint-factory.service';
import { HttpClient } from '@angular/common/http';
import { ConfigurationService } from '../../common/configuration.service';
import { Observable } from 'rxjs';
import { TimesheetTemplateModel } from '../../../models/configuration/timesheet/timesheet-template.model';
import { TimesheetScheduleModel } from '../../../models/configuration/timesheet/timesheet-schedule.model';

@Injectable()
export class TimesheetTemplateService extends EndpointFactory {

  private readonly _listUrl: string = "/api/TimeSheetTemplate/list";
  private readonly _createUrl: string = "/api/TimeSheetTemplate/create";
  private readonly _updateUrl: string = "/api/TimeSheetTemplate/update";
  private readonly _deleteUrl: string = "/api/TimeSheetTemplate/delete";
  private readonly _listScheduleUrl: string = "/api/TimeSheetTemplate/getAllSchedule";
  private readonly _setScheduleUrl: string = "/api/TimeSheetTemplate/setSchedule";
  private readonly _updateScheduleUrl: string = "/api/TimeSheetTemplate/updateSchedule";
  private readonly _getEmployeeByProjectIdUrl: string = "/api/TimeSheetTemplate/getEmployee";

  private get listUrl() { return this.configurations.baseUrl + this._listUrl; }
  private get createUrl() { return this.configurations.baseUrl + this._createUrl; }
  private get updateUrl() { return this.configurations.baseUrl + this._updateUrl; }
  private get deleteUrl() { return this.configurations.baseUrl + this._deleteUrl; }
  private get listScheduleUrl() { return this.configurations.baseUrl + this._listScheduleUrl; }
  private get setScheduleUrl() { return this.configurations.baseUrl + this._setScheduleUrl; }
  private get updateScheduleUrl() { return this.configurations.baseUrl + this._updateScheduleUrl; }
  private get getEmployeeByProjectIdUrl() { return this.configurations.baseUrl + this._getEmployeeByProjectIdUrl; }

  constructor(http: HttpClient, configurations: ConfigurationService, injector: Injector) {
    super(http, configurations, injector);
  }
  getAllTemplate(page?: number, pageSize?: number, name?: string): Observable<any> {
    let endpointUrl = `${this.listUrl}/${page}/${pageSize}/${name}`;
    return this.http.get(endpointUrl, this.getRequestHeaders()).catch(error => {
      return this.handleError(error, () => this.getAllTemplate(page, pageSize, name));
    })
  }

  create(template: TimesheetTemplateModel): Observable<string> {
    return this.http.post(this.createUrl, JSON.stringify(template), this.getRequestHeaders()).catch(error => { return this.handleError(error, () => this.create(template)); })
  }

  update(template: any, templateId?: string) {
    let endpointUrl = templateId ? `${this.updateUrl}/${templateId}` : this.updateUrl;
    return this.http.put(endpointUrl, JSON.stringify(template), this.getRequestHeaders())
      .catch(error => {
        return this.handleError(error, () => this.update(template, templateId));
      });
  }

  delete(templateId: string) {
    let endpointUrl = `${this.deleteUrl}/${templateId}`;
    return this.http.delete(endpointUrl, this.getRequestHeaders())
      .catch(error => {
        return this.handleError(error, () => this.delete(templateId));
      });
  }

  getAllSchedule(page?: number, pageSize?: number, name?: string): Observable<any> {
    let endpointUrl = `${this.listScheduleUrl}/${page}/${pageSize}/${name}`;
    return this.http.get(endpointUrl, this.getRequestHeaders()).catch(error => {
      return this.handleError(error, () => this.getAllSchedule(page, pageSize, name));
    })
  }

  setSchedule(schedule: TimesheetScheduleModel): Observable<string> {
    return this.http.post(this.setScheduleUrl, JSON.stringify(schedule), this.getRequestHeaders()).catch(error => { return this.handleError(error, () => this.setSchedule(schedule)); })
  }

  updateSchedule(schedule: any, scheduleId?: string) {
    let endpointUrl = scheduleId ? `${this.updateScheduleUrl}/${scheduleId}` : this.updateUrl;
    return this.http.put(endpointUrl, JSON.stringify(schedule), this.getRequestHeaders())
      .catch(error => {
        return this.handleError(error, () => this.updateSchedule(schedule, scheduleId));
      });
  }
  getEmployeeByProjectId(id: string, page?: number, pageSize?: number, name?: string): Observable<any> {
    let endpointUrl = `${this.getEmployeeByProjectIdUrl}/${id}/${page}/${pageSize}/${name}`;
    return this.http.get(endpointUrl, this.getRequestHeaders()).catch(error => {
      return this.handleError(error, () => this.getEmployeeByProjectId(id, page, pageSize, name));
    })
  }
}
