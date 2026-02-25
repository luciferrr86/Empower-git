import { Injectable, NgZone } from '@angular/core';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  public showLoader: boolean = false;
  private authState: any = null;

  constructor(private router: Router, public ngZone: NgZone) { }

  loginWithEmail(email: string, password: string): Promise<any> {
    this.showLoader = true;
    return new Promise((resolve) => {
      setTimeout(() => {
        this.authState = { email };
        this.showLoader = false;
        resolve({ user: this.authState });
      }, 800);
    });
  }

  singout(): void {
    this.authState = null;
    this.router.navigate(['/login']);
  }
}
