export class ProfileDetail {
    constructor(pictureId?:string,imageUrl?:string,userId?:string)
    {
        this.pictureId=pictureId;
        this.imageUrl=imageUrl; 
        this.userId=userId;   
    }
    public userId:string;
    public pictureId:string;
    public imageUrl:string;
}
