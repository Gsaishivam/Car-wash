import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';

@Injectable({
  providedIn: 'root',
})
export class CheckoutService {
  private baseUrl = `${environment.apiUrl}/checkout`;

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

  // Process checkout
  processCheckout(checkoutDTO: any): Observable<any> {
    return this.http.patch(`${this.baseUrl}`, checkoutDTO, this.getHttpOptions());
  }
}
