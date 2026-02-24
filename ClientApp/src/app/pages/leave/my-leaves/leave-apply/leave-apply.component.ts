import { Component, OnInit, ViewChild } from '@angular/core';
import { LeaveApply } from '../../../../models/leave/leave-apply.model';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { IOption } from '../../../../../../node_modules/ng-select';
import { DropDownList } from '../../../../models/common/dropdown';
import { MyLeaveService } from '../../../../services/leave/my-leave.service';
import { AccountService } from '../../../../services/account/account.service';
import { AlertService } from '../../../../services/common/alert.service';
import { Utilities } from '../../../../services/common/utilities';

@Component({
  selector: 'leave-apply',
  templateUrl: './leave-apply.component.html',
  styleUrls: ['./leave-apply.component.css']
})
export class LeaveApplyComponent implements OnInit {
  public isSaving = false;
  leavetypelist: Array<IOption> = [];
  leave: LeaveApply = new LeaveApply();
  filterQuery: string = "";
  pageNumber = 0;
  pageSize = 10;
  public leaveappliedlistCallback: () => void;
  @ViewChild('editorModal')
  editorModal: ModalDirective;
  minDate: Date = new Date();


  constructor(private myleaveService: MyLeaveService, private accountService: AccountService, private alertService: AlertService) { }

  ngOnInit() {
  }

  leaveApply(allLeaveTypeList: DropDownList[]) {
    this.leavetypelist = allLeaveTypeList;
    this.leave = new LeaveApply();
    this.editorModal.show();
    return this.leave;
  }


  public save() {
    this.isSaving = true;
    this.leave.userId = this.accountService.currentUser.id;
    this.myleaveService.createLeave(this.leave).subscribe(sucess => this.saveSuccessHelper(), error => this.saveFailedHelper(error));
  }


  private saveSuccessHelper() {
    this.isSaving = false;
    this.leaveappliedlistCallback();
    this.alertService.showSucessMessage("Leave applied successfully");
    this.editorModal.hide();

  }

  private saveFailedHelper(error: any) {
    this.isSaving = false;
    let test = Utilities.getHttpResponseMessage(error);
    this.alertService.showInfoMessage("Please try later" + test[0]);
  }





}
