import { Injectable, Injector } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ConfigurationService } from '../common/configuration.service';
import { EndpointFactory } from '../common/endpoint-factory.service';
import { Observable } from 'rxjs';

@Injectable()
export class ManageInterviewService extends EndpointFactory {

  private readonly _getShortListCandidateUrl: string = "/api/JobCandidateProfile/managerList";
  private readonly _getCandidateDetailUrl: string = "/api/CandidateInterViewSchedule/interviewcandidateDetail";
  private readonly _managerKpiUrl: string = "/api/CandidateInterViewSchedule/saveskillkpi";

  private get getShortListCandidateUr() { return this.configurations.baseUrl + this._getShortListCandidateUrl; }
  private get getCandidateDetailUrl() { return this.configurations.baseUrl + this._getCandidateDetailUrl; }
  private get managerKpiUrl() { return this.configurations.baseUrl + this._managerKpiUrl; }

  constructor(http: HttpClient, configurations: ConfigurationService, injector: Injector) {
    super(http, configurations, injector);
  }

  getShortListCandidate(id?: string, page?: number, pageSize?: number, name?: string): Observable<any> {
    let endpointUrl = id ? `${this.getShortListCandidateUr}/${id}/${page}/${pageSize}/${name}` : this.getCandidateDetailUrl;
    return this.http.get(endpointUrl, this.getRequestHeaders()).catch(error => { return this.handleError(error, () => this.getShortListCandidate(id, page, pageSize, name)) });
  }

  getCandidateJobDetail(id?: string) {
    let endpointUrl = id ? `${this.getCandidateDetailUrl}/${id}` : this.getShortListCandidateUr;
    return this.http.get(endpointUrl, this.getRequestHeaders()).catch(error => { return this.handleError(error, () => this.getCandidateJobDetail(id)) });
  }

  saveManagerKpi(managerKpi: any) {
    return this.http.post(this.managerKpiUrl, JSON.stringify(managerKpi), this.getRequestHeaders()).catch(error => { return this.handleError(error, () => this.saveManagerKpi(managerKpi)) });
  }

}
