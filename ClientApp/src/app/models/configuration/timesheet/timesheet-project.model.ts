import { DropDownList } from "../../common/dropdown";

export class ProjectModel {
    constructor(id?: string, projectName?: string, startDate?: Date, endDate?: Date, description?: string, status?: string, clientId?: string, managerId?: string) {

        this.id = id;
        this.projectName = projectName;
        this.startDate = startDate;
        this.endDate = endDate;
        this.description = description;
        this.status = status;
        this.clientId = clientId;
        this.managerId = managerId;
    }
    public id: string;
    public projectName: string;
    public startDate: Date;
    public endDate: Date;
    public description: string;
    public status: string;
    public clientId: string;
    public managerId: string;
}

export class ProjectViewModel {

    constructor(projectList?:ProjectModel[],totalCount?:number,){

        this.projectList=new Array<ProjectModel>();
        this.totalCount=totalCount;
    }
    public projectList: ProjectModel[];
    public clientList: DropDownList[];
    public managerList: DropDownList[];
    public totalCount: number;
}
