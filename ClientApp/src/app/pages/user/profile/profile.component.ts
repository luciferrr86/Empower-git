import { Component, OnInit, ViewChild, ViewEncapsulation } from '@angular/core';
import { Http } from '@angular/http';
import { animate, style, transition, trigger } from '@angular/animations';
import '../../../../assets/charts/echart/echarts-all.js';
import { AuthService } from '../../../services/common/auth.service';
import { ProfileDetail } from '../../../models/profile/profile.model';
import { PersonalDetail } from '../../../models/maintenance/personal-detail.model';
import { ProfessionalDetail } from '../../../models/maintenance/professional-detail.model';
import { EducationlDetail } from '../../../models/maintenance/educationl-detail.model';
import { PersonalDetailComponent } from '../personal-detail/personal-detail.component';
import { ProfessionalDetailComponent } from '../professional-detail/professional-detail.component';
import { EducationalDetailComponent } from '../educational-detail/educational-detail.component';
import { ProfileService } from '../../../services/maintenance/profile.service';
import { AlertService } from '../../../services/common/alert.service';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css'],
  encapsulation: ViewEncapsulation.None,
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
export class ProfileComponent implements OnInit {

  editProfile = true;
  editProfileIcon = 'icofont-edit';
  public isSaving = false;
  private isNew = false;
  public profileEdit: ProfileDetail = new ProfileDetail();
  public personalEdit: PersonalDetail = new PersonalDetail();
  public professionalEdit: ProfessionalDetail = new ProfessionalDetail();
  public educationalEdit: EducationlDetail = new EducationlDetail();
  public serverCallback: () => void;

  @ViewChild('personalEditor')
  personalEditor: PersonalDetailComponent;
  @ViewChild('professionalEditor')
  professionalEditor: ProfessionalDetailComponent
  @ViewChild('educationalEditor')
  educationalEditor: EducationalDetailComponent

  public isAdminProfilePic: boolean = false;

  constructor(public http: Http, private authService: AuthService, private profileService: ProfileService, private alertService: AlertService) {
  }

  ngOnInit() {
    this.profileEdit.userId = this.authService.currentUser.id;
    this.getProfilePic();
  }
  private getProfilePic() {
    this.profileService.getProfilePic(this.authService.currentUser.id).subscribe(result => this.onSuccessfulDataLoad(result), error => this.onDataLoadFailed(error));
  }
  get fullName(): string {
    return this.authService.currentUser ? this.authService.currentUser.fullName : "";
  }
  get role(): string {
    return this.authService.currentUser ? this.authService.currentUser.roles[0] : "";
  }
  refreshImages(status: ProfileDetail) {
    if (status) {
      this.profileEdit.pictureId = status.pictureId;
      this.profileEdit.imageUrl = status.imageUrl;
    }
  }

  onSuccessfulDataLoad(profile: ProfileDetail) {
    (<any>Object).assign(this.profileEdit, profile);
  }

  onDataLoadFailed(error: any) {
    this.alertService.showInfoMessage("Please Try Again");
  }
}
