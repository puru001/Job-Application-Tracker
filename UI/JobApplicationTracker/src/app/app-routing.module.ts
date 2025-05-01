import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { JobApplicationListComponent } from './features/jobapplications/job-application-list/job-application-list.component';
import { AddJobApplicationComponent } from './features/jobapplications/add-job-application/add-job-application.component';
import { EditJobApplicationComponent } from './features/jobapplications/edit-job-application/edit-job-application.component';

const routes: Routes = [
  {
    path: '', redirectTo:'/jobapplications', pathMatch: 'full'  //default route
  },
  {
    path: 'jobapplications', component: JobApplicationListComponent
  },
  {
    path: 'jobapplications/add', component: AddJobApplicationComponent
  },
  {
    path: 'jobapplications/:id', component: EditJobApplicationComponent
  }
];
 
@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
