import { DropDownList } from "../../common/dropdown";

export class TimesheetAssignProject {
   
    public id: string;
    public projectId: string;
    public employeelist :EmployeeList[];
}
export class TimesheetAssignProjectViewModel {
    constructor(EmployeeList?: EmployeeListModel[], totalCount?: number) {

        this.employeeList = new Array<EmployeeListModel>();
        this.totalCount = totalCount;
    }
    public projectList: DropDownList[];
    public employeeList: EmployeeListModel[];
    public allemployee :EmployeeList[]
    public totalCount :number
}
export class EmployeeListViewModel
{
    constructor(EmployeeList?: EmployeeListModel[], totalCount?: number) {

        this.employeeList = new Array<EmployeeListModel>();
        this.totalCount = totalCount;
    }
  
    public employeeList: EmployeeListModel[];
    public totalCount :number    
}
export class EmployeeListModel {
    public employeeId: string;
    public fullName: string;
    public designation: string;
    public projectId : string;
    public templateId :string;
    public isConfig :boolean;
}
export class EmployeeList {
    public employeeId: string;
}

