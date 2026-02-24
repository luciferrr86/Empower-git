import { Component, OnInit, ViewChild, TemplateRef } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { FunctionalTitleInfoComponent } from '../functional-title-info/functional-title-info.component';
import { FunctionalTitle, FunctionalTitleModel } from '../../../models/maintenance/functional-title.model';
import { TitleService } from '../../../services/maintenance/title.service';
import { AlertService } from '../../../services/common/alert.service';

@Component({
  selector: 'functional-title',
  templateUrl: './functional-title.component.html',
  styleUrls: ['./functional-title.component.css']
})
export class FunctionalTitleComponent implements OnInit {

  columns: any[] = [];
  filterQuery: string = "";
  pageNumber = 0;
  count = 0;
  pageSize = 10;

  rows: FunctionalTitle[] = [];
  rowsCache: FunctionalTitle[] = [];
  editedTitle: FunctionalTitle;
  sourceTitle: FunctionalTitle;
  editingTitleName: { name: string };
  loadingIndicator: boolean = true;

  @ViewChild('indexTemplate')
  indexTemplate: TemplateRef<any>;
  @ViewChild('titleEditor')
  titleEditor: FunctionalTitleInfoComponent;
  @ViewChild('actionsTemplate')
  actionsTemplate: TemplateRef<any>;

  constructor(private FunctionalTitleService: TitleService, private alertService: AlertService) {

  }

  ngOnInit() {
    this.columns = [
      { prop: "index", name: '#', cellTemplate: this.indexTemplate, canAutoResize: false },
      { prop: 'name', name: 'Name' },
      { prop: 'Action', name: 'Action', cellTemplate: this.actionsTemplate, resizeable: false, canAutoResize: false, sortable: false, draggable: false }
    ];
    this.getAllTitle(this.pageNumber, this.pageSize, this.filterQuery);
  }

  ngAfterViewInit() {
    this.titleEditor.serverCallback = () => {
      this.getAllTitle(this.pageNumber, this.pageSize, this.filterQuery);
    };
  }

  getAllTitle(page?: number, pageSize?: number, name?: string) {
    this.FunctionalTitleService.getAllTitle(page, pageSize, name).subscribe(result => this.onSuccessfulDataLoad(result), error => this.onDataLoadFailed(error));
  }

  getFilteredData(filterString) {
    this.getAllTitle(this.pageNumber, this.pageSize, this.filterQuery);

  }
  getData(e) {
    this.getAllTitle(this.pageNumber, e, this.filterQuery);
  }
  setPage(e) {
    this.getAllTitle(e.offset, this.pageSize, this.filterQuery);
  }

  onSuccessfulDataLoad(titles: FunctionalTitleModel) {

    this.rowsCache = [...titles.functionalTitleModel];
    this.rows = titles.functionalTitleModel;
    titles.functionalTitleModel.forEach((title, index, titles) => {
      (<any>title).index = index + 1;
    });
    this.count = titles.totalCount;
    this.loadingIndicator = false;
  }

  onDataLoadFailed(error: any) {
    this.alertService.showInfoMessage("Unable to retrieve list from the server");
  }

  addTitle() {
    this.editedTitle = this.titleEditor.newTitles();
  }
  editTitle(title: FunctionalTitle) {
    this.editedTitle = this.titleEditor.updateTitle(title);
  }
  deleteTitle(title: FunctionalTitle) {
    this.alertService.showConfirm("Are you sure you want to delete?", () => this.deleteTitleHelper(title));

  }
  deleteTitleHelper(title: FunctionalTitle) {
    this.FunctionalTitleService.delete(title.id)
      .subscribe(results => {
        this.getAllTitle(this.pageNumber, this.pageSize, this.filterQuery);        
        this.alertService.showSucessMessage('Deleted successfully.');
      },
        error => {
          this.alertService.showInfoMessage('An error occured whilst deleting.');         
        });
  }
}
