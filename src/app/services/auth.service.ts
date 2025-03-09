import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { IAuthToken } from '../types/auth';
import { catchError, throwError } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private baseUrl = 'https://localhost:7043/api/Account';
  constructor(private http: HttpClient) {}
  userEmail:string | undefined;

  login(email: string, password: string) {
    this.userEmail = email;
    return this.http
      .post<IAuthToken>(`${this.baseUrl}/Login`, {
        email: email,
        password: password,
      }
    )
      .pipe(
        catchError(this.handleError) 
      );
  }

  private handleError(error: HttpErrorResponse) {
    console.error('An error occurred:', error);
    return throwError(
      () => new Error('Something went wrong; please try again later.')
    );
  }

  logOut(){
    localStorage.removeItem('token');
    localStorage.removeItem('role');
    localStorage.removeItem('email');
  }

}
