import { Component, OnInit, ViewChild, TemplateRef } from '@angular/core';
import { AlertService } from '../../../../services/common/alert.service';
import { DropDownList } from '../../../../models/common/dropdown';
import { ExpenseBookingService } from '../../../../services/expense-booking/expense-booking.service';
import { ExpenseSubCategoryModel, ExpenseSubCategoryViewModel } from '../../../../models/expense-booking/expense-subcategory.model';
import { SubCategoryInfoComponent } from '../sub-category-info/sub-category-info.component';


@Component({
  selector: 'app-sub-category',
  templateUrl: './sub-category.component.html',
  styleUrls: ['./sub-category.component.css']
})
export class SubCategoryComponent implements OnInit {


  columns: any[] = [];
  filterQuery: string = "";
  pageNumber = 0;
  count = 0;
  pageSize = 10;

  rows: ExpenseSubCategoryModel[] = [];
  loadingIndicator: boolean = true;
  categoreyItem: ExpenseSubCategoryModel;
  categeroyList: DropDownList[];

  @ViewChild('indexTemplate')
  indexTemplate: TemplateRef<any>;
  @ViewChild('createItem')
  createItem: SubCategoryInfoComponent;
  @ViewChild('actionsTemplate')
  actionsTemplate: TemplateRef<any>;

  constructor(private bookingService: ExpenseBookingService, private alertService: AlertService) { }

  ngOnInit() {
    this.columns = [
      { prop: 'name', name: 'Name' },
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
    this.bookingService.getAllSubCategory(page, pageSize, name).subscribe(result => this.onSuccessfulDataLoad(result), error => this.onDataLoadFailed());
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

  onSuccessfulDataLoad(item: ExpenseSubCategoryViewModel) {

    this.rows = item.subCategoryList;
    item.subCategoryList.forEach((items, index) => {
      (<any>items).index = index + 1;
    });
    this.categeroyList = item.categoryList;
    this.count = item.totalCount;
    this.loadingIndicator = false;
  }

  onDataLoadFailed() {
    this.alertService.showInfoMessage("Unable to retrieve list from the server");
  }
  newSubCategory() {
    this.categoreyItem = this.createItem.addItem(this.categeroyList);
  }
  editSubCategory(item: ExpenseSubCategoryModel) {
    this.categoreyItem = this.createItem.update(item, this.categeroyList);
  }
  deleteSubCategory(item: ExpenseSubCategoryModel) {
    this.alertService.showConfirm("Are you sure you want to delete?", () => this.deleteItemHelper(item));
  }
  deleteItemHelper(item: ExpenseSubCategoryModel) {
    this.bookingService.deleteSubCategory(item.id)
      .subscribe(() => {
        this.getAllItems(this.pageNumber, this.pageSize, this.filterQuery);
      },
        () => {
          this.alertService.showInfoMessage("An error occured whilst deleting");
        });
  }

}
