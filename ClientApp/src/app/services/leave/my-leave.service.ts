import { Injectable, Injector } from '@angular/core';
import { EndpointFactory } from '../common/endpoint-factory.service';
import { HttpClient } from '../../../../node_modules/@angular/common/http';
import { ConfigurationService } from '../common/configuration.service';
import { Observable } from '../../../../node_modules/rxjs';
import { LeaveApply } from '../../models/leave/leave-apply.model';

@Injectable()
export class MyLeaveService extends EndpointFactory {

   
  private readonly _getAttendanceUrl: string = "/api/ExcelUpload/empMonthlyDetail";
  private readonly _getLeaveInfoUrl: string = "/api/LeaveManagement/listLeaveInfo";
  private readonly _creatEntitlementUrl: string = "/api/LeaveManagement/createEntitlement";
  private readonly _createLeaveUrl: string = "/api/LeaveManagement/createLeave";
  private readonly _getAllEmployeeLeaveUrl: string = "/api/LeaveManagement/list";
  private readonly _getEmployeeLeaveDeatilUrl: string = "/api/LeaveManagement/leaveDetail";
  private readonly _getCalenderUrl: string = "/api/LeaveManagement/calenders";
  private readonly _retractLeaveUrl: string = "/api/LeaveManagement/retract";
  private readonly _cancelLeaveUrl: string = "/api/LeaveManagement/cancel";
  private readonly _checkConfigUrl: string = "/api/LeaveManagement/check";

  public get getAttendanceUrl() { return this.configurations.baseUrl + this._getAttendanceUrl; }
  public get getLeaveInfoUrl() { return this.configurations.baseUrl + this._getLeaveInfoUrl; }
  public get creatEntitlementUrl() { return this.configurations.baseUrl + this._creatEntitlementUrl; }
  public get createLeaveUrl() { return this.configurations.baseUrl + this._createLeaveUrl; }
  public get getAllEmployeeLeaveUrl() { return this.configurations.baseUrl + this._getAllEmployeeLeaveUrl };
  public get getEmployeeLeaveDeatilUrl() { return this.configurations.baseUrl + this._getEmployeeLeaveDeatilUrl }
  public get getCalenderUrl() { return this.configurations.baseUrl + this._getCalenderUrl; }
  public get retractLeaveUrl() { return this.configurations.baseUrl + this._retractLeaveUrl; }
  public get cancelLeaveUrl() { return this.configurations.baseUrl + this._cancelLeaveUrl; }
  public get checkConfigUrl() { return this.configurations.baseUrl + this._checkConfigUrl; }
  constructor(http: HttpClient, configurations: ConfigurationService, injector: Injector) {
    super(http, configurations, injector);

  }
    getAttendanceInfo(id: string): Observable<any> {
        
        let endpointUrl = `${this.getAttendanceUrl}/${id}`;
        return this.http.get(endpointUrl, this.getRequestHeaders()).catch(error => {
            return this.handleError(error, () => this.getAttendanceInfo(id));
        })
    }
  getAllLeaveInfo(id: string): Observable<any> {
    let endpointUrl = `${this.getLeaveInfoUrl}/${id}`;
    return this.http.get(endpointUrl, this.getRequestHeaders()).catch(error => {
      return this.handleError(error, () => this.getAllLeaveInfo(id));
    })
  }

  creatEntitlement(id: string): Promise<any> {
    let endpointUrl = `${this.creatEntitlementUrl}/${id}`;
    return this.http
      .get(endpointUrl, this.getRequestHeaders())
      .toPromise();
  }

  createLeave(leaveApply: LeaveApply): Observable<string> {
    return this.http.post(this.createLeaveUrl, JSON.stringify(leaveApply), this.getRequestHeaders())
      .catch(error => { return this.handleError(error, () => this.createLeave(leaveApply)); });
  }

  getAllEmployeeLeave(userId?: string, page?: number, pageSize?: number, name?: string): Observable<any> {
    let endpointUrl = `${this.getAllEmployeeLeaveUrl}/${userId}/${page}/${pageSize}/${name}`;
    return this.http.get(endpointUrl, this.getRequestHeaders()).catch(error => {
      return this.handleError(error, () => this.getAllEmployeeLeave(userId, page, pageSize, name));
    })
  }


  getEmployeeLeaveDetail(leaveDetailId?: string): Observable<any> {
    let endpointUrl = `${this.getEmployeeLeaveDeatilUrl}/${leaveDetailId}`;
    return this.http.get(endpointUrl, this.getRequestHeaders()).catch(error => {
      return this.handleError(error, () => this.getAllEmployeeLeave(leaveDetailId));
    })
  }

  getCalenders(userId: string): Observable<any> {
    let endpointUrl = `${this.getCalenderUrl}/${userId}`;
    return this.http.get(endpointUrl, this.getRequestHeaders()).catch(error => {
      return this.handleError(error, () => this.getCalenders(userId));
    })
  }

  retractLeave(leaveDetailId?: string): Observable<string> {
    let endpointUrl = `${this.retractLeaveUrl}/${leaveDetailId}`;
    return this.http.get(endpointUrl, this.getRequestHeaders())
      .catch(error => { return this.handleError(error, () => this.retractLeave(leaveDetailId)); });
  }

  cancelLeave(leaveDetailId?: string): Observable<string> {
    let endpointUrl = `${this.cancelLeaveUrl}/${leaveDetailId}`;
    return this.http.get(endpointUrl, this.getRequestHeaders())
      .catch(error => { return this.handleError(error, () => this.cancelLeave(leaveDetailId)); });
  }

  checkConfig(id: string) {
    let endpointUrl = `${this.checkConfigUrl}/${id}`;
    return this.http.get(endpointUrl, this.getRequestHeaders())
      .catch(error => { return this.handleError(error, () => this.checkConfig(id)); });
  }

}
