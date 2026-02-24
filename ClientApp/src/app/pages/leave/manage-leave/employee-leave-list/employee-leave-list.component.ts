import { Component, OnInit, ViewChild, TemplateRef } from '@angular/core';
import { EmployeeLeaveDetailComponent } from '../employee-leave-detail/employee-leave-detail.component';
import { EmployeeLeaveList } from '../../../../models/leave/leave-EmployeeLeaveList.model';
import { ManageLeaveService } from '../../../../services/leave/manage-leave.service';
import { AlertService } from '../../../../services/common/alert.service';
import { AccountService } from '../../../../services/account/account.service';
import { ManageLeaveList, SubordinateLeaveListModel } from '../../../../models/leave/manage-leave-list.model';
import { leave } from '../../../../../../node_modules/@angular/core/src/profile/wtf_impl';

@Component({
  selector: 'employee-leave-list',
  templateUrl: './employee-leave-list.component.html',
  styleUrls: ['./employee-leave-list.component.css']
})

export class EmployeeLeaveListComponent implements OnInit {
  columns: any[] = [];
  rows: ManageLeaveList[] = [];
  filterQuery: string = "";
  pageNumber = 0;
  count = 0;
  pageSize = 10;
  loadingIndicator: boolean = true;

  @ViewChild('indexTemplate')
  indexTemplate: TemplateRef<any>;
  @ViewChild('startDateTemplate')
  startDateTemplate: TemplateRef<any>;
  @ViewChild('endDateTemplate')
  endDateTemplate: TemplateRef<any>;
  @ViewChild('actionsTemplate')
  actionsTemplate: TemplateRef<any>;
  @ViewChild('employeeLeaveDetails')
  employeeLeaveDetails: EmployeeLeaveDetailComponent;
  public serverCallback: () => void



  constructor(private manageLeaveService: ManageLeaveService, private alertService: AlertService, private accountService: AccountService) { }

  ngOnInit() {
    this.columns = [
      { prop: "index", name: '#', cellTemplate: this.indexTemplate, canAutoResize: false },
      { prop: 'employeeName', name: 'Employee Name' },
      { prop: 'startDate', name: 'Start Date', cellTemplate: this.startDateTemplate },
      { prop: 'endDate', name: 'End Date', cellTemplate: this.endDateTemplate },
      { prop: 'noOfDays', name: 'No Of Days' },
      { prop: 'status', name: 'Status' },
      { prop: 'Action', name: 'Action', cellTemplate: this.actionsTemplate, resizeable: false, canAutoResize: false, sortable: false, draggable: false }
    ];
    this.getManageleaveList(this.pageNumber, this.pageSize, this.filterQuery);
  }


  ngAfterViewInit() {
    this.employeeLeaveDetails.manageleavelistCallback = () => {
      this.serverCallback();
      this.getManageleaveList(this.pageNumber, this.pageSize, this.filterQuery);
    };


  }

  epmLeaveDetails(leaveList: ManageLeaveList) {
    this.employeeLeaveDetails.empLeaveDeatils(leaveList.leaveDeatilId)
  }

  viewLeaveDetails(leaveList: ManageLeaveList) {
    this.employeeLeaveDetails.viewLeaveDetails(leaveList.leaveDeatilId)
  }

  getFilteredData(filterString) {
    this.getManageleaveList(this.pageNumber, this.pageSize, filterString);
  }

  getData(e) {
    this.getManageleaveList(this.pageNumber, e, this.filterQuery);
  }

  setPage(e) {
    this.getManageleaveList(e.offset, this.pageSize, this.filterQuery);
  }

  getManageleaveList(page?: number, pageSize?: number, name?: string) {
    this.manageLeaveService.getManageleaveList(this.accountService.currentUser.id, page, pageSize, name).subscribe(result => this.onSuccessfulDataLoad(result), error => this.onDataLoadFailed(error));
  }
  onSuccessfulDataLoad(allmanageLeaveList: SubordinateLeaveListModel) {
    this.rows = allmanageLeaveList.subordinateLeaveListModel;
    allmanageLeaveList.subordinateLeaveListModel.forEach((leave, index, allmanageLeaveList) => {
      (<any>leave).index = index + 1;
    });
    this.count = allmanageLeaveList.totalCount;
    this.loadingIndicator = false;
  }
  onDataLoadFailed(error: any) {
    this.loadingIndicator = false;
    this.alertService.showInfoMessage("Unable to retrieve list from the server");

  }

}
