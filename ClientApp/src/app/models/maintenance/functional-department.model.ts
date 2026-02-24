export class FunctionalDepartment {

    constructor(id?: string,name?: string) {

        this.id = id;
        this.name = name;

    }

    public id: string;
    public name: string;
}
export class FunctionalDepartmentModel{
    constructor(functionalDepartmentModel?: FunctionalDepartment[], totalCount?: number) {
        this.functionalDepartmentModel= new Array<FunctionalDepartment>();
         this.totalCount = totalCount;
        
     }
     public totalCount: number;
     public functionalDepartmentModel: FunctionalDepartment[];
}