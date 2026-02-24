import { Component, OnInit, ViewChild } from '@angular/core';
import { EmployeeLeaveListComponent } from '../employee-leave-list/employee-leave-list.component';
import { EmployeeLeaveInfoListComponent } from '../employee-leave-info-list/employee-leave-info-list.component';
import { LeaveCalendarComponent } from '../../my-leaves/leave-calendar/leave-calendar.component';
import { ManageLeaveService } from '../../../../services/leave/manage-leave.service';
import { AccountService } from '../../../../services/account/account.service';
import { AlertService } from '../../../../services/common/alert.service';

@Component({
  selector: 'manage-leaves',
  templateUrl: './manage-leaves.component.html',
  styleUrls: ['./manage-leaves.component.css']
})
export class ManageLeavesComponent implements OnInit {

  filterQuery: string = "";
  pageNumber = 0;
  pageSize = 10;
  public isSet: boolean = false;
  loadingIndicator: boolean = true;
  public leave: string;
  @ViewChild('employeeLeaveList')
  employeeLeaveList: EmployeeLeaveListComponent;
  @ViewChild('leaveInfoListComponent')
  leaveInfoListComponent: EmployeeLeaveInfoListComponent;

  @ViewChild('leaveCalendarComponent')
  leaveCalendarComponent: LeaveCalendarComponent;

  constructor(private manageLeaveService: ManageLeaveService, private accountService: AccountService, private alertService: AlertService) { }

  ngOnInit() {
    this.checkConfig()
  }
  ngAfterViewInit() {
    this.employeeLeaveList.serverCallback = () => {
      this.leaveInfoListComponent.getAllemployee(this.pageNumber, this.pageSize, this.filterQuery);
      this.leaveCalendarComponent.getCalenderEvents();
    };
  }

  checkConfig() {
    this.manageLeaveService.checkConfig(this.accountService.currentUser.id).subscribe(result => this.onSuccessfulDataLoad(result), error => this.onDataLoadFailed(error));
  }


  onSuccessfulDataLoad(result: any) {
    if (result == 1) {
      this.isSet = false;
    } else {
      this.isSet = true;
    }
    this.loadingIndicator = false;
  }

  onDataLoadFailed(error: any) {
    this.alertService.showInfoMessage(error.error);
    this.loadingIndicator = false;
  }
}
