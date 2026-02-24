import { Component, OnInit, ViewChild, TemplateRef } from '@angular/core';
import { LeaveHolidayListService } from '../../../../services/configuration/leave/leave-holiday-list.service';
import { HolidayModel, Holiday } from '../../../../models/configuration/leave/leave-holiday.model';
import { AlertService } from '../../../../services/common/alert.service';
import { LeaveHolidaylistInfoComponent } from '../leave-holidaylist-info/leave-holidaylist-info.component';

@Component({
  selector: 'leave-holidaylist',
  templateUrl: './leave-holidaylist.component.html',
  styleUrls: ['./leave-holidaylist.component.css']
})
export class LeaveHolidaylistComponent implements OnInit {

  columns: any[] = [];
  filterQuery: string = "";
  pageNumber = 0;
  count = 0;
  pageSize = 10;
  rows: Holiday[] = [];
  editedHoliday: Holiday;
  public holiday: Holiday = new Holiday();
  loadingIndicator: boolean = true;

  @ViewChild('holidayDateTemplate')
  holidayDateTemplate: TemplateRef<any>;

  @ViewChild('indexTemplate')
  indexTemplate: TemplateRef<any>;

  @ViewChild('actionsTemplate')
  actionsTemplate: TemplateRef<any>;

  @ViewChild('leaveholidayInfo')
  leaveholidayInfo: LeaveHolidaylistInfoComponent;

  constructor(private leaveHolidayListService: LeaveHolidayListService, private alertService: AlertService) { }

  ngOnInit() {
    this.columns = [
      { prop: "index", name: '#', cellTemplate: this.indexTemplate, canAutoResize: false },
      { prop: 'name', name: 'Name' },
      { prop: 'holidayDate', name: 'Holiday Date', cellTemplate: this.holidayDateTemplate },
      { prop: 'Action', name: 'Action', cellTemplate: this.actionsTemplate, resizeable: false, canAutoResize: false, sortable: false, draggable: false }
    ]
    this.getAllHoliday(this.pageNumber, this.pageSize, this.filterQuery);
  }

  getFilteredData(filterString) {
    this.getAllHoliday(this.pageNumber, this.pageSize, filterString);

  }

  getData(e) {
    this.getAllHoliday(this.pageNumber, e, this.filterQuery);
  }

  setPage(e) {
    this.getAllHoliday(e.offset, this.pageSize, this.filterQuery);
  }

  ngAfterViewInit() {
    this.leaveholidayInfo.serverCallback = () => {
      this.getAllHoliday(this.pageNumber, this.pageSize, this.filterQuery);
    };
  }

  getAllHoliday(page?: number, pageSize?: number, name?: string) {
    this.leaveHolidayListService.getAllHolidayList(page, pageSize, name).subscribe(result => this.onSuccessfulDataLoad(result), error => this.onDataLoadFailed());
  }
  onSuccessfulDataLoad(holiday: HolidayModel) {
    this.rows = holiday.leaveHolidayListModel;
    holiday.leaveHolidayListModel.forEach((holidays, index) => {
      (<any>holidays).index = index + 1;
    });
    this.count = holiday.totalCount;
    this.loadingIndicator = false;
  }

  onDataLoadFailed() {
    this.alertService.showInfoMessage("Unable to retrieve list from the server");
  }

  newLeaveHoliday() {
    this.holiday = this.leaveholidayInfo.createHolidayList();
  }

  editLeaveHoliday(holiday: Holiday) {
    this.editedHoliday = this.leaveholidayInfo.editHolidayList(holiday);
  }


  deleteLeaveHoliday(holiday: Holiday) {

    this.alertService.showConfirm("Are you sure you want to delete?", () => this.deleteLeaveHolidayHelper(holiday))
  }

  deleteLeaveHolidayHelper(holiday: Holiday) {
    this.leaveHolidayListService.delete(holiday.id).subscribe(() => {
      this.getAllHoliday(this.pageNumber, this.pageSize, this.filterQuery);
    },
      () => {
        this.alertService.showInfoMessage("An error occured while  deleting")
      });
  }
}
