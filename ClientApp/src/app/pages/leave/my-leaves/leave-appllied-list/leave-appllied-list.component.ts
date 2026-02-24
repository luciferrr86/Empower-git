import { Component, OnInit, ViewChild, TemplateRef, Output, EventEmitter } from '@angular/core';
import { LeaveApplliedDetailComponent } from '../leave-appllied-detail/leave-appllied-detail.component';
import { MyLeaveService } from '../../../../services/leave/my-leave.service';
import { AlertService } from '../../../../services/common/alert.service';
import { AccountService } from '../../../../services/account/account.service';
import { EmployeeLeaveList, EmployeeLeaveListModel } from '../../../../models/leave/leave-employeeLeaveList.model';
import { Utilities } from '../../../../services/common/utilities';


@Component({
  selector: 'leave-appllied-list',
  templateUrl: './leave-appllied-list.component.html',
  styleUrls: ['./leave-appllied-list.component.css']
})
export class LeaveApplliedListComponent implements OnInit {

  columns: any[] = [];
  rows: EmployeeLeaveList[] = [];
  filterQuery: string = "";
  pageNumber = 0;
  count = 0;
  pageSize = 10;
  loadingIndicator: boolean = true;
  public leavecancellistCallback: () => void;

  @Output() loadinfo = new EventEmitter<string>();

  @ViewChild('leaveDetail')
  leaveDetail: LeaveApplliedDetailComponent;

  @ViewChild('indexTemplate')
  indexTemplate: TemplateRef<any>;

  @ViewChild('startDateTemplate')
  startDateTemplate: TemplateRef<any>;

  @ViewChild('endDateTemplate')
  endDateTemplate: TemplateRef<any>;

  @ViewChild('actionsTemplate')
  actionsTemplate: TemplateRef<any>;

  constructor(private myLeaveService: MyLeaveService, private alertService: AlertService, private accountService: AccountService) { }

  ngOnInit() {
    this.columns = [
      { prop: "index", name: '#', cellTemplate: this.indexTemplate, canAutoResize: false },
      { prop: 'leaveType', name: 'Leave Type' },
      { prop: 'startDate', name: 'Start Date', cellTemplate: this.startDateTemplate },
      { prop: 'endDate', name: 'End Date', cellTemplate: this.endDateTemplate },
      { prop: 'status', name: 'Status' },
      { prop: 'Action', name: 'Action', cellTemplate: this.actionsTemplate, resizeable: false, canAutoResize: false, sortable: false, draggable: false }
    ];
    this.getAllEmployeeLeave(this.pageNumber, this.pageSize, this.filterQuery);
  }


  getFilteredData(filterString) {
    this.getAllEmployeeLeave(this.pageNumber, this.pageSize, filterString);
  }

  getData(e) {
    this.getAllEmployeeLeave(this.pageNumber, e, this.filterQuery);
  }

  setPage(e) {
    this.getAllEmployeeLeave(e.offset, this.pageSize, this.filterQuery);
  }

  getAllEmployeeLeave(page?: number, pageSize?: number, name?: string) {
    this.myLeaveService.getAllEmployeeLeave(this.accountService.currentUser.id, page, pageSize, name).subscribe(result => this.onSuccessfulDataLoad(result), error => this.onDataLoadFailed());
  }
  onSuccessfulDataLoad(allLeaveList: EmployeeLeaveListModel) {
    this.rows = allLeaveList.employeeLeaveListModel;
    allLeaveList.employeeLeaveListModel.forEach((leave, index) => {
      (<any>leave).index = index + 1;
    });
    this.count = allLeaveList.totalCount;
    this.loadingIndicator = false;
  }
  onDataLoadFailed() {
    this.loadingIndicator = false;
    this.alertService.showInfoMessage("Unable to retrieve list from the server");
  }

  leaveDetails(leaveList: EmployeeLeaveList) {
    this.leaveDetail.leaveDetails(leaveList.leaveDeatilId);
  }
  retractLeave(leaveList: EmployeeLeaveList) {
    this.alertService.showConfirmCancel("Are you sure you want to retract?", "Retract", "Leave retracted sucessfully", () => this.retractLeaveHelper(leaveList));
  }
  retractLeaveHelper(leaveList: EmployeeLeaveList) {
    this.myLeaveService.retractLeave(leaveList.leaveDeatilId).subscribe(sucess => this.retractSuccessHelper(), error => this.retractFailedHelper(error));
  }

  private retractFailedHelper(error: any) {
    this.loadingIndicator = false;
    let test = Utilities.getHttpResponseMessage(error);
    this.alertService.showInfoMessage("Please try later" + test[0]);
  }
  private retractSuccessHelper() {
    this.leavecancellistCallback();
  }


  cancelLeave(leaveList: EmployeeLeaveList) {
    this.alertService.showConfirmCancel("Are you sure you want to cancel?", "Cancel", "Leave cancelled sucessfully", () => this.cancelLeaveHelper(leaveList));
  }
  cancelLeaveHelper(leaveList: EmployeeLeaveList) {
    this.myLeaveService.cancelLeave(leaveList.leaveDeatilId).subscribe(sucess => this.saveSuccessHelper(), error => this.saveFailedHelper(error));
  }

  private saveFailedHelper(error: any) {
    this.loadingIndicator = false;
    let test = Utilities.getHttpResponseMessage(error);
    this.alertService.showInfoMessage("Please try later" + test[0]);
  }
  private saveSuccessHelper() {
    this.leavecancellistCallback();
  }
}
