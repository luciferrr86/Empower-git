import { Component, OnInit } from '@angular/core';
import { Router, NavigationExtras } from "@angular/router";
import { JobCreate } from '../../../../models/recruitment/job-vacancy/job-create';
import { VacancyService } from '../../../../services/recruitment/vacancy.service';
import { AlertService } from '../../../../services/common/alert.service';
import { Utilities } from '../../../../services/common/utilities';
import { ActivatedRoute } from "@angular/router";
import { DropDownList } from '../../../../models/common/dropdown';
import { FormGroup, FormArray, FormBuilder, Validators } from '@angular/forms';
import { minLengthOnNumberService } from '../../../../services/Validation/minLengthOnNumberService';

@Component({
  selector: 'job-creation',
  templateUrl: './job-creation.component.html',
  styleUrls: ['./job-creation.component.css']
})
export class JobCreationComponent implements OnInit {
  public isSaving = false;
  private isNew = false;
  public modalTitle = "";
  public jobCreate: JobCreate = new JobCreate();
  public serverCallback: () => void;
  public jobVacancyId = "";
  public jdAvailable = "";
  showDiv = {

    next: false
  }
  divShow: string;
  public jobCreateForm: FormGroup;
  jobTypeList: DropDownList[] = [];
  hiringManagerList: DropDownList[] = [];
  quillConfiguration = {
    toolbar: [
      ['bold', 'italic', 'underline', 'strike'],
      ['blockquote', 'code-block'],
      [{ list: 'ordered' }, { list: 'bullet' }],
      [{ header: [1, 2, 3, 4, 5, 6, false] }],
      [{ color: [] }, { background: [] }],
      ['link'],
      ['clean'],
    ],
  }
    
  constructor(private _fb: FormBuilder, private route: ActivatedRoute, private vacancyService: VacancyService, private alertService: AlertService, private router: Router) {

  }
  

  ngOnInit() {
    this.jobCreateForm = this._fb.group({
      jobTitle: ['', Validators.required],
      jobTypeId: ['', Validators.required],
      jobLocation: ['', Validators.required],
      experience: ['', Validators.required],
      noOfvacancies: ['', Validators.compose([Validators.required, minLengthOnNumberService.checkLimit(1)])],
      currency: ['', Validators.required],
      salaryRange: ['', Validators.required],
      jobRequirements: ['', Validators.required],
      jobDescription: [''],
      jobVacancyLevel: this._fb.array([
        this.initInterviewLevel()
      ])
    });
    this.getJobCreationDetail();
  }

  initInterviewLevel() {

    return this._fb.group({
      id: [],
      name: [''],
      managerList: [[]]

    });
  }
  getJobCreationDetail() {

    this.route.queryParams.subscribe(params => {
      if (params['id']) {
        this.jobVacancyId = params['id'];
        this.vacancyService.getJobCreation(params['id']).subscribe(result => this.onSuccessfulDataLoad(result), error => this.onDataLoadFailed(error));
      } else {
        if (this.jobVacancyId != "") {
          this.vacancyService.getJobCreation(this.jobVacancyId).subscribe(result => this.onSuccessfulDataLoad(result), error => this.onDataLoadFailed(error));
        }
        else {
          this.isNew = true;
          this.vacancyService.getJobCreation(null).subscribe(result => this.onSuccessfulDataLoad(result), error => this.onDataLoadFailed(error));

        }
      }
    });
  }

  onSuccessfulDataLoad(vacancies: JobCreate) {
    this.jobCreate = vacancies;
    this.jobTypeList = vacancies.jobTypeList;
    this.hiringManagerList = vacancies.managerList;
    this.divShow = this.jobCreate.jdAvailable;
    
    if(this.jobCreate.jobDescription) this.showDiv.next = true;

    this.jobCreateForm.patchValue({
      jobTitle: this.jobCreate.jobTitle,
      jobTypeId: this.jobCreate.jobTypeId,
      jobLocation: this.jobCreate.jobLocation,
      experience: this.jobCreate.experience,
      noOfvacancies: this.jobCreate.noOfvacancies,
      currency: this.jobCreate.currency,
      salaryRange: this.jobCreate.salaryRange,
      jobRequirements: this.jobCreate.jobRequirements,
      jobDescription: this.jobCreate.jobDescription
      
    });
    if (vacancies.jobVacancyLevel.length > 0) {
      this.setLevels(this.jobCreate.jobVacancyLevel);
    }


  }
  setLevels(level: any[]) {
    let control = <FormArray>this.jobCreateForm.controls.jobVacancyLevel;
    if (control.length > 0) {
      for (var i = 0; i <= control.length; i++) {
        control.controls.splice(0);
      }
    }
    level.forEach(x => {
      control.push(this._fb.group({ id: x.id, name: x.name, managerList: [x.managerList] }))
    });

  }
  onDataLoadFailed(error: any) {
    this.alertService.showInfoMessage("Please Try Again");
  }
  get jobVacancyLevel() {
    return this.jobCreateForm.get('jobVacancyLevel') as FormArray;
  }
  addInterViewLevel() {
    const control = <FormArray>this.jobCreateForm.controls['jobVacancyLevel'];
    control.push(this.initInterviewLevel());
  }

