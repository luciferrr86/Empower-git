import { CategoryInfoComponent } from './../category-info/category-info.component';
import { Component, OnInit, ViewChild, TemplateRef } from '@angular/core';
import { SubCategoryItemModel } from '../../../../models/maintenance/sub-category-item-model';
import { AlertService } from '../../../../services/common/alert.service';;
import { ExpenseBookingService } from '../../../../services/expense-booking/expense-booking.service';
import { ExpenseCategoryViewModel, ExpenseCategoryModel } from '../../../../models/expense-booking/expense-category.model';


@Component({
  selector: 'app-category',
  templateUrl: './category.component.html',
  styleUrls: ['./category.component.css']
})
export class CategoryComponent implements OnInit {


  columns: any[] = [];
  filterQuery: string = "";
  pageNumber = 0;
  count = 0;
  pageSize = 10;

  rows: ExpenseCategoryModel[] = [];
  loadingIndicator: boolean = true;
  categoreyItem: ExpenseCategoryModel;

  @ViewChild('indexTemplate')
  indexTemplate: TemplateRef<any>;
  @ViewChild('createItem')
  createItem: CategoryInfoComponent;
  @ViewChild('actionsTemplate')
  actionsTemplate: TemplateRef<any>;

  constructor(private bookingService: ExpenseBookingService, private alertService: AlertService) { }

  ngOnInit() {
    this.columns = [
      { prop: 'name', name: 'Name' },
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
    this.bookingService.getAllCategory(page, pageSize, name).subscribe(result => this.onSuccessfulDataLoad(result), error => this.onDataLoadFailed());
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

  onSuccessfulDataLoad(item: ExpenseCategoryViewModel) {

    this.rows = item.categoryList;
    item.categoryList.forEach((items, index) => {
      (<any>items).index = index + 1;
    });
    this.count = item.totalCount;
    this.loadingIndicator = false;
  }

  onDataLoadFailed() {
    this.alertService.showInfoMessage("Unable to retrieve list from the server");
  }
  newCategory() {
    this.categoreyItem = this.createItem.addItem();
  }
  editCategory(item: ExpenseCategoryModel) {
    this.categoreyItem = this.createItem.update(item);
  }
  deleteCategory(item: SubCategoryItemModel) {
    this.alertService.showConfirm("Are you sure you want to delete?", () => this.deleteItemHelper(item));
  }
  deleteItemHelper(item: SubCategoryItemModel) {
    this.bookingService.deleteCategory(item.id)
      .subscribe(() => {
        this.getAllItems(this.pageNumber, this.pageSize, this.filterQuery);
      },
        () => {
          this.alertService.showInfoMessage("An error occured whilst deleting");
        });
  }

}
