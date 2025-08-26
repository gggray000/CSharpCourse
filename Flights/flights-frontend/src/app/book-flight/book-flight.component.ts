import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FlightService } from '../api/services';
import { BookDto, FlightRm } from '../api/models';
import { DatePipe } from '@angular/common';
import { AuthService } from '../auth/auth.service';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-book-flight',
  standalone: true,
  imports: [DatePipe, ReactiveFormsModule, CommonModule],
  templateUrl: './book-flight.component.html',
  styleUrl: './book-flight.component.css'
})
export class BookFlightComponent implements OnInit {

  flightId: string = 'not loaded';
  flight: FlightRm = {};

  form = this.fb.group({
    number: [1, Validators.compose([Validators.required, Validators.min(1), Validators.max(253)])]
  })

  constructor(private route: ActivatedRoute,
    private flightService: FlightService,
    private router: Router,
    private authService: AuthService,
    private fb: FormBuilder) { }

  ngOnInit(): void {

    if (!this.authService.currentUser)
      this.router.navigate(['/register-passenger']);

    this.route.paramMap
      .subscribe(param => this.findFlight(param.get("flightId")))
  }

  private findFlight = (flightId: string | null) => {
    this.flightId = flightId ?? 'not passed';

    this.flightService.findFlight({ id: this.flightId })
      .subscribe({
        next: response => this.flight = response,
        error: err => this.handleError(err)
      })
  }

  private handleError = (err: any) => {
    if (err.status == 404) {
      alert("Flight not found!");
      this.router.navigate(['/search-flights'])
    }
    console.log("Response Error: ", err.Status, " -- ", err.statusText);
  }

  book() {
    if (this.form.invalid)
      return;

    console.log(`Booking ${this.form.get('number')?.value} seats for flight: ${this.flight.id}`)
    const booking: BookDto = {
      flightId: this.flight.id,
      email: this.authService.currentUser?.email,
      numberOfSeats: this.form.get('number')?.value ?? 0
    }

    this.flightService.bookFlight({ body: booking })
      .subscribe({
        next: _ => this.router.navigate(['/my-booking']),
        error: err => this.handleError(err),
      })
  }

  get number() {
    return this.form.controls.number;
  }

}
