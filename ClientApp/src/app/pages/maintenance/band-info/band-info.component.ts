import { Component, OnInit, ViewChild } from '@angular/core';
import { Band } from '../../../models/maintenance/band.model';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { BandService } from '../../../services/maintenance/band.service';
import { AlertService } from '../../../services/common/alert.service';
import { Utilities } from '../../../services/common/utilities';
import { FormGroup } from '@angular/forms';

@Component({
  selector: 'band-info',
  templateUrl: './band-info.component.html',
  styleUrls: ['./band-info.component.css']
})
export class BandInfoComponent implements OnInit {

  form: FormGroup;
  public isSaving = false;
  private isNew = false;
  public modalTitle = "";
  public bandEdit: Band = new Band();
  public serverCallback: () => void;

  @ViewChild('editorModal')
  editorModal: ModalDirective;

  constructor(private bandService: BandService, private alertService: AlertService) { }

  ngOnInit() {

  }
  public addBand() {
    this.modalTitle = "Add ";
    this.editorModal.show();
    this.isNew = true;
    this.bandEdit = new Band();
    return this.bandEdit;
  }

  updateBand(band: Band) {
    this.modalTitle = "Edit "
    this.editorModal.show();
    this.isNew = false;
    if (band) {
      this.bandEdit = new Band();
      (<any>Object).assign(this.bandEdit, band);
      return this.bandEdit;
    }
  }

  public save() {
    this.isSaving = true;
    if (this.isNew) {
      this.bandService.create(this.bandEdit).subscribe(sucess => this.saveSuccessHelper(), error => this.saveFailedHelper(error));
    }
    else {
      this.bandService.update(this.bandEdit, this.bandEdit.id).subscribe(sucess => this.saveSuccessHelper(), error => this.saveFailedHelper(error));
    }
  }

  private saveSuccessHelper() {
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
