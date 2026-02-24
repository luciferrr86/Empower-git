import { DropDownList } from "../../common/dropdown";

export class LeaveRules{
    constructor(id?:string,name?:string,colorCode?:string,leavesPerYear?:string,leavePeriodId?:string,leaveTypeId?:string,bandId?:string){
        this.id = id;
        this.name = name;
        this.leavesPerYear = leavesPerYear;
        this.leaveTypeId = leaveTypeId;
        this.bandId = bandId;
    }

    public id : string ;
    public name :string;
    public leavesPerYear :string;
    public leavePeriodId :string;
    public leaveTypeId :string;
    public bandId :string;
    public bandList :DropDownList[];
    public leaveTypeList : DropDownList[];
}
export class LeaveRulesModel{
    constructor(leaveRulesModel? :LeaveRules[],totalCount? :number){
        this.leaveRulesModel = new Array<LeaveRules>();
        this.totalCount = totalCount;
    }

    public leaveRulesModel : LeaveRules[];
    public totalCount : number;
}