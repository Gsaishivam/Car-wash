import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { CommonModule } from '@angular/common';
import { environment } from '../../../environments/environment';

@Component({
  selector: 'app-search',
  templateUrl: './search.component.html',
  styleUrls: ['./search.component.css'],
  standalone: true,
  imports: [ReactiveFormsModule, CommonModule],
})
export class SearchComponent implements OnInit {
  searchForm: FormGroup;
  searchResults: any[] = [];
  message: string = '';  // Message to show status
  isLoading: boolean = false; // For loading indication
    constructor(
      private fb: FormBuilder,
      private http: HttpClient
    ) {
      this.searchForm = this.fb.group({
        name: [''],
        price: [''],
        description: [''],
      });
    }
  
    ngOnInit(): void {}
  
    // Function triggered when the user clicks on the 'Search' button
    search(): void {
      // Reset results and message before triggering a new search
      this.searchResults = [];
      this.message = '';
  
      // Show loading indicator
      this.isLoading = true;
  
      const searchParams = this.searchForm.value;

      this.getSearchResults(searchParams).subscribe(
        (response) => {
          this.isLoading = false; 
          if (response.length > 0) {
            this.searchResults = response;
          } else {
            this.searchResults = [];
            this.message = 'No results found.';
          }
        },
        (error) => {
          this.isLoading = false;
          console.error('Search failed:', error);
          this.message = 'Search failed. Please try again later.';
        }
      );
    }
  
    // Fetch search results with or without parameters
    getSearchResults(params: any): Observable<any[]> {
      let query = '';
      // Only include query parameters if they exist
      if (params) {
        query = `?name=${params.name || ''}&price=${params.price || ''}&description=${params.description || ''}`;
      }
  
      // Send the GET request to the backend with optional query parameters
      return this.http.get<any[]>(`${environment.apiUrl}/search${query}`);
    }
  }
  


