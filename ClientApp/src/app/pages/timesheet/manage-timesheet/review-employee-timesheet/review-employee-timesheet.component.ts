import { Component, OnInit } from '@angular/core';
import { ManageTimesheetService } from '../../../../services/timesheet/manage-timesheet.service';
import { ActivatedRoute } from '../../../../../../node_modules/@angular/router';
import { MyTimesheetViewModel, DayList, ProjectList, ProjectHour } from '../../../../models/timesheet/myTimesheet.model';
import { AlertService } from '../../../../services/common/alert.service';
import { Utilities } from '../../../../services/common/utilities';
import { MyTimesheetService } from '../../../../services/timesheet/my-timesheet.service';
import { FormGroup, FormBuilder, FormArray } from '@angular/forms';

@Component({
  selector: 'review-employee-timesheet',
  templateUrl: './review-employee-timesheet.component.html',
  styleUrls: ['./review-employee-timesheet.component.css']
})

export class ReviewEmployeeTimesheetComponent implements OnInit {
  myTimesheet: MyTimesheetViewModel = new MyTimesheetViewModel();
  loadingIndicator: boolean = true;
  public isSaving = false;
  public isSubmit = false;
  public userId: string;
  public reviewEmployeeTimesheetForm: FormGroup;
  lstWeekDate = [];
  constructor(private fb: FormBuilder, private myTimesheeService: MyTimesheetService, private manageSerivce: ManageTimesheetService, private route: ActivatedRoute, private alertService: AlertService) {
    this.route.queryParams.subscribe(params => {
      if (params['id']) {
        this.userId = params['id']
        this.manageSerivce.getEmployeeTimesheet(params['id']).subscribe(result => this.onSuccessfulDataLoad(result), error => this.onDataLoadFailed());
      }
    });
  }

  ngOnInit() {
    this.reviewEmployeeTimesheetForm = this.fb.group({
      designation: [''],
      approverlId: [''],
      employeeId: [''],
      frequency: [''],
      fullName: [''],
      isManagerApproved: [''],
      isUserSaved: [''],
      isUserSubmit: [''],
      type: [''],
      totalHour: [''],
      userSpanId: [''],
      employeeEmail: [''],
      mangerName: [''],
      mangerEmail: [''],
      lstUserWeeks: this.fb.array([
        this.initUserWeeksList()
      ]),
      dayList: this.fb.array([
        this.initDayList()
      ]),
      projectList: this.fb.array([
        this.initprojectList()
      ])
    });
  }
  initUserWeeksList() {
    return this.fb.group({
      value: [''],
      label: ['']
    });
  }
  initDayList() {
    return this.fb.group({
      userDetailId: [''],
      day: [''],
      date: [''],
      userSpanId: [''],
      totalHour: [''],
      isUserSaved: [''],
      isUserSubmit: [''],
      isManagerApproved: [''],
      isAllotted: [''],
      isAllow: ['']
    });
  }
  initprojectList() {
    return this.fb.group({
      projectId: [''],
      name: [''],
      projectHourList: this.fb.array([
        this.initprojectHour()
      ]),
      totalProjectHour: ['']
    });
  }


  initprojectHour() {
    return this.fb.group({
      userDetailProjectHourId: [''],
      isAllow: [''],
      isAllotted: [''],
      question: [''],
      hour: ['']
    });
  }
  get projectList() {
    var tet = this.reviewEmployeeTimesheetForm.get('projectList') as FormArray;
    return tet;
  }

