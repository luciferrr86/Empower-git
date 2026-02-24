import { Component, OnInit, ViewChild, TemplateRef } from '@angular/core';
import { Router } from '@angular/router';
import { ManageTimesheetService } from '../../../../services/timesheet/manage-timesheet.service';
import { Employee } from '../../../../models/maintenance/employee.model';
import { EmployeeListModel, EmployeeListViewModel } from '../../../../models/configuration/timesheet/timesheet-assign-project.model';
import { AlertService } from '../../../../services/common/alert.service';
import { AccountService } from '../../../../services/account/account.service';
import { ReviewEmployeeTimesheetComponent } from '../review-employee-timesheet/review-employee-timesheet.component';
import { MyTimesheetViewModel } from '../../../../models/timesheet/myTimesheet.model';

@Component({
  selector: 'timesheet-employee-list',
  templateUrl: './timesheet-employee-list.component.html',
  styleUrls: ['./timesheet-employee-list.component.css']
})
export class TimesheetEmployeeListComponent implements OnInit {
  columns: any[] = [];
  pageNumber = 0;
  count = 0;
  pageSize = 10;
  filterQuery: string = "";
  loadingIndicator: boolean = true;
  rows: EmployeeListModel[] = [];
  @ViewChild('indexTemplate')
  indexTemplate: TemplateRef<any>;
  @ViewChild('actionsTemplate')
  actionsTemplate: TemplateRef<any>;
  @ViewChild('reviewEmployee')
  reviewEmployee: ReviewEmployeeTimesheetComponent;

  constructor(private router: Router, private manageService: ManageTimesheetService, private alertService: AlertService, private accountService: AccountService) { }
  ngOnInit() {
    this.columns = [
      { prop: "index", name: '#', cellTemplate: this.indexTemplate, canAutoResize: false },
      { prop: 'fullName', name: 'Employee Name' },
      { prop: 'designation', name: 'Designation' },
      { prop: 'Action', name: 'Action', cellTemplate: this.actionsTemplate, resizeable: false, canAutoResize: false, sortable: false, draggable: false }
    ];
    this.getAll(this.pageNumber, this.pageSize, this.filterQuery)
  }
  viewTimesheet(id) {
   
   this.router.navigate(['../timesheet/review-employee-timesheet'], { queryParams: { id: id }, skipLocationChange: true});
  }

  getAll(page?: number, pageSize?: number, name?: string) {
    this.manageService.getAll(this.accountService.currentUser.id, page, pageSize, name).subscribe(result => this.onSuccessfulDataLoad(result), error => this.onDataLoadFailed(error));
  }
  getFilteredData(filterString) {
    this.getAll(this.pageNumber, this.pageSize, this.filterQuery);
  }

  getData(e) {
    this.getAll(this.pageNumber, e, this.filterQuery);
  }

  setPage(e) {
    this.getAll(e.offset, this.pageSize, this.filterQuery);
  }


  onSuccessfulDataLoad(employee: EmployeeListViewModel) {

    this.rows = employee.employeeList;
    employee.employeeList.forEach((emp, index, employee) => {
      (<any>emp).index = index + 1;
    });

    this.count = employee.totalCount;
    this.loadingIndicator = false;
  }

  onDataLoadFailed(error: any) {
    this.alertService.showInfoMessage("Unable to retrieve list from the server");
  }

  timesheetView(leaveList: EmployeeListModel) {
    // this.leaveDetail.leaveDetails(leaveList.leaveDeatilId);
  }

}
