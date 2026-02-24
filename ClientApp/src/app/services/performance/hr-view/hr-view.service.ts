import { Injectable, Injector } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { HttpClient } from '@angular/common/http';
import { EndpointFactory } from '../../common/endpoint-factory.service';
import { ConfigurationService } from '../../common/configuration.service';

@Injectable()
export class HrViewService extends EndpointFactory {
  private readonly _listManagerUrl: string = "/api/HrView/getAllManager";
  private readonly _listEmployeeUrl: string = "/api/HrView/getAllEmployee";
  private readonly _performanceInviteUrl: string = "/api/HrView/performanceInvitation";
  private readonly _hrViewPreCheckUrl: string = "/api/HrView/hrViewPreCheck";
  private readonly _reminderManagerInvitationUrl: string = "/api/HrView/managerInvitation";
  private readonly _reminderEmployeeInvitationUrl: string = "/api/HrView/employeeInvitation";

  private get listManagerUrl() { return this.configurations.baseUrl + this._listManagerUrl; }
  private get listEmployeeUrl() { return this.configurations.baseUrl + this._listEmployeeUrl; }
  private get inviteUrl() { return this.configurations.baseUrl + this._performanceInviteUrl; }
  private get preCheckUrl() { return this.configurations.baseUrl + this._hrViewPreCheckUrl; }
  private get reminderManagerInvitationUrl() { return this.configurations.baseUrl + this._reminderManagerInvitationUrl; }
  private get reminderEmployeeInvitationUrl() { return this.configurations.baseUrl + this._reminderEmployeeInvitationUrl; }


  constructor(http: HttpClient, configurations: ConfigurationService, injector: Injector) {
    super(http, configurations, injector);
  }

  getAllManager(page?: number, pageSize?: number, name?: string): Observable<any> {

    let endpointUrl = `${this.listManagerUrl}/${page}/${pageSize}/${name}`;
    return this.http.get(endpointUrl, this.getRequestHeaders()).catch(error => {
      return this.handleError(error, () => this.getAllManager(page, pageSize, name));
    })
  }

  hrViewPreCheck(): Observable<any> {
    let endpointUrl = `${this.preCheckUrl}`;
    return this.http.get(endpointUrl, this.getRequestHeaders()).catch(error => {
      return this.handleError(error, () => this.hrViewPreCheck());
    })
  }

  getAllEmployee(page?: number, pageSize?: number, name?: string): Observable<any> {

    let endpointUrl = `${this.listEmployeeUrl}/${page}/${pageSize}/${name}`;
    return this.http.get(endpointUrl, this.getRequestHeaders()).catch(error => {
      return this.handleError(error, () => this.getAllEmployee(page, pageSize, name));
    })
  }

  performanceInvitation() {
    let endpointUrl = `${this.inviteUrl}`;
    return this.http.get(endpointUrl, this.getRequestHeaders()).catch(error => {
      return this.handleError(error, () => this.performanceInvitation());
    });
  }

  reminderManagerInvitation(id: string) {
    let endpointUrl = `${this.reminderManagerInvitationUrl}/${id}`
    return this.http.get(endpointUrl, this.getRequestHeaders()).catch(error => {
      return this.handleError(error, () => this.reminderManagerInvitation(id));
    })
  }
  reminderEmployeeInvitation(id: string) {
    let endpointUrl = `${this.reminderEmployeeInvitationUrl}/${id}`
    return this.http.get(endpointUrl, this.getRequestHeaders()).catch(error => {
      return this.handleError(error, () => this.reminderEmployeeInvitation(id));
    })
  }
}


