import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NotificationsComponent } from './notifications.component';
import {RouterModule, Routes} from '@angular/router';
import {SharedModule} from '../../../../shared/shared.module';
import {ToastyModule} from 'ng2-toasty';



@NgModule({
  imports: [
    CommonModule,
    SharedModule,
    ToastyModule.forRoot()
  ],
  declarations: [NotificationsComponent],
  exports:[NotificationsComponent]

})
export class NotificationsModule { }
