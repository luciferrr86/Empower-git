import { ModuleAccess } from './../../models/account/user.model';
import { Injectable } from '@angular/core';
import { Router, NavigationExtras } from "@angular/router";
import { Observable } from 'rxjs/Observable';
import { Subject } from 'rxjs/Subject';
import 'rxjs/add/operator/map';
import { LocalStoreManager } from './local-store-manager.service';
import { EndpointFactory } from './endpoint-factory.service';
import { ConfigurationService } from './configuration.service';
import { DBkeys } from './db-key';
import { JwtHelper } from './jwt-helper';
import { Utilities } from './utilities';
import { LoginResponse, IdToken } from '../../models/account/login-response.model'
import { User } from '../../models/account/user.model';
import { Permission, PermissionNames, PermissionValues } from '../../models/account/permission.model';

@Injectable()
export class AuthService {

  public get loginUrl() { return this.configurations.loginUrl; }
  public get homeUrl() { return this.configurations.homeUrl; }

  public loginRedirectUrl: string;
  public logoutRedirectUrl: string;

  public reLoginDelegate: () => void;

  private previousIsLoggedInCheck = false;
  private _loginStatus = new Subject<boolean>();


  constructor(private router: Router, private configurations: ConfigurationService, private endpointFactory: EndpointFactory, private localStorage: LocalStoreManager) {
    this.initializeLoginStatus();
  }


  private initializeLoginStatus() {
    this.localStorage.getInitEvent().subscribe(() => {
      this.reevaluateLoginStatus();
    });
  }


  gotoPage(page: string, preserveParams = true) {

    let navigationExtras: NavigationExtras = {
      queryParamsHandling: preserveParams ? "merge" : "", preserveFragment: preserveParams
    };


    this.router.navigate([page], navigationExtras);
  }


  redirectLoginUser() {

    if (this.localStorage.getData("redirectUrl") != null) {
      this.loginRedirectUrl = this.localStorage.getData("redirectUrl");
      this.localStorage.deleteData("redirectUrl");
    }

    let redirect = this.loginRedirectUrl && this.loginRedirectUrl != '/' && this.loginRedirectUrl != ConfigurationService.defaultHomeUrl ? this.loginRedirectUrl : this.homeUrl;
    this.loginRedirectUrl = null;


    let urlParamsAndFragment = Utilities.splitInTwo(redirect, '#');
    let urlAndParams = Utilities.splitInTwo(urlParamsAndFragment.firstPart, '?');

    let navigationExtras: NavigationExtras = {
      fragment: urlParamsAndFragment.secondPart,
      queryParams: Utilities.getQueryParamsFromString(urlAndParams.secondPart),
      queryParamsHandling: "merge"
    };

    if (this.currentUser.roles.indexOf('candidate') > -1) {
      this.router.navigate(['candidate/job-application'], navigationExtras);
    }
    else {
      if (this.currentUser.roles != null) {
        if (this.currentUser.type == 'superadmin') {
          this.router.navigate(['administrator/client'], navigationExtras);
        }
        else if (this.currentUser.roles.indexOf('administrator') > -1) {
          //let a = urlAndParams.firstPart + navigationExtras;
          this.router.navigate([urlAndParams.firstPart], navigationExtras);
        } 
        else if (this.currentUser.roles.indexOf('Employee') > -1) {
          this.router.navigate([urlAndParams.firstPart], navigationExtras);
        }
        else {
          this.router.navigate([urlAndParams.firstPart]);
        }
      }
      else {
        this.router.navigate(['account/login'], navigationExtras);
      }
    }
  }


  redirectLogoutUser() {
    let redirect = this.logoutRedirectUrl ? this.logoutRedirectUrl : this.loginUrl;
    this.logoutRedirectUrl = null;

    this.router.navigate([redirect]);
  }


  redirectForLogin() {
    this.loginRedirectUrl = this.router.url;
    this.router.navigate([this.loginUrl]);
  }


  reLogin() {

    this.localStorage.deleteData(DBkeys.TOKEN_EXPIRES_IN);

    if (this.reLoginDelegate) {
      this.reLoginDelegate();
    }
    else {
      this.redirectForLogin();
    }
  }


  refreshLogin() {
    return this.endpointFactory.getRefreshLoginEndpoint<LoginResponse>()
      .map(response => this.processLoginResponse(response, this.rememberMe));
  }


  login(userName: string, password: string, rememberMe?: boolean) {

    if (this.isLoggedIn)
      this.logout();

    return this.endpointFactory.getLoginEndpoint<LoginResponse>(userName, password)
      .map(response => this.processLoginResponse(response, rememberMe));
  }


