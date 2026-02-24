import { Component, OnInit, ViewChild } from '@angular/core';
import { LeavePeriod } from '../../../../models/configuration/leave/leave-period.model';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { LeavePeriodService } from '../../../../services/configuration/leave/leave-period.service';
import { AlertService } from '../../../../services/common/alert.service';
import { Utilities } from '../../../../services/common/utilities';
import { Router } from '@angular/router';

@Component({
  selector: 'leave-period-info',
  templateUrl: './leave-period-info.component.html',
  styleUrls: ['./leave-period-info.component.css']
})


export class LeavePeriodInfoComponent implements OnInit {

  public isSaving = false;
  private isNew = false;
  public isEdit = true;
  public modalTitle = "";
  public leavePeriodEdit: LeavePeriod = new LeavePeriod();
  public serverCallback: () => void;
  minDate: Date = new Date();

  @ViewChild('editorModal')
  editorModal: ModalDirective;

  constructor(private leavePeriodService: LeavePeriodService, private alertService: AlertService, private router: Router) { }

  ngOnInit() {
  }
  createLeavePeriod() {
    this.leavePeriodEdit = new LeavePeriod();
    this.editorModal.show();
    this.isNew = true;
    this.isEdit = true;
    this.modalTitle = "Add";
    return this.leavePeriodEdit;
  }

  editLeavePeriod(leavePeriod: LeavePeriod) {
    this.isNew = false;
    this.editorModal.show();
    this.modalTitle = "Edit";
    if (leavePeriod) {
      this.leavePeriodEdit = new LeavePeriod();
      this.isEdit = leavePeriod.isEdit;
      (<any>Object).assign(this.leavePeriodEdit, leavePeriod);
      return this.leavePeriodEdit;
    }

  }


  public save() {
    this.isSaving = true;
    if (this.isNew) {
      this.leavePeriodService.create(this.leavePeriodEdit).subscribe(sucess => this.saveSuccessHelper(), error => this.saveFailedHelper(error));
    }
    else {
      this.leavePeriodService.update(this.leavePeriodEdit, this.leavePeriodEdit.id).subscribe(sucess => this.saveSuccessHelper(), error => this.saveFailedHelper(error));
    }
  }

  private saveSuccessHelper() {
    this.isSaving = false;
    if (this.isNew) {
      this.alertService.showSucessMessage("Saved successfully");
    } else {
      this.alertService.showSucessMessage("Updated successfully. Please update holiday list.");
      this.router.navigate(['../configuration/leave-config/leave-holiday']);
    }
    this.editorModal.hide();
    this.serverCallback();
  }


  private saveFailedHelper(error: any) {
    this.isSaving = false;
    let test = Utilities.getHttpResponseMessage(error);
    this.alertService.showInfoMessage("Please try later" + test[0]);
  }
}
