import { Component, OnInit, ViewChild, TemplateRef } from '@angular/core';
import { ExpenseBookingRequestViewModel, ExpenseBookingListModel, ExpenseBookingExcel, ExpenseBookingExcelViewModel } from '../../../models/expense-booking/expense-booking-request.model';
import { ExpenseBookingService } from '../../../services/expense-booking/expense-booking.service';
import { AccountService } from '../../../services/account/account.service';
import { AlertService } from '../../../services/common/alert.service';
import { ApproveRejectModel } from '../../../models/expense-booking/approve-reject.model';
import { saveAs } from 'file-saver'
import * as XLSX from 'xlsx';

@Component({
  selector: 'app-approved-booking',
  templateUrl: './approved-booking.component.html',
  styleUrls: ['./approved-booking.component.css']
})
export class ApprovedBookingComponent implements OnInit {

  EXCEL_TYPE = 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet;charset=UTF-8';
  EXCEL_EXTENSION = '.xlsx';
  columns: any[] = [];
  filterQuery: string = "";
  pageNumber = 0;
  count = 0;
  pageSize = 10;

  rows: ExpenseBookingListModel[] = []
  requestStatus: ApproveRejectModel;
  loadingIndicator: boolean = true;

  @ViewChild('indexTemplate')
  indexTemplate: TemplateRef<any>;

  @ViewChild('downloadTemplate')
  downloadTemplate: TemplateRef<any>;

  constructor(private expenseBookingService: ExpenseBookingService, private accountService: AccountService, private alertService: AlertService) { }

  ngOnInit() {
    this.columns = [
      { prop: 'bookingId', name: 'Booking ID' },
      { prop: 'employeeName', name: 'Employee Name' },
      { prop: 'expensePeriod', name: 'Expense Period' },
      { prop: 'amount', name: 'Amount in INR' },
      { prop: 'department', name: 'Department' },
      { prop: 'requestedDate', name: 'Request Date' },
      { prop: 'approvedOrRejectedDate', name: 'Approved Date' },
      { prop: 'action', name: 'Action', cellTemplate: this.downloadTemplate, canAutoResize: false },
    ];
    this.getApproveRequest(this.pageNumber, this.pageSize, this.filterQuery);
  }

  getApproveRequest(page?: number, pageSize?: number, name?: string) {
    this.expenseBookingService.getAllApproveList(this.accountService.currentUser.id, page, pageSize, name).subscribe(result => this.onSuccessfulDataLoad(result), error => this.onDataLoadFailed(error));
  }
  getFilteredData(filterString) {
    this.getApproveRequest(this.pageNumber, this.pageSize, this.filterQuery);

  }
  getData(e) {
    this.getApproveRequest(this.pageNumber, e, this.filterQuery);
  }
  setPage(e) {
    this.getApproveRequest(e.offset, this.pageSize, this.filterQuery);
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


  getApprovedExcelManager() {
    this.expenseBookingService.getApprovedAllExcel().subscribe(result => this.onSuccessfulDataLoadExcel(result), error => this.onDataLoadFailedExcel(error));
  }
  onSuccessfulDataLoadExcel(request: ExpenseBookingExcelViewModel) {

    let newJson = request.expenseBookingExcel.map(rec => {
      return {
        'Booking Id': rec.bookingId,
        'Employee Name': rec.name,
        'Department': rec.department,
        'Amount': rec.amount,
        'Expense Period': rec.expensePeriod,
        'Requested Date': rec.requestDate,
        'Approved Date': rec.approvedDate,
      }
    });
    this.exportAsExcelFile(newJson);
    this.loadingIndicator = false;
  }

  onDataLoadFailedExcel(error: any) {
    this.alertService.showInfoMessage("Unable to retrieve list from the server");
    this.loadingIndicator = false;
  }

  public exportAsExcelFile(json: any): void {
    const excelFileName: string = 'ApproveRequest';
    const worksheet: XLSX.WorkSheet = XLSX.utils.json_to_sheet(json);
    const workbook: XLSX.WorkBook = { Sheets: { 'data': worksheet }, SheetNames: ['data'] };
    const excelBuffer: any = XLSX.write(workbook, { bookType: 'xlsx', type: 'array' });
    this.saveAsExcelFile(excelBuffer, excelFileName);
  }
  private saveAsExcelFile(buffer: any, fileName: string): void {
    const data: Blob = new Blob([buffer], { type: this.EXCEL_TYPE });
    saveAs(data, fileName + '_export_' + new Date().getTime() + this.EXCEL_EXTENSION);
  }

}
