import { Component, OnInit } from '@angular/core';
import { ReviewService } from '../services/review.service';
import { ActivatedRoute } from '@angular/router';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-reviews',
  imports: [ReactiveFormsModule,CommonModule,FormsModule],
  templateUrl: './reviews.component.html',
  styleUrl: './reviews.component.css'
})

export class ReviewComponent implements OnInit {

  reviews: any[] = [];
  reviewData = {
    OrderID: 0,
    Rating: 0,
    Review_comment: ''
  };
  userID: number = 1; // Assuming you have the userID from your authentication
  washerID: number = 1; // For washer reviews

  constructor(private reviewService: ReviewService, private route: ActivatedRoute) {}

  ngOnInit(): void {
    this.loadUserReviews();
    this.loadWasherReviews();
  }

  // Fetch reviews given by the user
  loadUserReviews() {
    this.reviewService.getReviewsGivenByUser(this.userID).subscribe(
      (data) => {
        this.reviews = data;
        console.log("test1"+ this.reviews);
      },
      (error) => {
        console.error('Error fetching user reviews', error);
      }
    );
  }

  // Fetch reviews for the washer
  loadWasherReviews() {
    this.reviewService.getReviewsForWasher(this.washerID).subscribe(
      (data) => {
        this.reviews = data;
        console.log("test2"+ this.reviews);
      },
      (error) => {
        console.error('Error fetching washer reviews', error);
      }
    );
  }

  // Add a review
  addReview() {
    this.reviewService.addReview(this.reviewData).subscribe(
      (response) => {
        console.log('Review added successfully', response);
        alert('Review added successfully!');
        this.loadUserReviews(); // Refresh the user reviews list
      },
      (error) => {
        console.error('Error adding review', error);
      }
    );
  }

  // Delete a review
  deleteReview(reviewId: number) {
    this.reviewService.deleteReview(reviewId).subscribe(
      (response) => {
        console.log('Review deleted successfully', response);
        this.loadUserReviews(); // Refresh the user reviews list
      },
      (error) => {
        console.error('Error deleting review', error);
      }
    );
  }
}