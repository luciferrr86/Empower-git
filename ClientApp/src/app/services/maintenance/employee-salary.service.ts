import { Injectable, Injector } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient, HttpParams } from '@angular/common/http';
import { EndpointFactory } from '../common/endpoint-factory.service';
import { ConfigurationService } from '../common/configuration.service';
import { EmployeeSalaryModel } from '../../models/maintenance/employee-salary.model';
import { EmployeeCtcModel } from '../../models/maintenance/employee-ctc.model';
import { User } from '../../models/account/user.model';
import { SalaryComponentModel } from '../../models/maintenance/salary-component.model';
import { MonthlyAttendence } from '../../models/leave/monthly-attendence.model';



@Injectable()
export class EmployeeSalaryService extends EndpointFactory {

    private readonly _getUrl: string = "/api/Salary/empSalList";
    private get getUrl() { return this.configurations.baseUrl + this._getUrl; }
    private readonly _getSalaryDetail: string = "/api/Salary/salaryDetail";
    private get getSalaryDetail() { return this.configurations.baseUrl + this._getSalaryDetail; }
    private readonly _getSalaryUrl: string = "/api/Salary";
    private get getSalaryUrl() { return this.configurations.baseUrl + this._getSalaryUrl; }
    private readonly _createUrl: string = "/api/Salary/salary";
    private get createUrl() { return this.configurations.baseUrl + this._createUrl; }
    //private readonly _addUrl: string = "/api/Salary/managectc";
    //private get addUrl() { return this.configurations.baseUrl + this._addUrl; }
    private readonly _addUrl: string = "/api/EmployeeCtc/managectc";
    private get addUrl() { return this.configurations.baseUrl + this._addUrl; }
    private readonly _processSal: string = "/api/Salary/saveAllEmpSalDetail";
    private get processSal() { return this.configurations.baseUrl + this._processSal; }
    private readonly _checkSal: string = "/api/Salary/empSalList";
    private get checkSal() { return this.configurations.baseUrl + this._checkSal; }
    private readonly _checkEmpSal: string = "/api/Salary/getEmpSal";
    private get checkEmpSal() { return this.configurations.baseUrl + this._checkEmpSal; }
    //private readonly _getCtc: string = "/api/Salary/ctc";
    //private get getCtc() { return this.configurations.baseUrl + this._getCtc; }
    private readonly _getCtc: string = "/api/EmployeeCtc/ctc";
    private get getCtc() { return this.configurations.baseUrl + this._getCtc; }
    private readonly _viewSalary: string = "/api/Salary/viewSalary";
    private get viewSalary() { return this.configurations.baseUrl + this._viewSalary; }
    private readonly _saveFilename: string = "/api/Salary/saveFile";
    private get saveFilename() { return this.configurations.baseUrl + this._saveFilename; }
    private readonly _addSalComp: string = "/api/Salary/salComp";
    private get addSalComp() { return this.configurations.baseUrl + this._addSalComp; }
    private readonly _empList: string = "/api/Salary/empListForLeave";
    private get empList() { return this.configurations.baseUrl + this._empList; }
    private readonly _dateList: string = "/api/ExcelUpload/empAllMonthlyDetail";
    private get dateList() { return this.configurations.baseUrl + this._dateList; }
    private readonly _getEmpAtt: string = "/api/Salary/empAtt";
    private get getEmpAtt() { return this.configurations.baseUrl + this._getEmpAtt; }
    private readonly _empListMonth: string = "/api/ExcelUpload/empMonthlyDetail";
    private get empListMonth() { return this.configurations.baseUrl + this._empListMonth; }
    private readonly _editEmpAtt: string = "/api/ExcelUpload/UpdateEmpAtt";
    private get editEmpAtt() { return this.configurations.baseUrl + this._editEmpAtt; }
    private readonly _updateAttSummary: string = "/api/ExcelUpload/attSummary";
    private get updateAttSummary() { return this.configurations.baseUrl + this._updateAttSummary; }
    private readonly _allEmpAttSumm: string = "/api/ExcelUpload/saveAllEmpAttSummary";
    private get allEmpAttSumm() { return this.configurations.baseUrl + this._allEmpAttSumm; }
    private readonly _allEmpList: string = "/api/Salary/allEmpSal";
    private get allEmpList() { return this.configurations.baseUrl + this._allEmpList; }
    private readonly _upEmpSal: string = "/api/Salary/updateEmpSal";
    private get upEmpSal() { return this.configurations.baseUrl + this._upEmpSal; }
    private readonly _empAttSummary: string = "/api/ExcelUpload/getEmpAttSummary";
    private get empAttSummary() { return this.configurations.baseUrl + this._empAttSummary; }
    private readonly _salComponentList: string = "/api/Salary/getSalComponentList";
    private get salComponentList() { return this.configurations.baseUrl + this._salComponentList; }
    private readonly _compById: string = "/api/Salary/getComponentById";
    private get compById() { return this.configurations.baseUrl + this._compById; }

