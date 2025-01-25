import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { Router, RouterModule} from '@angular/router';
@Component({
  selector: 'app-navbar',
  imports: [RouterModule,CommonModule],
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.css'
})
export class NavbarComponent implements OnInit {

  isLoggedIn: boolean = false;
  isAdminLoggedIn: boolean = false;
  constructor(private router: Router) {}

  ngOnInit(): void { 
    this.isLoggedIn = !!sessionStorage.getItem('token'); 
    this.isAdminLoggedIn = !!localStorage.getItem('token'); 
  }

  logout() {
    sessionStorage.removeItem('token'); 
    this.isLoggedIn = false;
    alert('Logout successful!');
    window.location.reload();  
    this.router.navigate(['/login']);  
  }
  Adminlogout() {
    localStorage.removeItem('token'); 
    this.isAdminLoggedIn = false;
    alert('Logout successful!');
    window.location.reload();  
    this.router.navigate(['/admin_login']);  
  }
}
