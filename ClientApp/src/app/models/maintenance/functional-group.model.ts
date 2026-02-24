import { DropDownList } from "../common/dropdown";

export class FunctionalGroup{
    constructor(id?:string,departmentId?:string,name?:string,isActive?:boolean,department?:string){
        
        this.id=id;
        this.name = name;
        this.departmentId = departmentId;
    }
    public id:string;
    public name:string;
    public departmentId:string;


}
export class FunctionalGroupModel{
    constructor(functionalGroupModel?: FunctionalGroup[], totalCount?: number) {
        this.functionalGroupModel= new Array<FunctionalGroup>();
         this.totalCount = totalCount;
        
     }
     public totalCount: number;
     public functionalGroupModel: FunctionalGroup[];
     public departmentList:DropDownList[];
}