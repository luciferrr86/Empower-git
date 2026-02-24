export class LeaveHrView {
    constructor(employeeId?: string, employeeName?: string, department?: string, remainingLeave?: string) {

        this.employeeId = employeeId;
        this.employeeName = employeeName;
        this.department = department;
        this.remainingLeave = remainingLeave;

    }
    public employeeId: string;
    public employeeName: string;
    public department: string;
    public remainingLeave: string;
}
export class EmployeeLeaveDetails {
    constructor(leaveType?: string, allottedLeaves?: string, takenLeaves?: string, remainingLeave?: string) {

        this.leaveType = leaveType;
        this.allottedLeaves = allottedLeaves;
        this.takenLeaves = takenLeaves;
        this.remainingLeave = remainingLeave;
    }
    public leaveType: string;
    public allottedLeaves: string;
    public takenLeaves: string;
    public remainingLeave: string;
}
export class LeaveHrViewModel {

    constructor(leaveEmployeeList: LeaveHrView[], totalCount: number) {

        this.leaveEmployeeList = new Array<LeaveHrView>();
        this.totalCount = totalCount;
    }

    public leaveEmployeeList: LeaveHrView[];
    public totalCount: number;
    public isConfigSet: boolean;
}