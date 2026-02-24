import { Component, OnInit, ViewChild, Input } from '@angular/core';
import { InterViewSchedule } from '../../../../models/recruitment/candidate-view/interview-schedule.model';
import { JobInterviewService } from '../../../../services/recruitment/job-interview.service';
import { AlertService } from '../../../../services/common/alert.service';
import { InterviewScheduleViewModel } from '../../../../models/recruitment/candidate-view/candidate-application.model';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { DropDownList } from '../../../../models/common/dropdown';
import { Utilities } from '../../../../services/common/utilities';
import { CandidateJobDetails } from '../../../../models/recruitment/manage-interview/shortlisted-candidate-job-detail.model';
import { Router } from '@angular/router';
import { LocalStoreManager } from '../../../../services/common/local-store-manager.service';
import { DBkeys } from '../../../../services/common/db-key';
import { User } from '../../../../models/account/user.model';
import { AccountEndpoint } from '../../../../services/account/account-endpoint.service';
@Component({
  selector: 'interview-schedule-list',
  templateUrl: './interview-schedule-list.component.html',
  styleUrls: ['./interview-schedule-list.component.css']
})
export class InterviewScheduleListComponent implements OnInit {
  public isSaving = false;
  public scheduleEdit: InterViewSchedule = new InterViewSchedule();
  @ViewChild('editorModal')
  editorModal: ModalDirective;
  @ViewChild('editorModalManager')
  editorModalManager: ModalDirective;
  public jobDetail: CandidateJobDetails = new CandidateJobDetails();
  public interviewSchedule: InterViewSchedule = new InterViewSchedule();
  public interviewLevelModel: InterviewScheduleViewModel = new InterviewScheduleViewModel();
  columns: any[] = [];
  filterQuery: string = "";
  pageNumber = 0;
  count = 0;
  pageSize = 10;
  managerList: DropDownList[] = [];
  interviewTypeList: DropDownList[] = [];
  interviewModeList: DropDownList[] = [];
  allInterviewsCompleted: boolean = false;
  public jobId: string;
  rolesAccess: any = ["administrator", "Human Resource"];
  public managerComment: string = "";
  user: User;
  employee: any;
  @Input() appId: string;

  @Input() status: boolean;
  @Input() jobStatus: string;

  public isEdit = false;

  public serverCallback: () => void;
  isHr: boolean;
  showButton: boolean;

  constructor(private jobInterviewService: JobInterviewService, private alertService: AlertService, private router: Router
    , private accountEndpoint: AccountEndpoint, private localStorage: LocalStoreManager) {
    this.user = this.localStorage.getData(DBkeys.CURRENT_USER);
    this.accountEndpoint.getUserEmployeeEndpoint(this.user.id).subscribe(
      result => {
        this.employee = result;
      });
  }

  ngOnInit() {
    this.getInterviewDetail(this.appId);;
  }

  private getInterviewDetail(id) {
    this.jobInterviewService.getInterviewInfo(id).subscribe(result => this.onSuccessfulDataLoad(result), error => this.onDataLoadFailed(error));
  }
  onSuccessfulDataLoad(interviewLeveModel: InterviewScheduleViewModel) {
    const roles: any = this.user.roles;
    if (this.rolesAccess.some(r => r.includes(roles))) {
      this.managerList = interviewLeveModel.managerList;
      this.interviewTypeList = interviewLeveModel.interviewTypeList;
      this.interviewLevelModel = interviewLeveModel;
      if (this.jobStatus !== 'Hired' && this.jobStatus !== 'Rejected') {
        this.allInterviewsCompleted = interviewLeveModel.interviewScheduleLevelList.every(e => e.isLevelCompleted == true);
      }
      this.isHr = true;

    }
    else {
      this.interviewLevelModel = interviewLeveModel;
      this.interviewLevelModel.interviewScheduleLevelList = this.interviewLevelModel.interviewScheduleLevelList.filter(f => f.levelManagerIds.indexOf(this.employee) !== -1);
    }

  }
  onDataLoadFailed(error: any) {
    console.log(error);
    this.alertService.showInfoMessage("Please try again!");
  }

  addSchedule(levelId: string) {

    this.scheduleEdit = new InterViewSchedule();
    //this.scheduleEdit.name = name;
    this.scheduleEdit.levelId = levelId;
    this.editorModal.show();
  }

