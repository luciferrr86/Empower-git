import { EmployeeAttendenceVM } from "./emp-attendence.model";

export class MonthlyAttendence {
    month: number;
    year: number;
    startDay: number;
    userId: string;
    employeeId: number;
    employeeAttendenceVM : EmployeeAttendenceVM[];
    monthDates: MonthDate[];
}
export class MonthDate {
    mDate: string;
    mDay: string;
    mDayStatus: number;

    }
