import { Component, OnInit } from '@angular/core';
import { AlertifyService } from '../../_services/alertify.service';
import { UserService } from '../../_services/user.service';
import { User } from '../../_models/user';
import { ActivatedRoute } from '@angular/router';


@Component({
  selector: 'app-caregivers-list',
  templateUrl: './caregivers-list.component.html',
  styleUrls: ['./caregivers-list.component.css']
})
export class CaregiversListComponent implements OnInit {

  caregivers: User[] = [];

  constructor(public userService: UserService, private aleritify: AlertifyService, private route: ActivatedRoute) { }

  ngOnInit() {
    this.loadCaregivers();
  }

  loadCaregivers() {
    this.userService.getCaregivers().subscribe(caregivers => {
      this.caregivers = caregivers;
    });
  }

  removeCaregiver(id) {
    this.userService.removeUser(id).subscribe();
    location.reload();
  }

}
