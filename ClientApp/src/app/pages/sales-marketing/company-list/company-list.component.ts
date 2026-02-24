import { Component, OnInit, ViewChild, TemplateRef } from '@angular/core';
import { SalesMarketingService } from '../../../services/sales-marketing/sales-marketing.service';
import { AlertService } from '../../../services/common/alert.service';
import { CompanyViewModel, Company } from '../../../models/sales-marketing/company-view.model';
import { CompanyInfoComponent } from '../company-info/company-info.component';
import { Router } from '@angular/router';

@Component({
  selector: 'app-company-list',
  templateUrl: './company-list.component.html',
  styleUrls: ['./company-list.component.css']
})
export class CompanyListComponent implements OnInit {
  columns: any[] = [];
  filterQuery: string = "";
  pageNumber = 0;
  count = 0;
  pageSize = 10;
  rows: Company[] = [];
  loadingIndicator: boolean = true;
  public company: Company = new Company();

  @ViewChild('actionsTemplate')
  actionsTemplate: TemplateRef<any>;

  @ViewChild('companyInfo')
  companyInfo: CompanyInfoComponent;
  constructor(private salesMarketingService: SalesMarketingService, private router: Router, private alertService: AlertService) { }

  ngOnInit() {
    this.columns = [
      { prop: 'comapnyName', name: 'Company Name' },
      { prop: 'telephone', name: 'Telephone' },
      { prop: 'emailId', name: 'EmailId' },
      { prop: 'Action', name: 'Action', cellTemplate: this.actionsTemplate }
    ];
    this.getAllCompany(this.pageNumber, this.pageSize, this.filterQuery);
  }
  getFilteredData(filterString) {
    this.getAllCompany(this.pageNumber, this.pageSize, filterString);
  }

  getData(e) {
    this.getAllCompany(this.pageNumber, e, this.filterQuery);
  }

  setPage(e) {
    this.getAllCompany(e.offset, this.pageSize, this.filterQuery);
  }

  getAllCompany(page?: number, pageSize?: number, name?: string) {
    this.salesMarketingService.getAllCompanylist(page, pageSize, name).subscribe(result => this.onSuccessfulDataLoad(result), error => this.onDataLoadFailed(error));
  }
  onSuccessfulDataLoad(companyList: CompanyViewModel) {
    this.rows = companyList.listCompany;
    companyList.listCompany.forEach((company, index, companys) => {
      (<any>company).index = index + 1;
    });
    this.count = companyList.totalCount;
    this.loadingIndicator = false;
  }

  onDataLoadFailed(error: any) {
    this.loadingIndicator = false;
    this.alertService.showInfoMessage("Unable to retrieve list from the server");

  }
  editCompany(id) {
    this.router.navigate(['../sales-tracker/companyinfo'], { queryParams: { id: id } });
  }
  deleteComapny(company: Company) {
    this.alertService.showConfirm("Are you sure you want to delete?", () => this.deleteCompanyHelper(company));
  }
  deleteCompanyHelper(company: Company) {
    this.salesMarketingService.deleteCompany(company.id)
      .subscribe(results => {
        this.getAllCompany(this.pageNumber, this.pageSize, this.filterQuery);
      },
        error => {
          this.alertService.showInfoMessage("An error occured whilst deleting");
        });
  }

}
