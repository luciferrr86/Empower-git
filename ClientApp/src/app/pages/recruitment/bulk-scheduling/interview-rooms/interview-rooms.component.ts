import { Component, OnInit, ViewChild } from '@angular/core';
import { InterViewRooms } from '../../../../models/recruitment/bulk-scheduling/interview-rooms.model';
import { InterviewRoomsEditorComponent } from '../interview-rooms-editor/interview-rooms-editor.component';
import { AlertService } from '../../../../services/common/alert.service';
import { BulkInterviewScheduleService } from '../../../../services/recruitment/bulk-interview-schedule.service';

@Component({
  selector: 'interview-rooms',
  templateUrl: './interview-rooms.component.html',
  styleUrls: ['./interview-rooms.component.css']
})
export class InterviewRoomsComponent implements OnInit {

  public editedRoom: InterViewRooms = new InterViewRooms();
  public roomList: InterViewRooms[];

  @ViewChild('roomEditor')
  roomEditor: InterviewRoomsEditorComponent;
  constructor(private alertService: AlertService, private bulkInterviewScheduleService: BulkInterviewScheduleService) { }



  ngOnInit() {
    this.getRooms();
  }
  ngAfterViewInit() {
    this.roomEditor.serverCallback = () => {
      this.getRooms();
    };

  }
  newRoom() {
    this.editedRoom = this.roomEditor.newRooms();
  }
  getRooms() {
    this.bulkInterviewScheduleService.getRooms().subscribe(result => this.onSuccessfulDataLoad(result), error => this.onDataLoadFailed(error));
  }
  onSuccessfulDataLoad(interviewRomms: InterViewRooms[]) {
    this.roomList = interviewRomms;
  }

  onDataLoadFailed(error: any) {
    this.alertService.showInfoMessage("Unable to retrieve list from the server");
  }
}
