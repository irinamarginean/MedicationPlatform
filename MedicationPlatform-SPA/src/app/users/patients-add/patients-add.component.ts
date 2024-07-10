import { Component, OnInit } from '@angular/core';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { UserService } from 'src/app/_services/user.service';
import { Patient } from '../../_models/patient';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { ActivatedRoute } from '@angular/router';


@Component({
  selector: 'app-patients-add',
  templateUrl: './patients-add.component.html',
  styleUrls: ['./patients-add.component.css']
})
export class PatientsAddComponent implements OnInit {

  model: Patient = {} as Patient;

  constructor(public userService: UserService, private alertify: AlertifyService, private router: Router, private route: ActivatedRoute) { }

  ngOnInit() {
  }

  registerPatient(registerPatientForm: NgForm) {
    this.model.role = 'Patient';
    this.userService.registerPatient(this.model).subscribe(patient => {
      this.alertify.success(`${this.model.firstName} ${this.model.lastName} was added successfully!`);
      this.router.navigate(['/patients']);
    },
    error => {
      this.alertify.error(error);
    });
  }

}
