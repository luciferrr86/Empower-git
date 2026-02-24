import { Component, OnInit, ViewChild } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { LeaveHolidayListService } from '../../../../services/configuration/leave/leave-holiday-list.service';
import { Holiday } from '../../../../models/configuration/leave/leave-holiday.model';
import { AlertService } from '../../../../services/common/alert.service';
import { Utilities } from '../../../../services/common/utilities';

@Component({
  selector: 'leave-holidaylist-info',
  templateUrl: './leave-holidaylist-info.component.html',
  styleUrls: ['./leave-holidaylist-info.component.css']
})
export class LeaveHolidaylistInfoComponent implements OnInit {

  public isSaving = false;
  private isNew = false;
  public modalTitle = "";
  public holidayEdit: Holiday = new Holiday();
  public serverCallback: () => void;
  constructor(private leaveholidayListService: LeaveHolidayListService, private alertService: AlertService) { }

  @ViewChild('editorModal')
  editorModal: ModalDirective;

  ngOnInit() {
  }
  createHolidayList() {
    this.holidayEdit = new Holiday();
    this.editorModal.show();
    this.isNew = true;
    this.modalTitle = "Add";
    return this.holidayEdit;
  }

  editHolidayList(holiday: Holiday) {
    this.isNew = false;
    this.editorModal.show();
    this.modalTitle = "Edit";
    if (holiday) {
      this.holidayEdit = new Holiday();
      (<any>Object).assign(this.holidayEdit, holiday);
      return this.holidayEdit;
    }
  }

  public save() {
    this.isSaving = true;
    if (this.isNew) {
      this.leaveholidayListService.create(this.holidayEdit).subscribe(sucess => this.saveSuccessHelper(sucess), error => this.saveFailedHelper(error));
    }
    else {
      this.leaveholidayListService.update(this.holidayEdit, this.holidayEdit.id).subscribe(sucess => this.saveSuccessHelper(sucess), error => this.saveFailedHelper(error));
    }
  }

  private saveSuccessHelper(result?: string) {
    this.isSaving = false;
    if (this.isNew) {
      this.alertService.showSucessMessage("Saved successfully");
    } else {
      this.alertService.showSucessMessage("Updated successfully");
    }
    this.editorModal.hide();
    this.serverCallback();
  }


  private saveFailedHelper(error: any) {
    this.isSaving = false;
    let test = Utilities.getHttpResponseMessage(error);
    this.alertService.showInfoMessage(test[0]);
  }
}



