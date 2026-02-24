export class LeaveCalender {

    constructor(title?: string, start?: Date, end?: Date, allDay?: boolean, backgroundColor?: string, borderColor?: string, tooltip?: string, status?: string) {

        this.title = title;
        this.start = start;
        this.end = end;
        this.allDay = allDay;
        this.backgroundColor = backgroundColor;
        this.borderColor = borderColor;
        this.tooltip = tooltip;
        this.status = status;

    }

    public title: string;
    public start: Date;
    public end: Date;
    public allDay: boolean;
    public backgroundColor: string;
    public borderColor: string;
    public tooltip: string;
    public status: string;
}