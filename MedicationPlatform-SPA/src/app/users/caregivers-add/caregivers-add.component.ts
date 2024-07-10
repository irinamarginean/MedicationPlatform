import { Component, OnInit } from '@angular/core';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { UserService } from 'src/app/_services/user.service';
import { User } from '../../_models/user';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-caregivers-add',
  templateUrl: './caregivers-add.component.html',
  styleUrls: ['./caregivers-add.component.css']
})
export class CaregiversAddComponent implements OnInit {

  model: User = {} as User;

  constructor(public userService: UserService, private alertify: AlertifyService, private router: Router, private route: ActivatedRoute) { }

  ngOnInit() {
  }

  registerCaregiver(registerCaregiverForm: NgForm) {
    this.model.role = 'Caregiver';
    this.userService.registerUser(this.model).subscribe(patient => {
      this.alertify.success(`${this.model.firstName} ${this.model.lastName} was added successfully!`);
      this.router.navigate(['/caregivers']);
    },
    error => {
      console.log(error);
      this.alertify.error(error);
    });
  }
}
