import { Injectable, Injector } from '@angular/core';
import { Http, Headers, RequestOptions } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import { ConfigurationService } from '../common/configuration.service';
import { AuthService } from '../common/auth.service';
@Injectable()
export class FileUploadService {
  private readonly _postUrl: string = "/api/fileupload/create";
  private get postUrl() { return this.configurations.baseUrl + this._postUrl; }
  constructor(public http: Http, public authService: AuthService, public configurations: ConfigurationService, injector: Injector) {

  }

  upload(formData: FormData, param: any) {
    let headers = new Headers();
    headers.append('Authorization', 'Bearer ' + this.authService.accessToken);
    let options = new RequestOptions({ headers: headers });
    options.params = param;
    return this.http.post(this.postUrl, formData, options)
      .map(response => response.json())
      .catch(error => Observable.throw(error));
  }
}
