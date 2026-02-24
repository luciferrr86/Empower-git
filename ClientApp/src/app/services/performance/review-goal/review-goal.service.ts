import { Injectable, Injector } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { EndpointFactory } from '../../common/endpoint-factory.service';
import { ConfigurationService } from '../../common/configuration.service';
import { Observable } from 'rxjs';


@Injectable()
export class ReviewGoalService extends EndpointFactory {

  private readonly _getEmployeeListUrl: string = "/api/ReviewGoal/employeeList";
  private readonly _getEmployeeDetailsUrl: string = "/api/ReviewGoal/employeeDetails";
  private readonly _getEmployeePerformanceUrl: string = "/api/ReviewGoal/getCurrentYearGoal";
  private readonly _saveCurrentYearGoalUrl: string = "/api/ReviewGoal/saveCurrentYearGoal";

  private readonly _getTrainingClassesUrl: string = "/api/ReviewGoal/getTrainingClasses";

  private readonly _getDevelopmentPlanUrl: string = "/api/ReviewGoal/getDevelopmentPlan";
  private readonly _saveDevelopmentPlanUrl: string = "/api/ReviewGoal/saveDevelopmentPlan";

  private readonly _getRatingUrl: string = "/api/ReviewGoal/getRating";
  private readonly _saveRatingUrl: string = "/api/ReviewGoal/saveRating";

  private get getEmployeeDetailsUrl() { return this.configurations.baseUrl + this._getEmployeeDetailsUrl; }
  private get getEmployeeListUrl() { return this.configurations.baseUrl + this._getEmployeeListUrl; }
  private get getEmployeePerformanceUrl() { return this.configurations.baseUrl + this._getEmployeePerformanceUrl; }
  private get saveCurrentYearGoalUrl() { return this.configurations.baseUrl + this._saveCurrentYearGoalUrl; }

  private get getTrainingClassesUrl() { return this.configurations.baseUrl + this._getTrainingClassesUrl; }

  private get getDevelopmentPlanUrl() { return this.configurations.baseUrl + this._getDevelopmentPlanUrl; }
  private get saveDevelopmentPlanUrl() { return this.configurations.baseUrl + this._saveDevelopmentPlanUrl; }

  private get getRatingUrl() { return this.configurations.baseUrl + this._getRatingUrl; }
  private get saveRatingUrl() { return this.configurations.baseUrl + this._saveRatingUrl; }

  constructor(http: HttpClient, configurations: ConfigurationService, injector: Injector) {
    super(http, configurations, injector);
  }

  getEmployeeList(id?: string, page?: number, pageSize?: number, name?: string) {
    let endpointUrl = `${this.getEmployeeListUrl}/${id}/${page}/${pageSize}/${name}`;
    return this.http.get(endpointUrl, this.getRequestHeaders()).catch(error => {
      return this.handleError(error, () => this.getEmployeeList(id, page, pageSize, name));
    });
  }

  getEmployeePerformance(id?: string) {
    let endpointUrl = `${this.getEmployeePerformanceUrl}/${id}`;
    return this.http.get(endpointUrl, this.getRequestHeaders()).catch(error => {
      return this.handleError(error, () => this.getEmployeePerformance(id));
    });
  }

  getEmployeeDetails(id?: string) {
    let endpointUrl = `${this.getEmployeeDetailsUrl}/${id}`;
    return this.http.get(endpointUrl, this.getRequestHeaders()).catch(error => {
      return this.handleError(error, () => this.getEmployeeDetails(id));
    });
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
}
