import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { Utilities } from '../../../../services/common/utilities';
import { AlertService } from '../../../../services/common/alert.service';
import { BulkInterviewScheduleService } from '../../../../services/recruitment/bulk-interview-schedule.service';
@Component({
  selector: 'automatic-schedule',
  templateUrl: './automatic-schedule.component.html',
  styleUrls: ['./automatic-schedule.component.css']
})
export class AutomaticScheduleComponent implements OnInit {
  massId = "";
  constructor(private router: Router, private route: ActivatedRoute,private alertService: AlertService, private bulkInterviewScheduleService: BulkInterviewScheduleService) { 
    this.route.queryParams.subscribe(params => {
      if (params['id'] != undefined) {
        this.massId = params['id'];
      }
      else {
        this.router.navigate(['/bulk-scheduling']);
      }
    });
  }

  ngOnInit() {
  }
  scheduleInterView(){
    if (this.massId != "") {
      this.bulkInterviewScheduleService.scheduleInterView(this.massId).subscribe(sucess => this.saveSuccessHelper(sucess), error => this.saveFailedHelper(error));
    }
    else {
      this.alertService.showInfoMessage("Please select date first");
    }

    
  }

  private saveSuccessHelper(result?: string) {

    this.alertService.showSucessMessage("Saved successfully");

}


private saveFailedHelper(error: any) {
  let test = Utilities.getHttpResponseMessage(error);
  this.alertService.showInfoMessage("Please try later" + test[0]);

}
}
