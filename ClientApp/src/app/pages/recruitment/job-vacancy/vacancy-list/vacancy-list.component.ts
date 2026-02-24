import { Component, OnInit, ViewChild, TemplateRef } from '@angular/core';
import { Router } from "@angular/router";
import { AlertService } from '../../../../services/common/alert.service';
import { Vacancy, VacancyModel } from '../../../../models/recruitment/job-vacancy/vacancy-list.model';
import { VacancyService } from '../../../../services/recruitment/vacancy.service';
import { DropDownList } from '../../../../models/common/dropdown';
import { Utilities } from '../../../../services/common/utilities';
@Component({
  selector: 'app-vacancy-list',
  templateUrl: './vacancy-list.component.html',
  styleUrls: ['./vacancy-list.component.css']
})
export class VacancyListComponent implements OnInit {

  public isSaving = false;
  columns: any[] = [];
  filterQuery: string = "";
  pageNumber = 0;
  count = 0;
  pageSize = 10;

  rows: Vacancy[] = [];
  rowsCache: Vacancy[] = [];
  loadingIndicator: boolean = true;
  allJobTypeList: DropDownList[] = [];
  allManagerList: DropDownList[] = [];
  public serverCallback: () => void;
  @ViewChild('actionsTemplate')
  actionsTemplate: TemplateRef<any>;
  @ViewChild('publishTemplate')
  publishTemplate: TemplateRef<any>; 
  @ViewChild('jdReasonTemplate')
  jdReasonTemplate: TemplateRef<any>;

  constructor(private alertService: AlertService, private vacancyService: VacancyService, private router: Router) {

  }
  ngOnInit() {
    this.columns = [
      { prop: "index", name: '#', canAutoResize: false },
      { prop: 'jobTitle', name: 'Job Title' },
      { prop: 'jobLocation', name: 'Job Location' },
      { prop: 'experience', name: 'Job Experience' },
      { prop: 'jdAvailable', name: 'JD Available', cellTemplate: this.jdReasonTemplate, resizeable: false, canAutoResize: false, sortable: false, draggable: false },
      { prop: 'bIsPublished', name: 'Published', cellTemplate: this.publishTemplate, resizeable: false, canAutoResize: false, sortable: false, draggable: false },
      { prop: 'Action', name: 'Action', cellTemplate: this.actionsTemplate, resizeable: false, canAutoResize: false, sortable: false, draggable: false }
    ];
    this.getAllVacancy(this.pageNumber, this.pageSize, this.filterQuery);
  }
  getAllVacancy(page?: number, pageSize?: number, name?: string) {
    this.loadingIndicator = true;
    this.vacancyService.getAllJobVacany(page, pageSize, name).subscribe(result => this.onSuccessfulDataLoad(result), error => this.onDataLoadFailed());
  }
  getData(e) {
    this.getAllVacancy(this.pageNumber, e, this.filterQuery);
  }
  setPage(e) {
    this.getAllVacancy(e.offset, this.pageSize, this.filterQuery);
  }
  getFilteredData(filterString) {
    this.getAllVacancy(this.pageNumber, this.pageSize, filterString);

  }
  onSuccessfulDataLoad(vacancies: VacancyModel) {
    console.log(vacancies);
    this.rows = vacancies.jobVacancyModel;
    vacancies.jobVacancyModel.forEach((vacancy, index) => {
      (<any>vacancy).index = index + 1;
    });

    this.count = vacancies.totalCount;
    this.loadingIndicator = false;
  }

  onDataLoadFailed() {
    this.alertService.showInfoMessage("Unable to retrieve list from the server");
  }

  public addVacancy() {
    this.router.navigate(['../recruitment/job-create']);
  }
  //public jdReason() {
  //  this.router.navigate(['../recruitment/job-reason']);
  //}

  public editVaccancy(id) {
    this.router.navigate(['../recruitment/job-create'], { queryParams: { id: id } });
  }
  public jdReason(id) {
    this.router.navigate(['../recruitment/job-reason', id]);
  }

  deleteVacancy(vacancy: Vacancy) {
    this.alertService.showConfirm("Are you sure you want to delete?", () => this.deleteVacancyHelper(vacancy));
  }
  deleteVacancyHelper(vacancy: Vacancy) {
    this.vacancyService.deleteJob(vacancy.id)
      .subscribe(() => {
        this.getAllVacancy(this.pageNumber, this.pageSize, this.filterQuery);
      },
        () => {
          this.alertService.showInfoMessage("An error occured whilst deleting");
        });
  }
  published(id) {
    this.vacancyService.publish(id).subscribe(sucess => this.saveSuccessHelper(), error => this.saveFailedHelper(error));
  }

  private saveSuccessHelper() {
    this.isSaving = false;
    this.alertService.showSucessMessage("Action completed successfully.");
    this.serverCallback();
  }

  private saveFailedHelper(error: any) {
    this.isSaving = false;
    let test = Utilities.getHttpResponseMessage(error);
    this.alertService.showInfoMessage("Please try later" + test[0]);
  }
}
