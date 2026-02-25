import { Injectable, OnDestroy } from '@angular/core';
import { Router } from '@angular/router';
import { BehaviorSubject, fromEvent, Subject } from 'rxjs';
import { takeUntil, debounceTime } from 'rxjs/operators';

//Menu Bar
export interface Menu {
  headTitle?: string,
  path?: string;
  title?: string;
  icon?: string;
  subIcon?: string;
  type?: string;
  badgeType?: string;
  badgeValue?: string;
  badgeClass?: string;
  active?: boolean;
  bookmark?: boolean;
  children?: Menu[];
  Menusub?: boolean;
  selected?: boolean;
  dirchange?: boolean;
}

@Injectable({
  providedIn: 'root'
})

export class NavService implements OnDestroy {
  private unsubscriber: Subject<any> = new Subject();
  public screenWidth: BehaviorSubject<number> = new BehaviorSubject(window.innerWidth);

  public megaMenu: boolean = false;
  public megaMenuCollapse: boolean = window.innerWidth < 1199 ? true : false;
  public collapseSidebar: boolean = window.innerWidth < 991 ? true : false;
  public fullScreen: boolean = false;
  constructor(private router: Router) {
    this.setScreenWidth(window.innerWidth);
    fromEvent(window, 'resize').pipe(
      debounceTime(1000),
      takeUntil(this.unsubscriber)
    ).subscribe((evt: any) => {
      this.setScreenWidth(evt.target.innerWidth);
      if (evt.target.innerWidth < 991) {
        this.collapseSidebar = false;
        this.megaMenu = false;
      }
      if (evt.target.innerWidth < 1199) {
        this.megaMenuCollapse = true;
      }
    });
    if (window.innerWidth < 991) {
      this.router.events.subscribe(event => {
        this.collapseSidebar = false;
        this.megaMenu = false;
      });
    }
  }
  ngOnDestroy() {
    this.unsubscriber.next;
    this.unsubscriber.complete();
  }

  private setScreenWidth(width: number): void {
    this.screenWidth.next(width);
  }

  MENUITEMS: Menu[] = [
    //Title
    { headTitle: 'MAIN' },
    //Title
    { headTitle: 'Web Apps' },
    {
      dirchange: false, title: 'Nested Menu',
      icon: `<svg xmlns="http://www.w3.org/2000/svg" class="side-menu__icon" viewBox="0 0 256 256"><rect width="256" height="256" fill="none"></rect><polygon points="32 80 128 136 224 80 128 24 32 80" opacity="0.2"></polygon><polyline points="32 176 128 232 224 176" fill="none" stroke="currentColor" stroke-linecap="round" stroke-linejoin="round" stroke-width="16"></polyline><polyline points="32 128 128 184 224 128" fill="none" stroke="currentColor" stroke-linecap="round" stroke-linejoin="round" stroke-width="16"></polyline><polygon points="32 80 128 136 224 80 128 24 32 80" fill="none" stroke="currentColor" stroke-linecap="round" stroke-linejoin="round" stroke-width="16"></polygon></svg>`,
      type: 'sub',
      active: false,
      children: [
        {
          title: 'Nested-1',
          dirchange: false,
          subIcon: `<svg xmlns="http://www.w3.org/2000/svg" class="side-menu-doublemenu__icon" viewBox="0 0 256 256"><rect width="256" height="256" fill="none"></rect><rect x="32" y="80" width="160" height="128" rx="8" opacity="0.2"></rect><rect x="32" y="80" width="160" height="128" rx="8" fill="none" stroke="currentColor" stroke-linecap="round" stroke-linejoin="round" stroke-width="16"></rect><path d="M64,48H216a8,8,0,0,1,8,8V176" fill="none" stroke="currentColor" stroke-linecap="round" stroke-linejoin="round" stroke-width="16"></path></svg>`,
          type: 'empty',
          active: false,
          selected: false,
          path: '/nested-menu/nested-1',
        },
        {
          title: 'Nested-2',
          dirchange: false,
          subIcon: `<svg xmlns="http://www.w3.org/2000/svg" class="side-menu-doublemenu__icon" viewBox="0 0 256 256"><rect width="256" height="256" fill="none"></rect><rect x="32" y="80" width="160" height="128" rx="8" opacity="0.2"></rect><rect x="32" y="80" width="160" height="128" rx="8" fill="none" stroke="currentColor" stroke-linecap="round" stroke-linejoin="round" stroke-width="16"></rect><path d="M64,48H216a8,8,0,0,1,8,8V176" fill="none" stroke="currentColor" stroke-linecap="round" stroke-linejoin="round" stroke-width="16"></path></svg>`,
          type: 'sub',
          selected: false,
          children: [
            {
              title: 'Nested-2.1',
              dirchange: false,
              type: 'empty',
              active: false,
              selected: false,
              path: '/nested-menu/nested-2/nested-2-1',
            },
            {
              title: 'Nested-2.2',
              dirchange: false,
              type: 'empty',
              active: false,
              selected: false,
              path: '/nested-menu/nested-2/nested-2-2',
            },
          ],
        },
      ],
    },
  ]

  items = new BehaviorSubject<Menu[]>(this.MENUITEMS);

}
