import { Component, OnInit } from '@angular/core';
import { CheckSalaryModel } from '../../../models/maintenance/check-salary.model';
import { EmployeeSalaryService } from '../../../services/maintenance/employee-salary.service';
import { Router } from '@angular/router';
import { MonthDropdown } from '../../../models/common/month.dropdown';
import { EmployeeSalaryModel } from '../../../models/maintenance/employee-salary.model';
import { NgForm } from '@angular/forms';
import { MonthlyAttendence } from '../../../models/leave/monthly-attendence.model';

@Component({
  selector: 'app-leave-entry',
  templateUrl: './leave-entry.component.html',
  styleUrls: ['./leave-entry.component.css']
})
export class LeaveEntryComponent implements OnInit {
    employees: CheckSalaryModel[];
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
  //  monthlyAttendence: any;
    monthlyAttendence: MonthlyAttendence;
    dateList: any[];
    constructor(private employeeSalaryService: EmployeeSalaryService, private router: Router) {
        this.empSal = new EmployeeSalaryModel();
        this.monthlyAttendence = new MonthlyAttendence();
        this.monthlyAttendence.employeeAttendenceVM = [];
        this.monthlyAttendence.monthDates = [];
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
       // this.monthlyAttendence.MonthDates =[];
      this.EmpList();
      this.employeeSalaryService.GetDate(this.empSal.month, this.empSal.year).subscribe(result => {
            
            this.monthlyAttendence = result;
            // 
        });       
    }
   
    EmpList() {
        this.employeeSalaryService.getEmployeeListForLeaveEntry().subscribe(result => {
            this.employees = result;
        });
    }
    checkAttendance(form: NgForm) {
        var formdata = this.empSal;
        this.employeeSalaryService.GetDate(formdata.month, formdata.year).subscribe(result => {
            
            this.monthlyAttendence = result;
            //this.monthlyAttendence.employeeAttendenceVM.forEach(el => {
            //    console.log(el);
            //});
        });       
    }

    GetDateAttendence(id: string, dt: Date) {
   //     var attendence = this.monthlyAttendence.employeeAttendenceVM.where(date.key);
       // 
        var att = '';
      
        //if (dt.toString() == "2/15/2020") {
        //    
        //  //  var empid = id;
        //}

        var attendence = this.monthlyAttendence.employeeAttendenceVM.filter(s => s.dateView == dt.toString() && s.employeeId == id);
        if (attendence != null && attendence.length > 0) {
            
            var att = (attendence[0].punchIn ? attendence[0].punchIn : '') + ' : ' + (attendence[0].punchOut ? attendence[0].punchOut : '');
           // var att = attendence[0].punchIn + ' : ' + attendence[0].punchOut;
        }
   //   var attendence = this.monthlyAttendence.employeeAttendenceVM.filter(s => s.Date == dt && s.EmployeeId == id);
       
        return att;
    }

}
