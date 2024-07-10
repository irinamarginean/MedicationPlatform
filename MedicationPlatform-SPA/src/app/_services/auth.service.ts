import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { JwtHelperService } from '@auth0/angular-jwt';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  authUrl = environment.apiUrl + 'authentication/';
  jwtHelper = new JwtHelperService();
  decodedToken: any;

  constructor(private http: HttpClient) {
  }

  login(model: any) {
    return this.http.post(this.authUrl + 'login', model)
            .pipe(
              map((response: any) => {
                const user = response;
                if (user) {
                  localStorage.setItem('token', user.token);
                  this.decodedToken = this.jwtHelper.decodeToken(user.token);
                }
              })
            );
  }

  loggedIn() {
    const token = this.getToken();

    if (token === null) {
      return false; }

    return !this.jwtHelper.isTokenExpired(token);
  }

  logOut() {
    localStorage.removeItem('token');
  }

  getToken() {
    return localStorage.getItem('token');
  }

  getUserId(): string {
    return this.jwtHelper.decodeToken(this.getToken())['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier'];
  }


  isInRole(role): boolean {
    return this.jwtHelper.decodeToken(this.getToken())['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'].includes(role);
  }
}
