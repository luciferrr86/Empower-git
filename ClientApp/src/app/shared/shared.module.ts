import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import {ToggleFullscreenDirective} from './fullscreen/toggle-fullscreen.directive';
import {AccordionAnchorDirective} from './accordion/accordionanchor.directive';
import {AccordionLinkDirective} from './accordion/accordionlink.directive';
import {AccordionDirective} from './accordion/accordion.directive';

import {NgbModule} from '@ng-bootstrap/ng-bootstrap';
import {ScrollModule} from './scroll/scroll.module';

import {MenuItems,CandidateMenuItems} from './menu-items/menu-items';
import {SpinnerComponent} from './spinner/spinner.component';
import {CardComponent} from './card/card.component';
import {CardRefreshDirective} from './card/card-refresh.directive';
import {CardToggleDirective} from './card/card-toggle.directive';
import {ModalAnimationComponent} from './modal-animation/modal-animation.component';
import {ModalBasicComponent} from './modal-basic/modal-basic.component';
import {DataFilterPipe} from "./element/data-filter.pipe";
import { LocalStoreManager } from '../services/common/local-store-manager.service';
import { ConfigurationService } from '../services/common/configuration.service';
import { AlertService } from '../services/common/alert.service';
import { AuthService } from '../services/common/auth.service';
import { EndpointFactory } from '../services/common/endpoint-factory.service';
import { HttpClientModule } from '@angular/common/http';
import { DataTable, DataTableModule } from 'angular2-datatable';
import { FormsModule } from '@angular/forms';
import { NgxDatatableModule } from '@swimlane/ngx-datatable';
import { FileUploadComponent } from './file-upload/file-upload.component';
import { QuillModule } from 'ngx-quill';

@NgModule({
  imports: [
    CommonModule,
    HttpClientModule,
    ScrollModule,
    NgbModule.forRoot()
  ],
  declarations: [
    AccordionAnchorDirective,
    AccordionLinkDirective,
    AccordionDirective,
    ToggleFullscreenDirective,
    CardRefreshDirective,
    CardToggleDirective,
    SpinnerComponent,
    CardComponent,
    ModalAnimationComponent,
    ModalBasicComponent,
    DataFilterPipe,
    FileUploadComponent,
  ],
  exports: [
    AccordionAnchorDirective,
    AccordionLinkDirective,
    AccordionDirective,
    ToggleFullscreenDirective,
    CardRefreshDirective,
    CardToggleDirective,
    ScrollModule,
    NgbModule,
    SpinnerComponent,
    CardComponent,
    ModalAnimationComponent,
    ModalBasicComponent,
    DataFilterPipe,
    DataTableModule,
    FormsModule,
    NgxDatatableModule,
    FileUploadComponent,
    QuillModule
  ],
  providers: [
    MenuItems,
    CandidateMenuItems,
    LocalStoreManager,
    AlertService,
    AuthService,
    ConfigurationService,
    EndpointFactory
  ]
})
export class SharedModule { }
