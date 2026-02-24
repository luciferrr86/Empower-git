import { Component, OnInit } from "@angular/core";
import { ActivatedRoute } from "@angular/router";
import { JobApplyModel } from "../../models/recruitment/job-vacancy/job-apply.model";
import { AlertService, MessageSeverity} from "../../services/common/alert.service";
import { VacancyService } from "../../services/recruitment/vacancy.service";


@Component({
  selector: 'apply-job',
  templateUrl: './apply-job.component.html',
  styleUrls: ['./apply-job.component.css']
})
export class ApplyJobComponent implements OnInit  {
  jobApplyModel: JobApplyModel;
  constructor(private route: ActivatedRoute, private jobVacancyService: VacancyService, private alertService: AlertService) {
    this.jobApplyModel = new JobApplyModel();
  }
  jobName: string = "";
  jobDescription: string = "";
  isError: boolean;
  isSuccess: boolean = false;
  jobVancancyModel :any;
  
  ngOnInit() {
      
    if (this.route.snapshot.params['id'] != null) this.jobApplyModel.jobid = this.route.snapshot.params['id'];
    this.jobVacancyService.getJobCreation(this.jobApplyModel.jobid).
      subscribe((result) => {
        if (result.length !== 0) {
          this.jobName = result['jobTitle'];
          this.jobDescription = result['jobDescription'];
        }
      });
    }

  submit(files: any) {
    if (files.length === 0) {
      return;
    }

    // let fileToUpload = <File>files[0];
    // const formData = new FormData();    
    // formData.append('file', fileToUpload, fileToUpload.name);

    let fileToUpload : File[]=files;
    const formData=new FormData();

    Array.from(fileToUpload).map((file,index)=>
    {
      return formData.append('file'+index,file,file.name);
    });

    formData.append('jobApplyModel', JSON.stringify(this.jobApplyModel));

    this.jobVacancyService.applyforJob(formData)
      .subscribe(() => {
        this.isSuccess = true;
      }, 
      error =>{ 
        this.isError=true;
        console.log(error)
      });
     }    
   }
