import { Component, OnInit } from '@angular/core';
import { LeaveEntitlementModel } from '../../../../models/leave/leave-info.model';
import { MyLeaveService } from '../../../../services/leave/my-leave.service';
import { AlertService } from '../../../../services/common/alert.service';
import { AccountService } from '../../../../services/account/account.service';

@Component({
  selector: 'leave-info',
  templateUrl: './leave-info.component.html',
  styleUrls: ['./leave-info.component.css']
})
export class LeaveInfoComponent implements OnInit {
  loadingIndicator: boolean = true;
  public entitlement: LeaveEntitlementModel = new LeaveEntitlementModel();
  public serverCallback: () => void;
  constructor(private myLeaveService: MyLeaveService, private alertService: AlertService, private accountService: AccountService) { }

  ngOnInit() {
    this.getAllLeaveInfo();
  }

  getAllLeaveInfo() {
    this.myLeaveService.getAllLeaveInfo(this.accountService.currentUser.id).subscribe(result => this.onSuccessfulDataLoad(result), error => this.onDataLoadFailed());
  }

  onSuccessfulDataLoad(leaveEntitlement: LeaveEntitlementModel) {
    this.entitlement = leaveEntitlement;
    this.loadingIndicator = false;
  }

  onDataLoadFailed() {
    this.alertService.showInfoMessage("Unable to retrieve list from the server");
    this.loadingIndicator = false;
  }
}
