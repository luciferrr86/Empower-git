export class FunctionalTitle{
    constructor(id?:string,name?:string,isActive?:boolean,inUsed?:boolean){
        
        this.id=id;
        this.name = name;
        this.isActive = isActive;
        this.inUsed= inUsed;
    }
    public id:string;
    public name:string;
    public isActive: boolean;
    public inUsed: boolean;
}
export class FunctionalTitleModel{
    constructor(functionalTitleModel?: FunctionalTitle[], totalCount?: number) {
        this.functionalTitleModel= new Array<FunctionalTitle>();
         this.totalCount = totalCount;
        
     }
     public totalCount: number;
     public functionalTitleModel: FunctionalTitle[];
}