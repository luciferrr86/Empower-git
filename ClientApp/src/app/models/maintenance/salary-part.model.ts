import { SalaryComponentModel } from "./salary-component.model";
import { CtcOtherComponent } from "./ctc-other-component.model";

export class SalaryPart {
    id: number;
    amount: number;
    employeeSalaryId: number;
    ctcOtherComponentId: number;
    //ctcOtherComponent: CtcOtherComponent;
}
