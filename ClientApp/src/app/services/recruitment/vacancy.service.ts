import { Injectable, Injector } from '@angular/core';
import { ConfigurationService } from '../common/configuration.service';
import { Observable } from 'rxjs/Observable';
import { HttpClient, HttpParams } from '@angular/common/http';
import { EndpointFactory } from '../common/endpoint-factory.service';
import { Vacancy } from '../../models/recruitment/job-vacancy/vacancy-list.model';
import { BehaviorSubject } from 'rxjs';
import { Body } from '@angular/http/src/body';
import { JobApplyModel } from '../../models/recruitment/job-vacancy/job-apply.model';
import { EmailDirectory } from '../../models/common/emailDirectory';

@Injectable()
export class VacancyService extends EndpointFactory {
  private readonly _listUrl: string = "/api/JobVacancy/list";
  private readonly _getVacancyUrl: string = "/api/JobVacancy/vacancy";
  private readonly _saveVacancyUrl: string = "/api/JobVacancy/save";
  private readonly _publishVacancyUrl: string = "/api/JobVacancy/publish";
  private readonly _deleteInterviewLevelUrl: string = "/api/JobVacancy/deleteinterviewlevel";
  private readonly _deleteVacancyUrl: string = "/api/JobVacancy/delete";

  private readonly _getScreeningUrl: string = "/api/JobVacancy/getscreening";
  private readonly _saveScreeningUrl: string = "/api/JobVacancy/savescreening";
  private readonly _deleteScreeningUrl: string = "/api/JobVacancy/deletescreening";

  private readonly _getHrKpiUrl: string = "/api/JobVacancy/gethrquestion";
  private readonly _saveHrKpiUrl: string = "/api/JobVacancy/savehrquestion";
  private readonly _deleteHrKpiUrl: string = "/api/JobVacancy/deletehrquestion";

  private readonly _getSkillUrl: string = "/api/JobVacancy/getskillquestion";
  private readonly _saveSkillUrl: string = "/api/JobVacancy/saveskillquestion";
  private readonly _deleteSkillUrl: string = "/api/JobVacancy/deleteskillquestion";
  private readonly _applyJobUrl: string = "/api/JobVacancy/applyforjob";
  private readonly _allAvailableVancacy: string = "/api/JobVacancy/joblist";
  private readonly _sendJDMail: string = "/api/JobVacancy/sendMail";
  private readonly _updateJDReason: string = "/api/JobVacancy/updateReason";
  private readonly _getReason: string = "/api/JobVacancy/getReason";
  private readonly _directoryList: string = "/api/EmailDirectory";
  private readonly _saveDirectory: string = "/api/EmailDirectory/create";
  private readonly _updateDirectory: string = "/api/EmailDirectory";
  private readonly _directorylistUrl: string = "/api/EmailDirectory/list";
  private readonly _deleteDirectoryUrl: string = "/api/EmailDirectory/delete";
  private readonly _getDirectory: string = "/api/EmailDirectory";

  private get listvacancy() { return this.configurations.baseUrl + this._listUrl; }
  private get getVacancy() { return this.configurations.baseUrl + this._getVacancyUrl; }
  private get saveVacancy() { return this.configurations.baseUrl + this._saveVacancyUrl; }
  private get publishVacancy() { return this.configurations.baseUrl + this._publishVacancyUrl; }
  private get deleteLevel() { return this.configurations.baseUrl + this._deleteInterviewLevelUrl; }
  private get deleteVacancy() { return this.configurations.baseUrl + this._deleteVacancyUrl; }
  private get applyJobVacancy() { return this.configurations.baseUrl + this._applyJobUrl; }
  private get getAllJobVacancy() { return this.configurations.baseUrl + this._allAvailableVancacy; }

  private get getScreening() { return this.configurations.baseUrl + this._getScreeningUrl; }
  private get saveScreening() { return this.configurations.baseUrl + this._saveScreeningUrl }
  private get deletescreening() { return this.configurations.baseUrl + this._deleteScreeningUrl; }

  private get getHrKpi() { return this.configurations.baseUrl + this._getHrKpiUrl; }
  private get saveHrKpi() { return this.configurations.baseUrl + this._saveHrKpiUrl }
  private get deleteHrKpi() { return this.configurations.baseUrl + this._deleteHrKpiUrl; }

