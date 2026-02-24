import { Injectable, Injector } from '@angular/core';
import { EndpointFactory } from '../common/endpoint-factory.service';
import { ConfigurationService } from '../common/configuration.service';
import { HttpClient } from '@angular/common/http';
import { FunctionalGroup } from '../../models/maintenance/functional-group.model';
import { Observable } from 'rxjs';
@Injectable()
export class GroupService extends EndpointFactory {

  private readonly _groupUrl: string = "/api/Group/groupList";
  private readonly _getGroupUrl: string = "/api/Group/getById";
  private readonly _createGroupUrl: string = "/api/Group/create";
  private readonly _updateGroupUrl: string = "/api/Group/update";
  private readonly _deleteGroupUrl: string = "/api/Group/delete";

  private get groupUrl() { return this.configurations.baseUrl + this._groupUrl; }
  private get getUrl() { return this.configurations.baseUrl + this._getGroupUrl; }
  private get createUrl() { return this.configurations.baseUrl + this._createGroupUrl; }
  private get updateUrl() { return this.configurations.baseUrl + this._updateGroupUrl; }
  private get deleteUrl() { return this.configurations.baseUrl + this._deleteGroupUrl; }
  constructor(http: HttpClient, configurations: ConfigurationService, injector: Injector) {

    super(http, configurations, injector);
  }
  getAllGroup(page?: number, pageSize?: number, name?: string): Observable<any> {
    let endpointUrl = `${this.groupUrl}/${page}/${pageSize}/${name}`;
    return this.http.get(endpointUrl, this.getRequestHeaders()).
      catch(error => {
        return this.handleError(error, () => this.getAllGroup(page, pageSize, name));
      });
  }
  create(groupObject: any) {
    return this.http.post(this.createUrl, JSON.stringify(groupObject), this.getRequestHeaders()).
      catch(error => {
        return this.handleError(error, () => this.create(groupObject));
      });
  }
  update(groupObject: any, groupId?: string) {

    let endpointUrl = groupId ? `${this.updateUrl}/${groupId}` : this.updateUrl;
    return this.http.put(endpointUrl, JSON.stringify(groupObject), this.getRequestHeaders()).
      catch(error => {
        return this.handleError(error, () => this.update(groupObject, groupId));
      });
  }
  delete(groupId?: string | FunctionalGroup) {
    let endpoint = `${this.deleteUrl}/${groupId}`
    return this.http.delete(endpoint, this.getRequestHeaders()).
      catch(error => {
        return this.handleError(error, () => this.delete(groupId));
      })
  }
}
