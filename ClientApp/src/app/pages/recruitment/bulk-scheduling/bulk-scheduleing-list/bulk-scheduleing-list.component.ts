import { Component, OnInit, ViewChild, TemplateRef } from '@angular/core';
import { BulkScheduleingList, BulkScheduleingListModel } from '../../../../models/recruitment/bulk-scheduling/bulk-scheduleing-list.model';
import { Router } from '@angular/router';
import { BulkInterviewScheduleService } from '../../../../services/recruitment/bulk-interview-schedule.service';
import { AlertService } from '../../../../services/common/alert.service';

@Component({
  selector: 'bulk-scheduleing-list',
  templateUrl: './bulk-scheduleing-list.component.html',
  styleUrls: ['./bulk-scheduleing-list.component.css']
})
export class BulkScheduleingListComponent implements OnInit {

  columns: any[] = [];
  filterQuery: string = "";
  pageNumber = 0;
  count = 0;
  pageSize = 10;
  rows: BulkScheduleingList[] = [];
  loadingIndicator: boolean = true;
  public bulkScheduling: BulkScheduleingList = new BulkScheduleingList();


  @ViewChild('actionsTemplate')
  actionsTemplate: TemplateRef<any>;


  constructor(private router: Router, private bulkInterviewScheduleService: BulkInterviewScheduleService, private alertService: AlertService) { }

  ngOnInit() {
    this.columns = [
      { prop: "index", name: '#', canAutoResize: false },
      { prop: 'fromDate', name: 'From Date' },
      { prop: 'toDate', name: 'To Date' },
      { prop: 'venue', name: 'Venue' },
      { prop: 'Action', name: 'Action', cellTemplate: this.actionsTemplate, resizeable: false, canAutoResize: false, sortable: false, draggable: false }
    ];
    this.getBulkSchedulingList(this.pageNumber, this.pageSize, this.filterQuery);
  }
  ngAfterViewInit() {
    this.getBulkSchedulingList(this.pageNumber, this.pageSize, this.filterQuery);
  }
  getBulkSchedulingList(page?: number, pageSize?: number, name?: string) {
    this.bulkInterviewScheduleService.bulkScheduleList(page, pageSize, name).subscribe(result => this.onSuccessfulDataLoad(result), error => this.onDataLoadFailed(error));
  }

  getData(e) {
    this.getBulkSchedulingList(this.pageNumber, e, this.filterQuery);
  }
  setPage(e) {
    this.getBulkSchedulingList(e.offset, this.pageSize, this.filterQuery);
  }
  getFilteredData(filterString) {
    this.getBulkSchedulingList(this.pageNumber, this.pageSize, filterString);

  }

  onSuccessfulDataLoad(scheduleList: BulkScheduleingListModel) {
    this.rows = scheduleList.massSchedulingList;
    scheduleList.massSchedulingList.forEach((schedule, index, scheduleList) => {
      (<any>schedule).index = index + 1;
    });
    this.count = scheduleList.totalCount;
    this.loadingIndicator = false;
  }

  onDataLoadFailed(error: any) {
    this.alertService.showInfoMessage("Unable to retrieve list from the server");
    this.loadingIndicator = false;
  }

  addBulkSchedule() {
    this.router.navigate(['../recruitment/bulk-scheduling']);
  }
  editBulkSchedule(id) {
    this.router.navigate(['../recruitment/bulk-scheduling'], { queryParams: { id: id } });
  }
  deleteBulkSchedule(id) {

    this.alertService.showConfirm("Are you sure you want to delete?", () => this.deleteBandHelper(id));
  }
  deleteBandHelper(id) {
    this.bulkInterviewScheduleService.deleteSchedule(id).subscribe(results => {
      this.getBulkSchedulingList(this.pageNumber, this.pageSize, this.filterQuery);
    },
      error => {
        this.alertService.showInfoMessage("An error occured whilst deleting");
      });
  }
}
