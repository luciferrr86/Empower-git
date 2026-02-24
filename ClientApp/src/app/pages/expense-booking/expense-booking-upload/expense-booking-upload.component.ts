import { Component, OnInit, Output, EventEmitter } from '@angular/core';

@Component({
  selector: 'expense-booking-upload',
  templateUrl: './expense-booking-upload.component.html',
  styleUrls: ['./expense-booking-upload.component.css']
})
export class ExpenseBookingUploadComponent implements OnInit {
  @Output() getId: EventEmitter<string> = new EventEmitter<string>();;
  public url: string[] = new Array<string>();
  constructor() { }

  ngOnInit() {
  }

  refreshDocument(status: any) {
    status.forEach(item => {
      this.url.push(item.imageUrl);
    });
    this.url.push(status.imageUrl);
    this.getId.emit(status);
  }
}
