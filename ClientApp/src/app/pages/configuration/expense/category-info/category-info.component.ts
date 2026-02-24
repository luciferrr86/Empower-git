import { Component, OnInit, ViewChild } from '@angular/core';
import { IOption } from 'ng-select';
import { ExpenseBookingService } from '../../../../services/expense-booking/expense-booking.service';
import { SubCategoryItemModel } from '../../../../models/maintenance/sub-category-item-model';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { DropDownList } from '../../../../models/common/dropdown';
import { Utilities } from '../../../../services/common/utilities';
import { AlertService } from '../../../../services/common/alert.service';
import { ExpenseCategoryModel } from '../../../../models/expense-booking/expense-category.model';

@Component({
  selector: 'category-info',
  templateUrl: './category-info.component.html',
  styleUrls: ['./category-info.component.css']
})
export class CategoryInfoComponent implements OnInit {


  public isSaving = false;
  private isNew = false;
  ctegoryList: Array<IOption> = [];
  subcategoryList: Array<IOption> = [];
  categoryItem: ExpenseCategoryModel = new ExpenseCategoryModel();
  loadingIndicator: boolean = true;

  public serverCallback: () => void;
  public modalTitle = "";

  @ViewChild('editorModal')
  editorModal: ModalDirective;

  constructor(private bookingService: ExpenseBookingService, private alertService: AlertService) { }

  ngOnInit() {
  }
  addItem() {
    this.modalTitle = "Add ";
    this.isNew = true;
    this.editorModal.show();
    this.categoryItem = new ExpenseCategoryModel();
    return this.categoryItem;
  }
  update(item: ExpenseCategoryModel) {
    this.modalTitle = "Edit ";
    this.editorModal.show();
    this.isNew = false;
    if (item) {
      this.categoryItem = new SubCategoryItemModel();
      Object.assign(this.categoryItem, item);
      return this.categoryItem;
    }
  }

  save() {
    this.isSaving = true;
    if (this.isNew) {
      this.bookingService.createCategory(this.categoryItem).subscribe(sucess => this.saveSuccessHelper(sucess), error => this.saveFailedHelper(error));
    }
    else {
      this.bookingService.updateCategory(this.categoryItem, this.categoryItem.id).subscribe(sucess => this.saveSuccessHelper(sucess), error => this.saveFailedHelper(error));
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
