import { DropDownList } from "../common/dropdown";

export class SubcategoryViewModel {
constructor(subCategoryList?:SubCategoryItemModel[],totalCount?:number){

    this.subCategoryItemList=new Array<SubCategoryItemModel>();
    this.totalCount=totalCount;
}
    public subCategoryItemList: SubCategoryItemModel[];
    public categoryList: DropDownList[];
    public totalCount: number;
}

export class SubCategoryItemModel {
    constructor(id?: string, name?: string,categoryId?:string, subCategoryId?: string,subCategory?: string, category?: string) {

        this.id = id;
        this.name = name;
        this.subCategoryId = subCategoryId;
        this.categoryId=categoryId;
        this.subCategory = subCategory;
        this.category = category;
    }
    public id: string;
    public name: string;
    public subCategory: string;
    public category: string;
    public subCategoryId: string;
    public categoryId: string;
    public suCategoryList: DropDownList[];
}