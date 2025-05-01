import { Injectable } from '@angular/core';
import { AddJob } from '../models/add-job.model';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ApiResponse, JobApplication } from '../models/job-application.model';
import { environment } from '../../../../environments/environment.development';

@Injectable({
  providedIn: 'root'
})
export class JobapplicationService {
  private apiUrl = `${environment.apiBaseUrl}/api/JobApplicationAPI`;
  
  constructor(private http: HttpClient) { }

  addJobapplication(model: AddJob): Observable<AddJob> {
    return this.http.post<AddJob>(this.apiUrl, model);
  }  

  getAllJobapplications(): Observable<ApiResponse<JobApplication[]>> {
    return this.http.get<ApiResponse<JobApplication[]>>(this.apiUrl);
  }

  getUrlWithId(id:string): string {
    return `${this.apiUrl}/${id}`;
  }

  getJobApplicationById(id: string): Observable<ApiResponse<JobApplication>> {
    return this.http.get<ApiResponse<JobApplication>>(this.getUrlWithId(id));
  }

  updateJobApplication(jobapplication: JobApplication){
    return this.http.put<JobApplication>(this.getUrlWithId(jobapplication.id.toString()), jobapplication);
  }
}
