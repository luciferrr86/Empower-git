import { Component, ElementRef, OnInit, ViewChild } from "@angular/core";
import { animate, AUTO_STYLE, state, style, transition, trigger, } from "@angular/animations";
import { MenuItems } from "../../shared/menu-items/menu-items";
import { ToastyService, ToastyConfig, ToastOptions, ToastData, } from "ng2-toasty";
import { Router } from "@angular/router";
import { AuthService } from "../../services/common/auth.service";
import { MessageSeverity, AlertService, AlertMessage, AlertDialog, DialogType, } from "../../services/common/alert.service";
import { Permission } from "../../models/account/permission.model";
import { AccountService } from "../../services/account/account.service";
import { ProfileDetail } from "../../models/profile/profile.model";
import { ProfileService } from "../../services/maintenance/profile.service";

@Component({
  selector: "app-admin",
  templateUrl: "./admin.component.html",
  animations: [
    trigger("mobileMenuTop", [
      state(
        "no-block, void",
        style({
          overflow: "hidden",
          height: "0px",
        })
      ),
      state(
        "yes-block",
        style({
          height: AUTO_STYLE,
        })
      ),
      transition("no-block <=> yes-block", [animate("400ms ease-in-out")]),
    ]),
    trigger("slideInOut", [
      state(
        "in",
        style({
          transform: "translate3d(0, 0, 0)",
        })
      ),
      state(
        "out",
        style({
          transform: "translate3d(100%, 0, 0)",
        })
      ),
      transition("in => out", animate("400ms ease-in-out")),
      transition("out => in", animate("400ms ease-in-out")),
    ]),
    trigger("slideOnOff", [
      state(
        "on",
        style({
          transform: "translate3d(0, 0, 0)",
        })
      ),
      state(
        "off",
        style({
          transform: "translate3d(100%, 0, 0)",
        })
      ),
      transition("on => off", animate("400ms ease-in-out")),
      transition("off => on", animate("400ms ease-in-out")),
    ]),
    trigger("fadeInOutTranslate", [
      transition(":enter", [
        style({ opacity: 0 }),
        animate("400ms ease-in-out", style({ opacity: 1 })),
      ]),
      transition(":leave", [
        style({ transform: "translate(0)" }),
        animate("400ms ease-in-out", style({ opacity: 0 })),
      ]),
    ]),
  ],
})
export class AdminComponent implements OnInit
{
  navType: string; /* st1, st2(default), st3, st4 */
  themeLayout: string; /* vertical(default) */
  layoutType: string; /* dark, light */
  verticalPlacement: string; /* left(default), right */
  verticalLayout: string; /* wide(default), box */
  deviceType: string; /* desktop(default), tablet, mobile */
  verticalNavType: string; /* expanded(default), offcanvas */
  verticalEffect: string; /* shrink(default), push, overlay */
  vNavigationView: string; /* view1(default) */
  pcodedHeaderPosition: string; /* fixed(default), relative*/
  pcodedSidebarPosition: string; /* fixed(default), absolute*/
  headerTheme: string; /* theme1(default), theme2, theme3, theme4, theme5, theme6 */
  logoTheme: string; /* theme1(default), theme2, theme3, theme4, theme5, theme6 */

  innerHeight: string;
  windowWidth: number;

  toggleOn: boolean;

  headerFixedMargin: string;
  navBarTheme: string; /* theme1, themelight1(default)*/
  activeItemTheme: string; /* theme1, theme2, theme3, theme4(default), ..., theme11, theme12 */

  isCollapsedMobile: string;
  isCollapsedSideBar: string;

  chatToggle: string;
  chatToggleInverse: string;
  chatInnerToggle: string;
  chatInnerToggleInverse: string;

  menuTitleTheme: string; /* theme1, theme2, theme3, theme4, theme5(default), theme6 */
  itemBorder: boolean;
  itemBorderStyle: string; /* none(default), solid, dotted, dashed */
  subItemBorder: boolean;
  subItemIcon: string; /* style1, style2, style3, style4, style5, style6(default) */
  dropDownIcon: string; /* style1(default), style2, style3 */
  configOpenRightBar: string;
  isSidebarChecked: boolean;
  isHeaderChecked: boolean;

