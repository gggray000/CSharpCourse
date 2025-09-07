import { Injectable } from '@angular/core';
import { PassengerService } from '../api/services';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(
    private passengerService: PassengerService,
    private router: Router
  ) { }

  currentUser?: User;

  loginUser(user: User) {
    this.passengerService.loginPassenger({ body: user })
      .subscribe({
        next: _ => {
          this.currentUser = user;
          this.router.navigate(['search-flights']);
        },
        error: err => {
          console.error(err);
        }
      });
  }

  logOut(email: string) {
     console.log(email + " logged out.");
    this.currentUser = undefined;
  }
}

interface User {
  email: string,
  password: string
} 
