import { Component, OnInit, ViewChild } from '@angular/core';
import { InterviewType } from '../../../../models/configuration/recruitment/interview-type.model';
import { InterviewTypeService } from '../../../../services/configuration/recruitment/interview-type.service';
import { AlertService } from '../../../../services/common/alert.service';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { Utilities } from '../../../../services/common/utilities';

@Component({
  selector: 'interview-type-info',
  templateUrl: './interview-type-info.component.html',
  styleUrls: ['./interview-type-info.component.css']
})
export class InterviewTypeInfoComponent implements OnInit {
  public isSaving = false;
  private isNew = false;
  public modalTitle = "";
  public interviewTypeEdit: InterviewType = new InterviewType();
  public serverCallback: () => void;

  @ViewChild('editorModal')
  editorModal: ModalDirective;

  constructor(private alertService: AlertService, private interviewTypeService: InterviewTypeService) { }

  ngOnInit() {
  }

  public newInterviewType() {
    this.interviewTypeEdit = new InterviewType();
    this.editorModal.show();
    this.isNew = true;
    this.modalTitle = "Add";
    return this.interviewTypeEdit;
  }
  public save() {
    this.isSaving = true;
    if (this.isNew) {
      this.interviewTypeService.create(this.interviewTypeEdit).subscribe(sucess => this.saveSuccessHelper(sucess), error => this.saveFailedHelper(error));
    }
    else {
      this.interviewTypeService.update(this.interviewTypeEdit, this.interviewTypeEdit.id).subscribe(sucess => this.saveSuccessHelper(sucess), error => this.saveFailedHelper(error));
    }

  }

  editInterviewType(interviewType: InterviewType) {
    this.modalTitle = "Edit";
    this.isNew = false;
    this.editorModal.show();
    if (interviewType) {
      this.interviewTypeEdit = new InterviewType();
      (<any>Object).assign(this.interviewTypeEdit, interviewType);
      return this.interviewTypeEdit;
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
