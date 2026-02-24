import { DropDownList } from "../../common/dropdown";

export class InterViewPanel {

    constructor(date?: string, startTime?: string, endTime?: string) {
        this.date = date;
        this.startTime = startTime;
        this.endTime = endTime;
    }
    public date: string;
    public startTime: string;
    public endTime: string;
    public jobVacancyList: DropDownList[];
    public mangerList: DropDownList[];
    public dateList: DropDownList[];
    public panleModel: InterviewPanalSchedule[];
}
export class InterviewPanalSchedule {
    constructor(id?: string, date?: string, startTime?: string, endTime?: string) {

        this.id = id;
        this.interViewDate = date;
        this.startTime = startTime;
        this.endTime = endTime;
    }
    public id: string;
    public name:string;
    public breakStartTime:string;
    public breakEndTime:string;
    public interViewDate: string;
    public startTime: string;
    public endTime: string;
    public vacancyId: string;
    public vacancy: string;
    public interViewDateId: string;
    public managerIdList: string[];

}