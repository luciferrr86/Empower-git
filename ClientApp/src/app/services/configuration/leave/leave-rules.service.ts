import { Injectable, Injector } from '@angular/core';
import { EndpointFactory } from '../../common/endpoint-factory.service';
import { HttpClient } from '@angular/common/http';
import { ConfigurationService } from '../../common/configuration.service';
import { Observable } from 'rxjs';
import { LeaveRules } from '../../../models/configuration/leave/leave-rules.model';

@Injectable()
export class LeaveRulesService extends EndpointFactory {

  private readonly _getLeaveRulesUrl: string = "/api/LeaveRules/list";
  private readonly _createUrl: string = "/api/LeaveRules/create";
  private readonly _updateUrl: string = "/api/LeaveRules/update";
  private readonly _deleteUrl: string = "/api/LeaveRules/delete";

  private get getUrl() { return this.configurations.baseUrl + this._getLeaveRulesUrl; }
  private get createUrl() { return this.configurations.baseUrl + this._createUrl; }
  private get updateUrl() { return this.configurations.baseUrl + this._updateUrl; }
  private get deleteUrl() { return this.configurations.baseUrl + this._deleteUrl; }

  constructor(http: HttpClient, configurations: ConfigurationService, injector: Injector) {
    super(http, configurations, injector);
  }

  getAllLeaveRules(page?: number, pageSize?: number, name?: string): Observable<any> {
    let endpointUrl = `${this.getUrl}/${page}/${pageSize}/${name}`;
    return this.http.get(endpointUrl, this.getRequestHeaders()).catch(error => {
      return this.handleError(error, () => this.getAllLeaveRules(page, pageSize, name));
    })
  }

  create(leaveRules: LeaveRules) {
    return this.http.post(this.createUrl, JSON.stringify(leaveRules), this.getRequestHeaders())
      .catch(error => { return this.handleError(error, () => this.create(leaveRules)); });
  }

  update(leaveRules: any, leaveRulesId?: string) {
    let endpointUrl = leaveRulesId ? `${this.updateUrl}/${leaveRulesId}` : this.updateUrl;
    return this.http.put(endpointUrl, JSON.stringify(leaveRules), this.getRequestHeaders())
      .catch(error => {
        return this.handleError(error, () => this.update(leaveRules, leaveRulesId));
      });
  }

  delete(leaveRulesId?: string) {
    let endpointUrl = `${this.deleteUrl}/${leaveRulesId}`;
    return this.http.delete(endpointUrl, this.getRequestHeaders()).catch(error => {
      return this.handleError(error, () => this.delete(leaveRulesId));
    });
  }

}
