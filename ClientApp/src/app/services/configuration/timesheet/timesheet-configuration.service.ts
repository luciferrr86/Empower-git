import { Injectable, Injector } from '@angular/core';
import { EndpointFactory } from '../../common/endpoint-factory.service';
import { HttpClient } from '@angular/common/http';
import { ConfigurationService } from '../../common/configuration.service';
import { TimesheetConfiguration } from '../../../models/configuration/timesheet/timesheet-configuration.model';
import { Observable } from 'rxjs';

@Injectable()
export class TimesheetConfigurationService extends EndpointFactory {

  private readonly _getConfigurationUrl: string = "/api/TimeSheetConfiguration/getAll";
  private readonly _createUrl: string = "/api/TimeSheetConfiguration/create";
  private readonly _updateUrl: string = "/api/TimeSheetConfiguration/update";

  private get getConfigurationUrl() { return this.configurations.baseUrl + this._getConfigurationUrl; }
  private get createUrl() { return this.configurations.baseUrl + this._createUrl; }
  private get updateUrl() { return this.configurations.baseUrl + this._updateUrl; }

  constructor(http: HttpClient, configurations: ConfigurationService, injector: Injector) {
    super(http, configurations, injector);
  }

  getconfiguration() {
    let endpointUrl = `${this.getConfigurationUrl}`;
    return this.http.get(endpointUrl, this.getRequestHeaders()).catch(error => {
      return this.handleError(error, () => this.getconfiguration());
    });
  }

  create(configuration: TimesheetConfiguration): Observable<string> {
    return this.http.post(this.createUrl, JSON.stringify(configuration), this.getRequestHeaders()).catch(error => {
      return this.handleError(error, () => this.create(configuration));
    });
  }
  update(configuration: any, id?: string) {
    let endpointUrl = id ? `${this.updateUrl}/${id}` : this.updateUrl;
    return this.http.put(endpointUrl, JSON.stringify(configuration), this.getRequestHeaders()).catch(error => {
      return this.handleError(error, () => this.update(configuration, id));
    });
  }

}
