import { HrKpi } from './../../models/recruitment/job-vacancy/hr-kpi.model';
import { Injectable } from '@angular/core';
import { JobVacancyFormData } from '../../models/recruitment/job-vacancy/job-vacancy-form-data.model';
import { JobCreate } from '../../models/recruitment/job-vacancy/job-create';
import { SkillInterview } from '../../models/recruitment/job-vacancy/skill-interview.model';

@Injectable()
export class JobVacancyFormDataService {

  private formData: JobVacancyFormData = new JobVacancyFormData();
  constructor() {

  }

  setVacancyForm(data: JobCreate) {
    this.formData.jobVacany = data;
  }

  setHrKpi(data: HrKpi[]) {
    this.formData.hrKpiArray = data;
  }
  setSkillKpi(data: SkillInterview[]) {
    this.formData.skillInterviewArray = data;
  }
  getVacancyForm(): JobCreate {
    var jobcreate = new JobCreate();
    jobcreate = this.formData.jobVacany;
    return jobcreate;
  }

  getHrKpi(): HrKpi[] {
    return this.formData.hrKpiArray;
  }

  getSkillInterview(): SkillInterview[] {
    return this.formData.skillInterviewArray;
  }
}
