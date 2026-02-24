import { DropDownList } from "../common/dropdown";

export class ExpenseSubCategoryViewModel {
    constructor(categoryList?: ExpenseSubCategoryModel[], totalCount?: number) {
        this.subCategoryList = new Array<ExpenseSubCategoryModel>();
        this.totalCount = totalCount;
    }
    public subCategoryList: ExpenseSubCategoryModel[];
    public totalCount: number;
    public categoryList: DropDownList[];
}

export class ExpenseSubCategoryModel {
    public id: string;
    public name: string;
    public categoryId: string;
}