import { Injectable, Injector } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { EndpointFactory } from '../../common/endpoint-factory.service';
import { ConfigurationService } from '../../common/configuration.service';

@Injectable()
export class SetGoalService extends EndpointFactory {
  private readonly _getListUrl: string = "/api/SetGoal/getlist";
  private readonly _createUrl: string = "/api/SetGoal/savegoal";
  private readonly _saveListUrl: string = "/api/SetGoal/savelist";
  private readonly _releaseUrl: string = "/api/SetGoal/releaseGoal";
  private readonly _deleteUrl: string = "/api/SetGoal/deleteMeasure";

  private get getListUrl() { return this.configurations.baseUrl + this._getListUrl; }
  private get createUrl() { return this.configurations.baseUrl + this._createUrl; }
  private get saveUrl() { return this.configurations.baseUrl + this._saveListUrl; }
  private get releaseUrl() { return this.configurations.baseUrl + this._releaseUrl; }
  private get deleteGoalMeasureUrl() { return this.configurations.baseUrl + this._deleteUrl; }


  constructor(http: HttpClient, configurations: ConfigurationService, injector: Injector) {
    super(http, configurations, injector);
  }

  getGoalList(id: string, val: string) {
    let endpointUrl = `${this.getListUrl}/${id}/${val}`;
    return this.http.get(endpointUrl, this.getRequestHeaders()).catch(error => {
      return this.handleError(error, () => this.getGoalList(id, val));
    })
  }

  saveGoalList(id: string, goalObject: any) {
    let endpointUrl = `${this.saveUrl}/${id}`;
    return this.http.post(endpointUrl, JSON.stringify(goalObject), this.getRequestHeaders()).
      catch(error => {
        return this.handleError(error, () => this.saveGoalList(id, goalObject));
      });
  }

  create(goalNameObject: any, id: string) {
    let endpointUrl = `${this.createUrl}/${id}`;
    return this.http.post(endpointUrl, JSON.stringify(goalNameObject), this.getRequestHeaders()).
      catch(error => {
        return this.handleError(error, () => this.create(goalNameObject, id));
      });

  }

  release(id: string) {
    let endpointUrl = `${this.releaseUrl}/${id}`;
    return this.http.get(endpointUrl, this.getRequestHeaders()).
      catch(error => {
        return this.handleError(error, () => this.release(id));
      });
  }

  deleteGoalMeasure(id: string) {    
    let endpointUrl = `${this.deleteGoalMeasureUrl}/${id}`;
    return this.http.delete(endpointUrl, this.getRequestHeaders())
      .catch(error => {
        return this.handleError(error, () => this.deleteGoalMeasure(id));
      });
  }

}
