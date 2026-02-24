import { Injectable, Injector } from '@angular/core';
import { EndpointFactory } from '../common/endpoint-factory.service';
import { ConfigurationService } from '../common/configuration.service';
import { HttpClient } from '@angular/common/http';

@Injectable()
export class ExpenseBookingService extends EndpointFactory {

  private readonly _getAllExpenseBookingUrl: string = "/api/ExpenseBooking/list";
  private readonly _getEmployeeRequestUrl: string = "/api/ExpenseBooking/requestList";
  private readonly _getApproveListUrl: string = "/api/ExpenseBooking/approveList";
  private readonly _getAllApproveListUrl: string = "/api/ExpenseBooking/allapproveList";
  private readonly _getApproveListManagerExcelUrl: string = "/api/ExpenseBooking/approvedmanager";
  private readonly _getApproveListAllExcelUrl: string = "/api/ExpenseBooking/approvedall";
  private readonly _getAddRequestUrl: string = "/api/ExpenseBooking/getAddRequest";
  private readonly _addRequestUrl: string = "/api/ExpenseBooking/addRequest";
  private readonly _updateRequestUrl: string = "/api/ExpenseBooking/update";
  private readonly _viewRequestUrl: string = "/api/ExpenseBooking/ViewRequestApprover";
  private readonly _approveRejectUrl: string = "/api/ExpenseBooking/approveReject";
  private readonly _inviteApproveRejectUrl: string = "/api/ExpenseBooking/inviteApproveReject";
  private readonly _deleteDocumentUrl: string = "/api/ExpenseBooking/deletedocument";


  private readonly _inviteApproverUrl: string = "/api/ExpenseBooking/inviteApprover";
  private readonly _submitRequestUrl: string = "/api/ExpenseBooking/submitRequest";
  private readonly _deleteRequestUrl: string = "/api/ExpenseBooking/deleteRequest";

  private readonly _getAllCategoryUrl: string = "/api/ExpenseSubCategoryItem/categorylist";
  private readonly _createCategoryUrl: string = "/api/ExpenseSubCategoryItem/createcategory";
  private readonly _updateCategoryUrl: string = "/api/ExpenseSubCategoryItem/updatecategory";
  private readonly _deleteCategoryUrl: string = "/api/ExpenseSubCategoryItem/deletecategory";

  private readonly _getAllSubCategoryUrl: string = "/api/ExpenseSubCategoryItem/subcategorylist";
  private readonly _createSubCategoryUrl: string = "/api/ExpenseSubCategoryItem/createsubcategory";
  private readonly _updateSubCategoryUrl: string = "/api/ExpenseSubCategoryItem/updatesubcategory";
  private readonly _deleteSubCategoryUrl: string = "/api/ExpenseSubCategoryItem/deletesubcategory";

  private readonly _getAllTitleAmountUrl: string = "/api/ExpenseSubCategoryItem/expanseList";
  private readonly _createTitleAmountUrl: string = "/api/ExpenseSubCategoryItem/createexpensetitle";
  private readonly _updateTitleAmountUrl: string = "/api/ExpenseSubCategoryItem/updateexpensetitle";
  private readonly _deleteTitleAmountUrl: string = "/api/ExpenseSubCategoryItem/deleteexpensetitle";

  private readonly _getAllExpenseBookingItemUrl: string = "/api/ExpenseSubCategoryItem/list";
  private readonly _createUrl: string = "/api/ExpenseSubCategoryItem/create";
  private readonly _updateUrl: string = "/api/ExpenseSubCategoryItem/update";
  private readonly _getSubCategoryUrl: string = "/api/ExpenseSubCategoryItem/getSubCategory";
  private readonly _deleteUrl: string = "/api/ExpenseSubCategoryItem/delete";


  private get getAllSubCategoryUrl() { return this.configurations.baseUrl + this._getAllSubCategoryUrl; }
  private get createSubCategoryUrl() { return this.configurations.baseUrl + this._createSubCategoryUrl; }
  private get updateSubCategoryUrl() { return this.configurations.baseUrl + this._updateSubCategoryUrl; }
  private get deleteSubCategoryUrl() { return this.configurations.baseUrl + this._deleteSubCategoryUrl; }

