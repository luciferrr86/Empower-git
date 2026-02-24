import { Component, OnInit, Input } from '@angular/core';
import { ProfileDetail } from '../../../models/profile/profile.model';
import { AuthService } from '../../../services/common/auth.service';
import { CandidateService } from '../../../services/candidate/candidate.service';
import { AlertService } from '../../../services/common/alert.service';

@Component({
  selector: 'app-candidate-resume',
  templateUrl: './candidate-resume.component.html',
  styleUrls: ['./candidate-resume.component.css']
})
export class CandidateResumeComponent implements OnInit {

  public profileEdit: ProfileDetail = new ProfileDetail();
  constructor(private authService: AuthService, private profileService: CandidateService, private alertService: AlertService) { }
  public image: string;

  @Input() isCandidatePic: boolean;

  ngOnInit() {
    this.profileEdit.userId = this.authService.currentUser.id;
    this.getCandidateResume();
  }
  refreshImages(status: ProfileDetail) {
    if (status) {
      this.profileEdit.pictureId = status.pictureId;
      this.profileEdit.imageUrl = status.imageUrl;
    }
  }
  private getCandidateResume() {
    this.profileService.getResume(this.authService.currentUser.id).subscribe(result => this.onSuccessfulDataLoad(result), error => this.onDataLoadFailed(error));
  }
  onSuccessfulDataLoad(profile: ProfileDetail) {
    this.profileEdit.imageUrl = profile.imageUrl;
    Object.assign(this.profileEdit, profile);
  }

  onDataLoadFailed(error: any) {
    this.alertService.showInfoMessage("Unable to retrieve data from the server");
  }

}
