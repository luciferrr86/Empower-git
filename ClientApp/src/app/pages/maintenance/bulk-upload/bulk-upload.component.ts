import { Component, OnInit } from '@angular/core';
import { BulkUploadService } from '../../../services/maintenance/bulk-upload.service';
import { AlertService } from '../../../services/common/alert.service';
import { BulkUpload, BulkUploadModel } from '../../../models/maintenance/bulk-upload.model';

@Component({
  selector: 'app-bulk-upload',
  templateUrl: './bulk-upload.component.html',
  styleUrls: ['./bulk-upload.component.css']
})


export class BulkUploadComponent implements OnInit {

  columns: any[] = [];
  filterQuery: string = "";
  pageNumber = 0;
  count = 0;
  pageSize = 10;

  fileToUpload: File = null;
  rows: BulkUpload[] = [];
  rowsCache: BulkUpload[] = [];
  loadingIndicator: boolean = true;
  data: any;

  constructor(private bulkuploadService: BulkUploadService, private alertService: AlertService) {

  }
  ngOnInit() {
    this.columns = [
      { prop: 'fullName', name: 'Full Name' },
      { prop: 'workEmailAddress', name: 'Work Email Address' },
      { prop: 'personalEmailID', name: 'Personal Mail Id' },
      { prop: 'reportingHeadEmailId', name: 'Reporting Head Mail Id' },
      { prop: 'status', name: 'Status' },
      { prop: 'errorMessage', name: 'Error Message' }
    ];

    this.getFailedBulkUpload(this.pageNumber, this.pageSize, this.filterQuery);
  }


  getFailedBulkUpload(page?: number, pageSize?: number, name?: string) {
    this.loadingIndicator = true;
    this.bulkuploadService.getFailedBulkUpload(page, pageSize, name).subscribe(result => this.onSuccessfulDataLoad(result, "onInit"), error => this.onDataLoadFailed(error));;
  }

  getFilteredData(filterString) {
    this.getFailedBulkUpload(this.pageNumber, this.pageSize, filterString);
  }
  getData(e) {
    this.getFailedBulkUpload(this.pageNumber, e, this.filterQuery);
  }
  setPage(e) {
    this.getFailedBulkUpload(e.offset, this.pageSize, this.filterQuery);
  }

  handleFileInput(event) {
    let fileList: FileList = event.target.files;
    if (fileList.length > 0) {
      this.fileToUpload = fileList[0];
      this.bulkuploadService.postFile(this.fileToUpload).subscribe(result => this.onSuccessfulDataLoad(result, "onChange"), error => this.onDataLoadFailed(error));;
    }

  }
  onSuccessfulDataLoad(bulkuploads: BulkUploadModel, action: string) {
    this.rowsCache = [...bulkuploads.bulkUploadModel];
    this.rows = bulkuploads.bulkUploadModel;
    bulkuploads.bulkUploadModel.forEach((bulkupload, index, bulkuploads) => {
      (<any>bulkupload).index = index + 1;
    });
    this.data = bulkuploads.bulkUploadModel;
    this.count = bulkuploads.totalCount;
    this.loadingIndicator = false;
    if (action == "onChange") {
      if (this.count == 0) {
        this.alertService.showInfoMessage("Bulk Upload Successful");
      }

      else {
        this.alertService.showInfoMessage("Bulk Upload partially Successful");
      }
    }
  }

  onDataLoadFailed(error: any) {
    this.alertService.showInfoMessage("Bulk Upload Failed");
  }
}
