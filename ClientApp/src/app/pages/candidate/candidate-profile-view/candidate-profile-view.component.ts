import { Component, OnInit, ViewChild, trigger, transition, animate, style, ViewEncapsulation } from '@angular/core';
import { CandidatePersonalDetailComponent } from '../candidate-personal-detail/candidate-personal-detail.component';
import { CandidateWorkexperienceDetailComponent } from '../candidate-workexperience-detail/candidate-workexperience-detail.component';
import { CandidateEducationalDetailComponent } from '../candidate-educational-detail/candidate-educational-detail.component';
import { AuthService } from '../../../services/common/auth.service';

@Component({
  selector: 'candidate-profile-view',
  templateUrl: './candidate-profile-view.component.html',
  styleUrls: ['./candidate-profile-view.component.css'],
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
export class CandidateProfileViewComponent implements OnInit {

  public isCandidateProfilePic:boolean=false;

  @ViewChild("personalEditor")
  personalEditor: CandidatePersonalDetailComponent
  @ViewChild("workexperienceEditor")
  workexperienceEditor: CandidateWorkexperienceDetailComponent
  @ViewChild("educationalEditor")
  educationalEditor: CandidateEducationalDetailComponent

  constructor(private authService: AuthService) { }

  ngOnInit() {
    
  }
  get fullName(): string {
    return this.authService.currentUser ? this.authService.currentUser.fullName : "";
  }
}
