export class Band{
    constructor(id?:string,name?:string,isActive?:boolean,inUsed?:boolean){

        this.id=id;
        this.name=name;

    }

    public id:string;
    public name:string;
}
export class BandModel{
    constructor(bandModel?: Band[], totalCount?: number) {
        this.bandModel= new Array<Band>();
         this.totalCount = totalCount;
        
     }
     public totalCount: number;
     public bandModel: Band[];
}
