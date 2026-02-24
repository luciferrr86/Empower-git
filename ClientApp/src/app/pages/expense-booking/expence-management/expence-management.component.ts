import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ExpenseBookingService } from '../../../services/expense-booking/expense-booking.service';
import { ExpenseBookingViewModel, CategoryModel } from '../../../models/expense-booking/expense-booking.model';
import { AlertService } from '../../../services/common/alert.service';
import { ExpenseBookingExcel } from '../../../models/expense-booking/expense-booking-request.model';
@Component({
  selector: 'app-expence-management',
  templateUrl: './expence-management.component.html',
  styleUrls: ['./expence-management.component.css']
})
export class ExpenceManagementComponent implements OnInit {

  EXCEL_TYPE = 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet;charset=UTF-8';
  EXCEL_EXTENSION = '.xlsx';
  columns: any[] = [];
  filterQuery: string = "";
  pageNumber = 0;
  count = 0;
  pageSize = 10;

  rows: CategoryModel[] = [];
  excelData: ExpenseBookingExcel[];
  categoryList: CategoryModel[];
  loadingIndicator: boolean = true;

  constructor(private router: Router, private expenseBookingService: ExpenseBookingService, private alertService: AlertService) {
  }
  ngOnInit() {
    this.getAllExpenseBooking();
  }
  employeesRequest() {
    this.router.navigate(['../expense/employees-request']);
  }
  approveRequest() {
    this.router.navigate(['../expense/approve-request']);
  }

  getAllExpenseBooking() {
    this.expenseBookingService.getAllExpenseBooking().subscribe(result => this.onSuccessfulDataLoad(result), () => this.onDataLoadFailed());
  }

  onSuccessfulDataLoad(category: ExpenseBookingViewModel) {
    this.categoryList = category.categoryModel;
    this.count = category.categoryCount;
    this.loadingIndicator = false;
  }

  onDataLoadFailed() {
    this.alertService.showInfoMessage("Unable to retrieve list from the server");
    this.loadingIndicator = false;
  }
}
