import { CanActivateFn, Router } from '@angular/router';
import { AuthService } from './auth.service';
import { inject } from '@angular/core';

export const authGuard: CanActivateFn = (route, state) => {
  const authService = inject(AuthService);
  const router = inject(Router);
  // allow if logged in
  if (authService.currentUser) return true;
  // redirect if not logged in
  return router.navigate(['/register-passenger', {requestedUrl: state.url}]);
};
