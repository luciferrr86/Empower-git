import { Injectable, Injector } from '@angular/core';
import { EndpointFactory } from '../../common/endpoint-factory.service';
import { HttpClient } from '@angular/common/http';
import { ConfigurationService } from '../../common/configuration.service';
import { Observable } from 'rxjs';
import { LeaveType } from '../../../models/configuration/leave/leave-type.model';

@Injectable()
export class LeaveTypeService extends EndpointFactory {

  private readonly _getAllLeaveTypeUrl: string = "/api/LeaveType/list";
  private readonly _createUrl: string = "/api/LeaveType/create";
  private readonly _updateUrl: string = "/api/LeaveType/update";
  private readonly _deleteUrl: string = "/api/LeaveType/delete";

  public get getUrl() { return this.configurations.baseUrl + this._getAllLeaveTypeUrl; }
  public get createUrl() { return this.configurations.baseUrl + this._createUrl; }
  public get updateUrl() { return this.configurations.baseUrl + this._updateUrl; }
  public get deleteUrl() { return this.configurations.baseUrl + this._deleteUrl; }

  constructor(http: HttpClient, configurations: ConfigurationService, injector: Injector) {
    super(http, configurations, injector);
  }

  getAllLeaveType(page?: number, pageSize?: number, name?: string): Observable<any> {
    let endpointUrl = `${this.getUrl}/${page}/${pageSize}/${name}`;
    return this.http.get(endpointUrl, this.getRequestHeaders()).catch(error => {
      return this.handleError(error, () => this.getAllLeaveType(page, pageSize, name));
    })
  }

  create(leaveType: LeaveType): Observable<string> {
    return this.http.post(this.createUrl, JSON.stringify(leaveType), this.getRequestHeaders())
      .catch(error => { return this.handleError(error, () => this.create(leaveType)); });
  }


  update(leaveType: any, leaveTypeId?: string) {
    let endpointUrl = leaveTypeId ? `${this.updateUrl}/${leaveTypeId}` : this.updateUrl;
    return this.http.put(endpointUrl, JSON.stringify(leaveType), this.getRequestHeaders()).catch(error => {
      return this.handleError(error, () => this.update(leaveType, leaveTypeId));
    });
  }

  delete(leaveTypeId?: string) {
    let endpointUrl = `${this.deleteUrl}/${leaveTypeId}`;
    return this.http.delete(endpointUrl, this.getRequestHeaders()).catch(error => {
      return this.handleError(error, () => this.delete(leaveTypeId));
    });
  }

}
