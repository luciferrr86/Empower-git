

export class EmployeeLeaveList{
    constructor(leaveDeatilId?:string,leaveType?:string,startDate?:string,endDate?:string,status?:string,isSubmitted?:boolean,isSave?:boolean,iscancelPeriodEnd?:boolean,){
       this.leaveType = leaveType;
       this.startDate = startDate;
       this.endDate = endDate;
       this.status = status;
       this.leaveDeatilId = leaveDeatilId;
       this.isSubmitted = isSubmitted;
       this.isSave = isSave;
       this.iscancelPeriodEnd = iscancelPeriodEnd;
    }
    public leaveDeatilId :string;
    public leaveType :string;
    public startDate :string;
    public endDate :string;
    public status :string;
    public isSubmitted : boolean;
    public isSave :boolean;
    public iscancelPeriodEnd : boolean;
}


export class EmployeeLeaveListModel{
    constructor (employeeLeaveListModel?: EmployeeLeaveList[] ,totalCount? :number){
        this.employeeLeaveListModel = new Array<EmployeeLeaveList>();
        this.totalCount = totalCount;
      }
    public employeeLeaveListModel : EmployeeLeaveList[];
    public totalCount : number;
}

