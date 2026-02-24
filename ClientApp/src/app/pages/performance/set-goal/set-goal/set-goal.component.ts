import { Component, OnInit, ViewChild } from '@angular/core';
import { FormGroup, FormBuilder, FormArray } from '@angular/forms';
import { SetGoalService } from '../../../../services/performance/set-goal/set-goal.service';
import { AlertService } from '../../../../services/common/alert.service';
import { SetGoalModel, SetGoal, ReleaseGoalMessage } from '../../../../models/performance/set-goal/set-goal.model';
import { AccountService } from '../../../../services/account/account.service';
import { SetGoalEditorComponent } from '../set-goal-editor/set-goal-editor.component';
@Component({
  selector: 'app-set-goal',
  templateUrl: './set-goal.component.html',
  styleUrls: ['./set-goal.component.css']
})
export class SetGoalComponent implements OnInit {

  @ViewChild("goalEditor") goalEditor: SetGoalEditorComponent;

  public index: number;
  public goalElement: any;
  public setGoalForm: FormGroup;
  public performanceGoalList = [];
  public performanceQuadrantList = [];
  public individualList = [];
  public groupList = [];
  public levelList = [];
  public EmpNames = "";
  public setGoal: SetGoalModel = new SetGoalModel();
  public isPerformanceStarted: boolean;
  public isManagerRealeased: boolean;
  public isGoalReleased: boolean;
  public isCEO: boolean;
  constructor(private _fb: FormBuilder, private setGoalService: SetGoalService, private alertService: AlertService, private accountService: AccountService) { }

  ngOnInit() {
    this.setGoalForm = this._fb.group({
      searchIndiGroupId: '',
      searchFunGroupId: '',
      searchLevelId: '',
      lstSetGoal: this._fb.array([
        this.initGoal(),
      ])
    });
    this.getSetGoalList();

  }

  initGoal() {
    return this._fb.group({
      firstQuadrantId: '',
      secondQuadrantId: '',
      goalMeasure: '',
      performanceGoalId: '',
      setIndividual: [''],
      setFunctionalGroupId: [''],
      levelId: '',
      performanceGoalMeasureId: ''
    });

  }

  addGoal() {

    const control = <FormArray>this.setGoalForm.controls['lstSetGoal'];
    control.push(this.initGoal());
  }

  removeGoalMeasure($event, i) {
    if ($event != "" && $event != null) {
      this.setGoalService.deleteGoalMeasure($event).subscribe(sucess => this.deleteSuccessHelper(i), error => this.saveFailedHelper("Couldn't delete successfully."));
    }
    else {
      const control = <FormArray>this.setGoalForm.controls['lstSetGoal'];
      control.removeAt(i);
    }


  }
  private deleteSuccessHelper(i: number) {
    const control = <FormArray>this.setGoalForm.controls['lstSetGoal'];
    control.removeAt(i);
    this.alertService.showInfoMessage("Deleted Successfully");
  }

  private saveFailedHelper(errMsg: string) {
    this.alertService.showInfoMessage(errMsg);
  }

