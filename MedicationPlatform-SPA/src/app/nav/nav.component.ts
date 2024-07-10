import { Component, OnInit } from '@angular/core';
import { AlertifyService } from '../_services/alertify.service';
import { AuthService } from '../_services/auth.service';
import { Router } from '@angular/router';
import { SignalRService } from '../_services/signalR.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {

  model: any = {};

  // tslint:disable-next-line: max-line-length
  constructor(public authService: AuthService, private alertify: AlertifyService, private router: Router, private signalR: SignalRService) { }

  ngOnInit() {
  }

  logIn() {
    this.authService.login(this.model).subscribe(next => {
        this.alertify.success('Logged in successfully!');
        this.router.navigate(['./']);
        this.signalR.startListening();
        this.signalR.getNotification();
      },
      error => {
        this.alertify.error(error);
      });
  }

  loggedIn() {
    return this.authService.loggedIn();
  }

  logOut() {
    this.authService.logOut();
    this.alertify.message('Logged out!');
    this.router.navigate(['./']);
  }
}
