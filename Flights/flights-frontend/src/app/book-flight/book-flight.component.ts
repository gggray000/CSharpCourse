import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FlightService } from '../api/services';
import { FlightRm } from '../api/models';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-book-flight',
  standalone: true,
  imports: [DatePipe],
  templateUrl: './book-flight.component.html',
  styleUrl: './book-flight.component.css'
})
export class BookFlightComponent implements OnInit {

  flightId: string = 'not loaded';
  flight: FlightRm = {};

  constructor(private route: ActivatedRoute,
    private flightService: FlightService,
    private router: Router) { }

  ngOnInit(): void {
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

}
