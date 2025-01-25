import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '/.NET/Car wash/frontend/car_wash/src/app/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ReviewService {

  private apiUrl = `${environment.apiUrl}/reviews`; // Adjust with your backend API
  constructor(private http: HttpClient) {}

  // Add a review
  addReview(reviewData: any): Observable<any> {
    return this.http.post<any>(`${this.apiUrl}/add`, reviewData, { headers: this.getHeaders() });
  }

  deleteReview(reviewId: number): Observable<any> {
    return this.http.delete<any>(`${this.apiUrl}/delete/${reviewId}`, { headers: this.getHeaders() });
  }

  getReviewsForWasher(washerID: number): Observable<any[]> {
    return this.http.get<any[]>(`${this.apiUrl}/washer/?washerID=${washerID}`, { headers: this.getHeaders() });
  }
  getReviewsGivenByUser(userID: number): Observable<any[]> {
    return this.http.get<any[]>(`${this.apiUrl}/user?userID=${userID}`, { headers: this.getHeaders() });
  }

  private getHeaders() {
    return {
      'Authorization': `Bearer ${sessionStorage.getItem('token')}`, // Or sessionStorage based on your storage method
      'Content-Type': 'application/json'
    };
  }
}