import { Component, OnInit } from '@angular/core';
import { CheckSalaryModel } from '../../../models/maintenance/check-salary.model';
import { EmployeeSalaryService } from '../../../services/maintenance/employee-salary.service';
import { Router, ActivatedRoute } from '@angular/router';
import { EmployeeSalaryModel } from '../../../models/maintenance/employee-salary.model';
import { EmployeeAttendenceVM } from '../../../models/leave/emp-attendence.model';
import { MonthlyAttendence, MonthDate } from '../../../models/leave/monthly-attendence.model';
import { MonthDropdown } from '../../../models/common/month.dropdown';
import { AccountService } from '../../../services/account/account.service';

@Component({
  selector: 'app-employee-attendence',
  templateUrl: './employee-attendence.component.html',
  styleUrls: ['./employee-attendence.component.css']
})
export class EmployeeAttendenceComponent implements OnInit {
    employee: CheckSalaryModel;
    empSal: EmployeeSalaryModel;
    empAttendance: EmployeeAttendenceVM;
    monthlyAttendence: MonthlyAttendence;
    punchin: string;
    punchout: string;
    leaveType: number;
    empid: string;
    date: Date;
  dateList: any[];
  dataAvailable: boolean = false;
    //allowedLeaves: number;

  currentMonth: any;
  currentYear: any;
  previousYear: any;
  years: any[];
  months: MonthDropdown[] = [
    { id: 1, name: 'January' }, { id: 2, name: 'February' }, { id: 3, name: 'March' }, { id: 4, name: 'April' }, { id: 5, name: 'May' }, { id: 6, name: 'June' },
    { id: 7, name: 'July' }, { id: 8, name: 'August' }, { id: 9, name: 'September' }, { id: 10, name: 'October' },
    { id: 11, name: 'November' }, { id: 12, name: 'December' },
  ];
    constructor(private route: ActivatedRoute, private employeeSalaryService: EmployeeSalaryService, private accountService: AccountService) {
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
        this.monthlyAttendence = new MonthlyAttendence();
        this.monthlyAttendence.employeeAttendenceVM = [];
        this.monthlyAttendence.monthDates = [];
        this.employee = new CheckSalaryModel();
        this.leaveType = 0;
    }

    ngOnInit() {
        let userId = this.route.snapshot.params['id'];
        if (userId != null) {
            this.EmpById(userId);
            this.empAttendanceById(userId);
        }
        else {
            this.EmpList();
            this.checkAttendance();
        }
        
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
        //var userId = this.accountService.currentUser.id;
       // var em = this.employee;
    this.employeeSalaryService.GetEmpByMonth(this.employee.userId, formdata.month, formdata.year).subscribe(result => {
      
      this.monthlyAttendence = result;
      console.log(this.monthlyAttendence.employeeAttendenceVM);
      if (this.monthlyAttendence.employeeAttendenceVM.length > 0) {
        
        this.dataAvailable = true;
      }
      else {
        this.dataAvailable = false;
      }
        });
    }

    updateAttSummary() {
        this.employeeSalaryService.UpdateAttSummary(this.employee.employeeId, this.empSal.month, this.empSal.year).subscribe(result => {
          });
    }

  GetPunchIn(id: string, dt: Date) {
        var att = '';
        var attendence = this.monthlyAttendence.employeeAttendenceVM.filter(s => s.dateView == dt.toString()); //&& s.employeeId == id
        if (attendence != null && attendence.length > 0) {
            var att = (attendence[0].punchIn);
        }
        return att;
    }

    GetPunchOut(id: string, dt: Date) {
        var att = '';
        var attendence = this.monthlyAttendence.employeeAttendenceVM.filter(s => s.dateView == dt.toString() && s.employeeId == id);
        if (attendence != null && attendence.length > 0) {
            var att = (attendence[0].punchOut);
        }
        return att;
    }

    GetDuration(id: string, dt: Date) {
        var att = '';
        var attendence = this.monthlyAttendence.employeeAttendenceVM.filter(s => s.dateView == dt.toString() && s.employeeId == id);
        if (attendence != null && attendence.length > 0) {
            var att = (attendence[0].duration);
        }
        return att;
    }

    GetLeaveStatus(id: string, dt: MonthDate) {
        var status = 0;
        var attendence = this.monthlyAttendence.employeeAttendenceVM.filter(s => s.dateView == dt.mDate.toString() && s.employeeId == id);
       // 
        if (attendence != null && attendence.length > 0) {
            if (attendence[0].leaveType != null && attendence[0].leaveType != 0) {
                status = attendence[0].leaveType;
            }
            else {
            status = (attendence[0].leaveStatus);
            if (status == 0) {
                status = dt.mDayStatus;
                }
            }
        }
        else {
            status = dt.mDayStatus;
        }
        return status;
    }

    enableEdit(dt: Date) {
        var attendence = this.monthlyAttendence.employeeAttendenceVM.filter(s => s.dateView == dt.toString()); //&& s.employeeId == id
        if (attendence != null && attendence.length > 0) {
            attendence[0].isEdit = true;
            this.punchin = attendence[0].punchIn;
            this.punchout = attendence[0].punchOut;
            this.leaveType = attendence[0].leaveStatus;
        }

    }

    disableEdit(dt: Date) {
        var attendence = this.monthlyAttendence.employeeAttendenceVM.filter(s => s.dateView == dt.toString()); //&& s.employeeId == id
        if (attendence != null && attendence.length > 0) {
            attendence[0].isEdit = false;
            this.punchin = '';
            this.punchout = '';
            this.leaveType = 0;
        }

    }

    updateAttendence(id: string,dt: Date) {
        var attendence = this.monthlyAttendence.employeeAttendenceVM.filter(s => s.dateView == dt.toString()); //&& s.employeeId == id
        if (attendence != null && attendence.length > 0) {
            attendence[0].punchIn = this.punchin;
            attendence[0].punchOut = this.punchout;
            this.date = dt;
            this.empid = id;
            this.employeeSalaryService.UpdateEmpAttendance(this.empid, this.punchin, this.punchout, this.date, this.leaveType).subscribe(result => {
                this.checkAttendance();
            });
            this.punchin = '';
            this.punchout = '';
        }
        this.disableEdit(dt);

    }

    GetEditStatus(dt: Date) {
        var att = false;
        var attendence = this.monthlyAttendence.employeeAttendenceVM.filter(s => s.dateView == dt.toString()); //&& s.employeeId == id
        if (attendence != null && attendence.length > 0) {
            var att = attendence[0].isEdit;
        }
        return att;
    }

    LeaveNameByStatus(id: string, dt: MonthDate) {
        var status = this.GetLeaveStatus(id, dt);
      //  
        var leaveName = '';
        switch (status) {
            case 0: leaveName = 'Not Applicable';
                return leaveName;
            case 1: leaveName = 'Approved Leave';
                return leaveName;
            case 2: leaveName = 'Comp off';
                return leaveName;
            case 3: leaveName = 'Full Day';
                return leaveName;
            case 4: leaveName = 'Half Day';
                return leaveName;
            case 5: leaveName = 'Holiday';
                return leaveName;
            case 6: leaveName = 'Short Leave';
                return leaveName;
            case 7: leaveName = 'Unpaid Leave';
                return leaveName;
            case 8: leaveName = 'Weekly Off';
                return leaveName;
        }
    }
   
}
