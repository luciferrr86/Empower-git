import { Component, OnInit, ViewChild, TemplateRef } from '@angular/core';
import { AlertService, MessageSeverity, DialogType } from '../../../services/common/alert.service';
import { ModalDirective, BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import { AccountService } from '../../../services/account/account.service';
import { User, UserViewModel } from '../../../models/account/user.model';
import { UserEdit } from '../../../models/account/user-edit.model';
import { Permission } from '../../../models/account/permission.model';
import { Role } from '../../../models/account/role.model';
import { Utilities } from '../../../services/common/utilities';
import { UserInfoComponent } from '../user-info/user-info.component';
import { DropDownList } from '../../../models/common/dropdown';
import { ToastyService, ToastyConfig, ToastOptions, ToastData } from 'ng2-toasty';
import { EmployeeDetailsComponent } from '../employee-details/employee-details.component';
import { IOption } from 'ng-select';
import { Router } from '@angular/router';
import { EmployeeSalaryService } from '../../../services/maintenance/employee-salary.service';
import { AddEmpCtcComponent } from '../add-emp-ctc/add-emp-ctc.component';
import { EmployeeCtcModel } from '../../../models/maintenance/employee-ctc.model';
import { EmployeeSalaryModel } from '../../../models/maintenance/employee-salary.model';
import { ColumnMode, SortType } from '@swimlane/ngx-datatable';


@Component({
  selector: 'app-employee',
  templateUrl: './employee.component.html',
  styleUrls: ['./employee.component.css']
})
export class EmployeeComponent implements OnInit {
  modalRef: BsModalRef;
  mangerId: string;
  columns: any[] = [];
  ColumnMode = ColumnMode;
  SortType = SortType;
  filterQuery: string = "";
  pageNumber = 0;
  count = 0;
  pageSize = 10;
  isSaving: false;
  managerId: string;
  managerList: Array<IOption> = [];
  rows: User[] = [];
  rowsCache: User[] = [];
  editedUser: UserEdit;
  sourceUser: UserEdit;
  editingUserName: { name: string };
  loadingIndicator: boolean = true;
  allRoles: Role[] = [];
  allBand: DropDownList[] = [];
  allTitle: DropDownList[] = []
  allGroup: DropDownList[] = [];
  allRole: DropDownList[] = [];
  allMangerList: DropDownList[] = [];
  allDesignationList: DropDownList[] = [];
  empCtc: EmployeeCtcModel;
  empSal: EmployeeSalaryModel;
  @ViewChild('editorModalProxy')
  editorModal: ModalDirective;

  @ViewChild('indexTemplate')
  indexTemplate: TemplateRef<any>;

  @ViewChild('actionsTemplate')
  actionsTemplate: TemplateRef<any>;

  @ViewChild('proxyTemplate')
  proxyTemplate: TemplateRef<any>;

  @ViewChild('userEditor')
  userEditor: UserInfoComponent;
  @ViewChild('employeeDetails')
  employeeDetails: EmployeeDetailsComponent;

  constructor(private alertService: AlertService, private router: Router, private accountService: AccountService, private employeeSalaryService: EmployeeSalaryService, private modalService: BsModalService) {

  }

  ngOnInit() {

    this.columns = [
      { prop: "index", name: '#', cellTemplate: this.indexTemplate, width: 65, draggable: false },
      { prop: 'empCode', name: 'Emp Code', width: 120, draggable: false },
      { prop: 'fullName', name: 'Name', draggable: false },
      { prop: 'group', name: 'Functional Group', draggable: false },
      { prop: 'designation', name: 'Designation', draggable: false },
      { prop: 'email', name: 'Email', draggable: false },
      { prop: 'doj', name: 'DOJ', width: 100, draggable: false },
      { prop: 'Proxy', name: 'Proxy', cellTemplate: this.proxyTemplate, width: 75, resizeable: false, canAutoResize: false, sortable: false, draggable: false },
      { prop: 'Action', name: 'Action', cellTemplate: this.actionsTemplate, resizeable: false, canAutoResize: false, sortable: false, draggable: false }
    ];
    this.getAllUsers(this.pageNumber, this.pageSize);
  }

  ngAfterViewInit() {
    this.userEditor.serverCallback = () => {
      this.getAllUsers(this.pageNumber, this.pageSize);
    };
  }

  getAllUsers(page?: number, pageSize?: number, name?: string) {
    this.loadingIndicator = true;
    this.accountService.getUsersAndRoles(page, pageSize, name).subscribe(result => this.onSuccessfulDataLoad(result[0], result[1]), error => this.onDataLoadFailed(error));
  }

  getFilteredData(filterString) {
    this.getAllUsers(this.pageNumber, this.pageSize, filterString);

  }
  getData(e) {
    this.getAllUsers(this.pageNumber, e, this.filterQuery);
  }
  setPage(e) {
    this.getAllUsers(e.offset, this.pageSize, this.filterQuery);
  }
  onSuccessfulDataLoad(result: UserViewModel, roles: any) {
    this.rowsCache = [...result.userModel];
    this.rows = result.userModel;
    result.userModel.forEach((user, index, users) => {
      (<any>user).index = index + 1;
    });
    this.allRoles = roles;
    this.allBand = result.bandList;
    this.allGroup = result.groupList;
    this.allTitle = result.titleList;
    this.allDesignationList = result.designationList;
    this.allMangerList = result.managerList;
    this.allRole = result.roleList;
    this.loadingIndicator = false;
    this.count = result.totalCount;
    this.loadingIndicator = false;
  }

  onDataLoadFailed(error: any) {
    this.alertService.showInfoMessage("Unable to retrieve list from the server");
  }

  newUser() {
    this.editedUser = this.userEditor.newUser(this.allRoles, this.allBand, this.allRole, this.allTitle, this.allGroup, this.allMangerList, this.allDesignationList);
  }

  editUser(user: UserEdit) {
    this.editedUser = this.userEditor.editUser(user, this.allBand, this.allRole, this.allTitle, this.allGroup, this.allMangerList, this.allDesignationList);
  }

  viewUser(user: UserEdit) {
    this.editedUser = this.employeeDetails.userDetails(user, this.allBand, this.allRole, this.allTitle, this.allGroup, this.allMangerList, this.allDesignationList);
  }

  deleteUser(user: UserEdit) {
    this.alertService.showConfirm("Are you sure you want to delete?", () => this.deleteUserHelper(user));
  }

  deleteUserHelper(user: UserEdit) {
    this.accountService.deleteUser(user.id)
      .subscribe(results => {
        this.getAllUsers(this.pageNumber, this.pageSize, this.filterQuery);
        this.alertService.showSucessMessage('Deleted successfully.');
      },
        error => {
          this.alertService.showInfoMessage('An error occured whilst deleting.');
        });
  }
  salaryInfo(user: UserEdit) {
    this.router.navigate(['../maintenance/employee-salary/', user.id]);

  }
  salaryDetail(user: UserEdit) {
    this.router.navigate(['../maintenance/employee-salary-detail/', user.id]);
  }
  addCtc(user: UserEdit) {
    this.router.navigate(['../maintenance/employee-ctc/', user.id]);
  }
  allEmpSal() {
    this.employeeSalaryService.processSalary(this.empSal).subscribe(result => {
      this.router.navigate(['../maintenance/employee/list']);
    });
  }

  setProxy(user: UserEdit) {
    this.editorModal.show();
  }
  save() {

  }
}
