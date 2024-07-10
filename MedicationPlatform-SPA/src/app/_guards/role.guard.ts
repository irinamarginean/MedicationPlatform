import { Injectable } from '@angular/core';
import { CanActivate, Router, ActivatedRouteSnapshot } from '@angular/router';
import { AuthService } from '../_services/auth.service';
import { AlertifyService } from '../_services/alertify.service';

@Injectable({
  providedIn: 'root'
})
export class RoleGuard implements CanActivate {

  constructor(
      private authService: AuthService,
      private router: Router,
      private alertify: AlertifyService) {}

  canActivate(route: ActivatedRouteSnapshot): boolean {
    const expectedRole = route.data.expectedRole;

    console.log(route.data);
    console.log(this.authService.decodedToken['http://schemas.microsoft.com/ws/2008/06/identity/claims/role']);

    if (this.authService.loggedIn() &&
        this.authService.decodedToken['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'] === expectedRole) {
      return true;
    }

    this.alertify.error('You do not have access to this page!');
    this.router.navigate(['/home']);
    }
}