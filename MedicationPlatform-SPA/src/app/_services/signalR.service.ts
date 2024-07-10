import { Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr';
import { Subject, Observable } from 'rxjs';
import { AlertifyService } from './alertify.service';

@Injectable({
  providedIn: 'root'
})
export class SignalRService {

  private signalRHubConnection: signalR.HubConnection;
  private notification: Subject<string>;
  private patientEmail: string;

  constructor(private aleritfy: AlertifyService) { }

   public startListening() {
      this.signalRHubConnection = new signalR.HubConnectionBuilder()
      .configureLogging(signalR.LogLevel.Debug)
      .withUrl('http://localhost:5001/notificationhub', {
      })
      .build();

      this.signalRHubConnection
          .start()
          .then(() => console.log('Connected'))
          .catch((error) => console.log(error));
      this.getNotification();
    }

    public getNotification() {
      this.signalRHubConnection.on('notification', (activity) => {
        this.aleritfy.confirm(activity, () => {});
      });
    }
}
