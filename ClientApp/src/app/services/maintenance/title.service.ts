import { Injectable, Injector } from '@angular/core';
import { EndpointFactory } from '../common/endpoint-factory.service';
import { ConfigurationService } from '../common/configuration.service';
import { HttpClient } from '@angular/common/http';
import { FunctionalTitle } from '../../models/maintenance/functional-title.model';
import { Observable } from 'rxjs';

@Injectable()
export class TitleService extends EndpointFactory{
  
  private readonly _titleUrl:string="/api/Title/titleList";
  private readonly _getTitleUrl:string="/api/Title/getById";
  private readonly _createTitleUrl:string="/api/Title/create";
  private readonly _updateTitleUrl:string="/api/Title/update";
  private readonly _deleteTitleUrl:string="/api/Title/delete";

  private get titleUrl(){return this.configurations.baseUrl + this._titleUrl;}
  private get getUrl(){return this.configurations.baseUrl + this._getTitleUrl;}
  private get createUrl(){return this.configurations.baseUrl + this._createTitleUrl;}
  private get updateUrl(){return this.configurations.baseUrl + this._updateTitleUrl;}
  private get deleteUrl(){return this.configurations.baseUrl + this._deleteTitleUrl;}

  constructor(http: HttpClient, configurations: ConfigurationService, injector: Injector) {

    super(http, configurations, injector);
  }
  getAllTitle(page?: number, pageSize?: number, name?: string):Observable<any>  {
    let endpointUrl = `${this.titleUrl}/${page}/${pageSize}/${name}`;  
    return this.http.get(endpointUrl,this.getRequestHeaders()).
    catch(error=>{
      return this.handleError(error,()=>this.getAllTitle(page, pageSize,name));
    });
  }
 create(titleObject:any){
   return this.http.post(this.createUrl,JSON.stringify(titleObject),this.getRequestHeaders()).
   catch(error=>{
     return this.handleError(error,()=>this.create(titleObject));
   });
 }
 update(titleObject:any,titleId?:string){

    let endpointUrl = titleId ? `${this.updateUrl}/${titleId}` : this.updateUrl;
    return this.http.put(endpointUrl,JSON.stringify(titleObject),this.getRequestHeaders()).
    catch(error=>{
      return this.handleError(error,()=>this.update(titleObject,titleId));
    });
 }
 delete(titleId?:string | FunctionalTitle){
   let endpoint=`${this.deleteUrl}/${titleId}`
   return this.http.delete(endpoint,this.getRequestHeaders()).
   catch(error=>{
    return this.handleError(error,()=>this.delete(titleId));
   })
 }
}
