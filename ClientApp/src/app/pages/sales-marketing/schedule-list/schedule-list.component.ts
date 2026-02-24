import { Component, OnInit, ViewChild, TemplateRef } from '@angular/core';
import { AlertService } from '../../../services/common/alert.service';
import { Router, ActivatedRoute } from "@angular/router";
import { ScheduleCompany, ScheduleModel } from '../../../models/sales-marketing/Schedule-view.model';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { IOption } from 'ng-select';
import { SalesMarketingService } from '../../../services/sales-marketing/sales-marketing.service';

@Component({
  selector: 'schedule-list',
  templateUrl: './schedule-list.component.html',
  styleUrls: ['./schedule-list.component.css']
})
export class ScheduleListComponent implements OnInit {

  columns: any[] = [];
  filterQuery: string = "";
  pageNumber = 0;
  count = 0;
  pageSize = 10;
  public companyId = "";
  rows: ScheduleCompany[] = [];
  schedule: ScheduleCompany = new ScheduleCompany();
  public scheduleModal: ScheduleModel = new ScheduleModel();
  loadingIndicator: boolean = true;
  clientmaillist: Array<IOption> = [];
  internalmaillist: Array<IOption> = [];
  public isSaving = false;

  @ViewChild('actionsTemplate')
  actionsTemplate: TemplateRef<any>;

  @ViewChild('momTemplate')
  momTemplate: TemplateRef<any>;

  @ViewChild('editorModal')
  editorModal: ModalDirective;

  constructor(private route: ActivatedRoute, private router: Router, private salesMarketingService: SalesMarketingService, private alertService: AlertService) { }
  ngOnInit() {
    this.columns = [

      { prop: 'subject', name: 'Subject' },
      { prop: 'mettingDate', name: 'Date' },
      { prop: 'agenda', name: 'Agenda' },
      { prop: 'venue', name: 'Venue' },
      { prop: 'MOM', name: 'MOM', cellTemplate: this.momTemplate },
      { prop: 'Appendix', name: 'Appendix', cellTemplate: this.actionsTemplate }
    ];
    this.getCompanyMeeting();

  }
  getCompanyMeeting() {
    this.route.queryParams.subscribe(params => {
      if (params['id']) {
        this.companyId = params['id'];
        this.getAllSchedule(this.pageNumber, this.pageSize, this.filterQuery);
      }
    });
  }

  getFilteredData(filterString) {
    this.getAllSchedule(this.pageNumber, this.pageSize, filterString);
  }

  getData(e) {
    this.getAllSchedule(this.pageNumber, e, this.filterQuery);
  }

  setPage(e) {
    this.getAllSchedule(e.offset, this.pageSize, this.filterQuery);
  }

  getAllSchedule(page?: number, pageSize?: number, name?: string) {
    this.salesMarketingService.getCompanyMeetinglist(this.companyId, this.pageNumber, this.pageSize, this.filterQuery).subscribe(result => this.onSuccessfulDataLoad(result), error => this.onDataLoadFailed(error));
  }
  onSuccessfulDataLoad(scheduleList: ScheduleModel) {
    this.scheduleModal.statusCompanyModel = scheduleList.statusCompanyModel;
    this.rows = scheduleList.lstSchedule;
    scheduleList.lstSchedule.forEach((company, index, companys) => {
      (<any>company).index = index + 1;
    });
    this.count = scheduleList.totalCount;
    this.loadingIndicator = false;
  }

  onDataLoadFailed(error: any) {
    this.loadingIndicator = false;
    this.alertService.showInfoMessage("Unable to retrieve list from the server");

  }

  addSchedule() {
    this.editorModal.show();
  }

  save() {

  }

  onSelect() {

  }

}
