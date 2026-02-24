import { Component, OnInit, ViewChild } from '@angular/core';
import { EmployeeLeaveDetails } from '../../../../models/leave/leave-hr-view.model';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { HrLeaveService } from '../../../../services/leave/hr-leave.service';
import { AlertService } from '../../../../services/common/alert.service';

@Component({
  selector: 'employee-leave-info',
  templateUrl: './employee-leave-info.component.html',
  styleUrls: ['./employee-leave-info.component.css']
})
export class EmployeeLeaveInfoComponent implements OnInit {

  leavedetails: EmployeeLeaveDetails[];

  @ViewChild('editorModal')
  editorModal: ModalDirective;
  constructor(private hrViewService: HrLeaveService, private alertService: AlertService) { }

  ngOnInit() {
  }

  leaveDetails(employeeId: string) {
    this.hrViewService.getLeaveDetails(employeeId).subscribe(result => this.onSuccessfulDataLoad(result), error => this.onDataLoadFailed(error));
    this.editorModal.show();
  }
  onSuccessfulDataLoad(leave: EmployeeLeaveDetails[]) {
    this.leavedetails = leave;
  }

  onDataLoadFailed(error: any) {
    this.alertService.showInfoMessage("Unable to retrieve list from the server");
  }
}
