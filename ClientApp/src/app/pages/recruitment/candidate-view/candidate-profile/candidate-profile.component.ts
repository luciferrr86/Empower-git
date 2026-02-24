import { Component, OnInit, trigger, transition, style, animate } from '@angular/core';
import { ActivatedRoute, Router } from "@angular/router";
import { AlertService } from '../../../../services/common/alert.service';
import { CandidateService } from '../../../../services/candidate/candidate.service';
import { JobCandidateProfile } from '../../../../models/candidate/candidate-personal.model';
import { AccountService } from '../../../../services/account/account.service';
import { JobQualification } from '../../../../models/candidate/candidate-educational.model';
import { JobWorkExperience } from '../../../../models/candidate/candidate-workexperience.model';

@Component({
  selector: 'app-candidate-profile',
  templateUrl: './candidate-profile.component.html',
  styleUrls: ['./candidate-profile.component.css'],
  animations: [
    trigger('fadeInOutTranslate', [
      transition(':enter', [
        style({ opacity: 0 }),
        animate('400ms ease-in-out', style({ opacity: 1 }))
      ]),
      transition(':leave', [
        style({ transform: 'translate(0)' }),
        animate('400ms ease-in-out', style({ opacity: 0 }))
      ])
    ])
  ]
})
export class CandidateProfileComponent implements OnInit {
  public personalEdit: JobCandidateProfile = new JobCandidateProfile();
  public educationalEdit: JobQualification = new JobQualification();
  public professionalEdit: JobWorkExperience = new JobWorkExperience();
  constructor(private router: Router, private accountService: AccountService, private profileService: CandidateService, private route: ActivatedRoute, private alertService: AlertService) {
    this.route.queryParams.subscribe(params => {
      if (params['id'] != undefined) {
        this.getPersonalDetail(params['id']);
        this.getQualification(params['id']);
        this.getProfessional(params['id']);

      } else {
        this.router.navigate(['/candidate-list']);
      }
    });
  }

  ngOnInit() {
  }
  private getPersonalDetail(id) {

    this.profileService.getPersonal(id).subscribe(result => this.onSuccessfulProfileDataLoad(result), error => this.onDataLoadFailed(error));
  }
  onSuccessfulProfileDataLoad(personal: JobCandidateProfile) {
    (<any>Object).assign(this.personalEdit, personal);
  }

  private getQualification(id) {
    this.profileService.getQualification(id).subscribe(result => this.onSuccessfulEducationDataLoad(result), error => this.onDataLoadFailed(error));
  }
  onSuccessfulEducationDataLoad(educationl: JobQualification) {
    (<any>Object).assign(this.educationalEdit, educationl);


  }

  private getProfessional(id) {
    this.profileService.getprofessional(id).subscribe(result => this.onSuccessfulProfessionalDataLoad(result), error => this.onDataLoadFailed(error));
  }
  onSuccessfulProfessionalDataLoad(professional: JobWorkExperience) {

    (<any>Object).assign(this.professionalEdit, professional);
  }

  onDataLoadFailed(error: any) {
    this.alertService.showInfoMessage("Please Try Again");
  }

}
