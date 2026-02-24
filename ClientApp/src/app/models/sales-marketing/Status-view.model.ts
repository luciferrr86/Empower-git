import { DropDownList } from "../common/dropdown";

export class StatusModel {
    constructor(dailyCallModel?: DailyCall[], totalCount?: number, ddlSaleStatus?) {
        this.dailyCallModel = new Array<DailyCall>();
        this.totalCount = totalCount;
        this.statusCompanyModel=new StatusCompanyModel();
    }
    public dailyCallModel: DailyCall[];
    public listStatus: StatusCompany[];
    public ddlSaleStatus: DropDownList[];
    public statusCompanyModel: StatusCompanyModel;
    public totalCount: number;
}
export class StatusCompanyModel {
    public companyId: string;
    public companyName: string;
    public companyAddress: string;
    public companyTelePhoneNo: string;
    public emailId:string;

}
export class StatusCompany {
    public id: string;
    public salesCompanyId: string;
    public callDateTime: string;
    public salesStatusId:string;
    public description: string;

}

export class DailyCall {
    public id: string;
    public callDateTime: Date;
    public description: string;
    public salesCompanyId: string;
}