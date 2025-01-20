import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { SignupRequest } from '../models/signup-request.model'; // Model import

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private apiUrl = 'http://localhost:5160/signup';  // Adjust the backend URL

  constructor(private http: HttpClient) {}

  userSignup(user: SignupRequest): Observable<any> {
    return this.http.post(`${this.apiUrl}/user`, user); // Change the endpoint if needed
  }
}
