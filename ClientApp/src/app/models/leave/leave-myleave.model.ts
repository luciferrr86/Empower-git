import { DropDownList } from "../common/dropdown";

export class MyLeave{
    constructor (ddlleaveType? :DropDownList[] )
    {
        this.ddlleaveType = ddlleaveType;
    }
    public ddlleaveType: DropDownList[];
    public isSet : boolean;
   
}