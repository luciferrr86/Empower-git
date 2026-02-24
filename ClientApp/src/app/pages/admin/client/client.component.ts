import { Component, OnInit } from '@angular/core';
import { Client } from '../../../models/admin/client.model';
import { AdminConfigService } from '../../../services/admin/admin-config.service';
import { AlertService } from '../../../services/common/alert.service';
import { Utilities } from '../../../services/common/utilities';

@Component({
  selector: 'app-client',
  templateUrl: './client.component.html',
  styleUrls: ['./client.component.css']
})
export class ClientComponent implements OnInit {

  public isSaving = false;
  public client: Client = new Client();
  constructor(private adminService: AdminConfigService, private alertService: AlertService) { }

  ngOnInit() {
    this.getClient();
  }

  getClient() {
    this.adminService.getClient().subscribe(result => this.onSuccessfulDataLoad(result), error => this.onDataLoadFailed(error));
  }
  onSuccessfulDataLoad(client: Client) {
    this.client = client;
  }

  onDataLoadFailed(error: any) {
    //this.alertService.showInfoMessage("Unable to retrieve  from the server");
  }

  save() {
    this.isSaving = true;
    this.adminService.createClient(this.client).subscribe(sucess => this.saveSuccessHelper(sucess), error => this.saveFailedHelper(error));
  }

  saveSuccessHelper(result?: string) {
    this.isSaving = false;
    this.alertService.showSucessMessage("Employee created successfully");
  }
  saveFailedHelper(error: any) {
    let test = Utilities.getHttpResponseMessage(error);
    this.alertService.showInfoMessage(test[0]);
    this.isSaving = false;
  }
}