  private processLoginResponse(response: LoginResponse, rememberMe: boolean) {

    let accessToken = response.access_token;

    if (accessToken == null)
      throw new Error("Received accessToken was empty");

    let idToken = response.id_token;
    let refreshToken = response.refresh_token || this.refreshToken;
    let expiresIn = response.expires_in;

    let tokenExpiryDate = new Date();
    tokenExpiryDate.setSeconds(tokenExpiryDate.getSeconds() + expiresIn);

    let accessTokenExpiry = tokenExpiryDate;

    let jwtHelper = new JwtHelper();
    let decodedIdToken = <IdToken>jwtHelper.decodeToken(response.id_token);

    let permissions: PermissionValues[] = Array.isArray(decodedIdToken.permission) ? decodedIdToken.permission : [decodedIdToken.permission];
    if (!this.isLoggedIn)
      this.configurations.import(decodedIdToken.configuration);

    let user = new User(
      decodedIdToken.sub,
      decodedIdToken.name,
      decodedIdToken.fullname,
      decodedIdToken.email,
      decodedIdToken.phone,
      Array.isArray(decodedIdToken.role) ? decodedIdToken.role : [decodedIdToken.role], decodedIdToken.type);
    user.isEnabled = true;
    let moduleAccess = new ModuleAccess(
      decodedIdToken.leave == "1" ? true : false,
      decodedIdToken.performance == "1" ? true : false,
      decodedIdToken.timesheet == "1" ? true : false,
      decodedIdToken.expanseManagement == "1" ? true : false,
      decodedIdToken.recruitment == "1" ? true : false,
      decodedIdToken.salesMarketing == "1" ? true : false,
    );
    this.saveUserDetails(user, permissions, accessToken, idToken, refreshToken, accessTokenExpiry, rememberMe, moduleAccess);
    this.reevaluateLoginStatus(user);

    return user;
  }


  private saveUserDetails(user: User, permissions: PermissionValues[], accessToken: string, idToken: string, refreshToken: string, expiresIn: Date, rememberMe: boolean, applicationModule: ModuleAccess) {

    if (rememberMe) {
      this.localStorage.savePermanentData(accessToken, DBkeys.ACCESS_TOKEN);
      this.localStorage.savePermanentData(idToken, DBkeys.ID_TOKEN);
      this.localStorage.savePermanentData(refreshToken, DBkeys.REFRESH_TOKEN);
      this.localStorage.savePermanentData(expiresIn, DBkeys.TOKEN_EXPIRES_IN);
      this.localStorage.savePermanentData(permissions, DBkeys.USER_PERMISSIONS);
      this.localStorage.savePermanentData(user, DBkeys.CURRENT_USER);
      this.localStorage.savePermanentData(applicationModule, DBkeys.APPLICATION_MODULE);
    }
    else {
      this.localStorage.saveSyncedSessionData(accessToken, DBkeys.ACCESS_TOKEN);
      this.localStorage.saveSyncedSessionData(idToken, DBkeys.ID_TOKEN);
      this.localStorage.saveSyncedSessionData(refreshToken, DBkeys.REFRESH_TOKEN);
      this.localStorage.saveSyncedSessionData(expiresIn, DBkeys.TOKEN_EXPIRES_IN);
      this.localStorage.saveSyncedSessionData(permissions, DBkeys.USER_PERMISSIONS);
      this.localStorage.saveSyncedSessionData(user, DBkeys.CURRENT_USER);
      this.localStorage.savePermanentData(applicationModule, DBkeys.APPLICATION_MODULE);
    }

    this.localStorage.savePermanentData(rememberMe, DBkeys.REMEMBER_ME);
  }



  logout(): void {
    this.localStorage.deleteData(DBkeys.ACCESS_TOKEN);
    this.localStorage.deleteData(DBkeys.ID_TOKEN);
    this.localStorage.deleteData(DBkeys.REFRESH_TOKEN);
    this.localStorage.deleteData(DBkeys.TOKEN_EXPIRES_IN);
    this.localStorage.deleteData(DBkeys.USER_PERMISSIONS);
    this.localStorage.deleteData(DBkeys.CURRENT_USER);
    this.localStorage.savePermanentData(DBkeys.APPLICATION_MODULE);

    this.configurations.clearLocalChanges();

    this.reevaluateLoginStatus();
  }


  private reevaluateLoginStatus(currentUser?: User) {

    let user = currentUser || this.localStorage.getDataObject<User>(DBkeys.CURRENT_USER);
    let isLoggedIn = user != null;

    if (this.previousIsLoggedInCheck != isLoggedIn) {
      setTimeout(() => {
        this._loginStatus.next(isLoggedIn);
      });
    }

    this.previousIsLoggedInCheck = isLoggedIn;
  }


  getLoginStatusEvent(): Observable<boolean> {
    return this._loginStatus.asObservable();
  }


  get currentUser(): User {

    let user = this.localStorage.getDataObject<User>(DBkeys.CURRENT_USER);
    this.reevaluateLoginStatus(user);

    return user;
  }
  get module(): ModuleAccess {

    let module = this.localStorage.getDataObject<ModuleAccess>(DBkeys.APPLICATION_MODULE);
    return module;
  }

  get userPermissions(): PermissionValues[] {
    return this.localStorage.getDataObject<PermissionValues[]>(DBkeys.USER_PERMISSIONS) || [];
  }

  get accessToken(): string {

    this.reevaluateLoginStatus();
    return this.localStorage.getData(DBkeys.ACCESS_TOKEN);
  }

  get accessTokenExpiryDate(): Date {

    this.reevaluateLoginStatus();
    return this.localStorage.getDataObject<Date>(DBkeys.TOKEN_EXPIRES_IN, true);
  }

  get isSessionExpired(): boolean {

    if (this.accessTokenExpiryDate == null) {
      return true;
    }

    return !(this.accessTokenExpiryDate.valueOf() > new Date().valueOf());
  }


  get idToken(): string {

    this.reevaluateLoginStatus();
    return this.localStorage.getData(DBkeys.ID_TOKEN);
  }

  get refreshToken(): string {

    this.reevaluateLoginStatus();
    return this.localStorage.getData(DBkeys.REFRESH_TOKEN);
  }

  get isLoggedIn(): boolean {
    return this.currentUser != null;
  }

  get rememberMe(): boolean {
    return this.localStorage.getDataObject<boolean>(DBkeys.REMEMBER_ME) == true;
  }
}