  get lstSetGoal() {
    return this.setGoalForm.get('lstSetGoal') as FormArray;
  }
  getSetGoalList() {
    this.setGoalService.getGoalList(this.accountService.currentUser.id, "0").subscribe(result => this.onSuccessfulDataLoad(result), error => this.onDataLoadFailed());
  }
  getFilteredSetGoalList(val: string) {
    this.setGoalService.getGoalList(this.accountService.currentUser.id, val).subscribe(result => this.onSuccessfulDataLoad(result), error => this.onDataLoadFailed());
  }
  onSuccessfulDataLoad(setGoal: SetGoalModel) {
    this.setGoal = setGoal;
    this.isCEO = setGoal.isCEO;
    this.isPerformanceStarted = setGoal.isPerformanceStarted;
    this.isManagerRealeased = setGoal.isManagerRealeased;
    this.isGoalReleased = setGoal.isGoalReleased;
    this.setGoalForm.patchValue({
      searchIndividualGroup: this.setGoal.searchIndividualGroup, searchIndiGroupId: this.setGoal.searchIndiGroupId,
      searchFunctionalGroup: this.setGoal.searchFunctionalGroup, searchFunGroupId: this.setGoal.searchFunGroupId, searchLevel: this.setGoal.searchLevel, searchLevelId: this.setGoal.searchLevelId
    });

    if (setGoal.lstSetGoal != null && setGoal.lstSetGoal.length > 0) {
      this.performanceGoalList = this.setGoal.lstSetGoal[0].selectedPerformanceGoal;
      this.performanceQuadrantList = this.setGoal.lstSetGoal[0].selectedQuadrant;
      this.individualList = this.setGoal.lstSetGoal[0].selectedIndividualManager;
      this.groupList = this.setGoal.lstSetGoal[0].selectedFunctionalGroup;
      this.levelList = this.setGoal.lstSetGoal[0].selectedLevel;
      this.setGoals(this.setGoal.lstSetGoal);
    }
  }
  setGoals(goal: SetGoal[]) {
    let control = <FormArray>this.setGoalForm.controls.lstSetGoal;
    if (control.length > 0) {
      for (var i = 0; i <= control.length; i++) {
        control.controls.splice(0);
      }
    }

    goal.forEach(x => {
      control.push(this._fb.group({
        performanceGoalId: x.performanceGoalId,
        firstQuadrantId: x.firstQuadrantId,
        secondQuadrantId: x.secondQuadrantId,
        setIndividual: [x.setIndividual],
        setFunctionalGroupId: [x.setFunctionalGroupId],
        levelId: x.levelId,
        goalMeasure: x.goalMeasure,
        performanceGoalMeasureId: x.performanceGoalMeasureId
      }))
    });


  }
  onDataLoadFailed() {
    this.alertService.showInfoMessage("Unable to retrieve list from the server");
  }

  filterByGroup() {


  }

  saveGoal() {
    this.setGoal = this.setGoalForm.value;
    this.setGoalService.saveGoalList(this.accountService.currentUser.id, this.setGoal).subscribe(result => this.onSaveSuccessfulDataLoad(), error => this.onSaveDataLoadFailed());
  }
  onSaveSuccessfulDataLoad() {
    this.alertService.showInfoMessage("Goals saved successfully.");
    this.getSetGoalList();
  }

  onSaveDataLoadFailed() {
    this.alertService.showInfoMessage("Unable to save the goal list");
  }

  releaseGoal() {
    this.setGoalService.release(this.accountService.currentUser.id).subscribe(sucess => this.releaseSuccessHelper(sucess), error => this.saveFailedHelper("Couldn't released."));
  }
  private releaseSuccessHelper(result: ReleaseGoalMessage) {

    if (result.status == 1) {
      this.alertService.showInfoMessage("Please set initial rating for all your subordinates before releasing.");
    }
    else if (result.status == 2) {
      for (var i = 0; i < result.lstEmpName.length; i++) {
        if (this.EmpNames == "") {
          this.EmpNames = result.lstEmpName[i];
        }
        else {
          this.EmpNames = this.EmpNames + "," + result.lstEmpName[i];
        }

      }
      this.alertService.showInfoMessage("You haven't set goal for these employees:" + this.EmpNames);
    }
    else if (result.status == 3) {
      this.getSetGoalList();
    }
    else if (result.status == 4) {
      for (var i = 0; i < result.lstEmpName.length; i++) {
        if (this.EmpNames == "") {
          this.EmpNames = result.lstEmpName[i];
        }
        else {
          this.EmpNames = this.EmpNames + "," + result.lstEmpName[i];
        }

      }
      this.alertService.showInfoMessage("Please assign president for these employees:" + this.EmpNames);
    }
    else {
      this.alertService.showInfoMessage("Goals are not released.");
    }
  }

  newGoalName(i) {
    this.index = i;
    this.goalEditor.newGoalName();
  }

  setGoalName($event) {
    this.goalElement = $event;
    if (this.index != null && this.goalElement != null) {
      let control = <FormArray>this.setGoalForm.controls.lstSetGoal.value[this.index];
      this.performanceGoalList.push({ value: this.goalElement.goalId, label: this.goalElement.goalName })

      control.controls[this.index].value.performanceGoalList = this.performanceGoalList;
      control.controls[this.index].value.performanceGoalId = this.goalElement.goalId;

      control.controls[this.index].value.performanceGoalList.active = this.goalElement;

      control.controls[this.index].updateValueAndValidity();

    }

  }
}
