import { SalaryPart } from "./salary-part.model";

export class EmployeeSalaryModel {
    id: number;
    month: number;
    year: number;
    totalDaysOfMonth: number;
    allowedLeave: number;
    leaveTaken: number;
    workedDays: number;
    //ctc: number;
    basicSalary: number;
    da: number;
    hra: number;
    conveyance: number;
   // convWorking: number;
    medicalExpenses: number;
    special: number;
    bonus: number;
    ta: number;
    total: number;
    contributionToPf: number;
    professionTax: number;
    tds: number;
    salaryAdvance: number;
    salaryTotal: number;
    netPayable: number;
   // pfApplicable: number;
    medicalBillAmount: number;
    unpaidDays: number;
    employeeId: string;
    employeeName: string;
    employeeCode: number;
    isEdit: boolean;
    employeeCtcId: number;
    salaryPart: SalaryPart[];
}
