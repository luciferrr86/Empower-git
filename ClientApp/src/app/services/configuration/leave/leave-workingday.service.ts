import { Injectable, Injector } from '@angular/core';
import { EndpointFactory } from '../../common/endpoint-factory.service';
import { ConfigurationService } from '../../common/configuration.service';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';


@Injectable()
export class LeaveWorkingdayService extends EndpointFactory {
   private readonly _getAllWorkingDayUrl :string = "/api/LeaveWorkingDay/list";
   private readonly _createUrl :string ="/api/LeaveWorkingDay/create";
   private readonly _updateUrl :string = "/api/LeaveWorkingDay/update";

   public get getUrl() { return this.configurations.baseUrl +this._getAllWorkingDayUrl;}
   public get createUrl(){return this.configurations.baseUrl +this._createUrl;}
   public get updateUrl(){return this.configurations.baseUrl +this._updateUrl;}
   constructor(http: HttpClient, configurations: ConfigurationService, injector: Injector) {
    super(http, configurations, injector);
  }

  getAllWorkingDay():Observable<any>{
    let endpointUrl = `${this.getUrl}`;
    return this.http.get(endpointUrl,this.getRequestHeaders()).catch(error =>{ 
      return this.handleError(error,()=>this.getAllWorkingDay());
    })
  }

  update(workingDay:any){
  return this.http.put(this.updateUrl,JSON.stringify(workingDay), this.getRequestHeaders())
  .catch(error => { return this.handleError(error, () => this.update(workingDay)); });  
  }
}
