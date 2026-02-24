import { Component, OnInit, ViewChild, TemplateRef } from '@angular/core';
import { TimesheetScheduleInfoComponent } from '../timesheet-schedule-info/timesheet-schedule-info.component';
import { AlertService } from '../../../../services/common/alert.service';
import { TimesheetScheduleModel, TimesheetScheduleViewModel, UserScheduleModel } from '../../../../models/configuration/timesheet/timesheet-schedule.model';
import { TimesheetTemplateService } from '../../../../services/configuration/timesheet/timesheet-template.service';
import { DropDownList } from '../../../../models/common/dropdown';
import { EmployeeListModel } from '../../../../models/configuration/timesheet/timesheet-assign-project.model';

@Component({
  selector: 'app-timesheet-schedule',
  templateUrl: './timesheet-schedule.component.html',
  styleUrls: ['./timesheet-schedule.component.css']
})
export class TimesheetScheduleComponent implements OnInit {

  columns: any[] = [];
  filterQuery: string = "";
  pageNumber = 0;
  count = 0;
  pageSize = 10;

  rows: UserScheduleModel[] = [];
  template: TimesheetScheduleModel;
  loadingIndicator: boolean = true;
  allTemplateList: DropDownList[] = [];
  allEmployeeList: EmployeeListModel[] = [];
  schedule: TimesheetScheduleViewModel;
  @ViewChild('scheduleInfo')
  scheduleInfo: TimesheetScheduleInfoComponent;
  @ViewChild('indexSchedule')
  indexSchedule: TemplateRef<any>;
  @ViewChild('actionsSchedule')
  actionsSchedule: TemplateRef<any>;
  @ViewChild('daysSchedule')
  daysSchedule: TemplateRef<any>;


  constructor(private templateService: TimesheetTemplateService, private alertService: AlertService) { }

  ngOnInit() {
    this.columns = [
      { prop: "index", name: '#', cellSchedule: this.indexSchedule, canAutoResize: false, draggable: false },
      { prop: 'fullName', name: 'Employee Name', draggable: false },
      { prop: 'timesheetFrequency', name: 'Timesheet Frequency', draggable: false },
      { prop: 'daysTemplate', name: 'Days', cellTemplate: this.daysSchedule, draggable: false },
    ];
    this.getAllSchedule(this.pageNumber, this.pageSize, this.filterQuery);
  }
  ngAfterViewInit() {
    this.scheduleInfo.serverCallback = () => {
      this.getAllSchedule(this.pageNumber, this.pageSize, this.filterQuery);
    };
  }
  getAllSchedule(page?: number, pageSize?: number, name?: string) {
    this.templateService.getAllSchedule(page, pageSize, name).subscribe(result => this.onSuccessfulDataLoad(result), error => this.onDataLoadFailed(error));
  }

  getFilteredData(filterString) {
    this.getAllSchedule(this.pageNumber, this.pageSize, this.filterQuery);
  }

  getData(e) {
    this.getAllSchedule(this.pageNumber, e, this.filterQuery);
  }

  setPage(e) {
    this.getAllSchedule(e.offset, this.pageSize, this.filterQuery);
  }

  onSuccessfulDataLoad(schedule: TimesheetScheduleViewModel) {

    this.rows = schedule.userScheduleList;
    schedule.userScheduleList.forEach((projects, index, schedule) => {
      (<any>projects).index = index + 1;
    });
    this.schedule = schedule;
    this.count = schedule.scheduleCount;
    this.loadingIndicator = false;
  }

  onDataLoadFailed(error: any) {
    this.loadingIndicator = false;
    this.alertService.showInfoMessage("Unable to retrieve list from the server");
  }

  newSchedule() {
    this.template = this.scheduleInfo.createSchedule(this.schedule.employeeList, this.schedule.timesheetTemplateList, this.schedule.scheduleCount);
  }
}
