import { Routes } from '@angular/router';
import { SearchFlightsComponent } from './search-flights/search-flights.component';
import { BookFlightComponent } from './book-flight/book-flight.component';

export const routes: Routes = [
    { path: '', component: SearchFlightsComponent },
    { path: 'search-flights', component: SearchFlightsComponent },
    { path: 'book-flight/:flightId', component: BookFlightComponent }
];
