import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class SearchService {
  constructor(private http: HttpClient) {}

  searchWashPackages(searchParams: any): Observable<any[]> {
    const query = `?name=${searchParams.name || ''}&price=${searchParams.price || ''}&description=${searchParams.description || ''}`;
    return this.http.get<any[]>(`/search${query}`);
  }
}

