import { Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { LeaveType, LeaveTypeModel } from '../../../../models/configuration/leave/leave-type.model';
import { LeaveTypeService } from '../../../../services/configuration/leave/leave-type.service';
import { AlertService } from '../../../../services/common/alert.service';
import { LeaveTypeInfoComponent } from '../leave-type-info/leave-type-info.component';

@Component({
  selector: 'leave-type',
  templateUrl: './leave-type.component.html',
  styleUrls: ['./leave-type.component.css']
})
export class LeaveTypeComponent implements OnInit {

  columns: any[] = [];
  filterQuery: string = "";
  pageNumber = 0;
  count = 0;
  pageSize = 10;
  rows: LeaveType[] = [];
  public leaveType: LeaveType = new LeaveType();
  editedLeavePeriod: LeaveType;
  loadingIndicator: boolean = true;

  @ViewChild('indexTemplate')
  indexTemplate: TemplateRef<any>;

  @ViewChild('actionsTemplate')
  actionsTemplate: TemplateRef<any>;

  @ViewChild('leaveTypeInfo')
  leaveTypeInfo: LeaveTypeInfoComponent;

  constructor(private leaveTypeService: LeaveTypeService, private alertService: AlertService) { }

  ngOnInit() {
    this.columns = [
      { prop: "index", name: '#', cellTemplate: this.indexTemplate, canAutoResize: false },
      { prop: 'name', name: 'Name' },
      { prop: 'Action', name: 'Action', cellTemplate: this.actionsTemplate, resizeable: false, canAutoResize: false, sortable: false, draggable: false }
    ];
    this.getAllLeaveType(this.pageNumber, this.pageSize, this.filterQuery);
  }
  ngAfterViewInit() {
    this.leaveTypeInfo.serverCallback = () => {
      this.getAllLeaveType(this.pageNumber, this.pageSize, this.filterQuery);
    };
  }

  getFilteredData(filterString) {
    this.getAllLeaveType(this.pageNumber, this.pageSize, filterString);
  }

  getData(e) {
    this.getAllLeaveType(this.pageNumber, e, this.filterQuery);
  }

  setPage(e) {
    this.getAllLeaveType(e.offset, this.pageSize, this.filterQuery);
  }


  getAllLeaveType(page?: number, pageSize?: number, name?: string) {
    this.leaveTypeService.getAllLeaveType(page, pageSize, name).subscribe(result => this.onSuccessfulDataLoad(result), error => this.onDataLoadFailed());
  }
  onSuccessfulDataLoad(leaveType: LeaveTypeModel) {
    this.rows = leaveType.leaveTypeModel;
    leaveType.leaveTypeModel.forEach((leave, index) => {
      (<any>leave).index = index + 1;
    });
    this.count = leaveType.totalCount;
    this.loadingIndicator = false;
  }

  onDataLoadFailed() {
    this.loadingIndicator = false;
    this.alertService.showInfoMessage("Unable to retrieve list from the server");

  }
  newLeaveType() {
    this.leaveType = this.leaveTypeInfo.createLeaveType();
  }
  editLeaveType(leaveType: LeaveType) {
    this.editedLeavePeriod = this.leaveTypeInfo.editLeaveType(leaveType);
  }

  deleteLeaveType(leaveType: LeaveType) {
    this.alertService.showConfirm("Are you sure you want to delete?", () => this.deleteLeaveTypeHelper(leaveType));
  }

  deleteLeaveTypeHelper(leaveType: LeaveType) {
    this.leaveTypeService.delete(leaveType.id).subscribe(() => {
      this.getAllLeaveType(this.pageNumber, this.pageSize, this.filterQuery);
    },
      () => {
        this.alertService.showInfoMessage("An error occured while  deleting")
      });

  }
}
