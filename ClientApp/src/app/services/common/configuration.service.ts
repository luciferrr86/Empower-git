import { Injectable } from '@angular/core';
import { LocalStoreManager } from './local-store-manager.service';
import { environment } from '../../../environments/environment';
import { Utilities } from './utilities';
import { DBkeys } from './db-key';

type UserConfiguration = {
  homeUrl: string
};

@Injectable()
export class ConfigurationService {

  public static readonly appVersion: string = "2.0.0";

  public baseUrl = environment.baseUrl || Utilities.baseUrl();
  public loginUrl = environment.loginUrl;
  public fallbackBaseUrl = "http://empower360plus.com";

  public static readonly defaultHomeUrl: string = "/";

  private _homeUrl: string = null;

  constructor(private localStorage: LocalStoreManager) {
    this.loadLocalChanges();
  }
  private loadLocalChanges() {

  }
  private saveToLocalStore(data: any, key: string) {
    setTimeout(() => this.localStorage.savePermanentData(data, key));
}
  public import(jsonValue: string) {
    this.clearLocalChanges();
    if (!jsonValue)
      return;
    let importValue: UserConfiguration = Utilities.JSonTryParse(jsonValue);
  }


  public export(changesOnly = true): string {

    let exportValue: UserConfiguration =
    {
      //homeUrl: changesOnly ? this._homeUrl : this.homeUrl
      homeUrl:changesOnly?this._homeUrl:this._homeUrl
    };

    return JSON.stringify(exportValue);
  }
  public clearLocalChanges() {
    this._homeUrl = null;
    //this.localStorage.deleteData(DBkey.HOME_URL);

  }
  set homeUrl(value: string) {
    this._homeUrl = value;
    this.saveToLocalStore(value, DBkeys.HOME_URL);
}
get homeUrl() {
    if (this._homeUrl != null)
        return this._homeUrl;

    return ConfigurationService.defaultHomeUrl;
}
}
