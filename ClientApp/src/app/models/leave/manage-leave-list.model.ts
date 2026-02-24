export class ManageLeaveList{
    constructor(leaveDeatilId?:string,startDate?:string,endDate?:string,status?:string,noOfDays?:string,employeeName?:string)
    {
        this.leaveDeatilId = leaveDeatilId;
        this.startDate = startDate;
        this.endDate = endDate;
        this.status = status;
        this.noOfDays = noOfDays;
        this.employeeName = employeeName;
    }
    public employeeName  :string;
    public leaveDeatilId :string;
    public startDate :string;
    public endDate :string;
    public status :string;
    public noOfDays :string;

}


export class SubordinateLeaveListModel{
    constructor(subordinateLeaveListModel? : ManageLeaveList[] ,totalCount? :number){
        this.subordinateLeaveListModel = new Array<ManageLeaveList>();
        this.totalCount = totalCount;

    }
    public subordinateLeaveListModel : ManageLeaveList[];
    public totalCount :number;
}