import { Injectable, Injector } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { EndpointFactory } from '../../common/endpoint-factory.service';
import { ConfigurationService } from '../../common/configuration.service';
import { PerformanceConfig } from '../../../models/configuration/performance/performance-config.model';

@Injectable()
export class PerformaceConfigurationService extends EndpointFactory {
  private readonly _getUrl: string = "/api/PerformanceConfig/get";
  private readonly _saveConfigUrl: string = "/api/PerformanceConfig/save";
  private readonly _deleteRatingUrl: string = "/api/PerformanceConfig/deleteRating";
  private readonly _deleteFeedbackUrl: string = "/api/PerformanceConfig/deleteFeedback";

  private get getUrl() { return this.configurations.baseUrl + this._getUrl; }
  private get saveConfigUrl() { return this.configurations.baseUrl + this._saveConfigUrl; }
  private get deleteRatingUrl() { return this.configurations.baseUrl + this._deleteRatingUrl; }
  private get deleteFeedbackUrl() { return this.configurations.baseUrl + this._deleteFeedbackUrl; }
  constructor(http: HttpClient, configurations: ConfigurationService, injector: Injector) {
    super(http, configurations, injector);
  }

  getPerformanceConfig() {
    let endpointUrl = `${this.getUrl}`;
    return this.http.get(endpointUrl, this.getRequestHeaders()).catch(error => {
      return this.handleError(error, () => this.getPerformanceConfig());
    })
  }

  savePerformanceConfig(perConfigObject: PerformanceConfig, id?: string) {
    let endpointUrl = id ? `${this.saveConfigUrl}/${id}` : this.saveConfigUrl;
    return this.http.put(endpointUrl, JSON.stringify(perConfigObject), this.getRequestHeaders())
      .catch(error => {
        return this.handleError(error, () => this.savePerformanceConfig(perConfigObject, id));
      });
  }
  deleteFeedback(id: string) {
    let endpointUrl = `${this.deleteFeedbackUrl}/${id}`;
    return this.http.delete(endpointUrl, this.getRequestHeaders())
      .catch(error => {
        return this.handleError(error, () => this.deleteFeedback(id));
      });
  }

  deleteRating(id: string) {
    let endpointUrl = `${this.deleteRatingUrl}/${id}`;
    return this.http.delete(endpointUrl, this.getRequestHeaders())
      .catch(error => {
        return this.handleError(error, () => this.deleteRating(id));
      });
  }
}
