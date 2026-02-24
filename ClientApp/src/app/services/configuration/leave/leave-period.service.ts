import { Injectable, Injector } from '@angular/core';
import { EndpointFactory } from '../../common/endpoint-factory.service';
import { HttpClient } from '@angular/common/http';
import { ConfigurationService } from '../../common/configuration.service';
import { Observable } from 'rxjs';
import { LeavePeriod } from '../../../models/configuration/leave/leave-period.model';

@Injectable()
export class LeavePeriodService extends EndpointFactory {

  private readonly _getLeavePeriodUrl: string = "/api/LeavePeriod/list";
  private readonly _createLeavePeriodUrl: string = "/api/LeavePeriod/create";
  private readonly _updateLeavePeriodUrl: string = "/api/LeavePeriod/update";
  private readonly _deleteLeavePeriodUrl: string = "/api/LeavePeriod/delete";

  private get getUrl() { return this.configurations.baseUrl + this._getLeavePeriodUrl; }
  private get createUrl() { return this.configurations.baseUrl + this._createLeavePeriodUrl; }
  private get updateUrl() { return this.configurations.baseUrl + this._updateLeavePeriodUrl; }
  private get deleteurl() { return this.configurations.baseUrl + this._deleteLeavePeriodUrl; }

  constructor(http: HttpClient, configurations: ConfigurationService, injector: Injector) {
    super(http, configurations, injector);
  }


  getAllLeavePeriod(page?: number, pageSize?: number, name?: string): Observable<any> {
    let endpointUrl = `${this.getUrl}/${page}/${pageSize}/${name}`;
    return this.http.get(endpointUrl, this.getRequestHeaders()).catch(error => {
      return this.handleError(error, () => this.getAllLeavePeriod(page, pageSize, name));
    })
  }

  create(leavePeriod: LeavePeriod): Observable<string> {
    return this.http.post(this.createUrl, JSON.stringify(leavePeriod), this.getRequestHeaders()).catch(error => { return this.handleError(error, () => this.create(leavePeriod)); });
  }

  update(leavePeriod: any, periodId?: string) {
    let endpointUrl = periodId ? `${this.updateUrl}/${periodId}` : this.updateUrl;
    return this.http.put(endpointUrl, JSON.stringify(leavePeriod), this.getRequestHeaders()).catch(error => {
      return this.handleError(error, () => this.update(leavePeriod, periodId));
    });
  }

  delete(periodId: string) {
    let endpointUrl = `${this.deleteurl}/${periodId}`;
    return this.http.delete(endpointUrl, this.getRequestHeaders()).catch(error => {
      return this.handleError(error, () => this.delete(periodId));
    });
  }
}
