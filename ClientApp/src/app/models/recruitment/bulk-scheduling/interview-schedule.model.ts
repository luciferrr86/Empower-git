export class InterviewScheduling {

    public startDate: Date;
    public endDate: Date;
    public venue: string;
    public address: string;
    public listTime: SelectTime[];

}

export class SelectTime {

    constructor(id?:string, date?: Date, startTime?: string, endTime?: string, duration?: string) {
        
        this.date = date;
        this.startTime = startTime;
        this.endTime = endTime;
        this.duration = duration;
    }
    public id:string;
    public date: Date;
    public startTime: string;
    public endTime: string;
    public duration: string;
}

export class SelectTimeModel{
    constructor(datTimeList?:SelectTime[]) {
        
        this.datTimeList = new Array<SelectTime>();
    
    }

    public datTimeList:SelectTime[];
}