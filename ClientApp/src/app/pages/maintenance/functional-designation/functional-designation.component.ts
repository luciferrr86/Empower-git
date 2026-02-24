import { Component, OnInit, ViewChild, TemplateRef } from '@angular/core';
import { FunctionalDesignationInfoComponent } from '../functional-designation-info/functional-designation-info.component';
import { FunctionalDesignation, FunctionalDesignationModel } from '../../../models/maintenance/functional-designation.model';
import { FunctionalDesignationService } from '../../../services/maintenance/functional-designation.service';
import { AlertService } from '../../../services/common/alert.service';



@Component({
  selector: 'functional-designation',
  templateUrl: './functional-designation.component.html',
  styleUrls: ['./functional-designation.component.css']
})
export class FunctionalDesignationComponent implements OnInit {

  columns: any[] = [];
  filterQuery: string = "";
  pageNumber = 0;
  count = 0;
  pageSize = 10;

  rows: FunctionalDesignation[] = [];
  rowsCache: FunctionalDesignation[] = [];
  editedDesignation: FunctionalDesignation;
  sourceDesignation: FunctionalDesignation;
  editingDesignationName: { name: string };
  loadingIndicator: boolean = true;

  @ViewChild('indexTemplate')
  indexTemplate: TemplateRef<any>;
  @ViewChild('designationEditor')
  designationEditor: FunctionalDesignationInfoComponent;
  @ViewChild('actionsTemplate')
  actionsTemplate: TemplateRef<any>;

  constructor(private alertService: AlertService, private functionalDesignationService: FunctionalDesignationService) { }

  ngOnInit() {
    this.columns = [
      { prop: "index", name: '#', cellTemplate: this.indexTemplate, canAutoResize: false },
      { prop: 'name', name: 'Name' },
      { prop: 'Action', name: 'Action', cellTemplate: this.actionsTemplate, resizeable: false, canAutoResize: false, sortable: false, draggable: false }
    ];

    this.getAllDesignation(this.pageNumber, this.pageSize, this.filterQuery);
  }

  ngAfterViewInit() {
    this.designationEditor.serverCallback = () => {
      this.getAllDesignation(this.pageNumber, this.pageSize, this.filterQuery);
    };
  }

  getAllDesignation(page?: number, pageSize?: number, name?: string) {
    this.loadingIndicator = true;
    this.functionalDesignationService.getAllDesignation(page, pageSize, name).subscribe(result => this.onSuccessfulDataLoad(result), error => this.onDataLoadFailed());
  }

  getFilteredData(filterString) {
    this.getAllDesignation(this.pageNumber, this.pageSize, filterString);

  }
  getData(e) {
    this.getAllDesignation(this.pageNumber, e, this.filterQuery);
  }
  setPage(e) {
    this.getAllDesignation(e.offset, this.pageSize, this.filterQuery);
  }
  onSuccessfulDataLoad(designations: FunctionalDesignationModel) {

    this.rowsCache = [...designations.functionalDesignationModel];
    this.rows = designations.functionalDesignationModel;
    designations.functionalDesignationModel.forEach((designation, index) => {
      (<any>designation).index = index + 1;
    });
    this.count = designations.totalCount;
    this.loadingIndicator = false;
  }

  onDataLoadFailed() {
    this.alertService.showInfoMessage("Unable to retrieve list from the server");
  }

  newDesignation() {
    this.editedDesignation = this.designationEditor.newDesignation();
  }

  editDesignation(designation: FunctionalDesignation) {
    this.editedDesignation = this.designationEditor.editDesignation(designation);
  }

  deleteDesignation(designation: FunctionalDesignation) {
    this.alertService.showConfirm("Are you sure you want to delete?", () => this.deleteDesignationHelper(designation));
  }
  deleteDesignationHelper(designation: FunctionalDesignation) {
    this.functionalDesignationService.delete(designation.id)
      .subscribe(() => {
        this.getAllDesignation(this.pageNumber, this.pageSize, this.filterQuery);
        this.alertService.showSucessMessage('Deleted successfully.');
      },
        () => {
          this.alertService.showInfoMessage('An error occured whilst deleting.');
        });
  }

}
