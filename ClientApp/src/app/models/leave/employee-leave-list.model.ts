export class SubordinateLeaveList{
    constructor(leaveDeatilId?:string,employeeName?:string,startDate?:Date,endDate?:Date,status?:string,noOfDays?:string){
        this.leaveDeatilId = leaveDeatilId;
        this.employeeName = employeeName;
        this.noOfDays = noOfDays;
        this.startDate = startDate;
        this.endDate = endDate;
        this.status = status
    }    
    public leaveDeatilId:string;
    public employeeName:string;
    public noOfDays:string;
    public startDate:Date;
    public endDate:Date;
    public status:string;

} 

export class SubordinateLeaveListModel {
    constructor(SubordinateLeaveListModel?:SubordinateLeaveList[],totalCount? :number){
     this.SubordinateLeaveListModel = new Array<SubordinateLeaveList>();
     this.totalCount = totalCount;
    }

    public SubordinateLeaveListModel : SubordinateLeaveList[];
    public totalCount : number;
}
