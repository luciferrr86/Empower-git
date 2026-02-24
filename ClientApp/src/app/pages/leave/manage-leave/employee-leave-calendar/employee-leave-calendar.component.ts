import { Component, OnInit, ViewChild } from '@angular/core';
import { CalendarComponent } from 'ng-fullcalendar';
import { Options } from 'fullcalendar';
import { LeaveCalender } from '../../../../models/leave/leave-calender.model';
import { AccountService } from '../../../../services/account/account.service';
import { ManageLeaveService } from '../../../../services/leave/manage-leave.service';
import { AlertService } from '../../../../services/common/alert.service';

@Component({
  selector: 'employee-leave-calendar',
  templateUrl: './employee-leave-calendar.component.html',
  styleUrls: ['./employee-leave-calendar.component.css']
})
export class EmployeeLeaveCalendarComponent implements OnInit {
  calendarOptions: Options;
  @ViewChild('myCalendar') calendar: CalendarComponent;
  displayEvent: any;
  events: any;
  calender: LeaveCalender;
  constructor(private manageLeaveService: ManageLeaveService, private accountService: AccountService, private alertService: AlertService) {
    this.getCalenderEvents();
  }

  ngOnInit() {
    this.calendarOptions = {
      editable: false,
      eventLimit: false,
      header: {
        left: 'prev,next today',
        center: 'title',
        right: 'month,agendaWeek,agendaDay,listMonth'
      },
      events: this.events,
    };
  }
  clickButton() {
    this.getCalenderEvents();
  }

  getCalenderEvents() {
    this.manageLeaveService.getCalendersManager(this.accountService.currentUser.id).subscribe(result => this.onSuccessfulDataLoad(result), error => this.onDataLoadFailed());
  }
  onSuccessfulDataLoad(events: LeaveCalender[]) {
    this.events = events;
    this.calendar.fullCalendar('removeEvents');
    events.forEach(el => {
      this.calendar.fullCalendar('renderEvent', el);
    });
    this.calendar.fullCalendar('rerenderEvents');
  }
  onDataLoadFailed() {
    this.alertService.showInfoMessage("Unable to retrieve list from the server");
  }

  loadevents(events: LeaveCalender) {
    let data: Array<Object> = [{
      title: events.title,
      start: events.start,
      end: events.end,
      status: events.status,
      allDay: events.allDay,
      backgroundColor: events.backgroundColor
    }];
    data.forEach(() => {
      this.calender = events;
    });
    this.events = data;
  }
  eventClick(model: any) {
    model = {
      event: {
        id: model.event.id,
        start: model.event.start,
        end: model.event.end,
        title: model.event.title,
        allDay: model.event.allDay
        // other params
      },
      duration: {}
    }
    const dateObj = new Date(model.event.start);
    const dateObjEnd = new Date(model.event.end);

    var start = dateObj.getDate() + "/" + (dateObj.getMonth() + 1) + "/" + dateObj.getFullYear();
    var end = (dateObjEnd.getDate() - 1) + "/" + (dateObjEnd.getMonth() + 1) + "/" + dateObjEnd.getFullYear();
    if (dateObjEnd.getFullYear() == 1970) {
      this.alertService.showInfoMessage(model.event.title);
    }
    else {
      this.alertService.showInfoMessage(model.event.title + " From:" + start + " To:" + end);
    }

  }
}
