export class FunctionalDesignation {

    constructor(id?: string, name?: string) {
        this.id = id;
        this.name = name;

    }

    public id: string;
    public name: string;
}
export class FunctionalDesignationModel {

    constructor(functionalDesignationModel?: FunctionalDesignation[], totalCount?: number) {
       this.functionalDesignationModel= new Array<FunctionalDesignation>();
        this.totalCount = totalCount;
       
    }
    public totalCount: number;
    public functionalDesignationModel: FunctionalDesignation[];
}
