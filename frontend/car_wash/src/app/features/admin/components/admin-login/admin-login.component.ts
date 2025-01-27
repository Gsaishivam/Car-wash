import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AdminService } from '../../services/admin.service';
import { Router } from '@angular/router';
import { AuthService } from '/.NET/Car wash/frontend/car_wash/src/app/features/auth/services/auth.service'; 
@Component({
  selector: 'app-admin-login',
  templateUrl: './admin-login.component.html',
  styleUrl: './admin-login.component.css',
  standalone: true, 
    imports: [FormsModule,CommonModule],
})
export class AdminLoginComponent {
  email: string = '';
  password: string = '';
  message: string = '';

  constructor(
    private adminService: AdminService,
    private router: Router,
    private authService: AuthService  // Inject AuthService
  ) {}

  login(): void {
    this.adminService.adminLogin(this.email, this.password).subscribe(
      (response) => {
        if (response.token) {
          localStorage.setItem('token', response.token);
          sessionStorage.removeItem('token');
          this.authService.setAdminLoggedIn(true);
          alert('Login successful');
          this.router.navigate(['/admin/users']);
        } else {
          this.message = 'Login failed: No token received';
        }
      },
      (error) => {
        this.message = 'Invalid email or password';
      }
    );
  }
}