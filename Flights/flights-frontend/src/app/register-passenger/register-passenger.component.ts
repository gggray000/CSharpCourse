import { Component, OnInit } from '@angular/core';
import { PassengerService } from '../api/services';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { AuthService } from '../auth/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register-passenger',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './register-passenger.component.html',
  styleUrl: './register-passenger.component.css'
})
export class RegisterPassengerComponent implements OnInit {

  constructor(
    private passengerService: PassengerService,
    private fb: FormBuilder,
    private authService: AuthService,
    private router: Router
  ) { }

  form = this.fb.group({
    email: ['', Validators.compose([Validators.required, Validators.email,Validators.minLength(5), Validators.maxLength(100)])],
    firstName: ['', Validators.compose([Validators.required, Validators.minLength(2), Validators.maxLength(30)])],
    lastName: ['', Validators.compose([Validators.required,Validators.minLength(2), Validators.maxLength(30)])],
    isFemale: [true, Validators.required]
  })

  ngOnInit(): void {

  }

  checkPassenger(): void {
    const params = { email: this.form.get('email')?.value ?? '' }

    this.passengerService.findPassenger(params)
      .subscribe({
        next: _ => {
          console.log("Passenger exists, logging in now");
          this.login();
        },
        error: e => {
          if(e.status != 404)
            console.error(e);
          }
      })
  }

  register() {
    if(this.form.invalid)
      return;

    this.passengerService.registerPassenger({ body: this.form.value })
      .subscribe({
        next: this.login,
        error: console.error,
      });
  }

  private login = () => {
    this.authService.loginUser({
      email: this.form.get('email')?.value ?? ''
    });
    this.router.navigate(['/search-flights'])
  }
}