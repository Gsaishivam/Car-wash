import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { OrderService } from '../services/order.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-order',
  templateUrl: './order.component.html',
  styleUrls: ['./order.component.css'],
  standalone: true,
  imports: [ReactiveFormsModule, CommonModule],
})
export class OrderComponent implements OnInit {
  orderForm: FormGroup;
  message = '';
  orders: any[] = [];

  constructor(private formBuilder: FormBuilder, private orderService: OrderService) {
    this.orderForm = this.formBuilder.group({
      washPackageName: ['', [Validators.required]],
      washPackagePrice: ['', [Validators.required]],
      carNumber: ['', [Validators.required]],
      carType: ['', [Validators.required]],
      orderDate: ['', [Validators.required]],
    });
  }

  ngOnInit(): void {
    this.fetchOrders();
  }

  // Fetch all orders for the logged-in user
  fetchOrders() {
    this.orderService.fetchOrders().subscribe(
      (orders) => {
        this.orders = orders;
      },
      (error) => {
        this.message = `Failed to load orders: ${error.error.message}`;
      }
    );
  }

  // Add a new order
  addOrder() {
    if (this.orderForm.valid) {
      const newOrder = this.orderForm.value;

      this.orderService.addOrder(newOrder).subscribe(
        (response) => {
          this.message = `Order added successfully. Order ID: ${response.orderID}`;
          alert(`Order added successfully. Order ID: ${response.orderID}`);
          this.fetchOrders(); // Fetch orders after adding a new one
        },
        (error) => {
          this.message = `Failed to add order: ${error.error.message}`;
        }
      );
    } else {
      this.message = 'Please fill out all required fields correctly.';
    }
  }

  // Update order
  updateOrder(orderID: number) {
    if (this.orderForm.valid) {
      const updatedOrder = this.orderForm.value;

      this.orderService.updateOrder(orderID, updatedOrder).subscribe(
        (response) => {
          this.message = 'Order updated successfully.';
          this.fetchOrders(); // Fetch orders after updating one
        },
        (error) => {
          this.message = `Failed to update order: ${error.error.message}`;
        }
      );
    } else {
      this.message = 'Please fill out all required fields correctly.';
    }
  }

  // Cancel order (Just an example of what you can do)
  cancelOrder(orderID: number) {
    this.orderService.updateOrder(orderID, { orderStatus: 2 }).subscribe(
      (response) => {
        this.message = 'Order cancelled successfully.';
        this.fetchOrders(); // Fetch orders after cancellation
      },
      (error) => {
        this.message = `Failed to cancel order: ${error.error.message}`;
      }
    );
  }
}

// import { Component, OnInit } from '@angular/core';

// // Mock data for orders
// interface Order {
//   orderId: number;
//   carNumber: string;
//   carType: string;
//   washPackageName: string;
//   washPackagePrice: number;
//   orderDate: string;
//   orderStatus: number;
//   paymentStatus: number;
// }

// @Component({
//   selector: 'app-order',
//   templateUrl: './order.component.html',
//   styleUrls: ['./order.component.css']
// })
// export class OrderComponent implements OnInit {
//   orders: Order[] = [];
//   newOrder: Order = {
//     orderId: 0,
//     carNumber: '',
//     carType: '',
//     washPackageName: '',
//     washPackagePrice: 0,
//     orderDate: '',
//     orderStatus: 1, // Active
//     paymentStatus: 0 // Unpaid
//   };

//   constructor() { }

//   ngOnInit(): void {
//     // Initialize with mock orders
//     this.orders = [
//       { orderId: 1, carNumber: 'ABC123', carType: 'Sedan', washPackageName: 'Basic Wash', washPackagePrice: 15, orderDate: '2025-01-20', orderStatus: 1, paymentStatus: 0 },
//       { orderId: 2, carNumber: 'XYZ456', carType: 'SUV', washPackageName: 'Premium Wash', washPackagePrice: 25, orderDate: '2025-01-19', orderStatus: 0, paymentStatus: 1 }
//     ];
//   }

//   addOrder(): void {
//     const newOrderId = this.orders.length + 1; // Increment order ID
//     const orderToAdd: Order = { ...this.newOrder, orderId: newOrderId };
//     this.orders.push(orderToAdd);
//     this.resetNewOrder();
//   }

//   updateOrder(orderId: number): void {
//     const orderIndex = this.orders.findIndex(order => order.orderId === orderId);
//     if (orderIndex !== -1) {
//       this.orders[orderIndex] = { ...this.orders[orderIndex], ...this.newOrder };
//       this.resetNewOrder();
//     }
//   }

//   cancelOrder(orderId: number): void {
//     const order = this.orders.find(order => order.orderId === orderId);
//     if (order) {
//       order.orderStatus = 2; // Set status to 'Cancelled'
//     }
//   }

//   private resetNewOrder(): void {
//     this.newOrder = {
//       orderId: 0,
//       carNumber: '',
//       carType: '',
//       washPackageName: '',
//       washPackagePrice: 0,
//       orderDate: '',
//       orderStatus: 1,
//       paymentStatus: 0
//     };
//   }
// }
