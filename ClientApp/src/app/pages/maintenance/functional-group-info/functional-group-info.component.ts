import { Component, OnInit, ViewChild, Input } from '@angular/core';
import { IOption } from 'ng-select';
import { DropDownList } from '../../../models/common/dropdown';
import { FunctionalGroup } from '../../../models/maintenance/functional-group.model';
import { MessageSeverity, AlertService } from '../../../services/common/alert.service';
import { Utilities } from '../../../services/common/utilities';
import { GroupService } from '../../../services/maintenance/group.service';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { DepartmentService } from '../../../services/maintenance/department.service';
import { FunctionalDepartment } from '../../../models/maintenance/functional-department.model';

@Component({
  selector: 'functional-group-info',
  templateUrl: './functional-group-info.component.html',
  styleUrls: ['./functional-group-info.component.css']
})
export class FunctionalGroupInfoComponent implements OnInit {

  departmentlist: Array<IOption> = [];
  public formResetToggle = true;
  public isSaving = false;
  private isNew = false;
  public modalTitle = "";
  public groupEdit: FunctionalGroup = new FunctionalGroup();
  public serverCallback: () => void;

  @ViewChild('editorModal')
  editorModal: ModalDirective;

  constructor(private departmentService: DepartmentService, private groupService: GroupService, private alertService: AlertService) {

  }

  ngOnInit() {
  }
  addGroup(allDepartment: DropDownList[]) {
    this.modalTitle = "Add ";
    this.editorModal.show();
    this.isNew = true;
    this.groupEdit = new FunctionalGroup();
    this.departmentlist = allDepartment;
    return this.groupEdit;
  }
  updateGroup(group: FunctionalGroup, allDepartment: DropDownList[]) {
    this.modalTitle = "Edit ";
    this.editorModal.show();
    this.departmentlist = allDepartment;
    this.isNew = false;
    if (group) {
      this.groupEdit = new FunctionalGroup();
      (<any>Object).assign(this.groupEdit, group);
      return this.groupEdit;
    }
  }

  public save() {
    this.isSaving = true;
    if (this.isNew) {
      this.groupService.create(this.groupEdit).subscribe(sucess => this.saveSuccessHelper(sucess), error => this.saveFailedHelper(error));
    }
    else {

      this.groupService.update(this.groupEdit, this.groupEdit.id).subscribe(sucess => this.saveSuccessHelper(sucess), error => this.saveFailedHelper(error));
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
    this.alertService.showInfoMessage("Please try later" + test[0]);
  }

  onSuccessfulDataLoad(departments: FunctionalDepartment[]) {
    var resultArray: Array<any> = [] //empty array which we are going to push our selected items, always define types 

    departments.forEach(i => {
      resultArray.push(
        {
          "value": i.id,
          "label": i.name
        });
      this.departmentlist = resultArray;
    });
  }
  onDataLoadFailed(error: any) {
    this.alertService.stopLoadingMessage();
    this.alertService.showStickyMessage("Load Error", `Unable to retrieve department from the server.\r\nErrors: "${Utilities.getHttpResponseMessage(error)}"`, MessageSeverity.error, error);

  }
}
