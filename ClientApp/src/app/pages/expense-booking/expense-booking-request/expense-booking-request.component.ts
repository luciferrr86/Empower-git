import { Component, OnInit } from '@angular/core';
import { ExpenseBookingModel } from '../../../models/expense-booking/expense-booking-request.model';
import { ExpenseBookingService } from '../../../services/expense-booking/expense-booking.service';
import { AlertService } from '../../../services/common/alert.service';
import { Router, ActivatedRoute } from '@angular/router';
import { DropDownList } from '../../../models/common/dropdown';
import { AccountService } from '../../../services/account/account.service';
import { Utilities } from '../../../services/common/utilities';

@Component({
  selector: 'app-expense-booking-request',
  templateUrl: './expense-booking-request.component.html',
  styleUrls: ['./expense-booking-request.component.css']
})
export class ExpenseBookingRequestComponent implements OnInit {

  itemList: DropDownList[] = [];
  departmentList: DropDownList[] = [];
  public isSaving = false;
  public expanseTitle: string = "Add Expense Booking";
  public isSubmitting = false;
  public loading = true;
  public fileId: string[] = new Array<string>();
  public requestEdit: ExpenseBookingModel = new ExpenseBookingModel();
  loadingIndicator: boolean = true;

  constructor(private router: Router, private accountService: AccountService, private route: ActivatedRoute, private expemseBookingService: ExpenseBookingService, private alertService: AlertService) {
    this.route.queryParams.subscribe(params => {
      if (params['id']) {
        this.expemseBookingService.getAddRequest(params['id']).subscribe(result => this.onSuccessfulDataLoadRequest(result), error => this.onDataLoadFailedRequest(error));
      }
      else {
        this.expanseTitle = "View Expense Booking";
        this.expemseBookingService.viewRequest(params['requestid']).subscribe(result => this.onSuccessfulDataLoadRequest(result), error => this.onDataLoadFailedRequest(error));
      }
    });
  }

  ngOnInit() {
  }

  onSuccessfulDataLoadRequest(request: ExpenseBookingModel) {

    this.requestEdit = request;
    if (this.requestEdit.toDate.toString() == "0001-01-01T00:00:00") {
      this.requestEdit.toDate = null;
    }
    if (this.requestEdit.fromDate.toString() == "0001-01-01T00:00:00") {
      this.requestEdit.fromDate = null;
    }
    this.itemList = request.subCategoryItems;
    this.departmentList = request.departmentList;
  }
  onDataLoadFailedRequest(error: any) {
    this.alertService.showInfoMessage("Unable to retrieve list from the server");
  }

  getDocumentId($event) {
    $event.forEach(item => {
      this.fileId.push(item.pictureId);
    });
  }

  Save() {

    let todate = new Date(this.requestEdit.toDate);
    let fromdate = new Date(this.requestEdit.fromDate);
    if (fromdate > todate) {
      this.alertService.showInfoMessage("From date should not be greater than to date.");
    } else {
      this.alertService.startLoadingMessage("", "Please wait...");
      this.isSaving = true;
      this.requestEdit.file = this.fileId;
      if (this.requestEdit.id == null) {
        this.expemseBookingService.addRequest(this.requestEdit, this.accountService.currentUser.id).subscribe(sucess => this.saveSuccessHelper(sucess), error => this.saveFailedHelper(error));
      }
      else {
        this.expemseBookingService.updateRequest(this.requestEdit, this.requestEdit.id).subscribe(sucess => this.saveSuccessHelper(sucess), error => this.saveFailedHelper(error));
      }
    }

  }
  Submit() {
    if (this.requestEdit.id != null) {
      this.isSubmitting = true;
      this.alertService.startLoadingMessage("", "Please wait...");
      this.expemseBookingService.submitRequest(this.requestEdit.id).subscribe(sucess => this.submitSuccessHelper(sucess), error => this.submitFailedHelper(error));
    } else {
      this.alertService.showInfoMessage("Please save the data first");
    }

  }
  submitSuccessHelper(result?: string) {
    this.alertService.stopLoadingMessage();
    this.isSubmitting = false;
    this.alertService.showSucessMessage("Submitted successfully");
    this.router.navigate(['../expense/employees-request']);
  }
  private submitFailedHelper(error: any) {
    this.alertService.stopLoadingMessage();
    this.isSaving = false;
    this.alertService.stopLoadingMessage();
    let test = Utilities.getHttpResponseMessage(error);
    this.alertService.showInfoMessage("Please try later" + test[0]);
  }
  private saveSuccessHelper(result?: string) {
    this.alertService.stopLoadingMessage();
    this.isSaving = false;
    if (this.requestEdit.id == null) {
      this.alertService.showSucessMessage("Saved successfully");
      this.router.navigate(['../expense/employees-request']);
    }
    else {
      this.alertService.showSucessMessage("Updated successfully");
      this.router.navigate(['../expense/employees-request']);
    }
  }
  private saveFailedHelper(error: any) {
    this.alertService.stopLoadingMessage();
    this.isSaving = false;
    let test = Utilities.getHttpResponseMessage(error);
    this.alertService.showInfoMessage(test[0]);
  }
  deleteDocument(id) {
    this.alertService.showConfirm("Are you sure you want to delete?", () => this.deleteDocumentHelper(id));
  }
  deleteDocumentHelper(id) {
    this.expemseBookingService.deleteDocument(id)
      .subscribe(results => {
        this.alertService.showSucessMessage('Deleted successfully.');
        this.router.navigate(['../expense/employees-request']);
      },
        error => {
          this.alertService.showInfoMessage('An error occured whilst deleting.');
        });
  }
}
