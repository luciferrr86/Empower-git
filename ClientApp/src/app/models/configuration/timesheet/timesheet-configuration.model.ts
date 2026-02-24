export class TimesheetConfiguration {
    constructor(id?:string,timeSheetFrequency?:any,totalCount?:number){

        this.id=id;
        this.timeSheetFrequency=timeSheetFrequency;
    }
    public id: string;
    public timeSheetFrequency:any;
}