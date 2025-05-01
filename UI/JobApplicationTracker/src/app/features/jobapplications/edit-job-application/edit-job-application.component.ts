import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { map, Observable, Subscription } from 'rxjs';
import { JobapplicationService } from '../services/jobapplication.service';
import { JobApplication } from '../models/job-application.model';

@Component({
  selector: 'app-edit-job-application',
  standalone: false,
  templateUrl: './edit-job-application.component.html',
  styleUrl: './edit-job-application.component.scss'
})
export class EditJobApplicationComponent implements OnInit, OnDestroy{
  id: string | null = null;
  paramsSubscription?: Subscription;
  editJobApplicationSubscription?: Subscription;
  jobApplication$?: Observable<JobApplication>; 
  jobApplication!: JobApplication; 

  constructor(private route: ActivatedRoute, private jobApplicationService: JobapplicationService,
              private router: Router ) {        
  }
  
  ngOnInit(): void {
    this.paramsSubscription = this.route.paramMap.subscribe({
      next: (params) => {
        this.id = params.get('id');

        if(this.id) {
          this.jobApplication$ = this.jobApplicationService.getJobApplicationById(this.id)
             .pipe(
              map(response => response.result)
             ); 
             
          this.jobApplication$.subscribe((data)=>{
            this.jobApplication = { ...data };
          });
        }
      }
    });
  }  

  onJobApplicationEdit():void{    
    if(this.jobApplication.id){
      this.editJobApplicationSubscription = this.jobApplicationService.updateJobApplication(this.jobApplication)
      .subscribe({
        next: (response) => {
          this.router.navigateByUrl('/jobapplications');

        }
      })
    }
  }

  ngOnDestroy(): void {
    this.paramsSubscription?.unsubscribe();
    this.editJobApplicationSubscription?.unsubscribe();
  }
}
