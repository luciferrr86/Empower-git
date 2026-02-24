export class LeaveInfo{
    constructor (leaveType? :string ,noOfDays?:number,approved?:number,rejected?:number,remaining?:number)
    {
        this.leaveType = leaveType;
        this.noOfDays = noOfDays;
        this.rejected = rejected;
        this.remaining = remaining;
    }
    public leaveType: string;
    public noOfDays : number;
    public approved : number;
    public rejected : number;
    public remaining : number;
}
export class LeaveEntitlementModel{
    constructor (LeaveInfoModel?: LeaveInfo[])
    {
     this.listLeaveInfo = new Array<LeaveInfo>();
    }
    public listLeaveInfo : LeaveInfo[];
    public allLeave :number;
    public approved : number;
    public rejected :number;
    public remaining :number;
}