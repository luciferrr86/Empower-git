import { Component, OnInit, AfterViewInit, ViewChild, TemplateRef } from '@angular/core';
import { Role } from '../../../models/account/role.model';
import { Permission } from '../../../models/account/permission.model';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { RoleInfoComponent } from '../role-info/role-info.component';
import { AlertService } from '../../../services/common/alert.service';
import { AccountService } from '../../../services/account/account.service';
import { Utilities } from '../../../services/common/utilities';

@Component({
  selector: 'app-role',
  templateUrl: './role.component.html',
  styleUrls: ['./role.component.css']
})
export class RoleComponent implements OnInit, AfterViewInit {

  columns: any[] = [];
  filterQuery: string = "";
  pageNumber = 0;
  count = 5;
  pageSize = 10;

  rows: Role[] = [];
  rowsCache: Role[] = [];
  allPermissions: Permission[] = [];
  editedRole: Role;
  sourceRole: Role;
  editingRoleName: { name: string };
  loadingIndicator: boolean = true;

  @ViewChild('actionsTemplate')
  actionsTemplate: TemplateRef<any>;

  @ViewChild('editorModal')
  editorModal: ModalDirective;

  @ViewChild('roleEditor')
  roleEditor: RoleInfoComponent;

  constructor(private alertService: AlertService, private accountService: AccountService) {
  }

  ngOnInit() {
    this.columns = [

      { prop: 'name', name: 'Name', width: 200 },
      { prop: 'usersCount', name: 'Role Users Count', width: 30 },
      { name: 'Action', width: 130, cellTemplate: this.actionsTemplate, resizeable: false, canAutoResize: false, sortable: false, draggable: false }
    ];
    this.getAllRoles(this.pageNumber, this.pageSize, this.filterQuery);
  }

  ngAfterViewInit() {
    this.roleEditor.serverCallback = () => {
      this.getAllRoles(this.pageNumber, this.pageSize, this.filterQuery);
    };
  }

  getAllRoles(page?: number, pageSize?: number, name?: string) {
    this.loadingIndicator = true;
    this.accountService.getRolesAndPermissions(page, pageSize, name).subscribe(result => this.onSuccessfulDataLoad(result), error => this.onDataLoadFailed());
  }

  getFilteredData(filterString) {
    this.getAllRoles(this.pageNumber, this.pageSize, filterString);

  }
  getData(e) {
    this.getAllRoles(this.pageNumber, e, this.filterQuery);
  }
  setPage(e) {
    this.getAllRoles(e.offset, this.pageSize, this.filterQuery);
  }
  onSuccessfulDataLoad(roles: any) {
    let permissions = roles[1];
    this.rowsCache = [...roles[0].roleModel];
    this.rows = roles[0].roleModel;

    this.allPermissions = permissions;
    this.rowsCache = [...roles[0].roleModel];
    this.rows = roles[0].roleModel;
    roles[0].roleModel.forEach((role, index) => {
      (<any>role).index = index + 1;
    });
    this.count = roles[0].totalCount;
    this.loadingIndicator = false;
  }

  onDataLoadFailed() {
    this.alertService.showInfoMessage("Unable to retrieve list from the server");
  }


  addNewRoleToList() {
    if (this.sourceRole) {
      (<any>Object).assign(this.sourceRole, this.editedRole);

      let sourceIndex = this.rowsCache.indexOf(this.sourceRole, 0);
      if (sourceIndex > -1)
        Utilities.moveArrayItem(this.rowsCache, sourceIndex, 0);

      sourceIndex = this.rows.indexOf(this.sourceRole, 0);
      if (sourceIndex > -1)
        Utilities.moveArrayItem(this.rows, sourceIndex, 0);

      this.editedRole = null;
      this.sourceRole = null;
    }
    else {
      let role = new Role();
      (<any>Object).assign(role, this.editedRole);
      this.editedRole = null;

      let maxIndex = 0;
      for (let r of this.rowsCache) {
        if ((<any>r).index > maxIndex)
          maxIndex = (<any>r).index;
      }

      (<any>role).index = maxIndex + 1;

      this.rowsCache.splice(0, 0, role);
      this.rows.splice(0, 0, role);
      this.rows = [...this.rows];
    }
  }

  newRole() {
    this.editedRole = this.roleEditor.newRole(this.allPermissions);
  }

  editRole(role: Role) {
    this.editedRole = this.roleEditor.editRole(role, this.allPermissions);
  }

  deleteRole(role: Role) {
    this.alertService.showConfirm("Are you sure you want to delete?", () => this.deleteRoleHelper(role));
  }

  deleteRoleHelper(role: Role) {
    this.accountService.deleteRole(role.id)
      .subscribe(() => {
        this.getAllRoles(this.pageNumber, this.pageSize, this.filterQuery);
        this.alertService.showSucessMessage('Deleted successfully.');
      },
        () => {
          this.alertService.showInfoMessage('An error occured whilst deleting.');
        });
  }
}
