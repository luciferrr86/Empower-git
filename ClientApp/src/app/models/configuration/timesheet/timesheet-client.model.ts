export class ClientModel {
   
    constructor(id?: string, name?: string, emailId?: string, address?: string, contact?: string) {
        this.id = id;
        this.name = name;
        this.emailId = emailId;
        this.contact = contact;
    }
    public id: string;
    public name: string;
    public emailId: string;
    public address: string;
    public contact: string;
}
export class ClientViewModel {
    constructor(clientList?: ClientModel[], totalCount?: number) {
        this.clientList = new Array<ClientModel>();
        this.totalCount = totalCount;
    }
    public clientList: ClientModel[];
    public totalCount: number;
}