  private get getAllTitleAmountUrl() { return this.configurations.baseUrl + this._getAllTitleAmountUrl; }
  private get createTitleAmountUrl() { return this.configurations.baseUrl + this._createTitleAmountUrl; }
  private get updateTitleAmountUrl() { return this.configurations.baseUrl + this._updateTitleAmountUrl; }
  private get deleteTitleAmountUrl() { return this.configurations.baseUrl + this._deleteTitleAmountUrl; }


  private get getAllCategoryUrl() { return this.configurations.baseUrl + this._getAllCategoryUrl; }
  private get createCategoryUrl() { return this.configurations.baseUrl + this._createCategoryUrl; }
  private get updateCategoryUrl() { return this.configurations.baseUrl + this._updateCategoryUrl; }
  private get deleteCategoryUrl() { return this.configurations.baseUrl + this._deleteCategoryUrl; }


  private get getAllExpenseBookingItemUrl() { return this.configurations.baseUrl + this._getAllExpenseBookingItemUrl; }
  private get createUrl() { return this.configurations.baseUrl + this._createUrl; }
  private get updateUrl() { return this.configurations.baseUrl + this._updateUrl; }
  private get getSubCategoryUrl() { return this.configurations.baseUrl + this._getSubCategoryUrl; }
  private get deleteUrl() { return this.configurations.baseUrl + this._deleteUrl; }

  private get getAllExpenseBookingUrl() { return this.configurations.baseUrl + this._getAllExpenseBookingUrl; }
  private get getEmployeeRequestUrl() { return this.configurations.baseUrl + this._getEmployeeRequestUrl; }
  private get getApproveListUrl() { return this.configurations.baseUrl + this._getApproveListUrl; }
  private get getAllApproveListUrl() { return this.configurations.baseUrl + this._getAllApproveListUrl; }


  private get getApproveListManagerExcelUrl() { return this.configurations.baseUrl + this._getApproveListManagerExcelUrl; }
  private get getApprovedAllExcelUrl() { return this.configurations.baseUrl + this._getApproveListAllExcelUrl; }

  private get getAddRequestUrl() { return this.configurations.baseUrl + this._getAddRequestUrl; }
  private get addRequestUrl() { return this.configurations.baseUrl + this._addRequestUrl; }
  private get updateRequestUrl() { return this.configurations.baseUrl + this._updateRequestUrl; }
  private get viewRequestUrl() { return this.configurations.baseUrl + this._viewRequestUrl; }
  private get inviteApproverUrl() { return this.configurations.baseUrl + this._inviteApproverUrl; }
  private get approveRejectUrl() { return this.configurations.baseUrl + this._approveRejectUrl; }
  private get inviteApproveRejectUrl() { return this.configurations.baseUrl + this._inviteApproveRejectUrl; }
  private get submitRequestUrl() { return this.configurations.baseUrl + this._submitRequestUrl; }
  private get deleteRequestUrl() { return this.configurations.baseUrl + this._deleteRequestUrl; }
  private get deleteDocumentUrl() { return this.configurations.baseUrl + this._deleteDocumentUrl; }
  constructor(http: HttpClient, configurations: ConfigurationService, injector: Injector) {

    super(http, configurations, injector);
  }

  getAllExpenseBooking() {
    let endpointUrl = `${this.getAllExpenseBookingUrl}`;
    return this.http.get(endpointUrl, this.getRequestHeaders()).catch(error => {
      return this.handleError(error, () => this.getAllExpenseBooking())
    });
  }

  getEmployeeRequest(id?: string, page?: number, pageSize?: number, name?: string) {
    let endpointUrl = `${this.getEmployeeRequestUrl}/${id}/${page}/${pageSize}/${name}`;
    return this.http.get(endpointUrl, this.getRequestHeaders()).catch(error => {
      return this.handleError(error, () => this.getEmployeeRequest(id, page, pageSize, name))
    });
  }

  getApproveList(id?: string, page?: number, pageSize?: number, name?: string) {
    let endpointUrl = `${this.getApproveListUrl}/${id}/${page}/${pageSize}/${name}`;
    return this.http.get(endpointUrl, this.getRequestHeaders()).catch(error => {
      return this.handleError(error, () => this.getApproveList(id, page, pageSize, name))
    });
  }

