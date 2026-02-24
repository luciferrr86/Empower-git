import { Component, OnInit } from '@angular/core';
import { CheckSalaryModel } from '../../../models/maintenance/check-salary.model';
import { EmployeeSalaryModel } from '../../../models/maintenance/employee-salary.model';
import { EmployeeSalaryService } from '../../../services/maintenance/employee-salary.service';
import { MonthDropdown } from '../../../models/common/month.dropdown';
import { AccountService } from '../../../services/account/account.service';

@Component({
  selector: 'app-all-emp-sal',
  templateUrl: './all-emp-sal.component.html',
  styleUrls: ['./all-emp-sal.component.css']
})
export class AllEmpSalComponent implements OnInit {
    employees: EmployeeSalaryModel[];
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
    allowedLeaves: number;
    tds: number;
    empid: string;
    constructor(private employeeSalaryService: EmployeeSalaryService, private accountService: AccountService) {
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
    }

    ngOnInit() {
        this.checkSalary();
  }
   
    checkSalary() {
        var formdata = this.empSal;
        this.employeeSalaryService.getEmployeeSalary(formdata.month, formdata.year).subscribe(result => {
            
            this.employees = result;
        });
    }

    enableEdit(id: string) {
        var emp = this.employees.filter(s => s.employeeId == id); //&& s.employeeId == id
        if (emp != null && emp.length > 0) {
            
            emp[0].isEdit = true;
            this.allowedLeaves = emp[0].allowedLeave;
            this.tds = emp[0].tds;
        }
    }

    disableEdit(id: string) {
        var emp = this.employees.filter(s => s.employeeId == id); //&& s.employeeId == id
        if (emp != null && emp.length > 0) {
          
            emp[0].isEdit = false;
        }
    }
    

    updateSalary(id: string) {
        var emp = this.employees.filter(s => s.employeeId == id);
        if (emp != null && emp.length > 0) {
            
            emp[0].allowedLeave = this.allowedLeaves;
            emp[0].tds = this.tds;
            this.empid = id;
            this.employeeSalaryService.UpdateEmpSal(this.empid, this.allowedLeaves, this.tds).subscribe(result => {
                this.checkSalary();
            });
            this.allowedLeaves = 0;
            this.tds = 0;
        }
        this.disableEdit(id);

    }
}
