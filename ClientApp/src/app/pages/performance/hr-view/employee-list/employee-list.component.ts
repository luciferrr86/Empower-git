import { Component, OnInit, ViewChild, TemplateRef } from '@angular/core';
import { HrViewService } from '../../../../services/performance/hr-view/hr-view.service';
import { AlertService } from '../../../../services/common/alert.service';
import { Http } from '@angular/http';
import { HrView, hrViewList } from '../../../../models/performance/hr-view/hr-view.model';

@Component({
  selector: 'employee-list',
  templateUrl: './employee-list.component.html',
  styleUrls: ['./employee-list.component.css']
})
export class EmployeeListComponent implements OnInit {
  @ViewChild('actionsTemplate')
  actionsTemplate: TemplateRef<any>;
  constructor(private alertService: AlertService, public http: Http, public hrViewService: HrViewService) { }
  columns: any[] = [];
  filterQuery: string = "";
  pageNumber = 0;
  count = 0;
  pageSize = 10;
  public hrView: HrView = new HrView();
  empRows: Array<hrViewList>;
  loadingIndicator: boolean = false;
  ngOnInit() {
    this.columns = [
      { prop: "index", name: '#', canAutoResize: false },
      { prop: 'name', name: 'Employee Name' },
      { prop: 'designation', name: 'Designation' },
      { prop: 'department', name: 'Department' },
      { prop: 'status', name: 'Status' },
      { prop: 'Action', name: 'Action', cellTemplate: this.actionsTemplate, resizeable: false, canAutoResize: false, sortable: false, draggable: false }
    ];
    this.getAllEmployee(this.pageNumber, this.pageSize, this.filterQuery);
  }

  getFilteredData(filterString) {
    this.getAllEmployee(this.pageNumber, this.pageSize, filterString);

  }
  getData(e) {
    this.getAllEmployee(this.pageNumber, e, this.filterQuery);
  }
  setPage(e) {
    this.getAllEmployee(e.offset, this.pageSize, this.filterQuery);
  }

  getAllEmployee(page?: number, pageSize?: number, name?: string) {
    this.hrViewService.getAllEmployee(page, pageSize, name).subscribe(result => this.onSuccessfulDataLoad(result), error => this.onDataLoadFailed(error));
  }

  onSuccessfulDataLoad(hrView: HrView) {
    if (hrView.lstEmployee != null && hrView.lstEmployee.length > 0) {
      this.empRows = hrView.lstEmployee;
      hrView.lstEmployee.forEach((employee, index, emp) => {
        (<any>employee).index = index + 1;
      });
      this.count = hrView.totalCount;
    }
  }

  onDataLoadFailed(error: any) {
    this.alertService.showInfoMessage("Unable to retrieve list from the server");
  }

  sendReminder(id: string) {
    this.hrViewService.reminderEmployeeInvitation(id).subscribe(result => this.onSuccessSendReminder(result), error => this.onDataLoadFailed(error));
  }
  onSuccessSendReminder(result: string) {
    this.alertService.showInfoMessage("mail send successfuly");
  }
}
