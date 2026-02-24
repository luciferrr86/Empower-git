import { DropDownList } from "../common/dropdown";

export class MyTimesheetViewModel
{
    constructor (dayList?:DayList[],projectList?:ProjectList[],TimesheetUserConfig?:TimesheetUserConfig,userSpanId?:string,fullName?:string,type?:string,designation?:string,approverlId?:string
                 ,isUserSaved?:boolean,isUserSubmit?:boolean,isManagerApproved?:boolean,frequency?:string)
    {
        this.dayList = new Array<DayList>();
        this.projectList = new Array<ProjectList>();
        this.TimesheetUserConfig = TimesheetUserConfig;
        this.fullName = fullName;
        this.designation = designation;
        this.type = type;
        this.userSpanId=userSpanId;
        this.approverlId = approverlId;
        this.isUserSaved =isUserSaved;
        this.isUserSubmit =isUserSubmit;
        this.isManagerApproved =isManagerApproved; 
        this.frequency =frequency; 
    }
    public dayList :DayList[];
    public projectList:ProjectList[];
    public lstUserWeeks :DropDownList[];
    public TimesheetUserConfig :TimesheetUserConfig;
    public fullName :string;
    public designation :string;
    public type :string;
    public userSpanId :string;
    public approverlId : string;
    public isUserSaved : boolean;
    public isUserSubmit : boolean;
    public isManagerApproved : boolean;
    public frequency :string;
    public employeeId :string;
    public employeeEmail :string;
    public mangerName :string;
    public mangerEmail :string;
    public totalHour:string;
}

export class DayList{
    constructor(userDetailId?:string,day?:string,date?:Date,userSpanId?:string,totalHour?:string,isUserSaved?:boolean, 
        isUserSubmit?:boolean,isManagerApproved?:boolean,isAllotted?:boolean,isAllow?:boolean)
    {
        this.userDetailId =userDetailId;
        this.day = day;
        this.date = date;
        this.userSpanId = userSpanId;
        this.totalHour =totalHour;
        this.isUserSaved =isUserSaved;
        this.isUserSubmit =isUserSubmit;
        this.isManagerApproved =isManagerApproved;      
        this.isAllotted = isAllotted;
        this.isAllow = isAllow;
    }
    public userDetailId :string;
    public day :string;
    public date :Date;
    public userSpanId :string;
    public totalHour : string;
    public isUserSaved : boolean;
    public isUserSubmit : boolean;
    public isManagerApproved : boolean;
    public isAllotted:boolean;
    public isAllow : boolean;
}

export class ProjectList {
    constructor(projectId?:string,name?:string,projectHourList?:ProjectHour[])
    {
        this.projectId = projectId;
        this.name = name;
        this.projectHourList = new Array<ProjectHour>();
    }
    public projectId : string;
    public name : string;
    public projectHourList :ProjectHour[];
    public totalProjectHour:string;
}


export class ProjectHour
{
    constructor(userDetailProjectHourId?:string,isAllow?:boolean,isAllotted?:boolean,hour?:string)
    {
        this.userDetailProjectHourId = userDetailProjectHourId;
        this.isAllow = isAllow;
        this.isAllotted = isAllotted;
        this.hour = hour;
    }
    public userDetailProjectHourId :string;
    public isAllow : boolean;
    public isAllotted  :boolean;
    public hour : string;
    
}

export class AllottedDays
{
    constructor(isAllotted?:boolean,dayTypeName?:string)
    {
        this.dayTypeName =dayTypeName;
        this.isAllotted = isAllotted;
    }
    public isAllotted :boolean;
    public dayTypeName : string;
}

export class TimesheetUserConfig
{
    constructor (frequency?:string,startDate?:string)
    {
        this.frequency = frequency;
        this.startDate= startDate;
    }
    public frequency :string;
    public startDate :string;
}