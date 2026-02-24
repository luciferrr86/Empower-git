import { Component, OnInit, ViewChild, TemplateRef } from '@angular/core';
import { MyTimesheetService } from '../../../../services/timesheet/my-timesheet.service';
import { AccountService } from '../../../../services/account/account.service';
import { AlertService } from '../../../../services/common/alert.service';
import { MyTimesheetViewModel, DayList, ProjectList, ProjectHour } from '../../../../models/timesheet/myTimesheet.model';
import { Utilities } from '../../../../services/common/utilities';
import { DropDownList } from '../../../../models/common/dropdown';
import { FormGroup, FormBuilder, FormArray } from '@angular/forms';


@Component({
  selector: 'employee-timesheet',
  templateUrl: './employee-timesheet.component.html',
  styleUrls: ['./employee-timesheet.component.css']
})
export class EmployeeTimesheetComponent implements OnInit {

  heroes: ProjectHour[];
  public employeeTimesheetForm: FormGroup;
  public isConfig: boolean = false;
  public isSaving = false;
  public isSubmit = false;
  public total: number;

  columns: any[] = [];
  @ViewChild('daysTemplate')
  daysTemplate: TemplateRef<any>;
  loadingIndicator: boolean = true;
  lstWeekDate = [];
  constructor(private fb: FormBuilder, private myTimesheeService: MyTimesheetService, private accountService: AccountService, private alertService: AlertService) { }
  myTimesheet: MyTimesheetViewModel = new MyTimesheetViewModel();
  ngOnInit() {

    this.employeeTimesheetForm = this.fb.group({
      designation: [''],
      approverlId: [''],
      employeeId: [''],
      frequency: [''],
      fullName: [''],
      isManagerApproved: [''],
      isUserSaved: [''],
      isUserSubmit: [''],
      type: [''],
      userSpanId: [''],
      employeeEmail: [''],
      mangerName: [''],
      totalHour: [''],
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

    this.getMyTimesheet();
  }



  ngAfterContentChecked() {
    // this.total = this.heroes.reduce(funtion(runningValue: number, hero: Hero)=> {
    //   runningValue = runningValue + (hero.price * hero.qty);
    // }, 0);
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



  getMyTimesheet() {
    this.myTimesheeService.getAll(this.accountService.currentUser.id).subscribe(result => this.onSuccessfulDataLoad(result), error => this.onDataLoadFailed(error));
  }
  getWeekly(spanId: string) {
    this.myTimesheeService.getWeekly(this.accountService.currentUser.id, spanId).subscribe(result => this.onSuccessfulDataLoad(result), error => this.onDataLoadFailed(error));


  }
  get projectList() {
    var tet = this.employeeTimesheetForm.get('projectList') as FormArray;
    return tet;
  }

  onSuccessfulDataLoad(myTimesheetModel: MyTimesheetViewModel) {
    this.myTimesheet = myTimesheetModel;
    this.employeeTimesheetForm.patchValue({
      approverlId: this.myTimesheet.approverlId,
      designation: this.myTimesheet.designation,
      employeeId: this.myTimesheet.employeeId,
      frequency: this.myTimesheet.frequency,
      fullName: this.myTimesheet.fullName,
      isManagerApproved: this.myTimesheet.isManagerApproved,
      isUserSaved: this.myTimesheet.isUserSaved,
      isUserSubmit: this.myTimesheet.isUserSubmit,
      type: this.myTimesheet.type,
      totalHour: this.myTimesheet.totalHour,
      userSpanId: this.myTimesheet.userSpanId,
      employeeEmail: this.myTimesheet.employeeEmail,
      mangerName: this.myTimesheet.mangerName,
      mangerEmail: this.myTimesheet.mangerEmail
    });
    if (this.myTimesheet.lstUserWeeks != null && this.myTimesheet.projectList.length > 0) {
      this.lstWeekDate = myTimesheetModel.lstUserWeeks;
    }
    if (this.myTimesheet.dayList != null && this.myTimesheet.dayList.length > 0) {

      this.employeeTimesheetForm.setControl('dayList', this.setDayList(myTimesheetModel.dayList));
    }
    if (this.myTimesheet.projectList != null && this.myTimesheet.projectList.length > 0) {

      this.employeeTimesheetForm.setControl('projectList', this.setProjectsList(myTimesheetModel.projectList));
    }

    this.loadingIndicator = false;
    this.isConfig = true
  }
  setUserWeeksList(weeks: DropDownList[]): FormArray {
    const formArray = new FormArray([]);
    weeks.forEach(x => {
      formArray.push(this.fb.group({
        label: x.label,
        value: x.value
      }));
    });
    return formArray;
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

  onDataLoadFailed(error: any) {
    this.alertService.showInfoMessage(error.error);
    this.loadingIndicator = false;
  }


  public save() {
    this.isSaving = true;
    this.myTimesheet = this.employeeTimesheetForm.value;
    this.myTimesheet.type = "save"
    this.myTimesheeService.create(this.myTimesheet).subscribe(sucess => this.saveSuccessHelper(), error => this.saveFailedHelper(error));
  }

  private saveSuccessHelper() {
    this.isSaving = false;
    this.alertService.showSucessMessage("Saved successfully");
    this.getMyTimesheet();
  }
  public Sumbit() {
    this.isSubmit = true;
    this.myTimesheet = this.employeeTimesheetForm.value;
    this.myTimesheet.type = "Submit"
    this.myTimesheeService.create(this.myTimesheet).subscribe(sucess => this.sumbitSuccessHelper(), error => this.saveFailedHelper(error));
  }

  private sumbitSuccessHelper() {
    this.isSubmit = false;
    this.alertService.showSucessMessage("Sumbit successfully");
    this.getMyTimesheet();
  }


  private saveFailedHelper(error: any) {
    this.isSaving = false;
    this.isSubmit = false;
    let test = Utilities.getHttpResponseMessage(error);
    this.alertService.showInfoMessage("Please try later" + test[0]);
  }

  public doSomething() {

  }
}
