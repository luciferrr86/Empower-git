import { Component, OnInit, ViewChild } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { LeaveTypeService } from '../../../../services/configuration/leave/leave-type.service';
import { AlertService } from '../../../../services/common/alert.service';
import { LeaveType } from '../../../../models/configuration/leave/leave-type.model';
import { Utilities } from '../../../../services/common/utilities';

@Component({
  selector: 'leave-type-info',
  templateUrl: './leave-type-info.component.html',
  styleUrls: ['./leave-type-info.component.css']
})
export class LeaveTypeInfoComponent implements OnInit {

  public isSaving = false;
  private isNew = false;
  public modalTitle = "";
  public leaveTypeEdit: LeaveType = new LeaveType();
  public serverCallback: () => void;

  @ViewChild('editorModal')
  editorModal: ModalDirective;


  constructor(private leaveTypeService: LeaveTypeService, private alertService: AlertService) { }

  ngOnInit() {
  }

  createLeaveType() {
    this.leaveTypeEdit = new LeaveType();
    this.editorModal.show();
    this.isNew = true;
    this.modalTitle = "Add";
    return this.leaveTypeEdit;
  }

  editLeaveType(leaveType: LeaveType) {
    this.isNew = false;
    this.editorModal.show();
    this.modalTitle = "Edit";
    if (leaveType) {
      this.leaveTypeEdit = new LeaveType;
      (<any>Object).assign(this.leaveTypeEdit, leaveType);
      return this.leaveTypeEdit;
    }
  }

  public save() {
    this.isSaving = true;
    if (this.isNew) {
      this.leaveTypeService.create(this.leaveTypeEdit).subscribe(sucess => this.saveSuccessHelper(sucess), error => this.saveFailedHelper(error));
    }
    else {
      this.leaveTypeService.update(this.leaveTypeEdit, this.leaveTypeEdit.id).subscribe(sucess => this.saveSuccessHelper(sucess), error => this.saveFailedHelper(error));
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
