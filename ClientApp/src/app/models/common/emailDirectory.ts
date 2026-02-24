export class EmailDirectory{
  id: string;
  name: string;
  designation: string;
  email: string;
  phoneNumber: string;
}

export class EmailDirectoryViewModel {
  constructor(directoryListModel?: EmailDirectory[], totalCount?: number) {
    this.directoryListModel = new Array<EmailDirectory>();
    this.totalCount = totalCount;

  }
  public totalCount: number;
  public directoryListModel: EmailDirectory[];

}
