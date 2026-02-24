import { Component, OnInit } from '@angular/core';
import { TimesheetConfigurationService } from '../../../../services/configuration/timesheet/timesheet-configuration.service';
import { AlertService } from '../../../../services/common/alert.service';
import { TimesheetConfiguration } from '../../../../models/configuration/timesheet/timesheet-configuration.model';
import { Utilities } from '../../../../services/common/utilities';

@Component({
  selector: 'timesheet-configuration',
  templateUrl: './timesheet-configuration.component.html',
  styleUrls: ['./timesheet-configuration.component.css']
})
export class TimesheetConfigurationComponent implements OnInit {

  configuration: TimesheetConfiguration = new TimesheetConfiguration();
  loadingIndicator: boolean = true;
  constructor(private configurationService: TimesheetConfigurationService, private alertService: AlertService) { }

  ngOnInit() {
    this.getConfiguration();
  }
  getConfiguration() {
    this.configurationService.getconfiguration().subscribe(result => this.onSuccessfulDataLoad(result), error => this.onDataLoadFailed(error));
  }
  onSuccessfulDataLoad(configurations: TimesheetConfiguration) {
    this.configuration = configurations;
    this.loadingIndicator = false;
  }

  onDataLoadFailed(error: any) {
    this.alertService.showInfoMessage("Unable to retrieve list from the server");
    this.loadingIndicator = false;
  }

  public save() {
    if (this.configuration.id == null) {
      this.configurationService.create(this.configuration).subscribe(sucess => this.saveSuccessHelper(sucess), error => this.saveFailedHelper(error));
    }
    else {
      this.configurationService.update(this.configuration, this.configuration.id).subscribe(sucess => this.saveSuccessHelper(sucess), error => this.saveFailedHelper(error));
    }
  }
  private saveSuccessHelper(result?: string) {
    if (this.configuration.id == null) {
      this.alertService.showSucessMessage("Saved successfully");
    } else {
      this.alertService.showSucessMessage("Updated successfully");
    }
    this.getConfiguration();
  }

  private saveFailedHelper(error: any) {

    let test = Utilities.getHttpResponseMessage(error);
    this.alertService.showInfoMessage("Please try later" + test[0]);
  }
}
