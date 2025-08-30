import { Component } from '@angular/core';
import { BookingRm, BookDto } from '../api/models';
import { BookingService } from '../api/services';
import { AuthService } from '../auth/auth.service';
import { CommonModule } from '@angular/common';
import { DatePipe } from '@angular/common';
import { Router } from '@angular/router';

@Component({
  selector: 'app-my-bookings',
  standalone: true,
  imports: [CommonModule, DatePipe],
  templateUrl: './my-bookings.component.html',
  styleUrl: './my-bookings.component.css'
})
export class MyBookingsComponent {

  bookings!: BookingRm[];

  constructor(
    private bookingService: BookingService,
    private authService: AuthService,
    private router: Router
  ) { }

  ngOnInit(): void {
    if (!this.authService.currentUser)
      this.router.navigate(['/register-passenger'])

    this.bookingService.listBooking({ email: this.authService.currentUser?.email ?? '' })
      .subscribe(result => this.bookings = result);
  }

  private handleError(err: any) {
    console.log("Response Error: ", err);
  }

  cancel(booking: BookingRm) {
    const dto: BookDto = {
      flightId: booking.flightId,
      numberOfSeats: booking.numberOfBookedSeats,
      email: booking.email
    };
    this.bookingService.cancelBooking({ body: dto })
      .subscribe(_ => {
        this.bookings = this.bookings.filter(b => b != booking);
        this.handleError;
      });
  }
}
