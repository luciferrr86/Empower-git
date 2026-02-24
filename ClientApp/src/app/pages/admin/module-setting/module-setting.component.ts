import { Component, OnInit } from '@angular/core';
import { AdminConfigService } from '../../../services/admin/admin-config.service';
import { AlertService } from '../../../services/common/alert.service';
import { ApplicationModule } from '../../../models/admin/application-module.model';


@Component({
  selector: 'app-module-setting',
  templateUrl: './module-setting.component.html',
  styleUrls: ['./module-setting.component.css']
})
export class ModuleSettingComponent implements OnInit {
multiple:boolean;
public appModule:ApplicationModule[];
  constructor(private alertService: AlertService,private adminConfigService:AdminConfigService) { }

  ngOnInit() {
    this.multiple=true;
    this.getAdminSettings();
  }

  getAdminSettings()
  {
    this.adminConfigService.getAdminSettings().subscribe(result => this.onSuccessfulDataLoad(result), error => this.onDataLoadFailed(error));
  }

  onSuccessfulDataLoad(applicationModule:ApplicationModule[]) {   
    this.appModule=applicationModule;
  }

  onDataLoadFailed(error: any) {
    this.alertService.showInfoMessage("Unable to retrieve list from the server");
  }

  onModuleChange(enable:boolean,id:string)
  {
    this.adminConfigService.updateModule(enable,id).subscribe(sucess => this.getAdminSettings(), error => this.onDataLoadFailed(error));
  }

  onSubModuleChange(enable:boolean,id:string)
  {   
    this.adminConfigService.updateSubModule(enable,id).subscribe(sucess => this.getAdminSettings(), error => this.onDataLoadFailed(error));
  }
}
