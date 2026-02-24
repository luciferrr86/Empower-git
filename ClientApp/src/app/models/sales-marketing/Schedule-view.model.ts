import { StatusCompanyModel } from "./Status-view.model";
import { DropDownList } from "../common/dropdown";

export class ScheduleModel
{
    constructor(totalCount? :number){       
        this.totalCount = totalCount;
        this.statusCompanyModel=new StatusCompanyModel();
       }
     
       public lstSchedule : ScheduleCompany[];       
       public totalCount : number;
       public statusCompanyModel: StatusCompanyModel;
}

export class ScheduleCompany
{
    public id: string;
    public fileId:string;
    public isChecked:string;
    public clientMail:boolean;
    public  companyId:string;
    public clientPerson:string[];
    public internalPerson:string[]; 
    public writer : string;   
    public subject : string;
    public description : string;
    public mettingDate : string;
    public agenda : string;
    public venue : string;
    public selectInternalPerson: DropDownList[];
    public selectClientPerson: DropDownList[];
    
}
export class MomMeetingDetail
{
    public meetingId: string;
    public internalPerson:string[]; 
    public nextActionInternalPerson:string[]; 
    public momDescription : string;
    public writer : string; 
    public mettingDate:string  
    public subject : string;
    public agenda : string;
    public venue : string;
    public nextActionDescription:string;
    public nextActionDueDate : string;
    public nextActionStatus : string;
    public clientPerson:string[];
    public selectClientPerson: DropDownList[];
    public selectInternalPerson: DropDownList[];
}

export class MomMeeting
{
    public id: string;
    public internalPerson:string[]; 
    public description : string;
    public dueDate : string;
    public status : string;
}