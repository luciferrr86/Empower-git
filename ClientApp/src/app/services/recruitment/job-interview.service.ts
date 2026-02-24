import { Injectable, Injector } from '@angular/core';
import { EndpointFactory } from '../common/endpoint-factory.service';
import { HttpClient } from '@angular/common/http';
import { ConfigurationService } from '../common/configuration.service';


@Injectable()
export class JobInterviewService extends EndpointFactory {

  private readonly _getJobInfoUrl: string = "/api/CandidateInterViewSchedule/applicationDetail";
  private readonly _getInterviewInfoUrl: string = "/api/CandidateInterViewSchedule/interviewdetail";
  private readonly _getInterviewFeedbackUrl: string = "/api/CandidateInterViewSchedule/interviewfeedback";
  private readonly _getInterviewDeleteUrl: string = "/api/CandidateInterViewSchedule/delete";
  private readonly _getHrKpiUrl: string = "/api/CandidateInterViewSchedule/";
  private readonly _hrKpiUrl: string = "/api/CandidateInterViewSchedule/savehrkpi";
  private readonly _getInterviewListUrl: string = "/api/CandidateInterViewSchedule/";
  private readonly _interviewScheduleUrl: string = "/api/CandidateInterViewSchedule/saveTimeSchedule";
   private readonly _completeLevel: string = "/api/CandidateInterViewSchedule/completelevel";
  private readonly _changeJobStatus: string = "/api/CandidateInterViewSchedule/jobStatus";

  private get getJobInfoUrl() { return this.configurations.baseUrl + this._getJobInfoUrl; }
  private get getInterviewInfoUrl() { return this.configurations.baseUrl + this._getInterviewInfoUrl; }
  private get getInterviewFeedbackUrl() { return this.configurations.baseUrl + this._getInterviewFeedbackUrl; }
  private get getInterviewDeleteUrl() { return this.configurations.baseUrl + this._getInterviewDeleteUrl; }
  private get hrKpiUrl() { return this.configurations.baseUrl + this._hrKpiUrl; }
  private get interviewScheduleUrl() { return this.configurations.baseUrl + this._interviewScheduleUrl; }
  private get changeJobStatus() { return this.configurations.baseUrl + this._changeJobStatus; }

  private get completeLevelUrl() { return this.configurations.baseUrl + this._completeLevel; }


  constructor(http: HttpClient, configurations: ConfigurationService, injector: Injector) {
    super(http, configurations, injector);
  }

  getJobInfo(jobId?: string) {
    let endpointUrl = jobId ? `${this.getJobInfoUrl}/${jobId}` : this.getJobInfoUrl;
    return this.http.get(endpointUrl, this.getRequestHeaders()).catch(error => { return this.handleError(error, () => this.getJobInfo(jobId)) });
  }
  getInterviewInfo(id?: string) {
    let endpointUrl = id ? `${this.getInterviewInfoUrl}/${id}` : this.getInterviewInfoUrl;
    return this.http.get(endpointUrl, this.getRequestHeaders()).catch(error => { return this.handleError(error, () => this.getInterviewInfo(id)) });
  }

  changeVacancyStatusForCandidate(id: string, statusId: number) {
    let endpointUrl = `${this.changeJobStatus}/${id}/${statusId}`;
    return this.http.get(endpointUrl, this.getRequestHeaders()).catch(error => { return this.handleError(error, () => this.changeVacancyStatusForCandidate(id, statusId)) });
  }
  completeLevel(id: string, levelId: string, statusId: string) {
    let endpointUrl = `${this.completeLevelUrl}/${id}/${levelId}/${statusId}`;
    return this.http.get(endpointUrl, this.getRequestHeaders()).catch(error => { return this.handleError(error, () => this.completeLevel(id, levelId, statusId)) });
  }

  savehrKpi(hrKpi: any) {
    return this.http.post(this.hrKpiUrl, JSON.stringify(hrKpi), this.getRequestHeaders()).catch(error => { return this.handleError(error, () => this.savehrKpi(hrKpi)) });
  }

  saveinterviewSchedule(interview?: any) {
    interview['jobCandidateUrl'] = this.configurations.baseUrl + interview['jobCandidateUrl'];
    return this.http.post(this.interviewScheduleUrl, JSON.stringify(interview), this.getRequestHeaders()).catch(error => { return this.handleError(error, () => this.saveinterviewSchedule(interview)) });
  }
  interviewFeedbcak(interviewId?: string) {
    let endpointUrl = `${this.getInterviewFeedbackUrl}/${interviewId}`;
    return this.http.get(endpointUrl, this.getRequestHeaders()).catch(error => { return this.handleError(error, () => this.interviewFeedbcak(interviewId)) });
  }
  interviewDelete(interviewId?: string) {
    let endpointUrl = `${this.getInterviewDeleteUrl}/${interviewId}`;
    return this.http.get(endpointUrl, this.getRequestHeaders()).catch(error => { return this.handleError(error, () => this.interviewDelete(interviewId)) });
  }
}
