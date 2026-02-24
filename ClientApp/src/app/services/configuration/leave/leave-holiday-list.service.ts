import { Injectable, Injector } from '@angular/core';
import { EndpointFactory } from '../../common/endpoint-factory.service';
import { HttpClient } from '@angular/common/http';
import { ConfigurationService } from '../../common/configuration.service';
import { Observable } from 'rxjs';
import { Holiday } from '../../../models/configuration/leave/leave-holiday.model';

@Injectable()
export class LeaveHolidayListService extends EndpointFactory {

  private readonly _getHolidayListUrl: string = "/api/LeaveHolidayList/list";
  private readonly _createUrl: string = "/api/LeaveHolidayList/create";
  private readonly _updateUrl: string = "/api/LeaveHolidayList/update"
  private readonly _deleteUrl: string = "/api/LeaveHolidayList/delete";

  private get getUrl() { return this.configurations.baseUrl + this._getHolidayListUrl; }
  private get createUrl() { return this.configurations.baseUrl + this._createUrl; }
  private get updateUrl() { return this.configurations.baseUrl + this._updateUrl; }
  private get deleteUrl() { return this.configurations.baseUrl + this._deleteUrl; }

  constructor(http: HttpClient, configurations: ConfigurationService, injector: Injector) {
    super(http, configurations, injector);
  }

  getAllHolidayList(page?: number, pageSize?: number, name?: string): Observable<any> {
    let endpointUrl = `${this.getUrl}/${page}/${pageSize}/${name}`;
    return this.http.get(endpointUrl, this.getRequestHeaders()).catch(error => {
      return this.handleError(error, () => this.getAllHolidayList(page, pageSize, name));
    })
  }

  create(holiday: Holiday): Observable<string> {
    return this.http.post(this.createUrl, JSON.stringify(holiday), this.getRequestHeaders()).
      catch(error => { return this.handleError(error, () => this.create(holiday)); });

  }

  update(hoilday: any, holidayId?: string) {
    let endpointUrl = holidayId ? `${this.updateUrl}/${holidayId}` : this.updateUrl;
    return this.http.put(endpointUrl, JSON.stringify(hoilday), this.getRequestHeaders()).catch(error => {
      return this.handleError(error, () => this.update(hoilday, holidayId));
    });
  }


  delete(holidayId: string) {
    let endpointUrl = `${this.deleteUrl}/${holidayId}`;
    return this.http.delete(endpointUrl, this.getRequestHeaders()).catch(error => {
      return this.handleError(error, () => this.delete(holidayId));
    });
  }
}
