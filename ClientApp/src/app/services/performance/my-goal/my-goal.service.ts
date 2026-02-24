import { Injectable, Injector } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { HttpClient } from '@angular/common/http';

import { EndpointFactory } from '../../common/endpoint-factory.service';
import { ConfigurationService } from '../../common/configuration.service';


@Injectable()
export class MyGoalService extends EndpointFactory {
  private readonly _getUrl: string = "/api/MyGoal/getEmpDetail";
  private readonly _getCurrentYearGoalUrl: string = "/api/MyGoal/getCurrentYearGoal";
  private readonly _saveCurrentYearGoalUrl: string = "/api/MyGoal/saveCurrentYearGoal";
  private readonly _getPreCheckUrl: string = "/api/MyGoal/getPreCheck";

  private readonly _getTrainingClassesUrl: string = "/api/MyGoal/getTrainingClasses";
  private readonly _saveTrainingClassesUrl: string = "/api/MyGoal/saveTrainingClasses";
  private readonly _deleteTrainingClassesUrl: string = "/api/MyGoal/deleteTrainingClasses"
  private readonly _getDevelopmentPlanUrl: string = "/api/MyGoal/getDevelopmentPlan";
  private readonly _saveDevelopmentPlanUrl: string = "/api/MyGoal/saveDevelopmentPlan";

  private readonly _getRatingUrl: string = "/api/MyGoal/getRating";
  private readonly _saveRatingUrl: string = "/api/MyGoal/saveRating";

  private get getUrl() { return this.configurations.baseUrl + this._getUrl; }
  private get getPreCheckUrl() { return this.configurations.baseUrl + this._getPreCheckUrl; }
  private get getCurrentYearGoalUrl() { return this.configurations.baseUrl + this._getCurrentYearGoalUrl; }
  private get saveCurrentYearGoalUrl() { return this.configurations.baseUrl + this._saveCurrentYearGoalUrl; }

  private get getTrainingClassesUrl() { return this.configurations.baseUrl + this._getTrainingClassesUrl; }
  private get saveTrainingClassesUrl() { return this.configurations.baseUrl + this._saveTrainingClassesUrl; }
  private get deleteTrainingClassesUrl() { return this.configurations.baseUrl + this._deleteTrainingClassesUrl; }

  private get getDevelopmentPlanUrl() { return this.configurations.baseUrl + this._getDevelopmentPlanUrl; }
  private get saveDevelopmentPlanUrl() { return this.configurations.baseUrl + this._saveDevelopmentPlanUrl; }

  private get getRatingUrl() { return this.configurations.baseUrl + this._getRatingUrl; }
  private get saveRatingUrl() { return this.configurations.baseUrl + this._saveRatingUrl; }



  constructor(http: HttpClient, configurations: ConfigurationService, injector: Injector) {
    super(http, configurations, injector);
  }


  getEmployeeDetail(id: string): Observable<any> {
    let endpointUrl = `${this.getUrl}/${id}`;
    return this.http.get(endpointUrl, this.getRequestHeaders()).catch(error => {
      return this.handleError(error, () => this.getEmployeeDetail(id));
    })
  }

  chkPerformanceStart(id: string): Observable<any> {
    let endpointUrl = `${this.getPreCheckUrl}/${id}`;
    return this.http.get(endpointUrl, this.getRequestHeaders()).catch(error => {
      return this.handleError(error, () => this.chkPerformanceStart(id));
    })
  }

  getCurrentYearGoal(id: string): Observable<any> {
    let endpointUrl = `${this.getCurrentYearGoalUrl}/${id}`;
    return this.http.get(endpointUrl, this.getRequestHeaders()).catch(error => {
      return this.handleError(error, () => this.getCurrentYearGoal(id));
    })
  }

  saveCurrentYearGoal(id: string, goalObject: any, actionType: string) {
    let endpointUrl = `${this.saveCurrentYearGoalUrl}/${id}/${actionType}`;
    return this.http.post(endpointUrl, JSON.stringify(goalObject), this.getRequestHeaders()).
      catch(error => {
        return this.handleError(error, () => this.saveCurrentYearGoal(id, goalObject, actionType));
      });
  }

  getTrainingClassesList(id: string): Observable<any> {
    let endpointUrl = `${this.getTrainingClassesUrl}/${id}`;
    return this.http.get(endpointUrl, this.getRequestHeaders()).catch(error => {
      return this.handleError(error, () => this.getTrainingClassesList(id));
    })
  }

  saveTrainingClasses(id: string, goalObject: any, actionType: string) {
    let endpointUrl = `${this.saveTrainingClassesUrl}/${id}/${actionType}`;
    return this.http.post(endpointUrl, JSON.stringify(goalObject), this.getRequestHeaders()).
      catch(error => {
        return this.handleError(error, () => this.saveTrainingClasses(id, goalObject, actionType));
      });
  }

  getDevelopmentPlanList(id: string): Observable<any> {
    let endpointUrl = `${this.getDevelopmentPlanUrl}/${id}`;
    return this.http.get(endpointUrl, this.getRequestHeaders()).catch(error => {
      return this.handleError(error, () => this.getDevelopmentPlanList(id));
    })
  }

  saveDevelopmentPlan(id: string, goalObject: any, actionType: string) {
    let endpointUrl = `${this.saveDevelopmentPlanUrl}/${id}/${actionType}`;
    return this.http.post(endpointUrl, JSON.stringify(goalObject), this.getRequestHeaders()).
      catch(error => {
        return this.handleError(error, () => this.saveDevelopmentPlan(id, goalObject, actionType));
      });
  }

  getRating(id: string): Observable<any> {
    let endpointUrl = `${this.getRatingUrl}/${id}`;
    return this.http.get(endpointUrl, this.getRequestHeaders()).catch(error => {
      return this.handleError(error, () => this.getDevelopmentPlanList(id));
    })
  }

  saveRating(id: string, goalObject: any, actionType: string) {
    let endpointUrl = `${this.saveRatingUrl}/${id}/${actionType}`;
    return this.http.post(endpointUrl, JSON.stringify(goalObject), this.getRequestHeaders()).
      catch(error => {
        return this.handleError(error, () => this.saveDevelopmentPlan(id, goalObject, actionType));
      });
  }

  deleteTrainingClasses(id: string) {
    let endpointUrl = `${this.deleteTrainingClassesUrl}/${id}`;
    return this.http.delete(endpointUrl, this.getRequestHeaders())
      .catch(error => {
        return this.handleError(error, () => this.deleteTrainingClasses(id));
      });
  }

}
