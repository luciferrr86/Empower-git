import { Injectable, Injector } from '@angular/core';
import { EndpointFactory } from '../common/endpoint-factory.service';
import { ConfigurationService } from '../common/configuration.service';
import { HttpClient } from '@angular/common/http';
import { Band } from '../../models/maintenance/band.model';
import { Observable } from 'rxjs';
@Injectable()
export class BandService extends EndpointFactory {

  private readonly _bandUrl: string = "/api/Band/bandList";
  private readonly _createBandUrl: string = "/api/Band/create";
  private readonly _updateBandUrl: string = "/api/Band/update";
  private readonly _deleteBandUrl: string = "/api/Band/delete";

  private get bandUrl() { return this.configurations.baseUrl + this._bandUrl; }
  private get createUrl() { return this.configurations.baseUrl + this._createBandUrl; }
  private get updateUrl() { return this.configurations.baseUrl + this._updateBandUrl; }
  private get deleteUrl() { return this.configurations.baseUrl + this._deleteBandUrl; }
  constructor(http: HttpClient, configurations: ConfigurationService, injector: Injector) {

    super(http, configurations, injector);
  }
  getAllBand(page?: number, pageSize?: number, name?: string): Observable<any> {

    let endpointUrl = `${this.bandUrl}/${page}/${pageSize}/${name}`;
    return this.http.get(endpointUrl, this.getRequestHeaders()).
      catch(error => {
        return this.handleError(error, () => this.getAllBand(page, pageSize, name));
      });
  }
  create(bandObject: any) {

    return this.http.post(this.createUrl, JSON.stringify(bandObject), this.getRequestHeaders()).
      catch(error => {
        return this.handleError(error, () => this.create(bandObject));
      });
  }
  update(bandObject: any, bandId?: string) {

    let endpointUrl = bandId ? `${this.updateUrl}/${bandId}` : this.updateUrl;
    return this.http.put(endpointUrl, JSON.stringify(bandObject), this.getRequestHeaders()).
      catch(error => {
        return this.handleError(error, () => this.update(bandObject, bandId));
      });
  }
  delete(bandId?: string | Band) {
    let endpoint = `${this.deleteUrl}/${bandId}`
    return this.http.delete(endpoint, this.getRequestHeaders()).
      catch(error => {
        return this.handleError(error, () => this.delete(bandId));
      })
  }
}
