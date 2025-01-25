import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { forkJoin, map, Observable, switchMap, tap } from 'rxjs';
import { environment } from '../../../environments/environment';
import { Package } from '../models/packages.model';

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
    return localStorage.getItem('token');
  }

  // Check if the admin is logged in by checking for the token
  isAdminLoggedIn(): boolean {
    return this.getToken() !== null;
  }

  logout(): void {
    localStorage.removeItem('token');
  }

  // Users API calls
  getUsers(): Observable<any[]> {
    return this.http.get<any[]>(`${this.apiUrl}/users`, this.getHeaders());
  }

  deleteUser(id: number): Observable<any> {
    return this.http.delete(`${this.apiUrl}/users/${id}`, this.getHeaders());
  }

  // Orders API calls
  getAllOrders(): Observable<any[]> {
    return this.http.get<any[]>(`${this.apiUrl}/orders`, this.getHeaders());
  }

  deleteOrder(orderId: number): Observable<any> {
    return this.http.delete(`${this.apiUrl}/orders/${orderId}`, this.getHeaders());
  }
  // Get all washers
  getWashersWithOrders(): Observable<any> {
    return this.http.get<any[]>(`${this.apiUrl}/washers`, this.getHeaders());
  }

  // Fetch orders by their IDs
  getOrdersByIds(orderIds: number[]): Observable<any[]> {
    const orderRequests = orderIds.map(orderId =>
      this.http.get<any>(`${this.apiUrl}/orders/${orderId}`, this.getHeaders())
    );
    return forkJoin(orderRequests);
  }

  // Delete washer
  deleteWasher(washerId: number): Observable<any> {
    return this.http.delete(`${this.apiUrl}/washers/${washerId}`, this.getHeaders());
  }
  // Fetch all packages
  getPackages(): Observable<Package[]> {
    return this.http.get<Package[]>(`${this.apiUrl}/packages`, this.getHeaders());
  }

  // Delete a package by ID
  deletePackage(id: number): Observable<any> {
    return this.http.delete(`${this.apiUrl}/packages/${id}`, this.getHeaders());
  }

  // Add a new package
  addPackage(packageData: Package): Observable<any> {
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${this.getToken()}`,
    });
    return this.http.post(`${this.apiUrl}/packages`, packageData, { headers });
  }
  // Get leaderboard data
  getLeaderboard(): Observable<any[]> {
    return this.http.get<any[]>(`${this.apiUrl}/leaderboard`,this.getHeaders());
  }

  // Get orders with washer data
  getOrdersWithWasher(): Observable<any[]> {
    return this.http.get<any[]>(`${this.apiUrl}/orders`,this.getHeaders());
  }

  // Fetch order status by OrderID
  getpaymentStatus(orderId: number): Observable<any> {
    return this.http.get<any>(`${this.apiUrl}/orders/${orderId}`,this.getHeaders());
  }

  // Assign order to washer (implement this as per your backend API)
  assignOrderToWasher(orderId: number): Observable<any> {
    return this.http.patch<any>(`http://localhost:5160/washing/assign/?OrderID=${orderId}`, { OrderID: orderId },this.getHeaders());
  }

    private getHeaders() {
      const token = localStorage.getItem('token');  
      console.log(token,"debug 01");
      return {
        headers: new HttpHeaders({
          'Authorization': `Bearer ${token}`
        })
      };
  
}}
