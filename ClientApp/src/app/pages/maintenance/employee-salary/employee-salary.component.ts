import { Component, OnInit, ViewChild, TemplateRef, DoCheck } from '@angular/core';
import { EmployeeSalaryService } from '../../../services/maintenance/employee-salary.service';
import { EmployeeSalaryModel } from '../../../models/maintenance/employee-salary.model';
import { ActivatedRoute, Router } from '@angular/router';
import { NgForm } from '@angular/forms';
import { MonthDropdown } from '../../../models/common/month.dropdown';
import { CtcOtherComponent } from '../../../models/maintenance/ctc-other-component.model';
import { SalaryComponentModel } from '../../../models/maintenance/salary-component.model';
import { SalaryPart } from '../../../models/maintenance/salary-part.model';
import { EmployeeCtcModel } from '../../../models/maintenance/employee-ctc.model';

@Component({
  selector: 'app-employee-salary',
  templateUrl: './employee-salary.component.html',
  styleUrls: ['./employee-salary.component.css']
})

export class EmployeeSalaryComponent implements OnInit, DoCheck {
    employeeSalary: EmployeeSalaryModel;
    isSubmit = false;
    //basic: number;
    //da: number;
    //hra: number;
    empId: string;
    salaryParts: SalaryPart[];
    employeeCtc: EmployeeCtcModel;
    components: SalaryComponentModel[];
    earnings: SalaryPart[];
    deductions: SalaryPart[];
    totalEarnings: number;
    totalDeductions: number;
    employeeCode: number;
    employeeId: number;
    employeeName: string;
  currentMonth: any;
  currentYear: any;
  previousYear: any;
  years: any[];
    months: MonthDropdown[] = [{ id: 1, name: 'January' },
        { id: 2, name: 'February' }, { id: 3, name: 'March' },
        { id: 4, name: 'April' }, { id: 5, name: 'May' },
        { id: 6, name: 'June' }, { id: 7, name: 'July' },
        { id: 8, name: 'August' }, { id: 9, name: 'September' },
        { id: 10, name: 'October' }, { id: 11, name: 'November' },
        { id: 12, name: 'December' },
    ];
  //  years= ['2017','2018', '2019', '2020','2021','2022'];

    constructor(private employeeSalaryService: EmployeeSalaryService, private route: ActivatedRoute, private router: Router) {
        this.employeeSalary = new EmployeeSalaryModel();
        this.employeeSalary.salaryPart = [];
        this.earnings = [];
        this.deductions = [];
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
   //   this.empSal.month = this.currentMonth;
    //  this.empSal.year = this.currentYear;
    }

    ngOnInit() {
        this.empId = this.route.snapshot.params['id'];
        this.getSalaryByEmployeeId(this.empId, 0, 0);
    }


    ChangeSalaryMonthYear() {
        this.getSalaryByEmployeeId(this.empId, this.employeeSalary.month, this.employeeSalary.year);
       var m = this.employeeSalary.month;
       var y = this.employeeSalary.year;
       this.getDaysInMonth(m,y);
       
  }
  getDaysInMonth(month, year) {
    
    var lastDay = new Date(year, month, 0).getDate();
    this.employeeSalary.totalDaysOfMonth = lastDay;
  }

  //special-> Arrears, otherdeduction->netpayable
    getUpdateSalDetail(employeeSalary) {
        if (this.employeeSalary) {
            var totalDaysOfMonth = this.employeeSalary.totalDaysOfMonth;
            this.employeeSalary.workedDays = (this.employeeSalary.leaveTaken >= this.employeeSalary.allowedLeave) ? (totalDaysOfMonth - this.employeeSalary.leaveTaken + this.employeeSalary.allowedLeave) : totalDaysOfMonth;
           
          // Calculate deduction for unpaid days
          this.totalDeductions = this.employeeSalary.contributionToPf + this.employeeSalary.tds + this.employeeSalary.unpaidDays + this.employeeSalary.netPayable + this.employeeSalary.professionTax + this.employeeSalary.salaryAdvance + this.employeeSalary.medicalBillAmount;  // + unpaidDaysAmount
            if (this.deductions != null) {
                for (var i = 0; i < this.deductions.length; i++) {
                    this.totalDeductions = this.totalDeductions + this.deductions[i].amount;
                }
            }
          // add remaining
          this.totalEarnings = this.employeeSalary.basicSalary + this.employeeSalary.ta + this.employeeSalary.da + this.employeeSalary.hra + this.employeeSalary.medicalExpenses + this.employeeSalary.conveyance + this.employeeSalary.special + this.employeeSalary.bonus;
         
          if (this.earnings != null) {
                for (var i = 0; i < this.earnings.length; i++) {
                    this.totalEarnings = this.totalEarnings + this.earnings[i].amount;
                }
          }
            var total = this.totalEarnings - this.totalDeductions;
            this.employeeSalary.total = total;
          
        }
    }

    ngDoCheck() {
        this.getUpdateSalDetail(this.employeeSalary);
       // this.ChangeSalaryMonthYear();
    }
    getSalaryByEmployeeId(empId, month, year) {       
        this.employeeSalaryService.getSalaryByEmployeeId(empId, month, year)
            .subscribe(result => {
                this.earnings = [];
                this.deductions = [];
                this.employeeSalary = result.employeeSalary;
                this.components = result.salaryComponents;
                this.employeeCtc = result.employeeCtc;
                if (this.employeeSalary !== null) {
                    this.employeeSalary.employeeCtcId = result.employeeCtc.id;
                    this.employeeSalary.employeeName = result.employeeName;
                    this.employeeSalary.employeeCode = result.employeeCode;
                    this.employeeSalary.employeeId = result.employeeId
                    this.getUpdateSalDetail(this.employeeSalary);
                    
                    // this.earnings = this.employeeSalary.salaryPart.filter(q => q.salaryComponent.isMonthly && q.salaryComponent.isEarnings);
                    //  this.deductions = this.employeeSalary.salaryPart.filter(q => q.salaryComponent.isMonthly && !q.salaryComponent.isEarnings);


                    for (var i = 0; i < this.employeeSalary.salaryPart.length; i++) {
                        //  
                        // let compId = this.employeeSalary.salaryPart[i].ctcOtherComponent.salaryComponentId;
                        let compId = this.employeeSalary.salaryPart[i].ctcOtherComponentId;
                        let otherComponent = this.employeeCtc.ctcOtherComponent.filter(q => q.id == compId)[0];
                        if (otherComponent != null) {
                            let component = this.components.filter(q => q.id == otherComponent.salaryComponentId)[0];
                            if (component != null && component.isMonthly) {
                                if (component.isEarnings) {
                                    this.earnings.push(this.employeeSalary.salaryPart[i]);
                                } else {
                                    this.deductions.push(this.employeeSalary.salaryPart[i]);
                                }
                            }
                        }
                    }
                }
            });
    }

  submitSalary() {
        var formdata = this.employeeSalary;
        this.isSubmit = true;
        this.employeeSalaryService.addSalary(formdata)
                .subscribe((data) => {
                    this.router.navigate(['../maintenance/process-salary/check-salary']);
                });
    }

    
    getComponentName(otherComponentId: number) {
        let otherComponent = this.employeeCtc.ctcOtherComponent.filter(q => q.id == otherComponentId)[0];
        let comp = this.components.filter(q => q.id === otherComponent.salaryComponentId)[0];
        if (comp !== null) {
            return comp.name;
        } else {
            return '';
        }
    }
   
}

