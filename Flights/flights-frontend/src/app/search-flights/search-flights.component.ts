import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-search-flights',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './search-flights.component.html',
  styleUrl: './search-flights.component.css'
})
export class SearchFlightsComponent implements OnInit {

  searchResult: FlightRm[] = [
    {
      airline: "China Eastern",
      remainingSeats:150,
      departure: { time: Date.now().toString(), place: "Changsha" },
      arrival: { time: Date.now().toString(), place: "Shanghai" },
      price: "500",
    },
    {
      airline: "China Southern",
      remainingSeats: 150,
      departure: { time: Date.now().toString(), place: "Shanghai" },
      arrival: { time: Date.now().toString(), place: "Guangzhou" },
      price: "500",
    },
    {
      airline: "Lufthansa",
      remainingSeats: 300,
      departure: { time: Date.now().toString(), place: "Guangzhou" },
      arrival: { time: Date.now().toString(), place: "Munich" },
      price: "3000",
    }
  ]

  constructor() {

  }

  ngOnInit(): void {

  }


}

export interface FlightRm {
  airline: string;
  arrival: TimePlaceRm;
  departure: TimePlaceRm;
  price: string;
  remainingSeats: number;
}

export interface TimePlaceRm {
  place: string;
  time: string;
}
