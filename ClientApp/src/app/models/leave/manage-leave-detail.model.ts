export class ManageLeaveDetail{
    constructor(startDate?:Date,endDate?:Date,leaveType?:string,reasonForApply?:string,noOfDays?:string,managerName?:string,managerComment?:string,managerId?:string,status?:string,buttonType?:string,approvedby?:string,view?:boolean)
    {
       this.startDate = startDate;
       this.endDate = endDate,
       this.leaveType = leaveType,
       this.noOfDays = noOfDays,
       this.reasonForApply = reasonForApply,
       this.managerName = managerName,
       this.managerComment = managerComment
       this.managerId  = managerId
       this.status = status;
       this.buttonType = buttonType;
       this.approvedby = approvedby;
       this.view = view
    }

    public startDate :Date;
    public endDate:Date;
    public leaveType:string;
    public reasonForApply:string;
    public noOfDays:string;
    public managerName:string;
    public managerComment:string;
    public managerId:string;
    public status :string;
    public buttonType  : string;
    public approvedby :string;
    public view :boolean;
    }
