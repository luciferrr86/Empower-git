import { Component, OnInit, ViewChild } from '@angular/core';
import { BlogService } from '../../../services/blog/blog.service';
import { AlertService } from '../../../services/common/alert.service';
import { NgForm } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { BlogCreate } from '../../../models/blog/blog-create.model';

@Component({
  selector: 'app-blog-create',
  templateUrl: './blog-create.component.html',
  styleUrls: ['./blog-create.component.css']
})
export class BlogCreateComponent implements OnInit {
  blogCreate: BlogCreate = new BlogCreate();
  isSubmit: boolean = false;
  isAddMode: boolean;
  blogId: string;

  @ViewChild('manageBlogForm') manageBlogForm: any;

  public serverCallback: () => void;

  quillConfiguration = {
    toolbar: [
      ['bold', 'italic', 'underline', 'strike'],
      ['blockquote', 'code-block'],
      [{ list: 'ordered' }, { list: 'bullet' }],
      [{ script: 'sub' }, { script: 'super' }],
      [{ header: [1, 2, 3, 4, 5, 6, false] }],
      [{ color: [] }, { background: [] }],
      ['link', 'image', 'video'],
      [{ 'font': [] }],
      [{ 'align': [] }],
      ['clean'],
    ]
  }

  constructor(private route: ActivatedRoute, private blogService: BlogService, private alertService: AlertService, private router: Router) {
    // this.blogCreate.publishedDate=new Date()
  }

  ngOnInit() {
    this.blogId = this.route.snapshot.params['id'];
    this.isAddMode = !this.blogId;
    if (this.blogId != null && this.blogId != 'undefined') {
      this.getBlogById(this.blogId);
    }
  }

  getBlogById(blogId: string) {
    this.blogService.getBlogById(blogId)
      .subscribe(data => {
        this.blogCreate = data;
        // console.log('Data:', data);
        // Load images if they exist
        if (this.blogCreate.thumbnailImagePath) {
          this.blogCreate.thumbnailImage = null; // clear the file input
        }
        if (this.blogCreate.headerImagePath) {
          this.blogCreate.headerImage = null; // clear the file input
        }
        // Load existing images if any
        // this.blogCreate.thumbnailImagePath = data.thumbnailImagePath;
        // this.blogCreate.headerImagePath = data.headerImagePath;
      });
  }
  
  onThumbnailImageSelected(event: any) {    
    this.blogCreate.thumbnailImage = event.target.files[0];
    const fileLabel = event.target.nextElementSibling as HTMLLabelElement; // Get the label element
    fileLabel.textContent = this.blogCreate.thumbnailImage.name;
  }

  onHeaderImageSelected(event: any) {
    this.blogCreate.headerImage = event.target.files[0];
    const fileLabel = event.target.nextElementSibling as HTMLLabelElement; // Get the label element
    fileLabel.textContent = this.blogCreate.headerImage.name;
  }
  manageBlog() {
    if (this.manageBlogForm.valid) {
      const formData = new FormData();
      formData.append('thumbnailImage', this.blogCreate.thumbnailImage);
      formData.append('headerImage', this.blogCreate.headerImage);
      formData.append('title', this.blogCreate.title);
      formData.append('publishedDate', this.blogCreate.publishedDate.toString());
      formData.append('content', this.blogCreate.content);
      if (this.blogId)
        formData.append('id', this.blogCreate.id);
      this.isSubmit = true;
      this.blogService.manageBlog(formData).subscribe(
        () => {
        this.alertService.showSucessMessage("Saved successfully");
        this.router.navigate(['/blog']);
      },
        () => {
          this.alertService.showInfoMessage("Unable to save the data! Please Try Again ");
        })
    }
  }
  //  public saveBlog() {
  //    this.isSaving = true;
  //    this.blogCreate.title = this.blogCreateForm.controls['title'].value;
  //    this.blogCreate.content = this.blogCreateForm.controls['content'].value;
  //    if (this.blogId != "") {
  //      this.blogService.manageBlog(this.blogCreate, this.blogId).subscribe(sucess => this.saveSuccessBlog(sucess), error => this.failedHelper(error)); 
  //    } else {
  //      this.blogService.manageBlog(this.blogCreate).subscribe(sucess => this.saveSuccessBlog(sucess), error => this.failedHelper(error));
  //    }
  //  }
  //private saveSuccessBlog(result?: string) {
  //    this.isSaving = false;
  //    this.blogId = result;
  //    // this.vacancyService.changeVacancyId(this.jobVacancyId);
  //    if (this.isNew) {
  //      this.alertService.showSucessMessage("Saved successfully");
  //      this.router.navigate(['/blog/blog-list'], { queryParams: { id: this.blogId } });

  //    } else {
  //      this.alertService.showSucessMessage("Updated successfully");
  //    }
  //  }
  //  private failedHelper(error: any) {
  //    this.isSaving = false;
  //    let test = Utilities.getHttpResponseMessage(error);
  //    console.log(test[0]);
  //    this.alertService.showInfoMessage("Unable to save the data! Please Try Again ");
  //  }
}
