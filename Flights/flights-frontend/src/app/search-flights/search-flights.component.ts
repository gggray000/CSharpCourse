import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FlightService } from '../api/services';
import { FlightRm } from '../api/models';
import { RouterLink } from "@angular/router";
import { FormBuilder, ReactiveFormsModule } from '@angular/forms';

@Component({
  selector: 'app-search-flights',
  standalone: true,
  imports: [CommonModule, RouterLink, ReactiveFormsModule],
  templateUrl: './search-flights.component.html',
  styleUrl: './search-flights.component.css'
})
export class SearchFlightsComponent implements OnInit {

  searchResult: FlightRm[] = [

  ]

  constructor(
    private flightService: FlightService, 
    private fb: FormBuilder
  ) { }

  searchForm = this.fb.nonNullable.group({
    From: [''],
    To: [''],
    FromDate: [''],
    ToDate: [''],
    NumberOfPassenger: [1]
  });

  ngOnInit(): void {
    this.search();
   }

  search() {
    this.flightService.searchFlight(this.searchForm.value)
      .subscribe({
        next: res => this.searchResult = res,
        error: err => this.handleError(err)
      })
  }

  private handleError(err: any) {
    console.log("Response Error: ", err.Status, " -- ", err.statusText);
  }


}
