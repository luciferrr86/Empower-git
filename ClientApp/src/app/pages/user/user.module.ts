import { NgModule } from '@angular/core';
import { CommonModule, DatePipe } from '@angular/common';
import { UserComponent } from './user.component';
import { RouterModule, Routes } from '@angular/router';
import { SharedModule } from '../../shared/shared.module';
import { AccountService } from '../../services/account/account.service';
import { ChangePasswordComponent } from './change-password/change-password.component';
import { ChangePasswordService } from '../../services/profile/change-password.service';
import { AccountEndpoint } from '../../services/account/account-endpoint.service';
import { FileUploadService } from '../../services/file-upload/file-upload.service';
import { ProfilePictureComponent } from './profile-picture/profile-picture.component';

export const UserRoutes: Routes = [
  {
    path: '',
    data: {
      breadcrumb: 'User Profile',
      status: false
    },
    children: [
      {
        path: 'profile',
       loadChildren: './profile/profile.module#ProfileModule'
      },
      { path: "change-password", component: ChangePasswordComponent, data: { title: "Change Password" } },
    ]
  }
];

@NgModule({
  imports: [
    CommonModule,
    RouterModule.forChild(UserRoutes),
    SharedModule
  ],
  providers:[FileUploadService,AccountService,ChangePasswordService,AccountEndpoint,DatePipe],
  declarations: [UserComponent, ChangePasswordComponent]
})
export class UserModule { }
