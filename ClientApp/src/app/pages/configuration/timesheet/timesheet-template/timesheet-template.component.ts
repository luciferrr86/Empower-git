import { Component, OnInit, ViewChild, TemplateRef, ViewContainerRef } from '@angular/core';
import { TimesheetTemplateModel, TimesheetTemplateViewModel } from '../../../../models/configuration/timesheet/timesheet-template.model';
import { DropDownList } from '../../../../models/common/dropdown';
import { TimesheetTemplateService } from '../../../../services/configuration/timesheet/timesheet-template.service';
import { AlertService } from '../../../../services/common/alert.service';
import { TimesheetTemplateInfoComponent } from '../timesheet-template-info/timesheet-template-info.component';
import { Utilities } from '../../../../services/common/utilities';

@Component({
  selector: 'timesheet-template',
  templateUrl: './timesheet-template.component.html',
  styleUrls: ['./timesheet-template.component.css']
})
export class TimesheetTemplateComponent implements OnInit {

  columns: any[] = [];
  filterQuery: string = "";
  pageNumber = 0;
  count = 0;
  pageSize = 10;
  out: string = "";
  rows: TimesheetTemplateModel[] = [];
  template: TimesheetTemplateModel = new TimesheetTemplateModel();
  loadingIndicator: boolean = true;
  allConfigurationList: DropDownList[] = [];

  @ViewChild('templateInfo')
  templateInfo: TimesheetTemplateInfoComponent;
  @ViewChild('indexTemplate')
  indexTemplate: TemplateRef<any>;
  @ViewChild('actionsTemplate')
  actionsTemplate: TemplateRef<any>;
  @ViewChild('daysTemplate')
  daysTemplate: TemplateRef<any>;


  constructor(private templateService: TimesheetTemplateService, private alertService: AlertService) { }

  ngOnInit() {
    this.columns = [
      { prop: "index", name: '#', cellTemplate: this.indexTemplate, canAutoResize: false, draggable: false },
      { prop: 'templateName', name: 'Template Name', draggable: false },
      //{ prop: 'startDate', name: 'Start Date' },
      { prop: 'daysTemplate', name: 'Days', cellTemplate: this.daysTemplate, draggable: false },
      { prop: 'Action', name: 'Action', cellTemplate: this.actionsTemplate, resizeable: false, canAutoResize: false, sortable: false, draggable: false }
    ];
    this.getAllTemplate(this.pageNumber, this.pageSize, this.filterQuery)
  }
  ngAfterViewInit() {
    this.templateInfo.serverCallback = () => {
      this.getAllTemplate(this.pageNumber, this.pageSize, this.filterQuery);
    };

  }
  getAllTemplate(page?: number, pageSize?: number, name?: string) {
    this.templateService.getAllTemplate(page, pageSize, name).subscribe(result => this.onSuccessfulDataLoad(result), error => this.onDataLoadFailed(error));
  }

  getFilteredData(filterString) {
    this.getAllTemplate(this.pageNumber, this.pageSize, this.filterQuery);
  }

  getData(e) {
    this.getAllTemplate(this.pageNumber, e, this.filterQuery);
  }

  setPage(e) {
    this.getAllTemplate(e.offset, this.pageSize, this.filterQuery);
  }

  onSuccessfulDataLoad(template: TimesheetTemplateViewModel) {
    this.rows = template.timesheetTemplateList;
    template.timesheetTemplateList.forEach((templates, index, template) => {
      (<any>templates).index = index + 1;
    });
    this.allConfigurationList = template.timesheetConfigurationList;
    this.count = template.totalCount;
    this.loadingIndicator = false;
  }

  onDataLoadFailed(error: any) {
    this.loadingIndicator = false;
    this.alertService.showInfoMessage("Unable to retrieve list from the server");
  }

  newTemplate() {
    this.template = this.templateInfo.createTemplate(this.allConfigurationList);
  }
  editTemplate(template: TimesheetTemplateModel) {
    this.template = this.templateInfo.updateTemplate(this.allConfigurationList, template);

  }

  deleteTemplate(template: TimesheetTemplateModel) {
    this.alertService.showConfirm("Are you sure you want to delete?", () => this.deleteTemplateHelper(template));
  }
  deleteTemplateHelper(template: TimesheetTemplateModel) {
    this.templateService.delete(template.id).subscribe(results => {
      this.getAllTemplate(this.pageNumber, this.pageSize, this.filterQuery);
    },
      error => {
        let test = Utilities.getHttpResponseMessage(error);
        this.alertService.showInfoMessage("Please try later" + test[0]);
      });

  }

}
