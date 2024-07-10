import { Component, OnInit, Input } from '@angular/core';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { UserService } from 'src/app/_services/user.service';
import { Patient } from '../../_models/patient';
import { User } from '../../_models/user';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-patients-edit',
  templateUrl: './patients-edit.component.html',
  styleUrls: ['./patients-edit.component.css']
})
export class PatientsEditComponent implements OnInit {

  @Input() model: Patient = {} as Patient;
  caregivers: User[] = [];

  constructor(public userService: UserService, private alertify: AlertifyService, private route: ActivatedRoute, private router: Router) { }

  ngOnInit() {
    this.loadPatient();
    this.loadCaregivers();
    this.model.caregiverId = this.caregivers.filter((x: User) => x.id === this.model.caregiverId)[0]?.id;
  }

  loadPatient() {
    this.userService.getPatient(this.route.snapshot.params.id).subscribe(patient => {
      this.model = patient;
    });
  }

  loadCaregivers() {
    this.userService.getCaregivers().subscribe(caregivers => {
      this.caregivers = caregivers;
    });
  }

  editPatient(id, editPatientForm) {
    this.userService.updatePatient(this.model).subscribe(patient => {
      this.alertify.success('Patient was edited successfully!');
      this.router.navigate(['/patients']);
      console.log(this.model);
    }, error => {
      this.alertify.error(error);
    });
  }
}
