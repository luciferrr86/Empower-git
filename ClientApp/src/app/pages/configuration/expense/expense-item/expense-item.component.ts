import { Component, OnInit, ViewChild, TemplateRef } from '@angular/core';
import { ExpenseItemInfoComponent } from '../expense-item-info/expense-item-info.component';
import { SubCategoryItemModel, SubcategoryViewModel } from '../../../../models/maintenance/sub-category-item-model';
import { AlertService } from '../../../../services/common/alert.service';
import { DropDownList } from '../../../../models/common/dropdown';
import { ExpenseBookingService } from '../../../../services/expense-booking/expense-booking.service';

@Component({
  selector: 'app-expense-item',
  templateUrl: './expense-item.component.html',
  styleUrls: ['./expense-item.component.css']
})
export class ExpenseItemComponent implements OnInit {

  columns: any[] = [];
  filterQuery: string = "";
  pageNumber = 0;
  count = 0;
  pageSize = 10;

  rows: SubCategoryItemModel[] = [];
  loadingIndicator: boolean = true;
  categoreyItem: SubCategoryItemModel;
  categeroyList: DropDownList[];

  @ViewChild('indexTemplate')
  indexTemplate: TemplateRef<any>;
  @ViewChild('createItem')
  createItem: ExpenseItemInfoComponent;
  @ViewChild('actionsTemplate')
  actionsTemplate: TemplateRef<any>;

  constructor(private createExpenseItemService: ExpenseBookingService, private alertService: AlertService) { }

  ngOnInit() {
    this.columns = [
      { prop: 'name', name: 'Item Name' },
      { prop: 'subCategory', name: 'Sub Category' },
      { prop: 'category', name: 'Category' },
      { prop: 'Action', name: 'Action', cellTemplate: this.actionsTemplate, resizeable: false, canAutoResize: false, sortable: false, draggable: false }
    ];
    this.getAllItems(this.pageNumber, this.pageSize, this.filterQuery);
  }

  ngAfterViewInit() {
    this.createItem.serverCallback = () => {
      this.getAllItems(this.pageNumber, this.pageSize, this.filterQuery);
    };
  }
  getAllItems(page?: number, pageSize?: number, name?: string) {
    this.createExpenseItemService.getAllExpenseBookingItem(page, pageSize, name).subscribe(result => this.onSuccessfulDataLoad(result), error => this.onDataLoadFailed(error));
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

  onSuccessfulDataLoad(item: SubcategoryViewModel) {

    this.rows = item.subCategoryItemList;
    item.subCategoryItemList.forEach((items, index, item) => {
      (<any>items).index = index + 1;
    });
    this.categeroyList = item.categoryList;
    this.count = item.totalCount;
    this.loadingIndicator = false;
  }

  onDataLoadFailed(error: any) {
    this.alertService.showInfoMessage("Unable to retrieve list from the server");
  }
  newItem() {
    this.categoreyItem = this.createItem.addItem(this.categeroyList);
  }
  editItem(item: SubCategoryItemModel) {
    this.categoreyItem = this.createItem.update(item, this.categeroyList);
  }
  deleteItem(item: SubCategoryItemModel) {
    this.alertService.showConfirm("Are you sure you want to delete?", () => this.deleteItemHelper(item));
  }
  deleteItemHelper(item: SubCategoryItemModel) {
    this.createExpenseItemService.deleteExpenseItem(item.id)
      .subscribe(results => {
        this.getAllItems(this.pageNumber, this.pageSize, this.filterQuery);
      },
        error => {
          this.alertService.showInfoMessage("An error occured whilst deleting");
        });
  }
}
