import { NgFor } from '@angular/common';
import { Component, EventEmitter, Input, OnInit, Output, ViewChild} from '@angular/core';
import { NgForm } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { EmailDirectory } from '../../../../models/common/emailDirectory';
import { AlertService } from '../../../../services/common/alert.service';
import { Utilities } from '../../../../services/common/utilities';
import { VacancyService } from '../../../../services/recruitment/vacancy.service';



@Component({
  selector: 'directory',
  templateUrl: './directory.component.html',
  styleUrls: ['./directory.component.css']
})
export class ManageDirectoryComponent implements OnInit {
  @ViewChild('editorModal')
  editorModal: ModalDirective;

  // @Input() dirId: string;
  private isNew = false;
  public isSaving = false;
  dir: EmailDirectory = new EmailDirectory();
  @Output() parentFun: EventEmitter<any> = new EventEmitter();
  public serverCallback: () => void;
  modalTitle: string;
  constructor(private alertService: AlertService, private vacancyService: VacancyService, private router: Router) { }
  ngOnInit() { }


  newDir() {
    
    this.modalTitle = "Add"
    this.editorModal.show();
    this.isNew = true;
    this.dir = new EmailDirectory();
    return this.dir;
  }

  editDir(editDir) {
    
    this.modalTitle = "Edit"
    this.editorModal.show();
    if (editDir) {
      this.isNew = false;
      this.dir = editDir;
      return this.dir
    }
  }

  save() {
    
    console.log(this.dir);
    var formdata = this.dir;
    this.isSaving = true;
    
    if (this.dir.id) {
      this.vacancyService.updateEmailDirectory(formdata)
        .subscribe(sucess => this.saveSuccessHelper(sucess), error => this.saveFailedHelper(error));
         }
    else {
      
      this.vacancyService.addEmailDirectory(formdata)
        .subscribe(sucess => this.saveSuccessHelper(sucess), error => this.saveFailedHelper(error));

    }
    
  }

  private saveSuccessHelper(result: any) {

    this.isSaving = false;
    if (this.isNew) {
      this.alertService.showSucessMessage("Saved Successfully");
    } else {
      this.alertService.showSucessMessage("Updated Successfully");
    }
    this.editorModal.hide();
    this.parentFun.emit();
    this.serverCallback();
  }
  private saveFailedHelper(error: any) {
    console.log(error);
    this.isSaving = false;
    let test = Utilities.getHttpResponseMessage(error);
    this.alertService.showInfoMessage(test[0]);
  }
}
