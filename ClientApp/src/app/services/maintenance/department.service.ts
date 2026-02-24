import { Injectable, Injector } from '@angular/core';

import { EndpointFactory } from '../common/endpoint-factory.service';
import { ConfigurationService } from '../common/configuration.service';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import { FunctionalDepartment } from '../../models/maintenance/functional-department.model';

@Injectable()
export class DepartmentService extends EndpointFactory {

    private readonly _departmentsUrl: string = "/api/Department/departmentList";
    private readonly _getDepartmentUrl: string = "/api/Department/getById";
    private readonly _createDepartmentUrl: string = "/api/Department/create";
    private readonly _updateDepartmentUrl: string = "/api/Department/update";
    private readonly _deletedepartmentUrl: string = "/api/Department/delete";

    private get departmentUrl() { return this.configurations.baseUrl + this._departmentsUrl; }
    private get getUrl() { return this.configurations.baseUrl + this._getDepartmentUrl; }
    private get createUrl() { return this.configurations.baseUrl + this._createDepartmentUrl; }
    private get updateUrl() { return this.configurations.baseUrl + this._updateDepartmentUrl; }
    private get deleteUrl() { return this.configurations.baseUrl + this._deletedepartmentUrl; }

    constructor(http: HttpClient, configurations: ConfigurationService, injector: Injector) {

        super(http, configurations, injector);
    }

    getAllDepartment(page?: number, pageSize?: number, name?: string): Observable<any> {
        let endpointUrl = `${this.departmentUrl}/${page}/${pageSize}/${name}`;
        return this.http.get(endpointUrl, this.getRequestHeaders())
            .catch(error => {
                return this.handleError(error, () => this.getAllDepartment(page, pageSize, name));
            });
    }

    create(departmentObject: any) {
        return this.http.post(this.createUrl, JSON.stringify(departmentObject), this.getRequestHeaders())
            .catch(error => {
                return this.handleError(error, () => this.create(departmentObject));
            });
    }

    update(departmentObject: any, departmentId?: string) {
        let endpointUrl = departmentId ? `${this.updateUrl}/${departmentId}` : this.updateUrl;
        return this.http.put(endpointUrl, JSON.stringify(departmentObject), this.getRequestHeaders())
            .catch(error => {
                return this.handleError(error, () => this.update(departmentObject, departmentId));
            });
    }

    delete(departmentId: string | FunctionalDepartment) {
        let endpointUrl = `${this.deleteUrl}/${departmentId}`;
        return this.http.delete(endpointUrl, this.getRequestHeaders())
            .catch(error => {
                return this.handleError(error, () => this.delete(departmentId));
            });
    }
}
