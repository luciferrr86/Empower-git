import { Component, OnInit } from '@angular/core';
import { ExpenseBookingModelDetail } from '../../../models/expense-booking/expense-booking-request.model';
import { ExpenseBookingService } from '../../../services/expense-booking/expense-booking.service';
import { AlertService } from '../../../services/common/alert.service';
import { Router, ActivatedRoute } from '@angular/router';
import { DropDownList } from '../../../models/common/dropdown';
import { AccountService } from '../../../services/account/account.service';
import { Utilities } from '../../../services/common/utilities';
import { ApproveRejectModel } from '../../../models/expense-booking/approve-reject.model';

@Component({
  selector: 'app-approve-reject-detail',
  templateUrl: './approve-reject-detail.component.html',
  styleUrls: ['./approve-reject-detail.component.css']
})
export class ApproveRejectDetailComponent implements OnInit {
  itemList: DropDownList[] = [];
  employeeList: DropDownList[] = [];
  departmentList: DropDownList[] = [];
  public approveReject: ApproveRejectModel = new ApproveRejectModel();
  public isSaving = false;
  public isSubmitting = false;
  public isInviteApproved: true;
  public requestEdit: ExpenseBookingModelDetail = new ExpenseBookingModelDetail();
  loadingIndicator: boolean = true;

  constructor(private router: Router, private route: ActivatedRoute, private expenseBookingService: ExpenseBookingService, private alertService: AlertService) {
    this.route.queryParams.subscribe(params => {
      if (params['requestid']) {
        this.approveReject.id = params['requestid'];
        this.expenseBookingService.viewRequest(params['requestid']).subscribe(result => this.onSuccessfulDataLoadRequest(result), error => this.onDataLoadFailedRequest());
      }
    });
  }
  ngOnInit() {

  }
  onSuccessfulDataLoadRequest(request: ExpenseBookingModelDetail) {
    this.requestEdit = request;
    this.itemList = request.subCategoryItems;
    this.departmentList = request.departmentList;
    this.employeeList = request.inviteEmployeeList;

  }
  onDataLoadFailedRequest() {
    this.alertService.showInfoMessage("Unable to retrieve list from the server");
  }
  onSave(id) {
    this.approveReject.buttonType = id;
    this.expenseBookingService.approveReject(this.approveReject, this.requestEdit.approverId).subscribe(sucess => this.saveSuccessHelper(), error => this.saveFailedHelper(error));
  }
  private saveSuccessHelper() {

    if (this.approveReject.buttonType == "1") {
      this.alertService.showSucessMessage("Request approved Request");
      this.alertService.showSucessMessage("Submitted successfully");
      this.router.navigate(['../expense/approve-request']);
    }
    else if (this.approveReject.buttonType == "2") {
      this.alertService.showSucessMessage("Request rejected successfully");
      this.router.navigate(['../expense/approve-request']);
    } else {
      this.alertService.showSucessMessage("Request resubmitted successfully");
      this.router.navigate(['../expense/approve-request']);
    }
  }
  private saveFailedHelper(error: any) {
    let test = Utilities.getHttpResponseMessage(error);
    this.alertService.showInfoMessage("Please try later" + test[0]);
  }

  invite() {
    this.expenseBookingService.inviteApprover(this.requestEdit, this.requestEdit.approverId).subscribe(sucess => this.saveInviteSuccessHelper(), error => this.saveFailedInviteHelper(error));
  }
  private saveInviteSuccessHelper() {
    this.alertService.showSucessMessage("Invitation send successfully");
    this.router.navigate(['../expense/approve-request']);

  }
  private saveFailedInviteHelper(error: any) {
    let test = Utilities.getHttpResponseMessage(error);
    this.alertService.showInfoMessage("Please try later" + test[0]);
  }
}