    constructor(http: HttpClient, configurations: ConfigurationService, injector: Injector) {
        super(http, configurations, injector);
    }

    getCompById(id: number): Observable<any> {

        let endpointUrl = `${this.compById}/${id}`;
        return this.http.get(endpointUrl, this.getRequestHeaders()).catch(error => {
            return this.handleError(error, () => this.getCompById(id));
        })
    }

    getSalComponents(): Observable<any> {
        let endpointUrl = this.salComponentList;
        return this.http.get(endpointUrl, this.getRequestHeaders()).
            catch(error => {
                return this.handleError(error, () => this.getSalComponents());
            });
    }

    GetEmpAttSummary(month: number, year: number): Observable<any> {
        let endpointUrl = this.empAttSummary + "?Month=" + month + "&Year=" + year;
        return this.http.get(endpointUrl, this.getRequestHeaders()).
            catch(error => {
                return this.handleError(error, () => this.GetEmpAttSummary(month, year));
            });
    }

    UpdateEmpSal(empid: string, allowedLeaves: number, tds: number): Observable<any> {

        let endpointUrl = this.upEmpSal + "?Empid=" + empid + "&AllowedLeaves=" + allowedLeaves + "&Tds=" + tds;
        return this.http.post(endpointUrl, this.getRequestHeaders()).
            catch(error => {
                return this.handleError(error, () => this.UpdateEmpSal(empid, allowedLeaves, tds));
            });
    }
    getEmployeeSalary(month: number, year: number): Observable<any> {

        //  let endpointUrl = `${this.allEmpList}`;
        let endpointUrl = this.allEmpList + "?Month=" + month + "&Year=" + year;
        return this.http.get(endpointUrl, this.getRequestHeaders()).
            catch(error => {
                return this.handleError(error, () => this.getEmployeeSalary(month, year));
            });
    }

    getEmp(id: string): Observable<any> {

        let endpointUrl = `${this.getEmpAtt}/${id}`;
        return this.http.get(endpointUrl, this.getRequestHeaders()).catch(error => {
            return this.handleError(error, () => this.getEmp(id));
        })
    }

    GetEmpByMonth(userId: string, month: number, year: number): Observable<any> {
        let endpointUrl = this.empListMonth + "?Id=" + userId + "&Month=" + month + "&Year=" + year;
        return this.http.get(endpointUrl, this.getRequestHeaders()).
            catch(error => {
                return this.handleError(error, () => this.GetEmpByMonth(userId, month, year));
            });
    }

    UpdateEmpAttendance(empid: string, punchin: string, punchout: string, date: Date, status: number): Observable<any> {

        let endpointUrl = this.editEmpAtt + "?Empid=" + empid + "&PunchIn=" + punchin + "&PunchOut=" + punchout + "&Date=" + date + "&status=" + status;
        return this.http.post(endpointUrl, this.getRequestHeaders()).
            catch(error => {
                return this.handleError(error, () => this.UpdateEmpAttendance(empid, punchin, punchout, date, status));
            });
    }
    allEmpAttSummary(month: number, year: number): Observable<any> {
        let endpointUrl = this.allEmpAttSumm + "?Month=" + month + "&Year=" + year;
        return this.http.post(endpointUrl, this.getRequestHeaders()).
            catch(error => {
                return this.handleError(error, () => this.allEmpAttSummary(month, year));
            });
    }

    UpdateAttSummary(empid: string, month: number, year: number): Observable<any> {

        let endpointUrl = this.updateAttSummary + "?Empid=" + empid + "&Month=" + month + "&Year=" + year;
        return this.http.post(endpointUrl, this.getRequestHeaders()).
            catch(error => {
                return this.handleError(error, () => this.UpdateAttSummary(empid, month, year));
            });
    }
    GetDate(month: number, year: number): Observable<any> {
        let endpointUrl = this.dateList + "?Month=" + month + "&Year=" + year;
        return this.http.get(endpointUrl, this.getRequestHeaders()).
            catch(error => {
                return this.handleError(error, () => this.GetDate(month, year));
            });
    }

