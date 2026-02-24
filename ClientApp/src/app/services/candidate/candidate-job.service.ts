import { Injectable, Injector } from '@angular/core';
import { EndpointFactory } from '../common/endpoint-factory.service';
import { HttpClient } from '@angular/common/http';
import { ConfigurationService } from '../common/configuration.service';
import { Observable } from 'rxjs';

@Injectable()
export class CandidateJobService extends EndpointFactory {

  private readonly _getJodDetails: string = "/api/Job/details";
  private readonly _getApplicationData: string = "/api/Job/applicationData";
  private readonly _saveApplicationData: string = "/api/Job/saveApplication";
  private readonly _getJobList: string = "/api/JobCandidateProfile/appliedJob"

  private get getJodDetails() { return this.configurations.baseUrl + this._getJodDetails; }
  private get getApplicationData() { return this.configurations.baseUrl + this._getApplicationData; }
  private get saveApplicationData() { return this.configurations.baseUrl + this._saveApplicationData; }
  private get getJobLists() { return this.configurations.baseUrl + this._getJobList; }

  constructor(http: HttpClient, configurations: ConfigurationService, injector: Injector) {
    super(http, configurations, injector);
  }

  getJobDetail(jobId?: string) {
    let endpointUrl = jobId ? `${this.getJodDetails}/${jobId}` : this.getJodDetails;
    return this.http.get(endpointUrl, this.getRequestHeaders()).catch(error => {
      return this.handleError(error, () => this.getJobDetail(jobId));
    });
  }
  getApplication(jobId: string) {
    let endpointUrl = jobId ? `${this.getApplicationData}/${jobId}` : this.getApplicationData;
    return this.http.get(endpointUrl, this.getRequestHeaders()).catch(error => {
      return this.handleError(error, () => this.getApplication(jobId));
    });
  }
  saveApplication(applicationData: any) {
    return this.http.post(this.saveApplicationData, JSON.stringify(applicationData), this.getRequestHeaders()).catch(error => {
      return this.handleError(error, () => this.saveApplication(applicationData))
    });

  }
  getJobList(id?: string, page?: number, pageSize?: number, name?: string): Observable<any> {
    let endpointUrl = `${this.getJobLists}/${id}/${page}/${pageSize}/${name}`;
    return this.http.get(endpointUrl, this.getRequestHeaders()).catch(error => {
      return this.handleError(error, () => this.getJobList(id, page, pageSize, name));
    })
  }
}
