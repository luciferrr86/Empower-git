import { Component, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { SkillInterviewComponent } from '../skill-interview/skill-interview.component';
import { JobCreationComponent } from '../job-creation/job-creation.component';

@Component({
  selector: 'app-job-vacancy',
  templateUrl: './job-vacancy.component.html',
  styleUrls: ['./job-vacancy.component.css']
})
export class JobVacancyComponent implements OnInit {
  public isJobCreated = false;
  constructor(private route: ActivatedRoute, private router: Router) {

  }

  @ViewChild('skill')
  skillComponent: SkillInterviewComponent;

  @ViewChild('jobCreate')
  jobCreationComponent: JobCreationComponent;

  ngOnInit() {
    this.route.queryParams.subscribe(params => {
      if (params['id']) {
        this.isJobCreated = true;
      }
    });
  }
  loadSkillInterview() {
    this.skillComponent.getSkillQuestionDetail();
  }
}
