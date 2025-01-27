import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AdminService } from '../admin/services/admin.service';
import { ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-admin-dashboard',
  templateUrl: './admin-dashboard.component.html',
  styleUrls: ['./admin-dashboard.component.css'],
  standalone: true,
    imports: [ReactiveFormsModule, CommonModule]
})
export class AdminDashboardComponent implements OnInit {
  categories: string[] = ['Users', 'Packages', 'Washers', 'Admin Functions'];

  constructor(private adminService: AdminService, private router: Router) {}

  ngOnInit(): void {
    // Ensure the admin is logged in
    if (!this.adminService.isAdminLoggedIn()) {
      this.router.navigate(['/admin']);
    }
  }

  // Handle logout and navigate back to login page
  logout(): void {
    this.adminService.logout();
    localStorage.removeItem('token');
    this.router.navigate(['/admin_login']);
  }

  // Navigate to the selected category (e.g., Users, Packages, etc.)
  navigateToCategory(category: string): void {
    switch (category) {
      case 'Users':
        this.router.navigate(['/admin/users']);
        break;
      case 'Packages':
        this.router.navigate(['/admin/packages']);
        break;
      case 'Washers':
        this.router.navigate(['/admin/washers']);
        break;
      case 'Admin Functions':
        this.router.navigate(['/admin/admin-functions']);
        break;
      default:
        break;
    }
  }
}

