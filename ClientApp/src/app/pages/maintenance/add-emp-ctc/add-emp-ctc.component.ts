import { Component, OnInit} from '@angular/core';
import { EmployeeCtcModel } from '../../../models/maintenance/employee-ctc.model';
import { ActivatedRoute, Router } from '@angular/router';
import { EmployeeSalaryService } from '../../../services/maintenance/employee-salary.service';
import { NgForm } from '@angular/forms';
import { SalaryComponentModel } from '../../../models/maintenance/salary-component.model';
import { CtcOtherComponent } from '../../../models/maintenance/ctc-other-component.model';
import { CheckSalaryModel } from '../../../models/maintenance/check-salary.model';


@Component({
  selector: 'app-add-emp-ctc',
  templateUrl: './add-emp-ctc.component.html',
  styleUrls: ['./add-emp-ctc.component.css']
})
export class AddEmpCtcComponent implements OnInit  {
    employee: CheckSalaryModel;
    empCtc: EmployeeCtcModel;
    isSubmit = false;
    components: SalaryComponentModel[];
    yearlyComponents: CtcOtherComponent[];
    monthlyComponents: CtcOtherComponent[];
    comp: CtcOtherComponent;
    ctcOtherComponent: CtcOtherComponent[];
    constructor(private route: ActivatedRoute, private employeeSalaryService: EmployeeSalaryService, private router: Router) {
        this.empCtc = new EmployeeCtcModel();
        this.employee = new CheckSalaryModel();
        this.yearlyComponents = [];
        this.monthlyComponents = [];
    }

    ngOnInit() {
        const empId = this.route.snapshot.params['id'];        
        this.getSalComponent(empId);
        this.EmpById(empId);
    }

    EmpById(empId) {
        
        this.employeeSalaryService.getEmp(empId).subscribe(result => {
            this.employee = result;
        });
    }

    submitCtc(form: NgForm) {
      //  
      if (this.empCtc['employee'] != null) this.empCtc['employee'] = null;
      console.log(this.empCtc);
        var formdata = this.empCtc;
        this.isSubmit = true;
        this.employeeSalaryService.addEmployeeCtc(formdata)
            .subscribe((data) => {
                this.router.navigate(['../maintenance/employee/list']);
            });
    }
    
    getCtcByEmpId(empId: string) {
        this.employeeSalaryService.getCtcByEmployeeId(empId)
            .subscribe(result => {
                
                this.empCtc = result;
                this.ctcOtherComponent = this.empCtc.ctcOtherComponent;
                for (var i = 0; i < this.ctcOtherComponent.length; i++) {
                    let compId = this.ctcOtherComponent[i].salaryComponentId;
                    let component = this.components.filter(q => q.id == compId)[0];
                    if (component != null) {
                        if (component.isMonthly) {
                            this.monthlyComponents.push(this.ctcOtherComponent[i]);
                        } else {
                            this.yearlyComponents.push(this.ctcOtherComponent[i]);
                        }
                    }
                }                
            });
    }
    
    getSalComponent(empId: string) {
        this.employeeSalaryService.getSalComponents().subscribe((result) => {
            this.components = result;
            this.getCtcByEmpId(empId);
        });
    }

    getMonthlyIntake() {
        let monthlyIntake = this.empCtc.ctc;
        for (var i = 0; i < this.yearlyComponents.length; i++) {
            monthlyIntake = monthlyIntake - this.yearlyComponents[i].amount;
        }
      monthlyIntake = monthlyIntake / 12;
      return monthlyIntake.toFixed(2);
    // this.empCtc.total = monthlyIntake.toFixed(2);
  }
  ngDoCheck() {
    this.getUpdatedCTC(this.empCtc);
    // this.ChangeSalaryMonthYear();
  }
  getUpdatedCTC(empCtc) {
    if (this.empCtc) {
      this.empCtc.total = this.empCtc.basicSalary + this.empCtc.da + this.empCtc.hra + this.empCtc.conveyance + this.empCtc.medicalExpenses + this.empCtc.special + this.empCtc.bonus;
    }
  }

  //ngDoCheck() {
  //  this.getUpdateEmpCtc(this.empCtc);

  //}
  
    getComponentName(cmpId: number) {
    
        let comp = this.components.filter(q => q.id === cmpId)[0];
        if (comp !== null) {
            return comp.name;
        } else {
            return '';
        }
    }
}
