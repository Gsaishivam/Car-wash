import { Component } from '@angular/core';
import { LoginService } from '../services/login.service';
import { ReactiveFormsModule, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { CommonModule } from '@angular/common';
 import { AdminService } from '../../admin/services/admin.service';
import { Router, RouterModule } from '@angular/router';
@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
  standalone: true,
  imports: [ReactiveFormsModule, CommonModule,RouterModule],
})
export class LoginComponent {
  loginForm: FormGroup;
  message = '';
 
  constructor(private LoginService: LoginService, private AdminService:AdminService,private formBuilder: FormBuilder,private router: Router) {
    this.loginForm = this.formBuilder.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(3)]],
    });
  }
 
  login() {
    if (this.loginForm.valid) {
      const credentials = this.loginForm.value;
      this.LoginService.login(credentials).subscribe(
        (response) => {
          if (response && response.token) {
            // Store the token in session storage
            sessionStorage.setItem('token', response.token);
 
            // Confirm token storage in the session
            console.log('Token stored in session:', sessionStorage.getItem('token'));
 
            this.message = 'Login successful!';
            alert('Login successful!');
            //this.router.navigate(['/order']);
            window.location.reload();
          } else {
            this.message = 'Login failed: Token not found in response.';
          }
        },
        (error) => {
          this.message = `Login failed: ${error.error || 'Unknown error'}`;
        }
      );
    } 
    else {
      this.message = 'Please fill out all required fields correctly.';
    }
  }
}

