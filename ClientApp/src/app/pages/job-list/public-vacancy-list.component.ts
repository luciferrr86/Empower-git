import { Component, OnInit } from "@angular/core";
import { ActivatedRoute, Router } from "@angular/router";
import { JobApplyModel } from "../../models/recruitment/job-vacancy/job-apply.model";
import { Vacancy } from "../../models/recruitment/job-vacancy/vacancy-list.model";
import { AlertService, MessageSeverity } from "../../services/common/alert.service";
import { VacancyService } from "../../services/recruitment/vacancy.service";


@Component({
  selector: 'public-vacancy-list',
  templateUrl: './public-vacancy-list.component.html',
  styleUrls: ['./public-vacancy-list.component.css']
})
export class PublicVacancyListComponent implements OnInit {
  jobVacancyModel: Vacancy[];
  constructor(private jobVacancyService: VacancyService, public router: Router) {

  }
  jobVacancyModelFilter: any[];
  filter: any = {};
  isAvailable: boolean;

  ngOnInit() {
    this.jobVacancyService.getAllAvailableVacancy().
      subscribe((result) => {
        if (result.length !== 0) {
          this.isAvailable = true;
          this.jobVacancyModel = result;
          this.jobVacancyModelFilter = this.jobVacancyModel;
          console.log(this.jobVacancyModel);
        }
        else {
          this.isAvailable = false;
        }
      });

  }

  applyJob(id: string) {
    this.router.navigate(['../candidate/login'], { queryParams: { id: id } });
  }

  //serch filter code without pipe 
  onChange() {
      
    let allVacaniesForSearch = this.jobVacancyModelFilter;
    if (this.filter.name) {
      allVacaniesForSearch = allVacaniesForSearch.filter(v => v.jobTitle.toLowerCase().indexOf(this.filter.name.toString().toLowerCase()) > -1);
    }
    this.jobVacancyModel = allVacaniesForSearch;
    console.log(this.jobVacancyModel);
  }

  resetFilter() {
    this.filter = {};
    this.onChange();
  }

}
