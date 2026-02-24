import { Injectable, Injector } from '@angular/core';
import { EndpointFactory } from '../common/endpoint-factory.service';
import { HttpClient } from '../../../../node_modules/@angular/common/http';
import { ConfigurationService } from '../common/configuration.service';
import { Observable } from '../../../../node_modules/rxjs';
import { ManageLeaveDetail } from '../../models/leave/manage-leave-detail.model';

@Injectable()
export class ManageLeaveService extends EndpointFactory {

  private readonly _leaveUrl: string = "/api/SubOrdinateLeave/listemployee";
  private readonly _leaveDetailsUrl: string = "/api/SubOrdinateLeave/leaveDetails";
  private readonly _leavemanageListUrl: string = "/api/SubOrdinateLeave/list";
  private readonly _leavemanageleaveUrl: string = "/api/SubOrdinateLeave/subordinateleaveDetail";
  private readonly _leaveapproveReject: string = "/api/SubOrdinateLeave/approveReject";
  private readonly _leaveEmployeeConfigUrl: string = "/api/SubOrdinateLeave/checkAllConfig";

  private readonly _calendarEventUrl: string = "/api/SubOrdinateLeave/managercalender";

  private get leaveUrl() { return this.configurations.baseUrl + this._leaveUrl; }
  private get leaveDetailUrl() { return this.configurations.baseUrl + this._leaveDetailsUrl; }
  private get leavemanageListUrl() { return this.configurations.baseUrl + this._leavemanageListUrl }
  private get leavemanageleaveUrl() { return this.configurations.baseUrl + this._leavemanageleaveUrl }
  public get leaveapproveReject() { return this.configurations.baseUrl + this._leaveapproveReject; }
  public get leaveEmployeeConfigUrl() { return this.configurations.baseUrl + this._leaveEmployeeConfigUrl; }
  public get calendarEventUrl() { return this.configurations.baseUrl + this._calendarEventUrl; }

  constructor(http: HttpClient, configurations: ConfigurationService, injector: Injector) {
    super(http, configurations, injector);
  }
  getAllEmployees(userId?: string, page?: number, pageSize?: number, name?: string): Observable<any> {
    let endpointUrl = `${this.leaveUrl}/${userId}/${page}/${pageSize}/${name}`;
    return this.http.get(endpointUrl, this.getRequestHeaders()).
      catch(error => {
        return this.handleError(error, () => this.getAllEmployees(userId, page, pageSize, name));
      });
  }

  getLeaveDetails(employeeId: string) {
    let endpointUrl = `${this.leaveDetailUrl}/${employeeId}`;
    return this.http.get(endpointUrl, this.getRequestHeaders()).catch(error => {
      return this.handleError(error, () => this.getLeaveDetails(employeeId));
    });
  }

  getManageleaveList(userId?: string, page?: number, pageSize?: number, name?: string): Observable<any> {
    let endpointUrl = `${this.leavemanageListUrl}/${userId}/${page}/${pageSize}/${name}`;
    return this.http.get(endpointUrl, this.getRequestHeaders()).
      catch(error => {
        return this.handleError(error, () => this.getManageleaveList(userId, page, pageSize, name));
      });
  }

  getmanageleave(leaveDetailId?: string): Observable<any> {
    let endpointUrl = `${this.leavemanageleaveUrl}/${leaveDetailId}`;
    return this.http.get(endpointUrl, this.getRequestHeaders()).catch(error => {
      return this.handleError(error, () => this.getmanageleave(leaveDetailId));
    })
  }

  updateLeave(manageLeaveDetail: ManageLeaveDetail): Observable<string> {

    return this.http.post(this.leaveapproveReject, JSON.stringify(manageLeaveDetail), this.getRequestHeaders())
      .catch(error => { return this.handleError(error, () => this.updateLeave(manageLeaveDetail)); });
  }

  checkConfig(employeeId: string): Observable<string> {
    let endpointUrl = `${this.leaveEmployeeConfigUrl}/${employeeId}`;
    return this.http.get(endpointUrl, this.getRequestHeaders()).catch(error => {
      return this.handleError(error, () => this.checkConfig(employeeId));
    });
  }

  getCalendersManager(id: string): Observable<any> {
    let endpointUrl = `${this.calendarEventUrl}/${id}`;
    return this.http.get(endpointUrl, this.getRequestHeaders()).catch(error => {
      return this.handleError(error, () => this.getCalendersManager(id));
    })
  }

}
