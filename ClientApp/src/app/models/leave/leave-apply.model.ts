import { DropDownList } from "../common/dropdown";

export class LeaveApply {
    public id: string;
    public leaveTypeId: string;
    public startDate: Date;
    public endDate: Date;
    public noOfDays: string;
    public reasonForApply: string;
    public leaveType:DropDownList[];
    public userId : string;
}