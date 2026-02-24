import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'maintenance-user',
  template: '<router-outlet><app-spinner></app-spinner></router-outlet>'
})
export class MaintenanceComponent implements OnInit {

  constructor() { }

  ngOnInit() {
  }

}
