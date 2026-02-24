import { Injectable, Injector } from '@angular/core';
import { EndpointFactory } from '../common/endpoint-factory.service';
import { HttpClient } from '@angular/common/http';
import { ConfigurationService } from '../common/configuration.service';

@Injectable()
export class CreateExpenseItemService extends EndpointFactory {

  private readonly _getAllExpenseBookingItemUrl: string = "/api/ExpenseSubCategoryItem/list";
  private readonly _createtUrl: string = "/api/ExpenseSubCategoryItem/create";
  private readonly _updateUrl: string = "/api/ExpenseSubCategoryItem/update";
  private readonly _getSubCategoryUrl: string = "/api/ExpenseSubCategoryItem/getSubCategory";
  private readonly _deleteUrl: string = "/api/ExpenseSubCategoryItem/delete";

  private get getAllExpenseBookingItemUrl() { return this.configurations.baseUrl + this._getAllExpenseBookingItemUrl; }
  private get createtUrl() { return this.configurations.baseUrl + this._createtUrl; }
  private get updateUrl() { return this.configurations.baseUrl + this._updateUrl; }
  private get getSubCategoryUrl() { return this.configurations.baseUrl + this._getSubCategoryUrl; }
  private get deleteUrl() { return this.configurations.baseUrl + this._deleteUrl; }

  constructor(http: HttpClient, configurations: ConfigurationService, injector: Injector) {

    super(http, configurations, injector);
  }

  getAllExpenseBookingItem(page?: number, pageSize?: number, name?: string) {
    let endpointUrl = `${this.getAllExpenseBookingItemUrl}/${page}/${pageSize}/${name}`;
    return this.http.get(endpointUrl, this.getRequestHeaders()).catch(error => {
      return this.handleError(error, () => this.getAllExpenseBookingItem(page, pageSize, name))
    });
  }
  create(request: any) {
    return this.http.post(this.createtUrl, JSON.stringify(request), this.getRequestHeaders()).
      catch(error => {
        return this.handleError(error, () => this.create(request));
      });
  }
  update(item: any, id?: string) {
    let endpointUrl = id ? `${this.updateUrl}/${id}` : this.updateUrl;
    return this.http.put(endpointUrl, JSON.stringify(item), this.getRequestHeaders()).
      catch(error => {
        return this.handleError(error, () => this.update(item, id));
      });
  }
  getSubCategory(id?: string) {
    let endpointUrl = `${this.getSubCategoryUrl}/${id}`;
    return this.http.post(endpointUrl, this.getRequestHeaders()).catch(error => {
      return this.handleError(error, () => this.getSubCategory(id))
    });
  }
  delete(id?: string) {
    let endpoint = `${this.deleteUrl}/${id}`
    return this.http.delete(endpoint, this.getRequestHeaders()).
      catch(error => {
        return this.handleError(error, () => this.delete(id));
      })
  }
}
