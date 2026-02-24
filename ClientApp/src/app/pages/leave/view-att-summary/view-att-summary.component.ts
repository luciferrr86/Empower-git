import { Component, OnInit } from '@angular/core';
import { MonthDropdown } from '../../../models/common/month.dropdown';
import { AttendenceSummary } from '../../../models/maintenance/attendence-summary.model';
import { EmployeeSalaryModel } from '../../../models/maintenance/employee-salary.model';
import { EmployeeSalaryService } from '../../../services/maintenance/employee-salary.service';

@Component({
  selector: 'app-view-att-summary',
  templateUrl: './view-att-summary.component.html',
  styleUrls: ['./view-att-summary.component.css']
})
export class ViewAttSummaryComponent implements OnInit {
    employees: AttendenceSummary[];
    empSal: EmployeeSalaryModel;
  currentMonth: any;
  currentYear: any;
  previousYear: any;
  years: any[];
  months: MonthDropdown[] = [
    { id: 1, name: 'January' }, { id: 2, name: 'February' }, { id: 3, name: 'March' }, { id: 4, name: 'April' }, { id: 5, name: 'May' }, { id: 6, name: 'June' },
    { id: 7, name: 'July' }, { id: 8, name: 'August' }, { id: 9, name: 'September' }, { id: 10, name: 'October' },
    { id: 11, name: 'November' }, { id: 12, name: 'December' },
  ];
    constructor(private employeeSalaryService: EmployeeSalaryService) {
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
    }

    ngOnInit() {
        this.viewAttSummary();
  }
    viewAttSummary() {
        var formdata = this.empSal;
        this.employeeSalaryService.GetEmpAttSummary(formdata.month, formdata.year).subscribe(result => {
            
            this.employees = result;
            //this.monthlyAttendence.employeeAttendenceVM.forEach(el => {
            //    console.log(el);
            //});
        });       
    }
}
