import { Component, OnInit, EventEmitter, Output } from '@angular/core';
import { ExcelDetail } from '../../../models/maintenance/excel-detail.model';
import { MonthDropdown } from '../../../models/common/month.dropdown';
import { HttpRequest, HttpClient, HttpEventType } from '@angular/common/http';
import { NgForm } from '@angular/forms';
import { EmployeeSalaryService } from '../../../services/maintenance/employee-salary.service';

@Component({
  selector: 'app-excel-upload',
  templateUrl: './excel-upload.component.html',
  styleUrls: ['./excel-upload.component.css']
})
export class ExcelUploadComponent implements OnInit{
    public progress: number;
    public message: string;
    @Output() public onUploadFinished = new EventEmitter();
    excelModel: ExcelDetail;
    months: MonthDropdown[] = [
        { id: 2, name: 'February' }, { id: 3, name: 'March' },
    ];
    years = ['2020'];
    filename: string;
    constructor(private employeeSalaryService: EmployeeSalaryService,private http: HttpClient) {
        this.excelModel = new ExcelDetail();
    }
    ngOnInit() {

    }
    uploadFile(files: any) {
        
        if (files.length === 0) {
            return;
        }
        

        let fileToUpload = <File>files[0];
        const formData = new FormData();
        formData.append('file', fileToUpload, fileToUpload.name);
        formData.append('type', 'detail');
        
        this.http.post('/api/ExcelUpload', formData, { reportProgress: true, observe: 'events' })
            .subscribe(event => {
                if (event.type === HttpEventType.UploadProgress)
                    this.progress = Math.round(100 * event.loaded / event.total);
                else if (event.type === HttpEventType.Response) {
                    this.message = 'Data successfully saved.';
                    this.onUploadFinished.emit(event.body);
                }
            });
    }
   
}
