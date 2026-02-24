import { InterviewType, InterviewTypeModel } from '../../../../models/configuration/recruitment/interview-type.model';
import { Component, OnInit, ViewChild, TemplateRef } from '@angular/core';
import { AlertService } from '../../../../services/common/alert.service';
import { InterviewTypeService } from '../../../../services/configuration/recruitment/interview-type.service';
import { InterviewTypeInfoComponent } from '../interview-type-info/interview-type-info.component';

@Component({
  selector: 'interview-type',
  templateUrl: './interview-type.component.html',
  styleUrls: ['./interview-type.component.css']
})
export class InterviewTypeComponent implements OnInit {

  columns: any[] = [];
  filterQuery: string = "";
  pageNumber = 0;
  count = 0;
  pageSize = 10;

  rows: InterviewType[] = [];
  rowsCache: InterviewType[] = [];
  editedInterviewType: InterviewType;
  loadingIndicator: boolean = true;

  @ViewChild('interviewTypeEditor')
  interviewTypeEditor: InterviewTypeInfoComponent;
  @ViewChild('actionsTemplate')
  actionsTemplate: TemplateRef<any>;

  constructor(private alertService: AlertService, private interviewTypeService: InterviewTypeService) { }

  ngOnInit() {
    this.columns = [
      { prop: "index", name: '#', canAutoResize: false },
      { prop: 'name', name: 'Name' },
      { prop: 'Action', name: 'Action', cellTemplate: this.actionsTemplate, resizeable: false, canAutoResize: false, sortable: false, draggable: false }
    ];

    this.getAllInterviewType(this.pageNumber, this.pageSize, this.filterQuery);
  }

  ngAfterViewInit() {
    this.interviewTypeEditor.serverCallback = () => {
      this.getAllInterviewType(this.pageNumber, this.pageSize, this.filterQuery);
    };
  }

  getAllInterviewType(page?: number, pageSize?: number, name?: string) {
    this.loadingIndicator = true;
    this.interviewTypeService.getAllInterviewType(page, pageSize, name).subscribe(result => this.onSuccessfulDataLoad(result), error => this.onDataLoadFailed(error));
  }
  getData(e) {
    this.getAllInterviewType(this.pageNumber, e, this.filterQuery);
  }
  setPage(e) {
    this.getAllInterviewType(e.offset, this.pageSize, this.filterQuery);
  }
  getFilteredData(filterString) {
    this.getAllInterviewType(this.pageNumber, this.pageSize, filterString);

  }
  onSuccessfulDataLoad(interviewtypes: InterviewTypeModel) {
    this.rowsCache = [...interviewtypes.jobInterviewTypeModel];
    this.rows = interviewtypes.jobInterviewTypeModel;
    interviewtypes.jobInterviewTypeModel.forEach((interviewtype, index, interviewtypes) => {
      (<any>interviewtype).index = index + 1;
    });
    this.count = interviewtypes.totalCount;
    this.loadingIndicator = false;
  }

  onDataLoadFailed(error: any) {
    this.alertService.showInfoMessage("Unable to retrieve list from the server");
  }

  newInterviewType() {
    this.editedInterviewType = this.interviewTypeEditor.newInterviewType();
  }

  editInterviewType(interviewtype: InterviewType) {
    this.editedInterviewType = this.interviewTypeEditor.editInterviewType(interviewtype);
  }

  deleteInterviewType(interviewtype: InterviewType) {
    this.alertService.showConfirm("Are you sure you want to delete?", () => this.deleteInterviewTypeHelper(interviewtype));
  }
  deleteInterviewTypeHelper(interviewtype: InterviewType) {
    this.interviewTypeService.delete(interviewtype.id)
      .subscribe(results => {
        this.getAllInterviewType(this.pageNumber, this.pageSize, this.filterQuery);
        this.alertService.showSucessMessage('Deleted successfully.');
      },
        error => {
          this.alertService.showInfoMessage('An error occured whilst deleting.');
        });
  }

}
