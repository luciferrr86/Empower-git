import { SalaryComponentModel } from "./salary-component.model";

export class CtcOtherComponent {
    id: number;
    isActive: boolean;
    amount: number;
    employeeCtcId: number;
    salaryComponentId: number;
    salaryComponent: SalaryComponentModel;
}
