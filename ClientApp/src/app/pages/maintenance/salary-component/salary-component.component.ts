import { Component, OnInit } from '@angular/core';
import { SalaryComponentModel } from '../../../models/maintenance/salary-component.model';
import { NgForm } from '@angular/forms';
import { EmployeeSalaryService } from '../../../services/maintenance/employee-salary.service';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-salary-component',
  templateUrl: './salary-component.component.html',
  styleUrls: ['./salary-component.component.css']
})
export class SalaryComponentComponent implements OnInit {
    salaryComponent: SalaryComponentModel;
    isSubmit = false;
    constructor(private route: ActivatedRoute, private employeeSalaryService: EmployeeSalaryService, private router: Router) {
        this.salaryComponent = new SalaryComponentModel();
    }

    ngOnInit() {
        const compId = this.route.snapshot.params['id'];
        if (compId != null) {
            this.getComponentById(compId);
        }
  }
    getComponentById(compId) {
        this.employeeSalaryService.getCompById(compId)
            .subscribe((data) => {
                
                this.salaryComponent = data;
                if (this.salaryComponent.isEarnings == true) {
                    this.salaryComponent.isEarnings;
                }
            });
    }

    submitSalComponent(form: NgForm) {
        
        this.salaryComponent.isEarnings = form.controls['isEarnings'].value;
        this.salaryComponent.isActive = form.controls['isActive'].value;
        this.salaryComponent.isMonthly = form.controls['isMonthly'].value; 
        var formdata = this.salaryComponent;
        this.isSubmit = true;
        this.employeeSalaryService.addSalComponent(formdata)
            .subscribe((data) => {
               this.router.navigate(['../maintenance/process-salary/sal-com-list']);
            });
    }
}
