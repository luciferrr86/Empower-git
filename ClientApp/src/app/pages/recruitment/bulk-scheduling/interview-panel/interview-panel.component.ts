import { Component, OnInit, Input, ViewChild } from '@angular/core';
import { InterViewPanel, InterviewPanalSchedule } from '../../../../models/recruitment/bulk-scheduling/interview-panel.model';
import { InterviewPanelEditorComponent } from '../interview-panel-editor/interview-panel-editor.component';
import { DropDownList } from '../../../../models/common/dropdown';
import { AlertService } from '../../../../services/common/alert.service';
import { BulkInterviewScheduleService } from '../../../../services/recruitment/bulk-interview-schedule.service';
import { Router, ActivatedRoute } from '@angular/router';

@Component({
  selector: 'interview-panel',
  templateUrl: './interview-panel.component.html',
  styleUrls: ['./interview-panel.component.css']
})
export class InterviewPanelComponent implements OnInit {

  massId = "";
  public editedInterview: InterViewPanel = new InterViewPanel();
  public panalInterviewList: InterviewPanalSchedule[];
  public panalInterview: InterviewPanalSchedule = new InterviewPanalSchedule();
  public listPanel: InterviewPanalSchedule[];
  jobVacancyList: DropDownList[];
  managerList: DropDownList[];
  dateList: DropDownList[];

  @ViewChild('interviewEditor')
  interviewEditor: InterviewPanelEditorComponent;
  constructor(private router: Router, private route: ActivatedRoute, private alertService: AlertService, private bulkInterviewScheduleService: BulkInterviewScheduleService) {
    this.route.queryParams.subscribe(params => {
      if (params['id'] != undefined) {
        this.massId = params['id'];
        this.getInterviewPanal(params['id']);
      }
      else {
        this.router.navigate(['/bulk-scheduling']);
      }
    });
  }

  ngOnInit() {
    if (this.massId != "") {
      this.getInterviewPanal(this.massId);
    }
    else {
      this.alertService.showInfoMessage("Please select date first");
    }

  }
  ngAfterViewInit() {
    this.interviewEditor.serverCallback = () => {
      this.getInterviewPanal(this.massId);
    };

  }
  newInterviewer() {
    this.panalInterview = this.interviewEditor.newInterviewers(this.jobVacancyList, this.managerList, this.dateList);
  }
  getInterviewPanal(id: string) {
    this.bulkInterviewScheduleService.getInterviewPanal(id).subscribe(result => this.onSuccessfulDataLoad(result), error => this.onDataLoadFailed(error));
  }
  onSuccessfulDataLoad(interviewPanal: InterViewPanel) {
    this.jobVacancyList = interviewPanal.jobVacancyList;
    this.managerList = interviewPanal.mangerList;
    this.dateList = interviewPanal.dateList;
    this.listPanel = interviewPanal.panleModel;
  }

  onDataLoadFailed(error: any) {
    this.alertService.showInfoMessage("Unable to retrieve list from the server");
  }
}
