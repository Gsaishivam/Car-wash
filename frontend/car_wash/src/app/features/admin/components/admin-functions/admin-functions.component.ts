// admin-functions.component.ts
import { Component, OnInit } from '@angular/core';
import { AdminService } from '../../../admin/services/admin.service';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-admin-functions',
  templateUrl: './admin-functions.component.html',
  styleUrls: ['./admin-functions.component.css'],
  imports:[FormsModule,ReactiveFormsModule,CommonModule]
})
  export class AdminFunctionsComponent implements OnInit {
    ordersWithWasher: any[] = [];
    leaderboard: any[] = [];
    orderId: number = 0;
    paymentStatus: string = '';  // Update from 'orderStatus' to 'paymentStatus'
  
    constructor(private adminService: AdminService) {}
  
    ngOnInit(): void {
      // Fetch leaderboard and orders with washer data on component load
      this.getLeaderboard();
      this.getOrdersWithWasher();
    }
  
    getOrdersWithWasher(): void {
      // Call the service to get the orders with washer details
      this.adminService.getOrdersWithWasher().subscribe(data => {
        this.ordersWithWasher = data;
      }, error => {
        console.error('Error fetching orders with washer data:', error);
      });
    }
  
    getLeaderboard(): void {
      // Call the service to get the leaderboard data
      this.adminService.getLeaderboard().subscribe(data => {
        this.leaderboard = data;
        console.log(this.leaderboard);
      }, error => {
        console.error('Error fetching leaderboard data:', error);
      });
    }
  
    fetchPaymentStatus(): void {
      // Find the order payment status based on the entered orderId
      const order = this.ordersWithWasher.find(o => o.orderID === this.orderId);
      if (order) {
        this.paymentStatus = this.getPaymentStatusText(order.paymentStatus);
      } else {
        this.paymentStatus = 'Order not found';
      }
    }
  
    // Convert payment status code to text
    getPaymentStatusText(status: number): string {
      switch (status) {
        case 0:
          return 'Payment Pending';
        case 1:
          return 'Payment Completed';
        case 2:
          return 'Payment Failed';
        default:
          return 'Unknown Status';
      }
    }
  
    assignOrderToWasher(orderID: number): void {
      // Call the service to assign the order to the washer
      this.adminService.assignOrderToWasher(orderID).subscribe(response => {
        alert(`Order ID: ${orderID} has been assigned successfully.`);
        this.getOrdersWithWasher();  // Refresh the orders
      }, error => {
        console.error('Error assigning order:', error);
        alert('Failed to assign the order.');
      });
    }
  }