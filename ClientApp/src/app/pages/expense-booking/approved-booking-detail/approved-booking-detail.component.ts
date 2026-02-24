import { Component, OnInit } from '@angular/core';
import { ExpenseBookingModelDetail } from '../../../models/expense-booking/expense-booking-request.model';
import { ExpenseBookingService } from '../../../services/expense-booking/expense-booking.service';
import { AlertService } from '../../../services/common/alert.service';
import { Router, ActivatedRoute } from '@angular/router';
import { DropDownList } from '../../../models/common/dropdown';
import { AccountService } from '../../../services/account/account.service';
import { ApproveRejectModel } from '../../../models/expense-booking/approve-reject.model';

@Component({
  selector: 'app-approved-booking-detail',
  templateUrl: './approved-booking-detail.component.html',
  styleUrls: ['./approved-booking-detail.component.css']
})
export class ApprovedBookingDetailComponent implements OnInit {

  itemList: DropDownList[] = [];
  employeeList: DropDownList[] = [];
  departmentList: DropDownList[] = [];
  public approveReject: ApproveRejectModel = new ApproveRejectModel();
  public isSaving = false;
  public isSubmitting = false;
  public isInviteApproved: true;
  public requestEdit: ExpenseBookingModelDetail = new ExpenseBookingModelDetail();
  loadingIndicator: boolean = true;

  constructor(private route: ActivatedRoute, private expenseBookingService: ExpenseBookingService, private alertService: AlertService) {
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
    this.itemList = request.subCategoryItems;
    this.departmentList = request.departmentList;
    this.employeeList = request.inviteEmployeeList;
    this.requestEdit = request;
  }
  onDataLoadFailedRequest() {
    this.alertService.showInfoMessage("Unable to retrieve list from the server");
  }
}
