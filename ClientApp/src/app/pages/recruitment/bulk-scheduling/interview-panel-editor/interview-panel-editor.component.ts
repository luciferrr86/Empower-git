import { Component, OnInit, ViewChild } from '@angular/core';
import { InterviewPanalSchedule } from '../../../../models/recruitment/bulk-scheduling/interview-panel.model';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { IOption } from 'ng-select';
import { DropDownList } from '../../../../models/common/dropdown';
import { AlertService } from '../../../../services/common/alert.service';
import { BulkInterviewScheduleService } from '../../../../services/recruitment/bulk-interview-schedule.service';
import { Utilities } from '../../../../services/common/utilities';

@Component({
  selector: 'interview-panel-editor',
  templateUrl: './interview-panel-editor.component.html',
  styleUrls: ['./interview-panel-editor.component.css']
})
export class InterviewPanelEditorComponent implements OnInit {

  isSaving = false;
  public modalTitle: string;
  public interviewEdit: InterviewPanalSchedule = new InterviewPanalSchedule();
  public serverCallback: () => void;

  @ViewChild('editorModal')
  editorModal: ModalDirective;

  constructor(private alertService: AlertService, private bulkInterviewScheduleService: BulkInterviewScheduleService) { }

  allVacancy: Array<IOption> = [];
  allManager: Array<IOption> = [];
  allDate: Array<IOption> = [];

  ngOnInit() {
  }
  newInterviewers(vacancyList: DropDownList[], managerList: DropDownList[], dateList: DropDownList[]) {
    this.allVacancy = vacancyList;
    this.allManager = managerList;
    this.allDate = dateList;
    this.interviewEdit = new InterviewPanalSchedule();
    this.editorModal.show();
    this.modalTitle = "Add";
    return this.interviewEdit;
  }
  save() {
    this.isSaving = true;
    this.bulkInterviewScheduleService.saveInterviewPanal(this.interviewEdit).subscribe(sucess => this.saveSuccessHelper(sucess), error => this.saveFailedHelper(error));
  }

  private saveSuccessHelper(result?: string) {
    this.isSaving = false;
    this.alertService.showSucessMessage("Saved successfully");
    this.editorModal.hide();
    this.serverCallback();
  }


  private saveFailedHelper(error: any) {
    this.isSaving = false;
    let test = Utilities.getHttpResponseMessage(error);
    this.alertService.showInfoMessage("Please try later" + test[0]);
  }
}
