import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'blog',
  template: '<router-outlet><app-spinner></app-spinner></router-outlet>'
})
export class BlogComponent implements OnInit {

  constructor() { }

  ngOnInit() {
  }

}
