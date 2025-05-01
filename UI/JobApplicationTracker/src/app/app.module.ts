import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NavbarComponent } from './core/components/navbar/navbar.component';
import { JobApplicationListComponent } from './features/jobapplications/job-application-list/job-application-list.component';
import { AddJobApplicationComponent } from './features/jobapplications/add-job-application/add-job-application.component';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { EditJobApplicationComponent } from './features/jobapplications/edit-job-application/edit-job-application.component';


@NgModule({
  declarations: [
    AppComponent,
    NavbarComponent,
    JobApplicationListComponent,
    AddJobApplicationComponent,
    EditJobApplicationComponent
  ],
  imports: [   
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    HttpClientModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
