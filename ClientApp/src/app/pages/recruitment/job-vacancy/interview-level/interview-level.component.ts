import { Component, OnInit } from '@angular/core';
import { FormGroup, FormArray, FormBuilder, Validators } from '@angular/forms';
import { JobVacancyFormDataService } from '../../../../services/recruitment/job-vacancy-form-data.service';

@Component({
  selector: 'interview-level',
  templateUrl: './interview-level.component.html',
  styleUrls: ['./interview-level.component.css']
})
export class InterviewLevelComponent implements OnInit {


  public interviewLevelForm: FormGroup;
  constructor(private _fb: FormBuilder, private vacancyFormDataService: JobVacancyFormDataService, ) { }

  ngOnInit() {
    this.interviewLevelForm = this._fb.group({
      addresses: this._fb.array([
        this.initInterviewLevel(),
      ])
    });
  }

  initInterviewLevel() {

    return this._fb.group({
      name: ['', Validators.required],
      managerList: [[''], Validators.required]

    });
  }

  addInterViewLevel() {
    const control = <FormArray>this.interviewLevelForm.controls['addresses'];
    control.push(this.initInterviewLevel());
  }

  removeInterviewLevel(i: number) {
    const control = <FormArray>this.interviewLevelForm.controls['addresses'];
    control.removeAt(i);
  }
  get addresses() {
    return this.interviewLevelForm.get('addresses') as FormArray;
  }

  save(model: any) {

  }

}