  public isAdminProfilePic: boolean = true;
  public profileEdit: ProfileDetail = new ProfileDetail();

  @ViewChild("searchFriends") search_friends: ElementRef;
  /*  @ViewChild('toggleButton') toggle_button: ElementRef;
    @ViewChild('sideMenu') side_menu: ElementRef;*/

  constructor(
    public menuItems: MenuItems,
    private accountService: AccountService,
    private authService: AuthService,
    public router: Router,
    private profileService: ProfileService
  )
  {
    this.navType = "st2";
    this.themeLayout = "vertical";
    this.vNavigationView = "view1";
    this.verticalPlacement = "left";
    this.verticalLayout = "wide";
    this.deviceType = "desktop";
    this.verticalNavType = "expanded";
    this.verticalEffect = "shrink";
    this.pcodedHeaderPosition = "fixed";
    this.pcodedSidebarPosition = "fixed";
    this.headerTheme = "theme1";
    this.logoTheme = "theme1";

    this.toggleOn = true;

    this.headerFixedMargin = "80px";
    this.navBarTheme = "themelight1";
    this.activeItemTheme = "theme4";

    this.isCollapsedMobile = "no-block";
    this.isCollapsedSideBar = "no-block";

    this.chatToggle = "out";
    this.chatToggleInverse = "in";
    this.chatInnerToggle = "off";
    this.chatInnerToggleInverse = "on";

    this.menuTitleTheme = "theme5";
    this.itemBorder = true;
    this.itemBorderStyle = "none";
    this.subItemBorder = true;
    this.subItemIcon = "style6";
    this.dropDownIcon = "style1";
    this.isSidebarChecked = true;
    this.isHeaderChecked = true;

    const scrollHeight = window.screen.height - 150;
    this.innerHeight = scrollHeight + "px";
    this.windowWidth = window.innerWidth;
    this.setMenuAttributes(this.windowWidth);
  }

  ngOnInit()
  {
    this.setBackgroundPattern("pattern2");
    this.getProfilePic();
  }
  logout()
  {
    this.authService.logout();
    this.authService.redirectLogoutUser();
  }
  getProfilePic()
  {
    this.profileService
      .getProfilePic(this.authService.currentUser.id)
      .subscribe((result) => (this.profileEdit = result));
  }
  get fullName(): string
  {
    return this.authService.currentUser
      ? this.authService.currentUser.fullName
      : "";
  }
  get designation(): string
  {
    return this.authService.currentUser
      ? this.authService.currentUser.roles[0]
      : "";
  }
  onResize(event)
  {
    this.innerHeight = event.target.innerHeight + "px";
    /* menu responsive */
    this.windowWidth = event.target.innerWidth;
    let reSizeFlag = true;
    if (
      this.deviceType === "tablet" &&
      this.windowWidth >= 768 &&
      this.windowWidth <= 1024
    )
    {
      reSizeFlag = false;
    } else if (this.deviceType === "mobile" && this.windowWidth < 768)
    {
      reSizeFlag = false;
    }
    /* for check device */
    if (reSizeFlag)
    {
      this.setMenuAttributes(this.windowWidth);
    }
  }

  setMenuAttributes(windowWidth)
  {
    if (windowWidth >= 768 && windowWidth <= 1024)
    {
      this.deviceType = "tablet";
      this.verticalNavType = "offcanvas";
      this.verticalEffect = "push";
    } else if (windowWidth < 768)
    {
      this.deviceType = "mobile";
      this.verticalNavType = "offcanvas";
      this.verticalEffect = "overlay";
    } else
    {
      this.deviceType = "desktop";
      this.verticalNavType = "expanded";
      this.verticalEffect = "shrink";
    }
  }

  toggleOpened()
  {
    if (this.windowWidth < 768)
    {
      this.toggleOn =
        this.verticalNavType === "offcanvas" ? true : this.toggleOn;
    }
    this.verticalNavType =
      this.verticalNavType === "expanded" ? "offcanvas" : "expanded";
  }

