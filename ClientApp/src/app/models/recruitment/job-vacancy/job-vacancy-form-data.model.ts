import { JobCreate } from './job-create';
import { HrKpi } from './hr-kpi.model';
import { SkillInterview } from './skill-interview.model';
export class JobVacancyFormData {
public jobVacany:JobCreate;
public hrKpiArray:HrKpi[];
public skillInterviewArray:SkillInterview[];

}