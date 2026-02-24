import { Component, OnInit, Input, ViewChild, Output, EventEmitter } from '@angular/core';
import { FormGroup, FormControl, FormBuilder, Validators } from '@angular/forms';
import { IOption } from '../../../../../../node_modules/ng-select';
import { Goal } from '../../../../models/performance/set-goal/set-goal.model';
import { SetGoalService } from '../../../../services/performance/set-goal/set-goal.service';
import { AlertService } from '../../../../services/common/alert.service';
import { AccountService } from '../../../../services/account/account.service';
import { ModalDirective } from '../../../../../../node_modules/ngx-bootstrap/modal';
import { Utilities } from '../../../../services/common/utilities';


@Component({
  selector: 'set-goal-editor',
  templateUrl: './set-goal-editor.component.html',
  styleUrls: ['./set-goal-editor.component.css']
})
export class SetGoalEditorComponent implements OnInit {
  public goal: Goal = new Goal()
  public isSaving = false;
  public isGroupEnabled: boolean = true;
  public isIndividualEnabled: boolean = true;

  public serverCallback: () => void;

  @ViewChild('editorModal') editorModal: ModalDirective;
  @Output() getMeasureId: EventEmitter<string> = new EventEmitter<string>();
  @Output() getGoalId: EventEmitter<string> = new EventEmitter<string>();

  @Input('goallist')
  public goalForm: FormGroup;

  @Input('goal')
  public performanceGoalList: Array<IOption>;

  @Input('quadrant')
  public performanceQuadrantList: Array<IOption>;

  @Input('individual')
  public individualList: Array<IOption>;

  @Input('group')
  public groupList: Array<IOption>;

  @Input('level')
  public levelList: Array<IOption>;

  @Input('goalRelease')
  public isGoalReleased: boolean;

  @Input('CEO')
  public isCEO: boolean;


  constructor(private fb: FormBuilder, private setGoalService: SetGoalService, private alertService: AlertService, private accountService: AccountService) { }

  ngOnInit() {
    if (this.isGoalReleased) {

      this.goalForm.disable();
    }

    if (this.goalForm.controls.setFunctionalGroupId.value.length > 0) {
      this.toggleIndividual(this.goalForm.controls.setFunctionalGroupId.value);
    }
    else {
      this.toggleGroup(this.goalForm.controls.setIndividual.value);
    }
  }

  toggleGroup(event) {
    if (event.length > 0) {
      this.isGroupEnabled = false;
    }
    else {
      this.isGroupEnabled = true;
    }
  }

  toggleIndividual(event) {
    if (event.length > 0) {
      this.isIndividualEnabled = false;
    }
    else {
      this.isIndividualEnabled = true;
    }
  }
  newGoalName() {
    this.editorModal.show();
  }
  public save() {
    this.isSaving = true;
    this.setGoalService.create(this.goal, this.accountService.currentUser.id).subscribe(sucess => this.saveSuccessHelper(sucess), error => this.saveFailedHelper(error));
    this.isSaving = false;
  }

  private saveSuccessHelper(result?: any) {
    this.isSaving = false;
    this.getGoalId.emit(result);
    this.alertService.showSucessMessage("Saved successfully");
    this.editorModal.hide();
  }
  private saveFailedHelper(error: any) {
    this.isSaving = false;
    let test = Utilities.getHttpResponseMessage(error);
    this.alertService.showInfoMessage("Please try later" + test[0]);
  }

  removeGoalMeasure(id: string) {

    this.getMeasureId.emit(id);
  }
}
