import { Component, OnInit, ViewChild, TemplateRef } from '@angular/core';
import { ModalDirective, BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import { Router } from '@angular/router';
import { VacancyService } from '../../../../services/recruitment/vacancy.service';
import { EmailDirectory, EmailDirectoryViewModel } from '../../../../models/common/emailDirectory';
import { ManageDirectoryComponent } from '../directory/directory.component';
import { AlertService } from '../../../../services/common/alert.service';


@Component({
  selector: 'directoryList',
  templateUrl: './directory-list.component.html',
  styleUrls: ['./directory-list.component.css']
})
export class DirectoryListComponent implements OnInit {
  mangerId: string;
  columns: any[] = [];
  filterQuery: string = "";
  pageNumber = 0;
  count = 0;
  pageSize = 10;
  isSaving: false;

  rows: EmailDirectory[] = [];
  rowsCache: EmailDirectory[] = [];
  editedUser: EmailDirectory;
  sourceUser: EmailDirectory;
  editingUserName: { name: string };
  loadingIndicator: boolean = true;
  
  @ViewChild('editorModalProxy')
  editorModal: ModalDirective;



  @ViewChild('actionsTemplate')
  actionsTemplate: TemplateRef<any>;

 

  @ViewChild('manageDir')
  manageDir: ManageDirectoryComponent;
  
  constructor(private alertService: AlertService, private router: Router, private vacancyService: VacancyService) {

  }

  ngOnInit() {

    this.columns = [
      { prop: 'name', name: 'Name' },
      { prop: 'designation', name: 'Designation' },
      { prop: 'email', name: 'Email' },
      { prop: 'phoneNumber', name: 'Phone Number' },
      { prop: 'Action', name: 'Action', cellTemplate: this.actionsTemplate, resizeable: false, canAutoResize: false, sortable: false, draggable: false }
    ];
    this.getAllDirectory(this.pageNumber, this.pageSize);
  }

  ngAfterViewInit() {
    this.manageDir.serverCallback = () => {
      this.getAllDirectory(this.pageNumber, this.pageSize);
    };
  }

  getAllDirectory(page?: number, pageSize?: number, name?: string) {
    this.loadingIndicator = true;
    this.vacancyService.getDirectoryList(page, pageSize, name).subscribe(result => this.onSuccessfulDataLoad(result), error => this.onDataLoadFailed(error));
  }

  getFilteredData(filterString) {
    this.getAllDirectory(this.pageNumber, this.pageSize, filterString);

  }
  getData(e) {
    this.getAllDirectory(this.pageNumber, e, this.filterQuery);
  }
  setPage(e) {
    this.getAllDirectory(e.offset, this.pageSize, this.filterQuery);
  }
  onSuccessfulDataLoad(result: EmailDirectoryViewModel) {
    
    console.log(result);
    this.rowsCache = [...result.directoryListModel];
    this.rows = result.directoryListModel;
    result.directoryListModel.forEach((user, index, users) => {
      (<any>user).index = index + 1;
    });
    this.loadingIndicator = false;
    this.count = result.totalCount;
    this.loadingIndicator = false;
  }

  onDataLoadFailed(error: any) {
    this.alertService.showInfoMessage("Unable to retrieve list from the server");
  }

  newDir() {
    this.editedUser = this.manageDir.newDir();
  }

  editDir(dir) {
    console.log(dir);
    
    this.editedUser = this.manageDir.editDir(dir);
  }


  deleteDir(user: EmailDirectory) {
    this.alertService.showConfirm("Are you sure you want to delete?", () => this.deleteUserHelper(user));
  }

  deleteUserHelper(user: EmailDirectory) {
    this.vacancyService.deleteDirectory(user.id)
      .subscribe(results => {
        this.getAllDirectory(this.pageNumber, this.pageSize, this.filterQuery);
        this.alertService.showSucessMessage('Deleted successfully.');
      },
        error => {
          this.alertService.showInfoMessage('An error occured whilst deleting.');
        });
  }
  
  
}
