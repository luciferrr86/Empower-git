import { Component, OnInit, ViewChild } from '@angular/core';
import { User } from '../../../models/account/user.model';
import { UserEdit } from '../../../models/account/user-edit.model';
import { IOption } from 'ng-select';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { DropDownList } from '../../../models/common/dropdown';

@Component({
  selector: 'employee-details',
  templateUrl: './employee-details.component.html',
  styleUrls: ['./employee-details.component.css']
})
export class EmployeeDetailsComponent implements OnInit {

  public modalTitle = "";
  private user: User = new User();
  public userEdit: UserEdit = new UserEdit();
  public serverCallback: () => void;
  public isSaving: boolean;

  bandList: Array<IOption> = [];
  designationList: Array<IOption> = [];
  managerList: Array<IOption> = [];
  titleList: Array<IOption> = [];
  groupList: Array<IOption> = [];
  roleList: Array<IOption> = [];

  @ViewChild('editorModal')
  editorModal: ModalDirective;

  constructor() { }

  ngOnInit() {
  }
  userDetails(user: User, allBand: DropDownList[], allRole: DropDownList[], allTitleList: DropDownList[], allGroupList: DropDownList[], allMangerList: DropDownList[], allDesignation: DropDownList[]) {
    this.modalTitle = "Employee Details"
    this.editorModal.show();
    this.bandList = allBand;
    this.titleList = allTitleList;
    this.groupList = allGroupList;
    this.managerList = allMangerList;
    this.titleList = allTitleList;
    this.designationList = allDesignation;
    this.roleList = allRole;
    if (user) {
      this.user = new User();
      this.userEdit = new UserEdit();
      Object.assign(this.user, user);
      Object.assign(this.userEdit, user);
      return this.userEdit;
    }
  }

  save() {

  }
}
