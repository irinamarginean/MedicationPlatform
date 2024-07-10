import { Component, OnInit } from '@angular/core';
import { AuthService } from './_services/auth.service';
import { JwtHelperService } from '@auth0/angular-jwt';
import { Subscription } from 'rxjs';
import { SignalRService } from '../app/_services/signalR.service';
import { AlertifyService } from '../app/_services/alertify.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  private signalRSubscription: Subscription;
  public notification: string;
  public jwtHelper = new JwtHelperService();
  public signalRService;

  constructor(private authService: AuthService, private alertify: AlertifyService) {}

  ngOnInit(): void {
    const token = localStorage.getItem('token');

    if (token) {
      this.authService.decodedToken = this.jwtHelper.decodeToken(token);
    }

    this.signalRService = new SignalRService(this.alertify);
    this.signalRService.startListening();
  }
}
