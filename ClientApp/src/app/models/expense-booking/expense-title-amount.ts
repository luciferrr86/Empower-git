import { DropDownList } from "../common/dropdown";

export class ExpenseTitleAmountViewModel {
    constructor(categoryList?: ExpenseTitleAmountModel[], totalCount?: number) {
        this.expenseBookingTitleModel = new Array<ExpenseTitleAmountModel>();
        this.totalCount = totalCount;
    }
    public expenseBookingTitleModel: ExpenseTitleAmountModel[];
    public totalCount: number;
    public titleList: DropDownList[];
}

export class ExpenseTitleAmountModel {
    public id: string;
    public amount: string;
    public titleId: string;
}