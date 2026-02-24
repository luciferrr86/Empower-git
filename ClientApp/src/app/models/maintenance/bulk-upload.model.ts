export class BulkUpload {

    constructor(id?: string,employeeid?: string, fullname?: string,workemailaddress?: string,
        functionaldepartment?: string,functionalgroup?: string, designation?: string,personalemailid?: string, 
        title?: string,reportingheademailid?: string,rollaccess?: string,location?: string, dateofjoining?: string,
        reportingheadname?: string, band?: string,status?: string, errormessage?: string)
         {
        this.id = id;
        this.employeeid = employeeid;
        this.fullname = fullname;
        this.workemailaddress = workemailaddress;
        this.functionaldepartment = functionaldepartment;
        this.functionalgroup = functionalgroup;
        this.designation = designation;
        this.personalemailid = personalemailid;
        this.title = title;     
        this.reportingheademailid = reportingheademailid;
        this.rollaccess = rollaccess;
        this.location = location;
        this.dateofjoining = dateofjoining;
        this.reportingheadname = reportingheadname;
        this.band = band;
        this.status = status;
        this.errormessage = errormessage;

    }

    public id: string;    
    public employeeid: string;
    public fullname: string; 
    public workemailaddress: string;
    public functionaldepartment: string; 
    public functionalgroup: string;
    public designation: string;   
    public personalemailid: string;
    public title: string; 
    public reportingheademailid: string;
    public rollaccess: string; 
    public location: string;
    public dateofjoining: string; 
    public reportingheadname: string;
    public band: string; 
    public status: string;
    public errormessage: string; 
        
}
export class BulkUploadModel {

    constructor(bulkUploadModel?: BulkUpload[], totalCount?: number) {
       this.bulkUploadModel= new Array<BulkUpload>();
        this.totalCount = totalCount;
       
    }
    public totalCount: number;
    public bulkUploadModel: BulkUpload[];
}

