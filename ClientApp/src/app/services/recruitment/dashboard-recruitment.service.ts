import { Injectable, Injector } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ConfigurationService } from '../common/configuration.service';
import { EndpointFactory } from '../common/endpoint-factory.service';


@Injectable()
export class DashboardRecruitmentService extends EndpointFactory {
  private readonly _getDashboard: string = "/api/RecruitmentDashboard/recruitmentData";
  
  private get getDashboard() { return this.configurations.baseUrl + this._getDashboard; }

  constructor(http: HttpClient, configurations: ConfigurationService, injector: Injector) {

    super(http, configurations, injector);
  }

  getDashboardDetails(){
    let endpointUrl = `${this.getDashboard}`;
    return this.http.get(endpointUrl,this.getRequestHeaders()).catch(error=>{
      return this.handleError(error,()=>this.getDashboardDetails());
    });
  }

}
