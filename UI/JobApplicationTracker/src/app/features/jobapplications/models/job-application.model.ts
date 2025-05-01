export interface JobApplication { 
    id: number;   
    companyName: string;
    position: string;
    status: string;
    dateApplied: string;
}

export interface ApiResponse<T> {
    statusCode: number;
    isSuccess: boolean;
    errorMessages: string[] | null;
    result: T;
  }