  onSuccessfulDataLoad(myTimesheetModel: MyTimesheetViewModel) {
    this.reviewEmployeeTimesheetForm.patchValue({
      approverlId: myTimesheetModel.approverlId,
      designation: myTimesheetModel.designation,
      employeeId: myTimesheetModel.employeeId,
      frequency: myTimesheetModel.frequency,
      fullName: myTimesheetModel.fullName,
      isManagerApproved: myTimesheetModel.isManagerApproved,
      isUserSaved: myTimesheetModel.isUserSaved,
      isUserSubmit: myTimesheetModel.isUserSubmit,
      type: myTimesheetModel.type,
      totalHour: myTimesheetModel.totalHour,
      userSpanId: myTimesheetModel.userSpanId,
      employeeEmail: myTimesheetModel.employeeEmail,
      mangerName: myTimesheetModel.mangerName,
      mangerEmail: myTimesheetModel.mangerEmail
    });

    if (myTimesheetModel.lstUserWeeks != null && myTimesheetModel.projectList.length > 0) {
      this.lstWeekDate = myTimesheetModel.lstUserWeeks;
    }
    if (myTimesheetModel.dayList != null && myTimesheetModel.dayList.length > 0) {

      this.reviewEmployeeTimesheetForm.setControl('dayList', this.setDayList(myTimesheetModel.dayList));
    }
    if (myTimesheetModel.projectList != null && myTimesheetModel.projectList.length > 0) {

      this.reviewEmployeeTimesheetForm.setControl('projectList', this.setProjectsList(myTimesheetModel.projectList));

    }
    this.loadingIndicator = false;

  }
  setDayList(dayList: DayList[]): FormArray {
    const formArray = new FormArray([]);
    dayList.forEach(x => {
      formArray.push(this.fb.group({
        userDetailId: x.userDetailId != null ? x.userDetailId : "",
        day: x.day,
        date: x.date != null ? x.date : "",
        userSpanId: x.userSpanId != null ? x.userSpanId : "",
        totalHour: x.totalHour != null ? x.totalHour : "",
        isUserSaved: x.isUserSaved,
        isUserSubmit: x.isUserSubmit,
        isManagerApproved: x.isManagerApproved,
        isAllotted: x.isAllotted,
        isAllow: x.isAllow
      }));
    });
    return formArray;
  }
  setProjectsList(projectList: ProjectList[]): FormArray {
    const formArray = new FormArray([]);
    projectList.forEach(x => {
      formArray.push(this.fb.group({
        projectId: x.projectId,
        name: x.name,
        projectHourList: this.setProjectHour(x.projectHourList),
        totalProjectHour: x.totalProjectHour,
      }));
    });
    return formArray;

  }

  setProjectHour(projectHour: ProjectHour[]): FormArray {
    const formArray = new FormArray([]);
    projectHour.forEach(x => {
      formArray.push(this.fb.group({
        userDetailProjectHourId: x.userDetailProjectHourId != null ? x.userDetailProjectHourId : "",
        isAllow: x.isAllow,
        isAllotted: x.isAllotted,
        hour: x.hour
      }));
    });

    return formArray;
  }



  onDataLoadFailed() {
    this.alertService.showInfoMessage("Unable to retrieve list from the server");
  }

  public save() {
    this.isSaving = true;
    this.myTimesheet = this.reviewEmployeeTimesheetForm.value;
    this.myTimesheet.type = "Save"
    this.manageSerivce.approve(this.myTimesheet).subscribe(sucess => this.saveSuccessHelper(), error => this.saveFailedHelper(error));
  }

  public Approve() {
    this.isSubmit = true;
    this.myTimesheet = this.reviewEmployeeTimesheetForm.value;
    this.myTimesheet.type = "Approve"
    this.manageSerivce.approve(this.myTimesheet).subscribe(sucess => this.saveSuccessHelper(), error => this.saveFailedHelper(error));
  }

  private saveSuccessHelper() {
    this.isSaving = false;
    this.isSubmit = false;
    this.manageSerivce.getEmployeeTimesheet(this.userId).subscribe(result => this.onSuccessfulDataLoad(result), error => this.onDataLoadFailed());
    this.alertService.showSucessMessage("Saved successfully");
  }

  private saveFailedHelper(error: any) {
    this.isSaving = false;
    this.isSubmit = false;
    let test = Utilities.getHttpResponseMessage(error);
    this.alertService.showInfoMessage("Please try later" + test[0]);
  }

  getWeekly(employeeId: string, spanId: string) {
    this.myTimesheeService.getWeekly(employeeId, spanId).subscribe(result => this.onSuccessfulDataLoad(result), error => this.onDataLoadFailed());
  }



  public doSomething1() {

  }
}
