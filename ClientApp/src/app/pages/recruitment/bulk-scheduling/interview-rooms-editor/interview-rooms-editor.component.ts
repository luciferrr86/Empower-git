import { Component, OnInit, ViewChild } from '@angular/core';
import { InterViewRooms } from '../../../../models/recruitment/bulk-scheduling/interview-rooms.model';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { BulkInterviewScheduleService } from '../../../../services/recruitment/bulk-interview-schedule.service';
import { AlertService } from '../../../../services/common/alert.service';
import { Utilities } from '../../../../services/common/utilities';

@Component({
  selector: 'interview-rooms-editor',
  templateUrl: './interview-rooms-editor.component.html',
  styleUrls: ['./interview-rooms-editor.component.css']
})
export class InterviewRoomsEditorComponent implements OnInit {

  public isSaving = false;
  public modalTitle: string;
  public roomEdit: InterViewRooms = new InterViewRooms();
  public serverCallback: () => void;

  @ViewChild('editorModal')
  editorModal: ModalDirective;
  constructor(private bulkInterviewScheduleService: BulkInterviewScheduleService, private alertService: AlertService) { }

  ngOnInit() {
  }
  newRooms() {
    this.roomEdit = new InterViewRooms();
    this.editorModal.show();
    this.modalTitle = "Add";
    return this.roomEdit;
  }
  save() {
    this.bulkInterviewScheduleService.saveRooms(this.roomEdit).subscribe(sucess => this.saveSuccessHelper(sucess), error => this.saveFailedHelper(error));
  }
  private saveSuccessHelper(result?: string) {
    this.isSaving = false;
    this.alertService.showSucessMessage("Saved successfully");
    this.serverCallback();
    this.editorModal.hide();
  }

  private saveFailedHelper(error: any) {
    this.isSaving = false;
    let test = Utilities.getHttpResponseMessage(error);
    console.log(test);
    this.alertService.showInfoMessage("Please try later" + test[0]);
  }
}
