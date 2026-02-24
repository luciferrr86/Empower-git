import { Component, OnInit, ViewChild, TemplateRef } from '@angular/core';
import { LeaveHrView, EmployeeLeaveDetails, LeaveHrViewModel } from '../../../../models/leave/leave-hr-view.model';
import { HrEmployeeLeaveInfoComponent } from '../hr-employee-leave-info/hr-employee-leave-info.component';
import { AlertService } from '../../../../services/common/alert.service';
import { HrLeaveService } from '../../../../services/leave/hr-leave.service';

@Component({
  selector: 'hr-employee-list',
  templateUrl: './hr-employee-list.component.html',
  styleUrls: ['./hr-employee-list.component.css']
})
export class HrEmployeeListComponent implements OnInit {

  columns: any[] = [];
  filterQuery: string = "";
  pageNumber = 0;
  count = 0;
  pageSize = 10;
  rows: LeaveHrView[] = [];
  employees: LeaveHrView;
  leaveDeatils: EmployeeLeaveDetails;
  loadingIndicator: boolean = true;
  isConfigSet: boolean = false;

  @ViewChild('indexTemplate')
  indexTemplate: TemplateRef<any>;
  @ViewChild('actionsTemplate')
  actionsTemplate: TemplateRef<any>;
  @ViewChild('employeeLeaveDetails')
  employeeLeaveDetails: HrEmployeeLeaveInfoComponent;

  constructor(private hrViewService: HrLeaveService, private alertService: AlertService) { }

  ngOnInit() {
    this.columns = [
      { prop: 'employeeName', name: 'Employee Name' },
      { prop: 'department', name: 'Department' },
      { prop: 'remainingLeave', name: 'Remaining Leaves' },
      { prop: 'Action', name: 'Action', cellTemplate: this.actionsTemplate }
    ];
    this.getAllemployee(this.pageNumber, this.pageSize, this.filterQuery);
  }
  epmLeaveDetails(employee: LeaveHrView) {
    this.employeeLeaveDetails.leaveDetails(employee.employeeId);
  }

  getFilteredData(filterString) {
    this.getAllemployee(this.pageNumber, this.pageSize, filterString);
  }

  getData(e) {
    this.getAllemployee(this.pageNumber, e, this.filterQuery);
  }

  setPage(e) {
    this.getAllemployee(e.offset, this.pageSize, this.filterQuery);
  }


  getAllemployee(page?: number, pageSize?: number, name?: string) {
    this.hrViewService.getAllEmployees(page, pageSize, name).subscribe(result => this.onSuccessfulDataLoad(result), error => this.onDataLoadFailed(error));
  }
  onSuccessfulDataLoad(leaveList: LeaveHrViewModel) {

    this.isConfigSet = leaveList.isConfigSet;
    this.rows = leaveList.leaveEmployeeList;
    leaveList.leaveEmployeeList.forEach((leave, index, leaves) => {
      (<any>leave).index = index + 1;
    });
    this.count = leaveList.totalCount;
    this.loadingIndicator = false;
  }

  onDataLoadFailed(error: any) {
    this.loadingIndicator = false;
    this.alertService.showInfoMessage("Unable to retrieve list from the server");

  }
}
