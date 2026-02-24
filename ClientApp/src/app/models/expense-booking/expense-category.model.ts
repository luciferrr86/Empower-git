export class ExpenseCategoryViewModel {
    constructor(categoryList?: ExpenseCategoryModel[], totalCount?: number) {
        this.categoryList = new Array<ExpenseCategoryModel>();
        this.totalCount = totalCount;
    }
    public categoryList: ExpenseCategoryModel[];
    public totalCount: number;
}

export class ExpenseCategoryModel {
    public id: string;
    public name: string;
}