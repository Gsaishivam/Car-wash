import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private isLoggedInSubject = new BehaviorSubject<boolean>(false);
  private isAdminLoggedInSubject = new BehaviorSubject<boolean>(false);

  constructor() {
    this.isLoggedInSubject.next(!!sessionStorage.getItem('token'));
    this.isAdminLoggedInSubject.next(!!localStorage.getItem('token'));
  }

  setLoggedIn(isLoggedIn: boolean) {
    this.isLoggedInSubject.next(isLoggedIn);
  }

  setAdminLoggedIn(isAdminLoggedIn: boolean) {
    this.isAdminLoggedInSubject.next(isAdminLoggedIn);
  }

  get isLoggedIn$() {
    return this.isLoggedInSubject.asObservable();
  }

  get isAdminLoggedIn$() {
    return this.isAdminLoggedInSubject.asObservable();
  }
}