  private get getSkill() { return this.configurations.baseUrl + this._getSkillUrl }
  private get saveSkill() { return this.configurations.baseUrl + this._saveSkillUrl }
  private get deleteSkill() { return this.configurations.baseUrl + this._deleteSkillUrl; }
  private get jdMail() { return this.configurations.baseUrl + this._sendJDMail; } 
  private get jdReason() { return this.configurations.baseUrl + this._updateJDReason; }
  private get getReason() { return this.configurations.baseUrl + this._getReason; }
  private get getDirecotyList() { return this.configurations.baseUrl + this._directoryList; }
  private get saveDirectory() { return this.configurations.baseUrl + this._saveDirectory; }
  private get updateDirectory() {
    return this.configurations.baseUrl + this._updateDirectory;
  }
  private get directoryList() { return this.configurations.baseUrl + this._directorylistUrl; }
  private get delDirectory() { return this.configurations.baseUrl + this._deleteDirectoryUrl; }
  private get getDirectory() { return this.configurations.baseUrl + this._getDirectory; }

  constructor(http: HttpClient, configurations: ConfigurationService, injector: Injector) {
    super(http, configurations, injector);
  }

  private vacancyIdSource = new BehaviorSubject("");
  currentVacancyId = this.vacancyIdSource.asObservable();

  changeVacancyId(id: string) {
    this.vacancyIdSource.next(id)
  }


  //#region Vacancy And Creation
  getAllJobVacany(page?: number, pageSize?: number, name?: string): Observable<any> {
    let endpointUrl = `${this.listvacancy}/${page}/${pageSize}/${name}`;
    return this.http.get(endpointUrl, this.getRequestHeaders()).catch(error => {
      return this.handleError(error, () => this.getAllJobVacany(page, pageSize, name));
    })
  }
  getJobCreation(vacancyId?: string) {
    let endpointUrl = `${this.getVacancy}/${vacancyId}`;
    return this.http.get(endpointUrl, this.getRequestHeaders()).catch(error => {
      return this.handleError(error, () => this.getJobCreation(vacancyId));
    })
  }

  createJob(vacancy: any, vacancyId?: string) {
    let endpointUrl = `${this.saveVacancy}/${vacancyId}`;
    return this.http.post(endpointUrl, JSON.stringify(vacancy), this.getRequestHeaders()).catch(error => {
      return this.handleError(error, () => this.createJob(vacancy, vacancyId));
    });
  }

  deleteJob(vacancyId: string | Vacancy) {
    let endpointUrl = `${this.deleteVacancy}/${vacancyId}`;
    return this.http.delete(endpointUrl, this.getRequestHeaders())
      .catch(error => {
        return this.handleError(error, () => this.deleteJob(vacancyId));
      });
  }

  deleteInterviewLevel(interviewLevelId: string) {
    let endpointUrl = `${this.deleteLevel}/${interviewLevelId}`;
    return this.http.delete(endpointUrl, this.getRequestHeaders())
      .catch(error => {
        return this.handleError(error, () => this.deleteInterviewLevel(interviewLevelId));
      });
  }

  publish(vacancyId: string) {
    let endpointUrl = `${this.publishVacancy}/${vacancyId}`;
    return this.http.post(endpointUrl, this.getRequestHeaders()).catch(error => {
      { return this.handleError(error, () => this.publish(vacancyId)); }
    })
  }

  applyforJob(jobApplyModel:any) {
    let endpointUrl = this.applyJobVacancy;
    return this.http.post(endpointUrl, jobApplyModel).catch(error => {
      return this.handleError(error, () => this.applyforJob(jobApplyModel));
    });
  }

  getAllAvailableVacancy() {
    let endpointUrl = this.getAllJobVacancy;
    return this.http.get(endpointUrl).catch(error => {
      return this.handleError(error, () => this.getAllAvailableVacancy());
    });
  }

  sendJDMail(directory: any, vacancyId?: string) {
    let endpointUrl = `${this.jdMail}/${vacancyId}`;
    return this.http.post(endpointUrl, JSON.stringify(directory), this.getRequestHeaders()).catch(error => {
      return this.handleError(error, () => this.sendJDMail(directory, vacancyId));
    });
  }

  getJDReasonVacancy(vacancyId?: string) {
    let endpointUrl = `${this.getReason}/${vacancyId}`;
    return this.http.get(endpointUrl).catch(error => {
      return this.handleError(error, () => this.getJDReasonVacancy(vacancyId));
    });
  }

  updateReason(reason: string, vacancyId?: string) {
    let endpointUrl = `${this.jdReason}/${vacancyId}/${reason}`;
    return this.http.put(endpointUrl, this.getRequestHeaders()).catch(error => {
      return this.handleError(error, () => this.updateReason(reason, vacancyId));
    });
  }

  getEmailDirectoryList() {
    let endpointUrl = this.getDirecotyList;
    return this.http.get(endpointUrl, this.getRequestHeaders()).catch(error => {
      return this.handleError(error, () => this.getEmailDirectoryList());
    });
  }

