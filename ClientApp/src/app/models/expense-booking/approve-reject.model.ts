export class ApproveRejectModel {
    constructor(comment?: string, buttonType?: string) {

        this.comment = comment;
        this.buttonType = buttonType;
    }
    public id:string;
    public comment: string;
    public buttonType: string;
}