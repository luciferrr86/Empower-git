import { Component, OnInit } from '@angular/core';
import { EmployeeSalaryService } from '../../../services/maintenance/employee-salary.service';
import { EmployeeSalaryModel } from '../../../models/maintenance/employee-salary.model';
import { MonthDropdown } from '../../../models/common/month.dropdown';
import { Router } from '@angular/router';
import { NgForm } from '@angular/forms';
import { CheckSalaryModel } from '../../../models/maintenance/check-salary.model';

@Component({
  selector: 'app-check-salary',
  templateUrl: './check-salary.component.html',
  styleUrls: ['./check-salary.component.css']
})
export class CheckSalaryComponent implements OnInit {
  empSal: EmployeeSalaryModel;
  salaryEmployees: EmployeeSalaryModel[];
  chkSalary: CheckSalaryModel[];
  currentMonth: any;
  currentYear: any;
  previousYear: any;
  filterEmployeeName: any;
  noOfPages = 1;
  currentPage = 1;
  sortedBy = "default";
  pageContent: number[] = [10, 20, 50, 100];
  filter: any = {};
  itemsPerPage: number;
  pageSize: number = 10;

  years: any[];
  months: MonthDropdown[] = [
    { id: 1, name: 'January' }, { id: 2, name: 'February' }, { id: 3, name: 'March' }, { id: 4, name: 'April' }, { id: 5, name: 'May' }, { id: 6, name: 'June' },
    { id: 7, name: 'July' }, { id: 8, name: 'August' }, { id: 9, name: 'September' }, { id: 10, name: 'October' },
    { id: 11, name: 'November' }, { id: 12, name: 'December' },
  ];
  constructor(private employeeSalaryService: EmployeeSalaryService, private router: Router) {
    this.empSal = new EmployeeSalaryModel();
    var todayDate = new Date();
    this.currentMonth = todayDate.getMonth() + 1;
    this.currentYear = todayDate.getFullYear();
    this.previousYear = todayDate.getFullYear() - 1;
    var range = [];
    range.push(this.previousYear);
    for (var i = 1; i < 2; i++) {
      range.push(this.previousYear + i);
    }
    this.years = range;
    this.empSal.month = this.currentMonth;
    this.empSal.year = this.currentYear;
    //  this.empSal.month = 2;
    //  this.empSal.year = 2020;
    this.getFilterResult()
  }

  ngOnInit() {
    this.employeeSalaryService.checkSalary(this.empSal.month, this.empSal.year).subscribe(result => {
      this.chkSalary = result;
    });
  }

  checkEmpSal(form: NgForm) {
    var formdata = this.empSal;
    //  this.employeeSalaryService.checkSalary(this.empSal.month, this.empSal.year).subscribe(result => {
    this.employeeSalaryService.checkEmpSalary(this.empSal.month, this.empSal.year, this.filterEmployeeName, this.currentPage, this.pageSize, this.sortedBy).subscribe(data => {
      this.chkSalary = data;
      this.chkSalary.forEach((element) => {
        this.noOfPages = element.noOfPages;
      });
    });
  }
  viewSalary(empId) {
    this.router.navigate(['../maintenance/view-salary/', empId]);

  }
  salaryInfo(userId) {
    this.router.navigate(['../maintenance/employee-salary/', userId]);

  }
  getFilterResult() {
    if (this.filterEmployeeName == undefined) this.filterEmployeeName = '';
    this.employeeSalaryService.checkEmpSalary(this.empSal.month, this.empSal.year, this.filterEmployeeName, this.currentPage, this.pageSize, this.sortedBy).subscribe(data => {
      this.chkSalary = data;
      this.chkSalary.forEach((element) => {
        this.noOfPages = element.noOfPages;
      });
    });
  }
  resetFilter() {

    this.filterEmployeeName = '';
    this.getFilterResult();
  }
  changePage(pageNo: any) {

    this.currentPage = pageNo;
    this.getFilterResult();

  }
  handlePageChange(event) {
    this.pageSize = Number(event.target.value);
    this.currentPage = 1;
    this.getFilterResult();
  }
}
