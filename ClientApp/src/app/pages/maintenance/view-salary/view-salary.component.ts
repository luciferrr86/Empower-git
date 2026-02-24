import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import { EmployeeSalaryModel } from '../../../models/maintenance/employee-salary.model';
import { EmployeeSalaryService } from '../../../services/maintenance/employee-salary.service';
import { ActivatedRoute } from '@angular/router';
import * as jspdf from 'jspdf';
import html2canvas from 'html2canvas';
import { PayslipModel } from '../../../models/maintenance/Payslip.model';

@Component({
  selector: 'app-view-salary',
  templateUrl: './view-salary.component.html',
  styleUrls: ['./view-salary.component.css']
})
export class ViewSalaryComponent implements OnInit {
  empSalaryDetail: EmployeeSalaryModel;
  paySlip: PayslipModel;
  address: string;
  constructor(private route: ActivatedRoute, private employeeSalaryService: EmployeeSalaryService) {
    //this.empSalaryDetail = new EmployeeSalaryModel();
    this.paySlip = new PayslipModel();
  }

  ngOnInit() {
    let empSalId = this.route.snapshot.params['id'];
    this.getSalaryDetailById(empSalId);
  }

  getSalaryDetailById(empSalId) {
    this.employeeSalaryService.getSalaryDetailsById(empSalId)
      .subscribe(result => {
        this.paySlip = result;
        this.address = this.paySlip.location.toUpperCase();
      });
  }

  exportAsPDF() {
    var data = document.getElementById('MyDIv');  //Id of the table
    html2canvas(data).then(canvas => {
      // Few necessary setting options  
      let imgWidth = 208;
      let pageHeight = 295;
      let imgHeight = canvas.height * imgWidth / canvas.width;
      let heightLeft = imgHeight;

      const contentDataURL = canvas.toDataURL('image/png')
      let pdf = new jspdf('p', 'mm', 'a4'); // A4 size page of PDF  
      let position = 0;
      var name = this.paySlip.employeeName + '_' + this.paySlip.month + '_' + this.paySlip.year;
      var filename = name + '.pdf';
      pdf.addImage(contentDataURL, 'PNG', 0, position, imgWidth, imgHeight)
      pdf.save(filename); // Generated PDF
    });
  }


}
