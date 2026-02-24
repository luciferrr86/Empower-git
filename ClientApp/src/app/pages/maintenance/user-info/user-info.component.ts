import { Component, OnInit, Input, ViewChild } from '@angular/core';
import { Utilities } from '../../../services/common/utilities';
import { User } from '../../../models/account/user.model';
import { UserEdit } from '../../../models/account/user-edit.model';
import { Role } from '../../../models/account/role.model';
import { AlertService } from '../../../services/common/alert.service';
import { AccountService } from '../../../services/account/account.service';
import { IOption } from 'ng-select';
import { DropDownList } from '../../../models/common/dropdown';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { LocalStoreManager } from '../../../services/common/local-store-manager.service';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'user-info',
  templateUrl: './user-info.component.html',
  styleUrls: ['./user-info.component.css']
})
export class UserInfoComponent implements OnInit {

  private isNew = false;
  public isSaving = false;
  public modalTitle = "";
  public userEdit: UserEdit = new UserEdit();
  public serverCallback: () => void;

  bandList: Array<IOption> = [];
  designationList: Array<IOption> = [];
  managerList: Array<IOption> = [];
  titleList: Array<IOption> = [];
  groupList: Array<IOption> = [];
  roleList: Array<IOption> = [];

  @Input()
  isViewOnly: boolean;

  @ViewChild('editorModal')
  editorModal: ModalDirective;

  constructor(private alertService: AlertService, private accountService: AccountService, private localStorage: LocalStoreManager) {
    this.userEdit.doj = new Date();
  }

  ngOnInit() {
  }

  newUser(allRoles: Role[], allBand: DropDownList[], allRole: DropDownList[], allTitleList: DropDownList[], allGroupList: DropDownList[], allMangerList: DropDownList[], allDesignation: DropDownList[]) {

    this.modalTitle = "Add"
    this.editorModal.show();
    this.isNew = true;
    this.bandList = allBand;
    this.titleList = allTitleList;
    this.groupList = allGroupList;
    this.managerList = allMangerList;
    this.titleList = allTitleList;
    this.designationList = allDesignation;
    this.roleList = allRole;
    this.userEdit.isEnabled = true;
    return this.userEdit;
  }

  editUser(user: User, allBand: DropDownList[], allRole: DropDownList[], allTitleList: DropDownList[], allGroupList: DropDownList[], allMangerList: DropDownList[], allDesignation: DropDownList[]) {

    this.modalTitle = "Edit"
    this.editorModal.show();
    this.bandList = allBand;
    this.titleList = allTitleList;
    this.groupList = allGroupList;
    this.managerList = allMangerList;
    this.titleList = allTitleList;
    this.designationList = allDesignation;
    this.roleList = allRole;
    if (user) {
      this.isNew = false;
      this.userEdit = new UserEdit();
      (<any>Object).assign(this.userEdit, user);
      return this.userEdit;
    }
  }


  public save() {
    let roleName = this.roleList.filter(role => role.value == this.userEdit.roleId);
    this.isSaving = true;
    this.userEdit.roles = [roleName[0].label];
    if (this.isNew) {
      this.accountService.newUser(this.userEdit).subscribe(sucess => this.saveSuccessHelper(sucess), error => this.saveFailedHelper(error));
    }
    else {
      this.accountService.updateUser(this.userEdit).subscribe(result => this.saveSuccessHelper(result), error => this.saveFailedHelper(error));
    }
  }

  private saveSuccessHelper(result: any) {
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

  public close(form: NgForm) {
    form.reset();
    this.editorModal.hide();
  }
}
