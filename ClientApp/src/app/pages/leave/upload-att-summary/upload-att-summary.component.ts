import { Component, OnInit, EventEmitter, Output } from '@angular/core';
import { EmployeeSalaryService } from '../../../services/maintenance/employee-salary.service';
import { HttpClient, HttpEventType } from '@angular/common/http';

@Component({
  selector: 'app-upload-att-summary',
  templateUrl: './upload-att-summary.component.html',
  styleUrls: ['./upload-att-summary.component.css']
})
export class UploadAttSummaryComponent implements OnInit {
    public progress: number;
    public message: string;
    @Output() public onUploadFinished = new EventEmitter();
    constructor(private employeeSalaryService: EmployeeSalaryService, private http: HttpClient) {
   // this.excelModel = new ExcelDetail();
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
        formData.append('type', 'summary');

        this.http.post('/api/ExcelUpload', formData, { reportProgress: true, observe: 'events' })
            .subscribe(event => {
                if (event.type === HttpEventType.UploadProgress)
                  this.progress = Math.round(event.loaded / event.total * 100);
                else if (event.type === HttpEventType.Response) {
                    this.message = 'Data successfully saved.';
                    this.onUploadFinished.emit(event.body);
                }
            });
    }

}

