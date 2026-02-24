import { Injectable, Injector } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ConfigurationService } from '../common/configuration.service';
import { EndpointFactory } from '../common/endpoint-factory.service';


@Injectable()
export class AdminConfigService extends EndpointFactory {
  private readonly _getUrl: string = "/api/Admin/getModule";
  private readonly _updateModuleUrl: string = "/api/Admin/module";
  private readonly _updateSubModuleUrl: string = "/api/Admin/submodule";
  private readonly _createClientUrl: string = "/api/Admin/createClient";
  private readonly _getClientUrl: string = "/api/Admin/clientDetails";

  private get getClientUrl() { return this.configurations.baseUrl + this._getClientUrl; }
  private get createClientUrl() { return this.configurations.baseUrl + this._createClientUrl; }
  private get getUrl() { return this.configurations.baseUrl + this._getUrl; }
  private get updateModuleUrl() { return this.configurations.baseUrl + this._updateModuleUrl; }
  private get updateSubModuleUrl() { return this.configurations.baseUrl + this._updateSubModuleUrl; }


  constructor(http: HttpClient, configurations: ConfigurationService, injector: Injector) {
    super(http, configurations, injector);
  }

  createClient(client: any) {
    return this.http.post(this.createClientUrl, JSON.stringify(client), this.getRequestHeaders()).catch(error => {
      return this.handleError(error, () => this.createClient(client));
    })
  }
  getClient() {
    let endpointUrl = `${this.getClientUrl}`;
    return this.http.get(endpointUrl, this.getRequestHeaders()).catch(error => {
      return this.handleError(error, () => this.getClient());
    })
  }
  getAdminSettings() {
    let endpointUrl = `${this.getUrl}`;
    return this.http.get(endpointUrl, this.getRequestHeaders()).catch(error => {
      return this.handleError(error, () => this.getAdminSettings());
    })
  }


  updateModule(togVal: boolean, id: string) {
    let endpointUrl = id ? `${this.updateModuleUrl}/${id}` : this.updateModuleUrl;
    return this.http.put(endpointUrl, togVal, this.getRequestHeaders())
      .catch(error => {
        return this.handleError(error, () => this.updateModule(togVal, id));
      });
  }

  updateSubModule(togVal: boolean, id: string) {
    let endpointUrl = id ? `${this.updateSubModuleUrl}/${id}` : this.updateSubModuleUrl;
    return this.http.put(endpointUrl, togVal, this.getRequestHeaders())
      .catch(error => {
        return this.handleError(error, () => this.updateSubModule(togVal, id));
      });
  }

}
