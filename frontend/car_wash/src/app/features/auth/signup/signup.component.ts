import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { SignupService } from '../services/signup.service'; // Import the signup service
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-signup',
  templateUrl: './signup.component.html',
  styleUrls: ['./signup.component.css'],
  standalone: true,
  imports: [ReactiveFormsModule, CommonModule,RouterModule]
})
export class SignupComponent {
  signupForm: FormGroup;
  message = '';

  constructor(private formBuilder: FormBuilder, private signupService: SignupService) {
    this.signupForm = this.formBuilder.group({
      firstName: ['', [Validators.required, Validators.minLength(2)]],
      lastName: ['', [Validators.required, Validators.minLength(2)]],
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(6)]],
      phoneNumber: ['', [Validators.required, Validators.pattern('^[0-9]{10}$')]] // Regex for 10-digit phone number
    });
  }

  signup() {
    if (this.signupForm.valid) {
      const user = this.signupForm.value;
      this.signupService.signup(user).subscribe(
        (response) => {
          if (response && response.token) {
            sessionStorage.setItem('token', response.token); // Store token if response is successful
            this.message = 'Signup successful!';
          } else {
            this.message = 'Signup failed: Token not found in response.';
          }
        },
        (error) => {
          this.message = `Signup failed: ${error.error || 'Unknown error'}`;
        }
      );
    } else {
      this.message = 'Please fill out all required fields correctly.';
    }
  }
}
