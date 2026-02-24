import { Component, OnInit, ViewChild, TemplateRef } from '@angular/core';
import { TimesheetTemplateService } from '../../../../services/configuration/timesheet/timesheet-template.service';
import { AlertService } from '../../../../services/common/alert.service';
import { IOption } from 'ng-select';
import { DropDownList } from '../../../../models/common/dropdown';
import { TimesheetScheduleModel, TimesheetScheduleViewModel } from '../../../../models/configuration/timesheet/timesheet-schedule.model';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { EmployeeListModel, EmployeeList, EmployeeListViewModel } from '../../../../models/configuration/timesheet/timesheet-assign-project.model';
import { Utilities } from '../../../../services/common/utilities';

@Component({
  selector: 'timesheet-schedule-info',
  templateUrl: './timesheet-schedule-info.component.html',
  styleUrls: ['./timesheet-schedule-info.component.css']
})
export class TimesheetScheduleInfoComponent implements OnInit {

  columns: any[] = [];
  filterQuery: string = "";
  pageNumber = 0;
  count = 0;
  pageSize = 10;
  rows: EmployeeListModel[] = [];
  templateList: Array<IOption> = [];
  employeeList: EmployeeList[] = [];
  @ViewChild('indexSchedule')
  indexSchedule: TemplateRef<any>;
  @ViewChild('selectTemplate')
  selectTemplate: TemplateRef<any>;
  loadingIndicator: boolean = true;

  public isSaving = false;
  public modalTitle = "";
  public sheduleEdit: TimesheetScheduleModel = new TimesheetScheduleModel();
  public serverCallback: () => void;

  @ViewChild('editorModal')
  editorModal: ModalDirective;

  constructor(private templateService: TimesheetTemplateService, private alertService: AlertService) { }

  ngOnInit() {
    this.columns = [
      { prop: "index", name: '#', cellSchedule: this.indexSchedule, canAutoResize: false, draggable: false },
      { prop: 'fullName', name: 'Employee Name', draggable: false },
      { prop: 'selectTemplate', name: 'Select', cellTemplate: this.selectTemplate, resizeable: false, canAutoResize: false, sortable: false, draggable: false },
    ];
  }
  createSchedule(employeeListResult: EmployeeListModel[], timesheetTemplateList: DropDownList[], scheduleCount: number) {

    this.modalTitle = "Add";
    this.editorModal.show();
    this.rows = employeeListResult;
    employeeListResult.forEach((projects, index, schedule) => {
      (<any>projects).index = index + 1;
    });
    this.count = scheduleCount;
    this.templateList = timesheetTemplateList;
    this.sheduleEdit = new TimesheetScheduleModel();
    this.employeeList.splice(0, this.employeeList.length);
    this.loadingIndicator = false;
    return this.sheduleEdit;

  }
  onSelect(row) {
    if (this.sheduleEdit.templateId != undefined) {
      let itemExist = this.employeeList.find(m => m.employeeId == row.employeeId);
      if (itemExist != undefined) {
        for (let i = 0; i < this.employeeList.length; i++) {

          if (this.employeeList[i].employeeId === row.employeeId) {
            const index = this.employeeList.findIndex(s => s == row.employeeId);
            this.employeeList.splice(index, 1);
            break;

          }
        }
      }
      else {
        let employee = new EmployeeList();
        employee.employeeId = row.employeeId;
        this.employeeList.push(employee);
      }

    }

  }
  Save() {
    this.sheduleEdit.employeelist = this.employeeList;
    this.templateService.setSchedule(this.sheduleEdit).subscribe(sucess => this.saveSuccessHelper(sucess), error => this.saveFailedHelper(error));

  }

  private saveSuccessHelper(result?: string) {
    this.alertService.showSucessMessage("save successfully");
    this.editorModal.hide();
    this.serverCallback();
  }


  private saveFailedHelper(error: any) {
    this.isSaving = false;
    let test = Utilities.getHttpResponseMessage(error);
    this.alertService.showInfoMessage("Please try later" + test[0]);
  }

  employeelist(id: string) {
    this.templateService.getEmployeeByProjectId(id, this.pageNumber, this.pageSize, this.filterQuery).subscribe(result => this.onSuccessful(result), error => this.onDataLoadFailed(error));
  }

  onSuccessful(employeelistData: EmployeeListViewModel) {
    this.rows = employeelistData.employeeList;
    employeelistData.employeeList.forEach((projects, index, employeelist) => {
      (<any>projects).index = index + 1;
    });

    for (let i = 0; i < this.rows.length; i++) {
      if (this.rows[i].templateId != '0') {
        let employee = new EmployeeList();
        employee.employeeId = this.rows[i].employeeId;
        this.employeeList.push(employee);
      }
    }
    this.count = employeelistData.totalCount;
    this.loadingIndicator = false;

  }
  onDataLoadFailed(error: any) {
    this.alertService.showInfoMessage("Unable to retrieve list from the server");
  }


}
