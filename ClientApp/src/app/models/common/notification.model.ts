import { Utilities } from "../../services/common/utilities";

export class Notification {

    public static Create(data: {}) {
        let n = new Notification();
      (<any>Object).assign(n, data);

        if (n.date)
            n.date = Utilities.parseDate(n.date);

        return n;
    }


    public id: number;
    public header: string;
    public body: string;
    public isRead: boolean;
    public isPinned: boolean;
    public date: Date;
}
