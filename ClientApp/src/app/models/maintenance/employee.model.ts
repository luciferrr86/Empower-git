import { FunctionalDepartment } from "./functional-department.model";
import { FunctionalDesignation } from "./functional-designation.model";
import { FunctionalGroup } from "./functional-group.model";
import { FunctionalTitle } from "./functional-title.model";
import { Band } from "./band.model";


export class Employee{

    constructor(id?:string,firstName?:string,lastName?:string,emailId?:string,doj?:Date,password?:string,confirmPassword?:string,department?:FunctionalDepartment[],designation?:FunctionalDesignation[],title?:FunctionalTitle[],group?:FunctionalGroup[],band?:Band[]){

        this.id=id;
        this.firstName=firstName;
        this.lastName=lastName;
        this.emailId=emailId;
        this.password=password;
        this.confirmPassword=confirmPassword;
        this.doj=doj;
        this.department=department;
        this.designation=designation;
        this.title=title;
        this.group=group;
        this.band=band;

    }

    public id:string;
    public firstName:string;
    public lastName:string;
    public emailId:string;
    public doj:Date;
    public password:string;
    public confirmPassword:string;
    public department:FunctionalDepartment[];
    public designation:FunctionalDesignation[];
    public group:FunctionalGroup[];
    public title:FunctionalTitle[];
    public band:Band[];

}
export class EmployeeModel{
    constructor(EmployeeModel?: Employee[], totalCount?: number) {
        this.EmployeeModel= new Array<Employee>();
         this.totalCount = totalCount;
        
     }
     public totalCount: number;
     public EmployeeModel: Employee[];
}