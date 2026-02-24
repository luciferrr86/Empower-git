import { EmployeeDetail } from "../common/emp-detail.model";

export class ReviewGoalViewModel {

    constructor(employeeDetailList?: EmployeeDetail[], totalCount?: number) {

        this.employeeDetailList = new Array<EmployeeDetail>();
        this.totalCount = totalCount;
    }

    public employeeDetailList: EmployeeDetail[];
    public totalCount: number;
}
