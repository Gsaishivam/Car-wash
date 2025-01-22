import { Component, OnInit } from '@angular/core';
import { AdminService } from '../../services/admin.service';
import { ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-washers',
  templateUrl: './washers.component.html',
  styleUrls: ['./washers.component.css'],
  standalone: true,
  imports: [ReactiveFormsModule, CommonModule]
})
export class WashersComponent implements OnInit {
  washers: any[] = [];

  constructor(private adminService: AdminService) {}

  ngOnInit(): void {
    this.adminService.getWashers().subscribe((data) => {
      this.washers = data;
    });
  }

  deleteWasher(id: number): void {
    this.adminService.deleteWasher(id).subscribe(() => {
      this.washers = this.washers.filter((washer) => washer.id !== id);
    });
  }
}
