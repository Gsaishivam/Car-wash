import { Component, OnInit } from '@angular/core';
import { Router, RouterModule } from '@angular/router';
import { AuthService } from '/.NET/Car wash/frontend/car_wash/src/app/features/auth/services/auth.service';  // Import AuthService
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-navbar',
  imports: [RouterModule,CommonModule],
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {
  isLoggedIn: boolean = false;
  isAdminLoggedIn: boolean = false;

  constructor(private router: Router, private authService: AuthService) {}

  ngOnInit(): void {
    this.authService.isLoggedIn$.subscribe((loggedIn) => {
      this.isLoggedIn = loggedIn;
    });

    this.authService.isAdminLoggedIn$.subscribe((adminLoggedIn) => {
      this.isAdminLoggedIn = adminLoggedIn;
    });
  }

  logout() {
    sessionStorage.removeItem('token'); 
    this.authService.setLoggedIn(false);
    alert('Logout successful!');
    this.router.navigate(['/login']);
  }

  Adminlogout() {
    localStorage.removeItem('token'); 
    this.authService.setAdminLoggedIn(false);
    alert('Logout successful!');
    this.router.navigate(['/admin_login']);
  }
}
