import { Component, OnInit, ViewChild, TemplateRef } from '@angular/core';
import { LeaveRules, LeaveRulesModel } from '../../../../models/configuration/leave/leave-rules.model';
import { LeaveRulesService } from '../../../../services/configuration/leave/leave-rules.service';
import { AlertService } from '../../../../services/common/alert.service';
import { DropDownList } from '../../../../models/common/dropdown';
import { LeaveRulesInfoComponent } from '../leave-rules-info/leave-rules-info.component';

@Component({
  selector: 'leave-rules',
  templateUrl: './leave-rules.component.html',
  styleUrls: ['./leave-rules.component.css']
})
export class LeaveRulesComponent implements OnInit {

  columns: any[] = [];
  filterQuery: string = "";
  pageNumber = 0;
  count = 0;
  pageSize = 10;
  rows: LeaveRules[] = [];
  allBandList: DropDownList[] = [];
  allLeaveTypeList: DropDownList[] = [];
  loadingIndicator: boolean = true;
  editedRules: LeaveRules;

  @ViewChild('indexTemplate')
  indexTemplate: TemplateRef<any>;

  @ViewChild('actionsTemplate')
  actionsTemplate: TemplateRef<any>;

  @ViewChild('leaveRulesEditor')
  leaveRulesEditor: LeaveRulesInfoComponent;

  constructor(private leaveRulesService: LeaveRulesService, private alertService: AlertService) { }

  ngOnInit() {
    this.columns = [
      { prop: "index", name: '#', cellTemplate: this.indexTemplate, canAutoResize: false },
      { prop: 'name', name: 'Name' },
      { prop: 'Action', name: 'Action', cellTemplate: this.actionsTemplate, resizeable: false, canAutoResize: false, sortable: false, draggable: false }
    ];
    this.getAllLeaveRules(this.pageNumber, this.pageSize, this.filterQuery);
  }

  ngAfterViewInit() {
    this.leaveRulesEditor.serverCallback = () => {
      this.getAllLeaveRules(this.pageNumber, this.pageSize, this.filterQuery);
    };
  }
  getAllLeaveRules(page?: number, pageSize?: number, name?: string) {

    this.leaveRulesService.getAllLeaveRules(page, pageSize, name).subscribe(result => this.onSuccessfulDataLoad(result), error => this.onDataLoadFailed(error));
  }

  getFilteredData(filterString) {
    this.getAllLeaveRules(this.pageNumber, this.pageSize, this.filterQuery);

  }

  getData(e) {
    this.getAllLeaveRules(this.pageNumber, e, this.filterQuery);
  }

  setPage(e) {
    this.getAllLeaveRules(e.offset, this.pageSize, this.filterQuery);
  }

  onSuccessfulDataLoad(leaveRules: LeaveRulesModel) {
    this.rows = leaveRules.leaveRulesModel;
    leaveRules.leaveRulesModel.forEach((rules, index, leaveRules) => {
      (<any>rules).index = index + 1;
    });
    this.allBandList = leaveRules.leaveRulesModel[0].bandList;
    this.allLeaveTypeList = leaveRules.leaveRulesModel[0].leaveTypeList;
    this.count = leaveRules.totalCount;
    this.loadingIndicator = false;
  }

  onDataLoadFailed(error: any) {
    this.alertService.showInfoMessage("Unable to retrieve list from the server");
  }


  newLeaveRule() {
    this.editedRules = this.leaveRulesEditor.addleaveRule(this.allBandList, this.allLeaveTypeList);
  }

  editLeaveRule(leaveRules: LeaveRules) {
    this.editedRules = this.leaveRulesEditor.updateleaveRule(this.allBandList, this.allLeaveTypeList, leaveRules)
  }


  deleteLeaveRule(leaveRules: LeaveRules) {
    this.alertService.showConfirm("Are you sure you want to delete?", () => this.deleteLeavePeriodHelper(leaveRules));
  }

  deleteLeavePeriodHelper(leaveRules: LeaveRules) {
    this.leaveRulesService.delete(leaveRules.id)
      .subscribe(results => {
        this.getAllLeaveRules(this.pageNumber, this.pageSize, this.filterQuery);
      },
        error => {
          this.alertService.showInfoMessage("An error occured while  deleting")
        });
  }
}
