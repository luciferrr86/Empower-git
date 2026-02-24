export class WorkingDay {
    constructor(id? : string , workingDay?: string ,workingDayValue? :string,leavePeriodId? :string ){
        this.id = id;
        this.workingDay = workingDay;
        this.workingDayValue = workingDayValue;
        this.leavePeriodId = leavePeriodId;
    }

    public id:string;
    public workingDay :string;
    public workingDayValue :string;
    public leavePeriodId :string;
}

export class WorkingDayModel{
     constructor (workingDayModel?: WorkingDay[] ,totalCount? :number){
       this.workingdayModel = new Array<WorkingDay>();
       this.totalCount = totalCount;
     }
     public  workingdayModel : WorkingDay[];
     public totalCount : number;

}
