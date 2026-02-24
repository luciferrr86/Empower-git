import { ConfigurationService } from '../common/configuration.service';
import { EndpointFactory } from '../common/endpoint-factory.service';
import { Injectable, Injector } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable()
export class ProfileService extends EndpointFactory {
    private readonly _getProfilePicture: string = "/api/Profile/profilePic";
    private readonly _updateProfilePicDetail: string = "/api/Profile/updateProfilePicDetail";
    private readonly _getPersonalDetail: string = "/api/Profile/personal";
    private readonly _updatePersonalDetail: string = "/api/Profile/updatePersonalDetail";
    private readonly _getProfessionalDetail: string = "/api/Profile/Professional";
    private readonly _updateProfessionalDetail: string = "/api/Profile/updateProfessional";
    private readonly _getQualificationDetail: string = "/api/Profile/qualification";
    private readonly _updateQualificationDetail: string = "/api/Profile/updateQualiDetail";

    private get getProfilePicture() { return this.configurations.baseUrl + this._getProfilePicture; }
    private get getPersonalDetail() { return this.configurations.baseUrl + this._getPersonalDetail; }
    private get updatePersonalDetail() { return this.configurations.baseUrl + this._updatePersonalDetail; }
    private get getProfessionalDetail() { return this.configurations.baseUrl + this._getProfessionalDetail; }
    private get updateProfessionalDetail() { return this.configurations.baseUrl + this._updateProfessionalDetail; }
    private get getQualificationDetail() { return this.configurations.baseUrl + this._getQualificationDetail; }
    private get updateQualificationDetail() { return this.configurations.baseUrl + this._updateQualificationDetail; }

    constructor(http: HttpClient, configurations: ConfigurationService, injector: Injector) {
        super(http, configurations, injector);
    }

    getProfilePic(id: string) {
        let endpointUrl = id ? `${this.getProfilePicture}/${id}` : this.getProfilePicture;
        return this.http.get(endpointUrl, this.getRequestHeaders())
            .catch(error => {
                return this.handleError(error, () => this.getProfilePic(id));
            });
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

    updateProfessional(professionalObject: any, userId?: string) {
        let endpointUrl = userId ? `${this.updateProfessionalDetail}/${userId}` : this.updateProfessionalDetail;
        return this.http.put(endpointUrl, JSON.stringify(professionalObject), this.getRequestHeaders())
            .catch(error => {
                return this.handleError(error, () => this.updateProfessional(professionalObject, userId));
            });
    }

    getQualification(qualificationId?: string) {
        let endpointUrl = qualificationId ? `${this.getQualificationDetail}/${qualificationId}` : this.getQualificationDetail;
        return this.http.get(endpointUrl, this.getRequestHeaders())
            .catch(error => {
                return this.handleError(error, () => this.getPersonal(qualificationId));
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