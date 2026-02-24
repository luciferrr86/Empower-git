import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { EmployeeListComponent } from './hr-view/employee-list/employee-list.component';
import { ManagerListComponent } from './hr-view/manager-list/manager-list.component';
import { Routes, RouterModule } from '@angular/router';
import { NgxDatatableModule } from '@swimlane/ngx-datatable';
import { DataTableModule } from 'angular2-datatable';
import { HttpModule } from '@angular/http';
import { SharedModule } from '../../shared/shared.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { QuillEditorModule } from 'ngx-quill-editor';
import { TagInputModule } from 'ngx-chips';
import { AngularEchartsModule } from 'ngx-echarts';
import { FormWizardModule } from 'angular2-wizard/dist';
import { SelectModule } from 'ng-select';
import { SqueezeBoxModule } from 'squeezebox';
import { MyGoalComponent } from './my-goal/my-goal/my-goal.component';
import { SetGoalComponent } from './set-goal/set-goal/set-goal.component';
import { ReviewGoalComponent } from './review-goal/review-goal/review-goal.component';
import { SetGoalEditorComponent } from './set-goal/set-goal-editor/set-goal-editor.component';
import { EmpDetailComponent } from './my-goal/emp-detail/emp-detail.component';
import { PerformanceGoalComponent } from './my-goal/performance-goal/performance-goal.component';
import { DeltaPlusesComponent } from './my-goal/delta-pluses/delta-pluses.component';
import { ReviewEmpDetailComponent } from './review-goal/review-emp-detail/review-emp-detail.component';
import { ReviewPerformanceGoalComponent } from './review-goal/review-performance-goal/review-performance-goal.component';
import { ReviewDeltaPlusesComponent } from './review-goal/review-delta-pluses/review-delta-pluses.component';
import { ReviewTrainingClassesComponent } from './review-goal/review-training-classes/review-training-classes.component';
import { ReviewReviewRatingComponent } from './review-goal/review-review-rating/review-review-rating.component';
import { HrViewService } from '../../services/performance/hr-view/hr-view.service';
import { HrViewComponent } from './hr-view/hr-view.component';
import { ExternalFeedbackComponent } from './my-goal/external-feedback/external-feedback.component';
import { SetGoalService } from '../../services/performance/set-goal/set-goal.service';
import { MyGoalService } from '../../services/performance/my-goal/my-goal.service';
import { ReviewGoalService } from '../../services/performance/review-goal/review-goal.service';
import { TrainingClassesComponent } from './my-goal/training-classes/training-classes/training-classes.component';
import { TrainingClassesEditorComponent } from './my-goal/training-classes/training-classes-editor/training-classes-editor.component';
import { DevelopmentPlanComponent } from './my-goal/development-plan/development-plan/development-plan.component';
import { DevelopmentPlanEditorComponent } from './my-goal/development-plan/development-plan-editor/development-plan-editor.component';
import { RatingComponent } from './my-goal/rating/rating.component';
import { ReviewDevelopmentPlanEditorComponent } from './review-goal/review-development-plan/review-development-plan-editor/review-development-plan-editor.component';
import { ReviewDevelopmentPlanComponent } from './review-goal/review-development-plan/review-development-plan/review-development-plan.component';

export const PerformanceRoutes: Routes = [
  { path: "hr-view", component: HrViewComponent, data: { title: "HR View" } },
  { path: "set-goal", component: SetGoalComponent, data: { title: "Set Goal" } },
  { path: "my-goal", component: MyGoalComponent, data: { title: "My Goal" } },
  { path: "review-goal", component: ReviewGoalComponent, data: { title: "Review Goal" } },
  { path: "review-emp-detail", component: ReviewEmpDetailComponent }
];


@NgModule({
  imports: [
    CommonModule,
    RouterModule.forChild(PerformanceRoutes),
    HttpModule,
    SharedModule,
    DataTableModule,
    FormsModule,
    NgxDatatableModule,
    ReactiveFormsModule,
    QuillEditorModule,
    TagInputModule,
    AngularEchartsModule,
    FormWizardModule,
    SelectModule,
    SqueezeBoxModule

  ],
  providers: [HrViewService, SetGoalService, MyGoalService, ReviewGoalService],
  declarations: [EmployeeListComponent,
    ManagerListComponent,
    HrViewComponent,
    MyGoalComponent,
    SetGoalComponent,
    ReviewGoalComponent,
    SetGoalEditorComponent,
    EmpDetailComponent,
    PerformanceGoalComponent,
    DeltaPlusesComponent,
    TrainingClassesComponent,
    DevelopmentPlanComponent,
    DevelopmentPlanEditorComponent,
    RatingComponent,
    ReviewEmpDetailComponent,
    ReviewPerformanceGoalComponent,
    ReviewDeltaPlusesComponent,
    ReviewTrainingClassesComponent,
    ReviewDevelopmentPlanComponent,
    ExternalFeedbackComponent,   
    TrainingClassesEditorComponent,
    ReviewReviewRatingComponent,
    ReviewDevelopmentPlanEditorComponent
  ]
})
export class PerformanceModule { }
