import { Injectable, Injector } from '@angular/core';
import { EndpointFactory } from '../../common/endpoint-factory.service';
import { HttpClient } from '@angular/common/http';
import { ConfigurationService } from '../../common/configuration.service';
import { Observable } from 'rxjs';
import { ProjectModel } from '../../../models/configuration/timesheet/timesheet-project.model';
import { TimesheetAssignProject } from '../../../models/configuration/timesheet/timesheet-assign-project.model';


@Injectable()
export class TimesheetProjectService extends EndpointFactory {

  private readonly _listUrl: string = "/api/TimeSheetProject/list";
  private readonly _createUrl: string = "/api/TimeSheetProject/create";
  private readonly _updateUrl: string = "/api/TimeSheetProject/update";
  private readonly _deleteUrl: string = "/api/TimeSheetProject/delete";
  private readonly _listProjectUrl: string = "/api/TimeSheetProject/getAllProject";
  private readonly _getAssignProjectUrl: string = "/api/TimeSheetProject/getAssignProject";
  private readonly _assignProjectUrl: string = "/api/TimeSheetProject/assignProject";
  private readonly _getEmployeeByProjectIdUrl: string = "/api/TimeSheetProject/getEmployee";

  private get createUrl() { return this.configurations.baseUrl + this._createUrl; }
  private get listUrl() { return this.configurations.baseUrl + this._listUrl; }
  private get updateUrl() { return this.configurations.baseUrl + this._updateUrl; }
  private get deleteUrl() { return this.configurations.baseUrl + this._deleteUrl; }
  private get listProjectUrl() { return this.configurations.baseUrl + this._listProjectUrl; }
  private get assignProjectUrl() { return this.configurations.baseUrl + this._assignProjectUrl; }
  private get getAssignProjectUrl() { return this.configurations.baseUrl + this._getAssignProjectUrl; }
  private get getEmployeeByProjectIdUrl() { return this.configurations.baseUrl + this._getEmployeeByProjectIdUrl; }

  constructor(http: HttpClient, configurations: ConfigurationService, injector: Injector) {
    super(http, configurations, injector);
  }

  getAllProject(page?: number, pageSize?: number, name?: string): Observable<any> {
    let endpointUrl = `${this.listUrl}/${page}/${pageSize}/${name}`;
    return this.http.get(endpointUrl, this.getRequestHeaders()).catch(error => {
      return this.handleError(error, () => this.getAllProject(page, pageSize, name));
    })
  }

  create(project: ProjectModel): Observable<string> {
    return this.http.post(this.createUrl, JSON.stringify(project), this.getRequestHeaders()).catch(error => { return this.handleError(error, () => this.create(project)); })
  }

  update(project: any, projectId?: string) {
    let endpointUrl = projectId ? `${this.updateUrl}/${projectId}` : this.updateUrl;
    return this.http.put(endpointUrl, JSON.stringify(project), this.getRequestHeaders())
      .catch(error => {
        return this.handleError(error, () => this.update(project, projectId));
      });
  }

  delete(projectId: string) {
    let endpointUrl = `${this.deleteUrl}/${projectId}`;
    return this.http.delete(endpointUrl, this.getRequestHeaders())
      .catch(error => {
        return this.handleError(error, () => this.delete(projectId));
      });
  }
  getAllProjectList(page?: number, pageSize?: number, name?: string): Observable<any> {
    let endpointUrl = `${this.listProjectUrl}/${page}/${pageSize}/${name}`;
    return this.http.get(endpointUrl, this.getRequestHeaders()).catch(error => {
      return this.handleError(error, () => this.getAllProjectList(page, pageSize, name));
    })
  }

  assignProject(project: TimesheetAssignProject): Observable<string> {
    return this.http.post(this.assignProjectUrl, JSON.stringify(project), this.getRequestHeaders()).catch(error => { return this.handleError(error, () => this.assignProject(project)); })
  }

  getAssignProject(page?: number, pageSize?: number, name?: string): Observable<any> {
    let endpointUrl = `${this.getAssignProjectUrl}/${page}/${pageSize}/${name}`;
    return this.http.get(endpointUrl, this.getRequestHeaders()).catch(error => {
      return this.handleError(error, () => this.getAssignProject(page, pageSize, name));
    })
  }

  getEmployeeByProjectId(id: string, page?: number, pageSize?: number, name?: string): Observable<any> {
    let endpointUrl = `${this.getEmployeeByProjectIdUrl}/${id}/${page}/${pageSize}/${name}`;
    return this.http.get(endpointUrl, this.getRequestHeaders()).catch(error => {
      return this.handleError(error, () => this.getEmployeeByProjectId(id, page, pageSize, name));
    })
  }

}
