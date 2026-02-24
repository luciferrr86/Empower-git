import { Injectable, Injector } from '@angular/core';
import { EndpointFactory } from '../common/endpoint-factory.service';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { ConfigurationService } from '../common/configuration.service';
import { Observable } from 'rxjs/Observable';
import { BlogVM } from '../../models/blog/blog-list.model';
import { BlogCreate } from '../../models/blog/blog-create.model';

@Injectable()
export class BlogService extends EndpointFactory {
  private readonly _listBlogUrl: string = "/api/Blogs/list";
  private readonly _getBlogUrl: string = "/api/Blogs/blog";
  private readonly _manageBlogUrl: string = "/api/Blogs/manageBlog";
  private readonly _deleteBlogUrl: string = "/api/Blogs/delete";
  private readonly _publishBlogUrl: string = "/api/Blogs/publish";

  private get listBlogUrl() { return this.configurations.baseUrl + this._listBlogUrl; }
  private get getBlogUrl() { return this.configurations.baseUrl + this._getBlogUrl; }
  private get manageBlogUrl() { return this.configurations.baseUrl + this._manageBlogUrl; }
  private get deleteBlogUrl() { return this.configurations.baseUrl + this._deleteBlogUrl; }
  private get publishBlogUrl() { return this.configurations.baseUrl + this._publishBlogUrl; }

  constructor(http: HttpClient, configurations: ConfigurationService, injector: Injector) {
    super(http, configurations, injector);
  }

  getAllBlogs(page?: number, pageSize?: number, name?: string): Observable<any> {
    let endpointUrl = `${this.listBlogUrl}/${page}/${pageSize}/${name}`;
    return this.http.get(endpointUrl, this.getRequestHeaders()).catch(error => {
      return this.handleError(error, () => this.getAllBlogs(page, pageSize, name));
    })
  }

  getBlogById(blogId?: string) {
    let endpointUrl = `${this.getBlogUrl}/${blogId}`;
    return this.http.get(endpointUrl, this.getRequestHeaders()).catch(error => {
      return this.handleError(error, () => this.getBlogById(blogId));
    })
  }

  manageBlog(blog: FormData) {
    let endpointUrl = `${this.manageBlogUrl}`;
    const headers = new HttpHeaders();
    headers.set('Content-Type', 'multipart/form-data');
    return this.http.post(endpointUrl, blog, { headers })
      .catch(error => {
        return this.handleError(error, () => this.manageBlog(blog));
      });
  }

  deleteBlog(blogId: string | BlogVM) {
    let endpointUrl = `${this.deleteBlogUrl}/${blogId}`;
    return this.http.delete(endpointUrl, this.getRequestHeaders())
      .catch(error => {
        return this.handleError(error, () => this.deleteBlog(blogId));
      });
  }

  publish(blogId: string) {
    let endpointUrl = `${this.publishBlogUrl}/${blogId}`;
    return this.http.post(endpointUrl, this.getRequestHeaders()).catch(error => {
      { return this.handleError(error, () => this.publish(blogId)); }
    })
  }

}
