import { Component, OnInit, ViewChild, TemplateRef } from '@angular/core';
import { StatusCompany, StatusModel, DailyCall } from '../../../models/sales-marketing/Status-view.model';
import { AlertService } from '../../../services/common/alert.service';
import { Router, ActivatedRoute } from "@angular/router";
import { ModalDirective } from 'ngx-bootstrap/modal';
import { IOption } from 'ng-select';
import { SalesMarketingService } from '../../../services/sales-marketing/sales-marketing.service';
import { Utilities } from '../../../services/common/utilities';

@Component({
  selector: 'status-list',
  templateUrl: './status-list.component.html',
  styleUrls: ['./status-list.component.css']
})
export class StatusListComponent implements OnInit {
  columns: any[] = [];
  filterQuery: string = "";
  pageNumber = 0;
  count = 0;
  pageSize = 10;
  rows: DailyCall[] = [];
  public companyId = "";
  loadingIndicator: boolean = true;
  modalTitle: string;
  public statusModal: StatusModel = new StatusModel();
  status: StatusCompany = new StatusCompany();
  statusListOption: Array<IOption> = [];
  public isSaving = false;

  @ViewChild('actionsTemplate')
  actionsTemplate: TemplateRef<any>;

  @ViewChild('callDateTemplate')
  callDateTemplate: TemplateRef<any>;

  @ViewChild('editorModal')
  editorModal: ModalDirective;

  constructor(private route: ActivatedRoute, private router: Router, private salesMarketingService: SalesMarketingService, private alertService: AlertService) { }
  ngOnInit() {
    this.columns = [

      { prop: 'name', name: 'Status' },
      { prop: 'callDateTime', name: 'Date', cellTemplate: this.callDateTemplate },
      { prop: 'description', name: 'Description' },
      // { prop: 'Action', name: 'Action', cellTemplate: this.actionsTemplate }
    ];
    this.getCompanyStatus();
    //this.getAllStatus(this.pageNumber, this.pageSize, this.filterQuery);
  }
  getCompanyStatus() {
    this.route.queryParams.subscribe(params => {
      if (params['id']) {
        this.companyId = params['id'];
        this.getAllStatus();
      }
    });
  }

  // getFilteredData(filterString) {
  //   this.getAllStatus(this.pageNumber, this.pageSize, filterString);
  // }

  // getData(e) {
  //   this.getAllStatus(this.pageNumber, e, this.filterQuery);
  // }

  // setPage(e) {
  //   this.getAllStatus(e.offset, this.pageSize, this.filterQuery);
  // }

  getAllStatus() {
    this.salesMarketingService.getCompnayStatus(this.companyId).subscribe(result => this.onSuccessfulDataLoad(result), error => this.onDataLoadFailed(error));
  }
  onSuccessfulDataLoad(statusList: StatusModel) {
    this.statusModal.statusCompanyModel = statusList.statusCompanyModel;
    this.statusListOption = statusList.ddlSaleStatus;
    this.rows = statusList.dailyCallModel;
    statusList.dailyCallModel.forEach((company, index, companys) => {
      (<any>company).index = index + 1;
    });
    this.loadingIndicator = false;
  }

  onDataLoadFailed(error: any) {
    this.loadingIndicator = false;
    this.alertService.showInfoMessage("Unable to retrieve list from the server");

  }
  addStatus() {
    this.editorModal.show();
    this.modalTitle = "Add";
  }

  public save() {
    this.isSaving = true;
    this.status.salesCompanyId = this.companyId
    this.salesMarketingService.saveStatus(this.status).subscribe(sucess => this.saveSuccessHelper(sucess), error => this.saveFailedHelper(error));
  }


  private saveSuccessHelper(result?: string) {
    this.isSaving = false;
    this.getAllStatus();
    this.alertService.showSucessMessage("Status saved successfully");
    this.editorModal.hide();


  }

  private saveFailedHelper(error: any) {
    this.isSaving = false;
    let test = Utilities.getHttpResponseMessage(error);
    this.alertService.showInfoMessage("Please try later" + test[0]);
  }

}
