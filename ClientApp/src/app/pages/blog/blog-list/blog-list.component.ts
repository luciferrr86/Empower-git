import { Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { BlogList, BlogVM } from '../../../models/blog/blog-list.model';
import { BlogService } from '../../../services/blog/blog.service';
import { AlertService } from '../../../services/common/alert.service';
import { Router } from '@angular/router';
import { Utilities } from '../../../services/common/utilities';

@Component({
  selector: 'app-blog-list',
  templateUrl: './blog-list.component.html',
  styleUrls: ['./blog-list.component.css']
})
export class BlogListComponent implements OnInit {
  public isSaving = false;
  columns: any[] = [];
  filterQuery: string = "";
  pageNumber = 0;
  count = 0;
  pageSize = 10;
  rows: BlogVM[] = [];
  rowsCache: BlogVM[] = [];
  blog = BlogVM;
  loadingIndicator: boolean = true;
  public serverCallback: () => void;
  @ViewChild('actionsTemplate')
  actionsTemplate: TemplateRef<any>;
  @ViewChild('publishTemplate')
  publishTemplate: TemplateRef<any>;
  @ViewChild('publishedDateTemplate')
  publishedDateTemplate: TemplateRef<any>;

  constructor(private alertService: AlertService, private blogService: BlogService, private router: Router) { }

  ngOnInit() {
    this.columns = [
      { prop: "index", name: '#', canAutoResize: false, width:40, sortable:false },
      { prop: 'title', name: 'Title',width:190 },
      { prop: 'publishedDate', name: 'Published Date',cellTemplate:this.publishedDateTemplate,width:75,sortable:true },
      { prop: 'isPublished', name: 'Published', cellTemplate: this.publishTemplate, resizeable: false, canAutoResize: false, sortable: false, draggable: false },
      { prop: 'Action', name: 'Action', cellTemplate: this.actionsTemplate, resizeable: false, canAutoResize: false, sortable: false, draggable: false }
    ];
    this.getAllBlogs(this.pageNumber, this.pageSize, this.filterQuery);
  }

  getAllBlogs(page?: number, pageSize?: number, name?: string) {
    this.loadingIndicator = true;
    this.blogService.getAllBlogs(page, pageSize, name).subscribe(result => this.onSuccessfulDataLoad(result), error => this.onDataLoadFailed());
  }
  getData(e) {
    this.getAllBlogs(this.pageNumber, e, this.filterQuery);
  }
  setPage(e) {
    this.getAllBlogs(e.offset, this.pageSize, this.filterQuery);
  }
  getFilteredData(filterString) {
    this.getAllBlogs(this.pageNumber, this.pageSize, filterString);

  }
  onSuccessfulDataLoad(blogs: BlogList) {
    this.rows = blogs.blogVM;
      blogs.blogVM.forEach((blogVM, index) => {
        (<any>blogVM).index = index + 1;
      });
    
    this.count = blogs.totalCount;
    this.loadingIndicator = false;
  }

  onDataLoadFailed() {
    this.alertService.showInfoMessage("Unable to retrieve blog list from the server");
  }

  public addBlog() {
    this.router.navigate(['/blog/blog-create']);
  }

  public editBlog(blogId:string) {
    this.router.navigate(['/blog/blog-create',blogId] );
  }

  public deleteBlog(blogId: string) {
    this.alertService.showConfirm("Are you sure you want to delete?", () => this.deleteBlogHelper(blogId));
  }

  private deleteBlogHelper(blogId: string) {
    this.blogService.deleteBlog(blogId).subscribe(
                                        () => {
                                              this.alertService.showInfoMessage("Blog deleted successfully.");
                                              this.getAllBlogs(this.pageNumber, this.pageSize, this.filterQuery);
                                              },
                                        () => {
                                              this.alertService.showInfoMessage("An error occured while deleting")
                                              });
  }
  published(blogId: string) {
    this.blogService.publish(blogId).subscribe(sucess => this.saveSuccessHelper(), error => this.saveFailedHelper(error));
  }

  private saveSuccessHelper() {
    this.isSaving = false;
    this.alertService.showSucessMessage("Action completed successfully.");
    this.serverCallback();
  }

  private saveFailedHelper(error: any) {
    this.isSaving = false;
    let test = Utilities.getHttpResponseMessage(error);
    this.alertService.showInfoMessage("Please try later" + test[0]);
  }

}
