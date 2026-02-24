import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { DashboardService } from '../../services/dashboard/dashboard.service';
import { DashboardModel } from '../../models/dashboard/dashboard.model';
import { AlertService } from '../../services/common/alert.service';


@Component({
  selector: 'app-dashboard',
  template: '<router-outlet><app-spinner></app-spinner></router-outlet>'
})
export class DashboardComponent implements OnInit {

  constructor(private router: Router,private dashboardService: DashboardService, private alertService: AlertService) { }
 
  ngOnInit() {
  
  }


  

 
  
}
