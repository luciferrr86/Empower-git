import { Component, OnInit, ViewChild } from '@angular/core';
import { LeaveApply } from '../../../../models/leave/leave-apply.model';
import { LeaveApplyComponent } from '../leave-apply/leave-apply.component';
import { MyLeaveService } from '../../../../services/leave/my-leave.service';
import { AccountService } from '../../../../services/account/account.service';
import { AlertService } from '../../../../services/common/alert.service';
import { DropDownList } from '../../../../models/common/dropdown';
import { LeaveInfoComponent } from '../leave-info/leave-info.component';
import { LeaveApplliedListComponent } from '../leave-appllied-list/leave-appllied-list.component';
import { MyLeave } from '../../../../models/leave/leave-myleave.model';
import { LeaveCalendarComponent } from '../leave-calendar/leave-calendar.component';
import { ActivatedRoute, Router } from '@angular/router';



@Component({
  selector: 'app-my-leave',
  templateUrl: './my-leave.component.html',
  styleUrls: ['./my-leave.component.css']
})
export class MyLeaveComponent implements OnInit {
  filterQuery: string = "";
  pageNumber = 0;
  pageSize = 10;
  allLeaveTypeList: DropDownList[] = [];
  loadingIndicator: boolean = true;
  applyleave: LeaveApply;
  myLeave: MyLeave;
  allleave: number;
  public isSet = false;
  @ViewChild('applyleaves')
  applyleaves: LeaveApplyComponent;
  @ViewChild('leavesinfo')
  leavesinfo: LeaveInfoComponent;
  @ViewChild('leaveApplliedList')
  leaveApplliedList: LeaveApplliedListComponent;

  @ViewChild('leaveCalendar')
  leaveCalendar: LeaveCalendarComponent;

  constructor(private route: ActivatedRoute, private myleaveService: MyLeaveService, private accountService: AccountService, private alertService: AlertService, private routers: Router) {
    this.creatEntitlement();
  }

  ngOnInit() {
    this.route.data
      .subscribe((data: { myLeaveData: MyLeave }) => {
        this.myLeave = data.myLeaveData;
      });
  }

  ngAfterViewInit() {
    this.applyleaves.leaveappliedlistCallback = () => {
      this.leaveApplliedList.getAllEmployeeLeave(this.pageNumber, this.pageSize, this.filterQuery);
      this.leavesinfo.getAllLeaveInfo();
      this.leaveCalendar.getCalenderEvents();
    };
    this.leaveApplliedList.leavecancellistCallback = () => {
      this.leaveApplliedList.getAllEmployeeLeave(this.pageNumber, this.pageSize, this.filterQuery);
      this.leavesinfo.getAllLeaveInfo();
      this.leaveCalendar.getCalenderEvents();
    };
  }
  applyLeave() {
    this.applyleave = this.applyleaves.leaveApply(this.myLeave.ddlleaveType);
  }

  creatEntitlement() {

  }

  onSuccessfulDataLoad(myLeaveModel: MyLeave) {
    this.myLeave = myLeaveModel;
    this.isSet = this.myLeave.isSet;
    this.loadingIndicator = false;
  }

  onDataLoadFailed(error: any) {
    this.alertService.showInfoMessage(error.error);
    this.loadingIndicator = false;
  }

  loadleavesinfo($event) {
    this.leavesinfo.getAllLeaveInfo();
  }
}
