import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {RouterModule, Routes} from '@angular/router';
import {SharedModule} from '../../../shared/shared.module';
import { LoginComponent } from './login/login.component';
import {FormsModule} from '@angular/forms';
import { NotificationsModule } from '../../ui-elements/advance/notifications/notifications.module';

export const LoginRoutes: Routes = [
  {
    path: '',
    data: {
      breadcrumb: 'Login'
    },
    children: [
      {
        path: '',
        component: LoginComponent,
        data: {
          breadcrumb: 'Login'
        }
      }
      
    ]
  }
];

@NgModule({
  imports: [
    CommonModule,
    RouterModule.forChild(LoginRoutes),
    SharedModule,
    FormsModule,
    NotificationsModule
  ],
  declarations: [LoginComponent]
})
export class LoginModule { }
