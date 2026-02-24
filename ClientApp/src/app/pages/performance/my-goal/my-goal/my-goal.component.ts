import { Component, OnInit } from '@angular/core';
import { AlertService } from '../../../../services/common/alert.service';
import { MyGoalService } from '../../../../services/performance/my-goal/my-goal.service';
import { AccountService } from '../../../../services/account/account.service';


@Component({
  selector: 'app-my-goal',
  templateUrl: './my-goal.component.html',
  styleUrls: ['./my-goal.component.css']
})
export class MyGoalComponent implements OnInit {
  public isPerformanceStarted: boolean;
  public isManagerReleased: boolean;
  public chkManagerRelease: any;
  constructor(private myGoalService: MyGoalService, private alertService: AlertService, private accountService: AccountService) { }

  ngOnInit() {
    this.chkPerformanceStart();
  }

  chkPerformanceStart() {
    this.myGoalService.chkPerformanceStart(this.accountService.currentUser.id).subscribe(res => this.onLoadSuccess(res), error => this.OnLoadFailure(error))
  }
  onLoadSuccess(res: any) {
    this.isPerformanceStarted = res.isPerformanceStarted;
    this.isManagerReleased = res.isManagerReleased

  }

  OnLoadFailure(err: any) {
    this.alertService.showInfoMessage("Please Try Again");
  }
}
