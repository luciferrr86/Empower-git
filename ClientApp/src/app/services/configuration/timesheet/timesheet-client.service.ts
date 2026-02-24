import { Injectable, Injector } from '@angular/core';
import { EndpointFactory } from '../../common/endpoint-factory.service';
import { HttpClient } from '@angular/common/http';
import { ConfigurationService } from '../../common/configuration.service';
import { ClientModel } from '../../../models/configuration/timesheet/timesheet-client.model';
import { Observable } from 'rxjs';

@Injectable()
export class TimesheetClientService extends EndpointFactory {

  private readonly _listUrl: string = "/api/TimeSheetClient/list";
  private readonly _createUrl: string = "/api/TimeSheetClient/create";
  private readonly _updateUrl: string = "/api/TimeSheetClient/update";
  private readonly _deleteUrl: string = "/api/TimeSheetClient/delete";

  private get createUrl() { return this.configurations.baseUrl + this._createUrl; }
  private get listUrl() { return this.configurations.baseUrl + this._listUrl; }
  private get updateUrl() { return this.configurations.baseUrl + this._updateUrl; }
  private get deleteUrl() { return this.configurations.baseUrl + this._deleteUrl; }

  constructor(http: HttpClient, configurations: ConfigurationService, injector: Injector) {
    super(http, configurations, injector);
  }

  getAllClient(page?: number, pageSize?: number, name?: string): Observable<any> {
    let endpointUrl = `${this.listUrl}/${page}/${pageSize}/${name}`;
    return this.http.get(endpointUrl, this.getRequestHeaders()).catch(error => {
      return this.handleError(error, () => this.getAllClient(page, pageSize, name));
    })
  }

  create(client: ClientModel): Observable<string> {
    return this.http.post(this.createUrl, JSON.stringify(client), this.getRequestHeaders()).catch(error => { return this.handleError(error, () => this.create(client)); })
  }

  update(client: any, clientId?: string) {
    let endpointUrl = clientId ? `${this.updateUrl}/${clientId}` : this.updateUrl;
    return this.http.put(endpointUrl, JSON.stringify(client), this.getRequestHeaders())
      .catch(error => {
        return this.handleError(error, () => this.update(client, clientId));
      });
  }

  delete(clientId: string) {
    let endpointUrl = `${this.deleteUrl}/${clientId}`;
    return this.http.delete(endpointUrl, this.getRequestHeaders())
      .catch(error => {
        return this.handleError(error, () => this.delete(clientId));
      });
  }
}
