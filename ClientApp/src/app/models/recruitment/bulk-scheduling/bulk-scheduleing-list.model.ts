export class BulkScheduleingList {
    constructor(id?: string, fromDate?: string, toDate?: string, venue?: string) {

        this.id = id;
        this.fromDate = fromDate;
        this.toDate = toDate;
        this.venue = venue;

    }
    public id: string;
    public fromDate: string;
    public toDate: string;
    public venue: string;
}
export class BulkScheduleingListModel {

    constructor(massSchedulingList?: BulkScheduleingList[], totalCount?: number) {

        this.massSchedulingList = new Array<BulkScheduleingList>();
        this.totalCount = totalCount

    }

    public massSchedulingList: BulkScheduleingList[];
    public totalCount: number;
}