import { Component, OnInit, ViewChild } from '@angular/core';
import { IOption } from 'ng-select';
import { ExpenseBookingService } from '../../../../services/expense-booking/expense-booking.service';
import { SubCategoryItemModel } from '../../../../models/maintenance/sub-category-item-model';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { DropDownList } from '../../../../models/common/dropdown';
import { Utilities } from '../../../../services/common/utilities';
import { AlertService } from '../../../../services/common/alert.service';


@Component({
  selector: 'expense-item-info',
  templateUrl: './expense-item-info.component.html',
  styleUrls: ['./expense-item-info.component.css']
})
export class ExpenseItemInfoComponent implements OnInit {


  public isSaving = false;
  private isNew = false;
  ctegoryList: Array<IOption> = [];
  subcategoryList: Array<IOption> = [];
  categoryItem: SubCategoryItemModel = new SubCategoryItemModel();
  loadingIndicator: boolean = true;

  public serverCallback: () => void;
  public modalTitle = "";

  @ViewChild('editorModal')
  editorModal: ModalDirective;

  constructor(private createExpenseItemService: ExpenseBookingService, private alertService: AlertService) { }

  ngOnInit() {
  }
  addItem(categeroyList?: DropDownList[]) {
    this.modalTitle = "Add ";
    this.isNew = true;
    this.editorModal.show();
    this.ctegoryList = categeroyList;
    this.categoryItem = new SubCategoryItemModel();
    return this.categoryItem;
  }
  update(item: SubCategoryItemModel, categeroyList?: DropDownList[]) {
    this.modalTitle = "Edit ";
    this.editorModal.show();
    this.ctegoryList = categeroyList;
    this.isNew = false;
    if (item) {
      this.categoryItem = new SubCategoryItemModel();
      this.subCategoryList(item.categoryId);
      Object.assign(this.categoryItem, item);
      this.categoryItem.categoryId = item.categoryId.toLowerCase();
      this.categoryItem.subCategoryId = item.subCategoryId.toLowerCase();
      return this.categoryItem;
    }
  }
  subCategoryList(id?: string) {
    this.createExpenseItemService.getSubCategory(id).subscribe(result => this.onSuccessful(result), error => this.onDataLoadFailed(error));
  }
  save() {
    this.isSaving = true;
    if (this.isNew) {
      this.createExpenseItemService.create(this.categoryItem).subscribe(sucess => this.saveSuccessHelper(sucess), error => this.saveFailedHelper(error));
    }
    else {
      this.createExpenseItemService.update(this.categoryItem, this.categoryItem.id).subscribe(sucess => this.saveSuccessHelper(sucess), error => this.saveFailedHelper(error));
    }
  }
  private saveSuccessHelper(result?: string) {
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

  onDataLoadFailed(error: any) {
    this.alertService.showInfoMessage("Unable to retrieve list from the server");
  }

}
