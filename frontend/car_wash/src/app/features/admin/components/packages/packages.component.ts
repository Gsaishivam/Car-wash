import { Component, OnInit } from '@angular/core';
import { AdminService } from '../../services/admin.service';
import { ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-packages',
  templateUrl: './packages.component.html',
  styleUrls: ['./packages.component.css'],
  standalone: true,
    imports: [ReactiveFormsModule, CommonModule]
})
export class PackagesComponent implements OnInit {
  packages: any[] = [];

  constructor(private adminService: AdminService) {}

  ngOnInit(): void {
    this.adminService.getPackages().subscribe((data) => {
      this.packages = data;
    });
  }

  deletePackage(id: number): void {
    this.adminService.deletePackage(id).subscribe(() => {
      this.packages = this.packages.filter((pkg) => pkg.id !== id);
    });
  }
}
