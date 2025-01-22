import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';

@Injectable({
  providedIn: 'root',
})
export class OrderService {
  private baseUrl = `${environment.apiUrl}/orders`;

  constructor(private http: HttpClient) {}

  // Get the token from session storage
  private getToken(): string | null {
    return sessionStorage.getItem('token');
  }

  // Prepare headers to include JWT token
  private getHttpOptions() {
    const token = this.getToken();
    const headers = new HttpHeaders({
      Authorization: token ? `Bearer ${token}` : '',
    });
    return { headers };
  }

  // Add a new order
  addOrder(order: any): Observable<any> {
    return this.http.post(`${this.baseUrl}`, order, this.getHttpOptions());
  }

  // Update order
  updateOrder(orderID: number, order: any): Observable<any> {
    return this.http.patch(`${this.baseUrl}/update?orderID=${orderID}`, order, this.getHttpOptions());
  }

  // Fetch orders
  fetchOrders(): Observable<any> {
    return this.http.get(`${this.baseUrl}`, this.getHttpOptions());
  }
}