  onClickedOutside(e: Event)
  {
    if (
      this.windowWidth < 768 &&
      this.toggleOn &&
      this.verticalNavType !== "offcanvas"
    )
    {
      this.toggleOn = true;
      this.verticalNavType = "offcanvas";
    }
  }

  onMobileMenu()
  {
    this.isCollapsedMobile =
      this.isCollapsedMobile === "yes-block" ? "no-block" : "yes-block";
  }

  toggleChat()
  {
    this.chatToggle = this.chatToggle === "out" ? "in" : "out";
    this.chatToggleInverse = this.chatToggleInverse === "out" ? "in" : "out";
    this.chatInnerToggle = "off";
    this.chatInnerToggleInverse = "off";
  }

  toggleChatInner()
  {
    this.chatInnerToggle = this.chatInnerToggle === "off" ? "on" : "off";
    this.chatInnerToggleInverse =
      this.chatInnerToggleInverse === "off" ? "on" : "off";
  }

  searchFriendList(event)
  {
    const search = this.search_friends.nativeElement.value.toLowerCase();
    let search_input: string;
    let search_parent: any;
    const friendList = document.querySelectorAll(
      ".userlist-box .media-body .chat-header"
    );
    Array.prototype.forEach.call(friendList, function (elements, index)
    {
      search_input = elements.innerHTML.toLowerCase();
      search_parent = elements.parentNode.parentNode;
      if (search_input.indexOf(search) !== -1)
      {
        search_parent.classList.add("show");
        search_parent.classList.remove("hide");
      } else
      {
        search_parent.classList.add("hide");
        search_parent.classList.remove("show");
      }
    });
  }

  toggleOpenedSidebar()
  {
    this.isCollapsedSideBar =
      this.isCollapsedSideBar === "yes-block" ? "no-block" : "yes-block";
  }

  toggleRightbar()
  {
    this.configOpenRightBar = this.configOpenRightBar === "open" ? "" : "open";
  }

  setSidebarPosition()
  {
    this.isSidebarChecked = !this.isSidebarChecked;
    this.pcodedSidebarPosition =
      this.isSidebarChecked === true ? "fixed" : "absolute";
  }

  setHeaderPosition()
  {
    this.isHeaderChecked = !this.isHeaderChecked;
    this.pcodedHeaderPosition =
      this.isHeaderChecked === true ? "fixed" : "relative";
    this.headerFixedMargin = this.isHeaderChecked === true ? "80px" : "";
  }

  setBackgroundPattern(pattern)
  {
    document.querySelector("body").setAttribute("themebg-pattern", pattern);
  }

  setLayoutType(type: string)
  {
    this.layoutType = type;
    if (type === "dark")
    {
      this.headerTheme = "theme6";
      this.navBarTheme = "theme1";
      this.logoTheme = "theme6";
      document.querySelector("body").classList.add("dark");
    } else
    {
      this.headerTheme = "theme1";
      this.navBarTheme = "themelight1";
      this.logoTheme = "theme1";
      document.querySelector("body").classList.remove("dark");
    }
  }

  setNavBarTheme(theme: string)
  {
    if (theme === "themelight1")
    {
      this.navBarTheme = "themelight1";
    } else
    {
      this.navBarTheme = "theme1";
    }
  }

