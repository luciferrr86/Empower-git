import { CtcOtherComponent } from "./ctc-other-component.model";

export class SalaryComponentModel {
    id: number;
    name: string;
    isEarnings: boolean;
    isMonthly: boolean;
    isAllYearly: boolean;
    isActive: boolean;
    ctcOtherComponent: CtcOtherComponent[];
}
