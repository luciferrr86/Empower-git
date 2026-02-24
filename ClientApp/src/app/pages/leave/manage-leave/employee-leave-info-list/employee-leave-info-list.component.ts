import { Component, OnInit, ViewChild, TemplateRef } from '@angular/core';
import { EmployeeLeaveDetails, LeaveHrView, LeaveHrViewModel } from '../../../../models/leave/leave-hr-view.model';
import { EmployeeLeaveInfoComponent } from '../employee-leave-info/employee-leave-info.component';
import { ManageLeaveService } from '../../../../services/leave/manage-leave.service';
import { AlertService } from '../../../../services/common/alert.service';
import { AccountService } from '../../../../services/account/account.service';

@Component({
  selector: 'employee-leave-info-list',
  templateUrl: './employee-leave-info-list.component.html',
  styleUrls: ['./employee-leave-info-list.component.css']
})
export class EmployeeLeaveInfoListComponent implements OnInit {
  columns: any[] = [];
  filterQuery: string = "";
  pageNumber = 0;
  count = 0;
  pageSize = 10;
  rows: LeaveHrView[] = [];
  employees: LeaveHrView;
  leaveDeatils: EmployeeLeaveDetails;
  loadingIndicator: boolean = true;

  @ViewChild('indexTemplate')
  indexTemplate: TemplateRef<any>;
  @ViewChild('actionsTemplate')
  actionsTemplate: TemplateRef<any>;
  @ViewChild('employeeLeaveDetails')
  employeeLeaveDetails: EmployeeLeaveInfoComponent;

  constructor(private manageLeaveService: ManageLeaveService, private alertService: AlertService, private accountService: AccountService) { }

  ngOnInit() {
    this.columns = [
      { prop: 'employeeName', name: 'Employee' },
      { prop: 'department', name: 'Department' },
      { prop: 'remainingLeave', name: 'Remaining Leaves' },
      { prop: 'Action', name: 'Action', cellTemplate: this.actionsTemplate, resizeable: false, canAutoResize: false, sortable: false, draggable: false }
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
    this.manageLeaveService.getAllEmployees(this.accountService.currentUser.id, page, pageSize, name).subscribe(result => this.onSuccessfulDataLoad(result), error => this.onDataLoadFailed(error));
  }
  onSuccessfulDataLoad(leaveList: LeaveHrViewModel) {
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
