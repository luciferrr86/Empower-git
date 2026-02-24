import { Component, OnInit, ViewChild } from '@angular/core';
import { JobType } from '../../../../models/configuration/recruitment/job-type.model';
import { JobTypeService } from '../../../../services/configuration/recruitment/job-type.service';
import { AlertService } from '../../../../services/common/alert.service';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { Utilities } from '../../../../services/common/utilities';

@Component({
  selector: 'job-type-info',
  templateUrl: './job-type-info.component.html',
  styleUrls: ['./job-type-info.component.css']
})
export class JobTypeInfoComponent implements OnInit {
  public isSaving = false;
  private isNew = false;
  public modalTitle = "";
  public jobTypeEdit: JobType = new JobType();
  public serverCallback: () => void;

  @ViewChild('editorModal')
  editorModal: ModalDirective;

  constructor(private alertService: AlertService, private jobTypeService: JobTypeService) { }

  ngOnInit() {
  }

  public newJobType() {
    this.jobTypeEdit = new JobType();
    this.editorModal.show();
    this.isNew = true;
    this.modalTitle = "Add";
    return this.jobTypeEdit;
  }
  public save() {
     
    this.isSaving = true;
    if (this.isNew) {
      this.jobTypeService.create(this.jobTypeEdit).subscribe(sucess => this.saveSuccessHelper(sucess), error => this.saveFailedHelper(error));
    }
    else {
      this.jobTypeService.update(this.jobTypeEdit, this.jobTypeEdit.id).subscribe(sucess => this.saveSuccessHelper(sucess), error => this.saveFailedHelper(error));
    }

  }

  editJobType(jobType: JobType) {
     
    this.modalTitle = "Edit";
    this.isNew = false;
    this.editorModal.show();
    if (jobType) {
      this.jobTypeEdit = new JobType();
      (<any>Object).assign(this.jobTypeEdit, jobType);
      return this.jobTypeEdit;
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
