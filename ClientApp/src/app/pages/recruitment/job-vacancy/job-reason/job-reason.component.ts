import { Component, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { DropDownList } from '../../../../models/common/dropdown';
import { VacancyService } from '../../../../services/recruitment/vacancy.service';
import { AlertService } from '../../../../services/common/alert.service';
import { ManageDirectoryComponent } from '../directory/directory.component';

@Component({
  selector: 'app-job-reason',
  templateUrl: './job-reason.component.html',
  styleUrls: ['./job-reason.component.css']
})


export class JobReasonComponent implements OnInit {
  public isJobAvailable = false;
  id: string;
  public job: FormGroup;
  jdReason: any;
  isSaving: boolean;
  emailArray: any[];
  showEmail: DropDownList[];
  selectedEmail: any[];
  emailObjects: DropDownList[];
  showFlyout = false
  dir: any;
  @ViewChild('manageDir')
  manageDir: ManageDirectoryComponent;


  constructor(private _fb: FormBuilder, private route: ActivatedRoute, private router: Router, private alertService: AlertService, private vacancyService: VacancyService) { }



  ngOnInit() {
    this.id = this.route.snapshot.paramMap.get('id');
    this.job = this._fb.group({
      reason: ['', Validators.required],
    });
    this.getEmailDirectoryList();
    this.getJobReasoDetail();
    if (this.showFlyout === true) {
      this.getEmailDirectoryList();
    }
  }
  getJobReasoDetail() {
    this.vacancyService.getJDReasonVacancy(this.id).subscribe(data => {
      this.job.patchValue({
        reason: data['reason']
      });
    }, error => {
      console.log(error);
      this.alertService.showMessage("No reason available");
    }
    );
  }
  parentFun() { this.getEmailDirectoryList(); }

  saveReason() {
    
    this.isSaving = true;
    this.jdReason = this.job.controls['reason'].value;
    this.vacancyService.updateReason(this.jdReason, this.id).subscribe(() => {
      this.isSaving = false;
      this.alertService.showSucessMessage("Updated SuccessFully");

    },
      error => {
        console.log(error);
        this.isSaving = false;
        this.alertService.showMessage("Error Is Saving Data");
      }
    );
  }
  sendEmails() {
    this.isSaving = true;
    console.log(this.selectedEmail);
    this.vacancyService.sendJDMail(this.selectedEmail, this.id).subscribe((data) => {
      
      if (data['item1'] === true) {
        console.log(data);
        this.isSaving = false;
        this.alertService.showSucessMessage("Mail Sent Successfully");

      } else {
        this.alertService.showMessage("Error in sending mails");
      }
    }, error => {
      this.isSaving = false;
      console.log(error);
      this.alertService.showMessage("Error in sending mails");
    });

  }
  addDirectory() {

    this.dir = this.manageDir.newDir();
  }

  getEmailDirectoryList() {
    
    this.vacancyService.getEmailDirectoryList().subscribe(data => {
      this.emailObjects = data;
    });
  }

}
