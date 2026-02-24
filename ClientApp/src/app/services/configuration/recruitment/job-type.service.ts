import { Injectable, Injector } from '@angular/core';
import { ConfigurationService } from '../../common/configuration.service';
import { Observable } from 'rxjs/Observable';
import { HttpClient } from '@angular/common/http';
import { EndpointFactory } from '../../common/endpoint-factory.service';
import { JobType } from '../../../models/configuration/recruitment/job-type.model';


@Injectable()
export class JobTypeService extends EndpointFactory {
  private readonly _createUrl: string = "/api/JobType/create";
  private readonly _listUrl: string = "/api/JobType/list";
  private readonly _getUrl: string = "/api/JobType/jobtype";
  private readonly _updateUrl: string = "/api/JobType/update";
  private readonly _deleteUrl: string = "/api/JobType/delete";
  private get createUrl() { return this.configurations.baseUrl + this._createUrl; }
  private get listUrl() { return this.configurations.baseUrl + this._listUrl; }
  private get getUrl() { return this.configurations.baseUrl + this._getUrl; }
  private get updateUrl() { return this.configurations.baseUrl + this._updateUrl; }
  private get deleteUrl() { return this.configurations.baseUrl + this._deleteUrl; }

  constructor(http: HttpClient, configurations: ConfigurationService, injector: Injector) {
    super(http, configurations, injector);
  }

  create(jobType: JobType): Observable<string> {
    return this.http.post(this.createUrl, JSON.stringify(jobType), this.getRequestHeaders()).catch(error => { return this.handleError(error, () => this.create(jobType)); })
  }


  getAllJobType(page?: number, pageSize?: number, name?: string): Observable<any> {

    let endpointUrl = `${this.listUrl}/${page}/${pageSize}/${name}`;
    return this.http.get(endpointUrl, this.getRequestHeaders()).catch(error => {
      return this.handleError(error, () => this.getAllJobType(page, pageSize, name));
    })
  }

  update(jobTypeObject: any, jobTypeId?: string) {
    let endpointUrl = jobTypeId ? `${this.updateUrl}/${jobTypeId}` : this.updateUrl;
    return this.http.put(endpointUrl, JSON.stringify(jobTypeObject), this.getRequestHeaders())
      .catch(error => {
        return this.handleError(error, () => this.update(jobTypeObject, jobTypeId));
      });
  }

  delete(jobTypeId: string | JobType) {
    let endpointUrl = `${this.deleteUrl}/${jobTypeId}`;
    return this.http.delete(endpointUrl, this.getRequestHeaders())
      .catch(error => {
        return this.handleError(error, () => this.delete(jobTypeId));
      });
  }
}


