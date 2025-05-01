import { Component, OnDestroy } from '@angular/core';
import { AddJob } from '../models/add-job.model';
import { JobapplicationService } from '../services/jobapplication.service';
import { Subscription } from 'rxjs';
import { Router } from '@angular/router';

@Component({
  selector: 'app-add-job-application',
  standalone: false,
  templateUrl: './add-job-application.component.html',
  styleUrl: './add-job-application.component.scss'
})
export class AddJobApplicationComponent implements OnDestroy {
  model: AddJob;
  private addJobApplicationSubscription?: Subscription

  constructor(private jobApplicationService: JobapplicationService, private router: Router){
    this.model = {
      companyName: '',
      position: '',
      status: 'Applied',
      dateApplied: new Date()
    };
  }
  
  onJobAdd(){
    //console.log(this.model);
   this.addJobApplicationSubscription = this.jobApplicationService.addJobapplication(this.model)
   .subscribe({
    next: (response) => {
      // console.log("Test - response - successful");
      // console.log(response);
      this.router.navigateByUrl('/jobapplications');
    },
    error: (error) => {
      // console.log("test - error");
      // console.log(error);
    }
   });
  }

  ngOnDestroy(): void {
    this.addJobApplicationSubscription?.unsubscribe();
  }

}
