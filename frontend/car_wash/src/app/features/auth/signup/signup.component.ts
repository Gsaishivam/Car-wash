import { Component } from '@angular/core';
import { SignupRequest } from '../models/signup-request.model';  // Create this model
import { UserService } from '../services/user.service'; // Service to handle signup
import { Router } from '@angular/router';
import { FormsModule } from '@angular/forms';
@Component({
  selector: 'app-signup',
  templateUrl: './signup.component.html',
  styleUrls: ['./signup.component.css'],
  imports: [FormsModule]
})
export class SignupComponent {
  model: SignupRequest = {
    firstName: '',
    lastName: '',
    email: '',
    password: '',
    phoneNumber: ''
  };

  constructor(private userService: UserService, private router: Router) {}

  onSubmit() {
    this.userService.userSignup(this.model).subscribe(
      response => {
        console.log('Signup successful', response);
        this.router.navigate(['/login']); // Redirect to login after successful signup
      },
      error => {
        console.error('Signup error', error);
      }
    );
  }
}
