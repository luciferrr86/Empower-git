import { NgModule } from '@angular/core';
import { CommonModule, DatePipe } from '@angular/common';
import { ProfileComponent } from './profile.component';
import {RouterModule, Routes} from '@angular/router';
import {SharedModule} from '../../../shared/shared.module';
import {FormsModule,ReactiveFormsModule} from '@angular/forms';
import {QuillEditorModule} from 'ngx-quill-editor';
import {HttpModule} from '@angular/http';
import {DataTableModule} from 'angular2-datatable';
import {AngularEchartsModule} from 'ngx-echarts';
import { PersonalDetailComponent } from '../personal-detail/personal-detail.component';
import { ProfessionalDetailComponent } from '../professional-detail/professional-detail.component';
import { EducationalDetailComponent } from '../educational-detail/educational-detail.component';
import { ProfileService } from '../../../services/maintenance/profile.service';
import { AccountEndpoint } from '../../../services/account/account-endpoint.service';
import { AccountService } from '../../../services/account/account.service';
import { ProfessionalDetailEditorComponent } from '../professional-detail-editor/professional-detail-editor.component';
import { ProfilePictureComponent } from '../profile-picture/profile-picture.component';


export const profileRoutes: Routes = [
  {
    path: '',
    component: ProfileComponent,
    data: {
      breadcrumb: 'User Profile',
      icon: 'icofont-justify-all bg-c-green',
      status: true
    }
  }
];

@NgModule({
  imports: [
    CommonModule,
    RouterModule.forChild(profileRoutes),
    SharedModule,
    FormsModule,
    QuillEditorModule,
    HttpModule,
    DataTableModule,
    AngularEchartsModule,
    ReactiveFormsModule
  ],
  declarations: [ProfileComponent,ProfilePictureComponent, PersonalDetailComponent,ProfessionalDetailEditorComponent, ProfessionalDetailComponent, EducationalDetailComponent],
  exports:[ProfilePictureComponent],
  providers:[ProfileService,AccountService,AccountEndpoint,DatePipe]
})
export class ProfileModule { }
