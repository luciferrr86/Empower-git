export class BlogList {
    constructor(blogVM?: BlogVM[], totalCount?: number) {
      this.blogVM = new Array<BlogVM>();
      this.totalCount = totalCount;
    }
    public blogVM: BlogVM[];
    public totalCount: number;
  }
  
  export class BlogVM {
    constructor(id?: string, title?: string, content?: string, publishedDate?: Date, isPublished?: boolean) {
      this.id = id;
      this.title = title;
      this.content = content;
      this.publishedDate = publishedDate;
      this.isPublished = isPublished;
    }
    public id: string;
    public title: string;
    public content: string;
    public publishedDate: Date;
    public isPublished: boolean;
    public urlSlug: string;
    public thumbnailImage: string;
    public headerImage: string;
    public thumbnailImagePath: string;
    public headerImagePath: string;
  }
  