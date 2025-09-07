import { Component, OnInit } from '@angular/core';
import { PassengerService } from '../api/services';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { AuthService } from '../auth/auth.service';
import { Router, ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-login-passenger',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './login-passenger.component.html',
  styleUrl: './login-passenger.component.css'
})
export class LoginPassengerComponent {
constructor(
    private passengerService: PassengerService,
    private fb: FormBuilder,
    private authService: AuthService,
    private router: Router,
    private activatedRoute: ActivatedRoute
  ) { }

  requestedUrl?: string = undefined;

  form = this.fb.group({
    email: ['', Validators.compose([Validators.required, Validators.email, Validators.minLength(5), Validators.maxLength(100)])],
    password: ['', Validators.compose([Validators.required, Validators.minLength(6), Validators.maxLength(30)])]
  })

  ngOnInit(): void {
    this.activatedRoute.params.subscribe(p => this.requestedUrl = p['requestedUrl']);
  }

  login() {
    if (this.form.invalid)
      return;
    
    this.authService.loginUser({ 
      email: this.form.get('email')?.value ?? '',
      password: this.form.get('password')?.value ?? '' 
    })
  }
}
