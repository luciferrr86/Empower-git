import { Component, OnInit, ViewChild } from '@angular/core';
import { IOption } from 'ng-select/dist/option.interface';
import { DropDownList } from '../../../../models/common/dropdown';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { TimesheetProjectService } from '../../../../services/configuration/timesheet/timesheet-project.service';
import { AlertService } from '../../../../services/common/alert.service';
import { Utilities } from '../../../../services/common/utilities';
import { ProjectModel } from '../../../../models/configuration/timesheet/timesheet-project.model';

@Component({
  selector: 'timesheet-project-info',
  templateUrl: './timesheet-project-info.component.html',
  styleUrls: ['./timesheet-project-info.component.css']
})
export class TimesheetProjectInfoComponent implements OnInit {

  clientList: Array<IOption> = [];
  managerList: Array<IOption> = [];
  public isSaving = false;
  private isNew = false;
  public modalTitle = "";
  public project: ProjectModel = new ProjectModel();
  public serverCallback: () => void;

  @ViewChild('editorModal')
  editorModal: ModalDirective;

  constructor(private projectService: TimesheetProjectService, private alertService: AlertService) { }

  ngOnInit() {
  }

  createProject(allClientList: DropDownList[], allManagerList: DropDownList[]) {
    this.modalTitle = "Add";
    this.editorModal.show();
    this.isNew = true;
    this.clientList = allClientList;
    this.managerList = allManagerList;
    this.project = new ProjectModel();
    return this.project;
  }
  updateProject(allClientList: DropDownList[], allManagerList: DropDownList[], projectEdit: ProjectModel) {
    this.modalTitle = "edit";
    this.editorModal.show();
    this.isNew = false;
    this.clientList = allClientList;
    this.managerList = allManagerList;
    if (projectEdit) {
      this.project = new ProjectModel();
      (<any>Object).assign(this.project, projectEdit);
      return this.project;
    }
  }
  public save() {
    this.isSaving = true;
    if (this.isNew) {
      this.projectService.create(this.project).subscribe(sucess => this.saveSuccessHelper(sucess), error => this.saveFailedHelper(error));
    }
    else {
      this.projectService.update(this.project, this.project.id).subscribe(sucess => this.saveSuccessHelper(sucess), error => this.saveFailedHelper(error));
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
