import { Component, OnInit } from '@angular/core';
import { PassengerService } from '../api/services';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { AuthService } from '../auth/auth.service';
import { Router, ActivatedRoute } from '@angular/router';

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
    private router: Router,
    private activatedRoute: ActivatedRoute
  ) { }

  requestedUrl?: string = undefined;

  fieldTextType: boolean = false;

  form = this.fb.group({
    email: ['', Validators.compose([Validators.required, Validators.email, Validators.minLength(5), Validators.maxLength(100)])],
    firstName: ['', Validators.compose([Validators.required, Validators.minLength(2), Validators.maxLength(30)])],
    lastName: ['', Validators.compose([Validators.required, Validators.minLength(2), Validators.maxLength(30)])],
    isFemale: [true, Validators.required],
    password: ['', Validators.compose([Validators.required, Validators.minLength(6), Validators.maxLength(30)])]
  })

  ngOnInit(): void {
    this.activatedRoute.params.subscribe(p => this.requestedUrl = p['requestedUrl']);

  }

  checkPassenger(): void {
    const params = { email: this.form.get('email')?.value ?? '' }

    this.passengerService.findPassenger(params)
      .subscribe({
        next: _ => {
          console.log("Passenger exists, logging in now");
          this.router.navigate(['login-passenger']);
        },
        error: e => {
          if (e.status != 404)
            console.error(e);
        }
      })
  }

  toggleFieldTextType() {
    this.fieldTextType = !this.fieldTextType;
  }

  register() {
    if (this.form.invalid)
      return;

    this.passengerService.registerPassenger({ body: this.form.value })
      .subscribe({
        next: _ => this.router.navigate(['login-passenger']),
        error: console.error,
      });
  }

}