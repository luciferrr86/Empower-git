import { Component, OnInit } from '@angular/core';
import { CheckSalaryModel } from '../../../models/maintenance/check-salary.model';
import { EmployeeAttendence } from '../../../models/maintenance/employee-attendance.model';
import { EmployeeSalaryService } from '../../../services/maintenance/employee-salary.service';
import { Router } from '@angular/router';
import { EmployeeSalaryModel } from '../../../models/maintenance/employee-salary.model';
import { MonthDropdown } from '../../../models/common/month.dropdown';

@Component({
  selector: 'app-manage-attendance',
  templateUrl: './manage-attendance.component.html',
  styleUrls: ['./manage-attendance.component.css']
})
export class ManageAttendanceComponent implements OnInit {
    employees: CheckSalaryModel[];
    employeeAttendence: EmployeeAttendence;
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
    constructor(private employeeSalaryService: EmployeeSalaryService, private router: Router) {
        this.empSal = new EmployeeSalaryModel();
      var todayDate = new Date();
      this.currentMonth = todayDate.getMonth() + 1;
      this.currentYear = todayDate.getFullYear();
      this.previousYear = todayDate.getFullYear() - 1;
      var range = [];
      range.push(this.previousYear);
      for (var i = 1; i < 7; i++) {
        range.push(this.previousYear + i);
      }
      this.years = range;
      this.empSal.month = this.currentMonth;
      this.empSal.year = this.currentYear;
    }

    ngOnInit() {
        this.EmpList();
    }

    EmpList() {
        this.employeeSalaryService.getEmployeeListForLeaveEntry().subscribe(result => {
            this.employees = result;
        });
    }
    viewAttendance(userId) {
        
      this.router.navigate(['leave/attendance/employee-attendence/', userId]);
    }
    allEmpAttSummary() {
        
        this.employeeSalaryService.allEmpAttSummary(this.empSal.month, this.empSal.year).subscribe(result => {

        });

    }

}
