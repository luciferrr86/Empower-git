import { CtcOtherComponent } from "./ctc-other-component.model";
import { EmployeeSalaryModel } from "./employee-salary.model";

export class EmployeeCtcModel {
    id: number;
    ctc: number;
    basicSalary: number;
    da: number;
    hra: number;
    conveyance: number;
    medicalExpenses: number;
    special: number;
    bonus: number;
    total: number;
    employeeId: string;
    ctcOtherComponent: CtcOtherComponent[];
    Employeesalary: EmployeeSalaryModel[];
}
