import { Injectable, Injector } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { EndpointFactory } from '../common/endpoint-factory.service';
import { ConfigurationService } from '../common/configuration.service';

@Injectable()
export class ChangePasswordService  extends EndpointFactory{
  private readonly _putUrl: string = "/api/Account/changepassword";
  private get putUrl() { return this.configurations.baseUrl + this._putUrl; }
  constructor(http: HttpClient, configurations: ConfigurationService, injector: Injector) { 
    super(http, configurations, injector);
  }

  updatePassword(id:string,changePassword:any)
  { 
    let endpointUrl = `${this.putUrl}/${id}`;    
    return this.http.put(endpointUrl,JSON.stringify(changePassword),this.getRequestHeaders()).
    catch(error=>{
      return this.handleError(error,()=>this.updatePassword(id,changePassword));
    });     
  }
}
