import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FlightService } from '../api/services';
import { FlightRm } from '../api/models';
import { RouterLink } from "@angular/router";

@Component({
  selector: 'app-search-flights',
  standalone: true,
  imports: [CommonModule, RouterLink],
  templateUrl: './search-flights.component.html',
  styleUrl: './search-flights.component.css'
})
export class SearchFlightsComponent implements OnInit {

  searchResult: FlightRm[] = [

  ]

  constructor(private flightService: FlightService) { }

  ngOnInit(): void { }

  search() {
    this.flightService.searchFlight({})
      .subscribe({
        next: res => this.searchResult = res,
        error: err => this.handleError(err)
      })
  }

  private handleError(err: any) {
    console.log("Response Error: ", err.Status, " -- ", err.statusText);
  }


}
