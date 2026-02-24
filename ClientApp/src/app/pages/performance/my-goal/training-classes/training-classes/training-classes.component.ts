import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators, FormArray } from '../../../../../../../node_modules/@angular/forms';
import { TrainingClasses, TrainingClassesModel } from '../../../../../models/performance/common/training-classes.model';
import { MyGoalService } from '../../../../../services/performance/my-goal/my-goal.service';
import { AlertService } from '../../../../../services/common/alert.service';
import { AccountService } from '../../../../../services/account/account.service';

@Component({
  selector: 'training-classes',
  templateUrl: './training-classes.component.html',
  styleUrls: ['./training-classes.component.css']
})
export class TrainingClassesComponent implements OnInit {
  public trainingForm: FormGroup;
  public isSaving: boolean;
  public isSubmitting: boolean;
  public trainingClasses: TrainingClassesModel = new TrainingClassesModel();
  constructor(private _fb: FormBuilder, private myGoalService: MyGoalService, private alertService: AlertService, private accountService: AccountService) { }

  ngOnInit() {
    this.trainingForm = this._fb.group({
      instructionText: '',
      lstTrainingClasses: this._fb.array([
        this.initTrainingClasses()
      ])
    });
    this.getTrainingClassesList();
  }

  initTrainingClasses() {
    return this._fb.group({
      trainingClassId: '',
      trainingClass: ''
    });
  }

  addTrainingClasses() {
    const control = <FormArray>this.trainingForm.controls['lstTrainingClasses'];
    control.push(this.initTrainingClasses());
  }

  removeTrainingClasses($event, i) {
    if ($event != "" && $event != null) {
      this.myGoalService.deleteTrainingClasses($event).subscribe(sucess => this.deleteSuccessHelper(sucess, i), error => this.deleteFailedHelper(error));
    }
    else {
      const control = <FormArray>this.trainingForm.controls['lstTrainingClasses'];
      control.removeAt(i);
    }


  }

  private deleteSuccessHelper(res: any, i: number) {
    const control = <FormArray>this.trainingForm.controls['lstTrainingClasses'];
    control.removeAt(i);
    this.alertService.showInfoMessage("Deleted Successfully");
  }

  get lstTrainingClasses() {
    return this.trainingForm.get('lstTrainingClasses') as FormArray;
  }
  getTrainingClassesList() {
    this.myGoalService.getTrainingClassesList(this.accountService.currentUser.id).subscribe(result => this.onSuccessfulDataLoad(result), error => this.onDataLoadFailed(error));
  }

  onSuccessfulDataLoad(trainingClasses: TrainingClassesModel) {
    this.trainingClasses = trainingClasses;
    this.trainingForm.patchValue({ instructionText: this.trainingClasses.instructionText });
    this.setTrainingClasses(this.trainingClasses.lstTrainingClasses);
  }
  setTrainingClasses(training: TrainingClasses[]) {
    let control = <FormArray>this.trainingForm.controls.lstTrainingClasses;

    if (control.length > 0) {
      for (var i = 0; i <= control.length; i++) {
        control.controls.splice(0);
      }
    }

    training.forEach(x => {
      control.push(this._fb.group({
        trainingClassId: x.trainingClassId,
        trainingClass: x.trainingClass
      }))
    });

  }
  onDataLoadFailed(error: any) {
    this.alertService.showInfoMessage("Unable to retrieve list from the server");
  }
  saveTrainingClasses() {
    this.isSaving = true;
    this.trainingClasses.lstTrainingClasses = this.trainingForm.controls.lstTrainingClasses.value;
    this.myGoalService.saveTrainingClasses(this.accountService.currentUser.id, this.trainingClasses, 'save').subscribe(result => this.onSaveSuccessfulDataLoad(result, 'save'), error => this.onSaveDataLoadFailed(error));
  }

  submitTrainigClasses() {
    this.isSubmitting = true;
    this.trainingClasses.lstTrainingClasses = this.trainingForm.controls.lstTrainingClasses.value;
    this.myGoalService.saveTrainingClasses(this.accountService.currentUser.id, this.trainingClasses, 'submit').subscribe(result => this.onSaveSuccessfulDataLoad(result, 'submit'), error => this.onSaveDataLoadFailed(error));
  }
  onSaveSuccessfulDataLoad(res: any, actionType: string) {
    this.getTrainingClassesList();
    if (actionType == "save") {
      this.isSaving = false;
      this.alertService.showInfoMessage("Training and classes saved successfully.");
    }
    else {
      this.isSubmitting = false;
      this.alertService.showInfoMessage("Training and classes submitted successfully.");
    }

  }

  onSaveDataLoadFailed(error: any) {
    this.isSaving = false;
    this.alertService.showInfoMessage("Unable to save the training and classes.");
  }

  private deleteFailedHelper(error: any) {
    this.alertService.showInfoMessage("Couldn't delete successfully.");
  }
}
