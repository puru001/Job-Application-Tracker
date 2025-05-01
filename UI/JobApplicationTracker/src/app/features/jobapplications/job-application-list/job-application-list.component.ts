import { Component, OnInit } from '@angular/core';
import { JobapplicationService } from '../services/jobapplication.service';
import { ApiResponse, JobApplication } from '../models/job-application.model';
import { map, Observable, Subscription } from 'rxjs';

@Component({
  selector: 'app-job-application-list',
  standalone: false,
  templateUrl: './job-application-list.component.html',
  styleUrl: './job-application-list.component.scss'
})
export class JobApplicationListComponent implements OnInit {

  jobapplications$? : Observable<JobApplication[]>;  

  constructor(private jobApplicationService: JobapplicationService){
      
    }

  ngOnInit(): void {
   this.jobapplications$ = this.jobApplicationService.getAllJobapplications()
   .pipe(
    map(response => response.result)
   );    
  }

  updateApplicationStatus(jobapplication: JobApplication): void {
    this.jobApplicationService.updateJobApplication(jobapplication)
    .subscribe({
      next: (response) => {

      },
      error: (error) => {
        console.log("test - error");
        console.log(error);
      }
    });
  }
}
