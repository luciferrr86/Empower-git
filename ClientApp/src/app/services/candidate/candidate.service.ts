import { Injectable, Injector } from '@angular/core';
import { EndpointFactory } from '../common/endpoint-factory.service';
import { HttpClient } from '@angular/common/http';
import { ConfigurationService } from '../common/configuration.service';
import { CandidateViewModel } from '../../models/candidate/candidate.registration.model';
import { Observable } from 'rxjs';

@Injectable()
export class CandidateService extends EndpointFactory {

    private readonly _getProfilePicture: string = "/api/JobCandidateProfile/profilePic";
    
    private readonly _getResumeUrl: string = "/api/JobCandidateProfile/resume";
    private readonly _createcandidate: string = "/api/Job/candidateRegister";
    private readonly _getAllCandidate: string = "/api/JobCandidateProfile/list";
    private readonly _getPersonalDetail: string = "/api/JobCandidateProfile/candidateProfile";
    private readonly _updatePersonalDetail: string = "/api/JobCandidateProfile/updateCandidateProfile";
    private readonly _getProfessionalDetail: string = "/api/JobCandidateProfile/jobWorkExperience";
    private readonly _updateProfessionalDetail: string = "/api/JobCandidateProfile/updateWorkExperience";
    private readonly _getQualificationDetail: string = "/api/JobCandidateProfile/jobQulification";
    private readonly _updateQualificationDetail: string = "/api/JobCandidateProfile/updateQualification";

    private get getProfilePicture() { return this.configurations.baseUrl + this._getProfilePicture; }
    private get getResumeUrl() { return this.configurations.baseUrl + this._getResumeUrl; }
    private get createcandidate() { return this.configurations.baseUrl + this._createcandidate; }
    private get getCandidateListUrl() { return this.configurations.baseUrl + this._getAllCandidate; }
    private get getPersonalDetail() { return this.configurations.baseUrl + this._getPersonalDetail; }
    private get updatePersonalDetail() { return this.configurations.baseUrl + this._updatePersonalDetail; }
    private get getProfessionalDetail() { return this.configurations.baseUrl + this._getProfessionalDetail; }
    private get updateProfessionalDetail() { return this.configurations.baseUrl + this._updateProfessionalDetail; }
    private get getQualificationDetail() { return this.configurations.baseUrl + this._getQualificationDetail; }
    private get updateQualificationDetail() { return this.configurations.baseUrl + this._updateQualificationDetail; }

    constructor(http: HttpClient, configurations: ConfigurationService, injector: Injector) {

        super(http, configurations, injector);
    }

    getProfilePic(id:string)
    {      
        let endpointUrl = id ? `${this.getProfilePicture}/${id}` : this.getProfilePicture;
        return this.http.get(endpointUrl, this.getRequestHeaders())
            .catch(error => {
                return this.handleError(error, () => this.getProfilePic(id));
            });
    }
    getResume(id:string)
    {      
        let endpointUrl = id ? `${this.getResumeUrl}/${id}` : this.getProfilePicture;
        return this.http.get(endpointUrl, this.getRequestHeaders())
            .catch(error => {
                return this.handleError(error, () => this.getProfilePic(id));
            });
    }
    

    create(candidate: CandidateViewModel) {
        return this.http.post(this.createcandidate, JSON.stringify(candidate), this.getRequestHeaders()).catch(error => {
            return this.handleError(error, () => this.create(candidate))
        });

    }
    getAllCandidate(levelId:string,id?:string,page?: number, pageSize?: number, name?: string): Observable<any> {
        let endpointUrl = `${this.getCandidateListUrl}/${levelId}/${id}/${page}/${pageSize}/${name}`;
        return this.http.get(endpointUrl, this.getRequestHeaders()).catch(error => {
            return this.handleError(error, () => this.getAllCandidate(levelId,id,page, pageSize, name));
        })
    }
    getPersonal(personalId?: string) {
        let endpointUrl = personalId ? `${this.getPersonalDetail}/${personalId}` : this.getPersonalDetail;
        return this.http.get(endpointUrl, this.getRequestHeaders())
            .catch(error => {
                return this.handleError(error, () => this.getPersonal(personalId));
            });
    }

    updatePersonal(personalObject: any, personalId?: string) {
        let endpointUrl = personalId ? `${this.updatePersonalDetail}/${personalId}` : this.updatePersonalDetail;
        return this.http.put(endpointUrl, JSON.stringify(personalObject), this.getRequestHeaders())
            .catch(error => {
                return this.handleError(error, () => this.updatePersonal(personalObject, personalId));
            });
    }

    getprofessional(professionalId?: string) {
        let endpointUrl = professionalId ? `${this.getProfessionalDetail}/${professionalId}` : this.getProfessionalDetail;
        return this.http.get(endpointUrl, this.getRequestHeaders())
            .catch(error => {
                return this.handleError(error, () => this.getprofessional(professionalId));
            });
    }

    updateProfessional(professionalObject: any, professionalId?: string) {
        let endpointUrl = professionalId ? `${this.updateProfessionalDetail}/${professionalId}` : this.updateProfessionalDetail;
        return this.http.put(endpointUrl, JSON.stringify(professionalObject), this.getRequestHeaders())
            .catch(error => {
                return this.handleError(error, () => this.updateProfessional(professionalObject, professionalId));
            });
    }

    getQualification(qualificationId?: string) {
        let endpointUrl = qualificationId ? `${this.getQualificationDetail}/${qualificationId}` : this.getQualificationDetail;
        return this.http.get(endpointUrl, this.getRequestHeaders())
            .catch(error => {
                return this.handleError(error, () => this.getQualification(qualificationId));
            });
    }

    updateQualification(qualificationObject: any, qualificationId?: string) {
        let endpointUrl = qualificationId ? `${this.updateQualificationDetail}/${qualificationId}` : this.updateQualificationDetail;
        return this.http.put(endpointUrl, JSON.stringify(qualificationObject), this.getRequestHeaders())
            .catch(error => {
                return this.handleError(error, () => this.updateQualification(qualificationObject, qualificationId));
            });
    }
  

}
