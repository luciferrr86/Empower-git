import { Component, OnInit, AfterViewInit, ViewChild } from '@angular/core';
import { animate, style, transition, trigger } from '@angular/animations';
import { Http } from "@angular/http";
import 'rxjs/add/operator/map'
import { HrView } from '../../../models/performance/hr-view/hr-view.model';
import { HrViewService } from '../../../services/performance/hr-view/hr-view.service';
import { AlertService } from '../../../services/common/alert.service';
import { ManagerListComponent } from './manager-list/manager-list.component';




@Component({
  selector: 'app-hr-view',
  templateUrl: './hr-view.component.html',
  styleUrls: ['./hr-view.component.css'],
  animations: [
    trigger('fadeInOutTranslate', [
      transition(':enter', [
        style({ opacity: 0 }),
        animate('400ms ease-in-out', style({ opacity: 1 }))
      ]),
      transition(':leave', [
        style({ transform: 'translate(0)' }),
        animate('400ms ease-in-out', style({ opacity: 0 }))
      ])
    ])
  ]
})
export class HrViewComponent implements OnInit {
  @ViewChild(ManagerListComponent) manager;
  constructor(private alertService: AlertService, public http: Http, public hrViewService: HrViewService) { }
  columns: any[] = [];
  filterQuery: string = "";
  pageNumber = 0;
  count = 0;
  pageSize = 10;
  public hrView: HrView = new HrView();
  public isInviting: boolean;
  ngOnInit() {
    this.hrViewPreCheck();
  }

  hrViewPreCheck() {
    this.hrViewService.hrViewPreCheck().subscribe(result => this.onSuccessfulPreCheck(result), error => this.onDataLoadFailed(error, "Unable to retrieve data from the server"));
  }
  performanceInvitation() {
    this.isInviting = true;
    if (this.hrView.isConfigurationSet) {
      if (this.hrView.isPerformanceStart) {
        this.alertService.showInfoMessage("Performance process started already.");
        this.isInviting = false;
      }
      else {
        this.hrViewService.performanceInvitation().subscribe(result => this.onSuccessfulDataLoad(result), error => this.onDataLoadFailed(error, "Unable to retrieve list from the server"));
      }
    }
    else {
      this.alertService.showInfoMessage("Please set configuration first.");
      this.isInviting = false;
    }
  }
  onSuccessfulPreCheck(res: HrView) {
    this.hrView = res;

  }
  onSuccessfulDataLoad(res: any) {
    this.isInviting = false;
    this.hrViewPreCheck();
    this.manager.getAllManager(this.pageNumber, this.pageSize, this.filterQuery);
  }

  performanceReminder() {

  }
  onDataLoadFailed(error: any, errMsg: string) {
    this.isInviting = false;
    this.alertService.showInfoMessage(errMsg);
  }

}