  removeInterviewLevel($event, i) {
    if ($event != "" && $event != null) {
      this.vacancyService.deleteInterviewLevel($event).subscribe(sucess => this.deleteSuccessHelper(sucess, i), error => this.failedHelper(error));
    }
    else {
      const control = <FormArray>this.jobCreateForm.controls['jobVacancyLevel'];
      control.removeAt(i);
    }
  }
  private deleteSuccessHelper(res: any, i: number) {
    const control = <FormArray>this.jobCreateForm.controls['jobVacancyLevel'];
    control.removeAt(i);
    this.alertService.showInfoMessage("Deleted Successfully");
  }

  public saveJobVacancy() {
    this.isSaving = true;
    this.jobCreate.jobVacancyLevel = this.jobCreateForm.controls['jobVacancyLevel'].value;
    this.jobCreate.jobTitle = this.jobCreateForm.controls['jobTitle'].value;
    this.jobCreate.jobTypeId = this.jobCreateForm.controls['jobTypeId'].value;
    this.jobCreate.jobLocation = this.jobCreateForm.controls['jobLocation'].value;
    this.jobCreate.experience = this.jobCreateForm.controls['experience'].value;
    this.jobCreate.noOfvacancies = this.jobCreateForm.controls['noOfvacancies'].value;
    this.jobCreate.currency = this.jobCreateForm.controls['currency'].value;
    this.jobCreate.salaryRange = this.jobCreateForm.controls['salaryRange'].value;
    this.jobCreate.jobRequirements = this.jobCreateForm.controls['jobRequirements'].value;
    this.jobCreate.jobDescription = this.jobCreateForm.controls['jobDescription'].value;
    if (this.jobCreate.jobDescription) {
      this.jdAvailable = "Yes";
    }
    if (this.jobVacancyId != "") {
      this.vacancyService.createJob(this.jobCreate, this.jobVacancyId).subscribe(sucess => this.saveSuccessJobVaccancy(sucess), error => this.failedHelper(error));
    } else {
      this.vacancyService.createJob(this.jobCreate).subscribe(sucess => this.saveSuccessJobVaccancy(sucess), error => this.failedHelper(error));
    }
  }
  private saveSuccessJobVaccancy(result?: string) {
    this.isSaving = false;
    this.jobVacancyId = result;
    this.vacancyService.changeVacancyId(this.jobVacancyId);
    if (this.isNew) {
      this.alertService.showSucessMessage("Saved successfully");
      this.router.navigate(['../recruitment/job-create'], { queryParams: { id: this.jobVacancyId } });
     
    } else {
      this.alertService.showSucessMessage("Updated successfully");
    }
    
    if (this.jdAvailable === "No") {
      
      this.alertService.showConfirmCancelMessage("Would you like to update the reason for the Non-Existent Job Description ?", "Update Reason", () => this.updateReasonRoute())
    }
    this.getJobCreationDetail();

  }
  
  updateReasonRoute() {
    this.router.navigate(['../recruitment/job-reason', this.jobVacancyId]);
  }
  udpateValue() {
    
    this.jdAvailable = "No";
    this.alertService.showSucessMessage("Updated Value");
    console.log(this.jdAvailable);
  }
  private failedHelper(error: any) {
    this.isSaving = false;
    let test = Utilities.getHttpResponseMessage(error);
    console.log(test[0]);
    this.alertService.showInfoMessage("Unable to save the data! Please Try Again ");
  }
}
