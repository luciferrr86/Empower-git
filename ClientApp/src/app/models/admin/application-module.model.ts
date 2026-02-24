export class ApplicationModule {   
    constructor() {
        this.applicationModuleDetail=new Array<ApplicationModuleDetail>();
        
    }   
    public  id : string;
    public  moduleName : string;
    public  isActive : boolean;
    public applicationModuleDetail:ApplicationModuleDetail[];
}

export class ApplicationModuleDetail
{
    public  Id : string;
    public  SubModuleName : string;
    public  IsActive : boolean;
}
