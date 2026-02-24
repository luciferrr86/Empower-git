import { Component, OnInit } from '@angular/core';
import { SalaryComponentModel } from '../../../models/maintenance/salary-component.model';
import { EmployeeSalaryService } from '../../../services/maintenance/employee-salary.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-sal-component-list',
  templateUrl: './sal-component-list.component.html',
  styleUrls: ['./sal-component-list.component.css']
})
export class SalComponentListComponent implements OnInit {
    components: SalaryComponentModel[];
    salaryComponent: SalaryComponentModel;
    constructor(private employeeSalaryService: EmployeeSalaryService, private router: Router) {
        this.salaryComponent = new SalaryComponentModel();
    }

    ngOnInit() {
        this.getComponentList();
  }

    getComponentList() {
        this.employeeSalaryService.getSalComponents().subscribe((result) => {
            this.components = result;
        });
    }

    editComponent(compId: number) {
        this.router.navigate(['../maintenance/process-salary/salary-component/', compId]);
    }
}
