import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { CheckoutService } from '../services/checkout.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-checkout',
  templateUrl: './checkout.component.html',
  styleUrls: ['./checkout.component.css'],
  standalone: true,
  imports: [ReactiveFormsModule, CommonModule],
})
export class CheckoutComponent implements OnInit {
  checkoutForm: FormGroup;
  message: string = '';
  orderID: number = 0; // Assuming you have an orderID to checkout

  constructor(
    private formBuilder: FormBuilder,
    private checkoutService: CheckoutService
  ) {
    this.checkoutForm = this.formBuilder.group({
      orderID: [this.orderID, [Validators.required]],
      amount: ['', [Validators.required, Validators.min(1)]],
    });
  }

  ngOnInit(): void {
    // If needed, we can load the order ID dynamically from session storage or route params.
  }

  // Call API to process the checkout
  checkout() {
    if (this.checkoutForm.valid) {
      const checkoutDTO = this.checkoutForm.value;
      this.checkoutService.processCheckout(checkoutDTO).subscribe(
        (response) => {
          this.message = 'Checkout processed successfully!';
          alert('Checkout processed successfully!');
          console.log('Checkout success:', response);
        },
        (error) => {
          this.message = `Checkout failed: ${error.error?.message || error.message}`;
          console.error('Checkout error:', error);
        }
      );
    } else {
      this.message = 'Please fill out all required fields correctly.';
    }
  }
}
