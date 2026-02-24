export class LeaveType{
    constructor(id? :string ,name?:string , colorCode? :string,leavePeriodId? : string,inUsed? :Boolean){
        this.id = id ;
        this.name = name;
        this.leavePeriodId = leavePeriodId;
    }

    public id : string ;
    public name :string;
    public leavePeriodId  :string;
    public inUsed : Boolean;
}

export class LeaveTypeModel{
    constructor (leaveTypeModel?: LeaveType[] ,totalCount? :number){
        this.leaveTypeModel = new Array<LeaveType>();
        this.totalCount = totalCount;
      }
    public leaveTypeModel : LeaveType[];
    public totalCount : number;
}