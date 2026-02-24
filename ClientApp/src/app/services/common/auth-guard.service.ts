import { Injectable } from '@angular/core';
import { CanActivate, Router, ActivatedRouteSnapshot, RouterStateSnapshot, CanActivateChild, NavigationExtras, CanLoad, Route } from '@angular/router';
import { AuthService } from './auth.service';
import { LocalStoreManager } from './local-store-manager.service';


@Injectable()
export class AuthGuard implements CanActivate, CanActivateChild, CanLoad {
  constructor(private authService: AuthService, private router: Router, private localStorage: LocalStoreManager) { }
  
  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {
      
    let url: string = state.url;

    return this.checkLogin(url);
  }

  canActivateChild(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {

    return this.canActivate(route, state);
  }

  canLoad(route: Route): boolean {

    let url = `/${route.path}`;
    return this.checkLogin(url);
  }

  checkLogin(url: string): boolean {
    if (this.authService.isLoggedIn) {
      return true;
    }
   
    this.authService.loginRedirectUrl = url;
    this.localStorage.savePermanentData(this.authService.loginRedirectUrl, "redirectUrl");
    if (url.search('/candidate/') > -1) {
      this.router.navigate(['/candidate/login'])
    }
    else {
      this.router.navigate(['/account/login'])
    }

    return false;
  }
}


@Injectable()
export class AdminGuard implements CanActivate, CanActivateChild, CanLoad {
  constructor(private authService: AuthService, private router: Router) { }
  
  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {
      
    let url: string = state.url;

    return this.checkLogin(url);
  }

  canActivateChild(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {

    return this.canActivate(route, state);
  }

  canLoad(route: Route): boolean {

    let url = `/${route.path}`;
    return this.checkLogin(url);
  }

  checkLogin(url: string): boolean {
    if (this.authService.isLoggedIn) {
      if (this.authService.currentUser.type == 'superadmin') {
        return true;
      }
      else {
        this.authService.logout();
        this.router.navigate(['/account/login']);
      }
    }
    this.authService.loginRedirectUrl = url;
    this.router.navigate(['/account/login']);
    return false;
  }
}

@Injectable()
export class CandidateGuard implements CanActivate, CanActivateChild, CanLoad {
  constructor(private authService: AuthService, private router: Router) { }
 
  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {
      
    let url: string = state.url;

    return this.checkLogin(url);
  }

  canActivateChild(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {

    return this.canActivate(route, state);
  }

  canLoad(route: Route): boolean {

    let url = `/${route.path}`;
    return this.checkLogin(url);
  }

  checkLogin(url: string): boolean {
    if (this.authService.isLoggedIn) {
      if (this.authService.currentUser.roles.indexOf('candidate') > -1 && this.authService.currentUser.type != 'superadmin') {
        return true;
      } else {
        this.authService.logout();
        this.router.navigate(['/account/login']);
      }

    }
    this.authService.loginRedirectUrl = url;
    this.router.navigate(['/account/login'], { queryParams: { url: this.authService.loginRedirectUrl } });

    return false;
  }
}

@Injectable()
export class UserGuard implements CanActivate, CanActivateChild, CanLoad {
  constructor(private authService: AuthService, private router: Router) { }

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {
      
    let url: string = state.url;

    return this.checkLogin(url);
  }

  canActivateChild(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {

    return this.canActivate(route, state);
  }

  canLoad(route: Route): boolean {

    let url = `/${route.path}`;
    return this.checkLogin(url);
  }

  checkLogin(url: string): boolean {
    if (this.authService.isLoggedIn) {
      if (this.authService.currentUser.roles.indexOf('candidate') < 0 && this.authService.currentUser.type != 'superadmin') {
        return true;
      } else {
        this.authService.logout();
        this.router.navigate(['/account/login']);
      }

    }
    this.authService.loginRedirectUrl = url;
    this.router.navigate(['/account/login'])

    return false;
  }
}

