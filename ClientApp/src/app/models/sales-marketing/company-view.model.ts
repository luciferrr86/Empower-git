export class CompanyViewModel
{
    constructor(listCompany?:Company[],totalCount? :number){
        this.listCompany = new Array<Company>();
        this.totalCount = totalCount;
       }
       public listCompany : Company[];
       public totalCount : number;
}
export class Company
{
    public id: string;
    public comapnyName : string;
    public companyAddress : string;
    public city :string;
    public state :string;
    public country :string;
    public zipCode :string;
    public emailId :string;
    public telephone :string;

    public lstCompanyContacts : CompanyContacts[];
}

export class CompanyContacts
{
    public id: string;
    public firstName : string;
    public lastName :string;
    public mobileNo :string;
    public telephone :string;
    public designation :string;
    public emailId :string;
    public salesCompanyId :string;
}
