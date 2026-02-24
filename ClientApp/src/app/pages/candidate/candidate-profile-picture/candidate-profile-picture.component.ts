import { Component, OnInit, Input } from '@angular/core';
import { AuthService } from '../../../services/common/auth.service';
import { CandidateService } from '../../../services/candidate/candidate.service';
import { AlertService } from '../../../services/common/alert.service';
import { ProfileDetail } from '../../../models/profile/profile.model';

@Component({
  selector: 'candidate-profile-picture',
  templateUrl: './candidate-profile-picture.component.html',
  styleUrls: ['./candidate-profile-picture.component.css']
})
export class CandidateProfilePictureComponent implements OnInit {

  public profileEdit: ProfileDetail = new ProfileDetail();
  constructor(private authService: AuthService, private profileService: CandidateService, private alertService: AlertService) { }
  public image: string;

  @Input() isCandidatePic:boolean;

  ngOnInit() {
    this.profileEdit.userId = this.authService.currentUser.id;
    this.getProfilePic();
  }
  refreshImages(status: ProfileDetail) {
    if (status) {
      this.profileEdit.pictureId = status.pictureId;
      this.profileEdit.imageUrl = status.imageUrl;
    }
  }
  private getProfilePic() {
    this.profileService.getProfilePic(this.authService.currentUser.id).subscribe(result => this.onSuccessfulDataLoad(result), error => this.onDataLoadFailed(error));
  }
  onSuccessfulDataLoad(profile: ProfileDetail) {
    Object.assign(this.profileEdit, profile);
  }

  onDataLoadFailed(error: any) {
    this.alertService.showInfoMessage("Unable to retrieve data from the server");
  }
}
