import { Component, OnInit, ViewChild } from '@angular/core';
import { TimesheetTemplateModel } from '../../../../models/configuration/timesheet/timesheet-template.model';
import { TimesheetTemplateService } from '../../../../services/configuration/timesheet/timesheet-template.service';
import { AlertService } from '../../../../services/common/alert.service';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { IOption } from 'ng-select';
import { DropDownList } from '../../../../models/common/dropdown';
import { Utilities } from '../../../../services/common/utilities';

@Component({
  selector: 'timesheet-template-info',
  templateUrl: './timesheet-template-info.component.html',
  styleUrls: ['./timesheet-template-info.component.css']
})
export class TimesheetTemplateInfoComponent implements OnInit {

  public isSaving = false;
  private isNew = false;
  public modalTitle = "";
  public template: TimesheetTemplateModel = new TimesheetTemplateModel();
  public serverCallback: () => void;
  configurationList: Array<IOption> = [];
  public weeklystring = ""

  weekDaysList: Array<IOption> = [];
  list: Array<DropDownList> = [];

  @ViewChild('editorModal')
  editorModal: ModalDirective;
  constructor(private templateService: TimesheetTemplateService, private alertService: AlertService) { }
  ngOnInit() {
  }
  createTemplate(allConfigurationList: DropDownList[]) {
    this.template = new TimesheetTemplateModel();
    this.modalTitle = "Add";
    this.configurationList = allConfigurationList;
    this.list = [];
    this.list.push({ label: "Monday", value: 'monday' });
    this.list.push({ label: "Tuesday", value: 'tuesday' });
    this.list.push({ label: "Wednesday", value: 'wednesday' });
    this.list.push({ label: "Thursday", value: 'thursday' });
    this.list.push({ label: "Friday", value: 'friday' });
    this.list.push({ label: "Saturday", value: 'saturday' });
    this.list.push({ label: "Sunday", value: 'sunday' });
    this.weekDaysList = this.list;
    this.editorModal.show();
    this.isNew = true;
    return this.template;
  }
  updateTemplate(allConfigurationList: DropDownList[], templateEdit: TimesheetTemplateModel) {
    this.modalTitle = "Edit";
    this.editorModal.show();
    this.list = [];
    this.list.push({ label: "Monday", value: 'monday' });
    this.list.push({ label: "Tuesday", value: 'tuesday' });
    this.list.push({ label: "Wednesday", value: 'wednesday' });
    this.list.push({ label: "Thursday", value: 'thursday' });
    this.list.push({ label: "Friday", value: 'friday' });
    this.list.push({ label: "Saturday", value: 'saturday' });
    this.list.push({ label: "Sunday", value: 'sunday' });
    this.weekDaysList = this.list;
    this.template.selectedDays = templateEdit.selectedDays;
    this.isNew = false;
    this.configurationList = allConfigurationList;
    if (templateEdit) {
      this.template = new TimesheetTemplateModel();
      (<any>Object).assign(this.template, templateEdit)
      return this.template;
    }

  }
  public save() {
    this.isSaving = true;

    if (this.isNew) {
      this.templateService.create(this.template).subscribe(sucess => this.saveSuccessHelper(sucess), error => this.saveFailedHelper(error));
    }
    else {
      this.templateService.update(this.template, this.template.id).subscribe(sucess => this.saveSuccessHelper(sucess), error => this.saveFailedHelper(error));
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
}
