export class HrView {
    constructor() {
        this.lstManager = new Array<hrViewList>();
        this.lstEmployee = new Array<hrViewList>();

    }
    public isPerformanceStart: boolean;
    public isConfigurationSet: boolean;
    public isMidYearEnabled: boolean; 
    public isMidYearReviewCompleted: boolean;
    public lstManager: hrViewList[];
    public lstEmployee: hrViewList[];
    public totalCount: number;
}


export class hrViewList {

    public id: string;
    public name: string;
    public designation: string;
    public group: string;
    public status: string;
}


