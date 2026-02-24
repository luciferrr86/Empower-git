import { Component, OnInit, Input } from '@angular/core';
import { ProfileDetail } from '../../../models/profile/profile.model';
import { AuthService } from '../../../services/common/auth.service';
import { ProfileService } from '../../../services/maintenance/profile.service';
import { AlertService } from '../../../services/common/alert.service';

@Component({
  selector: 'profile-picture',
  templateUrl: './profile-picture.component.html',
  styleUrls: ['./profile-picture.component.css']
})
export class ProfilePictureComponent implements OnInit {
  public profileEdit: ProfileDetail = new ProfileDetail();
  constructor(private authService: AuthService, private profileService: ProfileService, private alertService: AlertService) { }
  public image: string;

  @Input() isAdminPic:boolean;

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
