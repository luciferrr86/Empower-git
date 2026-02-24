import { Injectable, Injector } from '@angular/core';
import { EndpointFactory } from '../common/endpoint-factory.service';
import { HttpClient } from '@angular/common/http';
import { ConfigurationService } from '../common/configuration.service';
import { Observable } from 'rxjs';

@Injectable()
export class BulkInterviewScheduleService extends EndpointFactory {

  private readonly _getBulkScheduleList: string = "/api/MassScheduling/list"
  private readonly _getDateTimeUrl: string = "/api/MassScheduling/";
  private readonly _saveDateTimeUrl: string = "/api/MassScheduling/saveInterviewScheduling";
  private readonly _getJobUrl: string = "/api/MassScheduling/joblist";
  private readonly _getRoomsUrl: string = "/api/MassScheduling/roomlist";
  private readonly _saveRoomsUrl: string = "/api/MassScheduling/saveRoom";
  private readonly _getInterviewPanalUrl: string = "/api/MassScheduling/interviewPanel";
  private readonly _scheduleInterviewlUrl: string = "/api/MassScheduling/schedule";
  private readonly _saveInterviewPanalUrl: string = "/api/MassScheduling/saveInterviewPanel";
  private readonly _getDeleteBulkScheduleUrl: string = "/api/MassScheduling/delete"

  private get getBulkScheduleList() { return this.configurations.baseUrl + this._getBulkScheduleList; }
  private get getDateTimeUrl() { return this.configurations.baseUrl + this._getDateTimeUrl; }
  private get saveDateTimeUrl() { return this.configurations.baseUrl + this._saveDateTimeUrl; }
  private get getJobUrl() { return this.configurations.baseUrl + this._getJobUrl; }
  private get getRoomsUrl() { return this.configurations.baseUrl + this._getRoomsUrl; }
  private get saveRoomsUrl() { return this.configurations.baseUrl + this._saveRoomsUrl; }
  private get getInterviewPanalUrl() { return this.configurations.baseUrl + this._getInterviewPanalUrl; }
  private get scheduleInterviewlUrl() { return this.configurations.baseUrl + this._scheduleInterviewlUrl; }
  private get saveInterviewPanalUrl() { return this.configurations.baseUrl + this._saveInterviewPanalUrl; }
  private get getDeleteBulkScheduleUrl() { return this.configurations.baseUrl + this._getDeleteBulkScheduleUrl; }


  constructor(http: HttpClient, configurations: ConfigurationService, injector: Injector) {
    super(http, configurations, injector);
  }

  bulkScheduleList(page?: number, pageSize?: number, name?: string): Observable<any> {
    let endpointUrl = `${this.getBulkScheduleList}/${page}/${pageSize}/${name}`;
    return this.http.get(endpointUrl, this.getRequestHeaders()).catch(error => {
      return this.handleError(error, () => this.bulkScheduleList(page, pageSize, name));
    })
  }

  getDateTime() {
    let endpointUrl = `${this.getDateTimeUrl}`;
    return this.http.get(endpointUrl, this.getRequestHeaders()).catch(error => { return this.handleError(error, () => this.getDateTime()) });
  }
  saveDateTime(dateTime: any) {
    return this.http.post(this.saveDateTimeUrl, JSON.stringify(dateTime), this.getRequestHeaders()).catch(error => { return this.handleError(error, () => this.saveDateTime(dateTime)) });
  }
  getJob() {
    let endpointUrl = `${this.getJobUrl}`;
    return this.http.get(endpointUrl, this.getRequestHeaders()).catch(error => { return this.handleError(error, () => this.getJob()) });
  }
  getRooms() {
    let endpointUrl = `${this.getRoomsUrl}`;
    return this.http.get(endpointUrl, this.getRequestHeaders()).catch(error => { return this.handleError(error, () => this.getRooms()) });
  }
  saveRooms(room: any) {
    return this.http.post(this.saveRoomsUrl, JSON.stringify(room), this.getRequestHeaders()).catch(error => { return this.handleError(error, () => this.saveRooms(room)) });
  }
  getInterviewPanal(id: string) {
    let endpointUrl = `${this.getInterviewPanalUrl}/${id}`;
    return this.http.get(endpointUrl, this.getRequestHeaders()).catch(error => { return this.handleError(error, () => this.getInterviewPanal(id)) });
  }
  saveInterviewPanal(interviewPanal: any) {
    return this.http.post(this.saveInterviewPanalUrl, JSON.stringify(interviewPanal), this.getRequestHeaders()).catch(error => { return this.handleError(error, () => this.saveInterviewPanal(interviewPanal)) });
  }
  scheduleInterView(id: string) {
    let endpointUrl = `${this.scheduleInterviewlUrl}/${id}`;
    return this.http.get(endpointUrl, this.getRequestHeaders()).catch(error => { return this.handleError(error, () => this.getInterviewPanal(id)) });
  }
  deleteSchedule(id?: string) {
    let endpointUrl = `${this.getDeleteBulkScheduleUrl}/${id}`
    return this.http.delete(endpointUrl, this.getRequestHeaders()).catch(error => { return this.handleError(error, () => this.deleteSchedule(id)) });

  }

}
