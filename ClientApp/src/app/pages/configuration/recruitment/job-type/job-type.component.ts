import { Component, OnInit, ViewChild, TemplateRef } from '@angular/core';
import { JobTypeModel, JobType } from '../../../../models/configuration/recruitment/job-type.model';
import { AlertService } from '../../../../services/common/alert.service';
import { JobTypeService } from '../../../../services/configuration/recruitment/job-type.service';
import { JobTypeInfoComponent } from '../job-type-info/job-type-info.component';
@Component({
  selector: 'job-type',
  templateUrl: './job-type.component.html',
  styleUrls: ['./job-type.component.css']
})
export class JobTypeComponent implements OnInit {
  columns: any[] = [];
  filterQuery: string = "";
  pageNumber = 0;
  count = 0;
  pageSize = 10;

  rows: JobType[] = [];
  rowsCache: JobType[] = [];
  editedJobType: JobType;
  loadingIndicator: boolean = true;

  @ViewChild('jobTypeEditor')
  jobTypeEditor: JobTypeInfoComponent;
  @ViewChild('actionsTemplate')
  actionsTemplate: TemplateRef<any>;

  constructor(private alertService: AlertService, private jobTypeService: JobTypeService) { }

  ngOnInit() {
    this.columns = [
      { prop: "index", name: '#', canAutoResize: false },
      { prop: 'name', name: 'Name' },
      { prop: 'Action', name: 'Action', cellTemplate: this.actionsTemplate, resizeable: false, canAutoResize: false, sortable: false, draggable: false }
    ];

    this.getAllJobType(this.pageNumber, this.pageSize, this.filterQuery);
  }

  ngAfterViewInit() {
    this.jobTypeEditor.serverCallback = () => {
      this.getAllJobType(this.pageNumber, this.pageSize, this.filterQuery);
    };
  }

  getAllJobType(page?: number, pageSize?: number, name?: string) {
    this.loadingIndicator = true;
    this.jobTypeService.getAllJobType(page, pageSize, name).subscribe(result => this.onSuccessfulDataLoad(result), error => this.onDataLoadFailed(error));
  }
  getData(e) {
    this.getAllJobType(this.pageNumber, e, this.filterQuery);
  }
  setPage(e) {
    this.getAllJobType(e.offset, this.pageSize, this.filterQuery);
  }
  getFilteredData(filterString) {
    this.getAllJobType(this.pageNumber, this.pageSize, filterString);

  }
  onSuccessfulDataLoad(jobtypes: JobTypeModel) {

    this.rowsCache = [...jobtypes.jobTypeModel];
    this.rows = jobtypes.jobTypeModel;
    jobtypes.jobTypeModel.forEach((jobtype, index, jobtypes) => {
      (<any>jobtype).index = index + 1;
    });
    this.count = jobtypes.totalCount;
    this.loadingIndicator = false;
  }

  onDataLoadFailed(error: any) {
    this.alertService.showInfoMessage("Unable to retrieve list from the server");
  }

  newJobType() {
    this.editedJobType = this.jobTypeEditor.newJobType();
  }

  editJobType(jobtype: JobType) {
    this.editedJobType = this.jobTypeEditor.editJobType(jobtype);
  }

  deleteJobType(jobtype: JobType) {
    this.alertService.showConfirm("Are you sure you want to delete?", () => this.deleteJobTypeHelper(jobtype));
  }
  deleteJobTypeHelper(jobtype: JobType) {
    this.jobTypeService.delete(jobtype.id)
      .subscribe(results => {
        this.getAllJobType(this.pageNumber, this.pageSize, this.filterQuery);
        this.alertService.showSucessMessage('Deleted successfully.');
      },
        error => {
          this.alertService.showInfoMessage('An error occured whilst deleting.');
        });
  }
}
