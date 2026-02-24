import { Injectable, Injector } from '@angular/core';
import { FunctionalDesignation } from '../../models/maintenance/functional-designation.model';
import { Observable } from 'rxjs/Observable';
import { HttpClient } from '@angular/common/http';
import { ConfigurationService } from '../common/configuration.service';
import { EndpointFactory } from '../common/endpoint-factory.service';

@Injectable()
export class FunctionalDesignationService extends EndpointFactory {
  private readonly _createUrl: string = "/api/Designation/create";
  private readonly _listUrl: string = "/api/Designation/designationList";
  private readonly _getUrl: string = "/api/Designation/designation";
  private readonly _updateUrl: string = "/api/Designation/update";
  private readonly _deleteUrl: string = "/api/Designation/delete";
  private get createUrl() { return this.configurations.baseUrl + this._createUrl; }
  private get listUrl() { return this.configurations.baseUrl + this._listUrl; }
  private get getUrl() { return this.configurations.baseUrl + this._getUrl; }
  private get updateUrl() { return this.configurations.baseUrl + this._updateUrl; }
  private get deleteUrl() { return this.configurations.baseUrl + this._deleteUrl; }

  constructor(http: HttpClient, configurations: ConfigurationService, injector: Injector) {
    super(http, configurations, injector);
  }

  create(designation: FunctionalDesignation): Observable<string> {
    return this.http.post(this.createUrl, JSON.stringify(designation), this.getRequestHeaders()).catch(error => { return this.handleError(error, () => this.create(designation)); })
  }


  getAllDesignation(page?: number, pageSize?: number, name?: string): Observable<any> {

    let endpointUrl = `${this.listUrl}/${page}/${pageSize}/${name}`;
    return this.http.get(endpointUrl, this.getRequestHeaders()).catch(error => {
      return this.handleError(error, () => this.getAllDesignation(page, pageSize, name));
    })
  }

  update(designationObject: any, designationId?: string) {
    let endpointUrl = designationId ? `${this.updateUrl}/${designationId}` : this.updateUrl;
    return this.http.put(endpointUrl, JSON.stringify(designationObject), this.getRequestHeaders())
      .catch(error => {
        return this.handleError(error, () => this.update(designationObject, designationId));
      });
  }

  delete(designationId: string | FunctionalDesignation) {
    let endpointUrl = `${this.deleteUrl}/${designationId}`;
    return this.http.delete(endpointUrl, this.getRequestHeaders())
      .catch(error => {
        return this.handleError(error, () => this.delete(designationId));
      });
  }
}


