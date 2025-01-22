import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AdminService } from '../../services/admin.service';
import { Router } from '@angular/router';

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
  
    constructor(private adminService: AdminService, private router: Router) {}
  
    login(): void {
      this.adminService.adminLogin(this.email, this.password).subscribe(
        (response) => {
          if (response.token) {
            // Store JWT token in localStorage
            localStorage.setItem('adminToken', response.token);
            
            // Navigate to the admin dashboard
            this.router.navigate(['/admin']);
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
