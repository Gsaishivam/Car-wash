import { Component, OnInit } from '@angular/core';
import { AdminService } from '../../services/admin.service';
import { ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.css'],
  standalone: true,
  imports: [ReactiveFormsModule, CommonModule]
})
export class UsersComponent implements OnInit {
  users: any[] = [];
  orders: any[] = [];  // Added to store orders

  constructor(private adminService: AdminService) {}

  ngOnInit(): void {
    this.fetchUsers();
    this.fetchOrders();  // Fetch orders when the component is initialized
  }

   // Fetch the list of users
   fetchUsers(): void {
    this.adminService.getUsers().subscribe(
      (data) => {
        this.users = data;
      },
      (error) => {
        console.error('Error fetching users', error);
      }
    );
  }

  // Fetch the list of orders
  fetchOrders(): void {
    this.adminService.getAllOrders().subscribe(
      (data) => {
        this.orders = data; // Ensure this has the correct structure
        console.log(this.orders); // Log the orders to verify
      },
      (error) => {
        console.error('Error fetching orders', error);
      }
    );
  }

  // Delete a user by their ID
  deleteUser(userId: number): void {
    if (confirm('Are you sure you want to delete this user?')) {
      this.adminService.deleteUser(userId).subscribe(
        (response) => {
          alert('User deleted successfully!');
          this.fetchUsers();  // Refresh the list after deletion
        },
        (error) => {
          console.error('Error deleting user', error);
        }
      );
    }
  }

  // Delete an order by its ID
  deleteOrder(orderId: number): void {
    if (confirm('Are you sure you want to delete this order?')) {
      this.adminService.deleteOrder(orderId).subscribe(
        (response) => {
          alert('Order deleted successfully!');
          this.fetchOrders();  // Refresh the orders list after deletion
        },
        (error) => {
          console.error('Error deleting order', error);
        }
      );
    }
  }
  // Translate order status to readable text
  getOrderStatus(status: number): string {
    switch (status) {
      case 0: return 'Pending';
      case 1: return 'In Progress';
      case 2: return 'Completed';
      default: return 'Unknown';
    }
  }

  // Translate payment status to readable text
  getPaymentStatus(status: number): string {
    switch (status) {
      case 0: return 'Pending';
      case 1: return 'Paid';
      case 2: return 'Failed';
      default: return 'Unknown';
    }
  }

  // Translate wash status to readable text
  getWashStatus(status: number): string {
    switch (status) {
      case 0: return 'Not Completed';
      case 1: return 'Completed';
      default: return 'Unknown';
    }
  }
}