import { Component, OnInit, ViewChild, TemplateRef } from '@angular/core';
import { AlertService } from '../../../../services/common/alert.service';
import { DropDownList } from '../../../../models/common/dropdown';
import { ExpenseBookingService } from '../../../../services/expense-booking/expense-booking.service';
import { ExpenseTitleAmountModel, ExpenseTitleAmountViewModel } from '../../../../models/expense-booking/expense-title-amount';
import { IOption } from 'ng-select';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { Utilities } from '../../../../services/common/utilities';


@Component({
  selector: 'app-title-amount',
  templateUrl: './title-amount.component.html',
  styleUrls: ['./title-amount.component.css']
})
export class TitleAmountComponent implements OnInit {

  public isSaving = false;
  private isNew = false;
  titleListDropDown: Array<IOption> = [];
  subcategoryList: Array<IOption> = [];
  titleAmount: ExpenseTitleAmountModel = new ExpenseTitleAmountModel();
  loadingIndicator: boolean = true;

  public modalTitle = "";

  @ViewChild('editorModal')
  editorModal: ModalDirective;
  columns: any[] = [];
  filterQuery: string = "";
  pageNumber = 0;
  count = 0;
  pageSize = 10;

  rows: ExpenseTitleAmountModel[] = [];

  categoreyItem: ExpenseTitleAmountModel;
  titleList: DropDownList[];

  @ViewChild('indexTemplate')
  indexTemplate: TemplateRef<any>;

  @ViewChild('actionsTemplate')
  actionsTemplate: TemplateRef<any>;

  constructor(private bookingService: ExpenseBookingService, private alertService: AlertService) { }

  ngOnInit() {
    this.columns = [
      { prop: "index", name: '#', cellTemplate: this.indexTemplate, canAutoResize: false },
      { prop: 'titleName', name: 'Title' },
      { prop: 'amount', name: 'Amount' },
      { prop: 'Action', name: 'Action', cellTemplate: this.actionsTemplate, resizeable: false, canAutoResize: false, sortable: false, draggable: false }
    ];
    this.getAllItems(this.pageNumber, this.pageSize, this.filterQuery);
  }

  ngAfterViewInit() {

  }
  getAllItems(page?: number, pageSize?: number, name?: string) {
    this.bookingService.getAllTitleAmount(page, pageSize, name).subscribe(result => this.onSuccessfulDataLoad(result), error => this.onDataLoadFailed());
  }


  getFilteredData() {
    this.getAllItems(this.pageNumber, this.pageSize, this.filterQuery);

  }
  getData(e) {
    this.getAllItems(this.pageNumber, e, this.filterQuery);
  }
  setPage(e) {
    this.getAllItems(e.offset, this.pageSize, this.filterQuery);
  }

  onSuccessfulDataLoad(item: ExpenseTitleAmountViewModel) {

    this.rows = item.expenseBookingTitleModel;
    item.expenseBookingTitleModel.forEach((items, index) => {
      (<any>items).index = index + 1;
    });
    this.titleList = item.titleList;
    this.count = item.totalCount;
    this.loadingIndicator = false;
  }

  onDataLoadFailed() {
    this.alertService.showInfoMessage("Unable to retrieve list from the server");
  }
  newTitleAmount() {
    this.modalTitle = "Add ";
    this.isNew = true;
    this.editorModal.show();
    this.titleListDropDown = this.titleList;
    this.titleAmount = new ExpenseTitleAmountModel();
    return this.titleAmount;
  }
  editTitleAmount(item: ExpenseTitleAmountModel) {
    this.modalTitle = "Edit ";
    this.editorModal.show();
    this.titleListDropDown = this.titleList;
    this.isNew = false;
    if (item) {
      this.titleAmount = new ExpenseTitleAmountModel();
      Object.assign(this.titleAmount, item);
      this.titleAmount.titleId = item.titleId.toLowerCase();
      return this.titleAmount;
    }
  }
  deleteTitleAmount(item: ExpenseTitleAmountModel) {
    this.alertService.showConfirm("Are you sure you want to delete?", () => this.deleteItemHelper(item));
  }
  deleteItemHelper(item: ExpenseTitleAmountModel) {
    this.bookingService.deleteTitleAmount(item.id)
      .subscribe(() => {
        this.getAllItems(this.pageNumber, this.pageSize, this.filterQuery);
      },
        () => {
          this.alertService.showInfoMessage("An error occured whilst deleting");
        });
  }

  save() {
    this.isSaving = true;
    if (this.isNew) {
      this.bookingService.createTitleAmount(this.titleAmount).subscribe(sucess => this.saveSuccessHelper(), error => this.saveFailedHelper(error));
    }
    else {
      this.bookingService.updateTitleAmount(this.titleAmount, this.titleAmount.id).subscribe(sucess => this.saveSuccessHelper(), error => this.saveFailedHelper(error));
    }
  }
  private saveSuccessHelper() {
    this.isSaving = false;
    if (this.isNew) {
      this.alertService.showSucessMessage("Saved successfully");
    } else {
      this.alertService.showSucessMessage("Updated successfully");
    }
    this.getAllItems(this.pageNumber, this.pageSize, this.filterQuery);
    this.editorModal.hide();
  }

  private saveFailedHelper(error: any) {
    this.isSaving = false;
    let test = Utilities.getHttpResponseMessage(error);
    this.alertService.showInfoMessage(test[0]);
  }


  onDataSaveFailed() {
    this.alertService.showInfoMessage("Unable to retrieve list from the server");
  }

}
