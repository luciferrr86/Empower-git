import { Component, OnInit } from '@angular/core';
import { BulkUploadService } from '../../../services/maintenance/bulk-upload.service';
import { AlertService } from '../../../services/common/alert.service';
import { BulkUpload, BulkUploadModel } from '../../../models/maintenance/bulk-upload.model';
import { CandidateBulkUpload, CandidateBulkUploadModel } from '../../../models/maintenance/candidate-bulk-upload.model';
import { error } from 'util';

@Component({
  selector: 'candidate-bulk-upload',
  templateUrl: './candidate-bulk-upload.component.html',
  styleUrls: ['./candidate-bulk-upload.component.css']
})


export class CandidateBulkUploadComponent implements OnInit {

  columns: any[] = [];
  filterQuery: string = "";
  pageNumber = 0;
  count = 0;
  pageSize = 10;
  public current_progress = 0;

  fileToUpload: File = null;
  candidateBulkModel: CandidateBulkUpload;
  candidateBulkUpload: CandidateBulkUpload[];
  rows: CandidateBulkUpload[] = [];
  rowsCache: CandidateBulkUpload[] = [];
  loadingIndicator: boolean = true;
  data: any;
  candidateName: string;
  jobName: string;
  jobTitle: string;
  phoneNumber: string;
  email: string;
  level1ManagerId: number;
    uploadComplete: boolean=false;
  constructor(private bulkuploadService: BulkUploadService, private alertService: AlertService) {

  }
  ngOnInit() {
    //this.columns = [
    //  { prop: 'candidateName', name: 'Full Name' },
    //  { prop: 'jobName', name: 'Vacancy Name' },
    //  { prop: 'phoneNumber', name: 'Personal Number' },
    //  { prop: 'email', name: 'Email Address' },
    //  { prop: 'lastInterviewedBy', name: 'Last Interviewed By' }
    //];

    //this.getCandidateBulkUpload(this.pageNumber, this.pageSize, this.filterQuery);
    this.getExcelCandidateData();
  }

  getExcelCandidateData() {
    this.bulkuploadService.getExcelCandidateList().subscribe((data) => {
      this.candidateBulkUpload = data;
    });
  }
  //getCandidateBulkUpload(page?: number, pageSize?: number, name?: string) {
  //  this.loadingIndicator = true;
  //  this.bulkuploadService.getFailedBulkUpload(page, pageSize, name).subscribe(result => this.onSuccessfulDataLoad(result, "onInit"), error => this.onDataLoadFailed(error));;
  //}

  //getFilteredData(filterString) {
  // // this.getCandidateBulkUpload(this.pageNumber, this.pageSize, filterString);
  //}
  //getData(e) {
  //  //this.getCandidateBulkUpload(this.pageNumber, e, this.filterQuery);
  //}
  //setPage(e) {
  // // this.getCandidateBulkUpload(e.offset, this.pageSize, this.filterQuery);
  //}

  handleFileInput(event) {
    this.current_progress = 0;
    let fileList: FileList = event.target.files;
    if (fileList.length > 0) {
      this.current_progress = 40;
      this.fileToUpload = fileList[0];
      this.current_progress = 55;
      this.bulkuploadService.candidatePostFile(this.fileToUpload).
        subscribe((data) => {
          this.candidateBulkUpload = data;
          //this.errorFileExist = data["errorFileName"];
          //console.log(this.errorFileExist);
          //console.log(data);
          this.current_progress = 100;
          this.alertService.showInfoMessage("Bulk Upload Successful");
          this.uploadComplete = true;
        });
    }

  }
  enableEdit(id: number) {
    const candidate = this.candidateBulkUpload.filter(s => s.id === id);
    if (candidate !== null && candidate.length > 0) {
      candidate[0].isEdit = true;
      this.candidateName = candidate[0].candidateName;
      this.jobName = candidate[0].jobName;
      this.jobTitle = candidate[0].jobTitle;
      this.level1ManagerId = candidate[0].level1ManagerId;
      this.email = candidate[0].email;
    //  this.feedback = candidate[0].feedback;
    }
  }

  import() {
    this.bulkuploadService.importCandidateData(this.candidateBulkUpload).subscribe((data) => this.onSuccessfulDataLoad(data), error => this.onDataLoadFailed(error))
  }

  disableEdit(id: number) {
    const candidateData = this.candidateBulkUpload.filter(s => s.id === id);
    if (candidateData !== null && candidateData.length > 0) {
      candidateData[0].isEdit = false;
    }
  }

  updateCandidateData(id: number) {
    const candidateData = this.candidateBulkUpload.filter(s => s.id === id);
    if (candidateData != null && candidateData.length > 0) {
      candidateData[0].candidateName = this.candidateName;
      candidateData[0].jobName = this.jobName;
      candidateData[0].jobTitle = this.jobTitle;
      candidateData[0].level1ManagerId = this.level1ManagerId;
      candidateData[0].email = this.email;
      //candidateData[0].feedback = this.candidateBulkModel.feedback;
      candidateData[0].id = id;
      this.bulkuploadService.updateCandidateData(id,this.candidateName, this.jobName, this.jobTitle, this.email, this.level1ManagerId).subscribe(result => {
        //  this.router.navigate(['manageProduct', productId]);
        });
      this.candidateName = '';
      this.jobName = '';
      this.jobTitle = '';
      this.email = '';
      this.level1ManagerId = 0;
    }
    this.disableEdit(id);
  }
  onSuccessfulDataLoad(data: any) {
    console.log(data);
    this.alertService.showSucessMessage("Data Import Successful");


  }
  

  onDataLoadFailed(error: any) {
  console.log(error);
    this.alertService.showInfoMessage("Data Import Failed");
  }
}

