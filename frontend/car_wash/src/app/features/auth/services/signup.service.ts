import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment'; // Make sure to set the API URL in environment file

@Injectable({
  providedIn: 'root',
})
export class SignupService {
  private baseUrl = `${environment.apiUrl}/signup`;  

  constructor(private http: HttpClient) {}

  signup(user: any): Observable<any> {
    return this.http.post(`${this.baseUrl}/user`, user);
}}
