export class LeaveDetails{
    constructor(startDate?:Date,endDate?:Date,leaveType?:string,reasonForApply?:string,noOfDays?:string,managerName?:string,managerComment?:string,managerId?:string)
    {
       this.startDate = startDate;
       this.endDate = endDate,
       this.leaveType = leaveType,
       this.noOfDays = noOfDays,
       this.reasonForApply = reasonForApply,
       this.managerName = managerName,
       this.managerComment = managerComment
       this.managerId  = managerId
    }

    public startDate :Date;
    public endDate:Date;
    public leaveType:string;
    public reasonForApply:string;
    public noOfDays:string;
    public managerName:string;
    public managerComment:string;
    public managerId:string;
}