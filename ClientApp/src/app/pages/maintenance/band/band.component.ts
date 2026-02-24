import { Component, OnInit, ViewChild, TemplateRef } from '@angular/core';
import { Band, BandModel } from '../../../models/maintenance/band.model';
import { BandInfoComponent } from '../band-info/band-info.component';
import { BandService } from '../../../services/maintenance/band.service';
import { AlertService } from '../../../services/common/alert.service';

@Component({
  selector: 'band',
  templateUrl: './band.component.html',
  styleUrls: ['./band.component.css']
})
export class BandComponent implements OnInit {

  columns: any[] = [];
  filterQuery: string = "";
  pageNumber = 0;
  count = 0;
  pageSize = 10;

  rows: Band[] = [];
  rowsCache: Band[] = [];
  editedBand: Band;
  sourceBand: Band;
  editingBand: { name: string };
  loadingIndicator: boolean = true;

  @ViewChild('indexTemplate')
  indexTemplate: TemplateRef<any>;
  @ViewChild('bandEditor')
  bandEditor: BandInfoComponent;
  @ViewChild('actionsTemplate')
  actionsTemplate: TemplateRef<any>;

  constructor(private bandService: BandService, private alertService: AlertService) {
  }

  ngOnInit() {
    this.columns = [
      { prop: "index", name: '#', cellTemplate: this.indexTemplate, canAutoResize: false },
      { prop: 'name', name: 'Name' },
      { prop: 'Action', name: 'Action', cellTemplate: this.actionsTemplate, resizeable: false, canAutoResize: false, sortable: false, draggable: false }
    ];
    this.getAllBand(this.pageNumber, this.pageSize, this.filterQuery);
  }
  ngAfterViewInit() {
    this.bandEditor.serverCallback = () => {
      this.getAllBand(this.pageNumber, this.pageSize, this.filterQuery);
    };
  }
  getAllBand(page?: number, pageSize?: number, name?: string) {
    this.bandService.getAllBand(page, pageSize, name).subscribe(result => this.onSuccessfulDataLoad(result), error => this.onDataLoadFailed(error));
  }


  getFilteredData(filterString) {
    this.getAllBand(this.pageNumber, this.pageSize, filterString);

  }
  getData(e) {
    this.getAllBand(this.pageNumber, e, this.filterQuery);
  }
  setPage(e) {
    this.getAllBand(e.offset, this.pageSize, this.filterQuery);
  }

  onSuccessfulDataLoad(bands: BandModel) {

    this.rowsCache = [...bands.bandModel];
    this.rows = bands.bandModel;
    bands.bandModel.forEach((band, index, departments) => {
      (<any>band).index = index + 1;
    });
    this.count = bands.totalCount;
    this.loadingIndicator = false;
  }

  onDataLoadFailed(error: any) {
    this.alertService.showInfoMessage("Unable to retrieve list from the server");
  }


  newBand() {
    this.editedBand = this.bandEditor.addBand();
  }

  editBand(band: Band) {
    this.editedBand = this.bandEditor.updateBand(band);
  }

  deleteBand(band: Band) {
    this.alertService.showConfirm("Are you sure you want to delete?", () => this.deleteBandHelper(band));
  }
  deleteBandHelper(band: Band) {
    this.bandService.delete(band.id)
      .subscribe(results => {
        this.getAllBand(this.pageNumber, this.pageSize, this.filterQuery);
        this.alertService.showSucessMessage('Deleted successfully.');
      },
        error => {
          this.alertService.showInfoMessage('An error occured whilst deleting.');
        });
  }
}
