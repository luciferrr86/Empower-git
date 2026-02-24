export class Holiday{
constructor(id?: string,name?:string,holidayDate?:Date,leavePeriodId?:string){
    this.id = id;
    this.name = name;
    this.holidayDate = holidayDate;
    this.leavePeriodId = leavePeriodId;
}

    public id :string;
    public name :string;
    public holidayDate :Date;
    public leavePeriodId : string;
}

export class HolidayModel {
    constructor(leaveHolidayListModel?:Holiday[] ,totalCount?: number)
    {
        this.leaveHolidayListModel = new Array<Holiday>();
        this.totalCount = totalCount;
    }

    public leaveHolidayListModel : Holiday[];
    public totalCount: number;
}