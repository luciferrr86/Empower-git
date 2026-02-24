import { Component, OnInit } from '@angular/core';
import { EmployeeSalaryModel } from '../../../models/maintenance/employee-salary.model';
import { EmployeeSalaryService } from '../../../services/maintenance/employee-salary.service';
import { Router } from '@angular/router';
import { MonthDropdown } from '../../../models/common/month.dropdown';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-process-salary',
  templateUrl: './process-salary.component.html',
  styleUrls: ['./process-salary.component.css']
})
export class ProcessSalaryComponent implements OnInit {
  empSal: EmployeeSalaryModel;
  currentMonth: any;
  currentYear: any;
  previousYear: any;
  years: any[];
  message: string;
 // months: any[];
    months: MonthDropdown[] = [
      { id: 1, name: 'January' }, { id: 2, name: 'February' }, { id: 3, name: 'March' }, { id: 4, name: 'April' }, { id: 5, name: 'May' }, { id: 6, name: 'June' },
      { id: 7, name: 'July' }, { id: 8, name: 'August' }, { id: 9, name: 'September' }, { id: 10, name: 'October' },
      { id: 11, name: 'November' }, { id: 12, name: 'December' },
    ];
 // years = ['2020'];
 
  constructor(private employeeSalaryService: EmployeeSalaryService, private router: Router) {
    
    this.empSal = new EmployeeSalaryModel();
    var todayDate = new Date();
    this.currentMonth = todayDate.getMonth() + 1;
    this.currentYear = todayDate.getFullYear();
    this.previousYear = todayDate.getFullYear() -1;
    var range = [];
    range.push(this.previousYear);
    for (var i = 1; i < 2; i++) {
      range.push(this.previousYear + i);
    }
    this.years = range;
    this.empSal.month = this.currentMonth;
    this.empSal.year = this.currentYear;
    // var mon = [];
    //if (this.currentYear == this.empSal.year) {
    //  mon.push(this.currentMonth);
    //  for (var i = 1; i < this.currentMonth; i++) {
    //    mon.push(i);
    //  }
    //  this.months = mon;
    //}
        //this.empSal.month = 2;
        //this.empSal.year = 2020;
    }

  ngOnInit() {
   
  }

    allEmpSal(form: NgForm) {
        var formdata = this.empSal;
      this.employeeSalaryService.processSalary(formdata).subscribe(result => {
          
        this.message = 'Salary processed.';
         // this.router.navigate(['../maintenance/process-salary/check-salary']);
        });
    }
}
