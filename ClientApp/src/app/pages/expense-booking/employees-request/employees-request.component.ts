import { Component, OnInit, ViewChild, TemplateRef } from '@angular/core';
import { ExpenseBookingService } from '../../../services/expense-booking/expense-booking.service';
import { AlertService } from '../../../services/common/alert.service';
import { AccountService } from '../../../services/account/account.service';
import { ExpenseBookingListModel, ExpenseBookingRequestViewModel } from '../../../models/expense-booking/expense-booking-request.model';
import { ExpenseBookingRequestComponent } from '../expense-booking-request/expense-booking-request.component';

@Component({
  selector: 'app-employees-request',
  templateUrl: './employees-request.component.html',
  styleUrls: ['./employees-request.component.css']
})
export class EmployeesRequestComponent implements OnInit {

  columns: any[] = [];
  filterQuery: string = "";
  pageNumber = 0;
  count = 0;
  pageSize = 10;

  rows: ExpenseBookingListModel[] = []
  bookingRequest: ExpenseBookingListModel;
  loadingIndicator: boolean = true;

  @ViewChild('indexTemplate')
  indexTemplate: TemplateRef<any>;
  @ViewChild('actionsTemplate')
  actionsTemplate: TemplateRef<any>;
  @ViewChild('downloadTemplate')
  downloadTemplate: TemplateRef<any>;
  @ViewChild('viewRequests')
  viewRequests: ExpenseBookingRequestComponent;

  constructor(private expenseBookingService: ExpenseBookingService, private accountService: AccountService, private alertService: AlertService) { }

  ngOnInit() {
    this.columns = [
      { prop: 'bookingId', name: 'Booking ID' },
      { prop: 'expensePeriod', name: 'Expense Period' },
      { prop: 'amount', name: 'Amount in INR' },
      { prop: 'department', name: 'Department' },
      { prop: 'requestedDate', name: 'Request Date' },
      { prop: 'status', name: 'Status' },
      { prop: 'approvedOrRejectedDate', name: 'Approved/Reject Date' },
      { prop: 'Action', name: 'Action', cellTemplate: this.actionsTemplate, resizeable: false, canAutoResize: false, sortable: false, draggable: false }
    ];
    this.getEmployeeRequest(this.pageNumber, this.pageSize, this.filterQuery);
  }

  getEmployeeRequest(page?: number, pageSize?: number, name?: string) {
    this.expenseBookingService.getEmployeeRequest(this.accountService.currentUser.id, page, pageSize, name).subscribe(result => this.onSuccessfulDataLoad(result), error => this.onDataLoadFailed(error));
  }

  getFilteredData(filterString) {
    this.getEmployeeRequest(this.pageNumber, this.pageSize, this.filterQuery);

  }
  getData(e) {
    this.getEmployeeRequest(this.pageNumber, e, this.filterQuery);
  }
  setPage(e) {
    this.getEmployeeRequest(e.offset, this.pageSize, this.filterQuery);
  }
  onSuccessfulDataLoad(request: ExpenseBookingRequestViewModel) {
    this.rows = request.expenseBookingListModel;
    request.expenseBookingListModel.forEach((requests, index, request) => {
      (<any>requests).index = index + 1;
    });
    this.count = request.expenseBookingCount;
    this.loadingIndicator = false;
  }

  onDataLoadFailed(error: any) {
    this.alertService.showInfoMessage("Unable to retrieve list from the server");
    this.loadingIndicator = false;
  }

  // viewRequest(request: ExpenseBookingListModel) {
  //   alert("view");
  //   this.viewRequests.viewRequestDetails(request.id);
  // }
}
