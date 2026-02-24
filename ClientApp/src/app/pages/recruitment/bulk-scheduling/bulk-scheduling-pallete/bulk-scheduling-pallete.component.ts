import { Component, OnInit } from '@angular/core';
import { animate, style, transition, trigger } from '@angular/animations';

@Component({
  selector: 'app-bulk-scheduling-pallete',
  templateUrl: './bulk-scheduling-pallete.component.html',
  styleUrls: ['./bulk-scheduling-pallete.component.css'],
  animations: [
    trigger('fadeInOutTranslate', [
      transition(':enter', [
        style({ opacity: 0 }),
        animate('400ms ease-in-out', style({ opacity: 1 }))
      ]),
      transition(':leave', [
        style({ transform: 'translate(0)' }),
        animate('400ms ease-in-out', style({ opacity: 0 }))
      ])
    ])
  ]
})
export class BulkSchedulingPalleteComponent implements OnInit {
  massId: string = "";
  constructor() { }

  ngOnInit() {
  }
  getId(event) {
    this.massId = event;
  }

}
