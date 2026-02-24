import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BlogComponent } from './blog.component';
import { RouterModule, Routes } from '@angular/router';
import { BlogListComponent } from './blog-list/blog-list.component';
import { BlogCreateComponent } from './blog-create/blog-create.component';
import { SharedModule } from '../../shared/shared.module';
import { DataTableModule } from 'angular2-datatable';
import { NgxDatatableModule } from '@swimlane/ngx-datatable';
import { ReactiveFormsModule } from '@angular/forms';
import { ToastyModule } from 'ng2-toasty';
import { NotificationsModule } from '../ui-elements/advance/notifications/notifications.module';
import { AccountService } from '../../services/account/account.service';
import { AlertService } from '../../services/common/alert.service';
import { AccountEndpoint } from '../../services/account/account-endpoint.service';
import { BlogService } from '../../services/blog/blog.service';
import { UiSwitchModule } from 'ng2-ui-switch';
import { QuillModule } from 'ngx-quill';

export const BlogRoutes: Routes = [
      { path: "", component: BlogListComponent, data: { title: "Blog List" } },
      { path: "blog-create", component: BlogCreateComponent, data: { title: "Blog" } },
      { path: "blog-create/:id", component: BlogCreateComponent, data: { title: "Blog" } }
  //{path:"",component:BlogListComponent,data:{title:"Blog List"}},
  //// { path: "blog-list", component: BlogListComponent, data: { title: "Blog List" } },
  //{ path: 'blog-create', component: BlogCreateComponent }
];

@NgModule({
  imports: [
    CommonModule,
    RouterModule.forChild(BlogRoutes),
    SharedModule,
    DataTableModule,
    NgxDatatableModule,
    ReactiveFormsModule,
    ToastyModule.forRoot(),
    NotificationsModule,
    UiSwitchModule,
    QuillModule
  ],
  providers: [AccountService, AlertService, AccountEndpoint, BlogService],
  declarations: [
    BlogComponent,
    BlogCreateComponent,
    BlogListComponent
  ]
})
export class BlogModule { }
