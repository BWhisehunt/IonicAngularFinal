import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError, tap } from 'rxjs/operators';
import { environment } from 'src/environments/environment';

export interface Task {
  taskId: number;
  title: string;
  completed: boolean;
}

@Injectable({
  providedIn: 'root'
})
export class TaskService {
  private apiUrl = environment.apiUrl;

  constructor(private http: HttpClient) {}

  getTasks(): Observable<Task[]> {
    return this.http.get<Task[]>(`${this.apiUrl}/task`).pipe(
      catchError(this.handleError)
    );
  }

  createTask(task: Task): Observable<Task> {
    const url = `${this.apiUrl}/task`; // Ensure this matches the backend route
    console.log('Sending create request to:', url);
    return this.http.post<Task>(url, task).pipe(
      tap((createdTask) => console.log('Task created:', createdTask)),
      catchError(this.handleError)
    );
  }

  updateTask(taskId: number, task: Task): Observable<Task> {
    const url = `${this.apiUrl}/task/${taskId}`;
    console.log('Sending update request to:', url);
    return this.http.put<Task>(url, task).pipe(
      tap((updatedTask) => console.log('Update successful:', updatedTask)),
      catchError(this.handleError)
    );
  }

  deleteTask(taskId: number): Observable<void> {
    const url = `${this.apiUrl}/task/${taskId}`;
    console.log('Sending delete request to:', url);
    return this.http.delete<void>(url).pipe(
      tap(() => console.log('Delete successful for task ID:', taskId)),
      catchError(this.handleError)
    );
  }

  private handleError(error: HttpErrorResponse): Observable<never> {
    let errorMessage = 'An unknown error occurred!';
    if (error.error instanceof ErrorEvent) {
      // Client-side or network error
      errorMessage = `An error occurred: ${error.error.message}`;
    } else {
      // Backend error
      errorMessage = `Server returned code: ${error.status}, error message is: ${error.message}`;
    }
    console.error(errorMessage);
    return throwError(() => new Error(errorMessage));
  }
}