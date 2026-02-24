import { Component, OnInit, ViewChild } from '@angular/core';
import { LeaveDetails } from '../../../../models/leave/leave-details.model';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { MyLeaveService } from '../../../../services/leave/my-leave.service';
import { AlertService } from '../../../../services/common/alert.service';

@Component({
  selector: 'leave-appllied-detail',
  templateUrl: './leave-appllied-detail.component.html',
  styleUrls: ['./leave-appllied-detail.component.css']
})
export class LeaveApplliedDetailComponent implements OnInit {

  leavedetails: LeaveDetails = new LeaveDetails();
  loadingIndicator: boolean = true;

  @ViewChild('editorModal')
  editorModal: ModalDirective;

  constructor(private myLeaveService: MyLeaveService, private alertService: AlertService) { }

  ngOnInit() {
  }

  leaveDetails(leaveDetailsId: string) {
    this.myLeaveService.getEmployeeLeaveDetail(leaveDetailsId).subscribe(result => this.onSuccessfulDataLoad(result), error => this.onDataLoadFailed(error));

  }


  onSuccessfulDataLoad(leaveDetails: LeaveDetails) {
    this.leavedetails = leaveDetails;
    this.editorModal.show();
  }

  onDataLoadFailed(error: any) {
    this.alertService.showInfoMessage("Unable to retrieve list from the server");
    this.loadingIndicator = false;
  }
}
