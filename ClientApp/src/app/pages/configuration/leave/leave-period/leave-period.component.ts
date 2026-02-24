import { Component, OnInit, ViewChild, TemplateRef } from '@angular/core';
import { LeavePeriod, LeavePeriodModel } from '../../../../models/configuration/leave/leave-period.model';
import { LeavePeriodService } from '../../../../services/configuration/leave/leave-period.service';
import { AlertService } from '../../../../services/common/alert.service';
import { LeavePeriodInfoComponent } from '../leave-period-info/leave-period-info.component';

@Component({
  selector: 'leave-period',
  templateUrl: './leave-period.component.html',
  styleUrls: ['./leave-period.component.css']
})
export class LeavePeriodComponent implements OnInit {

  columns: any[] = [];
  filterQuery: string = "";
  pageNumber = 0;
  count = 0;
  pageSize = 10;
  rows: LeavePeriod[] = [];
  editedLeavePeriod: LeavePeriod;
  public leavePeriod: LeavePeriod = new LeavePeriod();
  loadingIndicator: boolean = true;

  @ViewChild('indexTemplate')
  indexTemplate: TemplateRef<any>;

  @ViewChild('actionsTemplate')
  actionsTemplate: TemplateRef<any>;

  @ViewChild('startDateTemplate')
  startDateTemplate: TemplateRef<any>;

  @ViewChild('endDateTemplate')
  endDateTemplate: TemplateRef<any>;

  @ViewChild('leavePeriodInfo')
  leavePeriodInfo: LeavePeriodInfoComponent;



  constructor(private leavePeriodService: LeavePeriodService, private alertService: AlertService) { }

  ngOnInit() {
    this.columns = [
      { prop: "index", name: '#', cellTemplate: this.indexTemplate, canAutoResize: false },
      { prop: 'name', name: 'Name' },
      { prop: 'Start Date', name: 'Start Date', cellTemplate: this.startDateTemplate },
      { prop: 'End Date', name: 'End Date', cellTemplate: this.endDateTemplate },
      { prop: 'Action', name: 'Action', cellTemplate: this.actionsTemplate, resizeable: false, canAutoResize: false, sortable: false, draggable: false }
    ];
    this.getAllLeavePeriod(this.pageNumber, this.pageSize, this.filterQuery);
  }

  ngAfterViewInit() {
    this.leavePeriodInfo.serverCallback = () => {
      this.getAllLeavePeriod(this.pageNumber, this.pageSize, this.filterQuery);
    };
  }

  getFilteredData(filterString) {
    this.getAllLeavePeriod(this.pageNumber, this.pageSize, filterString);
  }

  getData(e) {
    this.getAllLeavePeriod(this.pageNumber, e, this.filterQuery);
  }

  setPage(e) {
    this.getAllLeavePeriod(e.offset, this.pageSize, this.filterQuery);
  }

  getAllLeavePeriod(page?: number, pageSize?: number, name?: string) {
    this.leavePeriodService.getAllLeavePeriod(page, pageSize, name).subscribe(result => this.onSuccessfulDataLoad(result), error => this.onDataLoadFailed());
  }
  onSuccessfulDataLoad(period: LeavePeriodModel) {
    this.rows = period.leavePeriodModel;
    period.leavePeriodModel.forEach((leave, index) => {
      (<any>leave).index = index + 1;
    });
    this.count = period.totalCount;
    this.loadingIndicator = false;

  }
  onDataLoadFailed() {
    this.alertService.showInfoMessage("Unable to retrieve list from the server");
    this.loadingIndicator = false;
  }

  newLeavePeriod() {
    this.leavePeriod = this.leavePeriodInfo.createLeavePeriod();
  }

  editLeavePeriod(leavePeriod: LeavePeriod) {
    this.editedLeavePeriod = this.leavePeriodInfo.editLeavePeriod(leavePeriod);
  }
  deleteLeavePeriod(leavePeriod: LeavePeriod) {
    this.alertService.showConfirm("Are you sure you want to delete?", () => this.deleteLeavePeriodHelper(leavePeriod));
  }

  deleteLeavePeriodHelper(leavePeriod: LeavePeriod) {
    this.leavePeriodService.delete(leavePeriod.id).subscribe(() => {
      this.getAllLeavePeriod(this.pageNumber, this.pageSize, this.filterQuery);
    },
      () => {
        this.alertService.showInfoMessage("An error occured while  deleting")
      });

  }




}