  getAllApproveList(id?: string, page?: number, pageSize?: number, name?: string) {
    let endpointUrl = `${this.getAllApproveListUrl}/${id}/${page}/${pageSize}/${name}`;
    return this.http.get(endpointUrl, this.getRequestHeaders()).catch(error => {
      return this.handleError(error, () => this.getAllApproveList(id, page, pageSize, name))
    });
  }


  getApprovedExcelManager(id?: string) {
    let endpointUrl = `${this.getApproveListManagerExcelUrl}/${id}`;
    return this.http.get(endpointUrl, this.getRequestHeaders()).catch(error => {
      return this.handleError(error, () => this.getApprovedExcelManager(id))
    });
  }
  getApprovedAllExcel() {
    let endpointUrl = `${this.getApprovedAllExcelUrl}`;
    return this.http.get(endpointUrl, this.getRequestHeaders()).catch(error => {
      return this.handleError(error, () => this.getApprovedAllExcel())
    });
  }

  getAddRequest(id?: string) {
    let endpointUrl = `${this.getAddRequestUrl}/${id}`;
    return this.http.get(endpointUrl, this.getRequestHeaders()).catch(error => {
      return this.handleError(error, () => this.getAddRequest(id))
    });
  }

  addRequest(request: any, id?: string) {
    let endpointUrl = `${this.addRequestUrl}/${id}`;
    return this.http.post(endpointUrl, JSON.stringify(request), this.getRequestHeaders()).
      catch(error => {
        return this.handleError(error, () => this.addRequest(request, id));
      });
  }
  updateRequest(request: any, id?: string) {
    let endpointUrl = id ? `${this.updateRequestUrl}/${id}` : this.updateRequestUrl;
    return this.http.put(endpointUrl, JSON.stringify(request), this.getRequestHeaders()).
      catch(error => {
        return this.handleError(error, () => this.updateRequest(request, id));
      });
  }

  viewRequest(id?: string) {
    let endpointUrl = `${this.viewRequestUrl}/${id}`;
    return this.http.get(endpointUrl, this.getRequestHeaders()).catch(error => {
      return this.handleError(error, () => this.viewRequest(id))
    });
  }

  approveReject(approve: any, id?: string) {
    let endpointUrl = `${this.approveRejectUrl}/${id}`;
    return this.http.put(endpointUrl, JSON.stringify(approve), this.getRequestHeaders()).
      catch(error => {
        return this.handleError(error, () => this.approveReject(approve, id));
      });
  }
  inviteApproveReject(approve: any, id?: string, employeeId?: string) {
    let endpointUrl = `${this.inviteApproveRejectUrl}/${id}/${employeeId}`;
    return this.http.put(endpointUrl, JSON.stringify(approve), this.getRequestHeaders()).
      catch(error => {
        return this.handleError(error, () => this.inviteApproveReject(approve, id, employeeId));
      });
  }
  inviteApprover(invite: any, id?: string) {
    let endpointUrl = `${this.inviteApproverUrl}/${id}`;
    return this.http.put(endpointUrl, JSON.stringify(invite), this.getRequestHeaders()).
      catch(error => {
        return this.handleError(error, () => this.inviteApprover(invite, id));
      });
  }

  submitRequest(id?: string) {
    let endpointUrl = `${this.submitRequestUrl}/${id}`;
    return this.http.get(endpointUrl, this.getRequestHeaders()).
      catch(error => {
        return this.handleError(error, () => this.submitRequest(id));
      });
  }
  delete(id?: string) {
    let endpoint = `${this.deleteRequestUrl}/${id}`
    return this.http.delete(endpoint, this.getRequestHeaders()).
      catch(error => {
        return this.handleError(error, () => this.delete(id));
      })
  }
  deleteDocument(id?: string) {
    let endpoint = `${this.deleteDocumentUrl}/${id}`
    return this.http.delete(endpoint, this.getRequestHeaders()).
      catch(error => {
        return this.handleError(error, () => this.delete(id));
      })
  }


  /*Category */

