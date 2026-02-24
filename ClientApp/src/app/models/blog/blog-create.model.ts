export class BlogCreate {
    constructor(title?: string, content?: string) {
      this.title = title;
      this.content = content;
    }
    public id: string;
    public title: string;
    public content: string;
    public publishedDate: Date;
    public isPublished: boolean;
    public urlSlug: string;
    public thumbnailImage: File;
    public headerImage: File;
    public thumbnailImagePath: string;
    public headerImagePath: string;
  }
  