  /*Menu item show hide*/
  CheckAllow(item): boolean
  {
    switch (item)
    {
      case "employee":
        if (this.canManageEmployee)
        {
          return true;
        } else
        {
          return false;
        }
      case "functional-department":
        if (this.canManageDepartment)
        {
          return true;
        } else
        {
          return false;
        }

      case "functional-group":
        if (this.canManageGroup)
        {
          return true;
        } else
        {
          return false;
        }

      case "functional-designation":
        if (this.canManageDesignation)
        {
          return true;
        } else
        {
          return false;
        }
      case "functional-title":
        if (this.canManageTitle)
        {
          return true;
        } else
        {
          return false;
        }
      case "band":
        if (this.canManageBand)
        {
          return true;
        } else
        {
          return false;
        }
      case "role":
        if (this.canManageRole)
        {
          return true;
        } else
        {
          return false;
        }
      case "process-salary":
        if (this.canManageProcessSalary)
        {
          return true;
        } else
        {
          return false;
        }
      case "check-salary":
        if (this.canManageCheckSalary)
        {
          return true;
        } else
        {
          return false;
        }
      case "salary-component":
        if (this.canManageSalaryComponent)
        {
          return true;
        } else
        {
          return false;
        }
      case "all-employee-salary":
        if (this.canManageAllEmployeeSalary)
        {
          return true;
        } else
        {
          return false;
        }
      case "salary-component-list":
        if (this.canManageSalaryComponentList)
        {
          return true;
        } else
        {
          return false;
        }
      case "maintenance":
        if (
          this.canManageEmployee ||
          this.canManageDepartment ||
          this.canManageGroup ||
          this.canManageTitle ||
          this.canManageDesignation ||
          this.canManageBand ||
          this.canManageRole ||
          this.canManageExpenseItem ||
          this.canManageProcessSalary ||
          this.canManageCheckSalary ||
          this.canManageSalaryComponent ||
          this.canManageAllEmployeeSalary ||
          this.canManageSalaryComponentList
        )
        {
          return true;
        } else
        {
          return false;
        }

      case "manage-leaves":
        if (this.canManageLeave)
        {
          return true;
        } else
        {
          return false;
        }
      case "my-leave":
        if (this.canManageMyLeave)
        {
          return true;
        } else
        {
          return false;
        }
      case "hr-viewlist":
        if (this.canManageHrLeave)
        {
          return true;
        } else
        {
          return false;
        }
      case "my-attendance":
        if (this.canManageMyAttendance)
        {
          return true;
        } else
        {
          return false;
        }
      case "manage-attendance":
        if (this.canManageAttendance)
        {
          return true;
        } else
        {
          return false;
        }
      case "manage-attendance-detail":
        if (this.canManageAttendanceDetail)
        {
          return true;
        } else
        {
          return false;
        }
      case "manage-upload-attendance-detail":
        if (this.canManageUploadAttendanceDetail)
        {
          return true;
        } else
        {
          return false;
        }
      case "manage-attendance-summary":
        if (this.canManageAttendanceSummary)
        {
          return true;
        } else
        {
          return false;
        }
      case "manage-upload-attendance-summary":
        if (this.canManageUploadAttendanceSummary)
        {
          return true;
        } else
        {
          return false;
        }
      case "leave":
        if (this.authService.module.isLeave)
        {
          if (
            this.canManageLeave ||
            this.canManageMyLeave ||
            this.canManageHrLeave ||
            this.canManageMyAttendance ||
            this.canManageAttendance ||
            this.canManageAttendanceDetail ||
            this.canManageUploadAttendanceDetail ||
            this.canManageAttendanceSummary ||
            this.canManageUploadAttendanceSummary
          )
          {
            return true;
          } else
          {
            return false;
          }
        } else
        {
          return false;
        }

      case "my-timesheet":
        if (this.canManageMyTimesheet)
        {
          return true;
        } else
        {
          return false;
        }
      case "manage-timesheet":
        if (this.canManageTimesheet)
        {
          return true;
        } else
        {
          return false;
        }
      case "timesheet":
        if (this.authService.module.isTimesheet)
        {
          if (this.canManageMyTimesheet || this.canManageTimesheet)
          {
            return true;
          } else
          {
            return false;
          }
        } else
        {
          return false;
        }

      case "hr-view":
        if (this.canManageHrView)
        {
          return true;
        } else
        {
          return false;
        }
      case "set-goal":
        if (this.canManageSetGoal)
        {
          return true;
        } else
        {
          return false;
        }
      case "my-goal":
        if (this.canManageMyGoal)
        {
          return true;
        } else
        {
          return false;
        }
      case "review-goal":
        if (this.canManageReviewGoal)
        {
          return true;
        } else
        {
          return false;
        }
      case "performance":
        if (this.authService.module.isPerformance)
        {
          if (
            this.canManageHrView ||
            this.canManageSetGoal ||
            this.canManageMyGoal ||
            this.canManageReviewGoal
          )
          {
            return true;
          } else
          {
            return false;
          }
        } else
        {
          return false;
        }

      case "recruitment-dashboard":
        if (this.canManageRecruitmentDashBoard)
        {
          return true;
        } else
        {
          return false;
        }

      case "job-vaccancy":
        if (this.canManageJobVaccancy)
        {
          return true;
        } else
        {
          return false;
        }
      case "candidate-list":
        if (this.canManageCandidateView)
        {
          return true;
        } else
        {
          return false;
        }
      case "manage-interview":
        if (this.canManageInterview)
        {
          return true;
        } else
        {
          return false;
        }
      case "bulk-scheduling":
        if (this.canManageBulkScheduling)
        {
          return true;
        } else
        {
          return false;
        }
      case "job-vaccancy-list":
        if (this.canManageJobVaccancyList)
        {
          return true;
        } else
        {
          return false;
        }
      case "recruitment":
        if (this.authService.module.isRecruitment)
        {
          if (
            this.canManageJobVaccancy ||
            this.canManageCandidateView ||
            this.canManageInterview ||
            this.canManageBulkScheduling ||
            this.canManageJobVaccancyList
          )
          {
            return true;
          } else
          {
            return false;
          }
        } else
        {
          return false;
        }
      case "expense-booking":
        if (this.canManageExpenseBooking)
        {
          return true;
        } else
        {
          return false;
        }
      case "approved-booking":
        if (this.canManageApprovedBooking)
        {
          return true;
        } else
        {
          return false;
        }
      case "expense":
        if (this.authService.module.isExpanse)
        {
          if (this.canManageExpenseBooking || this.canManageApprovedBooking)
          {
            return true;
          } else
          {
            return false;
          }
        } else
        {
          return false;
        }

      case "sales-tracker":
        if (this.authService.module.isSales)
        {
          if (this.canManageSalesMarketing)
          {
            return true;
          } else
          {
            return false;
          }
        } else
        {
          return false;
        }

      case "job-vaccancy":
        if (this.canManageJobVaccancy)
        {
          return true;
        } else
        {
          return false;
        }

      case "recruitment-config":
        if (this.authService.module.isRecruitment)
        {
          return true;
        } else
        {
          return false;
        }

      case "leave-config":
        if (this.authService.module.isLeave)
        {
          return true;
        } else
        {
          return false;
        }
      case "expense-config":
        if (this.authService.module.isExpanse)
        {
          return true;
        } else
        {
          return false;
        }
      case "performance-config":
        if (this.authService.module.isPerformance)
        {
          return true;
        } else
        {
          return false;
        }
      case "timesheet-config":
        if (this.authService.module.isRecruitment)
        {
          return true;
        } else
        {
          return false;
        }
      case "configuration":
        if (this.canManageConfiguration)
        {
          return true;
        } else
        {
          return false;
        }
      case 'blog':
        if (this.canManageBlog)
        {
          return true;
        } else
        {
          return false;
        }

      default:
        return true;
    }
  }

