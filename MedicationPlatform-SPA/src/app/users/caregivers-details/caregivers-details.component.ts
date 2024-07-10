import { Component, OnInit } from '@angular/core';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { UserService } from 'src/app/_services/user.service';
import { User } from '../../_models/user';
import { Patient } from '../../_models/patient';
import { ActivatedRoute } from '@angular/router';
import { AuthService } from 'src/app/_services/auth.service';

@Component({
  selector: 'app-caregivers-details',
  templateUrl: './caregivers-details.component.html',
  styleUrls: ['./caregivers-details.component.css']
})
export class CaregiversDetailsComponent implements OnInit {

  model: User = {} as User;
  patients: Patient[] = [];

  constructor(public userService: UserService, private alertify: AlertifyService, private route: ActivatedRoute, private authService: AuthService,
    private activatedRoute: ActivatedRoute) { }

  ngOnInit() {
    this.loadCaregiver();
    this.loadPatients();
  }

  loadCaregiver() {
    this.userService.getCurrentCaregiver().subscribe(caregiver => {
      this.model = caregiver;
    });
  }

  loadPatients() {
    this.userService.getPatientsByCaregiver(this.authService.decodedToken['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier']).subscribe(response => {
      this.patients = response;
    });
  }
}
