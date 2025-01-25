// washers.component.ts
import { Component, OnInit } from '@angular/core';
import { AdminService } from '../../services/admin.service';
import { CommonModule } from '@angular/common';
import { forkJoin } from 'rxjs';

@Component({
  selector: 'app-washers',
  templateUrl: './washers.component.html',
  styleUrls: ['./washers.component.css'],
  standalone: true,
  imports: [CommonModule,]
})
export class WashersComponent implements OnInit {
  washers: any[] = [];

  constructor(private adminService: AdminService) {}

  ngOnInit(): void {
    this.fetchWashersWithOrders();
  }

  // Fetch washers along with their assigned orders
  fetchWashersWithOrders(): void {
    this.adminService.getWashersWithOrders().subscribe(
      (data) => {
        this.washers = data;
      },
      (error) => {
        console.error('Error fetching washers with orders:', error);
      }
    );
  }

  // Delete washer by ID
  deleteWasher(washerId: number): void {
    if (confirm('Are you sure you want to delete this washer?')) {
      this.adminService.deleteWasher(washerId).subscribe(
        (response) => {
          alert('Washer deleted successfully!');
          this.fetchWashersWithOrders(); // Refresh the list after deletion
        },
        (error) => {
          console.error('Error deleting washer:', error);
          alert('Error deleting washer. Please try again.');
        }
      );
    }
  }
}
