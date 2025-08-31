import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor() { }

  currentUser?: User;

  loginUser(user: User) {
    console.log("Log in the user with email " + user.email);
    this.currentUser = user;
  }

  logOut(user: User) {
     console.log(user.email + " logged out.");
    this.currentUser = undefined;
  }
}

interface User {
  email: string
} 
