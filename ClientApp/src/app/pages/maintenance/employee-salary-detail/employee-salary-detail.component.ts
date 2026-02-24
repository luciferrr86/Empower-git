import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { EmployeeSalaryService } from '../../../services/maintenance/employee-salary.service';
import { EmployeeSalaryModel } from '../../../models/maintenance/employee-salary.model';

@Component({
  selector: 'app-employee-salary-detail',
  templateUrl: './employee-salary-detail.component.html',
  styleUrls: ['./employee-salary-detail.component.css']
})
export class EmployeeSalaryDetailComponent implements OnInit {
    empSalaryDetail: EmployeeSalaryModel;
    constructor(private route: ActivatedRoute, private employeeSalaryService: EmployeeSalaryService) {
        this.empSalaryDetail = new EmployeeSalaryModel();
    }

    ngOnInit() {
        let empId = this.route.snapshot.params['id'];
        this.getSalaryDetailByEmpId(empId);
    }

    getSalaryDetailByEmpId(empId) {
        
        this.employeeSalaryService.getSalaryDetailsByEmpId(empId)
            .subscribe(result => {
                
                this.empSalaryDetail = result
            });
    }
}
