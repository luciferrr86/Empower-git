export class InterViewRooms{

    constructor(id?: string, roomName?: string, startTime?: string, endTime?: string, duration?: string) {

        this.roomName = roomName;
        this.startTime = startTime;
        this.endTime = endTime;
        this.duration = duration;
    }
    public id: string;
    public roomName: string;
    public startTime: string;
    public endTime: string;
    public duration: string;
    
}