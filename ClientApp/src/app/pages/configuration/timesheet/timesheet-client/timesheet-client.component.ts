import { Component, OnInit, ViewChild, TemplateRef } from '@angular/core';
import { TimesheetClientService } from '../../../../services/configuration/timesheet/timesheet-client.service';
import { AlertService } from '../../../../services/common/alert.service';
import { ClientModel, ClientViewModel } from '../../../../models/configuration/timesheet/timesheet-client.model';
import { TimesheetClientInfoComponent } from '../timesheet-client-info/timesheet-client-info.component';

@Component({
  selector: 'timesheet-client',
  templateUrl: './timesheet-client.component.html',
  styleUrls: ['./timesheet-client.component.css']
})
export class TimesheetClientComponent implements OnInit {

  columns: any[] = [];
  filterQuery: string = "";
  pageNumber = 0;
  count = 0;
  pageSize = 10;

  rows: ClientModel[] = [];
  client: ClientModel;
  loadingIndicator: boolean = true;

  @ViewChild('clientInfo')
  clientInfo: TimesheetClientInfoComponent;
  @ViewChild('actionsTemplate')
  actionsTemplate: TemplateRef<any>;

  constructor(private clientSerivice: TimesheetClientService, private alertService: AlertService) { }

  ngOnInit() {
    this.columns = [
      { prop: "index", name: '#', canAutoResize: false },
      { prop: 'name', name: 'Name' },
      { prop: 'emailId', name: 'Email Id' },
      { prop: 'contact', name: 'Contact' },
      { prop: 'Action', name: 'Action', cellTemplate: this.actionsTemplate, resizeable: false, canAutoResize: false, sortable: false, draggable: false }
    ];
    this.getAllClient(this.pageNumber, this.pageSize, this.filterQuery);
  }
  ngAfterViewInit() {
    this.clientInfo.serverCallback = () => {
      this.getAllClient(this.pageNumber, this.pageSize, this.filterQuery);
    };
  }
  getAllClient(page?: number, pageSize?: number, name?: string) {
    this.clientSerivice.getAllClient(page, pageSize, name).subscribe(result => this.onSuccessfulDataLoad(result), error => this.onDataLoadFailed(error));
  }

  getData(e) {
    this.getAllClient(this.pageNumber, e, this.filterQuery);
  }
  setPage(e) {
    this.getAllClient(e.offset, this.pageSize, this.filterQuery);
  }
  getFilteredData(filterString) {
    this.getAllClient(this.pageNumber, this.pageSize, filterString);

  }

  onSuccessfulDataLoad(client: ClientViewModel) {
    this.rows = client.clientList;
    client.clientList.forEach((clients, index, client) => {
      (<any>clients).index = index + 1;
    });
    this.count = client.totalCount;
    this.loadingIndicator = false;
  }

  onDataLoadFailed(error: any) {
    this.loadingIndicator = false;
    this.alertService.showInfoMessage("Unable to retrieve list from the server");
  }

  newClient() {
    this.client = this.clientInfo.createClient();
  }
  editClient(client: ClientModel) {
    this.client = this.clientInfo.updateClient(client);
  }
  deleteClient(client: ClientModel) {
    this.alertService.showConfirm("Are you sure you want to delete?", () => this.deleteClienteHelper(client));
  }

  deleteClienteHelper(client: ClientModel) {
    this.clientSerivice.delete(client.id).subscribe(results => {
      this.getAllClient(this.pageNumber, this.pageSize, this.filterQuery);
    },
      error => {
        this.alertService.showInfoMessage("An error occured while  deleting")
      });

  }

}
