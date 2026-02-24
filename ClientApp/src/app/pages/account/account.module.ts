import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {RouterModule, Routes} from '@angular/router';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import { ResetPasswordComponent } from './reset-password/reset-password.component';
import { ForgotPasswordService } from '../../services/account/forgot-password.service';

export const AccountRoutes: Routes = [
  {
    path: '',
    children: [
      {
        path: 'login',
        loadChildren: './login/login.module#LoginModule',
        data: {
          breadcrumb: 'Login'
        }
      },
      {
        path: 'reset-password',
        component: ResetPasswordComponent,
      },
      {
        path: 'forgot',
        loadChildren: './forgot/forgot.module#ForgotModule'
      }, {
        path: 'lock-screen',
        loadChildren: './lock-screen/lock-screen.module#LockScreenModule'
      }
    ]
  }
];

@NgModule({
  imports: [
    CommonModule,
    RouterModule.forChild(AccountRoutes),
    FormsModule,
    ReactiveFormsModule
  ],
  declarations: [ResetPasswordComponent],
  providers: [
   ForgotPasswordService
  ],
})
export class AccountModule { }
