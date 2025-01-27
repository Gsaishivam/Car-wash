import { Component } from '@angular/core';
import { LoginService } from '../services/login.service';
import { ReactiveFormsModule, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { CommonModule } from '@angular/common';
 import { AdminService } from '../../admin/services/admin.service';
import { Router, RouterModule } from '@angular/router';
import { AuthService } from '/.NET/Car wash/frontend/car_wash/src/app/features/auth/services/auth.service';
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

  constructor(
    private LoginService: LoginService,
    private formBuilder: FormBuilder,
    private router: Router,
    private authService: AuthService  // Inject AuthService
  ) {
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
            sessionStorage.setItem('token', response.token);
            localStorage.removeItem('token');
            this.authService.setLoggedIn(true);
            alert('Login successful!');
            //window.location.reload();
            this.router.navigate(['/search']);
          } else {
            this.message = 'Login failed: Token not found in response.';
          }
        },
        (error) => {
          this.message = `Login failed: ${error.error || 'Unknown error'}`;
        }
      );
    } else {
      this.message = 'Please fill out all required fields correctly.';
    }
  }
}


