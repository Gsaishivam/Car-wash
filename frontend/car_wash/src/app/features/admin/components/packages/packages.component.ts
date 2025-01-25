import { Component, OnInit } from '@angular/core';
import { AdminService } from '../../services/admin.service';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { Package } from '../../models/packages.model';

@Component({
  selector: 'app-packages',
  templateUrl: './packages.component.html',
  styleUrls: ['./packages.component.css'],
  standalone: true,
    imports: [ReactiveFormsModule, CommonModule,FormsModule]
})
export class PackagesComponent implements OnInit {
  packages: Package[] = [];
  newPackage: Package = { packageID: 0, name: '', price: 0, description: '' };
  editingPackage: Package | null = null;

  constructor(private adminService: AdminService) { }

  ngOnInit(): void {
    this.fetchPackages();
  }
  // Fetch all packages from the backend
  fetchPackages(): void {
    this.adminService.getPackages().subscribe(
      (data) => {
        this.packages = data;
        console.log(this.packages);
      },
      (error) => {
        console.error('Error fetching packages', error);
      }
    );
  }

  // Delete a package by its ID
  deletePackage(id: number): void {
    if (confirm('Are you sure you want to delete this package?')) {
      this.adminService.deletePackage(id).subscribe(
        () => {
          alert('Package deleted successfully!');
          this.fetchPackages();  // Refresh the list after deletion
        },
        (error) => {
          console.error('Error deleting package', error);
        }
      );
    }
  }

  // Add a new package
  addPackage(): void {
    if (!this.newPackage.name || this.newPackage.price <= 0) {
      alert('Please provide valid package details!');
      return;
    }

    this.adminService.addPackage(this.newPackage).subscribe(
      (response) => {
        alert('Package added successfully!');
        this.fetchPackages();  // Refresh the list after adding a new package
        this.newPackage = {packageID: 0, name: '', price: 0 , description: ''}; // Reset form
      },
      (error) => {
        console.error('Error adding package', error);
      }
    );
  }
}