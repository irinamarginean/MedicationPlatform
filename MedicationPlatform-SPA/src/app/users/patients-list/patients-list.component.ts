import { Component, OnInit } from '@angular/core';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { UserService } from 'src/app/_services/user.service';
import { Patient } from '../../_models/patient';
import { ActivatedRoute } from '@angular/router';


@Component({
  selector: 'app-patients-list',
  templateUrl: './patients-list.component.html',
  styleUrls: ['./patients-list.component.css']
})
export class PatientsListComponent implements OnInit {

  patients: Patient[] = [];

  constructor(public userService: UserService, private aleritify: AlertifyService, private route: ActivatedRoute) { }

  ngOnInit() {
    this.loadPatients();
  }

  loadPatients() {
    this.userService.getPatients().subscribe(patients => {
      this.patients = patients;
    });
  }

  removePatient(id) {
    this.userService.removeUser(id).subscribe();
    location.reload();
  }
}
