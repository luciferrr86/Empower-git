import { Component, OnInit, ViewChild } from '@angular/core';
import { Router, ActivatedRoute } from "@angular/router";
import { Company } from '../../../models/sales-marketing/company-view.model';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { FormGroup, FormBuilder, Validators, FormArray } from '@angular/forms';
import { SalesMarketingService } from '../../../services/sales-marketing/sales-marketing.service';
import { AlertService } from '../../../services/common/alert.service';

@Component({
  selector: 'company-info',
  templateUrl: './company-info.component.html',
  styleUrls: ['./company-info.component.css']
})
export class CompanyInfoComponent implements OnInit {

  public companyInfoForm: FormGroup;
  public isSaving = false;
  public companyId = "";
  private isNew = false;
  public companyEdit: Company = new Company();

  constructor(private _fb: FormBuilder, private route: ActivatedRoute, private router: Router, private salesMarketingService: SalesMarketingService, private alertService: AlertService) { }

  ngOnInit() {
    this.companyInfoForm = this._fb.group({
      id: [''],
      comapnyName: ['', Validators.required],
      companyAddress: ['', Validators.required],
      city: ['', Validators.required],
      state: ['', Validators.required],
      country: ['', Validators.required],
      zipCode: ['', Validators.required],
      emailId: ['', Validators.required],
      telephone: ['', Validators.required],
      lstCompanyContacts: this._fb.array([
        this.initContact(),
      ])
    });
    this.getCompanyDetail();
  }
  getCompanyDetail() {
    this.route.queryParams.subscribe(params => {
      if (params['id']) {
        this.companyId = params['id'];
        this.salesMarketingService.getCompany(params['id']).subscribe(result => this.onSuccessfulDataLoad(result), error => this.onDataLoadFailed(error));
      }
    });
  }
  onSuccessfulDataLoad(company: Company) {
    this.companyInfoForm.patchValue({
      id: company.id,
      comapnyName: company.comapnyName,
      companyAddress: company.companyAddress,
      city: company.city,
      state: company.state,
      country: company.country,
      zipCode: company.zipCode,
      emailId: company.emailId,
      telephone: company.telephone,
    });
    if (company.lstCompanyContacts.length > 0) {
      this.setCompanyContact(company.lstCompanyContacts);
    }
  }
  setCompanyContact(level: any[]) {
    let control = <FormArray>this.companyInfoForm.controls.lstCompanyContacts;
    if (control.length > 0) {
      for (var i = 0; i <= control.length; i++) {
        control.controls.splice(0);
      }
    }
    level.forEach(x => {
      control.push(this._fb.group({ id: x.id, firstName: x.firstName, lastName: x.lastName, mobileNo: x.mobileNo, telephone: x.telephone, designation: x.designation, emailId: x.emailId, salesCompanyId: x.salesCompanyId }))
    });

  }
  onDataLoadFailed(error: any) {
    this.alertService.showInfoMessage("Unable to retrieve data from the server");
  }
  initContact() {
    return this._fb.group({
      id: [''],
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      mobileNo: ['', Validators.required],
      telephone: ['', Validators.required],
      designation: ['', Validators.required],
      emailId: ['', Validators.required],
      salesCompanyId: ['']
    });
  }

  addContact() {
    const control = <FormArray>this.companyInfoForm.controls['lstCompanyContacts'];
    control.push(this.initContact());
  }


  removeContact($event, i) {
    if ($event != "" && $event != null) {
      this.deleteComapnyContact($event, i);
    }
    else {
      const control = <FormArray>this.companyInfoForm.controls['lstCompanyContacts'];
      control.removeAt(i);
    }
  }
  deleteComapnyContact(id: string, count: number) {
    this.alertService.showConfirm("Are you sure you want to delete?", () => this.deleteCompanyHelper(id, count));
  }
  deleteCompanyHelper(id: string, count: number) {
    this.salesMarketingService.deleteCompanyContact(id)
      .subscribe(results => {
        const control = <FormArray>this.companyInfoForm.controls['lstCompanyContacts'];
        control.removeAt(count);
      },
        error => {
          this.alertService.showInfoMessage("An error occured whilst deleting");
        });
  }
  get lstCompanyContacts() {
    return this.companyInfoForm.get('lstCompanyContacts') as FormArray;
  }
  save() {
    this.isSaving = true;
    this.companyEdit.id = this.companyInfoForm.controls['id'].value;
    this.companyEdit.comapnyName = this.companyInfoForm.controls['comapnyName'].value;
    this.companyEdit.companyAddress = this.companyInfoForm.controls['companyAddress'].value;
    this.companyEdit.city = this.companyInfoForm.controls['city'].value;
    this.companyEdit.state = this.companyInfoForm.controls['state'].value;
    this.companyEdit.country = this.companyInfoForm.controls['country'].value;
    this.companyEdit.zipCode = this.companyInfoForm.controls['zipCode'].value;
    this.companyEdit.emailId = this.companyInfoForm.controls['emailId'].value;
    this.companyEdit.telephone = this.companyInfoForm.controls['telephone'].value;
    this.companyEdit.lstCompanyContacts = this.companyInfoForm.controls['lstCompanyContacts'].value;
    this.salesMarketingService.saveCompany(this.companyEdit).subscribe(sucess => this.saveSuccessHelper(sucess), error => this.saveFailedHelper(error, "Couldn't saved successfully."));

  }

  private saveSuccessHelper(result?: any) {
    this.isSaving = false;
    this.alertService.showInfoMessage("Company saved successfully.");

  }

  private saveFailedHelper(error: any, errMsg: string) {
    this.isSaving = false;
    this.alertService.showInfoMessage(errMsg);
  }

}
