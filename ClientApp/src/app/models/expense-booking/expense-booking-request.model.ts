import { DropDownList } from "../common/dropdown";

export class ExpenseBookingRequestViewModel {
    constructor(expenseBookingListModel?: ExpenseBookingListModel[], expenseBookingCount?: number) {
        this.expenseBookingListModel = new Array<ExpenseBookingListModel>();
        this.expenseBookingCount = expenseBookingCount;
    }
    public expenseBookingListModel: ExpenseBookingListModel[];
    public expenseBookingCount: number;
}
export class ExpenseBookingListModel {

    constructor(id?: string, expensePeriod?: string, amount?: string, department?: string, subCategoryItem?: string
        , remarks?: string, status?: string, file?: string) {

        this.id = id;
        this.expensePeriod = expensePeriod;
        this.amount = amount;
        this.department = department;
        this.subCategoryItem = subCategoryItem;
        this.remarks = remarks;
       
        this.status = status;
        this.file = file;
    }
    public id: string;
    public expensePeriod: string;
    public amount: string;
    public department: string;
    public subCategoryItem: string;
    public remarks: string;
    public status: string;
    public approvedOrRejectedDate: string;
    public employeName: string;
    public requestedDate: string;
    public file: string;
    public bookingId: string;
    public isInvite:boolean;
}
export class ExpenseBookingModel{
    
    constructor(id?: string, subCategoryItemId?: string, fromDate?: Date, toDate?: Date, amount?: string, departmentId?: string, department?: string, subCategoryItem?: string
        , remarks?: string, status?: string, file?: string[],isSubmitted?:boolean) {

        this.id = id;
        this.fromDate = fromDate;
        this.toDate = toDate;
        this.amount = amount;
        this.department = department;
        this.departmentId = departmentId;
        this.subCategoryItemId = subCategoryItemId;
        this.subCategoryItem = subCategoryItem;
        this.remarks = remarks;
        this.status = status;
        this.file = new Array<string>();
        this.isSubmitted=isSubmitted;
        this.expenseBookingDetail=new Array<ExpenseBookingDetailList>();
        this.expenseDocumentList=new Array<ExpenseDocumentList>();

    }
    public id: string;
    public fromDate: Date;
    public toDate: Date;
    public amount: string;
    public departmentId: string;
    public department: string;
    public subCategoryItem: string;
    public subCategoryItemId: string;
    public subCategory: string;
    public category: string;
    public remarks: string;
    public status: string;
    public file: string[];
    public isSubmitted:boolean;
    public subCategoryItems:DropDownList[];
    public departmentList:DropDownList[];
    public expenseBookingDetail:ExpenseBookingDetailList[];
    public expenseDocumentList:ExpenseDocumentList[];
}

export class ExpenseBookingModelDetail{
    
    constructor(id?: string, subCategoryItemId?: string, fromDate?: Date, toDate?: Date, amount?: string, departmentId?: string, department?: string, subCategoryItem?: string
        , remarks?: string, status?: string, file?: string,isSubmitted?:boolean) {

        this.id = id;
        this.fromDate = fromDate;
        this.toDate = toDate;
        this.amount = amount;
        this.department = department;
        this.departmentId = departmentId;
        this.subCategoryItemId = subCategoryItemId;
        this.subCategoryItem = subCategoryItem;
        this.remarks = remarks;
        this.status = status;
        this.file = file;
        this.isSubmitted=isSubmitted;
        this.expenseBookingDetail=new Array<ExpenseBookingDetailList>();
        this.expenseDocumentList=new Array<ExpenseDocumentList>();

    }
    public id: string;
    public fromDate: Date;
    public toDate: Date;
    public amount: string;
    public departmentId: string;
    public department: string;
    public subCategoryItem: string;
    public subCategoryItemId: string;
    public subCategory: string;
    public category: string;
    public remarks: string;
    public status: string;
    public file: string;
    public isSubmitted:boolean;
    public isInviteApproved:boolean;
    public approverId:string;
    public selectedApprover:string[];
    public subCategoryItems:DropDownList[];
    public departmentList:DropDownList[];
    public inviteEmployeeList:DropDownList[];
    public expenseBookingDetail:ExpenseBookingDetailList[];
    public expenseDocumentList:ExpenseDocumentList[];
}
// export class ExpenseBookingDetailList{
//     public id:string;
//     public employeeComment:string;
//     public managerComment:string;
// }
export class ExpenseBookingDetailList{
    constructor() {

        this.expenseBookingDetailApproverList=new Array<ExpenseBookingDetail>();
        this.expenseBookingIviteApproverList=new Array<ExpenseBookingIviteApprover>();

    }
    public id:string;
    public Level:number;
    public Status:string;
    public name:string;
    public expenseBookingDetailApproverList:ExpenseBookingDetail[];
    public expenseBookingIviteApproverList:ExpenseBookingIviteApprover[];
}
export class ExpenseBookingIviteApprover{
    constructor() {
        this.expenseBookingDetailIviteApproverList=new Array<ExpenseBookingDetail>();

    }
    public id:string;
    public Status:string;
    public name:string;
    public expenseBookingDetailIviteApproverList:ExpenseBookingDetail[];

}
export class ExpenseBookingDetail{
    public id:string;
    public employeeComment:string;
    public managerComment:string;
}

export class ExpenseDocumentList{
    public expenseDocumentId:string;
    public fileUrl:string;
    public fileName:string;
}


export class ExpenseBookingExcel{
    
    public name: string;
    public amount: string;
    public requestDate: string;
    public approvedDate: string;
    public department: string;
    public bookingId: string;
    public expensePeriod: string;
}
export class ExpenseBookingExcelViewModel{
    constructor() {
        this.expenseBookingExcel = new Array<ExpenseBookingExcel>();
    }
    public expenseBookingExcel: ExpenseBookingExcel[];
}