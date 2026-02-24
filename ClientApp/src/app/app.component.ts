import { Component, ViewEncapsulation } from '@angular/core';
import { Router, NavigationStart } from '@angular/router';
import { AuthService } from './services/common/auth.service';
import { MessageSeverity, AlertMessage, DialogType, AlertDialog, AlertService } from './services/common/alert.service';
import { ToastData, ToastOptions, ToastyConfig, ToastyService } from 'ng2-toasty';

@Component({
  selector: 'app-root',
  template: `<router-outlet><app-spinner></app-spinner></router-outlet>`,
  styleUrls: [
    './app.component.css'

  ]
})
export class AppComponent {
  title = 'app';
  isUserLoggedIn: boolean;

  constructor(private alertService: AlertService, private toastyConfig: ToastyConfig, private toastyService: ToastyService,
    private authService: AuthService, public router: Router) {
  }
  ngOnInit() {
    this.isUserLoggedIn = this.authService.isLoggedIn;
    this.authService.getLoginStatusEvent().subscribe(isLoggedIn => {
      this.isUserLoggedIn = isLoggedIn;
    });

    this.router.events.subscribe(event => {
      if (event instanceof NavigationStart) {
        let url = (<NavigationStart>event).url;

        if (url !== url.toLowerCase()) {
          this.router.navigateByUrl((<NavigationStart>event).url.toLowerCase());
        }
      }
    });
  }
}
