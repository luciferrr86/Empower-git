import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'configuration',
  template: '<router-outlet><app-spinner></app-spinner></router-outlet>'
})
export class ConfigurationComponent implements OnInit {

  constructor() { }

  ngOnInit() {
  }

}