  addEmailDirectory(directory: EmailDirectory) {
    
    let endpointUrl = this.saveDirectory;
    return this.http.post(endpointUrl, JSON.stringify(directory), this.getRequestHeaders()).catch(error => {
      return this.handleError(error, () => this.addEmailDirectory(directory));
    });

  }

  updateEmailDirectory(directory: EmailDirectory) {
    let endpointUrl = this.updateDirectory;
    return this.http.put(endpointUrl, JSON.stringify(directory), this.getRequestHeaders()).catch(error => {
      return this.handleError(error, () => this.updateEmailDirectory(directory));
    });

  }

  getDirectoryList(page?: number, pageSize?: number, name?: string): Observable<any> {
    let endpointUrl = `${this.directoryList}/${page}/${pageSize}/${name}`;
    return this.http.get(endpointUrl, this.getRequestHeaders()).catch(error => {
      return this.handleError(error, () => this.getAllJobVacany(page, pageSize, name));
    })
  }

  deleteDirectory(id: string) {
    let endpointUrl = `${this.delDirectory}/${id}`;
    return this.http.delete(endpointUrl, this.getRequestHeaders())
      .catch(error => {
        return this.handleError(error, () => this.deleteDirectory(id));
      });
  }

  getDirectoryById(id?: string) {
    let endpointUrl = `${this.getDirectory}/${id}`;
    return this.http.get(endpointUrl, this.getRequestHeaders()).catch(error => {
      return this.handleError(error, () => this.getDirectoryById(id));
    })
  }
  //#endregion

  //#region  Screening

  getJobScreening(vacancyId?: string) {
    let endpointUrl = `${this.getScreening}/${vacancyId}`;
    return this.http.get(endpointUrl, this.getRequestHeaders()).catch(error => {
      return this.handleError(error, () => this.getJobScreening(vacancyId));
    })
  }

  createScreening(screeningQuestionList: any, vacancyId?: string) {
    let endpointUrl = `${this.saveScreening}/${vacancyId}`;
    return this.http.post(endpointUrl, JSON.stringify(screeningQuestionList), this.getRequestHeaders()).catch(error => {
      return this.handleError(error, () => this.createScreening(screeningQuestionList, vacancyId));
    });
  }

  deleteScreening(screeningId: string) {
    let endpointUrl = `${this.deletescreening}/${screeningId}`;
    return this.http.delete(endpointUrl, this.getRequestHeaders())
      .catch(error => {
        return this.handleError(error, () => this.deleteScreening(screeningId));
      });
  }
  //#endregion

  //#region  Hr Kpi

  getHrQuestion(vacancyId?: string) {
    let endpointUrl = `${this.getHrKpi}/${vacancyId}`;
    return this.http.get(endpointUrl, this.getRequestHeaders()).catch(error => {
      return this.handleError(error, () => this.getHrQuestion(vacancyId));
    })
  }
  createHrQuestion(hrkpiArray: any, vacancyId?: string) {
    let endpointUrl = `${this.saveHrKpi}/${vacancyId}`;
    return this.http.post(endpointUrl, JSON.stringify(hrkpiArray), this.getRequestHeaders()).catch(error => {
      return this.handleError(error, () => this.createHrQuestion(hrkpiArray, vacancyId));
    });
  }

  deleteHrQuestion(id: string) {
    let endpointUrl = `${this.deleteHrKpi}/${id}`;
    return this.http.delete(endpointUrl, this.getRequestHeaders())
      .catch(error => {
        return this.handleError(error, () => this.deleteHrQuestion(id));
      });
  }

  //#endregion

  //#region  Skill
  getSkillQuestion(vacancyId?: string) {
    let endpointUrl = `${this.getSkill}/${vacancyId}`;
    return this.http.get(endpointUrl, this.getRequestHeaders()).catch(error => {
      return this.handleError(error, () => this.getSkillQuestion(vacancyId));
    })
  }
  createSkillQuestion(skill: any, vacancyId?: string) {
    let endpointUrl = `${this.saveSkill}/${vacancyId}`;
    return this.http.post(endpointUrl, JSON.stringify(skill), this.getRequestHeaders()).catch(error => {
      return this.handleError(error, () => this.createSkillQuestion(skill, vacancyId));
    });
  }

  deleteSkillQuestion(id: string) {
    let endpointUrl = `${this.deleteSkill}/${id}`;
    return this.http.delete(endpointUrl, this.getRequestHeaders())
      .catch(error => {
        return this.handleError(error, () => this.deleteSkillQuestion(id));
      });
  }

  
  //#endregion
}


