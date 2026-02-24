import { Component, OnInit, ViewChild, TemplateRef } from '@angular/core';
import { AlertService } from '../../../../services/common/alert.service';
import { Http } from '@angular/http';
import { HrViewService } from '../../../../services/performance/hr-view/hr-view.service';
import { HrView, hrViewList } from '../../../../models/performance/hr-view/hr-view.model';

@Component({
  selector: 'manager-list',
  templateUrl: './manager-list.component.html',
  styleUrls: ['./manager-list.component.css']
})
export class ManagerListComponent implements OnInit {

  @ViewChild('actionsTemplate')
  actionsTemplate: TemplateRef<any>;
  constructor(private alertService: AlertService, public http: Http, public hrViewService: HrViewService) { }

  columns: any[] = [];
  filterQuery: string = "";
  pageNumber = 0;
  count = 0;
  pageSize = 10;
  public hrView: HrView = new HrView();
  mgrRows: hrViewList[];
  ngOnInit() {

    this.columns = [
      { prop: "index", name: '#', canAutoResize: false },
      { prop: 'name', name: 'Manager Name' },
      { prop: 'designation', name: 'Designation' },
      { prop: 'group', name: 'Group' },
      { prop: 'status', name: 'Status' },
      { prop: 'Action', name: 'Action', cellTemplate: this.actionsTemplate, resizeable: false, canAutoResize: false, sortable: false, draggable: false }
    ];
    this.getAllManager(this.pageNumber, this.pageSize, this.filterQuery);
  }


  getFilteredData(filterString) {
    this.getAllManager(this.pageNumber, this.pageSize, filterString);

  }
  getData(e) {
    this.getAllManager(this.pageNumber, e, this.filterQuery);
  }
  setPage(e) {
    this.getAllManager(e.offset, this.pageSize, this.filterQuery);
  }

  getAllManager(page?: number, pageSize?: number, name?: string) {
    this.hrViewService.getAllManager(page, pageSize, name).subscribe(result => this.onSuccessfulDataLoad(result), error => this.onDataLoadFailed());
  }


  onSuccessfulDataLoad(hrView: HrView) {
    if (hrView.lstManager != null && hrView.lstManager.length > 0) {
      this.mgrRows = hrView.lstManager;
      hrView.lstManager.forEach((manager, index) => {
        (<any>manager).index = index + 1;
      });
      this.count = hrView.totalCount;
    }
  }

  onDataLoadFailed() {
    this.alertService.showInfoMessage("Unable to retrieve list from the server");
  }

  sendReminder(id: string) {
    this.hrViewService.reminderManagerInvitation(id).subscribe(result => this.onSuccessSendReminder(), error => this.onDataLoadFailed());
    //mail
  }
  onSuccessSendReminder() {
    this.alertService.showInfoMessage("mail send successfuly");
  }

}
