import { Component, OnInit, ViewChild, TemplateRef } from '@angular/core';
import { ProjectModel, ProjectViewModel } from '../../../../models/configuration/timesheet/timesheet-project.model';
import { TimesheetProjectInfoComponent } from '../timesheet-project-info/timesheet-project-info.component';
import { TimesheetProjectService } from '../../../../services/configuration/timesheet/timesheet-project.service';
import { AlertService } from '../../../../services/common/alert.service';
import { DropDownList } from '../../../../models/common/dropdown';
import { Router } from '@angular/router';
import { TimesheetAssignProjectComponent } from '../timesheet-assign-project/timesheet-assign-project.component';

@Component({
  selector: 'timesheet-project',
  templateUrl: './timesheet-project.component.html',
  styleUrls: ['./timesheet-project.component.css']
})
export class TimesheetProjectComponent implements OnInit {

  columns: any[] = [];
  filterQuery: string = "";
  pageNumber = 0;
  count = 0;
  pageSize = 10;

  rows: ProjectModel[] = [];
  project: ProjectModel;
  loadingIndicator: boolean = true;
  allClientList: DropDownList[] = [];
  allManagerList: DropDownList[] = [];
  projectList: DropDownList[] = [];
  @ViewChild('projectInfo')
  projectInfo: TimesheetProjectInfoComponent;
  @ViewChild('indexTemplate')
  indexTemplate: TemplateRef<any>;
  @ViewChild('actionsTemplate')
  actionsTemplate: TemplateRef<any>;
  @ViewChild('timesheetassignProject')
  timesheetassignProject: TimesheetAssignProjectComponent;

  @ViewChild('startDateTemplate')
  startDateTemplate: TemplateRef<any>;

  @ViewChild('endDateTemplate')
  endDateTemplate: TemplateRef<any>;

  constructor(private projectService: TimesheetProjectService, private router: Router, private alertService: AlertService) { }

  ngOnInit() {
    this.columns = [
      { prop: "index", name: '#', cellTemplate: this.indexTemplate, canAutoResize: false },
      { prop: 'projectName', name: 'Project Name' },
      { prop: 'startDate', name: 'Start Date', cellTemplate: this.startDateTemplate },
      { prop: 'endDate', name: 'End Date', cellTemplate: this.endDateTemplate },
      { prop: 'Action', name: 'Action', cellTemplate: this.actionsTemplate, resizeable: false, canAutoResize: false, sortable: false, draggable: false }
    ];
    this.getAllProject(this.pageNumber, this.pageSize, this.filterQuery)
  }
  ngAfterViewInit() {
    this.projectInfo.serverCallback = () => {
      this.getAllProject(this.pageNumber, this.pageSize, this.filterQuery);
    };
  }
  getAllProject(page?: number, pageSize?: number, name?: string) {
    this.projectService.getAllProject(page, pageSize, name).subscribe(result => this.onSuccessfulDataLoad(result), error => this.onDataLoadFailed(error));
  }

  getFilteredData(filterString) {
    this.getAllProject(this.pageNumber, this.pageSize, this.filterQuery);
  }

  getData(e) {
    this.getAllProject(this.pageNumber, e, this.filterQuery);
  }

  setPage(e) {
    this.getAllProject(e.offset, this.pageSize, this.filterQuery);
  }

  onSuccessfulDataLoad(project: ProjectViewModel) {

    this.rows = project.projectList;
    project.projectList.forEach((projects, index, project) => {
      (<any>projects).index = index + 1;
    });
    this.allClientList = project.clientList;
    this.allManagerList = project.managerList;
    this.count = project.totalCount;
    this.loadingIndicator = false;
  }

  onDataLoadFailed(error: any) {
    this.alertService.showInfoMessage("Unable to retrieve list from the server");
  }

  newProject() {
    this.project = this.projectInfo.createProject(this.allClientList, this.allManagerList)
  }
  editProject(project: ProjectModel) {
    this.project = this.projectInfo.updateProject(this.allClientList, this.allManagerList, project);
  }
  deleteProject(project: ProjectModel) {
    this.alertService.showConfirm("Are you sure you want to delete?", () => this.deleteProjecteHelper(project));
  }
  deleteProjecteHelper(project: ProjectModel) {
    this.projectService.delete(project.id).subscribe(results => {
      this.getAllProject(this.pageNumber, this.pageSize, this.filterQuery);
    },
      error => {
        this.alertService.showInfoMessage("An error occured while  deleting")
      });
  }
   
    assignProject() {
        this.router.navigate(['../configuration/timesheet-config/timesheet-assign-project']);
    }

}
