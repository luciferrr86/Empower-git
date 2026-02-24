import { Component, OnInit, ViewChild } from '@angular/core';
import { ClientModel } from '../../../../models/configuration/timesheet/timesheet-client.model';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { TimesheetClientService } from '../../../../services/configuration/timesheet/timesheet-client.service';
import { AlertService } from '../../../../services/common/alert.service';
import { Utilities } from '../../../../services/common/utilities';

@Component({
  selector: 'timesheet-client-info',
  templateUrl: './timesheet-client-info.component.html',
  styleUrls: ['./timesheet-client-info.component.css']
})
export class TimesheetClientInfoComponent implements OnInit {

  public isSaving = false;
  private isNew = false;
  public modalTitle = "";
  public client: ClientModel = new ClientModel();
  public serverCallback: () => void;

  @ViewChild('editorModal')
  editorModal: ModalDirective;
  constructor(private clientSerivice: TimesheetClientService, private alertService: AlertService) { }

  ngOnInit() {
  }
  createClient() {
    this.client = new ClientModel();
    this.editorModal.show();
    this.isNew = true;
    this.modalTitle = "Add";
    return this.client;
  }
  updateClient(client: ClientModel) {
    this.isNew = false;
    this.editorModal.show();
    this.modalTitle = "Edit";
    if (client) {
      this.client = new ClientModel;
      (<any>Object).assign(this.client, client);
      return this.client;
    }
  }
  public save() {
    this.isSaving = true;
    if (this.isNew) {
      this.clientSerivice.create(this.client).subscribe(sucess => this.saveSuccessHelper(sucess), error => this.saveFailedHelper(error));
    }
    else {
      this.clientSerivice.update(this.client, this.client.id).subscribe(sucess => this.saveSuccessHelper(sucess), error => this.saveFailedHelper(error));
    }
  }

  private saveSuccessHelper(result?: string) {
    this.isSaving = false;
    if (this.isNew) {
      this.alertService.showSucessMessage("Saved successfully");
    } else {
      this.alertService.showSucessMessage("Updated successfully");
    }
    this.editorModal.hide();
    this.serverCallback();
  }

  private saveFailedHelper(error: any) {
    this.isSaving = false;
    let test = Utilities.getHttpResponseMessage(error);
    this.alertService.showInfoMessage(test[0]);
  }
}