  save() {
    this.isSaving = true;
    this.scheduleEdit.jobApplicationId = this.appId;
    this.scheduleEdit.jobCandidateUrl = this.router.url;
    this.jobInterviewService.saveinterviewSchedule(this.scheduleEdit).subscribe(sucess => this.saveSuccessHelperSchedule(), error => this.saveFailedHelperSchedule(error));
  }

  private saveSuccessHelperSchedule() {
    this.isSaving = false;
    this.alertService.showSucessMessage("Saved successfully");
    this.getInterviewDetail(this.appId);
    this.editorModal.hide();

  }

  private saveFailedHelperSchedule(error: any) {
    this.isSaving = false;
    let test = Utilities.getHttpResponseMessage(error);
    this.alertService.showInfoMessage("Please try later" + test[0]);
  }
  viewInterview(interViewId: string) {
    if (interViewId != null) {
      this.jobInterviewService.interviewFeedbcak(interViewId).subscribe(sucess => this.viewSuccessHelperSchedule(sucess), error => this.viewFailedHelperSchedule(error));
      this.editorModalManager.show();
    }

  }
  private viewSuccessHelperSchedule(jobinfo: CandidateJobDetails) {
    this.jobDetail = jobinfo
    this.editorModalManager.show();

  }

  private viewFailedHelperSchedule(error: any) {
    this.isSaving = false;
    let test = Utilities.getHttpResponseMessage(error);
    this.alertService.showInfoMessage("Please try later" + test[0]);
  }
  editInterview(edit: InterViewSchedule) {
    this.isEdit = true;
    this.scheduleEdit = new InterViewSchedule();
    this.scheduleEdit.name = edit.name;
    this.scheduleEdit.id = edit.interviewId;
    this.scheduleEdit.levelId = edit.levelId;
    this.scheduleEdit.date = edit.date;
    this.scheduleEdit.jobInterviewTypeId = edit.interviewTypeId.toLowerCase();
    this.scheduleEdit.managerIdList = [edit.managerId];
    this.scheduleEdit.time = edit.time;
    this.scheduleEdit.isLevelCompleted = edit.isLevelCompleted;
    this.scheduleEdit.levelId = edit.levelId;
    this.editorModal.show();
  }
  deleteInterview(interViewId: string) {
    if (interViewId != null) {
      this.alertService.showConfirm("Are you sure you want to delete?", () => this.deleteHelper(interViewId));
    }
  }

  deleteHelper(interViewId: string) {
    this.jobInterviewService.interviewDelete(interViewId)
      .subscribe(() => {
        this.alertService.showInfoMessage("Deleted Successfully.");
        this.getInterviewDetail(this.appId);
      },
        () => {
          this.alertService.showInfoMessage("An error occurred whilst deleting");
        });
  }
  completeLevel(levelId: string, statusId: string) {

    this.jobInterviewService.completeLevel(this.appId, levelId, statusId).subscribe(sucess => this.saveSuccessHelperLevel(), error => this.saveFailedHelperLevel(error));
  }
  fillWeightage(interviewId: string) {
    this.router.navigate(['../recruitment/shortlisted-candidate-detail'], { queryParams: { id: interviewId } });

  }
  private saveSuccessHelperLevel() {
    this.alertService.showSucessMessage("Updated successfully");
    this.getInterviewDetail(this.appId);
  }

  private saveFailedHelperLevel(error: any) {
    let test = Utilities.getHttpResponseMessage(error);
    this.alertService.showInfoMessage(test[0]);
  }

  changeVacancyStatusForCandidate(statusId) {

    this.jobInterviewService.changeVacancyStatusForCandidate(this.appId, statusId).subscribe(sucess => this.saveSuccessHelperStatus(), error => this.saveFailedHelperStatus(error));
    this.allInterviewsCompleted = false;

  }
  private saveSuccessHelperStatus() {
    this.alertService.showSucessMessage("Updated successfully");
    this.serverCallback();
  }

  private saveFailedHelperStatus(error: any) {
    let test = Utilities.getHttpResponseMessage(error);
    this.alertService.showInfoMessage("Please try later" + test[0]);
  }
}
