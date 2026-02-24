import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, FormArray } from '@angular/forms';
import { PerformanceConfig } from '../../../../models/configuration/performance/performance-config.model';
import { PerformaceConfigurationService } from '../../../../services/configuration/performance/performace-configuration.service';
import { AlertService } from '../../../../services/common/alert.service';

@Component({
  selector: 'app-performance-config',
  templateUrl: './performance-config.component.html',
  styleUrls: ['./performance-config.component.css']
})
export class PerformanceConfigComponent implements OnInit {
  public configForm: FormGroup;
  public isSaving = false;
  public perConfig: PerformanceConfig = new PerformanceConfig();
  constructor(private _fb: FormBuilder, private alertService: AlertService, private performanceConfigService: PerformaceConfigurationService) { }

  ngOnInit() {
    this.configForm = this._fb.group({
      myGoalInstructionText: [''],
      careerDevInstructionText: [''],
      trainingClassesInstructionText: [''],
      isPerformaceStart: [],
      performanceConfigFeebackViewModel: this._fb.array([
        this.initFeedback(),
      ]),
      performanceConfigRatingViewModel: this._fb.array([
        this.initRating(),
      ])
    });

    this.getPerformanceConfig();

  }


  getPerformanceConfig() {
    this.performanceConfigService.getPerformanceConfig().subscribe(result => this.onSuccessfulDataLoad(result), error => this.onDataLoadFailed());
  }

  onSuccessfulDataLoad(PerformaceConfig: PerformanceConfig) {
    this.perConfig = PerformaceConfig;
    this.configForm.patchValue({
      myGoalInstructionText: this.perConfig.myGoalInstructionText,
      careerDevInstructionText: this.perConfig.careerDevInstructionText,
      trainingClassesInstructionText: this.perConfig.trainingClassesInstructionText,
      isPerformaceStart: this.perConfig.isPerformanceStart
    });
    if (PerformaceConfig.performanceConfigFeebackViewModel.length > 0) {
      this.setFeedbackText(this.perConfig.performanceConfigFeebackViewModel);
    }
    if (PerformaceConfig.performanceConfigRatingViewModel.length > 0) {
      this.setRatingText(this.perConfig.performanceConfigRatingViewModel);
    }
  }

  setFeedbackText(feedback: any[]) {

    let control = <FormArray>this.configForm.controls.performanceConfigFeebackViewModel;
    if (control.length > 0) {
      for (var i = 0; i <= control.length; i++) {
        control.controls.splice(0);
      }
    }
    feedback.forEach(x => {
      control.push(this._fb.group({ id: x.id, labelText: x.labelText }))
    });

  }

  setRatingText(rating: any[]) {
    let control = <FormArray>this.configForm.controls.performanceConfigRatingViewModel;
    if (control.length > 0) {
      for (var i = 0; i <= control.length; i++) {
        control.controls.splice(0);
      }
    }
    rating.forEach(x => {
      control.push(this._fb.group({ id: x.id, ratingName: x.ratingName, ratingDescription: x.ratingDescription }))
    });

  }
  onDataLoadFailed() {
    this.alertService.showInfoMessage("Unable to retrieve list from the server");
  }
  initFeedback() {
    return this._fb.group({
      id: [''],
      labelText: ['']
    });
  }

  addFeedback() {
    const control = <FormArray>this.configForm.controls['performanceConfigFeebackViewModel'];
    control.push(this.initFeedback());
  }

  removeFeedback($event, i) {

    if ($event != "" && $event != null) {
      this.performanceConfigService.deleteFeedback($event).subscribe(sucess => this.deleteSuccessHelper(i, 'performanceConfigFeebackViewModel'), error => this.saveFailedHelper("Couldn't delete successfully."));;

    }
    else {
      const control = <FormArray>this.configForm.controls['performanceConfigFeebackViewModel'];
      control.removeAt(i);
    }


  }
  get performanceConfigFeebackViewModel() {
    return this.configForm.get('performanceConfigFeebackViewModel') as FormArray;
  }

  initRating() {

    return this._fb.group({
      id: [''],
      ratingName: [''],
      ratingDescription: ['']
    });
  }

  addRating() {
    const control = <FormArray>this.configForm.controls['performanceConfigRatingViewModel'];
    control.push(this.initRating());
  }

  removeRating($event, i) {
    if ($event != "" && $event != null) {
      this.performanceConfigService.deleteRating($event).subscribe(sucess => this.deleteSuccessHelper(i, 'performanceConfigRatingViewModel'), error => this.saveFailedHelper("Couldn't delete successfully."));;

    }
    else {
      const control = <FormArray>this.configForm.controls['performanceConfigRatingViewModel'];
      control.removeAt(i);
    }

  }
  get performanceConfigRatingViewModel() {
    return this.configForm.get('performanceConfigRatingViewModel') as FormArray;
  }
  save() {

    this.isSaving = true;
    this.perConfig.myGoalInstructionText = this.configForm.controls['myGoalInstructionText'].value;
    this.perConfig.careerDevInstructionText = this.configForm.controls['careerDevInstructionText'].value;
    this.perConfig.trainingClassesInstructionText = this.configForm.controls['trainingClassesInstructionText'].value;
    this.perConfig.performanceConfigFeebackViewModel = this.configForm.controls['performanceConfigFeebackViewModel'].value;
    this.perConfig.performanceConfigRatingViewModel = this.configForm.controls['performanceConfigRatingViewModel'].value;
    this.performanceConfigService.savePerformanceConfig(this.perConfig, "").subscribe(sucess => this.saveSuccessHelper(), error => this.saveFailedHelper("Couldn't saved successfully."));
  }
  private saveSuccessHelper() {
    this.isSaving = false;
    this.alertService.showInfoMessage("Configuration saved successfully.");
    this.getPerformanceConfig();
  }

  private deleteSuccessHelper(i: number, ctrl: string) {
    const control = <FormArray>this.configForm.controls[ctrl];
    control.removeAt(i);
    this.alertService.showInfoMessage("Deleted Successfully");
    this.getPerformanceConfig();
  }
  private saveFailedHelper(errMsg: string) {
    this.isSaving = false;
    this.alertService.showInfoMessage(errMsg);
  }

}
