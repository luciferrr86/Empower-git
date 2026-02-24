import { Component, OnInit, ViewChild, TemplateRef } from '@angular/core';
import { FunctionalDepartmentInfoComponent } from '../functional-department-info/functional-department-info.component';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { FunctionalDepartment, FunctionalDepartmentModel } from '../../../models/maintenance/functional-department.model';
import { DepartmentService } from '../../../services/maintenance/department.service';
import { DialogType, AlertService, MessageSeverity } from '../../../services/common/alert.service';
import { Utilities } from '../../../services/common/utilities';
import { AccountService } from '../../../services/account/account.service';


@Component({
  selector: 'functional-department',
  templateUrl: './functional-department.component.html',
  styleUrls: ['./functional-department.component.css']
})
export class FunctionalDepartmentComponent implements OnInit {


  columns: any[] = [];
  filterQuery: string = "";
  pageNumber = 0;
  count = 0;
  pageSize = 10;

  rows: FunctionalDepartment[] = [];
  rowsCache: FunctionalDepartment[] = []; 
  editedDepartment: FunctionalDepartment;
  sourceDepartment: FunctionalDepartment;
  editingDepartmentName: { name: string };
  loadingIndicator: boolean=true;

  @ViewChild('indexTemplate')
  indexTemplate: TemplateRef<any>;
  @ViewChild('departmentEditor')
  departmentEditor: FunctionalDepartmentInfoComponent;
  @ViewChild('actionsTemplate')
  actionsTemplate: TemplateRef<any>;

  constructor(private functionalDepartmentService:DepartmentService,private alertService: AlertService) { 
  }

  ngOnInit() {
    this.columns = [
      { prop: "index", name: '#', cellTemplate: this.indexTemplate, canAutoResize: false },
      { prop: 'name', name: 'Name' },
      { prop: 'Action', name: 'Action', cellTemplate: this.actionsTemplate, resizeable: false, canAutoResize: false, sortable: false, draggable: false }
  ];
  this.getAllDepartment(this.pageNumber, this.pageSize, this.filterQuery);
  }
  ngAfterViewInit() {
    this.departmentEditor.serverCallback = () => {
      this.getAllDepartment(this.pageNumber, this.pageSize, this.filterQuery);
    };
  }

  getAllDepartment(page?: number, pageSize?: number, name?: string){
    this.functionalDepartmentService.getAllDepartment(page, pageSize, name).subscribe(result => this.onSuccessfulDataLoad(result), error => this.onDataLoadFailed(error));
  }

  getFilteredData(filterString) {
    this.getAllDepartment(this.pageNumber, this.pageSize, this.filterQuery);

  }
  getData(e) {
    this.getAllDepartment(this.pageNumber, e, this.filterQuery);
  }
  setPage(e) {
    this.getAllDepartment(e.offset, this.pageSize, this.filterQuery);
  }

  onSuccessfulDataLoad(departments: FunctionalDepartmentModel) { 
    this.rowsCache = [...departments.functionalDepartmentModel];
     this.rows = departments.functionalDepartmentModel;
     departments.functionalDepartmentModel.forEach((department, index, departments) => {
       (<any>department).index = index + 1;
     });
     this.count = departments.totalCount;
     this.loadingIndicator=false;
   }
 
   onDataLoadFailed(error: any) {
     this.alertService.showInfoMessage("Unable to retrieve list from the server");
   }

  newDepartment() {
    this.editedDepartment = this.departmentEditor.newDepartment();
  }

  editDepartment(department: FunctionalDepartment) {
    this.editedDepartment = this.departmentEditor.editDepartment(department);
  }

  deleteDepartment(department: FunctionalDepartment) {
    this.alertService.showConfirm("Are you sure you want to delete?",() => this.deleteDepartmentHelper(department));   
  }
  deleteDepartmentHelper(department: FunctionalDepartment){
    this.functionalDepartmentService.delete(department.id)
    .subscribe(results => {
      this.getAllDepartment(this.pageNumber, this.pageSize, this.filterQuery);        
      this.alertService.showSucessMessage('Deleted successfully.');
    },
      error => {
        this.alertService.showInfoMessage('An error occured whilst deleting.');         
      });
   }

}
