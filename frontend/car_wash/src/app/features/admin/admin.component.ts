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
    // Check if admin is logged in
    if (!this.adminService.isAdminLoggedIn()) {
      this.router.navigate(['/admin/dashboard']);
    }
  }

  logout(): void {
    localStorage.removeItem('adminToken');
    this.router.navigate(['/admin_login']);
  }
}

