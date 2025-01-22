// admin.service.ts
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';

@Injectable({
  providedIn: 'root',
})
export class AdminService {
  private apiUrl = `${environment.apiUrl}/admin`;  // Replace with your backend URL

  constructor(private http: HttpClient) {}

  // Admin Login - Save JWT token after login
  adminLogin(email: string, password: string): Observable<any> {
    const body = { email, password };
    return this.http.post<any>(`${this.apiUrl}/login`, body);
  }

  // Get the admin JWT token from localStorage
  getToken(): string | null {
    return localStorage.getItem('adminToken');
  }

  // Check if the admin is logged in by checking for the token
  isAdminLoggedIn(): boolean {
    return this.getToken() !== null;
  }

  // Get users
  getUsers(): Observable<any[]> {
    return this.http.get<any[]>(`${this.apiUrl}/users`);
  }

  // Get packages
  getPackages(): Observable<any[]> {
    return this.http.get<any[]>(`${this.apiUrl}/packages`);
  }

  // Get washers
  getWashers(): Observable<any[]> {
    return this.http.get<any[]>(`${this.apiUrl}/washers`);
  }

  // Delete user
  deleteUser(id: number): Observable<any> {
    return this.http.delete<any>(`${this.apiUrl}/users/${id}`);
  }

  // Delete washer
  deleteWasher(id: number): Observable<any> {
    return this.http.delete<any>(`${this.apiUrl}/washers/${id}`);
  }

  // Delete package
  deletePackage(id: number): Observable<any> {
    return this.http.delete<any>(`${this.apiUrl}/packages/${id}`);
  }
}
