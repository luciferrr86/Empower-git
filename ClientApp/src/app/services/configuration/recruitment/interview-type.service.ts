import { Injectable, Injector } from '@angular/core';
import { ConfigurationService } from '../../common/configuration.service';
import { Observable } from 'rxjs/Observable';
import { HttpClient } from '@angular/common/http';
import { EndpointFactory } from '../../common/endpoint-factory.service';
import { InterviewType } from '../../../models/configuration/recruitment/interview-type.model';


@Injectable()
export class InterviewTypeService extends EndpointFactory {
  private readonly _createUrl: string = "/api/JobInterviewType/create";
  private readonly _listUrl: string = "/api/JobInterviewType/interviewTypeList";
  private readonly _getUrl: string = "/api/JobInterviewType/interviewType";
  private readonly _updateUrl: string = "/api/JobInterviewType/update";
  private readonly _deleteUrl: string = "/api/JobInterviewType/delete";
  private get createUrl() { return this.configurations.baseUrl + this._createUrl; }
  private get listUrl() { return this.configurations.baseUrl + this._listUrl; }
  private get getUrl() { return this.configurations.baseUrl + this._getUrl; }
  private get updateUrl() { return this.configurations.baseUrl + this._updateUrl; }
  private get deleteUrl() { return this.configurations.baseUrl + this._deleteUrl; }

  constructor(http: HttpClient, configurations: ConfigurationService, injector: Injector) {
    super(http, configurations, injector);
  }

  create(interviewType: InterviewType): Observable<string> {
    return this.http.post(this.createUrl, JSON.stringify(interviewType), this.getRequestHeaders()).catch(error => { return this.handleError(error, () => this.create(interviewType)); })
  }


  getAllInterviewType(page?: number, pageSize?: number, name?: string): Observable<any> {

    let endpointUrl = `${this.listUrl}/${page}/${pageSize}/${name}`;
    return this.http.get(endpointUrl, this.getRequestHeaders()).catch(error => {
      return this.handleError(error, () => this.getAllInterviewType(page, pageSize, name));
    })
  }

  update(interviewTypeObject: any, interviewTypeId?: string) {
    let endpointUrl = interviewTypeId ? `${this.updateUrl}/${interviewTypeId}` : this.updateUrl;
    return this.http.put(endpointUrl, JSON.stringify(interviewTypeObject), this.getRequestHeaders())
      .catch(error => {
        return this.handleError(error, () => this.update(interviewTypeObject, interviewTypeId));
      });
  }

  delete(interviewTypeId: string | InterviewType) {
    let endpointUrl = `${this.deleteUrl}/${interviewTypeId}`;
    return this.http.delete(endpointUrl, this.getRequestHeaders())
      .catch(error => {
        return this.handleError(error, () => this.delete(interviewTypeId));
      });
  }
}


