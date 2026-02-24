import { Component, OnInit, ViewChild } from '@angular/core';
import { IOption } from 'ng-select';
import { LeaveRules } from '../../../../models/configuration/leave/leave-rules.model';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { LeaveRulesService } from '../../../../services/configuration/leave/leave-rules.service';
import { BandService } from '../../../../services/maintenance/band.service';
import { LeaveTypeService } from '../../../../services/configuration/leave/leave-type.service';
import { AlertService } from '../../../../services/common/alert.service';
import { DropDownList } from '../../../../models/common/dropdown';
import { Utilities } from '../../../../services/common/utilities';

@Component({
  selector: 'leave-rules-info',
  templateUrl: './leave-rules-info.component.html',
  styleUrls: ['./leave-rules-info.component.css']
})
export class LeaveRulesInfoComponent implements OnInit {

bandlist :Array<IOption> = [];
leavetypelist :Array<IOption> = [];
public isSaving = false;
private isNew = false;
public modalTitle = "";
public rulesEdit : LeaveRules = new LeaveRules();
public serverCallback: () => void;

@ViewChild('editorModal')
editorModal: ModalDirective;


  constructor(private leaveruleService:LeaveRulesService , private alertService: AlertService ) { }

  ngOnInit() {
  }

  addleaveRule(allBand:DropDownList[],allLeaveType:DropDownList[]){
   this.modalTitle = "Add";
   this.editorModal.show();
   this.isNew = true;  
   this.bandlist = allBand;
   this.leavetypelist = allLeaveType;
   this.rulesEdit = new LeaveRules();
    return this.rulesEdit;
  }

  updateleaveRule(allBand:DropDownList[],allLeaveType :DropDownList[],leaveRule :LeaveRules){
    this.modalTitle = "Edit";
    this.editorModal.show();
    this.isNew = false;
    this.bandlist = allBand;
    this.leavetypelist = allLeaveType;
    if(leaveRule){
      this.rulesEdit = new LeaveRules();
      (<any>Object).assign(this.rulesEdit,leaveRule);
      return leaveRule;
    }
  }

    public save() {
      this.isSaving = true;
      if(this.isNew){
        this.leaveruleService.create(this.rulesEdit).subscribe(sucess => this.saveSuccessHelper(), error => this.saveFailedHelper(error));
      }
      else
      {
         this.leaveruleService.update(this.rulesEdit,this.rulesEdit.id).subscribe(sucess => this.saveSuccessHelper(), error => this.saveFailedHelper(error));
      }
    }


    private saveSuccessHelper(){
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
      this.isSaving = false;
      let test=Utilities.getHttpResponseMessage(error);
      this.alertService.showInfoMessage("Please try later"+test[0]);
    }

  

}
