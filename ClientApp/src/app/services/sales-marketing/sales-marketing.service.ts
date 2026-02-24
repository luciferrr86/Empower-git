import { Injectable, Injector } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ConfigurationService } from '../common/configuration.service';
import { EndpointFactory } from '../common/endpoint-factory.service';
import { Observable } from 'rxjs';
import { Company } from '../../models/sales-marketing/company-view.model';

@Injectable()
export class SalesMarketingService extends EndpointFactory {

  private readonly _companylistUrl: string = "/api/SalesMarketing/list";
  private readonly _companyUrl: string = "/api/SalesMarketing/company";
  private readonly _createCompanyUrl: string = "/api/SalesMarketing/createCompany";
  private readonly _deleteCompanyUrl: string = "/api/SalesMarketing/delete";
  private readonly _deleteCompanyContactUrl: string = "/api/SalesMarketing/deletecontact";
  private readonly _updateCompanyUrl: string = "/api/SalesMarketing/update";

  private readonly _statusListUrl: string = "/api/SalesMarketing/statusList";
  private readonly _createStatusUrl: string = "/api/SalesMarketing/createStatus";

  private readonly _companyMeetinglistUrl: string = "/api/Schedule/list";
  private readonly _meetingScheduleUrl: string = "/api/Schedule/meetingschedule";
  private readonly _meetingMomUrl: string = "/api/Schedule/getMom";
  private readonly _saveMomUrl: string = "/api/Schedule/saveMom";
  private readonly _saveMeetingScheduleUrl: string = "/api/Schedule/saveSchedule";


  private get companylistUrl() { return this.configurations.baseUrl + this._companylistUrl; }
  private get companyDetailUrl() { return this.configurations.baseUrl + this._companyUrl; }
  private get saveCompanyUrl() { return this.configurations.baseUrl + this._createCompanyUrl; }
  private get updateCompanyUrl() { return this.configurations.baseUrl + this._updateCompanyUrl; }
  private get deleteCompanyUrl() { return this.configurations.baseUrl + this._deleteCompanyUrl; }
  private get deleteCompanyContactUrl() { return this.configurations.baseUrl + this._deleteCompanyContactUrl; }

  private get statuslistUrl() { return this.configurations.baseUrl + this._statusListUrl; }
  private get saveStatusUrl() { return this.configurations.baseUrl + this._createStatusUrl; }

  private get companyMettingListUrl() { return this.configurations.baseUrl + this._companyMeetinglistUrl; }
  private get meetingScheduleUrl() { return this.configurations.baseUrl + this._meetingScheduleUrl; }
  private get meetingMomUrl() { return this.configurations.baseUrl + this._meetingMomUrl; }
  private get saveMomUrl() { return this.configurations.baseUrl + this._saveMomUrl; }
  private get saveMeetingScheduleUrl() { return this.configurations.baseUrl + this._saveMeetingScheduleUrl; }

  constructor(http: HttpClient, configurations: ConfigurationService, injector: Injector) {
    super(http, configurations, injector);
  }

  /*Company Services*/
  getAllCompanylist(page?: number, pageSize?: number, name?: string): Observable<any> {
    let endpointUrl = `${this.companylistUrl}/${page}/${pageSize}/${name}`;
    return this.http.get(endpointUrl, this.getRequestHeaders()).
      catch(error => {
        return this.handleError(error, () => this.getAllCompanylist(page, pageSize, name));
      });
  }
  getCompany(id: string) {
    let endpointUrl = `${this.companyDetailUrl}/${id}`;
    return this.http.get(endpointUrl, this.getRequestHeaders()).catch(error => {
      return this.handleError(error, () => this.getCompany(id));
    });
  }
  saveCompany(companyObject: Company) {
    let endpointUrl = `${this.saveCompanyUrl}`;
    return this.http.post(endpointUrl, JSON.stringify(companyObject), this.getRequestHeaders())
      .catch(error => {
        return this.handleError(error, () => this.saveCompany(companyObject));
      });
  }
  updateCompany(id: String, companyObject: Company) {
    let endpointUrl = `${this.updateCompanyUrl}/${id}`;
    return this.http.put(endpointUrl, JSON.stringify(companyObject), this.getRequestHeaders())
      .catch(error => {
        return this.handleError(error, () => this.updateCompany(id, companyObject));
      });
  }

  deleteCompany(companyId: string) {
    let endpointUrl = `${this.deleteCompanyUrl}/${companyId}`;
    return this.http.delete(endpointUrl, this.getRequestHeaders())
      .catch(error => {
        return this.handleError(error, () => this.deleteCompany(companyId));
      });
  }
  deleteCompanyContact(id: string) {
    let endpointUrl = `${this.deleteCompanyContactUrl}/${id}`;
    return this.http.delete(endpointUrl, this.getRequestHeaders())
      .catch(error => {
        return this.handleError(error, () => this.deleteCompanyContact(id));
      });
  }

  /*Status Services */

  getCompnayStatus(companyId: String): Observable<any> {
    let endpointUrl = `${this.statuslistUrl}/${companyId}`;
    return this.http.get(endpointUrl, this.getRequestHeaders()).
      catch(error => {
        return this.handleError(error, () => this.getCompnayStatus(companyId));
      });
  }

  saveStatus(object: any) {
    let endpointUrl = `${this.saveStatusUrl}`;
    return this.http.post(endpointUrl, JSON.stringify(object), this.getRequestHeaders())
      .catch(error => {
        return this.handleError(error, () => this.saveStatus(object));
      });
  }
  /*Meeting Schedule Services */

  getCompanyMeetinglist(id: String, page?: number, pageSize?: number, name?: string): Observable<any> {
    let endpointUrl = `${this.companyMettingListUrl}/${id}/${page}/${pageSize}/${name}`;
    return this.http.get(endpointUrl, this.getRequestHeaders()).
      catch(error => {
        return this.handleError(error, () => this.getCompanyMeetinglist(id, page, pageSize, name));
      });
  }
  getMeetingSchedule(id: string) {
    let endpointUrl = `${this.meetingScheduleUrl}/${id}`;
    return this.http.get(endpointUrl, this.getRequestHeaders()).catch(error => {
      return this.handleError(error, () => this.getMeetingSchedule(id));
    });
  }
  getMeetingMom(id: string) {
    let endpointUrl = `${this.meetingMomUrl}/${id}`;
    return this.http.get(endpointUrl, this.getRequestHeaders()).catch(error => {
      return this.handleError(error, () => this.getMeetingMom(id));
    });
  }
  saveMeetingMom(object: any) {
    let endpointUrl = `${this.saveMomUrl}`;
    return this.http.post(endpointUrl, JSON.stringify(object), this.getRequestHeaders())
      .catch(error => {
        return this.handleError(error, () => this.saveMeetingMom(object));
      });
  }
  saveMeetingSchedule(object: any) {
    let endpointUrl = `${this.saveMeetingScheduleUrl}`;
    return this.http.post(endpointUrl, JSON.stringify(object), this.getRequestHeaders())
      .catch(error => {
        return this.handleError(error, () => this.saveMeetingSchedule(object));
      });
  }
}
