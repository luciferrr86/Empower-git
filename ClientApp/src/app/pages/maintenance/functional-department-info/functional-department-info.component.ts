import { Component, OnInit, ViewChild } from '@angular/core';
import { FunctionalDepartment } from '../../../models/maintenance/functional-department.model';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { DepartmentService } from '../../../services/maintenance/department.service';
import { AlertService } from '../../../services/common/alert.service';
import { Utilities } from '../../../services/common/utilities';

@Component({
  selector: 'functional-department-info',
  templateUrl: './functional-department-info.component.html',
  styleUrls: ['./functional-department-info.component.css']
})
export class FunctionalDepartmentInfoComponent implements OnInit {

  public isSaving = false;
  private isNew = false;
  public modalTitle = "";
  public departmentEdit: FunctionalDepartment = new FunctionalDepartment();
  public serverCallback: () => void;

  @ViewChild('editorModal')
  editorModal: ModalDirective;

  constructor(private functionalDepartmentService: DepartmentService, private alertService: AlertService) { }

  ngOnInit() {
  }

  newDepartment() {
    this.departmentEdit = new FunctionalDepartment();
    this.editorModal.show();
    this.isNew = true;
    this.modalTitle = "Add";
    return this.departmentEdit;
  }
  editDepartment(department: FunctionalDepartment) {
    this.modalTitle = "Edit";
    this.isNew = false;
    this.editorModal.show();
    if (department) {
      this.departmentEdit = new FunctionalDepartment();
      (<any>Object).assign(this.departmentEdit, department);
      return this.departmentEdit;
    }
  }

  public save() {
    this.isSaving = true;
    if (this.isNew) {
      this.functionalDepartmentService.create(this.departmentEdit).subscribe(sucess => this.saveSuccessHelper(), error => this.saveFailedHelper(error));
    }
    else {

      this.functionalDepartmentService.update(this.departmentEdit, this.departmentEdit.id).subscribe(sucess => this.saveSuccessHelper(), error => this.saveFailedHelper(error));
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
    this.alertService.showInfoMessage(test[0]);
  }
}
