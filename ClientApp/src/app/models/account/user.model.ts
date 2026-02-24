import { DropDownList } from "../common/dropdown";

export class User {
    constructor(id?: string, userName?: string, fullName?: string, email?: string, phoneNumber?: string, roles?: string[], type?: string) {

        this.id = id;
        this.userName = userName;
        this.fullName = fullName;
        this.email = email;
        this.phoneNumber = phoneNumber;
        this.roles = roles;
        this.type = type;
    }


    get friendlyName(): string {
        let name = this.fullName || this.userName;
        return name;
    }
    public id: string;
    public userName: string;
    public fullName: string;
    public email: string;
    public phoneNumber: string;
    public isEnabled: boolean;
    public isLockedOut: boolean;
    public managerId: string;
    public roles: string[];
    public type: string;
}
export class ModuleAccess{
    constructor(isLeave?: boolean, isPerformance?: boolean, isTimesheet?: boolean, isExpanse?: boolean, isRecruitment?: boolean, isSales?: boolean) {

        this.isLeave = isLeave;
        this.isPerformance = isPerformance;
        this.isTimesheet = isTimesheet;
        this.isExpanse = isExpanse;
        this.isRecruitment = isRecruitment;
        this.isSales = isSales;
    }
    public isLeave: boolean;
    public isPerformance: boolean; 
    public isTimesheet: boolean; 
    public isExpanse: boolean; 
    public isRecruitment: boolean; 
    public isSales: boolean;   
}
export class UserViewModel {
    constructor(UserModel?: User[], totalCount?: number) {
        this.userModel = new Array<User>();
        this.totalCount = totalCount;

        this.bandList = this.bandList;
        this.groupList = this.groupList;
        this.titleList = this.titleList;
        this.managerList = this.managerList;
        this.roleList = this.roleList;
        this.designationList = this.designationList;

    }
    public totalCount: number;
    public userModel: User[];
    public managerId: string;
    public bandList: DropDownList[];
    public groupList: DropDownList[];
    public titleList: DropDownList[];
    public managerList: DropDownList[];
    public roleList: DropDownList[];
    public designationList: DropDownList[];
}

