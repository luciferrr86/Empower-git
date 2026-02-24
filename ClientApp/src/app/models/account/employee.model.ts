import { FunctionalDepartment } from "../maintenance/functional-department.model";
import { FunctionalDesignation } from "../maintenance/functional-designation.model";
import { FunctionalGroup } from "../maintenance/functional-group.model";
import { FunctionalTitle } from "../maintenance/functional-title.model";
import { Band } from "../maintenance/band.model";

export class Employee {

    constructor(id?: string, emailId?: string, doj?: Date, password?: string, confirmPassword?: string, department?: FunctionalDepartment[], designation?: FunctionalDesignation[], title?: FunctionalTitle[], group?: FunctionalGroup[], band?: Band[]) {

        this.emailId = emailId;
        this.password = password;
        this.confirmPassword = confirmPassword;
        this.doj = doj;
        this.department = department;
        this.designation = designation;
        this.title = title;
        this.group = group;
        this.band = band;

    }
    public id: string;
    public firstName: string;
    public lastName: string;
    public emailId: string;
    public doj: Date;
    public password: string;
    public confirmPassword: string;
    public department: FunctionalDepartment[];
    public designation: FunctionalDesignation[];
    public group: FunctionalGroup[];
    public title: FunctionalTitle[];
    public band: Band[];
}