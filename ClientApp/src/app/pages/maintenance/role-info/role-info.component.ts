import { Component, OnInit, ViewChild } from '@angular/core';
import { AlertService, MessageSeverity } from '../../../services/common/alert.service';
import { Role } from '../../../models/account/role.model';
import { Permission } from '../../../models/account/permission.model';
import { AccountService } from '../../../services/account/account.service';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { Utilities } from '../../../services/common/utilities';

@Component({
  selector: 'role-info',
  templateUrl: './role-info.component.html',
  styleUrls: ['./role-info.component.css']
})
export class RoleInfoComponent implements OnInit {


  private isNew = false;
  public modalTitle = "";
  public isSaving = false;
  public roleEdit: Role = new Role();
  public allPermissions: Permission[] = [];
  private selectedValues: { [key: string]: boolean; } = {};
  public serverCallback: () => void;

  @ViewChild('editorModal')
  editorModal: ModalDirective;

  constructor(private alertService: AlertService, private accountService: AccountService) {
  }

  ngOnInit() {

  }

  public save() {
    this.isSaving = true;
    this.roleEdit.permissions = this.getSelectedPermissions();
    if (this.isNew) {
      this.accountService.newRole(this.roleEdit).subscribe(sucess => this.saveSuccessHelper(sucess), error => this.saveFailedHelper(error));
    }
    else {
      this.accountService.updateRole(this.roleEdit).subscribe(response => this.saveSuccessHelper(), error => this.saveFailedHelper(error));
    }
  }

  private saveSuccessHelper(result?: string) {
    this.isSaving = false;
    if (this.isNew) {
      this.alertService.showSucessMessage("Saved successfully");
    } else {
      this.alertService.showSucessMessage("Updated successfully");
    }
    this.editorModal.hide();
    this.serverCallback();
  }

  private saveFailedHelper(error: any) {
    this.isSaving = false;
    let test = Utilities.getHttpResponseMessage(error);
    this.alertService.showInfoMessage(test[0]);
  }


  private refreshLoggedInUser() {
    this.accountService.refreshLoggedInUser()
      .subscribe(user => { },
        error => {
          this.alertService.resetStickyMessage();
          this.alertService.showStickyMessage("Refresh failed", "An error occured whilst refreshing logged in user information from the server", MessageSeverity.error, error);
        });
  }

  public selectAll() {
    this.allPermissions.forEach(p => this.selectedValues[p.value] = true);
  }


  public selectNone() {
    this.allPermissions.forEach(p => this.selectedValues[p.value] = false);
  }


  public toggleGroup(groupName: string) {
    let firstMemberValue: boolean;

    this.allPermissions.forEach(p => {
      if (p.groupName != groupName)
        return;

      if (firstMemberValue == null)
        firstMemberValue = this.selectedValues[p.value] == true;

      this.selectedValues[p.value] = !firstMemberValue;
    });
  }


  private getSelectedPermissions() {
    return this.allPermissions.filter(p => this.selectedValues[p.value] == true);
  }


  newRole(allPermissions: Permission[]) {
    this.modalTitle = "Add"
    this.isNew = true;
    this.allPermissions = allPermissions;
    this.selectedValues = {};
    this.roleEdit = new Role();
    this.editorModal.show();
    return this.roleEdit;

  }

  editRole(role: Role, allPermissions: Permission[]) {
    this.modalTitle = "Edit"
    this.isNew = false;
    this.editorModal.show();
    if (role) {
      this.allPermissions = allPermissions;
      this.selectedValues = {};
      role.permissions.forEach(p => this.selectedValues[p.value] = true);
      this.roleEdit = new Role();
      (<any>Object).assign(this.roleEdit, role);

      return this.roleEdit;
    }
    else {
      return this.newRole(allPermissions);
    }
  }
}
