import { Component, OnInit, ViewChild } from '@angular/core';
import { FunctionalTitle } from '../../../models/maintenance/functional-title.model';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { AlertService } from '../../../services/common/alert.service';
import { TitleService } from '../../../services/maintenance/title.service';
import { Utilities } from '../../../services/common/utilities';

@Component({
  selector: 'functional-title-info',
  templateUrl: './functional-title-info.component.html',
  styleUrls: ['./functional-title-info.component.css']
})
export class FunctionalTitleInfoComponent implements OnInit {

  public isSaving = false;
  private isNew = false;
  public modalTitle = "";
  public titleEdit: FunctionalTitle = new FunctionalTitle();
  public serverCallback: () => void;

  @ViewChild('editorModal')
  editorModal: ModalDirective;

  constructor(private FunctionalTitleService: TitleService, private alertService: AlertService) { }

  ngOnInit() {
  }
  public newTitles() {
    this.modalTitle = "Add";
    this.editorModal.show();
    this.isNew = true;
    this.titleEdit = new FunctionalTitle();
    return this.titleEdit;
  }

  updateTitle(title: FunctionalTitle) {
    this.modalTitle = "Edit"
    this.editorModal.show();
    this.isNew = false;
    if (title) {
      this.titleEdit = new FunctionalTitle();
      (<any>Object).assign(this.titleEdit, title);
      return this.titleEdit;
    }
  }

  public save() {
    this.isSaving = true;
    if (this.isNew) {
      this.FunctionalTitleService.create(this.titleEdit).subscribe(sucess => this.saveSuccessHelper(), error => this.saveFailedHelper(error));
    }
    else {
      this.FunctionalTitleService.update(this.titleEdit, this.titleEdit.id).subscribe(sucess => this.saveSuccessHelper(), error => this.saveFailedHelper(error));
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
