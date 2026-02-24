import { Component, OnInit, ViewChild } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { AlertService } from '../../../../services/common/alert.service';
import { ManageLeaveService } from '../../../../services/leave/manage-leave.service';
import { ManageLeaveDetail } from '../../../../models/leave/manage-leave-detail.model';
import { AccountService } from '../../../../services/account/account.service';
import { Utilities } from '../../../../services/common/utilities';

@Component({
  selector: 'employee-leave-detail',
  templateUrl: './employee-leave-detail.component.html',
  styleUrls: ['./employee-leave-detail.component.css']
})
export class EmployeeLeaveDetailComponent implements OnInit {
  public isSaving = false;
  loadingIndicator: boolean = true;
  manageLeaveDetail: ManageLeaveDetail = new ManageLeaveDetail();
  public manageleavelistCallback: () => void;

  @ViewChild('editorModal')
  editorModal: ModalDirective;

  constructor(private manageLeaveService: ManageLeaveService, private alertService: AlertService, private accountService: AccountService) { }

  ngOnInit() {
  }




  empLeaveDeatils(leaveDetailsId: string) {
    this.manageLeaveService.getmanageleave(leaveDetailsId).subscribe(result => this.onSuccessfulDataLoad(result), error => this.onDataLoadFailed(error));

  }

  viewLeaveDetails(leaveDetailsId: string) {
    this.manageLeaveService.getmanageleave(leaveDetailsId).subscribe(result => this.onSuccessfulDataLoadview(result), error => this.onDataLoadFailed(error));

  }


  onSuccessfulDataLoadview(leaveDetails: ManageLeaveDetail) {
    leaveDetails.view = true;
    this.manageLeaveDetail = leaveDetails;
    this.editorModal.show();
  }


  onSuccessfulDataLoad(leaveDetails: ManageLeaveDetail) {
    this.manageLeaveDetail = leaveDetails;
    this.editorModal.show();
  }

  onDataLoadFailed(error: any) {
    this.alertService.showInfoMessage("Unable to retrieve list from the server");
    this.loadingIndicator = false;
  }


  public approve() {
    this.isSaving = true;
    this.manageLeaveDetail.buttonType = "1";
    this.manageLeaveDetail.approvedby = this.accountService.currentUser.id;
    this.manageLeaveService.updateLeave(this.manageLeaveDetail).subscribe(sucess => this.saveSuccessHelper(sucess), error => this.saveFailedHelper(error));
  }

  public reject() {
    this.isSaving = true;
    this.manageLeaveDetail.buttonType = "2";
    this.manageLeaveDetail.approvedby = this.accountService.currentUser.id;
    this.manageLeaveService.updateLeave(this.manageLeaveDetail).subscribe(sucess => this.saveSuccessHelper(sucess), error => this.saveFailedHelper(error));
  }



  private saveSuccessHelper(result?: string) {
    this.isSaving = false;
    this.alertService.showSucessMessage("Saved successfully");
    this.editorModal.hide();
    this.manageleavelistCallback();
  }

  private saveFailedHelper(error: any) {
    this.isSaving = false;
    let test = Utilities.getHttpResponseMessage(error);
    this.alertService.showInfoMessage(test[0]);
  }
}