  get canManageEmployee()
  {
    return this.accountService.userHasPermission(Permission.manageEmployee);
  }
  get canManageDepartment()
  {
    return this.accountService.userHasPermission(Permission.manageDepartment);
  }
  get canManageGroup()
  {
    return this.accountService.userHasPermission(Permission.manageGroup);
  }
  get canManageDesignation()
  {
    return this.accountService.userHasPermission(Permission.manageDesignation);
  }
  get canManageTitle()
  {
    return this.accountService.userHasPermission(Permission.manageTitle);
  }
  get canManageBand()
  {
    return this.accountService.userHasPermission(Permission.manageBand);
  }
  get canManageRole()
  {
    return this.accountService.userHasPermission(Permission.manageRole);
  }
  get canManageExpenseItem()
  {
    return this.accountService.userHasPermission(Permission.manageExpenseItem);
  }

  get canManageProcessSalary()
  {
    return this.accountService.userHasPermission(
      Permission.manageProcessSalary
    );
  }
  get canManageCheckSalary()
  {
    return this.accountService.userHasPermission(Permission.manageCheckSalary);
  }
  get canManageSalaryComponent()
  {
    return this.accountService.userHasPermission(
      Permission.manageSalaryComponent
    );
  }
  get canManageAllEmployeeSalary()
  {
    return this.accountService.userHasPermission(
      Permission.manageAllEmployeeSalary
    );
  }
  get canManageSalaryComponentList()
  {
    return this.accountService.userHasPermission(
      Permission.manageSalaryComponentList
    );
  }

