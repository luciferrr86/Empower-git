import { Component, OnInit, ViewChild } from '@angular/core';
import { FunctionalDesignation } from '../../../models/maintenance/functional-designation.model';
import { AlertService } from '../../../services/common/alert.service';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { FunctionalDesignationService } from '../../../services/maintenance/functional-designation.service';
import { Utilities } from '../../../services/common/utilities';
@Component({
  selector: 'functional-designation-info',
  templateUrl: './functional-designation-info.component.html',
  styleUrls: ['./functional-designation-info.component.css']
})
export class FunctionalDesignationInfoComponent implements OnInit {

  public isSaving = false;
  private isNew = false;
  public modalTitle = "";
  public designationEdit: FunctionalDesignation = new FunctionalDesignation();
  public serverCallback: () => void;

  @ViewChild('editorModal')
  editorModal: ModalDirective;

  constructor(private alertService: AlertService, private functionalDesignationService: FunctionalDesignationService) { }

  ngOnInit() {
  }

  public newDesignation() {
    this.designationEdit = new FunctionalDesignation();
    this.editorModal.show();
    this.isNew = true;
    this.modalTitle = "Add";
    return this.designationEdit;
  }
  public save() {
    this.isSaving = true;
    if (this.isNew) {
      this.functionalDesignationService.create(this.designationEdit).subscribe(sucess => this.saveSuccessHelper(), error => this.saveFailedHelper(error));
    }
    else {
      this.functionalDesignationService.update(this.designationEdit, this.designationEdit.id).subscribe(sucess => this.saveSuccessHelper(), error => this.saveFailedHelper(error));
    }

  }

  editDesignation(designation: FunctionalDesignation) {
    this.modalTitle = "Edit";
    this.isNew = false;
    this.editorModal.show();
    if (designation) {
      this.designationEdit = new FunctionalDesignation();
      (<any>Object).assign(this.designationEdit, designation);
      return this.designationEdit;
    }
  }


  private saveSuccessHelper() {
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
    this.alertService.showInfoMessage("Please try later" + test[0]);
  }
}