    GetDateList(): Observable<any> {
        let endpointUrl = `${this.dateList}`;
        return this.http.get(endpointUrl, this.getRequestHeaders()).
            catch(error => {
                return this.handleError(error, () => this.GetDateList());
            });
    }
    addEmployeeCtc(empCtc: EmployeeCtcModel): Observable<string> {

        return this.http.post(this.addUrl, empCtc, this.getRequestHeaders()).
            catch(error => {
                return this.handleError(error, () => this.addEmployeeCtc(empCtc));
            });
    }

    getCtcByEmployeeId(id: string) {
        let endpointUrl = id ? `${this.getCtc}/${id}` : this.getCtc;
        return this.http.get(endpointUrl, this.getRequestHeaders()).
            catch(error => {
                return this.handleError(error, () => this.getCtcByEmployeeId(id));
            });
    }

    getSalaryDetailsByEmpId(id: string) {
        let endpointUrl = id ? `${this.getSalaryDetail}/${id}` : this.getSalaryDetail;
        return this.http.get(endpointUrl, this.getRequestHeaders()).
            catch(error => {
                return this.handleError(error, () => this.getSalaryDetailsByEmpId(id));
            });
    }

    getSalaryByEmployeeId(id: string, month: number, year: number) {
        let endpointUrl = id ? `${this.getSalaryUrl}/${id}` : this.getSalaryUrl;
        endpointUrl = endpointUrl + "?Month=" + month + "&Year=" + year;
        return this.http.get(endpointUrl, this.getRequestHeaders()).
            catch(error => {
                return this.handleError(error, () => this.getSalaryByEmployeeId(id, month, year));
            });
    }

    addSalary(empSal: EmployeeSalaryModel): Observable<string> {

        return this.http.post(this.createUrl, JSON.stringify(empSal), this.getRequestHeaders()).
            catch(error => {
                return this.handleError(error, () => this.addSalary(empSal));
            });
    }

    getAllEmployeeSalaryView(): Observable<any> {
        let endpointUrl = `${this.getUrl}`;
        return this.http.get(endpointUrl, this.getRequestHeaders()).
            catch(error => {
                return this.handleError(error, () => this.getAllEmployeeSalaryView());
            });
    }

    processSalary(empSal: EmployeeSalaryModel): Observable<string> {
        return this.http.post(this.processSal, JSON.stringify(empSal), this.getRequestHeaders()).
            catch(error => {
                return this.handleError(error, () => this.processSalary(empSal));
            });
    }

    checkSalary(month: number, year: number): Observable<any[]> {
        let endpointUrl = this.checkSal + "?Month=" + month + "&Year=" + year;
        return this.http.get(endpointUrl, this.getRequestHeaders()).
            catch(error => {
                return this.handleError(error, () => this.checkSalary(month, year));
            });
    }
    checkEmpSalary(month: number, year: number, employeeName: string, currentPage: any, pageSize: any, sortedBy: any) {

        let endpointUrl = this.checkEmpSal + "?Month=" + month + "&Year=" + year + "&EmployeeName=" + employeeName + "&CurrentPage=" + currentPage + "&PageSize=" + pageSize + "&SortedBy=" + sortedBy;
        return this.http.get<any[]>(endpointUrl, this.getRequestHeaders());

    }
    getSalaryDetailsById(id: number) {
        let endpointUrl = id ? `${this.viewSalary}/${id}` : this.getSalaryDetail;
        return this.http.get(endpointUrl, this.getRequestHeaders()).
            catch(error => {
                return this.handleError(error, () => this.getSalaryDetailsById(id));
            });
    }

    saveFile(filename: string): Observable<string> {
        return this.http.post(this.saveFilename, JSON.stringify(filename), this.getRequestHeaders()).
            catch(error => {
                return this.handleError(error, () => this.saveFile(filename));
            });
    }

    addSalComponent(salComp: SalaryComponentModel): Observable<string> {
        return this.http.post(this.addSalComp, JSON.stringify(salComp), this.getRequestHeaders()).
            catch(error => {
                return this.handleError(error, () => this.addSalComponent(salComp));
            });
    }

    getEmployeeListForLeaveEntry(): Observable<any> {
        let endpointUrl = `${this.empList}`;
        return this.http.get(endpointUrl, this.getRequestHeaders()).
            catch(error => {
                return this.handleError(error, () => this.getEmployeeListForLeaveEntry());
            });
    }

}
