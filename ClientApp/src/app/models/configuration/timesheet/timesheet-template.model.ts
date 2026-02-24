import { DropDownList } from "../../common/dropdown";

export class TimesheetTemplateModel {
    constructor(id?: string, templateName?: string, startDate?: Date, endDate?: Date, timesheetConfigurationId?: string, monday?: boolean, tuesday?: boolean, wednesday?: boolean, thursday?: boolean, friday?: boolean, saturday?: boolean, sunday?: boolean) {

        this.id = id;
        this.templateName = templateName;
        this.startDate = startDate;
        this.endDate = endDate;
        this.timesheetConfigurationId = timesheetConfigurationId;
        this.monday = monday;
        this.tuesday = tuesday;
        this.wednesday = wednesday;
        this.thursday = thursday;
        this.friday = friday;
        this.saturday = saturday;
        this.sunday = sunday;
    }
    public id: string;
    public templateName: string;
    public startDate: Date;
    public endDate: Date;
    public timesheetConfigurationId: string;
    public monday: boolean;
    public tuesday: boolean;
    public wednesday: boolean;
    public thursday: boolean;
    public friday: boolean;
    public saturday: boolean;
    public sunday: boolean;
    public selectedDays:string[];
}
export class TimesheetTemplateViewModel {
    constructor(timesheetTemplateList?: TimesheetTemplateModel[], totalCount?: number) {

        this.timesheetTemplateList = new Array<TimesheetTemplateModel>();
        this.totalCount = totalCount;
    }
    public timesheetTemplateList: TimesheetTemplateModel[];
    public timesheetConfigurationList: DropDownList[];
    public totalCount: number;
}