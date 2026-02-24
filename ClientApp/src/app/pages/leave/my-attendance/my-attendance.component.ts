import { Component, OnInit } from '@angular/core';
import { MyLeaveService } from '../../../services/leave/my-leave.service';
import { AlertService } from '../../../services/common/alert.service';
import { AccountService } from '../../../services/account/account.service';
import { MonthlyAttendence } from '../../../models/leave/monthly-attendence.model';
import { EmployeeSalaryService } from '../../../services/maintenance/employee-salary.service';
import { CheckSalaryModel } from '../../../models/maintenance/check-salary.model';
import { EmployeeSalaryModel } from '../../../models/maintenance/employee-salary.model';
import { NgForm } from '@angular/forms';
import { MonthDropdown } from '../../../models/common/month.dropdown';
import { EmployeeAttendenceVM } from '../../../models/leave/emp-attendence.model';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-my-attendance',
  templateUrl: './my-attendance.component.html',
  styleUrls: ['./my-attendance.component.css']
})
export class MyAttendanceComponent implements OnInit {
    employee: CheckSalaryModel;
    empSal: EmployeeSalaryModel;
    empAttendance: EmployeeAttendenceVM;
    monthlyAttendence: MonthlyAttendence;
    punchin: string;
    punchout: string;
    dateList: any[];
   
  currentMonth: any;
  currentYear: any;
  previousYear: any;
  years: any[];
  months: MonthDropdown[] = [
    { id: 1, name: 'January' }, { id: 2, name: 'February' }, { id: 3, name: 'March' }, { id: 4, name: 'April' }, { id: 5, name: 'May' }, { id: 6, name: 'June' },
    { id: 7, name: 'July' }, { id: 8, name: 'August' }, { id: 9, name: 'September' }, { id: 10, name: 'October' },
    { id: 11, name: 'November' }, { id: 12, name: 'December' },
  ];
    constructor(private route: ActivatedRoute,private employeeSalaryService: EmployeeSalaryService,private myLeaveService: MyLeaveService, private alertService: AlertService, private accountService: AccountService) {
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
       // this.empSal.month = 2;
      //  this.empSal.year = 2020;
        this.monthlyAttendence = new MonthlyAttendence();
        this.monthlyAttendence.employeeAttendenceVM = [];
        this.monthlyAttendence.monthDates = [];
        this.employee = new CheckSalaryModel();
    }

    ngOnInit() {
        
        let userId = this.route.snapshot.params['id'];
        if (userId != null) {
            this.EmpById(userId);
            this.empAttendanceById(userId);
        }
        this.EmpList();
        this.checkAttendance();
    }
    EmpById(userId) {
        this.employeeSalaryService.getEmp(userId).subscribe(result => {
            this.employee = result;
        });
    }
    empAttendanceById(userId) {
        var formdata = this.empSal;
        this.employeeSalaryService.GetEmpByMonth(userId, formdata.month, formdata.year).subscribe(result => {
            this.monthlyAttendence = result;

        });
    }
    EmpList() {
        this.employeeSalaryService.getEmp(this.accountService.currentUser.id).subscribe(result => {
            this.employee = result;
        });
    }
    checkAttendance() {
        var formdata = this.empSal;
        var userId = this.accountService.currentUser.id;
        this.employeeSalaryService.GetEmpByMonth(userId, formdata.month, formdata.year).subscribe(result => {
            this.monthlyAttendence = result;
            
        });
    }
    GetPunchIn(id: string, dt: Date) {
        var att = '';
        var attendence = this.monthlyAttendence.employeeAttendenceVM.filter(s => s.dateView == dt.toString()); //&& s.employeeId == id
        if (attendence != null && attendence.length > 0) {
           // 
            var att = (attendence[0].punchIn);
        }
        return att;
    }
    GetPunchOut(id: string, dt: Date) {
        var att = '';
        var attendence = this.monthlyAttendence.employeeAttendenceVM.filter(s => s.dateView == dt.toString() && s.employeeId == id);
        if (attendence != null && attendence.length > 0) {
           // 
            var att = (attendence[0].punchOut);
        }
        return att;
    }
    GetDuration(id: string, dt: Date) {
        var att = '';
        var attendence = this.monthlyAttendence.employeeAttendenceVM.filter(s => s.dateView == dt.toString() && s.employeeId == id);
        if (attendence != null && attendence.length > 0) {
            //
            var att = (attendence[0].duration);
        }
        return att;
    }
    enableEdit(dt: Date) {
        
      //  this.enableEditIndex = i;
        var attendence = this.monthlyAttendence.employeeAttendenceVM.filter(s => s.dateView == dt.toString()); //&& s.employeeId == id
        if (attendence != null && attendence.length > 0) {
            //
            attendence[0].isEdit = true;
            this.punchin = attendence[0].punchIn;
            this.punchout = attendence[0].punchOut;
        }
       
    }
    disableEdit(dt: Date) {
        
      //  this.enableEditIndex = i;
        var attendence = this.monthlyAttendence.employeeAttendenceVM.filter(s => s.dateView == dt.toString()); //&& s.employeeId == id
        if (attendence != null && attendence.length > 0) {
            //
            attendence[0].isEdit = false;
            this.punchin = '';
            this.punchout = '';
        }
       
    }
    updateAttendence(dt: Date) {
        
      //  this.enableEditIndex = i;
        var attendence = this.monthlyAttendence.employeeAttendenceVM.filter(s => s.dateView == dt.toString()); //&& s.employeeId == id
        if (attendence != null && attendence.length > 0) {
            //
            attendence[0].punchIn = this.punchin;
            attendence[0].punchOut = this.punchout;
            this.punchin = '';
            this.punchout = '';
        }
        this.disableEdit(dt);
       
    }
    GetEditStatus(dt: Date) {
       // 
        var att = false;
        var attendence = this.monthlyAttendence.employeeAttendenceVM.filter(s => s.dateView == dt.toString()); //&& s.employeeId == id
        if (attendence != null && attendence.length > 0) {
            //
            var att = attendence[0].isEdit;
        }
        return att;
    }
    //editAttendance() {
    //    var editAtt = new EmployeeAttendenceVM();
    //    editAtt.punchIn = null;
    //    editAtt.punchOut = null;
    //    this.empAttendance.push(editAtt);
    //}
   
}
