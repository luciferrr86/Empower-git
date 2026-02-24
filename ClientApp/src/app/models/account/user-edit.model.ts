import { User } from './user.model';

export class UserEdit extends User {
  constructor(groupId?: string, designationId?: string, doj?: Date) {

    super();
    this.bandId = this.bandId;
    this.designationId = this.designationId;
    this.doj = this.doj;
    this.groupId = this.groupId;
    this.groupId = this.groupId;
    this.roleName = this.roleName;
    this.location = this.location;
    this.employeeCode = this.employeeCode;
  }
  public managerId: string;
  public groupId: string;
  public designationId: string;
  public doj: Date;
  public titleId: string;
  public bandId: string;
  public roleId: string;
  public location: string;
  public roleName: string;
  public employeeCode: number;
  public empCode: string;
}
