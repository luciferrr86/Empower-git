import { IOption } from "ng-select";

export class LeavePeriod {

    constructor(id?: string, name?: string, periodStart?: Date, periodEnd?: Date, isLeavePeriodCompleted?: boolean, isEdit?: boolean) {

        this.id = id;
        this.name = name;
        this.periodStart = periodStart;
        this.periodEnd = periodEnd;
        this.isLeavePeriodCompleted = isLeavePeriodCompleted;
        this.isEdit = isEdit;
    }
    public id: string;
    public name: string;
    public periodStart: Date;
    public periodEnd: Date;
    public isLeavePeriodCompleted: boolean;
    public isEdit : boolean;
}
export class LeavePeriodModel {

    constructor(leavePeriodModel?: LeavePeriod[], totalCount?: number) {

        this.leavePeriodModel = new Array<LeavePeriod>();
        this.totalCount = totalCount;

    }

    public leavePeriodModel: LeavePeriod[];
    public totalCount: number;
    
}