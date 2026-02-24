import { DropDownList } from "../../common/dropdown";
import { EmployeeListModel, EmployeeList } from "./timesheet-assign-project.model";

export class TimesheetScheduleModel {
    constructor(id?: string, timesheetTemplateId?: string) {

        this.id = id;
        this.templateId = timesheetTemplateId;
    }
    public id: string;
    public templateId: string;
    public employeelist :EmployeeList[];
}
export class UserScheduleModel {
    constructor(employeeId?: string, fullName?: string, timesheetFrequency?: string,monday?: boolean,tuesday?: boolean,thursday?: boolean,friday?: boolean,saturday?: boolean,sunday?: boolean,wednesday?: boolean) {

        this.employeeId = employeeId;
        this.fullName = fullName;
        this.timesheetFrequency = timesheetFrequency;
        this.monday = monday;
        this.tuesday = tuesday;
        this.wednesday = wednesday;
        this.thursday = thursday;
        this.friday = friday;
        this.saturday = saturday;
        this.sunday = sunday;
    }
  
    public fullName: string;
    public employeeId: string;
    public timesheetFrequency: string;
    public monday: boolean;
    public tuesday: boolean;
    public wednesday: boolean;
    public thursday: boolean;
    public friday: boolean;
    public saturday: boolean;
    public sunday: boolean;
}
export class TimesheetScheduleViewModel {
    public timesheetTemplateList: DropDownList[];
    public employeeList: EmployeeListModel[];
    public userScheduleList : UserScheduleModel[];
    public employeeCount:number;
    public scheduleCount:number;
}