  getAllCategory(page?: number, pageSize?: number, name?: string) {
    let endpointUrl = `${this.getAllCategoryUrl}/${page}/${pageSize}/${name}`;
    return this.http.get(endpointUrl, this.getRequestHeaders()).catch(error => {
      return this.handleError(error, () => this.getAllCategory(page, pageSize, name))
    });
  }
  createCategory(request: any) {
    return this.http.post(this.createCategoryUrl, JSON.stringify(request), this.getRequestHeaders()).
      catch(error => {
        return this.handleError(error, () => this.create(request));
      });
  }
  updateCategory(item: any, id?: string) {
    let endpointUrl = id ? `${this.updateCategoryUrl}/${id}` : this.updateUrl;
    return this.http.put(endpointUrl, JSON.stringify(item), this.getRequestHeaders()).
      catch(error => {
        return this.handleError(error, () => this.updateCategory(item, id));
      });
  }
  deleteCategory(id?: string) {
    let endpoint = `${this.deleteCategoryUrl}/${id}`
    return this.http.delete(endpoint, this.getRequestHeaders()).
      catch(error => {
        return this.handleError(error, () => this.deleteCategory(id));
      })
  }

  /*Sub Category */

  getAllSubCategory(page?: number, pageSize?: number, name?: string) {
    let endpointUrl = `${this.getAllSubCategoryUrl}/${page}/${pageSize}/${name}`;
    return this.http.get(endpointUrl, this.getRequestHeaders()).catch(error => {
      return this.handleError(error, () => this.getAllExpenseBookingItem(page, pageSize, name))
    });
  }
  createSubCategory(request: any) {
    return this.http.post(this.createSubCategoryUrl, JSON.stringify(request), this.getRequestHeaders()).
      catch(error => {
        return this.handleError(error, () => this.createSubCategory(request));
      });
  }
  updateSubCategory(item: any, id?: string) {
    let endpointUrl = id ? `${this.updateSubCategoryUrl}/${id}` : this.updateUrl;
    return this.http.put(endpointUrl, JSON.stringify(item), this.getRequestHeaders()).
      catch(error => {
        return this.handleError(error, () => this.updateSubCategory(item, id));
      });
  }
  deleteSubCategory(id?: string) {
    let endpoint = `${this.deleteSubCategoryUrl}/${id}`
    return this.http.delete(endpoint, this.getRequestHeaders()).
      catch(error => {
        return this.handleError(error, () => this.deleteSubCategory(id));
      })
  }

  /*Sub Category */

  getAllTitleAmount(page?: number, pageSize?: number, name?: string) {
    let endpointUrl = `${this.getAllTitleAmountUrl}/${page}/${pageSize}/${name}`;
    return this.http.get(endpointUrl, this.getRequestHeaders()).catch(error => {
      return this.handleError(error, () => this.getAllTitleAmount(page, pageSize, name))
    });
  }
  createTitleAmount(request: any) {
    return this.http.post(this.createTitleAmountUrl, JSON.stringify(request), this.getRequestHeaders()).
      catch(error => {
        return this.handleError(error, () => this.createTitleAmount(request));
      });
  }
  updateTitleAmount(item: any, id?: string) {
    let endpointUrl = id ? `${this.updateTitleAmountUrl}/${id}` : this.updateUrl;
    return this.http.put(endpointUrl, JSON.stringify(item), this.getRequestHeaders()).
      catch(error => {
        return this.handleError(error, () => this.updateTitleAmount(item, id));
      });
  }
  deleteTitleAmount(id?: string) {
    let endpoint = `${this.deleteTitleAmountUrl}/${id}`
    return this.http.delete(endpoint, this.getRequestHeaders()).
      catch(error => {
        return this.handleError(error, () => this.deleteTitleAmount(id));
      });
  }

  /*Expense Item */

  getAllExpenseBookingItem(page?: number, pageSize?: number, name?: string) {
    let endpointUrl = `${this.getAllExpenseBookingItemUrl}/${page}/${pageSize}/${name}`;
    return this.http.get(endpointUrl, this.getRequestHeaders()).catch(error => {
      return this.handleError(error, () => this.getAllExpenseBookingItem(page, pageSize, name))
    });
  }
  create(request: any) {
    return this.http.post(this.createUrl, JSON.stringify(request), this.getRequestHeaders()).
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
    return this.http.get(endpointUrl, this.getRequestHeaders()).catch(error => {
      return this.handleError(error, () => this.getSubCategory(id))
    });
  }
  deleteExpenseItem(id?: string) {
    let endpoint = `${this.deleteUrl}/${id}`
    return this.http.delete(endpoint, this.getRequestHeaders()).
      catch(error => {
        return this.handleError(error, () => this.delete(id));
      })
  }
}
