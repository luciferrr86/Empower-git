import { Component, OnInit, ViewChild } from '@angular/core';
import { IOption } from 'ng-select';
import { ExpenseBookingService } from '../../../../services/expense-booking/expense-booking.service';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { DropDownList } from '../../../../models/common/dropdown';
import { Utilities } from '../../../../services/common/utilities';
import { AlertService } from '../../../../services/common/alert.service';
import { ExpenseSubCategoryModel } from '../../../../models/expense-booking/expense-subcategory.model';

@Component({
  selector: 'sub-category-info',
  templateUrl: './sub-category-info.component.html',
  styleUrls: ['./sub-category-info.component.css']
})
export class SubCategoryInfoComponent implements OnInit {


  public isSaving = false;
  private isNew = false;
  ctegoryList: Array<IOption> = [];
  subcategoryList: Array<IOption> = [];
  categoryItem: ExpenseSubCategoryModel = new ExpenseSubCategoryModel();
  loadingIndicator: boolean = true;

  public serverCallback: () => void;
  public modalTitle = "";

  @ViewChild('editorModal')
  editorModal: ModalDirective;

  constructor(private bookingService: ExpenseBookingService, private alertService: AlertService) { }

  ngOnInit() {
  }
  addItem(categeroyList?: DropDownList[]) {
    this.modalTitle = "Add ";
    this.isNew = true;
    this.editorModal.show();
    this.ctegoryList = categeroyList;
    this.categoryItem = new ExpenseSubCategoryModel();
    return this.categoryItem;
  }
  update(item: ExpenseSubCategoryModel, categeroyList?: DropDownList[]) {
    this.modalTitle = "Edit ";
    this.editorModal.show();
    this.ctegoryList = categeroyList;
    this.isNew = false;
    if (item) {
      this.categoryItem = new ExpenseSubCategoryModel();
      Object.assign(this.categoryItem, item);
      this.categoryItem.categoryId = item.categoryId.toLowerCase();
      return this.categoryItem;
    }
  }
  subCategoryList(id?: string) {
    this.bookingService.getSubCategory(id).subscribe(result => this.onSuccessful(result), error => this.onDataLoadFailed());
  }
  save() {
    this.isSaving = true;
    if (this.isNew) {
      this.bookingService.createSubCategory(this.categoryItem).subscribe(sucess => this.saveSuccessHelper(), error => this.saveFailedHelper(error));
    }
    else {
      this.bookingService.updateSubCategory(this.categoryItem, this.categoryItem.id).subscribe(sucess => this.saveSuccessHelper(), error => this.saveFailedHelper(error));
    }
  }
  private saveSuccessHelper() {
    this.isSaving = false;
    if (this.isNew) {
      this.alertService.showSucessMessage("Saved successfully");
    } else {
      this.alertService.showSucessMessage("Updated successfully");
    }
    this.editorModal.hide();
    this.serverCallback();
  }

  private saveFailedHelper(error: any) {
    let test = Utilities.getHttpResponseMessage(error);
    this.alertService.showInfoMessage(test[0]);
  }

  onSuccessful(subCategory: DropDownList[]) {
    this.subcategoryList = subCategory;
    this.loadingIndicator = false;
  }

  onDataLoadFailed() {
    this.alertService.showInfoMessage("Unable to retrieve list from the server");
  }

}
