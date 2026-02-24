import { Injectable } from '@angular/core';

export interface BadgeItem {
    type: string;
    value: string;
}

export interface ChildrenItems {
    state: string;
    target?: boolean;
    name: string;
    type?: string;
    IsVisble?: boolean,
    children?: ChildrenItems[];
}

export interface MainMenuItems {
    state: string;
    short_label?: string;
    main_state?: string;
    target?: boolean;
    name: string;
    type: string;
    icon: string;
    badge?: BadgeItem[];
    IsVisble?: boolean,
    children?: ChildrenItems[];
}

export interface Menu {
    label: string;
    main: MainMenuItems[];
}

export interface CandidateMenu {
    label: string;
    main: MainMenuItems[];
}

const CANDIDATE_MENUITEMS = [
    {
        label: '',
        main: [
            {
                state: 'dashboard',
                main_state: 'candidate',
                short_label: 'D',
                name: 'Dashboard',
                type: 'link',
                icon: 'ti-home'

            },

        ],

  },
  {
    label: '',
    main: [
      {
        state: 'job-vaccancy-list',
        main_state: 'candidate',
        short_label: 'JC',
        name: 'Vaccancy List',
        type: 'link',
        icon: 'ti-direction-alt',
        IsVisble: true,

      },

    ],
  },
];
const ADMINISTATOR_MENUITEMS = [
    {
        label: '',
        main: [
            {
                state: 'client',
                main_state: 'administrator',
                short_label: 'C',
                name: 'Client Setting',
                type: 'link',
                icon: 'ti-home'

            },
            {
                state: 'module-setting',
                main_state: 'administrator',
                short_label: 'M',
                name: 'Module Setting',
                type: 'link',
                icon: 'ti-home'

            }
        ],
    },
];

