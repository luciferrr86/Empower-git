import { Component, OnInit, ViewChild, TemplateRef, Input } from '@angular/core';
import { FunctionalGroup, FunctionalGroupModel } from '../../../models/maintenance/functional-group.model';
import { FunctionalGroupInfoComponent } from '../functional-group-info/functional-group-info.component';
import { GroupService } from '../../../services/maintenance/group.service';
import { AlertService, MessageSeverity } from '../../../services/common/alert.service';
import { DropDownList } from '../../../models/common/dropdown';

@Component({
  selector: 'functional-group',
  templateUrl: './functional-group.component.html',
  styleUrls: ['./functional-group.component.css']
})
export class FunctionalGroupComponent implements OnInit {

  columns: any[] = [];
  filterQuery: string = "";
  pageNumber = 0;
  count = 0;
  pageSize = 10;

  rows: FunctionalGroup[] = [];
  rowsCache: FunctionalGroup[] = []; 
  editedGroup: FunctionalGroup;
  sourceGroup: FunctionalGroup;
  allDepartment:DropDownList[]=[];
  editingGroupName: { name: string };
  loadingIndicator: boolean=true;


  @ViewChild('indexTemplate')
  indexTemplate: TemplateRef<any>;
  @ViewChild('groupEditor')
  groupEditor: FunctionalGroupInfoComponent;
  @ViewChild('actionsTemplate')
  actionsTemplate: TemplateRef<any>;

  constructor(private FunctionalFroupService:GroupService,private alertService: AlertService) { 
  }

  ngOnInit() {
    this.columns = [
      { prop: "index", name: '#', cellTemplate: this.indexTemplate, canAutoResize: false },
      { prop: 'name', name: 'Name' },
      { prop: 'Action', name: 'Action', cellTemplate: this.actionsTemplate, resizeable: false, canAutoResize: false, sortable: false, draggable: false }
  ];
  this.getAllGroup(this.pageNumber, this.pageSize, this.filterQuery);
  }
  
  ngAfterViewInit() {
    this.groupEditor.serverCallback = () => {
      this.getAllGroup(this.pageNumber, this.pageSize, this.filterQuery);
    };
}
  getAllGroup(page?: number, pageSize?: number, name?: string){
    this.FunctionalFroupService.getAllGroup(page, pageSize, name).subscribe(result => this.onSuccessfulDataLoad(result), error => this.onDataLoadFailed(error));
  }

  getFilteredData(filterString) {
    this.getAllGroup(this.pageNumber, this.pageSize, this.filterQuery);

  }
  getData(e) {
    this.getAllGroup(this.pageNumber, e, this.filterQuery);
  }
  setPage(e) {
    this.getAllGroup(e.offset, this.pageSize, this.filterQuery);
  }

  onSuccessfulDataLoad(groups: FunctionalGroupModel) {
    this.rowsCache = [...groups.functionalGroupModel];
     this.rows = groups.functionalGroupModel;
     groups.functionalGroupModel.forEach((group, index, groups) => {
       (<any>group).index = index + 1;
     });
    this.allDepartment=groups.departmentList;
     console.log(groups.functionalGroupModel);
     this.count = groups.totalCount;
     this.loadingIndicator=false;
   }
 
   onDataLoadFailed(error: any) {
     this.alertService.showInfoMessage("Unable to retrieve list from the server");
   }

  newGroup() {    
    this.editedGroup = this.groupEditor.addGroup(this.allDepartment);
  }

  editGroup(group: FunctionalGroup) {
    this.editedGroup = this.groupEditor.updateGroup(group,this.allDepartment);
  }

  deleteGroup(group: FunctionalGroup) {
    this.alertService.showConfirm("Are you sure you want to delete?",() => this.deleteGroupHelper(group));

  }
  deleteGroupHelper(group: FunctionalGroup){
    this.FunctionalFroupService.delete(group.id)
      .subscribe(results => {
        this.getAllGroup(this.pageNumber, this.pageSize, this.filterQuery);        
        this.alertService.showSucessMessage('Deleted successfully.');
      },
        error => {
          this.alertService.showInfoMessage('An error occured whilst deleting.');         
        });
  }
}
