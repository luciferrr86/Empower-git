import { Injectable, Injector } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { EndpointFactory } from '../common/endpoint-factory.service';
import { ConfigurationService } from '../common/configuration.service';
import { AuthService } from '../common/auth.service';
import { CandidateBulkUpload } from '../../models/maintenance/candidate-bulk-upload.model';

@Injectable()
export class BulkUploadService extends EndpointFactory {
  private readonly _getUrl: string = "/api/BulkUpload/failedUpload";
  private readonly _postUrl: string = "/api/BulkUpload";
  private readonly _candidatePostUrl: string = "/api/BulkUpload/CandidateBulkUpload";
  private readonly _candidateListUrl: string = "/api/BulkUpload/candidateExcelUpload";
  private readonly _excelCandidateListUrl: string = "/api/BulkUpload/candidateExcelData";
  private readonly _updateCandidateDataUrl: string = "/api/BulkUpload/updateCandidateExcelData";
  private readonly _importDataUrl: string = "/api/BulkUpload/ImportCandidateBulkData";

  private get getUrl() { return this.configurations.baseUrl + this._getUrl; }
  private get postUrl() { return this.configurations.baseUrl + this._postUrl; }
  private get candidateListUrl() { return this.configurations.baseUrl + this._candidateListUrl; }
  private get candidatePostUrl() { return this.configurations.baseUrl + this._candidatePostUrl; }
  private get excelCandidateListUrl() { return this.configurations.baseUrl + this._excelCandidateListUrl; }
  private get updateExcelCandidateData() { return this.configurations.baseUrl + this._updateCandidateDataUrl; }
  private get importExcelData() { return this.configurations.baseUrl + this._importDataUrl; }

  constructor(http: HttpClient, configurations: ConfigurationService, injector: Injector, private auth: AuthService) {
    super(http, configurations, injector);
  }

  postFile(fileToUpload: File): Observable<any> {
    const formData: FormData = new FormData();
    formData.append('fileKey', fileToUpload, fileToUpload.name);
    let headers = new HttpHeaders();
    headers.append('Content-Type', 'multipart/form-data');
    headers.append('Accept', 'application/json');
    return this.http.post(`${this.postUrl}`, formData, { headers: headers })
      .catch(error => Observable.throw(error => {
        return this.handleError(error, () => this.postFile(fileToUpload));
      }))

  }

  getFailedBulkUpload(page?: number, pageSize?: number, name?: string): Observable<any> {
    let endpointUrl = `${this.getUrl}/${page}/${pageSize}/${name}`;
    return this.http.get(endpointUrl, this.getRequestHeaders()).catch(error => {
      return this.handleError(error, () => this.getFailedBulkUpload());
    })
  }

  //getCandidateBulkUpload(page?: number, pageSize?: number, name?: string): Observable<any> {
  //  let endpointUrl = `${this.candidateListUrl}/${page}/${pageSize}/${name}`;
  //  return this.http.get(endpointUrl, this.getRequestHeaders()).catch(error => {
  //    return this.handleError(error, () => this.getFailedBulkUpload());
  //  })
  //}

  candidatePostFile(fileToUpload: File): Observable<any> {
    const formData: FormData = new FormData();
    formData.append('fileKey', fileToUpload, fileToUpload.name);
    let headers = new HttpHeaders();
    headers.append('Content-Type', 'multipart/form-data');
    headers.append('Accept', 'application/json');
    return this.http.post(`${this.candidatePostUrl}`, formData, { headers: headers }).catch(error => {
      return this.handleError(error, () => this.candidatePostFile(fileToUpload));
    })
      

  }
  getExcelCandidateList(): Observable<any> {
    let endpointUrl = `${this.excelCandidateListUrl}`;
    return this.http.get(endpointUrl, this.getRequestHeaders()).catch(error => {
      return this.handleError(error, () => this.getExcelCandidateList());
    })
  }
  updateCandidateData(id: number, candidateName: string, jobName: string, jobTitle: string, email: string, level1ManagerId:number): Observable<string> {
    
    let endpointUrl = this.updateExcelCandidateData + "?Id=" + id + "&CandidateName=" + candidateName + "&JobName=" + jobName + "&JobTitle=" + jobTitle + "&Email=" + email + "&Level1ManagerId=" + level1ManagerId;
    return this.http.post(endpointUrl, null).
      catch(error => {
        return this.handleError(error, () => this.updateCandidateData(id, candidateName, jobName, jobTitle, email, level1ManagerId));
      });

  }



  importCandidateData(excelList: CandidateBulkUpload[]): Observable<any> {

    return this.http.post(this.importExcelData, excelList).
      catch(error => {
        return this.handleError(error, () => this.importCandidateData(excelList));
      })
  }
    //let endpointUrl = this.updateCandidateData;
    //return this.http.post(endpointUrl, this.getRequestHeaders()).
    //  catch(error => {
    //    return this.handleError(error, () => this.updateCandidateData());
    //  });
  
}
