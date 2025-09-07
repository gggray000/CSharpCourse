import { Component } from '@angular/core';
import { RouterOutlet, RouterLink, RouterLinkActive } from '@angular/router';
import { AuthService } from './auth/auth.service';
import { CommonModule } from '@angular/common';
import { authGuard } from './auth/auth.guard';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, RouterLink, RouterLinkActive, CommonModule],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'flights-frontend';

  constructor(public authService: AuthService) { }

  get email(): string {
    return this.authService.currentUser?.email ?? '';
  }

  logout(email: string) {
    this.authService.logOut(email);
  }
}
