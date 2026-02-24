export class CategoryModel {
    constructor(id?: string, name?: string, subCategoryList?: SubCategoryModel[]) {

        this.id = id;
        this.name = name;
        //this.subCategoryList = new Array<SubCategoryModel>();
    }
    public id: string;
    public name: string;
    public subCategoryList: SubCategoryModel[];
}

export class SubCategoryModel {
    constructor(id?: string, name?: string) {

        this.id = id;
        this.name = name;
    }
    public id: string;
    public name: string
}

export class ExpenseBookingViewModel {

    constructor(categoryModel?: CategoryModel[], categoryCount?: number) {

        this.categoryModel = new Array<CategoryModel>();
        this.categoryCount = categoryCount;
    }

    public categoryModel: CategoryModel[];
    public categoryCount: number;
}