  get canManageMyLeave()
  {
    return this.accountService.userHasPermission(Permission.manageMyLeave);
  }
  get canManageLeave()
  {
    return this.accountService.userHasPermission(Permission.manageLeave);
  }
  get canManageHrLeave()
  {
    return this.accountService.userHasPermission(Permission.manageHrLeave);
  }
  get canManageMyAttendance()
  {
    return this.accountService.userHasPermission(Permission.manageMyAttendance);
  }
  get canManageAttendance()
  {
    return this.accountService.userHasPermission(Permission.manageAttendance);
  }
  get canManageAttendanceDetail()
  {
    return this.accountService.userHasPermission(
      Permission.manageAttendanceDetail
    );
  }
  get canManageUploadAttendanceDetail()
  {
    return this.accountService.userHasPermission(
      Permission.manageUploadAttendanceDetail
    );
  }
  get canManageAttendanceSummary()
  {
    return this.accountService.userHasPermission(
      Permission.manageAttendanceSummary
    );
  }
  get canManageUploadAttendanceSummary()
  {
    return this.accountService.userHasPermission(
      Permission.manageUploadAttendanceSummary
    );
  }

  get canManageMyTimesheet()
  {
    return this.accountService.userHasPermission(Permission.manageMyTimesheet);
  }
  get canManageTimesheet()
  {
    return this.accountService.userHasPermission(Permission.manageTimesheet);
  }

  get canManageHrView()
  {
    return this.accountService.userHasPermission(Permission.manageHrView);
  }
  get canManageSetGoal()
  {
    return this.accountService.userHasPermission(Permission.manageSetGoal);
  }
  get canManageMyGoal()
  {
    return this.accountService.userHasPermission(Permission.manageMyGoal);
  }
  get canManageReviewGoal()
  {
    return this.accountService.userHasPermission(Permission.manageReviewGoal);
  }

  get canManageRecruitmentDashBoard()
  {
    return this.accountService.userHasPermission(
      Permission.manageRecruitmentDashBoard
    );
  }
  get canManageJobVaccancy()
  {
    return this.accountService.userHasPermission(Permission.manageJobVaccancy);
  }
  get canManageCandidateView()
  {
    return this.accountService.userHasPermission(
      Permission.manageCandidateView
    );
  }
  get canManageInterview()
  {
    return this.accountService.userHasPermission(Permission.manageInterview);
  }
  get canManageBulkScheduling()
  {
    return this.accountService.userHasPermission(
      Permission.manageBulkScheduling
    );
  }
  get canManageJobVaccancyList()
  {
    return this.accountService.userHasPermission(
      Permission.manageJobVaccancyList
    );
  }
  get canManageExpenseBooking()
  {
    return this.accountService.userHasPermission(
      Permission.manageExpenseBooking
    );
  }
  get canManageApprovedBooking()
  {
    return this.accountService.userHasPermission(
      Permission.manageExpenseApproved
    );
  }
  get canManageSalesMarketing()
  {
    return this.accountService.userHasPermission(
      Permission.manageSalesMarketing
    );
  }
  get canManageConfiguration()
  {
    return this.accountService.userHasPermission(
      Permission.manageConfiguration
    );
  }
  get canManageBlog()
  {
    return this.accountService.userHasPermission(Permission.manageBlog);
  }
}