const MENUITEMS = [
    {
        label: '',
        main: [
            {
                state: 'dashboard',
                short_label: 'D',
                name: 'Dashboard',
                type: 'link',
                icon: 'ti-home',
                IsVisble: true,

            },

        ],
    },
   
    {
        label: '',
        main: [
            {
                state: 'leave',
                short_label: 'L',
                name: 'Leave',
                type: 'sub',
                icon: 'ti-direction-alt',
                IsVisble: true,
                children: [
                    {
                        state: 'manage-leaves',
                        short_label: 'ML',
                        name: 'Manage Leave',
                        type: 'link',
                        icon: 'ti-direction-alt',
                        IsVisble: true,

                    },
                    {
                        state: 'my-leave',
                        short_label: 'ML',
                        name: 'My Leave',
                        type: 'link',
                        icon: 'ti-layout-sidebar-left',
                        IsVisble: true,
                    },
                    {
                        state: 'hr-viewlist',
                        short_label: 'HL',
                        name: 'HR Leave',
                        type: 'link',
                        icon: 'ti-layout-sidebar-left',
                        IsVisble: true,
                  },
                 
                    {
                      state: 'attendance',
                      name: 'Attendance',
                      short_label: 'A',
                      type: 'sub',
                      IsVisble: true,
                      children: [
                         {
                        state: 'my-attendance',
                        name: 'My Attendance',
                        target: false,
                        IsVisble: true,
                    },
                         {
                        state: 'manage-attendance',
                        name: 'Manage Attendance',
                        target: false,
                        IsVisble: true,
                    },
                         {
                          state: 'leave-entry',
                          name: 'Attendance Detail',
                          target: false,
                          IsVisble: true,
                        },
                         {
                          state: 'excel-upload',
                          name: 'Upload Attendance Detail',
                          target: false,
                          IsVisble: true,
                        },
                         {
                          state: 'upload-attendance-summary',
                          name: 'Upload Attendance Summary',
                          target: false,
                          IsVisble: true,
                        },
                         {
                          state: 'view-summary',
                          name: 'View Attendence Summary',
                          target: false,
                          IsVisble: true,
                        }
                      ]
                    },

                ]
            },

        ]
    },
    {
        label: '',
        main: [
            {
                state: 'timesheet',
                short_label: 'T',
                name: 'Timesheet',
                type: 'sub',
                icon: 'ti-direction-alt',
                IsVisble: true,
                children: [
                    {
                        state: 'my-timesheet',
                        short_label: 'MT',
                        name: 'My Timesheet',
                        type: 'link',
                        icon: 'ti-direction-alt',
                        IsVisble: true,

                    },
                    {
                        state: 'manage-timesheet',
                        short_label: 'MT',
                        name: 'Manage Timesheet',
                        type: 'link',
                        icon: 'ti-layout-sidebar-left',
                        IsVisble: true,
                    },
                ]
            },

        ]
    },
    {
        label: '',
        main: [
            {
                state: 'performance',
                short_label: 'P',
                name: 'Performance',
                type: 'sub',
                icon: 'ti-direction-alt',
                IsVisble: true,
                children: [
                    {
                        state: 'hr-view',
                        short_label: 'HV',
                        name: 'HR View',
                        type: 'link',
                        icon: 'ti-direction-alt',
                        IsVisble: true,

                    },
                    {
                        state: 'set-goal',
                        short_label: 'SG',
                        name: 'Set Goal',
                        type: 'link',
                        icon: 'ti-layout-sidebar-left',
                        IsVisble: true,
                    },
                    {
                        state: 'my-goal',
                        short_label: 'MI',
                        name: 'My Goal',
                        type: 'link',
                        icon: 'ti-layout-sidebar-left',
                        IsVisble: true,
                    },
                    {
                        state: 'review-goal',
                        short_label: 'RG',
                        name: 'Review Goal',
                        type: 'link',
                        icon: 'ti-layout-sidebar-left',
                        IsVisble: true,
                    }

                ]
            },

        ]
    },
    {
        label: '',
        main: [
            {
                state: 'recruitment',
                short_label: 'R',
                name: 'Recruitment',
                type: 'sub',
                icon: 'ti-direction-alt',
                IsVisble: true,
                children: [
                    {
                        state: 'recruitment-dashboard',
                        short_label: 'D',
                        name: 'Dashboard',
                        type: 'link',
                        icon: 'ti-direction-alt',
                        IsVisble: true,
                    },
                    {
                        state: 'job-vaccancy',
                        short_label: 'JV',
                        name: 'Job Vacancy',
                        type: 'link',
                        icon: 'ti-direction-alt',
                        IsVisble: true,

                    },
                    // {
                    //   state: 'candidate-list',
                    //   short_label: 'CV',
                    //   name: 'Candidate View',
                    //   type: 'link',
                    //   icon: 'ti-layout-sidebar-left',
                    //   IsVisble: true,
                    // },
                  {
                    state: 'candidate-bulkupload',
                    short_label: 'CB',
                    name: 'Candidate BulkUpload',
                    type: 'link',
                    icon: 'ti-direction-alt',
                    IsVisble: true,
                  },
                  {
                    state: 'directory-list',
                    short_label: 'CB',
                    name: 'Client Directory',
                    type: 'link',
                    icon: 'ti-direction-alt',
                    IsVisble: true,
                  },
                    {
                        state: 'manage-interview',
                        short_label: 'MI',
                        name: 'Manage Interview',
                        type: 'link',
                        icon: 'ti-layout-sidebar-left',
                        IsVisble: true,
                    }
                    // {
                    //   state: 'bulk-scheduling',
                    //   short_label: 'BS',
                    //   name: 'Bulk Scheduling',
                    //   type: 'link',
                    //   icon: 'ti-layout-sidebar-left',
                    //   IsVisble: true,
                    // }

                ]
            },

        ]
    },
    {
        label: '',
        main: [
            {
                state: 'maintenance',
                short_label: 'M',
                name: 'Maintenance',
                type: 'sub',
                icon: 'ti-direction-alt',
                IsVisble: true,
                children: [
                    {
                        state: 'employee',
                        name: 'Employee',
                        short_label: 'E',
                        type: 'sub',
                        IsVisble: true,
                        children: [
                            {
                                state: 'list',
                                name: 'Employee List',
                                target: false,
                                IsVisble: true,
                            },
                            {
                                state: 'bulk-upload',
                                name: 'Bulk Upload',
                                target: false,
                                IsVisble: true,
                            }
                        ]
                    },
                    {
                        state: 'process-salary',
                        name: 'Salary',
                        short_label: 'S',
                        type: 'sub',
                        IsVisble: true,
                        children: [
                            {
                                state: 'empsalary',
                                name: 'Process Salary',
                                target: false,
                                IsVisble: true,
                            },
                            {
                                state: 'check-salary',
                                name: 'Check Salary',
                                target: false,
                                IsVisble: true,
                            },
                             {
                                state: 'salary-component',
                                name: 'Salary Component',
                                target: false,
                                IsVisble: true,
                            },
                            {
                                state: 'all-emp-sal',
                                name: 'All Employee Salary',
                                target: false,
                                IsVisble: true,
                            },
                            {
                                state: 'sal-com-list',
                                name: 'Salary Component List',
                                target: false,
                                IsVisble: true,
                            },
                        ]
                    },
                    //{
                    //    state: 'upload-att-summary',
                    //    name: 'Attendance',
                    //    short_label: 'A',
                    //    type: 'sub',
                    //    IsVisble: true,
                    //    children: [
                           
                    //        {
                    //            state: 'leave-entry',
                    //            name: 'Attendance Detail',
                    //            target: false,
                    //            IsVisble: true,
                    //        },
                    //        {
                    //            state: 'excel-upload',
                    //            name: 'Upload Attendance Detail',
                    //            target: false,
                    //            IsVisble: true,
                    //        },
                    //        {
                    //            state: 'upload-attendance-summary',
                    //            name: 'Upload Attendance Summary',
                    //            target: false,
                    //            IsVisble: true,
                    //        },
                    //        {
                    //            state: 'view-summary',
                    //            name: 'View Attendence Summary',
                    //            target: false,
                    //            IsVisble: true,
                    //        }
                    //    ]
                    //},

                    {
                        state: 'functional-department',
                        short_label: 'FD',
                        name: 'Functional Department',
                        type: 'link',
                        icon: 'ti-layout-sidebar-left',
                        IsVisble: true,
                    },
                    {
                        state: 'functional-group',
                        short_label: 'FG',
                        name: 'Functional Group',
                        type: 'link',
                        icon: 'ti-layout-sidebar-left',
                        IsVisble: true,
                    },
                    {
                        state: 'functional-designation',
                        short_label: 'FG',
                        name: 'Designation',
                        type: 'link',
                        icon: 'ti-layout-sidebar-left',
                        IsVisble: true,
                    },
                    {
                        state: 'functional-title',
                        short_label: 'T',
                        name: 'Title',
                        type: 'link',
                        icon: 'ti-layout-sidebar-left',
                        IsVisble: true,
                    },
                    {
                        state: 'band',
                        short_label: 'B',
                        name: 'Band',
                        type: 'link',
                        icon: 'ti-layout-sidebar-left',
                        IsVisble: true,
                    },
                    {
                        state: 'role',
                        short_label: 'RN',
                        name: 'Role Name',
                        type: 'link',
                        icon: 'ti-layout-sidebar-left',
                        IsVisble: true,
                    },
                    
                ]
            },

        ]
    },

    {
        label: '',
        main: [
            {
                state: 'configuration',
                short_label: 'CF',
                name: 'Configuration',
                type: 'sub',
                icon: 'ti-direction-alt',
                IsVisble: true,
                children: [
                    {
                        state: 'recruitment-config',
                        short_label: 'CR',
                        name: 'Recruitment',
                        type: 'sub',
                        IsVisble: true,
                        children: [
                            {
                                state: 'job-type',
                                name: 'Job Type',
                                target: false,
                                IsVisble: true,
                            },
                            {
                                state: 'interview-type',
                                name: 'InterView Type',
                                target: false,
                                IsVisble: true,
                            }
                        ]
                    },
                    {
                        state: 'leave-config',
                        short_label: 'CL',
                        name: 'Leave',
                        type: 'sub',
                        IsVisble: true,
                        children: [
                            {
                                state: 'leave-period',
                                name: 'Leave Period',
                                target: false,
                                IsVisble: true,
                            },
                            {
                                state: 'leave-holiday',
                                name: 'HolidayList',
                                target: false,
                                IsVisble: true,
                            },
                            {
                                state: 'leave-type',
                                name: 'Leave Type',
                                target: false,
                                IsVisble: true,
                            },
                            {
                                state: 'leave-rules',
                                name: 'Leave Rules',
                                target: false,
                                IsVisble: true,
                            },
                            {
                                state: 'leave-workingday',
                                name: 'Leave Workingday',
                                target: false,
                                IsVisble: true,
                            }
                        ]

                    },
                    {
                        state: 'timesheet-config',
                        short_label: 'TS',
                        name: 'Timesheet',
                        type: 'sub',
                        IsVisble: true,
                        children: [
                            {
                                state: 'type',
                                name: 'Type',
                                target: false,
                                IsVisble: true,
                            },
                            {
                                state: 'template',
                                name: 'Template',
                                target: false,
                                IsVisble: true,
                            },
                            {
                                state: 'schedule',
                                name: 'Schedule',
                                target: false,
                                IsVisble: true,
                            },
                            {
                                state: 'client',
                                name: 'Client',
                                target: false,
                                IsVisble: true,
                            },
                            {
                                state: 'project',
                                name: 'Project',
                                target: false,
                                IsVisble: true,
                            }
                        ]
                    },
                    {
                        state: 'expense-config',
                        short_label: 'EM',
                        name: 'Expense',
                        type: 'sub',
                        IsVisble: true,
                        children: [
                            {
                                state: 'category',
                                name: 'Category',
                                target: false,
                                IsVisble: true,
                            },
                            {
                                state: 'sub-category',
                                name: 'Sub Category',
                                target: false,
                                IsVisble: true,
                            },
                            {
                                state: 'item',
                                name: 'Expense Item',
                                target: false,
                                IsVisble: true,
                            },
                            {
                                state: 'title',
                                name: 'Approval Amount Limit',
                                target: false,
                                IsVisble: true,
                            },
                        ]
                    },
                    {
                        state: 'performance-config',
                        short_label: 'GC',
                        name: 'Performance Config',
                        type: 'link',
                        icon: 'ti-layout-sidebar-left',
                        IsVisble: true,
                    }
                   
                    
                ]
            }
        ]
    },
    {
        label: '',
        main: [
            {
                state: 'expense',
                short_label: 'EM',
                name: 'Expense',
                type: 'sub',
                icon: 'ti-direction-alt',
                IsVisble: true,
                children: [
                    {
                        state: 'expense-booking',
                        name: 'ExpenseBooking',
                        target: false,
                        type: 'link',
                        IsVisble: true,
                    },
                    {
                        state: 'approved-booking',
                        name: 'Approved Booking',
                        target: false,
                        type: 'link',
                        IsVisble: true,
                    },
                ]
            },

        ]
    },
    {
        label: '',
        main: [
            {
                state: 'sales-tracker',
                short_label: 'EB',
                name: 'Sales Marketing',
                type: 'link',
                icon: 'ti-direction-alt',
                IsVisble: true,

            },
        ]
    },
    {
        label: '',
        main: [
            {
                state: 'blog',
                short_label: 'BL',
                name: 'Blog',
                type: 'link',
                icon: 'icofont icofont-pen-nib',
                IsVisble: true,
            },

        ]
    }
];


@Injectable()
export class MenuItems {
    getAll(): Menu[] {
        return MENUITEMS;
    }
}

@Injectable()
export class CandidateMenuItems {
    getAll(): Menu[] {
        return CANDIDATE_MENUITEMS;
    }
}
@Injectable()
export class AdministatorMenuItems {
    getAll(): Menu[] {
        return ADMINISTATOR_MENUITEMS;
    }
}
